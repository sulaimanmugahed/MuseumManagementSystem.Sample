using AutoMapper;
using MuseumManagementSystem.Application.DTOs.Artifact;
using MuseumManagementSystem.Application.DTOs.ArtifactCondition;
using MuseumManagementSystem.Application.DTOs.ArtifactImage;
using MuseumManagementSystem.Application.DTOs.ArtifactMaterial;
using MuseumManagementSystem.Application.DTOs.ArtifactType;
using MuseumManagementSystem.Application.DTOs.BioDeg;
using MuseumManagementSystem.Application.DTOs.Material;
using MuseumManagementSystem.Application.DTOs.Safe;
using MuseumManagementSystem.Application.DTOs.TimePeriod;
using MuseumManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region Artifact Mappings
            CreateMap<Artifact, ArtifactDto>()
                .ForMember(dest => dest.ImportantMaterial, src => src.Ignore()).ReverseMap();
            CreateMap<Artifact, CreateArtifactDto>().ReverseMap();
            CreateMap<Artifact, UpdateArtifactDto>().ReverseMap();
            CreateMap<Artifact, ArtifactListDto>().ReverseMap();
            #endregion

            #region BioDeg Mappings
            CreateMap<BioDeg, BioDegDto>().ReverseMap();
            CreateMap<BioDeg, CreateBioDegDto>().ReverseMap();
            CreateMap<BioDeg, UpdateBioDegDto>().ReverseMap();
            #endregion

            #region TimePeriod Mappings
            CreateMap<TimePeriod, TimePeriodDto>().ReverseMap();
            CreateMap<TimePeriod, CreateTimePeriodDto>().ReverseMap();
            CreateMap<TimePeriod, UpdateTimePeriodDto>().ReverseMap();
            #endregion

            #region Safe Mappings
            CreateMap<Safe, SafeDto>().ReverseMap();
            #endregion

            #region ArtifactType Mappings
            CreateMap<ArtifactType, ArtifactTypeDto>().ReverseMap();
            #endregion

            #region ArtifactCondition Mappings
            CreateMap<ArtifactCondition, ArtifactConditionDto>().ReverseMap();
            #endregion

            #region ArtifactMaterial Mappings
            CreateMap<ArtifactMaterial, ArtifactMaterialDto>().ReverseMap();
            #endregion

            #region ArtifacImage Mappings
            CreateMap<ArtifactImage, ArtifactImageDto>().ReverseMap();
            #endregion

            #region Material Mappings
            CreateMap<Material, MaterialDto>().ReverseMap();
            #endregion
        }
    }
}
