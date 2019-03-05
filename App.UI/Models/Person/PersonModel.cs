using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{



[Table("Persons", Schema="dbo")]
    public class PersonModel : BaseColumnModel
    {
        [Key]
        public int PersonId { get; set;}

        [Display(Name = "نام"), StringLength(200)]
        public string FirstName { get; set; }


        [Display(Name = "نام خانوادگی"), StringLength(200)]
        public string LastName { get; set; }



        [Display(Name = "نام کامل"), StringLength(200)]
        [NotMapped]
        public string FullName

        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }



        [Display(Name = "کد ملی"), StringLength(10)]
        public string NationalID { get; set; }



        [Display(Name = "شماره موبایل"), StringLength(11)]
        public string MobileNo { get; set; }


        [Display(Name = "شماره تماس "), StringLength(50)]
        public string Tel { get; set; }



        [Display(Name = "نام پدر "), StringLength(100)]
        public string FatherName { get; set; }

        

        [Display(Name = "شماره شناسنامه ")]
        public int? IDNumber { get; set; }


        [Display(Name = "پست الکترونیک "), StringLength(200)]
        public string Email { get; set; }


        [Display(Name = "آدرس "), StringLength(1000)]
        public string Address { get; set; }

        [Display(Name = "تحصیلات  ")]
        public int? EducationLevelRef { get; set; }

        [ForeignKey("EducationLevelRef")]
        public virtual EducationalLevelModel EducationalLevel { get; set; }

        [StringLength(200)]
        [NotMapped]
        public string EducationalLevelTitle { get;set;}

        [Display(Name = "وضعیت")]
        public byte? State { get; set; }
    }
}
