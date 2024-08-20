


using Microsoft.AspNetCore.Http;
using MuseumManagementSystem.Application.Contracts.Persistence;

using MuseumManagementSystem.Persistence.Repositories;

namespace MuseumManagementSystem.Persistence
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
       
        private readonly ApplicationDbContext _context = context;
        private TimePeriodsRepository? _timePeriodsRepository;
        private ArtifactImagesRepository? _artifactImagesRepository;
        private ArtifactTypesRepository? _artifactTypesRepository;
        private MaterialsRepository? _materialsRepository;
        private BioDegsRepository? _bioDegsRepository;
        private SafesRepository? _safesRepository;
        private StowagesRepository? _stowagesRepository;
        private ArtifactConditionsRepository? _artifactConditionsRepository;
        private ArtifactsRepository? _artifactsRepository;

        public ITimePeriodsRepository TimePeriods =>
            _timePeriodsRepository ??= new TimePeriodsRepository(_context);

        public IArtifactImagesRepository ArtifactImages =>
            _artifactImagesRepository ??= new ArtifactImagesRepository(_context);

        public IArtifactTypesRepository ArtifactTypes =>
            _artifactTypesRepository ??= new ArtifactTypesRepository(_context);

        public IMaterialsRepository Materials =>
           _materialsRepository ??= new MaterialsRepository(_context);

        public IBioDegsRepository BioDegs =>
           _bioDegsRepository ??= new BioDegsRepository(_context);

        public ISafesRepository Safes =>
           _safesRepository ??= new SafesRepository(_context);

        public IStowagesRepository Stowages =>
           _stowagesRepository ??= new StowagesRepository(_context);

        public IArtifactConditionsRepository ArtifactConditions =>
           _artifactConditionsRepository ??= new ArtifactConditionsRepository(_context);

        public IArtifactsRepository Artifacts =>
           _artifactsRepository ??= new ArtifactsRepository(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
