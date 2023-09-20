using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ProjetWeb.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjetWeb.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date de début")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Date de fin")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Source")]
        public string Source { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "Contenu")]
        public String? Content { get; set; }

        public Truck? Truck { get; set; }

        [Required]
        [Display(Name = "Etat")]
        public Boolean Delivered { get; set; }
        [MaxLength(1000)]

        [Display(Name = "Commentaire")]
        public String? Comment { get; set; }
        
        public Boolean Accepted { get; set; }

        public Customer? Customer { get; set; }
        public Driver? Driver { get; set; }


        public static async Task<List<OrderListViewModel>> GetUnconfirmed(_DbContext context)
        {

            var orders = await context.Orders
                .Join(context.Customers, o => o.Customer.Id, u => u.Id, (o, u) => new { Order = o, User = u })
                .Where(o => o.Order.Accepted == false && o.Order.Comment == null)
                .Select(o => new OrderListViewModel
                {
                    Id = o.Order.Id,
                    StartDate = o.Order.StartDate,
                    EndDate = o.Order.EndDate,
                    Source = o.Order.Source,
                    Destination = o.Order.Destination,
                    Content = o.Order.Content,
                    NameCustomer = o.User.Name,
                    BadPayer = o.User.BadPayer
                })
                .ToListAsync();

            return orders;
        }

        public static async Task<IEnumerable<Order>> GetById(int id, DbSet<Order> DbOrders)
        {

            /*var orders = from Order in DbOrders
                         where Order.Id == id
                         select new { Order.StartDate, Order.EndDate };*/

            return DbOrders.Where(o => o.Id == id).ToList();

            
        }

        public static async Task<IEnumerable<SelectListItem>> GetAvailbaleDrivers(DbSet<Order> DbOrders, DbSet<Driver> DbDriver, IEnumerable<Order> orderInfo)
        {

            var orders = await DbDriver
                .Where(d => !DbOrders
                    .Any(o => o.Driver.Id == d.Id && (o.StartDate <= orderInfo.FirstOrDefault().StartDate && o.EndDate >= orderInfo.FirstOrDefault().StartDate
                                    || (o.StartDate <= orderInfo.FirstOrDefault().EndDate && o.EndDate >= orderInfo.FirstOrDefault().EndDate.AddHours(1)))))
                    .Select(driver => new SelectListItem { Text = driver.Name + " License: " + driver.License, Value = driver.Id })
                    .ToListAsync();

            return orders;
        }


        public static IEnumerable<Order> GetBy(StatFilterViewModel filter, DbSet<Order> DbOrders)
        {

            IEnumerable<Order> orders = new List<Order>();
            
            switch (filter.FilterName)
            {
                case "Driver":
                    orders = GetByDriver(filter.DriverName, DbOrders).Result;
                    break;
                case "Date":
                    orders = GetByDate(filter.DeliveryDate, DbOrders).Result;
                    break;
                case "Customer":
                    orders = GetByCustomer(filter.CustomerName, DbOrders).Result;
                    break;
            }

            return orders;
        }

        private static async Task<List<Order>> GetByTruck(int id, DbSet<Order> DbOrders)
        {
            return await DbOrders.Where(o => o.Truck.Id == id).ToListAsync();
        }


        private static async Task<IEnumerable<Order>> GetByDriver(string id, DbSet<Order> DbOrders)
        {

            var orders = await DbOrders.Where(o => o.Driver.Name == id).ToListAsync();
            return orders;
        }


        private  static async Task<IEnumerable<Order>> GetByCustomer(string id, DbSet<Order> DbOrders)
        {
            var orders = await DbOrders.Where(o => o.Customer.Name == id).ToListAsync();
            return orders;
        }

        private static async Task<IEnumerable<Order>> GetByDate(DateTime date, DbSet<Order> DbOrders)
        {
            var orders = await DbOrders.Where(o => o.StartDate.Equals(date) || o.EndDate.Equals(date)).ToListAsync();
            return orders;
        }

    }
}
