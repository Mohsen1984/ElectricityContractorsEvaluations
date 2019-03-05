using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{

    //پروژه های شرکت کننده در دوره ارزیابی
    [Table("EVFormTemplateItems", Schema="dbo")]
    public class EVFormTemplateItemModel : BaseColumnModel
    {
        [Key]
        public int EVFormTemplateItemId { get; set;}

                     
        [Display(Name = " قالب فرم ارزیابی")]
        public int? EVFormTemplateRef { get; set; }

        [ForeignKey("EVFormTemplateRef")]
        public virtual EVFormTemplateModel EVFormTemplate { get; set; }


        [Display(Name = "  معیار ارزیابی")]
        public int? EvaluationFactorRef { get; set; }
        [ForeignKey("EvaluationFactorRef")]
        public virtual EvaluationFactorTreeModel EvaluationFactor { get; set; }


        [Display(Name = "معیار ارزیابی پدر")]
        public int? EVFormTemplateItemRef { get; set; }
        [ForeignKey("EVFormTemplateItemRef")]
        public virtual EVFormTemplateItemModel EVFormTemplateParentItem { get; set; }


        [Display(Name = "امتیاز")]
        public decimal Score { get; set; }



    }
}
