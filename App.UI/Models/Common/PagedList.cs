using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models.Common
{
    public class PagedList<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItemsCount { get; set; }
        public List<T> Items { get; set; }
    }
}
