namespace MuseumManagementSystem.Web.Dtos
{
    public class ArtifactsByStowageIdExportDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long SerialNumber { get; set; }
        public string? OldMuseumNumber { get; set; }
        public string? NewMuseumNumber { get; set; }
        public int Count { get; set; }
        public string? Size { get; set; }
        public string? Note { get; set; }
        public string? ArtifactType { get; set; }
        public string? ArtifactCondition { get; set; }
        public string? BioDeg { get; set; }
        public string? TimePeriod { get; set; }

        public string? Safe { get; set; }

        public string? ImportantMaterial { get; set; }
        public string? Materials { get; set; }
    }
}
