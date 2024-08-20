

using Microsoft.EntityFrameworkCore;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Persistence.Repositories.Base;

namespace MuseumManagementSystem.Persistence.Repositories
{
    public class BioDegsRepository :
        GenericRepository<BioDeg>,
        IBioDegsRepository
    {
        private readonly ApplicationDbContext _context;

        public BioDegsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int GetArtifactCount(string id)
        {
            return _context.Artifacts.Where(a => a.BioDegId.ToString() == id).Count();
        }


        public bool IsNameAssigned(string name)
        {
            return _context.BioDegs.Any(b => b.Name == name);
        }


        public bool IsNameAssigned(string name, Guid id)
        {
            return _context.BioDegs.Any(s => s.Name == name && s.Id != id);
        }


        public bool IsContainArtifacts(Guid id)
        {
            return _context.Artifacts.Any(a => a.BioDegId == id);
        }

        public async Task<PagedResult<BioDeg>> GetAllBioDegsPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC")
        {

            var query = _context.BioDegs.AsQueryable();

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


            return new PagedResult<BioDeg>
            {
                Data = await query.ToListAsync(),
                Draw = 1,
                RecordsFiltered = totalCount,
                RecordsTotal = totalCount,

            };

        }

    }
}
