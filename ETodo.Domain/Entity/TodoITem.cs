using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETodo.Domain.Entity
{
    public class TodoITem
    {

        public string Id { get; set; }
        public string TaskName { get; set; }
        public string TaskStatus { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Second { get; set; }
        public string Password { get; set; }

        

    }
}
