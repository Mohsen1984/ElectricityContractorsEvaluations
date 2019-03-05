using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{


    [Table("ExternalOrganizations", Schema="dbo")]
    public class ExternalOrganizationModel : BaseColumnModel
    {
        [Key]
        public int ExternalOrganizationId { get; set;}

        public int? ExternalOrgTypeRef { get; set; }

        [ForeignKey("ExternalOrgTypeRef")]
        public virtual ExternalOrgTypeModel ExternalOrgType { get; set; }


    
        [Display(Name = "عنوان")]
        public string Title { get; set; }


        [Display(Name = "نام مدیر عامل"), StringLength(200)]
        public string Manager { get; set; }

        [Display(Name = "شماره همراه مدیر عامل"), StringLength(200)]
        public string ManagerMobile { get; set; }

        [Display(Name = "شماره تماس"), StringLength(200)]
        public string Tel { get; set; }

        [Display(Name = "فکس"), StringLength(200)]
        public string Fax { get; set; }

        [Display(Name = "ایمیل"), StringLength(200)]
        public string Mail { get; set; }

        [Display(Name = "آدرس اصلی"), StringLength(1000)]
        public string MainAddress { get; set; }

        [Display(Name = "آدس فرعی"), StringLength(1000)]
        public string SubAddress { get; set; }

        [Display(Name = "شماره ثبت"), StringLength(200)]
        public string RegisterCode { get; set; }



        [NotMapped]
        public string ExternalOrgTypeTitle { get; set; }
    }
}
