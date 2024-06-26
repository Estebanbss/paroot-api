
using Microsoft.EntityFrameworkCore;
using paroot_api.Data;
using paroot_api.Data.DTOs;
using paroot_api.Data.Models;

namespace paroot_api.Services;

public class UrlService
{
    private readonly ParootDbContext _context;

    public UrlService(ParootDbContext context)
    {
        _context = context;
    }

    public async Task<Url> CreateUrl(UrlDtoIn urlDtoIn)
    {
               var random = new Random();
               const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
               var randomStr = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());

               var url = new Url();
               
               url.OriginalUrl = urlDtoIn.OriginalUrl;
               url.ShortUrl = randomStr;
               url.CreatedAt = DateTime.Now;
               url.Clicks = 0;
               url.LastClickedAt = null;
               url.LastClickedCountry = null;
               
               _context.Urls.Add(url);
               await _context.SaveChangesAsync();

               return url;
    }
  
    public async Task<IEnumerable<UrlDtoOut>> GetAllUrls()
    {
        return await _context.Urls.Select(u => new UrlDtoOut
        {
            Id = u.Id,
            OriginalUrl = u.OriginalUrl,
            ShortUrl = u.ShortUrl,
            CreatedAt = u.CreatedAt,
            Clicks = u.Clicks,
            LastClickedAt = u.LastClickedAt,
            LastClickedCountry = u.LastClickedCountry
        }).ToListAsync();
    }
    
    public async Task<Url?> GetById(int id){
            return await _context.Urls.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UrlDtoOut?>GetDtoById(int id)
    {
      return await _context.Urls.Where(u => u.Id == id).Select(u => new UrlDtoOut
      {
          Id = u.Id,
          OriginalUrl = u.OriginalUrl,
          ShortUrl = u.ShortUrl,
          CreatedAt = u.CreatedAt,
          Clicks = u.Clicks,
          LastClickedAt = u.LastClickedAt,
          LastClickedCountry = u.LastClickedCountry
      }).SingleOrDefaultAsync();
    }

     public async Task<UrlDtoOut?> GetDtoByOriginalUrl(string OriginalUrl)
     {
     return await _context.Urls.Where(u => u.OriginalUrl == OriginalUrl).Select(u => new UrlDtoOut
     {
     Id = u.Id,
     OriginalUrl = u.OriginalUrl,
     ShortUrl = u.ShortUrl,
     CreatedAt = u.CreatedAt,
     Clicks = u.Clicks,
     LastClickedAt = u.LastClickedAt,
     LastClickedCountry = u.LastClickedCountry
     }).SingleOrDefaultAsync();
     }

     public async Task<string> GetShortUrl(string ShortUrl)
     {
     var url = await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == ShortUrl);
     return url!.OriginalUrl; // Retorna la URL corta si se encuentra, o null si no se encuentra ninguna URL con la URL original proporcionada
     }


     public async Task Update(int id, UrlDtoIn url)
     {
          var urlExist = await GetById(id);
          
          if (urlExist is not null)
          {
               urlExist.OriginalUrl = url.OriginalUrl;
               urlExist.ShortUrl = url.ShortUrl;
               urlExist.CreatedAt = url.CreatedAt;
               urlExist.Clicks = url.Clicks;
               urlExist.LastClickedAt = url.LastClickedAt;
               urlExist.LastClickedCountry = url.LastClickedCountry;
               await _context.SaveChangesAsync();
          }

          
     }

     public async Task Delete(int id)
     {
          var url = await GetById(id);
          if (url is not null)
          {
               _context.Urls.Remove(url);
               await _context.SaveChangesAsync();
          }
     }

}