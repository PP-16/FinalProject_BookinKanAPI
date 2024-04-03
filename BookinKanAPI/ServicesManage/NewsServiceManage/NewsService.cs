using AutoMapper;
using BookinKanAPI.Data;
using BookinKanAPI.DTOs;
using BookinKanAPI.Models;
using BookinKanAPI.ServicesManage.ImageServiceManage;
using Microsoft.EntityFrameworkCore;

namespace BookinKanAPI.ServicesManage.NewsServiceManage
{
    public class NewsService : INewsService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IImageCarsService _image;

        public NewsService(DataContext dataContext,IMapper mapper,IImageCarsService image)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _image = image;
        }

        public async Task<string> CreateAndUpdateNews(NewsDTO newsDTO)
        {
            (string errorMessage, List<string> imageNames) = await UploadImageAsync(newsDTO.FormFiles);
            if (!string.IsNullOrEmpty(errorMessage)) return errorMessage;

            var news = await _dataContext.News.AsNoTracking().Include(i=>i.ImageNews).FirstOrDefaultAsync(i => i.NewsId == newsDTO.NewsId);
            var mapNew = _mapper.Map<News>(newsDTO);
           
            if (news == null)
            {
                _dataContext.News.AddAsync(mapNew);
                await _dataContext.SaveChangesAsync();
                //จัดการไฟล์ในฐานข้อมูล
                if (imageNames.Count > 0)
                {
                    var images = new List<ImageNews>();
                    foreach (var image in imageNames)
                    {
                        images.Add(new ImageNews
                        {
                            NewsId = mapNew.NewsId,
                            Images = image
                        });
                    }
                    await _dataContext.ImageNews.AddRangeAsync(images);
                }
            }
            else
            {
                _dataContext.News.Update(mapNew);
                //ตรวจสอบและจัดการกับไฟล์ที่ส่งเข้ามาใหม่
                if (imageNames.Count > 0)
                {
                    var images = new List<ImageNews>();
                    foreach (var image in imageNames)
                    {
                        images.Add(new ImageNews
                        {
                            NewsId = mapNew.NewsId,
                            Images = image
                        });
                    }
                    //ลบไฟล์เดิม
                    var oldImages = await _dataContext.ImageNews.Where(p => p.NewsId == mapNew.NewsId).ToListAsync();
                    if (oldImages != null)
                    {
                        //ลบไฟล์ใน database
                        _dataContext.ImageNews.RemoveRange(oldImages);
                        //ลบไฟล์ในโฟลเดอร์

                        var files = oldImages.Select(p => p.Images).ToList();
                        await _image.DeleteFileImages(files);

                    }
                    //ใส่ไฟล์เข้าไปใหม่
                    await _dataContext.ImageNews.AddRangeAsync(images);
                }
            }
            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "error can't save news";

            return null;
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
                    ImgName = await _image.UploadImageswithPath(formFiles, "news");
                }
            }
            return (errorMessege, ImgName);
        }

        public async Task<string> DeleteNews(int Id)
        {
            var news = await _dataContext.News.FirstOrDefaultAsync(i => i.NewsId == Id);
            if (news != null)
            {
                _dataContext.News.Remove(news);

                var result = await _dataContext.SaveChangesAsync();
                if (result <= 0) return "error can't  news";
                return null;
            }
            else
            {
                return "Can't find News";
            }

        }

        public async Task<List<News>> getNews()
        {
            return (await _dataContext.News.Include(i => i.ImageNews).ToListAsync());
        }

        public async Task<List<News>> GetNewsById(int Id)
        {
            var findNews = await _dataContext.News.Include(i=>i.ImageNews).Where(i => i.NewsId == Id).ToListAsync();
            if (findNews != null) { return findNews; } else return null;
        }
    }
}
