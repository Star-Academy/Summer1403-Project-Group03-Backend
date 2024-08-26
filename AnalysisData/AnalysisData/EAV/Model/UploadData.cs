﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AnalysisData.UserManage.Model;

namespace AnalysisData.EAV.Model;

public class UploadData
{
    [Key] 
    public int Id { get; set; }
    
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
    [Required]
    public DateTime UploadDate { get; set; }
    public string Category { get; set; }
    public string Name { get; set; }
    public ICollection<EntityNode> EntityNodes { get; set; }
}