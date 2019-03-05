using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.UI.Models
{
    [Table("ProjectPersones", Schema = "dbo")]
    public class ProjectPersoneModel : BaseColumnModel
    {

        [Key]
        public int ProjectPersonelId { get; set; }

        
        [Display(Name = "نوع نقش (ارزیابی کننده / ارزیابی شونده / هر دو )")]
        public byte? EvalType { get; set; }


        [Display(Name = " شناسه شخص")]
        public int? PersoneRef { get; set; }
        [ForeignKey("PersoneRef")]
        public virtual PersonModel Persone { get; set; }


        [Display(Name = " شناسه نقش سازمانی")]
        public int? RoleOrgRef { get; set; }
        [ForeignKey("RoleOrgRef")]
        public virtual RoleOrgModel Role { get; set; }


        [Display(Name = " شناسه درخت پروژه")]
        public int? ProjectTreeRef { get; set; }
        [ForeignKey("ProjectTreeRef")]
        public virtual ProjectTreeModel ProjectTree { get; set; }



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
