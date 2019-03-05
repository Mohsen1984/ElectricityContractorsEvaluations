using App.UI.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{
    public class EvaluationFactorTreeNodeModel : LazyLoadNode
    {
        public int? Id { get; set; }
        public string Text { get; set; }

        public string Level { get; set; }
        public string LevelCode { get; set; }
        public string ParentId { get; set; }
        public bool HasChild { get; set; }
        public int ProjectRef { get; set; }

    }
}
