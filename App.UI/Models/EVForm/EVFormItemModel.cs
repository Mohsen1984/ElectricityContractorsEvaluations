using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{

    //پروژه های شرکت کننده در دوره ارزیابی
    [Table("EVFormItems", Schema="dbo")]
    public class EVFormItemModel : BaseColumnModel
    {
        [Key]
        public int EVFormItemId { get; set;}

        [Display(Name = "  شناسه ارزیابی")]
        public Guid? EVFormInstanceID { get; set; }


        [Display(Name = " فرم ارزیابی")]
        public int? EVFormRef { get; set; }
        [ForeignKey("EVFormRef")]
        public virtual EVFormModel EVForm { get; set; }


        [Display(Name = " معیار ارزیابی")]
        public int? EvaluationFactorRef { get; set; }
        [ForeignKey("EvaluationFactorRef")]
        public virtual EvaluationFactorTreeModel EvaluationFactor { get; set; }


        [Display(Name = "معیار ارزیابی پدر")]
        public int? EVFormItemRef { get; set; }
        [ForeignKey("EVFormItemRef")]
        public virtual EVFormItemModel EVFormParentItem { get; set; }

        [Display(Name = "امتیاز")]
        public decimal? WeightFactor { get; set; }

        [Display(Name = "امتیاز")]
        public decimal? Result { get; set; }


        [Display(Name = "امتیاز")]
        public decimal? Score { get; set; }


    }


}
