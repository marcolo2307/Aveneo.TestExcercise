using AutoMapper;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.Web.Services;
using Aveneo.TestExcercise.Web.ViewModels;

namespace Aveneo.TestExcercise.Web.Profiles.Resolvers
{
    public class IconResolver : IValueResolver<Feature, FeatureDetailsViewModel, string>
    {
        private IIconDecoder _iconDecoder { get; }

        public IconResolver(IIconDecoder iconDecoder)
        {
            _iconDecoder = iconDecoder;
        }


        public string Resolve(Feature source, FeatureDetailsViewModel destination, string destMember, ResolutionContext context)
        {
            return _iconDecoder.Decode(source.IconName);
        }
    }
}
