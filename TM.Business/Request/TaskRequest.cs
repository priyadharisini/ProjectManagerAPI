using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Business.Request
{
    public class TaskRequest
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int Priority { get; set; }
        public int ParentTaskId { get; set; }
        public string ParentTaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string Status { get; set; }

    }
}
