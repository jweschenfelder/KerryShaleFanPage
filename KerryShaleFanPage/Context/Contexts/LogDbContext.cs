﻿using Microsoft.EntityFrameworkCore;
using System;
using KerryShaleFanPage.Context.Entities;

namespace KerryShaleFanPage.Context.Contexts
{
    public class LogDbContext : DbContext
    {
        private readonly string? _connectionString;

        public LogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
            try
            {
                _connectionString = Database.GetConnectionString();
                Database.Migrate();
            }
            catch (Exception ex) 
            { 
            }
        }

        public LogDbContext(string? connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(_connectionString ?? "server=127.0.0.1;database=kerryshalefanpg;uid={username};pwd={password};");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<LogEntry>? LogEntries { get; set; }
    }
}
