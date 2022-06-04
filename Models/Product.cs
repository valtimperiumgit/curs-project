using System;
using System.Collections.Generic;

namespace Valtimperium
{
    public partial class Product
    {
        public Product()
        {
            Ords = new HashSet<Ord>();
        }

        public int IdProduct { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public int? IdType { get; set; }
        public string? Description { get; set; }
        public int? Availability { get; set; }
        public string? Photo { get; set; }
        public virtual Type? IdTypeNavigation { get; set; }
        public virtual ICollection<Ord> Ords { get; set; }
    }
}
