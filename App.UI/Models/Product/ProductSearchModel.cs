using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models.Product
{
    public class ProductSearchModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
