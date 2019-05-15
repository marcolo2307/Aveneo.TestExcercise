using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aveneo.TestExcercise.Web.ViewModels;
using Aveneo.TestExcercise.ApplicationCore.Services;
using System.Linq;
using Aveneo.TestExcercise.Web.Services;

namespace Aveneo.TestExcercise.Web.Controllers
{
    public class DataObjectsController : Controller
    {
        private _IDataObjectService _dataObjectService { get; }
        private IDataObjectViewModelService _dataObjectViewModelService { get; }

        public DataObjectsController(
            _IDataObjectService dataObjectsService,
            IDataObjectViewModelService dataObjectsViewModelService)
        {
            _dataObjectService = dataObjectsService;
            _dataObjectViewModelService = dataObjectsViewModelService;
        }

        // GET: DataObjects
        public async Task<IActionResult> Index()
        {
            var dataObjects = await _dataObjectService.DataObjects.GetAllAsync();
            var viewModels = await _dataObjectViewModelService.GetDetailsAsync(dataObjects);

            return View(viewModels);
        }

        // GET: DataObjects
        public async Task<IActionResult> Grid()
        {
            var dataObjects = await _dataObjectService.DataObjects.GetAllAsync();
            var viewModels = await _dataObjectViewModelService.GetDetailsAsync(dataObjects);

            return View(viewModels);
        }

        // GET: DataObjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var dataObject = await _dataObjectService.DataObjects.FindByIdAsync(id.Value);
            if (dataObject == null)
                return NotFound();

            var viewModel = await _dataObjectViewModelService.GetDetailsAsync(dataObject);

            var photos = await _dataObjectService.GetPhotosAsync(dataObject);
            if (photos.Count > 0)
                photos = photos.Skip(1).ToList();
            var photosViewModels = photos.ToList().ConvertAll<PhotoViewModel>(p => new PhotoViewModel
            {
                Id = p.Id,
                Sequence = p.Sequence,
                Photo = p.Source
            });

            ViewData["Photos"] = photosViewModels;

            return View(viewModel);
        }

        // GET: DataObjects/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = await _dataObjectViewModelService.GetEditAsync();

            return View(viewModel);
        }

        // POST: DataObjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DataObjectEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _dataObjectViewModelService.SaveEditAsync(viewModel);

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: DataObjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var dataObject = await _dataObjectService.DataObjects.FindByIdAsync(id.Value);
            if (dataObject == null)
                return NotFound();

            var viewModel = await _dataObjectViewModelService.GetEditAsync(dataObject);

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
                await _dataObjectViewModelService.SaveEditAsync(viewModel);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: DataObjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var dataObject = await _dataObjectService.DataObjects.FindByIdAsync(id.Value);
            if (dataObject == null)
                return NotFound();

            var viewModel = await _dataObjectViewModelService.GetDetailsAsync(dataObject);

            var photos = await _dataObjectService.GetPhotosAsync(dataObject);
            if (photos.Count > 0)
                photos = photos.Skip(1).ToList();
            var photosViewModels = photos.ToList().ConvertAll<PhotoViewModel>(p => new PhotoViewModel
            {
                Id = p.Id,
                Sequence = p.Sequence,
                Photo = p.Source
            });

            ViewData["Photos"] = photosViewModels;

            return View(viewModel);
        }

        // POST: DataObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataObject = await _dataObjectService.DataObjects.FindByIdAsync(id);

            if (dataObject == null)
                return NotFound();

            await _dataObjectService.DeleteAsync(dataObject);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult EditPhotos(int id)
        {
            ViewData["ObjectId"] = id;
            return View();
        }
    }
}
