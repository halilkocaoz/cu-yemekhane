using Cu.Yemekhane.Common.Models;
using Cu.Yemekhane.Common.Models.Data;
using Flurl;
using Flurl.Http;

namespace Cu.Yemekhane.Common.Services;

public interface IWebApiService
{
    Task<ApiResponse<List<Menu>>> GetMenus();
    Task<ApiResponse<Menu>> GetMenu(string date);
}

public class WebApiService : IWebApiService
{
    private const string BaseUrl = "https://cu-yemekhane.herokuapp.com/";

    public Task<ApiResponse<Menu>> GetMenu(string date)
        => BaseUrl.AppendPathSegment("menu").AppendPathSegment(date).GetJsonAsync<ApiResponse<Menu>>();

    public async Task<ApiResponse<List<Menu>>> GetMenus()
        => await BaseUrl.AppendPathSegment("menu").GetJsonAsync<ApiResponse<List<Menu>>>();
}