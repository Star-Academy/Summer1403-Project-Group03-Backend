using System.Security.Claims;
using AnalysisData.Exception;
using AnalysisData.Services;
using AnalysisData.Services.Abstraction;
using AnalysisData.UserDto.UserDto;
using AnalysisData.UserManage.LoginModel;
using AnalysisData.UserManage.NewPasswordModel;
using AnalysisData.UserManage.RegisterModel;
using AnalysisData.UserManage.ResetPasswordModel;
using AnalysisData.UserManage.UpdateModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnalysisData.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPermissionService _permissionService;

    public UserController(IUserService userService, IPermissionService permissionService)
    {
        _userService = userService;
        _permissionService = permissionService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var user = await _userService.LoginAsync(userLoginDto);
        return Ok(new { user.FirstName, user.LastName, user.ImageURL });
    }

    [HttpGet("permissions")]
    public IActionResult GetPermissions()
    {
        var userClaims = User;
        var permission = _permissionService.GetPermission(userClaims);
        var firstName = userClaims.FindFirstValue("firstname");
        var lastName = userClaims.FindFirstValue("lastname");
        var image = userClaims.FindFirstValue("image");

        return Ok(new { image, firstName, lastName, permission });
    }

    [Authorize(Roles = "admin")]
    [HttpPost("reset-passadword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        var userClaim = User;
        await _userService.ResetPasswordAsync(userClaim, resetPasswordDto.NewPassword, resetPasswordDto.ConfirmPassword);
        
        return Ok(new { massage = "success" });
    }

    [Authorize(Roles = "admin")]
    [HttpPost("upload-image")]
    public IActionResult UploadImage(IFormFile file)
    {
        var user = User;
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { massage = "No file uploaded." });
        }

        _userService.UploadImageAsync(user, file.FileName);

        return Ok(new { massage = "Uploaded successfully." });
    }

    [HttpPut("update-user")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
    {
        var user = User;
        await _userService.UpdateUserInformationAsync(user, updateUserDto);
        return Ok(new { massage = "updated successfully" });
    }

    [HttpPost("new-password")]
    public async Task<IActionResult> NewPassword([FromBody] NewPasswordDto newPasswordDto)
    {
        var userClaim = User;
        await _userService.NewPasswordAsync(userClaim, newPasswordDto.OldPassword,
            newPasswordDto.NewPassword,
            newPasswordDto.ConfirmPassword);
        return Ok(new { massage = "reset successfully" });
    }


    [HttpGet("get-user-information")]
    public async Task<IActionResult> GetUserInformation()
    {
        var user = User;
        var result = await _userService.GetUserAsync(user);
        if (result != null)
        {
            return Ok(new GetUserInformationDto()
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                PhoneNumber = result.PhoneNumber,
                Email = result.Email
            });
        }

        return BadRequest(new { message = "not found!" });
    }

    [HttpPost("logOut")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        return Ok(new { message = "Logout successful" });
    }
}