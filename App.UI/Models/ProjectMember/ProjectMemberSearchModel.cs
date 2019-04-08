using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{
    public class ProjectMemberSearchModel
    {

        public int ProjectMemberID { get; set; }

        public int ServiceTreeRef { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
