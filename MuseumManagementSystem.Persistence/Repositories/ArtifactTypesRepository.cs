using Microsoft.EntityFrameworkCore;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Persistence.Repositories.Base;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Persistence.Repositories
{
    public class ArtifactTypesRepository :
        GenericRepository<ArtifactType>,
        IArtifactTypesRepository
    {

        private readonly ApplicationDbContext _context;

        public ArtifactTypesRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;

        }

        public int GetArtifactCount(string id)
        {
            return _context.Artifacts.Where(a => a.ArtifactTypeId.ToString() == id).Count();
        }


        public bool IsNameAssigned(string name)
        {
            return _context.ArtifactTypes.Any(s => s.Name == name);
        }


      
        public bool IsNameAssigned(string name, Guid id)
        {
            return _context.ArtifactTypes.Any(s => s.Name == name && s.Id != id);
        }

  

        public async Task<PagedResult<ArtifactType>> GetAllArtifactTypesPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC")
        {

            var query = _context.ArtifactTypes.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(u =>
                u.Name.Contains(searchValue)
                );
            }

            int totalCount = await query.CountAsync();


            if (!string.IsNullOrEmpty(sortColumn))
                if (sortDirection == "desc")
                    query = query.OrderByDescending(a => EF.Property<object>(a, sortColumn));
                else
                    query = query.OrderBy(a => EF.Property<object>(a, sortColumn));


            if (start.HasValue)
                query = query.Skip(start.Value);

            if (length.HasValue)
                query = query.Take(length.Value);


            return new PagedResult<ArtifactType>
            {
                Data = await query.ToListAsync(),
                Draw = 1, 
                RecordsFiltered = totalCount,
                RecordsTotal = totalCount,

            };

        }

    }
}
