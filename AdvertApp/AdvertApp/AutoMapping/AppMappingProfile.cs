using AdvertApp.EF.Entities;
using AdvertApp.Models.ViewModels;
using AutoMapper;

namespace AdvertApp.AutoMapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Advert, AdvertViewModel>()
            .ForMember(dest => dest.Image, src => src.Ignore());
    }
}
