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

            CreateMap<DataObject, DataObjectDetailsViewModel>()
                .ForMember(e => e.Latitude, o => o.MapFrom(s => s.Location.Latitude))
                .ForMember(e => e.Longitude, o => o.MapFrom(s => s.Location.Longitude))
                .ForMember(e => e.Features, o => o.MapFrom<FeaturesResolver>());

            CreateMap<DataObjectDetailsViewModel, DataObject>()
                .ForMember(e => e.Location, o => o.MapFrom(s => new Geography
                {
                    Latitude = s.Latitude,
                    Longitude = s.Longitude
                }));

            CreateMap<DataObject, DataObjectEditViewModel>()
                .ForMember(e => e.Latitude, o => o.MapFrom(s => s.Location.Latitude))
                .ForMember(e => e.Longitude, o => o.MapFrom(s => s.Location.Longitude));

            CreateMap<DataObjectEditViewModel, DataObject>()
                .ForMember(e => e.Location, o => o.MapFrom(s => new Geography
                {
                    Latitude = s.Latitude,
                    Longitude = s.Longitude
                })); 
        }
    }
}
