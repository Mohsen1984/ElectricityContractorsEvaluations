using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.UI.Models
{
    [Table("EvaluationFactorTrees", Schema = "dbo")]
    public class EvaluationFactorTreeModel : BaseColumnModel
    {

        [Key]
        public int EvaluationFactorId { get; set; }


        [Display(Name = "عنوان معیار")]
        [StringLength(200)]
        public string Title { get; set; }



        [Display(Name = " شناسه پدر")]
        public int? EvaluationFactorRef { get; set; }
        [ForeignKey("EvaluationFactorRef")]
        public virtual EvaluationFactorTreeModel EvaluationFactorParent { get; set; }



        public ICollection<EvaluationFactorTreeModel> Children { get; set; }



        //smallint (0 کمی و 1 کیفی)
        [Display(Name = "نوع معیار")]
        [StringLength(5)]
        public string EvaluationFactorType { get; set; }

        [NotMapped]
        public string EvaluationFactorParentName { get; set; }



        [NotMapped]
        public string EvaluationFactorTypeName
        {

            get
            {

                string result;
                if (this.EvaluationFactorType == "0")
                    result = "کمی";
                else
                    result = "کیفی";

                return result;
            }

        }

    }
}
