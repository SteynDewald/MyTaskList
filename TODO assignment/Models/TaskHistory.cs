using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TODO_assignment.Models
{
    [Table("History")]
    public class TaskHistory
    {
        [Key]
        public Guid HistID { get; set; }
        [ForeignKey("TaskItem")]
        public Guid TaskID { get; set; }
        public DateTime TimeStamp { get; set; }
        public String Description { get; set; }

        public virtual TaskItem TaskItem { get; set; }
    }
}