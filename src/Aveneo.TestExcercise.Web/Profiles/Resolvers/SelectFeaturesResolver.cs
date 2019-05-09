using AutoMapper;
using Aveneo.TestExcercise.ApplicationCore;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Aveneo.TestExcercise.Web.Profiles.Resolvers
{
    public class SelectFeaturesResolver
        : IValueResolver<DataObject, DataObjectEditViewModel, ICollection<SelectListItem>>
    {
        private IRepository<Feature> _features { get; }

        public SelectFeaturesResolver(IRepository<Feature> features)
        {
            _features = features;
        }

        public ICollection<SelectListItem> Resolve(
            DataObject source,
            DataObjectEditViewModel destination,
            ICollection<SelectListItem> destMember,
            ResolutionContext context)
        {
            var featuresTask = _features.GetAllAsync();
            featuresTask.Wait();
            var features = featuresTask.Result;

            return features.ToList().ConvertAll<SelectListItem>(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = f.IconName
            });
        }
    }
}
