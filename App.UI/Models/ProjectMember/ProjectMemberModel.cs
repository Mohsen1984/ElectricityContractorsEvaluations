using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.UI.Models
{
    [Table("ProjectMember", Schema = "dbo")]
    public class ProjectMemberModel : BaseColumnModel
    {
        [Key]
        public int ProjectMemberID { get; set; }


        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "شناسه شرکت برق منطقه ای")]
        public int? ReginalPowerCorpRef { get; set; }

        [ForeignKey("ReginalPowerCorpRef")]
        public virtual ReginalPowerCorpModel ReginalPowerCorp { get; set; }

        public int? ServiceTreeRef { get; set; }
        [ForeignKey("ServiceTreeRef")]
        public virtual ServiceTreeModel ServiceTreechild { get; set; }


        public int? ProjectInfoRef { get; set; }
        [ForeignKey("ProjectInfoRef")]
        public virtual ProjectInfoModel ProjectInfo { get; set; }


        [Display(Name = " شناسه شخص")]
        public int? PersoneRef { get; set; }
        [ForeignKey("PersoneRef")]
        public virtual PersonModel Persone { get; set; }


        [Display(Name = " شناسه نقش")]
        public int? RoleOrgsRef { get; set; }
        [ForeignKey("RoleOrgsRef")]
        public virtual RoleOrgModel RoleOrgs { get; set; }



        [NotMapped]
        public string PersonFullName { get; set; }

        [NotMapped]
        public string ReginalPowerCorpTitle { get; set; }

        [NotMapped]
        public string RoleName { get; set; }
    }
}

