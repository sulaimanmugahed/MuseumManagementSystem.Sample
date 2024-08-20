using MuseumManagementSystem.Application.Contracts.Persistence;


namespace MuseumManagementSystem.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IArtifactsRepository Artifacts { get; }
        IArtifactTypesRepository ArtifactTypes { get; }
        IMaterialsRepository Materials { get; }
        ISafesRepository Safes { get; }
        IStowagesRepository Stowages { get; }
        ITimePeriodsRepository TimePeriods { get; }
        IArtifactConditionsRepository ArtifactConditions { get; }
        IBioDegsRepository BioDegs { get; }
        IArtifactImagesRepository ArtifactImages { get; }
        Task SaveAsync();
    }
}
