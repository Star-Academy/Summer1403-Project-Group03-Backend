﻿using AnalysisData.EAV.Model;
using AnalysisData.EAV.Repository.NodeRepository.Abstraction;
using AnalysisData.EAV.Service.Business.Abstraction;
using CsvHelper;
using Microsoft.Extensions.Azure;

namespace AnalysisData.EAV.Service.Business;

public class NodeRecordProcessor : INodeRecordProcessor
{
    private readonly IEntityNodeRepository _entityNodeRepository;
    private readonly int _batchSize;

    public NodeRecordProcessor(IEntityNodeRepository entityNodeRepository, int batchSize = 1000)
    {
        _entityNodeRepository = entityNodeRepository;
        _batchSize = batchSize;
    }

    public async Task<IEnumerable<EntityNode>> ProcessEntityNodesAsync(CsvReader csv, IEnumerable<string> headers, string id, int fileId)
    {
        var entityNodes = new List<EntityNode>();
        var batch = new List<EntityNode>();

        while (csv.Read())
        {
            var entityId = csv.GetField(id);
            if (string.IsNullOrEmpty(entityId)) continue;

            var entityNode = new EntityNode { Name = entityId, NodeFileReferenceId = fileId };
            entityNodes.Add(entityNode);
            batch.Add(entityNode);

            if (batch.Count >= _batchSize)
            {
                await InsertBatchAsync(batch);
                batch.Clear();
            }
        }

        if (batch.Any())
        {
            await InsertBatchAsync(batch);
        }

        return entityNodes;
    }

    private async Task InsertBatchAsync(IEnumerable<EntityNode> batch)
    {
        await _entityNodeRepository.AddRangeAsync(batch);
    }
    
}
