using System.Security.Claims;
using AiJobEx1.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AiJobEx1.Web.Data;

public class HttpAccessContext : IAccessContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpAccessContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? GetCurrentUserId()
    {
        var userIdValue = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(userIdValue, out var userId) ? userId : null;
    }
}
