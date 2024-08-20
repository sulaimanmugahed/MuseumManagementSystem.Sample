using AutoMapper;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Web.ViewModels;

namespace MuseumManagementSystem.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Artifacts Mapping
            CreateMap<Artifact, ArtifactViewModel>();

            CreateMap<ArtifactCreateViewModel, Artifact>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => int.Parse(src.Count ?? "1")));


            CreateMap<Artifact, ArtifactEditViewModel>()
                .ForMember(dest => dest.SerialNumber, src => src.MapFrom(src => src.SerialNumber.ToString()));


            CreateMap<ArtifactEditViewModel, Artifact>()
                .ForMember(dest => dest.SerialNumber, src => src.MapFrom(src => long.Parse(src.SerialNumber!)))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => int.Parse(src.Count ?? "1")));



            CreateMap<Artifact, ArtifactDetailsViewModel>()
                .ForMember(dest => dest.SerialNumber, src => src.MapFrom(src => src.SerialNumber.ToString()))
                .ForMember(dest => dest.ImportantMaterial, src => src.Ignore())
                .ForMember(dest => dest.ArtifactType, src => src.MapFrom(src => src.ArtifactType!.Name))
                .ForMember(dest => dest.ArtifactCondition, src => src.MapFrom(src => src.ArtifactCondition!.Name))
                .ForMember(dest => dest.Safe, src => src.MapFrom(src => src.Safe!.Name))
                .ForMember(dest => dest.TimePeriod, src => src.MapFrom(src => src.TimePeriod!.Name))
                .ForMember(dest => dest.BioDeg, src => src.MapFrom(src => src.BioDeg!.Name))
                .ForMember(dest => dest.DateCreated, src => src.MapFrom(src => src.DateCreated.ToString()))
                .ForMember(dest => dest.LastModifiedDate, src => src.MapFrom(src => src.LastModifiedDate.ToString()))
                .ForMember(dest => dest.Images, src => src.MapFrom(src => src.Images.Select(i => new ArtifactImageViewModel { Id = i.Id, Url = i.Url })));
            #endregion

            #region ArtifactConditionsMapping

            CreateMap<ArtifactCondition, ArtifactConditionViewModel>();
            #endregion

            #region ArtifactConditionsMapping
            CreateMap<ArtifactImage, ArtifactImageViewModel>().ReverseMap();
            #endregion


            #region ReportMapping

            CreateMap<Artifact, ReportViewModel>()
                .ForMember(dest => dest.ArtifactType, opt => opt.MapFrom(src => src.ArtifactType!.Name))
                .ForMember(dest => dest.ImportantMaterial, opt => opt.MapFrom(src => src.GetImportantMaterialName()))
                .ForMember(dest => dest.ArtifactCondition, opt => opt.MapFrom(src => src.ArtifactCondition!.Name))
                .ForMember(dest => dest.Safe, opt => opt.MapFrom(src => src.Safe!.Name))
                .ForMember(dest => dest.Stowage, opt => opt.MapFrom(src => src.Safe.Stowage.Name));





            #endregion
        }
    }
}
