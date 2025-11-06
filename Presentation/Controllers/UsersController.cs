using Application.DTOs;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("CreateUser")]
    public async Task<IActionResult> Create(UserDTO dto)
    {
        var user = await _userService.Create(dto);
        if (user == null) return NoContent();
        return Ok(user);
    }
    [HttpGet("{email}")]
    public async Task<ActionResult<RecipeDTO>> GetUserByEmail(string email)
    {
        var user = await _userService.GetByEmailAsync(email);

        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserDTO dto)
    {
        var newUser = await _userService.Update(dto);
        if (newUser == null)
            return NoContent();
        return Ok(newUser);
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
