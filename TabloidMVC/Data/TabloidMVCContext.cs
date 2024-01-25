using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TabloidMVC.Models;

namespace TabloidMVC.Data
{
    public class TabloidMVCContext : DbContext
    {
        public TabloidMVCContext (DbContextOptions<TabloidMVCContext> options)
            : base(options)
        {
        }

        public DbSet<TabloidMVC.Models.Category> Category { get; set; } = default!;
    }
}
