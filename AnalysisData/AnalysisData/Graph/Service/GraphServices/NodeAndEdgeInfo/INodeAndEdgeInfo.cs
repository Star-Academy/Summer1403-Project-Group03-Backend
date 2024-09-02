using System.Security.Claims;

namespace AnalysisData.Graph.Service.GraphServices.NodeAndEdgeInfo;

public interface INodeAndEdgeInfo
{
    Task<Dictionary<string, string>> GetNodeInformationAsync(ClaimsPrincipal claimsPrincipal, int nodeId);
    Task<Dictionary<string, string>> GetEdgeInformationAsync(ClaimsPrincipal claimsPrincipal, int edgeId);
}