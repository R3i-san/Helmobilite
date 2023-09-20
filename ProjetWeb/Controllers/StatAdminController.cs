using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetWeb.Models;
using ProjetWeb.ViewModels;

namespace ProjetWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatAdminController : Controller
    {
        private readonly _DbContext _context;
        


        public StatAdminController(_DbContext context) { 
            _context = context;
        }


        // GET: StatAdminController
        [ResponseCache(Duration = 3600)]
        public async Task<IActionResult> Index(string filter = "Driver")
        {
            //string filter = "Driver", string idDriver = "", string idCustomer = "", DateTime date = new DateTime()      
            ViewData["Filter"] = filter;
            return View(await _context.Orders.ToListAsync());

        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Filter([Bind("FilterName, DriverName, CustomerName, DeliveryDate")] StatFilterViewModel filter)
        {
            //string filter = "Driver", string idDriver = "", string idCustomer = "", DateTime date = new DateTime()      
            ViewData["Filter"] = filter.FilterName;
            var o = Order.GetBy(filter, _context.Orders);
            return View("Index", o);
        }


        // GET: StatAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StatAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StatAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StatAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StatAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StatAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
