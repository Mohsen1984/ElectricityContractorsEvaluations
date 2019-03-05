using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{

    //پروژه های شرکت کننده در دوره ارزیابی
    [Table("EvaluationProjects", Schema="dbo")]
    public class EvaluationProjectModel : BaseColumnModel
    {
        [Key]
        public int EvaluationProjectId { get; set;}
     

        public int? PeriodRef { get; set; }

        [ForeignKey("PeriodRef")]
        public virtual EvaluationPeriodModel Period { get; set; }


        public int? ReginalPowerCorpRef{ get; set; }

        [ForeignKey("ReginalPowerCorpRef")]
        public virtual ReginalPowerCorpModel ReginalPowerCorp { get; set; }


        public int? ProjectTreeRef{ get; set; }

        [ForeignKey("ProjectTreeRef")]
        public virtual ProjectTreeModel ProjectTree { get; set; }


        [NotMapped]
        public string PeriodTitle { get; set; }

        [NotMapped]
        public string ReginalPowerCorpTitle { get; set; }

        
        [NotMapped]
        public string ProjectTreeTitle { get; set; }

    }


}
