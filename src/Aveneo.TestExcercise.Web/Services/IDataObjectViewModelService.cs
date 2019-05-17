using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.Web.Services
{
    public interface IDataObjectViewModelService
    {
        Task<DataObjectDetailsViewModel> GetDetailsAsync(DataObject dataObject);
        Task<ICollection<DataObjectDetailsViewModel>> GetDetailsAsync(IEnumerable<DataObject> dataObjects);
        Task<DataObjectEditViewModel> GetEditAsync(DataObject dataObject);
        Task<DataObjectEditViewModel> GetEditAsync();
        Task SaveEditAsync(int id, DataObjectEditViewModel viewModel);
        Task SaveEditAsync(DataObjectEditViewModel viewModel);
    }
}
