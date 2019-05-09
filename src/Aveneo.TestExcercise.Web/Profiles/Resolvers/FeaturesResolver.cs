using AutoMapper;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.ApplicationCore.Services;
using Aveneo.TestExcercise.Web.ViewModels;
using System.Collections.Generic;

namespace Aveneo.TestExcercise.Web.Profiles.Resolvers
{
    public class FeaturesResolver :
        IValueResolver<DataObject, DataObjectDetailsViewModel, ICollection<FeatureDetailsViewModel>>
    {
        private IDataObjectService _dataObjectService { get; }
        private IMapper _mapper { get; }

        public FeaturesResolver(IDataObjectService dataObjectService, IMapper mapper)
        {
            _dataObjectService = dataObjectService;
            _mapper = mapper;
        }

        public ICollection<FeatureDetailsViewModel> Resolve(
            DataObject source, 
            DataObjectDetailsViewModel destination, 
            ICollection<FeatureDetailsViewModel> destMember, 
            ResolutionContext context)
        {
            var featuresTask = _dataObjectService.GetFeaturesAsync(source);
            featuresTask.Wait();
            var features = featuresTask.Result;

            return _mapper.Map<ICollection<FeatureDetailsViewModel>>(features);
        }
    }
}
