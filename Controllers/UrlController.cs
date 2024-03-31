using Microsoft.AspNetCore.Mvc;
using paroot_api.Data.DTOs;
using paroot_api.Services;

namespace paroot_api.Controllers
{
    [ApiController]
    [Route("/")]
    public class UrlController : ControllerBase
    {

        private readonly UrlService _urlService;

        public UrlController(UrlService urlService)
        {
               _urlService = urlService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUrl(UrlDtoIn urlDtoIn)
        {
           var newUrl = await _urlService.CreateUrl(urlDtoIn);
               return CreatedAtAction(nameof(GetUrl), new { shortUrl = newUrl.ShortUrl }, newUrl);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UrlDtoOut>> GetUrl(int id)
        {
          var url = await _urlService.GetDtoById(id);
          if (url is null)
          {
              return NotFound();
          }
          return url;      
        }

     [HttpGet("/OriginalUrl/{*originalUrl}")]
        
         public async Task<ActionResult<UrlDtoOut>> GetUrlByOriginalUrl(string originalUrl)
         {
          Console.WriteLine(originalUrl);
               string originalUrl_ = Uri.UnescapeDataString(originalUrl);
               var url = await _urlService.GetDtoByOriginalUrl(originalUrl_);
               if (url is null)
               {
                     return NotFound();
               }
               return url;
          }


        [HttpGet("{shortUrl}")]

        public async Task<IActionResult> RedirectToOriginalUrl(string shortUrl)
        {
               var originalUrl = await _urlService.GetShortUrl(shortUrl);

               if (originalUrl is not null)
               {
                    return Redirect(originalUrl);
               }

               return NotFound();
         }


          
    }
}