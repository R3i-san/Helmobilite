using Bogus;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Text;

namespace ProjetWeb.Models
{

    public class _DbContext : IdentityDbContext<User>
    {
        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Dispatcher>? Dispatchers { get; set; }
        public DbSet<Driver>? Drivers { get; set; }
        public DbSet<Order>? Orders { get; set; }

        //public DbSet<OrderUser>? OrderUser { get; set; }
        public DbSet<Truck>? Trucks { get; set; }

        public _DbContext(DbContextOptions<_DbContext> creds) : base(creds) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /*builder.Entity<Order>()
                .HasMany<User>(o => o.Users)
                .WithMany(u => u.Orders)
                .UsingEntity(s => s.ToTable("UserOrder"));

            builder.Entity<Truck>()
                .HasMany(t => t.Orders)
                .WithOne(o => o.Truck)
                .HasForeignKey(t => t.IdTruck);*/

            //SeedOrderUser(builder);
        }

        private void SeedOrderUser(ModelBuilder modelBuilder) {
            modelBuilder.Entity<OrderUser>().HasData(new OrderUser { IdUser = "307472ff-5963-4a80-8c4d-c8d94bffb06d", IdOrder = 50 });
            modelBuilder.Entity<OrderUser>().HasData(new OrderUser { IdUser = "34b2037d-b0fe-4466-bf30-ee463b25ca7a", IdOrder = 51 });
            modelBuilder.Entity<OrderUser>().HasData(new OrderUser { IdUser = "563f2f04-bf22-4763-8e56-ac3602f0be6f", IdOrder = 52 });
            modelBuilder.Entity<OrderUser>().HasData(new OrderUser { IdUser = "d93c7c93-16e7-4672-9895-a86630ec4ce2", IdOrder = 53 });
            modelBuilder.Entity<OrderUser>().HasData(new OrderUser { IdUser = "dba2d86d-413a-4230-8149-d0ef39218e5c", IdOrder = 54 });
            modelBuilder.Entity<OrderUser>().HasData(new OrderUser { IdUser = "307472ff-5963-4a80-8c4d-c8d94bffb06d", IdOrder = 55 });
            modelBuilder.Entity<OrderUser>().HasData(new OrderUser { IdUser = "34b2037d-b0fe-4466-bf30-ee463b25ca7a", IdOrder = 56 });
            modelBuilder.Entity<OrderUser>().HasData(new OrderUser { IdUser = "563f2f04-bf22-4763-8e56-ac3602f0be6f", IdOrder = 57 });
            modelBuilder.Entity<OrderUser>().HasData(new OrderUser { IdUser = "d93c7c93-16e7-4672-9895-a86630ec4ce2", IdOrder = 58 });
            modelBuilder.Entity<OrderUser>().HasData(new OrderUser { IdUser = "dba2d86d-413a-4230-8149-d0ef39218e5c", IdOrder = 59 });

        }

        public void Seed(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < 10; i++)
            {
                var plate = $"1-{getRandomLetter()}-{Randomizer.Seed.Next(100, 999)}";
                var tonne = Randomizer.Seed.Next(10, 75) * 100;
                modelBuilder.Entity<Truck>().HasData(
                    new Truck
                    {
                        Id = i + 1,
                        Brand = "Iveco",                        
                        Model = "Eurocargo",
                        Plate = plate,
                        Types = getLicense(),
                        MaxWeight = tonne,
                    });
            }

        for (int i = 0; i < 10; i++)
        {
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = i + 50,
                    StartDate = new DateTime(2023, i + 1, (i + 5) % 30, (i + 12) % 24, (i * 10) % 60, 0),
                    EndDate = new DateTime(2024, i + 1, (i + 5) % 30, (i + 12) % 24, (i * 10) % 60, 0),

                    Source = generateAdress(), 
                    Destination = generateAdress(),
                    Content = getContains(i),
                    Delivered = false,
                    //IdTruck = Randomizer.Seed.Next(1, 10)
                    Truck = null
                });
        }

        }

        private string getRandomLetter()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 3)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string getContains(int index)
        {
            string[] contains = { "20 tonnes de poisson", "un chat", "10 tonnes de bois", "des tables et chaises pour helmo", "une bombe nucléaire" };
            return contains[index % 5];
        }

        private string getLicense()
        {
            Random random = new Random();
            return random.Next(2) % 2 == 0 ? "C" : "CE";
        }
      
        private string generateAdress()
        {
            Random random = new Random();
            string[] nameStreet = { "Nuit", "Kleetlaan", "Pegasuslaan", "Help", "Croix", "Stonks", "Take my Money" };
            int num = Randomizer.Seed.Next(5, 125);
            string[] localite = { "Anvers", "Mons", "Liège", "Charleroi", "Ans" };
            int postalCode = Randomizer.Seed.Next(4000, 4699);
            return $"rue de {nameStreet[random.Next(7)]}, {num} {postalCode} {localite[random.Next(5)]}";
        }
    }
}