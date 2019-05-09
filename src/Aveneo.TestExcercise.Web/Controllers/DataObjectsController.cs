using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.ApplicationCore;
using AutoMapper;
using Aveneo.TestExcercise.Web.ViewModels;
using System.Collections.Generic;
using Aveneo.TestExcercise.ApplicationCore.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aveneo.TestExcercise.Web.Controllers
{
    public class DataObjectsController : Controller
    {
        private IRepository<DataObject> _dataObjects { get; }
        private IRepository<Feature> _features { get; }
        private IDataObjectService _dataObjectsService { get; }
        private IMapper _mapper { get; }

        public DataObjectsController(
            IRepository<DataObject> dataObjects,
            IRepository<Feature> features,
            IDataObjectService dataObjectsService,
            IMapper mapper)
        {
            _dataObjects = dataObjects;
            _features = features;
            _dataObjectsService = dataObjectsService;
            _mapper = mapper;
        }

        // GET: DataObjects
        public async Task<IActionResult> Index()
        {
            var dataObjects = await _dataObjects.GetAllAsync();
            var viewModels = _mapper.Map<ICollection<DataObjectDetailsViewModel>>(dataObjects);

            return View(viewModels);
        }

        // GET: DataObjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var dataObject = await _dataObjects.FindByIdAsync(id.Value);
            if (dataObject == null)
                return NotFound();

            var viewModel = _mapper.Map<DataObjectDetailsViewModel>(dataObject);

            return View(viewModel);
        }

        // GET: DataObjects/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new DataObjectEditViewModel();

            var features = await _features.GetAllAsync();

            viewModel.Features = features.ToList().ConvertAll<SelectListItem>(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = f.IconName
            });
            viewModel.SelectedFeatures = new int[] { };

            return View(viewModel);
        }

        // POST: DataObjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DataObjectEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dataObject = _mapper.Map<DataObject>(viewModel);

                await _dataObjects.CreateAsync(dataObject);

                var selectedIds = viewModel.SelectedFeatures?.ToArray();
                if (selectedIds == null)
                    selectedIds = new int[] { };
                var selectedFeatures = await _features.WhereAsync(e => selectedIds.Contains(e.Id));
                await _dataObjectsService.SetFeatures(dataObject, selectedFeatures);

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: DataObjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var dataObject = await _dataObjects.FindByIdAsync(id.Value);
            if (dataObject == null)
                return NotFound();

            var viewModel = _mapper.Map<DataObjectEditViewModel>(dataObject);

            return View(viewModel);
        }

        // POST: DataObjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DataObjectEditViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var dataObject = await _dataObjects.FindByIdAsync(id);

                _mapper.Map(viewModel, dataObject);

                await _dataObjects.UpdateAsync(dataObject);

                var selectedIds = viewModel.SelectedFeatures?.ToArray();
                if (selectedIds == null)
                    selectedIds = new int[] { };
                var selectedFeatures = await _features.WhereAsync(e => selectedIds.Contains(e.Id));
                await _dataObjectsService.SetFeatures(dataObject, selectedFeatures);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: DataObjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var dataObject = await _dataObjects.FindByIdAsync(id.Value);
            if (dataObject == null)
                return NotFound();

            var viewModel = _mapper.Map<DataObjectDetailsViewModel>(dataObject);

            return View(viewModel);
        }

        // POST: DataObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataObject = await _dataObjects.FindByIdAsync(id);

            if (dataObject == null)
                return NotFound();

            await _dataObjects.DeleteAsync(dataObject);
            return RedirectToAction(nameof(Index));
        }
    }
}
