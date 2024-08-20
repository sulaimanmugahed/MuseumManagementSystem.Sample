using MuseumManagementSystem.Application.DTOs;
using MuseumManagementSystem.Application.Contracts.Persistence.Base;

using MuseumManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MuseumManagementSystem.Application.Models;

namespace MuseumManagementSystem.Application.Contracts.Persistence
{
  
    public interface IArtifactTypesRepository : IGenericRepository<ArtifactType>
    {
        int GetArtifactCount(string id);
        bool IsNameAssigned(string name);
        bool IsNameAssigned(string name, Guid id);
        Task<PagedResult<ArtifactType>> GetAllArtifactTypesPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC");
    }
}
