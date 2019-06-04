using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Entities
{
    [Table("Tasks")]
    public class ProjectTask
    {
        [Key]
        public int TaskId { get; set; }

        public int ParentTaskId { get; set; }

        [ForeignKey("ParentTaskId")]
        public virtual ParentTask ParentTask { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public string Title { get; set; }
        public int Priority { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Status { get; set; }
    }
}
