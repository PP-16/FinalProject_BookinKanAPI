using BookinKanAPI.DTOs;
using BookinKanAPI.Models;

namespace BookinKanAPI.ServicesManage.NewsServiceManage
{
    public interface INewsService
    {
        Task<List<News>> getNews();
        Task<string> CreateAndUpdateNews(NewsDTO newsDTO);
        Task<List<News>> GetNewsById(int Id);
        Task<string> DeleteNews(int Id);
    }
}
