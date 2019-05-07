using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aveneo.TestExcercise.ApplicationCore.Entities;

namespace Aveneo.TestExcercise.ApplicationCore.Services
{
    public class DataObjectService : IDataObjectService
    {
        private IRepository<DataObject> _dataObjects { get; }
        private IRepository<Feature> _features { get; }
        private IRepository<DataObjectFeature> _dataObjectFeatures { get; }

        public DataObjectService(IRepository<DataObject> dataObjects, IRepository<Feature> features, IRepository<DataObjectFeature> dataObjectFeatures)
        {
            _dataObjects = dataObjects;
            _features = features;
            _dataObjectFeatures = dataObjectFeatures;
        }


        public async Task SetFeaturesAsync(DataObject dataObject, ICollection<Feature> features)
        {
            var existingFeatures = await _dataObjectFeatures.WhereAsync(e =>
                e.DataObject.Id == dataObject.Id);

            await _dataObjectFeatures.DeleteAsync(existingFeatures.ToArray());

            var newFeatures = features.ToList().ConvertAll(f => new DataObjectFeature
            {
                DataObject = dataObject,
                Feature = f
            });
            await _dataObjectFeatures.CreateAsync(newFeatures.ToArray());

            dataObject.Features = newFeatures;
            await _dataObjects.UpdateAsync(dataObject);
        }
    }
}
