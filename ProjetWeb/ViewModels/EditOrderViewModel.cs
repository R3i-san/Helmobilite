namespace ProjetWeb.ViewModels
{
    public class EditOrderViewModel
    {
        //info pour les Trucks
        public int IdTruck { get; set; }
        public string? TypeTruck { get; set; }
        public string? PlateTruck { get; set; }
        public int MaxWeight { get; set; }

        //info pour les Drivers
        public string? IdDriver { get; set; }
        public string? NameDriver { get; set; }
        public string? LicenseDriver { get; set; }

        //info de l'Order
        public int IdOrder { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
