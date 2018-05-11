using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TODO_assignment.Models
{
    [Table("Tasks")]
    public class TaskItem
    {
        [Key]
        public Guid TaskID { get; set; }
        public String Title { get; set; }
        public Boolean Completed { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<TaskHistory> History { get; set; }
    }
}