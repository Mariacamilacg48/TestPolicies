﻿using GAPTest.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Data
{
    public class DataContext :  IdentityDbContext<User> 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public DbSet<Policy> Policies{ get; set; }
        public DbSet<CoveringType> CoveringTypes { get; set; }
        public DbSet<RiskType> RiskTypes { get; set; }

    }
    
}
