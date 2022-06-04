using System;
using System.Collections.Generic;

namespace Valtimperium
{
    public partial class BankCard
    {
        public int IdCard { get; set; }
        public string? Number { get; set; }
        public int? Pin { get; set; }
        public int? Money { get; set; }
    }
}
