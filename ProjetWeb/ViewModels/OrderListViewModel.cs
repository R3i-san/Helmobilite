using ProjetWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetWeb.ViewModels
{
    public class OrderListViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public string? Content { get; set; }
        public string? NameCustomer { get; set; }
        public bool? Delivered { get; set; }
        public string? Comment { get; set; }
        public Boolean BadPayer { get; set; }
    }
}
