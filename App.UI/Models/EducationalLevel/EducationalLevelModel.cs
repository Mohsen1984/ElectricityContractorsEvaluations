using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{
    [Table("EducationalLevels", Schema="dbo")]
    public class EducationalLevelModel : BaseColumnModel
    {
        [Key]
        public int EducationalLevelId { get; set;}

        [Display(Name = "عنوان"), StringLength(200)]
        public string Title { get; set; }

    }
}
