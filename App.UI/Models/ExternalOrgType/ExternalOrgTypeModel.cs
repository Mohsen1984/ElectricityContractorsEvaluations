using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{
    //انواع سازمانهای خارجی
    [Table("ExternalOrgTypes", Schema="dbo")]
    public class ExternalOrgTypeModel : BaseColumnModel
    {
        [Key]
        public int ExternalOrgTypeId { get; set;}

    
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        

        [Display(Name = "کد")]
        public string Code { get; set; }



    }
}
