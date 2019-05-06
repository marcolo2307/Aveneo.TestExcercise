using AutoMapper;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.Web.ViewModels;
using System.Linq;

namespace Aveneo.TestExcercise.Web.Profiles
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<DataObject, DataObjectViewModel>()
                .ForMember(e => e.Latitude, o => o.MapFrom(s => s.Location.Latitude))
                .ForMember(e => e.Longitude, o => o.MapFrom(s => s.Location.Longitude))
                .ForMember(e => e.Features, o => o.Ignore());

            CreateMap<DataObjectViewModel, DataObject>()
                .ForMember(e => e.Location, o => o.MapFrom(s => new Geography
                {
                    Latitude = s.Latitude,
                    Longitude = s.Longitude
                }))
                .ForMember(e => e.Features, o => o.Ignore());

            CreateMap<Feature, FeatureViewModel>().ReverseMap();
        }
    }
}
