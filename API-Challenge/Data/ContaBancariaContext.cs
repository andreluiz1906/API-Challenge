using API_Challenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Challenge.Data
{
    public class ContaBancariaContext : DbContext
    {
        public ContaBancariaContext(DbContextOptions<ContaBancariaContext> opt) : base(opt)
        {

        }

        public DbSet<Conta> Contas { get; set; }

    }
}
