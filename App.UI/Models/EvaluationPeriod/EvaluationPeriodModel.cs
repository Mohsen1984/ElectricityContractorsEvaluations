using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{

    //پروژه های شرکت کننده در دوره ارزیابی
    [Table("EvaluationPeriods", Schema="dbo")]
    public class EvaluationPeriodModel : BaseColumnModel
    {
        [Key]
        public int PeriodId { get; set;}


        [Display(Name = "عنوان")]
        public string Title { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }


        public int? ReginalPowerCorpRef { get; set; }

        [ForeignKey("ReginalPowerCorpRef")]
        public virtual ReginalPowerCorpModel ReginalPowerCorp { get; set; }



        [NotMapped]
        public string FromDatePersian {/*get; set;*/
            get
            {
                string result = "Not Definde";
                if (this.FromDate != null)
                    result = Business.Base.GetPersianDate(this.FromDate ?? DateTime.Now);
                return result;
            }
            set
            {
                this.FromDate = Business.Base.GetMiladiDate(value);
            }
        }

        [NotMapped]
        public string ToDatePersian{
            //get; set;
            get
            {
                string result = "Not Definde";
                if (this.ToDate != null)
                    result = Business.Base.GetPersianDate(this.ToDate ?? DateTime.Now);
                return result;
            }
            set
            {
                this.ToDate = Business.Base.GetMiladiDate(value);
            }
        }

        [NotMapped]
        public string ReginalPowerCorpTitle{  get;set;  }



    }


}
