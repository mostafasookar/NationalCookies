using NationalCookies.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NationalCookies.Data.Services
{
    public class CookieService : ICookieService
    {
        private CookieContext _context;
        private IDistributedCache _cache;

        public CookieService(CookieContext context, IDistributedCache cache )
        {
            _context = context;
            _cache = cache;
        }

        public List<Cookie> GetAllCookies()
        {
            List<Cookie> cookies;


            var cachedCookies = _cache.GetString("cookies");
            if (!string.IsNullOrEmpty(cachedCookies)){
            
                cookies = JsonConvert.DeserializeObject<List<Cookie>>(cachedCookies);
            }
            else
            {
            //get the cookies from the database
            cookies = _context.Cookies.ToList();
            
            _cache.SetString("cookies", JsonConvert.SerializeObject(cookies), options);

            return cookies;
        }
    }
}
