using System.Security.Claims;

namespace AnalysisData.Services.Abstraction;

public interface IPermissionService
{
    IEnumerable<string> GetPermission(ClaimsPrincipal userClaims);
}