﻿using Crawler.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Persistence.Context
{
    public class CrawlerContext : DbContext
    {
        public CrawlerContext(DbContextOptions<CrawlerContext> options) : base(options) { }

        public DbSet<Processo>? Processos { get; set; }
        public DbSet<Movimentacao>? Movimentacoes { get; set; }
        public DbSet<SitProcesso>? SitProcessos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Processo>().HasMany(x => x.Movimentacoes).WithOne(x => x.Processo).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Processo>().HasMany(x => x.SitProcessos).WithOne(x => x.Processo).OnDelete(DeleteBehavior.Cascade);

        }
    }
    
}
