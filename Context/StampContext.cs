using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskEF.Models;

namespace TestTaskEF.Context
{
    public class StampContext: DbContext
    {
        public StampContext(DbContextOptions<StampContext> option) : base(option) { Database.EnsureCreated(); }
        public DbSet<StampModel> Stamps { get; set; }
        public DbSet<ListModel> Lists { get; set; }
        public DbSet<DeptModel> Depts { get; set; }
    }
}
