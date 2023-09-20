using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetWeb.Models;

namespace ProjetWeb.Controllers
{

    [Authorize(Roles = "Admin")]
    public class CustomerAdminController : Controller
    {

        _DbContext _context;

        public CustomerAdminController(_DbContext context)
        {
            _context = context;
        }

        // GET: CustomerAdminController
        [ResponseCache(Duration = 3600)]

        public ActionResult Index()
        {
            return View(Customer.GetActiveCustomer(_context).Result);
        }

        // GET: CustomerAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerAdminController/Create
        [ResponseCache(Duration = 3600)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerAdminController/Create
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

        // GET: CustomerAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerAdminController/Edit/5
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

        // GET: CustomerAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerAdminController/Delete/5
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

        public IActionResult TagAsBadPayer(string id)
        {
            Customer.TagAsBAdPayer(_context, id);
            return RedirectToAction(nameof(Index));
        }
    }
}
