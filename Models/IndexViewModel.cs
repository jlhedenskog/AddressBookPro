using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book_Pro.Models
{
    public class IndexViewModel
    {
        public List<Address> Addresses { get; set; }

        public Address SelectedAddress { get; set; }
    }
}
