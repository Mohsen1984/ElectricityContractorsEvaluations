﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{
    public class ServiceTemplateTreeSearchModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectTreeRef {get; set;}
    }
}
