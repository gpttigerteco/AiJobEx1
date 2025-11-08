using AiJobEx1.Application.Common.Interfaces;
using AiJobEx1.Domain.Entities;
using AiJobEx1.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiJobEx1.Web.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : ApiControllerBase
{
    private readonly IApplicationDbContext _context;

    public UsersController(IApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<User>>> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var query = _context.Users.AsNoTracking().OrderByDescending(u => u.CreatedAt);
        var total = await query.LongCountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return Ok(new PagedResult<User>(items, page, pageSize, total));
    }
}
