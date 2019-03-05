using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.UI.Models
{
    [Table("ContractInfos", Schema = "dbo")]
    public class ContractInfoModel : BaseColumnModel
    {
     
        [Key]
        public int ContractId { get; set; }

        [Display(Name = "عنوان قرار داد")]
        public string Title { get; set; }
     
        public string ContractNo { get; set; }
        

        [Display(Name = " شناسه درخت پروژه")]
        public int? ProjectTreeRef { get; set; }
        [ForeignKey("ProjectTreeRef")]
        public virtual ProjectTreeModel ProjectTree { get; set; }


        [Display(Name = " شناسه شرکت برق منطقه ای")]
        public int? ReginalPowerCorpRef { get; set; }
        [ForeignKey("ReginalPowerCorpRef")]
        public virtual ReginalPowerCorpModel ReginalPowerCorp { get; set; }





        [Display(Name = " شناسه پیمانکار")]
        public int? ExternalOrganizationRef { get; set; }
        [ForeignKey("ExternalOrganizationRef")]
        public virtual ExternalOrganizationModel ExternalOrganization { get; set; }


        [NotMapped]
        public string ProjectTreeTitle { get; set; }

        [NotMapped]
        public string ReginalPowerCorpTitle { get; set; }


        [NotMapped]
        public string ExternalOrganizationTitle { get; set; }


    }
}
