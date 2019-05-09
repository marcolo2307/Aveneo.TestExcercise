using Aveneo.TestExcercise.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.ApplicationCore.Services
{
    public interface IDataObjectService
    {
        Task<ICollection<Feature>> GetFeaturesAsync(DataObject dataObject);
        Task SetFeaturesAsync(DataObject dataObject, IEnumerable<Feature> features);
    }
}
