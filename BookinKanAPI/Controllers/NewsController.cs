using BookinKanAPI.DTOs;
using BookinKanAPI.ServicesManage.NewsServiceManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookinKanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNews()
        {
            return Ok(await _newsService.getNews());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllNews()
        {
            return Ok(await _newsService.getAllNews());
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int Id)
        {
            var news = await _newsService.GetNewsById(Id);
            if (news == null) return BadRequest("Can't Find");
            return Ok(news);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUpdateNews([FromForm] NewsDTO newsDTO)
        {
            var news = await _newsService.CreateAndUpdateNews(newsDTO);

            if (news != null) return BadRequest(news);
            return Ok(StatusCodes.Status200OK);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteNewsById(int Id)
        {
            var result = await _newsService.DeleteNews(Id);

            if (result != null) return BadRequest(result);

            return Ok(StatusCodes.Status200OK);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> DeleteImageNewss(int id)
        {
            await _newsService.DeleteImageNews(id);
            return Ok(new { status = "Deleted", id, StatusCodes.Status200OK });
        }

    }
}
