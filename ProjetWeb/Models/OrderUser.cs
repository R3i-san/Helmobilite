using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace ProjetWeb.Models
{
    public class OrderUser
    {
        public int IdOrder { get; set; }
        public Order? Order { get; set; }
        public string? IdUser { get; set; }
        public User? User { get; set; }
    }
}
