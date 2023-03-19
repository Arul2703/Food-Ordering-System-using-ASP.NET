using FoodWebAppMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;

public class CachingFilter : IActionFilter
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachingFilter> _logger;

    public CachingFilter(IMemoryCache cache, ILogger<CachingFilter> logger)
    {
        _cache = cache;
        _logger = logger;
    }

   public void OnActionExecuting(ActionExecutingContext context)
    {
        var cacheKey = context.HttpContext.Request.Path;
        if (_cache.TryGetValue(cacheKey, out List<Products> cachedResult))
        {
            _logger.LogInformation($"Retrieved cached result for key '{cacheKey}'");
            var controller = context.Controller as Controller;
            var viewResult = new ViewResult { ViewName = "Index", ViewData = controller.ViewData };
            viewResult.ViewData.Model = cachedResult;
            context.Result = viewResult;
        }
    }



    public void OnActionExecuted(ActionExecutedContext context)
    {
        var cacheKey = context.HttpContext.Request.Path;
        if (!context.Canceled && context.Exception == null && context.Result is ViewResult viewResult && viewResult.Model is List<Products> products)
        {
            _cache.Set(cacheKey, products, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(20)));
            _logger.LogInformation($"Added result to cache with key '{cacheKey}'");
        }
    }
}
