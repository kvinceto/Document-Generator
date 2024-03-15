using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocGen.Data.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using static DocGen.Common.ApplicationGlobalConstants;

namespace DocGen.Data
{
    public class DocGenDbContext : IdentityDbContext<IdentityUser<string>, IdentityRole<string>, string>
    {
        public DocGenDbContext(DbContextOptions<DocGenDbContext> options)
        : base(options) { }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Company>(c =>
            {
                c.HasKey(c => c.Id);
            });

            builder.Entity<Client>(c =>
            {
                c.HasKey(c => c.Id);
            });

            builder.Entity<Invoice>(i =>
            {
                i.HasKey(i => i.Id);

                i.HasOne(i => i.Company)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);

                i.HasOne(i => i.Client)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

                i.Property(i => i.Subtotal).HasColumnType("decimal(18,2)");
                i.Property(i => i.Discount).HasColumnType("decimal(18,2)");
                i.Property(i => i.Tax).HasColumnType("decimal(18,2)");
                i.Property(i => i.Total).HasColumnType("decimal(18,2)");
                i.Property(i => i.DateOfIsue).HasColumnType("datetime2");
                i.Property(i => i.DateOfServiceProvided).HasColumnType("datetime2");
                i.Property(i => i.DueDate).HasColumnType("datetime2");
            });

            base.OnModelCreating(builder);
        }
    }
}
