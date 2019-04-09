using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{
    public class AddTaskModel
    {
        public string Title { get; set; }

        public Guid? WfInstanceID { get; set; }

        public string FormLink { get; set; }

        public DateTime? DueDate { get; set; }

        public int? SenderProjectMemberRef { get; set; }

        public int? ReciverProjectMemberRef { get; set; }

        //  public int Priority;
        //( 0 عادی / 1 متوسط / 2 مهم )
        public byte? Priority { get; set; }

        public int? ProjectInfoRef { get; set; }

        public int? EvaluationPeriodRef { get; set; }

    }
}