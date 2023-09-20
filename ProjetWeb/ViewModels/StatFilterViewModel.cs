using ProjetWeb.Models;
using System.ComponentModel;

namespace ProjetWeb.ViewModels
{
    public class StatFilterViewModel
    {

        public string FilterName { get; set; }

        [DisplayName("Nom du chauffeur")]
        public string? DriverName { get; set; }

        [DisplayName("Nom du client")]
        public string? CustomerName { get; set; }

        [DisplayName("Date de chargement/livraison")]
        public DateTime DeliveryDate { get; set; }


    }
}
