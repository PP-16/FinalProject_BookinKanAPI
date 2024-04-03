using AutoMapper;
using BookinKanAPI.Data;
using BookinKanAPI.DTOs;
using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.Models;
using BookinKanAPI.ServicesManage.ImageServiceManage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookinKanAPI.ServicesManage.SystemSettingServiceManage
{
    public class SystemSettingService : ISystemSettingService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IImageCarsService _image;

        public SystemSettingService(DataContext dataContext,IMapper mapper, IImageCarsService image)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _image = image;
        }


        public async Task<List<SystemSetting>> GetSystem()
        {
            return (await _dataContext.SystemSettings.Include(i => i.ImageSlide).ToListAsync());
        }

        public async Task<List<SystemSetting>> GetSystemById(int Id)
        {
            var findNews = await _dataContext.SystemSettings.Include(i => i.ImageSlide).Where(i => i.SystemSettingId == Id).ToListAsync();
            if (findNews != null) { return findNews; } else return null;
        }


        public async Task<string> CreateAndUpdateSystem(SystemSettingDTO settingDTO)
        {
            (string errorMessage, List<string> imageNames) = await UploadImageAsync(settingDTO.FormFiles,"imageSlide");
            if (!string.IsNullOrEmpty(errorMessage)) return errorMessage;

            var system = await _dataContext.SystemSettings.AsNoTracking().Include(i => i.ImageSlide).FirstOrDefaultAsync(i => i.SystemSettingId == settingDTO.SystemSettingId);
            var mapSetting = _mapper.Map<SystemSetting>(settingDTO);

            if (system == null)
            {
                _dataContext.SystemSettings.AddAsync(mapSetting);
                if (settingDTO.Logo != null)
                {
                    (errorMessage, imageNames) = await UploadImageAsync(settingDTO.Logo, "Logo");
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        return errorMessage;
                    }
                    mapSetting.Logo = imageNames[0];
                }
                if (settingDTO.ImagePrompay != null)
                {
                    (errorMessage, imageNames) = await UploadImageAsync(settingDTO.ImagePrompay, "prompay");
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        return errorMessage;
                    }
                    mapSetting.ImagePrompay = imageNames[0];
                }

                await _dataContext.SaveChangesAsync();
              
                //จัดการไฟล์ในฐานข้อมูล
                if (imageNames.Count > 0)
                {
                    var images = new List<ImageSlide>();
                    foreach (var image in imageNames)
                    {
                        images.Add(new ImageSlide
                        {
                            SystemSettingId = mapSetting.SystemSettingId,
                            ImageSlides = image
                        });
                    }
                    await _dataContext.ImageSlides.AddRangeAsync(images);
                }
            }
            else
            {
                _dataContext.SystemSettings.Update(mapSetting);
                //ตรวจสอบและจัดการกับไฟล์ที่ส่งเข้ามาใหม่
                if (imageNames.Count > 0)
                {
                    var images = new List<ImageSlide>();
                    foreach (var image in imageNames)
                    {
                        images.Add(new ImageSlide
                        {
                            SystemSettingId = mapSetting.SystemSettingId,
                            ImageSlides = image
                        });
                    }
                    //ลบไฟล์เดิม
                    var oldImages = await _dataContext.ImageSlides.Where(p => p.SystemSettingId == mapSetting.SystemSettingId).ToListAsync();
                    if (oldImages != null)
                    {
                        //ลบไฟล์ใน database
                        _dataContext.ImageSlides.RemoveRange(oldImages);
                        //ลบไฟล์ในโฟลเดอร์

                        var files = oldImages.Select(p => p.ImageSlides).ToList();
                        await _image.DeleteFileImages(files);

                    }
                    //ใส่ไฟล์เข้าไปใหม่
                    await _dataContext.ImageSlides.AddRangeAsync(images);
                }
            }
            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "error can't save news";

            return null;
        }


        public async Task<(string errorMessage, List<string> imageNames)> UploadImageAsync(IFormFileCollection formFiles, string folderName)
        {
            var errorMessege = string.Empty;
            var ImgName = new List<string>();

            if (_image.IsUpload(formFiles))
            {
                errorMessege = _image.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessege))
                {
                    ImgName = await _image.UploadImageswithPath(formFiles,folderName);
                }
            }
            return (errorMessege, ImgName);
        }

        public async Task<string> DeleteSetting(int Id)
        {
            var setting = await _dataContext.SystemSettings.FirstOrDefaultAsync(i => i.SystemSettingId == Id);
            if (setting != null)
            {
                _dataContext.SystemSettings.Remove(setting);

                var result = await _dataContext.SaveChangesAsync();
                if (result <= 0) return "error can't  setting";
                return null;
            }
            else
            {
                return "Can't find setting";
            }

        }



    }
}
