using AutoMapper;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.Web.ViewModels;

namespace Aveneo.TestExcercise.Web.Profiles
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Feature, FeatureViewModel>();
        }
    }
}
