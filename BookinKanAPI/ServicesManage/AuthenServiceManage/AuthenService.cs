using BookinKanAPI.Data;
using BookinKanAPI.DTOs.AuthenDto;
using BookinKanAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;
using Azure.Core;
using BookinKanAPI.ServicesManage.ImageServiceManage;

namespace BookinKanAPI.ServicesManage.AuthenServiceManage
{
    public class AuthenService:IAuthenService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IImageCarsService _image;

        public AuthenService(DataContext context,IConfiguration config,IHttpContextAccessor httpContext,IImageCarsService image)
        {
            _context = context;
            _config = config;
            _httpContext = httpContext;
            _image = image;
        }
        private string GetUser() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            return Regex.IsMatch(email, pattern);
        }
        private string CreateToken(Passenger user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:Token"])); // ที่อยู่ securityKey
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature); // (1,2) 1. รหัสลับ 2. เข้ารหัส
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.PassengerName),
                new Claim(ClaimTypes.Role,user.Roles.RoleName)
            };

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
          
        }
        
        
        public Task<List<Passenger>> GetAllAccounts()
        {
            var user = _context.Passengers.Include(r => r.Roles).OrderByDescending(i => i.PassengerId).ToListAsync();
            return user;
        }
        public async Task<object> Register(RegisterDTO request)
        {

            var mail = await _context.Passengers.FirstOrDefaultAsync(m => m.Email == request.Email);
            if (mail != null) { return null; }

            var name = await _context.Passengers.FirstOrDefaultAsync(n => n.PassengerName == request.PassengerName);
            if (name != null) { return null; }

            if (!IsValidEmail(request.Email)) { return null; }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var passenger = new Passenger()
            {
                Email = request.Email,
                IDCardNumber = request.IDCardNumber,
                PassengerName = request.PassengerName,
                Password = passwordHash,
                Phone = request.Phone,
                RoleId = 2,
                isUse = true
                
            };
            if (request.ImagePassenger != null)
            {
                (string errorMessage, List<string> imageNames) = await UploadImageAsync(request.ImagePassenger);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    return errorMessage;
                }
                passenger.ImagePassenger = imageNames[0];
            }
            


            await _context.Passengers.AddAsync(passenger);
            var result = await _context.SaveChangesAsync();
            if (result <= 0) { return null; }
            return passenger;
        }

        public async Task<(string errorMessage, List<string> imageNames)> UploadImageAsync(IFormFileCollection formFiles)
        {
            var errorMessege = string.Empty;
            var ImgName = new List<string>();

            if (_image.IsUpload(formFiles))
            {
                errorMessege = _image.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessege))
                {
                    ImgName = await _image.UploadImageswithPath(formFiles, "userImage");
                }
            }
            return (errorMessege, ImgName);
        }
        public async Task<object> Login(LoginDTO request)
        {
            if (!IsValidEmail(request.Email)) return "format";


            var user = await _context.Passengers.Include(r => r.Roles).FirstOrDefaultAsync(e => e.Email == request.Email);
            if (user == null) return null;
            if (user.isUse == false) return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) return null;

            var passenger = new UserDTO()
            {
                PassengerId = user.PassengerId,
                //PassengerName = user.PassengerName,
                //Email = user.Email,
                //IDCardNumber = user.IDCardNumber,
                //Phone = user.Phone,
                //Password = user.Password,
                RoleId = user.RoleId,
                Token = CreateToken(user)
            };


            return passenger;
        }
        public async Task<string> ChangePassword(string NewPass, string checkNewPass)
        {

            var user = await _context.Passengers.FirstOrDefaultAsync(e => e.Email == GetUser());
            //var user = await _context.Passengers.FindAsync(PassengerId);
            if (user == null) return "not found";
            if (BCrypt.Net.BCrypt.Verify(NewPass, user.Password))
                return "It's same as the old password";
            if (NewPass != checkNewPass) return "It's not correct";
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(NewPass);
            user.Password = passwordHash;

            await _context.SaveChangesAsync();

            return null;
        }
        public async Task<object> ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["AppSettings:Token"]);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                var passengerIdClaim = principal.FindFirst(ClaimTypes.Email);
                var roleClaim = principal.FindFirst(ClaimTypes.Role);

                if (passengerIdClaim == null || roleClaim == null)
                {
                    return null;
                }

                var passengerId = passengerIdClaim.Value;
                var roleName = roleClaim.Value;
                var user = await _context.Passengers
                    .Include(r => r.Roles)
                    .FirstOrDefaultAsync(e => e.Email == passengerId);

                if (user == null || user.Roles.RoleName != roleName)
                {
                    return null; 
                }
                var userDto = new UserDTO()
                {
                    PassengerId = user.PassengerId,
                    PassengerName = user.PassengerName,
                    Email = user.Email,
                    IDCardNumber = user.IDCardNumber,
                    Phone = user.Phone,
                    Password = user.Password,
                    Token = token
                };

                return userDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string>CheckOldPass (string oldpass)
        {
            var user = await _context.Passengers.FirstOrDefaultAsync(e => e.Email == GetUser());
            if (user == null) return "not found";
            if (!BCrypt.Net.BCrypt.Verify(oldpass, user.Password)) return "wrong pass";
            return null;
        }

        public async Task<string> ChangeIsuse(int Id,bool isuse)
        {
            var checkIsuse = await _context.Passengers.FindAsync(Id);
            if (checkIsuse != null)
            {
                checkIsuse.isUse = isuse;
            }
            var result = await _context.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";
            return null;
        }

        public async Task<object>ChangeRole(int PassId,int RoleId)
        {
            var passenger = await _context.Passengers.FindAsync(PassId);
            if(passenger != null)
            {
                passenger.RoleId = RoleId;
            }
            var result = await _context.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";
            return null;

        }


        public async Task<List<Passenger>> GetAdmin()
        {
            var admin = await _context.Passengers.Where(r=>r.RoleId == 1).ToListAsync();
            if (admin == null) return null;
            return admin;
        }

        public async Task<object> RegisterAdmin(RegisterDTO request)
        {
            var mail = await _context.Passengers.FirstOrDefaultAsync(m => m.Email == request.Email);
            if (mail != null) { return null; }

            var name = await _context.Passengers.FirstOrDefaultAsync(n => n.PassengerName == request.PassengerName);
            if (name != null) { return null; }

            if (!IsValidEmail(request.Email)) { return null; }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var passenger = new Passenger()
            {
                Email = request.Email,
                IDCardNumber = request.IDCardNumber,
                PassengerName = request.PassengerName,
                Password = passwordHash,
                Phone = request.Phone,
                RoleId = 1,
                isUse = true
            };

            await _context.Passengers.AddAsync(passenger);
            var result = await _context.SaveChangesAsync();
            if (result <= 0) { return null; }
            return passenger;
        }

        //************************************************RoleManage*****************************************************************************************//


        public async Task<object> createRole (string rolename,string rolenameTH)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == rolename || r.RoleNameTH == rolenameTH);
            if (role != null) return null;

            var newrole = new Role()
            {
                RoleName = rolename,
                RoleNameTH = rolenameTH,
            };

            await _context.Roles.AddAsync(newrole);
            var result = await _context.SaveChangesAsync();
            if (result <= 0) { return null; }
            return newrole;

        }

        public async Task<object> getRole()
        {
            return await _context.Roles.OrderByDescending(i => i.RoleId).ToListAsync();
        }
        //public async Task<string> ChangeIsuseRole(int Id, bool isuse)
        //{
        //    var checkIsuse = await _context.Roles.FindAsync(Id);
        //    if (checkIsuse != null)
        //    {
        //        checkIsuse.isUse = isuse;
        //    }
        //    var result = await _context.SaveChangesAsync();
        //    if (result <= 0) return "Can't Update Sattus";
        //    return null;
        //}
    }
}
