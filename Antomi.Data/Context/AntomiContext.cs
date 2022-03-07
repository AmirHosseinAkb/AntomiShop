using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Antomi.Data.Context
{
    public class AntomiContext:DbContext
    {
        public AntomiContext(DbContextOptions<AntomiContext> options) : base(options)
        {

        }
    }
}
