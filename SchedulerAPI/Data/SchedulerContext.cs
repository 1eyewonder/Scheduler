﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Data
{
    public class SchedulerContext : DbContext
    {
        public SchedulerContext(DbContextOptions<SchedulerContext> options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<QuoteRevision> QuoteRevisions { get; set; }
        public DbSet<JobRevision> JobRevisions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Originally had quotenumber as unique but realized copy jobs could be more fluid if quote numbers could be copied
            //builder.Entity<Job>(entity => entity.HasIndex(e => e.QuoteNumber).IsUnique());
            builder.Entity<Job>(entity => entity.HasIndex(e => e.JobNumber).IsUnique());
        }
    }
}