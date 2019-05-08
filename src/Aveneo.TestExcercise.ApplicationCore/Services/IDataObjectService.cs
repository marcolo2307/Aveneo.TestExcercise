using Aveneo.TestExcercise.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.ApplicationCore.Services
{
    public interface IDataObjectService
    {
        Task<ICollection<Feature>> GetFeatures(DataObject dataObject);
        Task SetFeatures(DataObject dataObject, IEnumerable<Feature> features);
    }
}
