﻿using AnalysisData.EAV.Model;

namespace AnalysisData.EAV.Repository.EdgeRepository.Abstraction;

public interface IValueEdgeRepository
{
    Task AddAsync(ValueEdge entity);
    Task<IEnumerable<ValueEdge>> GetAllAsync();
    Task<ValueEdge> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}