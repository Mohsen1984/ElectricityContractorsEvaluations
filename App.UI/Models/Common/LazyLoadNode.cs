using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models.Common
{
    public abstract class LazyLoadNode
    {
        public bool? HasChild { get; set; }
    }
}
