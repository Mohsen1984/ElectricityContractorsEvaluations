using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{
    [Table("RoleOrgs", Schema="dbo")]
    public class RoleOrgModel : BaseColumnModel
    {
        [Key]
        public int RoleOrgId { get; set;}

        [Display(Name = "عنوان نقش سازمانی"), StringLength(200)]
        public string Title { get; set; }

    }
}
