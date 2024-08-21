﻿using AnalysisData.EAV.Dto;
using AnalysisData.EAV.Service.Abstraction;
using AnalysisData.Exception;
using Microsoft.AspNetCore.Mvc;

namespace AnalysisData.EAV.Controllers;


[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly INodeToDbService _nodeToDbService;
    private readonly IEdgeToDbService _edgeToDbService;

    
    public FileController(INodeToDbService nodeToDbService,IEdgeToDbService edgeToDbService)
    {
        _nodeToDbService = nodeToDbService;
        _edgeToDbService = edgeToDbService;
    }

    [HttpPost("upload-file-node")]
    public async Task<IActionResult> UploadNodeFile([FromForm] NodeUploadDto nodeUpload , string categoryFile)
    {
        var uniqueAttribute = nodeUpload.Header;
        var file = nodeUpload.File;

        if (file == null || file.Length == 0 || uniqueAttribute == null)
        {
            throw new NoFileUploadedException();
        }

        try
        {
            await _nodeToDbService.ProcessCsvFileAsync(file, uniqueAttribute, categoryFile); 
            return Ok(new
            {
                massage = "File uploaded successfully !"
            }); 
        }
        catch (System.Exception e)
        {
            return StatusCode(400, $"An error occurred while processing the file: {e.Message}");
        }
    }
    
    
    [HttpPost("upload-file-edge")]
    public async Task<IActionResult> UploadEdgeFile([FromForm] EdgeUploadDto edgeUploadDto)
    {
        var from = edgeUploadDto.From;
        var to = edgeUploadDto.To;
        var file = edgeUploadDto.File;

        if (file == null || file.Length == 0 || from == null || to==null)
        {
            throw new NoFileUploadedException();
        }

        try
        {
            await _edgeToDbService.ProcessCsvFileAsync(file, from,to); 
            return Ok(new
            {
                massage = "Node account saved successfully in the database."
            }); 
        }
        catch (System.Exception e)
        {
            return StatusCode(400, $"An error occurred while processing the file: {e.Message}");
        }
    }
}