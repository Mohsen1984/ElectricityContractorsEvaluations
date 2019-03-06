using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.UI.Models
{
    [Table("ProjectInfos", Schema = "dbo")]
    public class ProjectInfoModel : BaseColumnModel
    {


        [Key]
        public int ProjectInfoId { get; set; }
      
        public string Title { get; set; }
        
        [Display(Name = "شماره پروژه")]
        public string ProjectNo { get; set; }


        [Display(Name = "تاریخ پیش بینی شروع")]
        public DateTime? PlanedStartDate { get; set; }
        [NotMapped]
        public string PlanedStartDatePersian
        {
            get
            {
                string result = "Not Definde";
                if (this.PlanedStartDate != null)
                    result = Business.Base.GetPersianDate(this.PlanedStartDate ?? DateTime.Now);
                return result;
            }
            set
            {
                this.PlanedStartDate = Business.Base.GetMiladiDate(value);
            }
        }


        [Display(Name = "تاریخ پیش بینی اتمام")]
        public DateTime? PlanedFinishDate { get; set; }
        [NotMapped]
        public string PlanedFinishDatePersian
        {
            get
            {
                string result = "Not Definde";
                if (this.PlanedFinishDate != null)
                    result = Business.Base.GetPersianDate(this.PlanedFinishDate ?? DateTime.Now);
                return result;
            }
            set
            {
                this.PlanedFinishDate = Business.Base.GetMiladiDate(value);
            }
        }

        [Display(Name = "تاریخ واقعی شروع")]
        public DateTime? ActualStartDate { get; set; }
        [NotMapped]
        public string ActualStartDatePersian
        {
            get
            {
                string result = "Not Definde";
                if (this.ActualStartDate != null)
                    result = Business.Base.GetPersianDate(this.ActualStartDate ?? DateTime.Now);
                return result;
            }
            set
            {
                this.ActualStartDate = Business.Base.GetMiladiDate(value);
            }
        }

        [Display(Name = "تاریخ واقعی اتمام")]
        public DateTime? ActualFinishDate { get; set; }
        [NotMapped]
        public string ActualFinishDatePersian
        {
            get
            {
                string result = "Not Definde";
                if (this.ActualFinishDate != null)
                    result = Business.Base.GetPersianDate(this.ActualFinishDate ?? DateTime.Now);
                return result;
            }
            set
            {
                this.ActualFinishDate = Business.Base.GetMiladiDate(value);
            }
        }

        [Display(Name = "برآورد اولیه پروژه")]
        public decimal? EstimatedAmount { get; set; }

        [Display(Name = " مدیر پروژه")]
        public int? ProjectManagerRef { get; set; }
        [ForeignKey("ProjectManagerRef")]
        public virtual PersonModel ProjectManager { get; set; }


        [Display(Name = " مسئول کنترل پروژه")]
        public int? ProjectControllerRef { get; set; }
        [ForeignKey("ProjectControllerRef")]
        public virtual PersonModel ProjectController { get; set; }


        [Display(Name = "مسئول مستند سازی پروژه")]
        public int? DocumnetCotrollerRef { get; set; }
        [ForeignKey("DocumnetCotrollerRef")]
        public virtual PersonModel DocumnetCotroller { get; set; }


        [Display(Name = " شناسه درخت پروژه")]
        public int? ProjectTreeRef { get; set; }
        [ForeignKey("ProjectTreeRef")]
        public virtual ProjectTreeModel ProjectTree { get; set; }



        [Display(Name = " شناسه درخت نمونه های خدمات")]
        public int? ServiceTemplateTreeRef { get; set; }
        [ForeignKey("ServiceTemplateTreeRef")]
        public virtual ServiceTemplateTreeModel ServiceTemplateTree { get; set; }



        [Display(Name = " شناسه شرکت برق منطقه ای")]
        public int? ReginalPowerCorpRef { get; set; }
        [ForeignKey("ReginalPowerCorpRef")]
        public virtual ReginalPowerCorpModel ReginalPowerCorp { get; set; }


        [NotMapped]
        public string ProjectTreeTitle { get; set; }

        [NotMapped]
        public string ReginalPowerCorpTitle { get; set; }

    }
}
