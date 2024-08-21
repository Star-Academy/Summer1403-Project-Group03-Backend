using AnalysisData.EAV.Dto;
using AnalysisData.EAV.Model;

namespace AnalysisData.EAV.Service;

public interface IGraphServiceEav
{
    Task<PaginatedListDto> GetNodesAsync(int pageIndex, int pageSize);
    Task<Dictionary<string, object>> GetNodeInformation(string headerUniqueId);
    Task<PaginatedListDto> GetNodesPaginationAsync(int pageIndex, int pageSize);
    Task<(IEnumerable<NodeDto>, IEnumerable<EdgeDto>)> GetRelationalEdgeBaseNode(string id);
}