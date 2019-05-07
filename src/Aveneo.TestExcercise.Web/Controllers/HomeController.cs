using Aveneo.TestExcercise.ApplicationCore;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.Web.Services;
using Aveneo.TestExcercise.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<DataObject> _dataObjects { get; }
        private IIconDecoder _iconDecoder { get; }

        public HomeController(IRepository<DataObject> dataObjects, IIconDecoder iconDecoder)
        {
            _dataObjects = dataObjects;
            _iconDecoder = iconDecoder;
        }

        public async Task<IActionResult> Index()
        {
            var models = await _dataObjects.GetAllAsync();

            var viewModels = models.ToList().ConvertAll(m =>
            {
                var sb = new StringBuilder();
                if (m.Features != null)
                {
                    foreach (var f in m.Features)
                    {
                        if (f.Feature == null)
                            continue;
                        var icon = _iconDecoder.Decode(f.Feature.IconName);
                        sb.Append(icon);
                    }
                }

                return new GridDataObjectViewModel
                {
                    Name = m.Name,
                    Price = m.Price,
                    Description = m.Description,
                    Features = sb.ToString()
                };
            });

            return View(viewModels);
        }
    }
}