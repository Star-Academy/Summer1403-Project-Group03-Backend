using AnalysisData.UserManage.LoginModel;
using AnalysisData.UserManage.RegisterModel;

namespace AnalysisData.Services;

public interface IUserService
{
    Task<string> Login(UserLoginModel userLoginModel);
    Task<bool> Register(UserRegisterModel userRegisterModel);
}