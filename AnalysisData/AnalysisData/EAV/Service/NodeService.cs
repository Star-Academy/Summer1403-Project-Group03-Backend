﻿using AnalysisData.EAV.Service.Abstraction;
using AnalysisData.EAV.Service.Business.Abstraction;

namespace AnalysisData.EAV.Service;

public class NodeService : INodeService
{
    private readonly ICsvReaderService _csvReaderService;
    private readonly IHeaderProcessor _headerProcessor;
    private readonly INodeRecordProcessor _nodeRecordProcessor;

    public NodeService(ICsvReaderService csvReaderService, IHeaderProcessor headerProcessor, INodeRecordProcessor nodeRecordProcessor)
    {
        _csvReaderService = csvReaderService;
        _headerProcessor = headerProcessor;
        _nodeRecordProcessor = nodeRecordProcessor;
    }


    public async Task ProcessCsvFileAsync(IFormFile file, string id)
    {
        var csv = _csvReaderService.CreateCsvReader(file);
        var headers = _csvReaderService.ReadHeaders(csv);

        await _headerProcessor.ProcessHeadersAsync(headers, id);
        await _nodeRecordProcessor.ProcessRecordsAsync(csv, headers, id);
    }
    
}
