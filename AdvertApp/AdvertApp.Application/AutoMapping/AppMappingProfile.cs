using AutoMapper;
using AdvertApp.Domain.Entities;
using AdvertApp.Contracts.Models.ViewModels;

namespace AdvertApp.AutoMapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Advert, AdvertViewModel>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Audit.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Audit.UpdatedAt))
            .ForMember(dest => dest.Image, src => src.Ignore())
            .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => Path.GetFileName(src.FilePath)));

        CreateMap<AdvertModel, AdvertViewModel>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Audit_CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Audit_UpdatedAt))
            .ForMember(dest => dest.Image, src => src.Ignore())
            .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => Path.GetFileName(src.FilePath)));
    }
}
