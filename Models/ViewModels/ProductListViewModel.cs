using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TT_LAB2.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PageInfo PageInfo { get; set; }
        public string currentCategory { get; set; }
    }
}
