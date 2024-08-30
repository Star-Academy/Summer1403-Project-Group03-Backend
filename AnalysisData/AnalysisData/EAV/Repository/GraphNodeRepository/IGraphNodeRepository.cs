﻿using AnalysisData.EAV.Model;

namespace AnalysisData.EAV.Repository.Abstraction;

public interface IGraphNodeRepository
{
    Task<IEnumerable<dynamic>> GetNodeAttributeValueAsync(int id);
    Task<bool> IsNodeAccessibleByUser(string userName, int nodeId);
    Task<IEnumerable<EntityNode>> GetEntityNodesForAdminAsync();
    Task<IEnumerable<EntityNode>> GetNodeContainSearchInputForAdminAsync(string input);
    Task<IEnumerable<EntityNode>> GetNodeStartsWithSearchInputForAdminAsync(string input);
    Task<IEnumerable<EntityNode>> GetNodeEndsWithSearchInputForAdminAsync(string input);
    Task<IEnumerable<EntityNode>> GetEntityNodesForAdminWithCategoryIdAsync(int categoryId);
    Task<IEnumerable<EntityNode>> GetEntityNodeForUserWithCategoryIdAsync(string userGuidId, int categoryId);
    Task<IEnumerable<EntityNode>> GetEntityNodesForUserAsync(string userGuidId);
    Task<IEnumerable<EntityNode>> GetNodeContainSearchInputForUserAsync(string username, string input);
    Task<IEnumerable<EntityNode>> GetNodeStartsWithSearchInputForUserAsync(string username, string input);
    Task<IEnumerable<EntityNode>> GetNodeEndsWithSearchInputForUserAsync(string username, string input);
}