using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.UI.Models
{
    [Table("ServiceTree", Schema = "dbo")]
    public class ServiceTreeModel : BaseColumnModel
    {
        [Key]
        public int ServiceTreeId { get; set; }


        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "شناسه شرکت برق منطقه ای")]
        public int? ReginalPowerCorpRef { get; set; }

        [ForeignKey("ReginalPowerCorpRef")]
        public virtual ReginalPowerCorpModel ReginalPowerCorp { get; set; }


        [Display(Name = "شناسه پروژه")]
        public int? ProjectInfoRef { get; set; }

        [ForeignKey("ProjectInfoRef")]
        public virtual ProjectInfoModel ProjectInfo { get; set; }


        public int? ServiceTreeRef { get; set; }
        [ForeignKey("ServiceTreeRef")]
        public virtual ServiceTreeModel ServiceTreechild { get; set; }

        public int? ProjectTreeRef { get; set; }
        [ForeignKey("ProjectTreeRef")]
        public virtual ProjectTreeModel RelatedProject { get; set; }


        public int? ServiceTemplateTreeRef { get; set; }
        [ForeignKey("ServiceTemplateTreeRef")]
        public virtual ServiceTemplateTreeModel ServiceTemplateTreechild { get; set; }

        //پروژه ، خدمات ، نقش ها 
        [Display(Name = "نوع شاخه درختواه")]
        public string Level { get; set; }

        [Display(Name = "رشته درختواه")]
        public string LevelCode { get; set; }

        [Display(Name = "کد")]
        public string Code { get; set; }


        [NotMapped]
        public string ServiceParent { get; set; }

        [NotMapped]
        public virtual string ReginalPowerCorpTitle { get; set; }




    }
}
