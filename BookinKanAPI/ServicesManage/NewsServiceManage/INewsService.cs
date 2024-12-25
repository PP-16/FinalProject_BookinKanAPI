using BookinKanAPI.DTOs;
using BookinKanAPI.Models;

namespace BookinKanAPI.ServicesManage.NewsServiceManage
{
    public interface INewsService
    {
        Task<List<News>> getNews();
        Task<List<News>> getAllNews();
        Task<string> CreateAndUpdateNews(NewsDTO newsDTO);
        Task<List<News>> GetNewsById(int Id);
        Task<object> DeleteImageNews(int imgId);
        Task<string> DeleteNews(int Id);
    }
}
