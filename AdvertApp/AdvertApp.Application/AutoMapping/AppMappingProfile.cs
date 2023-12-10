using AutoMapper;
using AdvertApp.Domain.Entities;
using AdvertApp.Contracts.Models.ViewModels;

namespace AdvertApp.AutoMapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Advert, AdvertViewModel>()
            .ForMember(dest => dest.Image, src => src.Ignore());
    }
}
