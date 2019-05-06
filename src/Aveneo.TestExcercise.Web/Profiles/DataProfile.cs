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
            CreateMap<Feature, FeatureViewModel>().ReverseMap();
        }
    }
}
