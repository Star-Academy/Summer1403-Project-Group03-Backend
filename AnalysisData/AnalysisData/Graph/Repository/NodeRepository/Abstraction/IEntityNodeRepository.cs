﻿using AnalysisData.EAV.Model;

namespace AnalysisData.EAV.Repository.NodeRepository.Abstraction;

public interface IEntityNodeRepository
{
    Task AddRangeAsync(IEnumerable<EntityNode> entityNodes); 
    Task AddAsync(EntityNode entity);
    Task<IEnumerable<EntityNode>> GetAllAsync();
    Task<EntityNode> GetByNameAsync(string id);
    Task DeleteAsync(int id);
    Task<EntityNode> GetByIdAsync(int id);
}