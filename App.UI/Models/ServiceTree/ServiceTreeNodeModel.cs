using App.UI.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{
    public class ServiceTreeNodeModel : LazyLoadNode
    {
        public int? Id { get; set; }
        public string Text { get; set; }

        public string Level { get; set; }
        public string LevelCode { get; set; }
        public string ParentId { get; set; }
  
        public int ProjectRef { get; set; }

        public int ServiceTemplateTreeRef { get; set; }

        public int ProjectInfoRef { get; set; }
    }
}
