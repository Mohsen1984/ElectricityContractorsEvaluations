using App.UI.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models.TreeComponent
{
    public class EntityNode : LazyLoadNode
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public string ParentId { get; set; }
    }
}
