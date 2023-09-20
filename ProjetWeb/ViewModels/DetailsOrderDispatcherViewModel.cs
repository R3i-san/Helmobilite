namespace ProjetWeb.ViewModels
{
    public class DetailsOrderDispatcherViewModel
    {

        //info pour les Customer
        public string? NameCustomer { get; set; }
        public string? EmailCustomer { get; set; }
        public string? AddressCustomer { get; set; }
        public Boolean BadPayer { get; set; }

        //info de l'Order
        public int IdOrder { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public string? Content { get; set; }
    }
}
