using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{

    //پروژه های شرکت کننده در دوره ارزیابی
    [Table("EVFormTemplates", Schema = "dbo")]
    public class EVFormTemplateModel : BaseColumnModel
    {
        [Key]
        public int EVFormTemplateId { get; set; }

        [Display(Name = " عنوان قالب فرم ارزیابی")]
        public string Title { get; set; }

        [Display(Name = " نقش ارزیاب")]
        public int? EvaluatorRoleRef { get; set; }

        [ForeignKey("EvaluatorRoleRef")]
        public virtual RoleOrgModel EvaluatorRole { get; set; }


        [Display(Name = "  نقش ارزیابی شونده")]
        public int? EvaluatedRoleRef { get; set; }

        [ForeignKey("EvaluatedRoleRef")]
        public virtual RoleOrgModel EvaluatedRole { get; set; }


        public int? ReginalPowerCorpRef { get; set; }

        [ForeignKey("ReginalPowerCorpRef")]
        public virtual ReginalPowerCorpModel ReginalPowerCorp { get; set; }


        public int? ProjectTreeRef { get; set; }

        [ForeignKey("ProjectTreeRef")]
        public virtual ProjectTreeModel ProjectTree { get; set; }


        

        public int? EvaluationPeriodRef { get; set; }

        [ForeignKey("EvaluationPeriodRef")]
        public virtual EvaluationPeriodModel EvaluationPeriod { get; set; }


        [NotMapped]
        public string EvaluatorRoleTitle { get; set; }

        [NotMapped]
        public string EvaluatedRoleTitle { get; set; }

        [NotMapped]
        public string ReginalPowerCorpTitle { get; set; }


        [NotMapped]
        public string ProjectTreeTitle { get; set; }
    }


}
