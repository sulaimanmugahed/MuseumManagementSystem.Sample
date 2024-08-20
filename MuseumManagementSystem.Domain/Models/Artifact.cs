using System.Text.Json.Serialization;

namespace MuseumManagementSystem.Domain.Models
{
    public class Artifact : BaseEntity, IAuditable, ISoftDeleteable
    {
        public string Name { get; set; }
        public long SerialNumber { get; set; }
        public string? OldMuseumNumber { get; set; }
        public string? NewMuseumNumber { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public string? Note { get; set; }
        public string? ImageLink { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Deleted { get; set; }
        public string? DeletedBy { get; set; }
        public Guid? BioDegId { get; set; }
        public BioDeg? BioDeg { get; set; }
        public Guid? TimePeriodId { get; set; }
        public TimePeriod? TimePeriod { get; set; }
        public List<ArtifactImage> Images { get; set; } = new();
        public List<ArtifactMaterial> ArtifactMaterials { get; set; } = new();
        public Guid? ArtifactTypeId { get; set; }
        public ArtifactType? ArtifactType { get; set; }
        public Guid? ArtifactConditionId { get; set; }
        public ArtifactCondition? ArtifactCondition { get; set; }
        public Guid? SafeId { get; set; }
        public Safe? Safe { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }



        public void AddImage(string url)
        {
            var image = new ArtifactImage
            { 
                Id = Guid.NewGuid(),
                Url = url, 
            };
               
            Images.Add(image);
        }

        public void SetCondition(Guid conditionId)
        {
            ArtifactConditionId = conditionId;
        }


        public void SetType(Guid typeId)
        {
            ArtifactTypeId = typeId;
        }


        public void SetBioDeg(Guid bioDegId)
        {
            BioDegId = bioDegId;
        }


        public void SetSafe(Guid safeId)
        {
            SafeId = safeId;
        }

        public void SetTimePeriod(Guid timePeriodId)
        {
            TimePeriodId = timePeriodId;
        }

        private void SetMaterials(List<Guid> materialIds)
        {
            foreach (var materialId in materialIds)
            {
                var artifactMaterial = new ArtifactMaterial { Artifact = this, MaterialId = materialId};

                if (artifactMaterial is not null)
                    ArtifactMaterials.Add(artifactMaterial);
            }

        }


        public string? GetMaterialsName()
        {
            var materialNames = ArtifactMaterials.Select(a => a.Material).Select(m => m.Name).ToList();
            if (materialNames.Count == 0)
                return null;

            string concatMaterialName = string.Join(", ", materialNames);
            return concatMaterialName;
        }

        public string? GetImportantMaterialName()
        {
            return ArtifactMaterials.FirstOrDefault(am => am.IsImportantMaterial)?.Material.Name;
        }


        public void AddMaterials(List<Guid> materialIds, Guid importantMaterial = default)
        {
            SetMaterials(materialIds);

            if (importantMaterial != Guid.Empty)
                SetImortantMaterial(importantMaterial);
        }


     

        public void UpdateMaterials(List<Guid> materialIds, Guid importantMaterialId = default)
        {
            var materialToRemove = ArtifactMaterials
                .Where(am => !materialIds.Contains(am.MaterialId)).ToList();

            if (materialToRemove.Count > 0)
                ArtifactMaterials.RemoveAll(am => materialToRemove.Contains(am));

            var materialsToAdd = materialIds
                .Except(ArtifactMaterials.Select(am => am.MaterialId)).ToList();

            if (materialsToAdd.Count != 0)
                SetMaterials(materialsToAdd);

            if (importantMaterialId != Guid.Empty)
                UpdateImportantMaterial(importantMaterialId);
        }


        private void SetImortantMaterial(Guid materialId)
        {
            var artifactMaterial = ArtifactMaterials
                .SingleOrDefault(x => x.MaterialId == materialId);

            artifactMaterial?.SetImportantMaterial();
        }


        private void UpdateImportantMaterial(Guid materialId)
        {
            var existImportantMaterial = ArtifactMaterials
                .SingleOrDefault(am => am.IsImportantMaterial);

            existImportantMaterial?.UnSetImportantMaterial();

            SetImortantMaterial(materialId);
        }




    }



}
