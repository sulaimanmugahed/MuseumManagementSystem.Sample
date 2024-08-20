
using MuseumManagementSystem.Application.Contracts.Persistence.Base;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Domain.Models;


namespace MuseumManagementSystem.Application.Contracts.Persistence
{
    public interface IMaterialsRepository : IGenericRepository<Material>
    {
        bool IsNameAssigned(string name);
        bool IsNameAssigned(string name, Guid id);
        Task<PagedResult<Material>> GetAllMaterialsPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC");
    }
}
