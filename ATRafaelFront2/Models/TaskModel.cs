using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATRafaelFront2.Models
{
    public class TaskModel {
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public string TaskTo { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

    }
}
