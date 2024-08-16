﻿
namespace AnalysisData.Repository.UserRepository.Abstraction;

public interface IUserRepository
{
    Task<User> GetUser(string userName);
    Task<IReadOnlyList<User>> GetAllUser();
    bool DeleteUser(string userName);
    void AddUser(User user);
}