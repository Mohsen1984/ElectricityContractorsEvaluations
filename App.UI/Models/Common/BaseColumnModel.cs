using Microsoft.ApplicationInsights.AspNetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{
    public abstract class BaseColumnModel
    {
        [Column(Order = 100)]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }


        [Column(Order = 101)]
        [Display(Name = "وضعیت")]

        public byte? State { get; set; }

        [Column(Order = 101)]
        [Display(Name = "ایجاد شده توسط")]
        [StringLength(80)]
        public string CreatedBy { get; set; }

        [Column(Order = 102), StringLength(10), Display(Name = "تاریخ ایجاد")]
        public string Created { get; set; }

        [Column(Order = 103)]
        [Display(Name = "ویرایش شده توسط")]
        [StringLength(80)]
        public string ModifyBy { get; set; }


        [Column(Order = 104)]
        [Display(Name = "تاریخ ویرایش")]


        [StringLength(10)]
        public string Modified { get; set; }

    }
}
