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
    public interface IStowagesRepository : IGenericRepository<Stowage>
    {
        int GetArtifactCount (string id);
        int GetSafesCount (string id);
        bool IsNameAssigned(string name);
        bool IsNameAssigned(string name, Guid id);
        Task<PagedResult<Stowage>> GetAllStowagesPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC");
    }
}
