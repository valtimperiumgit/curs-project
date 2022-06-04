namespace Valtimperium.ViewModels
{
    public class OrderViewModel
    {
       public Product product { get; set; }
       public Client client { get; set; }
        public int count { get; set; }

        public string addressDelivery { get; set; }

        public int totalPrice { get; set; }

        public DateTime dateOrder { get; set; }

        public int statusOrder { get; set; }

        public int bankCard { get; set; }

        public int pin { get; set; }
    }
}
