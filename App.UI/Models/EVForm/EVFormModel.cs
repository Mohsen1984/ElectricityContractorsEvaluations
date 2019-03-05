using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{

    //پروژه های شرکت کننده در دوره ارزیابی
    [Table("EVForms", Schema="dbo")]
    public class EVFormModel : BaseColumnModel
    {
        [Key]
        public int EVFormId { get; set;}


        [Display(Name = " عنوان فرم ارزیابی")]
        public string Title { get; set; }

        [Display(Name = "  شناسه ارزیابی")]
        public Guid? EVFormInstanceID { get; set; }


        [Display(Name = "قالب فرم ارزیابی")]
        public int? EVFormTemplateRef { get; set; }

        [ForeignKey("EVFormTemplateRef")]
        public virtual EVFormTemplateModel EVFormTemplate { get; set; }


        [Display(Name = " دوره ارزیابی")]
        public int? EvaluationPeriodRef { get; set; }

        [ForeignKey("EvaluationPeriodRef")]
        public virtual EvaluationPeriodModel EvaluationPeriod { get; set; }



        [Display(Name = " نقش ارزیاب")]
        public int? EvaluatorRoleRef { get; set; }

        [ForeignKey("EvaluatorRoleRef")]
        public virtual RoleOrgModel EvaluatorRole { get; set; }


        [Display(Name = "  نقش ارزیابی شونده")]
        public int? EvaluatedRoleRef { get; set; }

        [ForeignKey("EvaluatedRoleRef")]
        public virtual RoleOrgModel EvaluatedRole { get; set; }



        [Display(Name = " کاربر ارزیاب ")]
        public int? EvaluatorPersoneRef { get; set; }

        [ForeignKey("EvaluatorPersoneRef")]
        public virtual PersonModel EvaluatorPersone { get; set; }


        [Display(Name = "  کاربر ارزیابی شونده")]
        public int? EvaluatedPersoneRef { get; set; }

        [ForeignKey("EvaluatedPersoneRef")]
        public virtual PersonModel EvaluatedPersone{ get; set; }



        public int? ReginalPowerCorpRef{ get; set; }

        [ForeignKey("ReginalPowerCorpRef")]
        public virtual ReginalPowerCorpModel ReginalPowerCorp { get; set; }


        public int? ProjectTreeRef{ get; set; }

        [ForeignKey("ProjectTreeRef")]
        public virtual ProjectTreeModel ProjectTree { get; set; }


        [NotMapped]
        public string EvaluatedPersoneTitle { get; set; }

        [NotMapped]
        public string EvaluatorPersoneTitle { get; set; }

        [NotMapped]
        public string ReginalPowerCorpTitle { get; set; }


        [NotMapped]
        public string ProjectTreeTitle { get; set; }
    }


}
