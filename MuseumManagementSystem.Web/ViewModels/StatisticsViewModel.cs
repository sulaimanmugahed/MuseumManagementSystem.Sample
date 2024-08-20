namespace MuseumManagementSystem.Web.ViewModels
{
    public class StatisticsViewModel
    {
        public int TotalArtifactCount { get; set; }
        public int ArtifactWithoutOldMuseumNumberCount { get; set;}
        public int ArtifactWithoutNewMuseumNumberCount { get; set; }
        public int ArtifactWithoutSafeCount { get; set; }
        public int MaterialsCount { get; set; }
        public int TimePeriodsCount { get; set; }
        public int BiodegsCount { get; set; }
        public int ArtifactTypesCount { get; set; }
        public int ArtifactConditionsCount { get; set; }

    }
}
