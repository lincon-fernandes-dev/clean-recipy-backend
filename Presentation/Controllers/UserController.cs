using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetById(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        var deletedUser = await _userService.DeleteById(id);
        if (deletedUser == null)
            return NotFound();
        return Ok(deletedUser);
    }
}
