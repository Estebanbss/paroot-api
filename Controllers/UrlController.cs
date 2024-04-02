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

          var existingUrl = await _urlService.GetDtoByOriginalUrl(urlDtoIn.OriginalUrl);
          if (existingUrl != null)
          {
               // Si la URL original ya existe, devolver la URL existente
               return Ok(existingUrl);
          }

          // Si la URL original no existe, crear una nueva URL
          var newUrl = await _urlService.CreateUrl(urlDtoIn);
          // Devolver un código 201 (Created) junto con la ubicación de la nueva URL
          // y el objeto de contenido (la URL recién creada)
          return CreatedAtAction(nameof(GetUrl), new { id = newUrl.Id }, newUrl);

     }

     [HttpGet("/all")]

     public async Task<ActionResult<IEnumerable<UrlDtoOut>>> GetAllUrls()
     {
          var urls = await _urlService.GetAllUrls();
          return urls.ToList();
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

         [HttpPut("{id:int}")]
           public async Task<IActionResult> UpdateUrl(int id, UrlDtoIn urlDtoIn)
           {
               if(id != urlDtoIn.Id)
                  return BadRequest(new {message=$"the id = {id} does not match the urlDtoId id {urlDtoIn.Id} in the request body"});

                  var url = await _urlService.GetById(id);
     
                  if (url is null)
                  {
                         return NotFound();
                  }

     
                  await _urlService.Update(id, urlDtoIn);
     
                  return NoContent();
            }
     
            [HttpDelete("{id:int}")]
     
            public async Task<IActionResult> DeleteUrl(int id)
            {
                  var url = await _urlService.GetById(id);
     
                  if (url is null)
                  {
                         return NotFound();
                  }
     
                  await _urlService.Delete(id);
     
                  return NoContent();
            }
          
    }
}