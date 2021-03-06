﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Models
{
    public class TaskListSearchModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public string Title { get; set; }
        public string ProjectNo { get; set; }

        public byte? OuteCome { get; set; }

        public string ProjectInfo { get; set; }

    }
}
