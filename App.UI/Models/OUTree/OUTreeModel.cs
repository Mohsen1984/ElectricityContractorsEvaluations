using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{

    //درختواره واحد های سازمانی
    [Table("OUTrees", Schema="dbo")]
    public class OUTreeModel 
       
      : BaseColumnModel
    {
        [Key]
        public int OUTreeId { get; set;}

        public int? ReginalPowerCorpRef{ get; set; }

        [ForeignKey("ReginalPowerCorpRef")]
        public virtual ReginalPowerCorpModel ReginalPowerCorp { get; set; }

        public int? OUTreeRef { get; set; }

        [ForeignKey("OUTreeRef")]
        public virtual OUTreeModel OUTree { get; set; }

        [NotMapped]
        public string OuTreeParent { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "کد")]
        public string Code { get; set; }



        [NotMapped]
        public string ReginalPowerCorpTitle { get; set; }
        
    }


}
