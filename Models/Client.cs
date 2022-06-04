using System;
using System.Collections.Generic;

namespace Valtimperium
{
    public partial class Client
    {
        public Client()
        {
            Ords = new HashSet<Ord>();
        }

        public int IdClient { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Ord> Ords { get; set; }
    }
}
