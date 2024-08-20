

namespace MuseumManagementSystem.Domain.Models
{
    public class ArtifactMaterial
    {
     

        public Guid ArtifactId { get; set; }
        public Artifact Artifact { get; set; }
        public Guid MaterialId { get; set; }
        public Material Material { get; set; }
        public bool IsImportantMaterial { get; set; }

        public void SetImportantMaterial()
        {
            IsImportantMaterial = true;
        }

        public void UnSetImportantMaterial()
        {
            IsImportantMaterial = false;
        }


    }
}
