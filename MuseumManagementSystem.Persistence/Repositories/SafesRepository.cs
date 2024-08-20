using Microsoft.EntityFrameworkCore;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Persistence.Repositories.Base;


namespace MuseumManagementSystem.Persistence.Repositories
{
    public class SafesRepository(ApplicationDbContext context) 
        : GenericRepository<Safe>(context),
        ISafesRepository
    {
        private readonly ApplicationDbContext _context = context;


        public bool IsInStowage(Guid id)
        {
            return _context.Safes.Include(s=> s.Stowage).Any(s=>s.StowageId == id);
            
        }

        public bool IsNameAssigned(string name)
        {
            return _context.Safes.Any(s=> s.Name == name);
        }



        public bool IsNameAssigned(string name, Guid id)
        {
            return _context.Safes.Any(s => s.Name == name && s.Id != id);
        }

        public async Task<PagedResult<Safe>> GetSafesPagedByStowageId(Guid id, string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC")
        {
            var query = _context.Safes.AsQueryable();

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


            return new PagedResult<Safe>
            {
                Data = await query.ToListAsync(),
                Draw = 1,
                RecordsFiltered = totalCount,
                RecordsTotal = totalCount,

            };

        }

        public async Task<PagedResult<Safe>> GetAllSafesPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC")
        {

            var query = _context.Safes.AsQueryable();

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


            return new PagedResult<Safe>
            {
                Data = await query.Include(s=> s.Stowage).ToListAsync(),
                Draw = 1,
                RecordsFiltered = totalCount,
                RecordsTotal = totalCount,

            };

        }

        public int GetArtifactCount(Guid id)
        {
            return _context.Artifacts.Count(a => a.SafeId == id);
        }
    }
}
