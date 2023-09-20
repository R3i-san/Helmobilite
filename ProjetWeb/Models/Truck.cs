using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace ProjetWeb.Models
{
    public class Truck
    {
        public int? Id { get; set; }

        [MaxLength(200)]
        [Required]
        public String? Brand { get; set; }

        [MaxLength(200)]
        [Required]
        public String? Model { get; set; }

        [MaxLength(200)]
        [Required]
        public String? Plate { get; set; }

        [MaxLength(200)]
        [Required]
        public String? Types { get; set; }

        [Required]
        public int MaxWeight { get; set; }

        public ICollection<Order>? Orders { get; set; }

        async public static Task<Truck?> GetById(DbSet<Truck> DbTrucks, int id)
        {
            return await DbTrucks.FindAsync(id);
        }


        async public static Task<IEnumerable<SelectListItem>> GetAvailableTrucks(
            DbSet<Truck> DbTrucks, DbSet<Order> DbOrders, Order orderInfo)
        {
            return await DbTrucks
                .Where(t => DbOrders
                        .Any(o => o.Truck.Id == t.Id &&
                        (o.StartDate <= orderInfo.StartDate && 
                        o.EndDate >= orderInfo.StartDate ||
                        (o.StartDate <= orderInfo.EndDate && 
                        o.EndDate >= orderInfo.EndDate))))
                .Select(truck => new SelectListItem {
                    Text = truck.Plate + " License: " + truck.Types + " Max tonne: " + truck.MaxWeight, Value = truck.Id + "" })
                .ToListAsync();
        }

    }


    

}
