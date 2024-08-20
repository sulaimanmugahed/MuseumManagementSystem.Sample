using Microsoft.EntityFrameworkCore;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Persistence.Repositories.Base;


namespace MuseumManagementSystem.Persistence.Repositories
{
    public class StowagesRepository(ApplicationDbContext context) 
        : GenericRepository<Stowage>(context),
        IStowagesRepository
    {

        private readonly ApplicationDbContext _context =  context;

        public int GetArtifactCount(string id)
        {
            return _context.Artifacts.Include(a=>a.Safe).Where(a => a.Safe.StowageId.ToString() == id).Count();
        }

        public int GetSafesCount(string id)
        {
            return _context.Safes.Where(s=> s.StowageId.ToString() == id).Count();
        }

        public bool IsNameAssigned(string name)
        {
            return _context.Stowages.Any(s => s.Name == name);
        }

        public bool IsNameAssigned(string name, Guid id)
        {
            return _context.Stowages.Any(s => s.Name == name && s.Id != id);
        }

        public async Task<PagedResult<Stowage>> GetAllStowagesPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC")
        {

            var query = _context.Stowages.AsQueryable();

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


            return new PagedResult<Stowage>
            {
                Data = await query.ToListAsync(),
                Draw = 1,
                RecordsFiltered = totalCount,
                RecordsTotal = totalCount,

            };

        }
    }
}
