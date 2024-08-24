﻿using System.ComponentModel.DataAnnotations;

namespace AnalysisData.EAV.Dto;

public class NodeUploadDto
{
    [Required]
    public string Header { get; set; }
    [Required]
    public IFormFile File { get; set; }
    [Required]
    public string Category { get; set; }
}