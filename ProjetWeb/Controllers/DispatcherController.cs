using Bogus.DataSets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetWeb.Models;
using ProjetWeb.ViewModels;
using System.Linq;
using System.Net;

namespace ProjetWeb.Controllers
{
    [Authorize(Roles = "Dispatcher")]
    public class DispatcherController : Controller
    {
        private readonly _DbContext _context;

        public DispatcherController(_DbContext context)
        {
            _context = context;
        }
        // GET: DispatcherController
        [ResponseCache(Duration = 3600)]
        public ActionResult Index()
        {

            var orderListViewModel = Order.GetUnconfirmed(_context).Result;

            return View(orderListViewModel);
        }

        // GET: DispatcherController/Details/5
        public ActionResult Details(int id)
        {
            var detailsOrderDispatcherViewModel = new DetailsOrderDispatcherViewModel();
            var InfoDetail =
                from Order in _context.Orders
                join User in _context.Customers on Order.Customer.Id equals User.Id
                where Order.Id == id
                select new
                {
                    User.Name,
                    User.Email,
                    User.Address,
                    User.BadPayer,
                    Order.Id,
                    Order.StartDate,
                    Order.EndDate,
                    Order.Source,
                    Order.Destination,
                    Order.Content,
                };
            detailsOrderDispatcherViewModel.NameCustomer = InfoDetail.FirstOrDefault().Name;
            detailsOrderDispatcherViewModel.EmailCustomer = InfoDetail.FirstOrDefault().Email;
            detailsOrderDispatcherViewModel.AddressCustomer = InfoDetail.FirstOrDefault().Address;
            detailsOrderDispatcherViewModel.BadPayer = InfoDetail.FirstOrDefault().BadPayer;
            detailsOrderDispatcherViewModel.IdOrder = InfoDetail.FirstOrDefault().Id;
            detailsOrderDispatcherViewModel.StartDate = InfoDetail.FirstOrDefault().StartDate;
            detailsOrderDispatcherViewModel.EndDate = InfoDetail.FirstOrDefault().EndDate;
            detailsOrderDispatcherViewModel.Source = InfoDetail.FirstOrDefault().Source;
            detailsOrderDispatcherViewModel.Destination = InfoDetail.FirstOrDefault().Destination;
            detailsOrderDispatcherViewModel.Content = InfoDetail.FirstOrDefault().Content;

            return View(detailsOrderDispatcherViewModel);
        }

        // GET: DispatcherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DispatcherController/Create
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

        // GET: DispatcherController/Edit/5
        public ActionResult Edit(int id)
        {
            TempData["message"] = "";

            var orderInfo = Order.GetById(id, _context.Orders).Result;

            ViewBag.Driver = Order.GetAvailbaleDrivers(_context.Orders, _context.Drivers, orderInfo).Result;

            ViewBag.Truck = Truck.GetAvailableTrucks(_context.Trucks, _context.Orders, orderInfo.FirstOrDefault()).Result;

            return View();
        }

        // POST: DispatcherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            int idTruck = int.Parse(collection["IdTruck"]);
            string idDriver = collection["IdDriver"];
            var LicenseTruck = await _context.Trucks.FindAsync(idTruck);
            var LicenseDriver = await _context.Drivers.FindAsync(idDriver);
            var order = await _context.Orders.FindAsync(id);

            if (LicenseTruck.Types == "CE" && LicenseDriver.License == "C")
            {
                var error = "Ce conducteur n'a pas le permis pour conduire ce camion";
                ViewBag.Message = error;
                return Edit(id);
            }

            var message = string.Format("Le conducteur {0} a été assigne à la livraison", LicenseDriver.Name);
            TempData["message"] = message;

            order.Truck = LicenseTruck;
            order.Driver = LicenseDriver;
            order.Accepted = true;
            _context.Update(order);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }

        // GET: DispatcherController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DispatcherController/Delete/5
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
