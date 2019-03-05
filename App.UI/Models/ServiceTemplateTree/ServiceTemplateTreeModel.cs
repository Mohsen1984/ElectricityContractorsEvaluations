using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.UI.Models
{
    [Table("ServiceTemplateTree", Schema = "dbo")]
    public class ServiceTemplateTreeModel : BaseColumnModel
    {
        [Key]
        public int ServiceTemplateTreeId { get; set; }


        [Display(Name = "عنوان")]
        public string Title { get; set; }


        [Display(Name = "شناسه شرکت برق منطقه ای")]
        public int? ReginalPowerCorpRef { get; set; }

        [ForeignKey("ReginalPowerCorpRef")]
        public virtual ReginalPowerCorpModel ReginalPowerCorp { get; set; }

        public int? ServiceTemplateTreeRef { get; set; }
        [ForeignKey("ServiceTemplateTreeRef")]
        public virtual ServiceTemplateTreeModel ServiceTemplateTreechild { get; set; }

        public int? ProjectTreeRef { get; set; }
        [ForeignKey("ProjectTreeRef")]
        public virtual ProjectTreeModel RelatedProject { get; set; }

        //پروژه ، خدمات ، نقش ها 
        [Display(Name = "نوع شاخه درختواه")]
        public string Level { get; set; }

        [Display(Name = "نوع نقش (ارزیابی کننده / ارزیابی شونده / هر دو )")]
        public byte? EvalType { get; set; }

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
