using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antomi.Data.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Antomi.Data.Context
{
    public class AntomiContext:DbContext
    {
        public AntomiContext(DbContextOptions<AntomiContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
