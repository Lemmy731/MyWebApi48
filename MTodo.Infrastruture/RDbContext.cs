using ETodo.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTodo.Infrastruture
{
    public class RDbContext: IdentityDbContext<AppTodoItem>
    {
        public RDbContext(DbContextOptions<RDbContext> options): base(options)
        {

        }
        public DbSet<Activity> Activities { get; set; }

    }
}
