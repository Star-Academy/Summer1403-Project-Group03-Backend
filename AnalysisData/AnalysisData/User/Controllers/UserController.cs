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
    private readonly IUploadImageService _uploadImageService;

    public UserController(IUserService userService, IPermissionService permissionService,IUploadImageService uploadImageService)
    {
        _userService = userService;
        _permissionService = permissionService;
        _uploadImageService = uploadImageService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var user = await _userService.LoginAsync(userLoginDto);
        return Ok(new { user.FirstName, user.LastName, user.ImageURL });
    }

    [HttpGet("permissions")]
    public async Task<IActionResult> GetPermissions()
    {
        var userClaims = User;
        var permission = await _permissionService.GetPermission(userClaims);
        var firstName = userClaims.FindFirstValue("firstname");
        var lastName = userClaims.FindFirstValue("lastname");
        var image = userClaims.FindFirstValue("image");

        return Ok(new { image, firstName, lastName, permission });
    }
    // [Authorize(Policy = "gold")]
    // [Authorize(Roles = "admin")]
    [HttpPost("reset-passadword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        var userClaim = User;
        var check = await _userService.ResetPasswordAsync(userClaim, resetPasswordDto.NewPassword,
            resetPasswordDto.ConfirmPassword);
        if (check)
        {
            return Ok(new { massage = "success" });
        }

        return BadRequest(new { massage = "not success" });
    }

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var user = User;
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { massage = "No file uploaded." });
        }

        await _uploadImageService.UploadImageAsync(user, file);

        return Ok(new { massage = "Uploaded successfully." });
    }

    // [Authorize(Policy = "gold")]
    [HttpPut("update-user")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
    {
        var user = User;
        var updatedUser = await _userService.UpdateUserInformationAsync(user, updateUserDto);
        if (updatedUser != null)
        {
            return Ok(new { massage = "updated successfully" });
        }

        return BadRequest(new { massage = "not success" });
    }

    [HttpPost("new-password")]
    public async Task<IActionResult> NewPassword([FromBody] NewPasswordDto newPasswordDto)
    {
        var userClaim = User;
        var check = await _userService.NewPasswordAsync(userClaim, newPasswordDto.OldPassword,
            newPasswordDto.NewPassword,
            newPasswordDto.ConfirmPassword);
        if (check)
        {
            return Ok(new { massage = "reset successfully" });
        }

        return BadRequest(new { massage = "not success" });
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