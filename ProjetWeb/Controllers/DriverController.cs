using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetWeb.Models;
using System.Security.Claims;
using static NuGet.Packaging.PackagingConstants;

namespace ProjetWeb.Controllers
{
    public class DriverController : Controller
    {

        private readonly _DbContext _context;
        private readonly UserManager<User> _userManager;

        public DriverController(_DbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: DriverController
        [ResponseCache(Duration = 3600)]
        public async Task<IActionResult> Index(string state = "all")
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = Driver.GetOrders(_context.Orders, userId, state);

            return View(orders);
        }


        // GET: DriverController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: DriverController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Confirm(int Id, string State, string CommentFailed, string CommentSucess)
        {
            
            var order = _context?.Orders?.Where(o => o.Id == Id)?.First();

            switch (State.ToLower())
            {
                case "delivered":
                    order.Delivered = true;
                    order.Comment = CommentSucess;
                    break;
                default:
                    order.Delivered = false;
                    order.Comment = CommentFailed;
                    break;
            }
            
            _context.SaveChanges();

            Index();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EndOrder(int id)
        {
            var order = _context?.Orders?.Where(o => o.Id == id).First();
            return View("Edit", order);
        }
    }
}
