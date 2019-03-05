using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{
    [Table("ProjectTrees", Schema="dbo")]
    public class ProjectTreeModel : BaseColumnModel
    {
        [Key]
        public int ProjectTreeId { get; set;}

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "شناسه شرکت برق منطقه ای")]
        public int? ReginalPowerCorpRef{ get; set; }

        [ForeignKey("ReginalPowerCorpRef")]
        public virtual ReginalPowerCorpModel ReginalPowerCorp { get; set; }

        public int? ProjectTreeRef { get; set; }
        [ForeignKey("ProjectTreeRef")]
        public virtual ProjectTreeModel ProjectTreechild { get; set; }



        //پروژه ، خدمات ، نقش ها 
        [Display(Name = "نوع شاخه درختواه")]
        public string Level { get; set; }

        [Display(Name = "رشته درختواه")]
        public string LevelCode { get; set; }

        [Display(Name = "کد")]
        public string Code { get; set; }

        
        [Display(Name = "قالب است ؟")]
        public bool? IsTemplate { get; set; }



        [NotMapped]
        public string ProjectParent { get;set; }

        [NotMapped]
        public virtual string ReginalPowerCorpTitle { get; set; }




    }
}
