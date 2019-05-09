using AutoMapper;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.ApplicationCore.Services;
using Aveneo.TestExcercise.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Aveneo.TestExcercise.Web.Profiles.Resolvers
{
    public class FeaturesIdsResolver
        : IValueResolver<DataObject, DataObjectEditViewModel, IEnumerable<int>>
    {
        private IDataObjectService _dataObjectService { get; }

        public FeaturesIdsResolver(IDataObjectService dataObjectService)
        {
            _dataObjectService = dataObjectService;
        }

        public IEnumerable<int> Resolve(
            DataObject source,
            DataObjectEditViewModel destination,
            IEnumerable<int> destMember,
            ResolutionContext context)
        {
            var featuresTask = _dataObjectService.GetFeaturesAsync(source);
            featuresTask.Wait();
            var features = featuresTask.Result;

            return features.Select(f => f.Id);
        }
    }
}
