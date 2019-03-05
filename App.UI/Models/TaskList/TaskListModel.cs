using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.UI.Models
{
    [Table("TaskLists", Schema = "dbo")]
    public class TaskListModel : BaseColumnModel
    {

        //  public int Id;
        [Key]
        public int TaskId { get; set; }


        //public string Title;
        [Display(Name = "عنوان وظیفه")]
        [StringLength(100)]
        public string Title { get; set; }


        [Display(Name = "شناسه فرآیند")]
        public Guid? WfInstanceID { get; set; }


        //   public string FormLink;
        [Display(Name = "آدرس فرم مرتبط")]
        [StringLength(100)]
        public string FormLink { get; set; }


        // public string DueDate;
        [Display(Name = "مهلت اقدام")]
        public DateTime? DueDate { get; set; }
        [NotMapped]
        public string DueDatePersian
        {
            //get; set;
            get
            {
                string result = "Not Definde";
                if (this.DueDate != null)
                    result = Business.Base.GetPersianDate(this.DueDate ?? DateTime.Now);
                return result;
            }
            set
            {
                this.DueDate = Business.Base.GetMiladiDate(value);
            }
        }
        



        // public string DueDate;
        [Display(Name = "تاریخ و زمان ورود به کارتابل")]

        public DateTime? InsertDate { get; set; }


        // public string DueDate;
        [Display(Name = "تاریخ و زمان مشاهده")]

        public DateTime? WatchDate { get; set; }




        [Display(Name = "تاریخ و زمان انجام")]
        public DateTime? DoDate { get; set; }



        [Display(Name = "شناسه فرستنده عضو پروژه")]
        public int? SenderProjectMemberRef { get; set; }
        [ForeignKey("SenderProjectMemberRef")]
        public virtual ProjectMemberModel SenderProjectMember { get; set; }


        [Display(Name = "شناسه گیرنده عضو پروژه")]
        public int? ReciverProjectMemberRef { get; set; }
        [ForeignKey("ReciverProjectMemberRef")]
        public virtual ProjectMemberModel ReciverProjectMember { get; set; }


        //  public int Priority;
        //( 0 عادی / 1 متوسط / 2 مهم )
        [Display(Name = "اهمیت")]
        public byte? Priority { get; set; }

        [NotMapped]
        public string PriorityText {

            get
            {
                string result;
                if (this.Priority == 2)
                    result = "مهم";
                else if (this.Priority == 1)
                    result = "متوسط";
                else
                    result = "عادی";
                return result;
            }
        }


        [Display(Name = "اطلاعات پروژه")]
        public int? ProjectInfoRef { get; set; }

        [ForeignKey("ProjectInfoRef")]
        public virtual ProjectInfoModel ProjectInfo { get; set; }
        

        [Display(Name = "دوره ارزیابی")]
        public int? EvaluationPeriodRef { get; set; }

        [ForeignKey("EvaluationPeriodRef")]
        public virtual EvaluationPeriodModel EvaluationPeriod { get; set; }


        //( 0 شروع نشده / 5 در حال بررسی / 10 تکمیل شده)       
        [Display(Name = "وضعیت نتیجه")]
        public byte? OutCome { get; set; }

        public _OutCome OutComeText
        {

            get
            {
                _OutCome result = new _OutCome();
                if (this.OutCome == (int)StatusKey.Not_Started)
                    result = status[StatusKey.Not_Started];

                if (this.OutCome == (int)StatusKey.Completed)
                    result = status[StatusKey.Completed];

                if (this.OutCome == (int)StatusKey.In_Progress)
                    result = status[StatusKey.In_Progress];

                return result;

            }
        }

        enum StatusKey
        {
            Not_Started = 0,
            In_Progress = 5,
            Completed = 10

        }
        [NotMapped]
        private Dictionary<StatusKey, _OutCome> status = new Dictionary<StatusKey, _OutCome>
        {
            { StatusKey.Not_Started, new _OutCome { Id = 0, Status = "Not_Started", translatedStatus = "شروع نشده" } },
            { StatusKey.In_Progress, new _OutCome { Id = 5, Status = "In_Progress", translatedStatus = "در دست اقدام" } },
            { StatusKey.Completed, new _OutCome { Id = 10, Status = "Completed", translatedStatus = "انجام شده" } }
        };

        [NotMapped]
        public List<_Action> Actions;

        [NotMapped]
        public string InsertDatePersian
        {
            //get; set;
            get
            {
                string result = "Not Definde";
                if (this.InsertDate != null)
                    result = Business.Base.GetPersianDate(this.InsertDate ?? DateTime.Now);
                return result;
            }
            set
            {
                this.InsertDate = Business.Base.GetMiladiDate(value);
            }
        }


        [NotMapped]
        public string DoDatePersian
        {
            //get; set;
            get
            {
                string result = "Not Definde";
                if (this.DoDate != null)
                    result = Business.Base.GetPersianDate(this.DoDate ?? DateTime.Now);
                return result;
            }
            set
            {
                this.DoDate = Business.Base.GetMiladiDate(value);
            }
        }

        [NotMapped]
        public string WatchDatePersian
        {
            //get; set;
            get
            {
                string result = "Not Definde";
                if (this.WatchDate != null)
                    result = Business.Base.GetPersianDate(this.WatchDate ?? DateTime.Now);
                return result;
            }
            set
            {
                this.WatchDate = Business.Base.GetMiladiDate(value);
            }
        }
    }/// <summary>
     /// ENDModel
     /// </summary>
     /// 


    [NotMapped]
    public class _OutCome
    {

        public int Id;
        public string Status;
        public string translatedStatus;
    }


    [NotMapped]
    public class _Action
    {
        public int Id;
        public string Title;
        public string TranslatedTitle;
        public string Link;
        public string Icon;
    }



}
