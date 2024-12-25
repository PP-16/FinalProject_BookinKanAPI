using Autofac;
using Autofac.Extensions.DependencyInjection;
using BookinKanAPI.Data;
using BookinKanAPI.ServicesManage.AuthenServiceManage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });


    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new
    TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new
    SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]))
    };
});


builder.Services.AddScoped<IAuthenService, AuthenService>();

builder.Services.AddDbContext<DataContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options => options.AddPolicy(name: MyAllowSpecificOrigins,
policy =>
{
    policy.AllowAnyOrigin()
    .AllowAnyMethod().
    AllowAnyHeader();
}));


//�� AutoRefac ŧ����¹���ѵ��ѵԡó�������� Service
builder.Host.UseServiceProviderFactory(new
AutofacServiceProviderFactory(containerBuilder =>
{
    containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
    .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Test"))
    .AsImplementedInterfaces();
}));

var app = builder.Build();

#region //���ҧ�ҹ�������ѵ��ѵ�
using var scope = app.Services.CreateScope(); //using ��ѧ� ҧҹ���稨ж١� ���¨ҡMemory
var context = scope.ServiceProvider.GetRequiredService<DataContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync(); //���ҧ DB ����ѵ��ѵԶ���ѧ�����
}
catch (Exception ex)
{
    logger.LogError(ex, "Problem migrating data");
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCors(MyAllowSpecificOrigins);
app.MapFallbackToController("Index", "Fallback");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
