using MuseumManagementSystem.Application.DTOs;
using MuseumManagementSystem.Application.Contracts.Persistence.Base;

using MuseumManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MuseumManagementSystem.Application.Models;

namespace MuseumManagementSystem.Application.Contracts.Persistence
{
    public interface ISafesRepository : IGenericRepository<Safe>
    {
        Task<PagedResult<Safe>> GetSafesPagedByStowageId(Guid id, string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC");
        int GetArtifactCount (Guid id);
        bool IsInStowage(Guid id);
        bool IsNameAssigned(string name);
        bool IsNameAssigned(string name, Guid id);
        Task<PagedResult<Safe>> GetAllSafesPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC");
    }
}
