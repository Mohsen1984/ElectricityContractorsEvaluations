using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models.Product
{
    public class CategorySearchModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
