using System;
using System.Collections.Generic;

namespace Valtimperium
{
    public partial class Type
    {
        public Type()
        {
            Products = new HashSet<Product>();
        }

        public int IdType { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
