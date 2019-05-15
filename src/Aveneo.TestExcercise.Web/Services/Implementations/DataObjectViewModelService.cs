using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Aveneo.TestExcercise.ApplicationCore;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.ApplicationCore.Services;
using Aveneo.TestExcercise.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aveneo.TestExcercise.Web.Services.Implementations
{
    public class DataObjectViewModelService : IDataObjectViewModelService
    {
        private IMapper _mapper { get; }
        private _IDataObjectService _dataObjectService { get; }
        private IRepository<Feature> _features { get; }

        public DataObjectViewModelService(
            IMapper mapper,
            _IDataObjectService dataObjectService,
            IRepository<Feature> features)
        {
            _mapper = mapper;
            _dataObjectService = dataObjectService;
            _features = features;
        }

        public async Task<DataObjectDetailsViewModel> GetDetailsAsync(DataObject dataObject)
        {
            var defaultPhoto = await _dataObjectService.GetDefaultPhotoAsync(dataObject);

            var features = await _dataObjectService.GetFeaturesAsync(dataObject);
            var featuresViewModels = _mapper.Map<ICollection<FeatureDetailsViewModel>>(features);

            var viewModel = new DataObjectDetailsViewModel
            {
                Id = dataObject.Id,
                Name = dataObject.Name,
                Description = dataObject.Description,
                Price = dataObject.Price,
                Latitude = dataObject.Location.Latitude,
                Longitude = dataObject.Location.Longitude,
                DefaultPhoto = defaultPhoto?.Source,
                Features = featuresViewModels
            };

            return viewModel;
        }

        public async Task<ICollection<DataObjectDetailsViewModel>> GetDetailsAsync(IEnumerable<DataObject> dataObjects)
        {
            return await Task.WhenAll(dataObjects.Select(async e => await GetDetailsAsync(e)));
        }

        public async Task<DataObjectEditViewModel> GetEditAsync(DataObject dataObject)
        {
            var features = await _features.GetAllAsync();
            var featuresItems = features.ToList().ConvertAll<SelectListItem>(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = f.IconName
            });

            var dataObjectFeatures = await _dataObjectService.GetFeaturesAsync(dataObject);
            var selectedFeatures = dataObjectFeatures.Select(f => f.Id);

            var viewModel = new DataObjectEditViewModel
            {
                Id = dataObject.Id,
                Name = dataObject.Name,
                Description = dataObject.Description,
                Price = dataObject.Price,
                Latitude = dataObject.Location.Latitude,
                Longitude = dataObject.Location.Longitude,
                Features = featuresItems,
                SelectedFeatures =  selectedFeatures
            };

            return viewModel;
        }

        public async Task<DataObjectEditViewModel> GetEditAsync()
        {
            var features = await _features.GetAllAsync();
            var featuresItems = features.ToList().ConvertAll<SelectListItem>(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = f.IconName
            });

            var viewModel = new DataObjectEditViewModel
            {
                Features = featuresItems,
                SelectedFeatures = new int[] { }
            };

            return viewModel;
        }

        public async Task SaveEditAsync(DataObjectEditViewModel viewModel)
        {
            var dataObject = await _dataObjectService.DataObjects.FindByIdAsync(viewModel.Id);

            dataObject.Name = viewModel.Name;
            dataObject.Description = viewModel.Description;
            dataObject.Price = viewModel.Price;
            dataObject.Location = new Geography
            {
                Latitude = viewModel.Latitude,
                Longitude = viewModel.Longitude
            };

            await _dataObjectService.DataObjects.UpdateAsync(dataObject);

            if (viewModel.SelectedFeatures == null)
                viewModel.SelectedFeatures = new int[] { };

            var features = await _features.WhereAsync(f => viewModel.SelectedFeatures.Contains(f.Id));
            await _dataObjectService.SetFeaturesAsync(dataObject, features);
        }
    }
}
