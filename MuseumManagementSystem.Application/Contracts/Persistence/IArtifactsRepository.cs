using MuseumManagementSystem.Application.Contracts.Persistence.Base;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.Contracts.Persistence
{
    public interface IArtifactsRepository : IGenericRepository<Artifact>
    {
        Task<Artifact?> GetDeletedArtifactAsync(Guid id);
        Task<bool> RecoveryAsync(Guid id);

        Task<List<Artifact>?> GetAllDeletedArtifactAsync();

        Task RemoveRange(Expression<Func<Artifact, bool>> criteria);
        Task<Artifact?> GetArtifactDetailAsync(Guid id);
        int GetArtifactCountForMaterial(Guid id);
        bool IsInSafe(Guid id);
        bool IsHasMaterial(Guid id);

        Task RemoveRange(IEnumerable<Artifact> artifacts);

        IQueryable<Artifact> GetDeletedArtifactsToDataTable(string searchValue,string sortColumn, string sortColumnDirection);
        bool IsSerialNumberAssigned(string serialNumber);
        bool IsSerialNumberAssigned(string serialNumber, Guid id);
        Task<PagedResult<Artifact>> GetArtifactsPagedBySafeId(Guid id, string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC");
        Task<PagedResult<Artifact>> GetAllArtifactsPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC", Expression<Func<Artifact, bool>>? criteria = null);

    }
}
