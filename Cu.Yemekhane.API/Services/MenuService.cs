using Cu.Yemekhane.Common.Models.Data;
using Cu.Yemekhane.Common;
using Microsoft.Extensions.Caching.Memory;
using Cu.Yemekhane.Common.Models;

namespace Cu.Yemekhane.API.Services;

public interface IMenuService
{
    ApiResponse<List<Menu>> GetMenus();
    ApiResponse<Menu> GetMenu(string date);
}

public class MenuService : IMenuService
{
    private readonly IWebScrapper _webScrapper;
    private readonly IMemoryCache _memoryCache;

    public MenuService(IWebScrapper webScrapper, IMemoryCache memoryCache)
    {
        _webScrapper = webScrapper;
        _memoryCache = memoryCache;
    }

    private List<Menu> getCachedMenus()
    {
        if (_memoryCache.TryGetValue("menus_cache", out List<Menu> menus)) 
            return menus;
        
        menus = _webScrapper.ScrapMenus();
        _memoryCache.Set("menus_cache", menus, new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddHours(6)
        });
        return menus;
    }

    public ApiResponse<Menu> GetMenu(string date)
    {
        ApiResponse<Menu> response = new();
        if (date.ParseableAsDate())
        {
            var menus = getCachedMenus();
            response.Data = menus.FirstOrDefault(x => x.Date == date);
        }
        else
            response.ErrorMessage = ErrorMessages.InvalidDateFormat;

        return response;
    }

    public ApiResponse<List<Menu>> GetMenus()
    {
        return new ApiResponse<List<Menu>>
        {
            Data = getCachedMenus()
        };
    }
}