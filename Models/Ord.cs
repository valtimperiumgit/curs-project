using System;
using System.Collections.Generic;

namespace Valtimperium
{
    public partial class Ord
    {
        public int IdOrder { get; set; }
        public int? IdClient { get; set; }
        public int? IdProduct { get; set; }
        public DateTime? DateOrder { get; set; }
        public int? CountProduct { get; set; }
        public int? TotalPrice { get; set; }
        public string? AdressDelivery { get; set; }

        public virtual Client? IdClientNavigation { get; set; }
        public virtual Product? IdProductNavigation { get; set; }
    }
}
