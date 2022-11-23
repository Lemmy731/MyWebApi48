using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETodo.Domain.Dto
{
    public class UTodoItemDto
    {
        public string TaskName { get; set; }
        public string TaskStatus { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
       
    }
}
