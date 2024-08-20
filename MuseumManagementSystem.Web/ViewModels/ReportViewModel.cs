namespace MuseumManagementSystem.Web.ViewModels
{
    public class ReportViewModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public long SerialNumber { get; set; }

        public string? OldMuseumNumber { get; set; }

        public string? NewMuseumNumber { get; set; }

        public string? Count { get; set; }

        public string? ArtifactType { get; set; }

        public string? ImportantMaterial { get; set; }

        public string? ImageLink { get; set; }

        public string? ArtifactCondition { get; set; }

        public string Safe { get; set; }
        public string Stowage { get; set; }

    }
}
