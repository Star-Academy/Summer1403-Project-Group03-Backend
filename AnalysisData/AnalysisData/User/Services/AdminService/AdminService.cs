﻿using System.Security.Cryptography;
using System.Text;
using AnalysisData.Exception;
using AnalysisData.JwtService.abstractions;
using AnalysisData.Repository.RoleRepository.Abstraction;
using AnalysisData.Repository.UserRepository.Abstraction;
using AnalysisData.Services.Abstraction;
using AnalysisData.UserManage.Model;
using AnalysisData.UserManage.RegisterModel;
using AnalysisData.UserManage.RolePaginationModel;
using AnalysisData.UserManage.UpdateModel;
using AnalysisData.UserManage.UserPaginationModel;

namespace AnalysisData.Services;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidationService _validationService;
    private readonly IRoleRepository _roleRepository;
    private readonly IJwtService _jwtService;

    public AdminService(IUserRepository userRepository, IValidationService validationService,
        IRoleRepository roleRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _validationService = validationService;
        _roleRepository = roleRepository;
        _jwtService = jwtService;
    }

    public async Task UpdateUserInformationByAdminAsync(Guid id, UpdateAdminDto updateAdminDto)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        var checkUsername = await _userRepository.GetUserByUsernameAsync(updateAdminDto.Username);
        var checkEmail = await _userRepository.GetUserByEmailAsync(updateAdminDto.Email);

        if ((checkUsername != null && !user.Equals(checkUsername)) || (checkEmail != null && !user.Equals(checkEmail)))
            throw new DuplicateUserException();

        _validationService.EmailCheck(updateAdminDto.Email);
        _validationService.PhoneNumberCheck(updateAdminDto.PhoneNumber);
        var role = await _roleRepository.GetRoleByNameAsync(updateAdminDto.RoleName);
        if (role == null)
        {
            throw new RoleNotFoundException();
        }

        SetUpdatedInformation(user, updateAdminDto);
        await _jwtService.UpdateUserCookie(user.Username, false);
    }

    private async Task SetUpdatedInformation(User user, UpdateAdminDto updateAdminDto)
    {
        user.FirstName = updateAdminDto.FirstName;
        user.LastName = updateAdminDto.LastName;
        user.Email = updateAdminDto.Email;
        user.PhoneNumber = updateAdminDto.PhoneNumber;
        user.Username = updateAdminDto.Username;
        user.Role.RoleName = updateAdminDto.RoleName;
        await _userRepository.UpdateUserAsync(user.Id, user);
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var isDelete = await _userRepository.DeleteUserAsync(id);
        if (!isDelete)
            throw new UserNotFoundException();
        return true;
    }

    public async Task<int> GetUserCountAsync()
    {
        return await _userRepository.GetUsersCountAsync();
    }

    public async Task<List<UserPaginationDto>> GetAllUserAsync(int page, int limit)
    {
        var users = await _userRepository.GetAllUserPaginationAsync(page, limit);
        var paginationUsers = users.Select(x => new UserPaginationDto()
        {
            Guid = x.Id.ToString(), Username = x.Username, FirstName = x.FirstName, LastName = x.LastName,
            Email = x.Email, PhoneNumber = x.PhoneNumber, RoleName = x.Role.RoleName
        });
        return paginationUsers.ToList();
    }
}