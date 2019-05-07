using Aveneo.TestExcercise.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.ApplicationCore
{
    public interface IDataObjectService
    {
        Task SetFeaturesAsync(DataObject dataObject, ICollection<Feature> features);
    }
}
