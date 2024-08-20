using Microsoft.EntityFrameworkCore;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Contracts.Persistence;

using MuseumManagementSystem.Application.Exceptions;
using MuseumManagementSystem.Application.Models.Identity;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MuseumManagementSystem.Application.Contracts;

namespace MuseumManagementSystem.Persistence.Repositories
{
    public class ArtifactsRepository : GenericRepository<Artifact>, IArtifactsRepository
    {

        private readonly ApplicationDbContext _context;

        public ArtifactsRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;

        }

        public async Task<Artifact?> GetDeletedArtifactAsync(Guid id)
        {
           return await _context.Artifacts.IgnoreQueryFilters()
                  .Include(a => a.ArtifactType)
                  .Include(a => a.Safe)
                  .Include(a => a.TimePeriod)
                  .Include(a => a.ArtifactCondition)
                  .Include(a => a.Images)
                  .Include(a => a.ArtifactMaterials)
                  .ThenInclude(ar => ar.Material)
                .FirstOrDefaultAsync(a=> a.Id == id);
        }

        public async Task<List<Artifact>?> GetAllDeletedArtifactAsync()
        {
            return await _context.Artifacts.
                IgnoreQueryFilters()
                 .Include(a => a.Images)
                 .Where(a=> a.IsDeleted)
                 .ToListAsync();
        }


        public async Task<bool> RecoveryAsync(Guid id)
        {
            var artifact = await _context.Artifacts.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == id);
            if (artifact is null)
                return false;

            _context.Entry(artifact).State = EntityState.Unchanged;
            return true;
        }

        public async Task<Artifact?> GetArtifactDetailAsync(Guid id)
        {
            return await _context.Artifacts
                  .Include(a => a.BioDeg)
                  .Include(a => a.ArtifactType)
                  .Include(a => a.Safe)
                  .Include(a => a.TimePeriod)
                  .Include(a => a.ArtifactCondition)
                  .Include(a => a.Images)
                  .Include(a => a.ArtifactMaterials)
                  .FirstOrDefaultAsync(a => a.Id == id);
        }



        public int GetArtifactCountForMaterial(Guid id)
        {
            var count = _context.ArtifactMaterials.Count(
                am => am.MaterialId == id
                );
            return count;
        }




        public IQueryable<Artifact> GetDeletedArtifactsToDataTable(string searchValue, string sortColumn, string sortColumnDirection)
        {
            var artifacts = _context.Artifacts.IgnoreQueryFilters().Where(a => a.IsDeleted)
                .Select(a => new Artifact
                {
                    Id = a.Id,
                    Name = a.Name,
                    SerialNumber = a.SerialNumber,
                    OldMuseumNumber = a.OldMuseumNumber,
                    NewMuseumNumber = a.NewMuseumNumber,
                    Count = a.Count,

                }).Where(a => string.IsNullOrEmpty(searchValue)
               ? true
               : (a.Name!.Contains(searchValue) || a.SerialNumber.ToString().Contains(searchValue)));

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                if (sortColumnDirection == "desc")
                    artifacts = artifacts.OrderByDescending(a => EF.Property<object>(a, sortColumn));
                else
                    artifacts = artifacts.OrderBy(a => EF.Property<object>(a, sortColumn));

            return artifacts;
        }


       

        public bool IsHasMaterial(Guid id)
        {
            return _context.ArtifactMaterials.Include(a => a.Material)
                .Any(am => am.MaterialId == id && am.ArtifactId != Guid.Empty);

        }

        public bool IsInSafe(Guid id)
        {
            return _context.Artifacts.Include(a => a.Safe)
                .Any(a => a.SafeId == id);
        }

       

        public async Task<PagedResult<Artifact>> GetArtifactsPagedBySafeId(Guid id, string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC")
        {
            var arifacts = _context.Artifacts.Where(a => a.SafeId == id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                arifacts = arifacts.Where(u =>
                u.Name.Contains(searchValue)
                || u.SerialNumber.ToString().Contains(searchValue)
                || u.OldMuseumNumber.Contains(searchValue)
                || u.NewMuseumNumber.Contains(searchValue)
                || u.Count.ToString().Contains(searchValue)
                );
            }

            int totalCount = await arifacts.CountAsync();

            if (!string.IsNullOrEmpty(sortColumn))
                if (sortDirection == "desc")
                    arifacts = arifacts.OrderByDescending(a => EF.Property<object>(a, sortColumn));
                else
                    arifacts = arifacts.OrderBy(a => EF.Property<object>(a, sortColumn));


            if (start.HasValue)
                arifacts = arifacts.Skip(start.Value);

            if (length.HasValue)
                arifacts = arifacts.Take(length.Value);

            return new PagedResult<Artifact>
            {
                Data = await arifacts.ToListAsync(),
                Draw = 1,
                RecordsFiltered = totalCount,
                RecordsTotal = totalCount,

            };
        }

      

        public bool IsSerialNumberAssigned(string serialNumber, Guid id)
        {
            return _context.Artifacts.Any(a => a.SerialNumber.ToString() == serialNumber && a.Id != id);
        }

        public bool IsSerialNumberAssigned(string serialNumber)
        {
            return _context.Artifacts.Any(a => a.SerialNumber.ToString() == serialNumber);
        }


        public async Task<PagedResult<Artifact>> GetAllArtifactsPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC",Expression<Func<Artifact,bool>>? criteria=null)
        {

            var arifacts = _context.Artifacts.AsQueryable();

           

            if (!string.IsNullOrEmpty(searchValue))
            {
                arifacts = arifacts.Where(u =>
                u.Name.Contains(searchValue)
                || u.SerialNumber.ToString().Contains(searchValue)
                || u.OldMuseumNumber.Contains(searchValue)
                || u.NewMuseumNumber.Contains(searchValue)
                || u.Count.ToString().Contains(searchValue)
                );
            }

            if (criteria is not null)
            {
                arifacts = arifacts.Where(criteria);
            }

            int totalCount = await arifacts.CountAsync();

            if (!string.IsNullOrEmpty(sortColumn))
                if (sortDirection == "desc")
                    arifacts = arifacts.OrderByDescending(a => EF.Property<object>(a, sortColumn));
                else
                    arifacts = arifacts.OrderBy(a => EF.Property<object>(a, sortColumn));


            if (start.HasValue)
                arifacts = arifacts.Skip(start.Value);

            if (length.HasValue)
                arifacts = arifacts.Take(length.Value);

            return new PagedResult<Artifact>
            {
                Data = await arifacts.ToListAsync(),
                Draw = 1,
                RecordsFiltered = totalCount,
                RecordsTotal = totalCount,

            };

        }

        public async Task RemoveRange(Expression<Func<Artifact, bool>> criteria)
        {
           await _context
                .Artifacts
                .IgnoreQueryFilters()
                .Where(criteria)
                .ExecuteDeleteAsync();
        }

        public  Task RemoveRange(IEnumerable<Artifact> artifacts)
        {
                 _context
                .Artifacts
                .RemoveRange(artifacts);

            return Task.CompletedTask;
        }
    }
}
