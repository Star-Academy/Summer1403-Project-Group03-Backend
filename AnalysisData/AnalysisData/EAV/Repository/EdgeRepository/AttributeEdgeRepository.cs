﻿using AnalysisData.Data;
using AnalysisData.EAV.Model;
using AnalysisData.EAV.Repository.EdgeRepository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace AnalysisData.EAV.Repository.EdgeRepository;

public class AttributeEdgeRepository : IAttributeEdgeRepository
{
    private readonly ApplicationDbContext _context;

    public AttributeEdgeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AttributeEdge entity)
    {
        await _context.AttributeEdges.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AttributeEdge>> GetAllAsync()
    {
        return await _context.AttributeEdges.ToListAsync();
    }

    public async Task<AttributeEdge> GetByIdAsync(int id)
    {
        return await _context.AttributeEdges.FindAsync(id);
    }

    public async Task<AttributeEdge> GetByNameAttributeAsync(string name)
    {
        return await _context.AttributeEdges.FirstOrDefaultAsync(x => x.Name == name);

    }
    
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.AttributeEdges.FindAsync(id);
        if (entity != null)
        {
            _context.AttributeEdges.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}