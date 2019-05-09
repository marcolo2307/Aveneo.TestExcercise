using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aveneo.TestExcercise.ApplicationCore.Entities;

namespace Aveneo.TestExcercise.ApplicationCore.Services.Implementations
{
    public class DataObjectService : IDataObjectService
    {
        private IRepository<Feature> _features { get; }
        private IRepository<DataObjectFeature> _dataObjectFeatures { get; }

        public DataObjectService(
            IRepository<Feature> features,
            IRepository<DataObjectFeature> dataObjectFeatures)
        {
            _features = features;
            _dataObjectFeatures = dataObjectFeatures;
        }

        public async Task<ICollection<Feature>> GetFeaturesAsync(DataObject dataObject)
        {
            var dataObjectFeatures = await _dataObjectFeatures
                .WhereAsync(dof => dof.DataObjectId == dataObject.Id);

            var featuresIds = dataObjectFeatures.Select(dof => dof.FeatureId);

            var features = await _features.WhereAsync(f => featuresIds.Contains(f.Id));

            return features;
        }

        public async Task SetFeaturesAsync(DataObject dataObject, IEnumerable<Feature> features)
        {
            var exitstingBindings = await _dataObjectFeatures
                .WhereAsync(dof => dof.DataObjectId == dataObject.Id);
            await _dataObjectFeatures.DeleteAsync(exitstingBindings);

            var bindings = features.ToList().ConvertAll<DataObjectFeature>(f => new DataObjectFeature
            {
                DataObjectId = dataObject.Id,
                FeatureId = f.Id
            });

            await _dataObjectFeatures.CreateAsync(bindings);
        }
    }
}
