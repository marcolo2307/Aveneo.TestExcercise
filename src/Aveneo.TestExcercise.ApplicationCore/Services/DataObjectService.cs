using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aveneo.TestExcercise.ApplicationCore.Entities;

namespace Aveneo.TestExcercise.ApplicationCore.Services
{
    public class DataObjectService : IDataObjectService
    {
        private IRepository<DataObject> _dataObjects { get; }
        private IRepository<DataObjectFeature> _dataObjectFeatures { get; }

        public DataObjectService(IRepository<DataObject> dataObjects, IRepository<DataObjectFeature> dataObjectFeatures)
        {
            _dataObjects = dataObjects;
            _dataObjectFeatures = dataObjectFeatures;
        }

        public async Task SetFeaturesAsync(DataObject dataObject, IEnumerable<Feature> features)
        {
            var existingFeatures = await _dataObjectFeatures.WhereAsync(e =>
                e.DataObject.Id == dataObject.Id);

            await _dataObjectFeatures.DeleteAsync(existingFeatures);

            var newFeatures = features.ToList().ConvertAll(f => new DataObjectFeature
            {
                DataObject = dataObject,
                Feature = f
            });
            await _dataObjectFeatures.CreateAsync(newFeatures);

            dataObject.Features = newFeatures;
            await _dataObjects.UpdateAsync(dataObject);
        }

        public async Task<ICollection<Feature>> GetFeaturesAsync(DataObject dataObject)
        {
            var existingFeatures = await _dataObjectFeatures.WhereAsync(e =>
                e.DataObject.Id == dataObject.Id);

            var features = existingFeatures.Select(f => f.Feature);
            return features.ToList();
        }
    }
}
