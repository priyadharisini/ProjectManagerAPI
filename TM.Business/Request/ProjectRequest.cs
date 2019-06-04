﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Business.Request
{
    public class ProjectRequest
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }
        public string Manager { get; set; }
        public int UserId { get; set; }
        public int NumberOfTasks { get; set; }
        public string Completed { get; set; }

    }
}
