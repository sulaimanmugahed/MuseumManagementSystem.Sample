using MuseumManagementSystem.Application.Contracts.Persistence.Base;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.Contracts.Persistence
{
    public interface IBioDegsRepository : IGenericRepository<BioDeg>
    {
        int GetArtifactCount(string id);
        bool IsNameAssigned(string name);
        bool IsNameAssigned(string name, Guid id);
        bool IsContainArtifacts(Guid id);
        Task<PagedResult<BioDeg>> GetAllBioDegsPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC");

    }
}
