using AnalysisData.EAV.Service;
using AnalysisData.Graph.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnalysisData.EAV.Controllers;
[ApiController]
[Route("api/[controller]")]
public class GraphEavController : ControllerBase
{
    private readonly IGraphServiceEav _graphServiceEav;

    public GraphEavController(IGraphServiceEav graphServiceEav)
    {
        _graphServiceEav = graphServiceEav;
    }

    [HttpGet("GetNodesPaginationEav")]
    public async Task<IActionResult> GetNodesAsync([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var paginatedNodes = await _graphServiceEav.GetNodesAsync(pageIndex, pageSize);
        return Ok(paginatedNodes);
    }
}