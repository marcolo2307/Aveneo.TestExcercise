using AutoMapper;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.Web.Profiles.Resolvers;
using Aveneo.TestExcercise.Web.ViewModels;

namespace Aveneo.TestExcercise.Web.Profiles
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Feature, FeatureDetailsViewModel>()
                .ForMember(e => e.Icon, o => o.MapFrom<IconResolver>())
                .ReverseMap();

            CreateMap<Feature, FeatureEditViewModel>()
                .ReverseMap();
        }
    }
}
