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

            //Seed first user
            IdentityUser user = new IdentityUser()
            {
                Id = "868decd8-dfa1-44ac-a3b7-eae0d513a199",
                UserName = "Krasimir",
                NormalizedUserName = "KRASIMIR",
                Email = "test@test.bg",
                NormalizedEmail = "TEST@TEST.BG",
                EmailConfirmed = true,
                ConcurrencyStamp = "868decd8-dfa1-44ac-a3b7-eae0d513a199",
                PhoneNumber = null,
                PhoneNumberConfirmed = true,
                SecurityStamp = "868decd8-dfa1-44ac-a3b7-eae0d513a199",
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                AccessFailedCount = 0,
            };

            var hasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = hasher.HashPassword(user, "Krasito123");

            builder.Entity<IdentityUser>(u =>
            {
                u.HasData(user);
            });

            //Seed roles
            builder.Entity<IdentityRole>(r =>
            {
                r.HasData(new List<IdentityRole>()
                {
                    new IdentityRole()
                    {
                        Id = "762e22de-7afb-42c6-afbc-a14435f446a0",
                        Name = UserRoleName,
                        ConcurrencyStamp = "762e22de-7afb-42c6-afbc-a14435f446a0",
                        NormalizedName = UserRoleName
                    },
                    new IdentityRole()
                    {
                         Id = "6bb41b5a-8b5b-4378-a739-3ad34c8976a3",
                        Name = AdminRoleName,
                        ConcurrencyStamp = "6bb41b5a-8b5b-4378-a739-3ad34c8976a3",
                        NormalizedName = AdminRoleName
                    }
                });
            });

            base.OnModelCreating(builder);
        }
    }
}
