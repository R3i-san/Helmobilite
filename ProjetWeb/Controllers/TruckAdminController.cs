using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using ProjetWeb.Models;

namespace ProjetWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TruckAdminController : Controller
    {

        private readonly _DbContext _context;

        public TruckAdminController(_DbContext context)
        {
            _context = context;
        }

        // GET: TruckAdminController
        [ResponseCache(Duration = 3600)]
        public ActionResult Index()
        {
            return View(_context.Trucks);
        }

        // GET: TruckAdminController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Trucks == null)
            {
                return NotFound();
            }

            var truck = await _context.Trucks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        // GET: TruckAdminController/Create
        public ActionResult Create()
        {
            initListSelect();
            return View();
        }

        // POST: TruckAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Brand","Model","Plate","Types","MaxWeight")] Truck truck)
        {
            var PlateExists = await _context.Trucks.FirstOrDefaultAsync(t => t.Plate == truck.Plate);
            if (PlateExists != null) {
                TempData["Message"] = "un camion a déjà cette plaque";
                return Create();
            }
            if (ModelState.IsValid)
            {
                _context.Add(truck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(truck);
        }

        // GET: TruckAdminController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            initListSelect();

            if (id == null || _context.Trucks == null)
            {
                return NotFound();
            }

            var truck = await _context.Trucks.FindAsync(id);
            if (truck == null)
            {
                return NotFound();
            }
            return View(truck);

        }

        // POST: TruckAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Brand", "Model", "Plate", "Types", "MaxWeight")] Truck truck)
        {
            if (id != truck.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(truck);
        }

        // POST: TruckAdminController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Trucks == null)
            {
                return NotFound();
            }

            var truck = await _context.Trucks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var truck = Truck.GetById(_context.Trucks, id).Result;
            var listOrder = _context.Orders
            .Where(o => o.Truck.Id == id)
            .ToList();

            if (truck != null)
            {
                foreach (var item in listOrder)
                {
                    item.Accepted = false;
                    item.Driver = null;
                    item.Truck = null;
                    _context.Orders.Update(item);
                }
                await _context.SaveChangesAsync();
                _context.Trucks.Remove(truck);
           }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void initListSelect() {
            List<SelectListItem> ListBrand = new List<SelectListItem>();
            List<SelectListItem> ListModel = new List<SelectListItem>();
            List<SelectListItem> ListTypes = new List<SelectListItem>();

            ListBrand.Add(new SelectListItem { Text = "Iveco", Value = "Iveco" });
            ListBrand.Add(new SelectListItem { Text = "Renault", Value = "Renault" });
            ListBrand.Add(new SelectListItem { Text = "Citroën", Value = "Citroën" });
            ListBrand.Add(new SelectListItem { Text = "Volkswagen", Value = "Volkswagen" });
            ViewBag.Brands = ListBrand;

            ListModel.Add(new SelectListItem { Text = "Eurocargo", Value = "Eurocargo" });
            ListModel.Add(new SelectListItem { Text = "Transporter", Value = "Transporter" });
            ListModel.Add(new SelectListItem { Text = "Plateau", Value = "Plateau" });
            ListModel.Add(new SelectListItem { Text = "Fourgon", Value = "Fourgon" });
            ViewBag.Models = ListModel;

            ListTypes.Add(new SelectListItem { Text = "C", Value = "C" });
            ListTypes.Add(new SelectListItem { Text = "CE", Value = "CE" });
            ViewBag.Types = ListTypes;
        }
    }
}
