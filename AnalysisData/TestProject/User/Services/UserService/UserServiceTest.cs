﻿using System.Security.Claims;
using AnalysisData.Services.Business.Abstraction;
using AnalysisData.UserManage.LoginModel;
using AnalysisData.UserManage.UpdateModel;
using NSubstitute;

namespace TestProject.User.Services.UserService;

public class UserServiceTests
{
    private readonly IUserManager _userManager;
    private readonly IPasswordManager _passwordManager;
    private readonly ILoginManager _loginManager;
    private readonly AnalysisData.Services.UserService _sut;

    public UserServiceTests()
    {
        _userManager = Substitute.For<IUserManager>();
        _passwordManager = Substitute.For<IPasswordManager>();
        _loginManager = Substitute.For<ILoginManager>();
        _sut = new AnalysisData.Services.UserService(_userManager, _passwordManager, _loginManager);
    }

    [Fact]
    public async Task ResetPasswordAsync_ShouldReturnTrue_WhenPasswordResetSucceeds()
    {
        // Arrange
        var userClaim = new ClaimsPrincipal();
        var user = new AnalysisData.UserManage.Model.User();
        _userManager.GetUserAsync(userClaim).Returns(Task.FromResult(user));
        _passwordManager.ResetPasswordAsync(user, "password", "password")
            .Returns(Task.FromResult(true));

        // Act
        var result = await _sut.ResetPasswordAsync(userClaim, "password", "password");

        // Assert
        Assert.True(result);
        await _userManager.Received(1).GetUserAsync(userClaim);
        await _passwordManager.Received(1).ResetPasswordAsync(user, "password", "password");
    }

    [Fact]
    public async Task NewPasswordAsync_ShouldReturnTrue_WhenPasswordChangeSucceeds()
    {
        // Arrange
        var userClaim = new ClaimsPrincipal();
        var user = new AnalysisData.UserManage.Model.User();
        _userManager.GetUserAsync(userClaim).Returns(Task.FromResult(user));
        _passwordManager.NewPasswordAsync(user, "oldPassword", "newPassword", "newPassword")
            .Returns(Task.FromResult(true));

        // Act
        var result = await _sut.NewPasswordAsync(userClaim, "oldPassword", "newPassword", "newPassword");

        // Assert
        Assert.True(result);
        await _userManager.Received(1).GetUserAsync(userClaim);
        await _passwordManager.Received(1).NewPasswordAsync(user, "oldPassword", "newPassword", "newPassword");
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnUser_WhenLoginSucceeds()
    {
        // Arrange
        var userLoginDto = new UserLoginDto { userName = "test", password = "password" };
        var user = new AnalysisData.UserManage.Model.User();
        _loginManager.LoginAsync(userLoginDto).Returns(Task.FromResult(user));

        // Act
        var result = await _sut.LoginAsync(userLoginDto);

        // Assert
        Assert.Equal(user, result);
        await _loginManager.Received(1).LoginAsync(userLoginDto);
    }

    [Fact]
    public async Task GetUserAsync_ShouldReturnUser_WhenUserIsFound()
    {
        // Arrange
        var userClaim = new ClaimsPrincipal();
        var user = new AnalysisData.UserManage.Model.User();
        _userManager.GetUserAsync(userClaim).Returns(Task.FromResult(user));

        // Act
        var result = await _sut.GetUserAsync(userClaim);

        // Assert
        Assert.Equal(user, result);
        await _userManager.Received(1).GetUserAsync(userClaim);
    }

    [Fact]
    public async Task UpdateUserInformationAsync_ShouldReturnTrue_WhenUpdateSucceeds()
    {
        // Arrange
        var userClaim = new ClaimsPrincipal();
        var updateUserDto = new UpdateUserDto { FirstName = "John", LastName = "Doe" };
        var user = new AnalysisData.UserManage.Model.User();
        _userManager.GetUserAsync(userClaim).Returns(Task.FromResult(user));
        _userManager.UpdateUserInformationAsync(user, updateUserDto).Returns(Task.FromResult(true));

        // Act
        var result = await _sut.UpdateUserInformationAsync(userClaim, updateUserDto);

        // Assert
        Assert.True(result);
        await _userManager.Received(1).GetUserAsync(userClaim);
        await _userManager.Received(1).UpdateUserInformationAsync(user, updateUserDto);
    }
}