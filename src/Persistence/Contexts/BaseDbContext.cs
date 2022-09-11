using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Technologies);
            });

            modelBuilder.Entity<Technology>(a =>
            {
                a.ToTable("Technologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasOne(P => P.ProgrammingLanguage);
            });

            modelBuilder.Entity<User>(p =>
            {
                p.ToTable("Users").HasKey(u => u.Id);
                p.Property(u => u.Id).HasColumnName("Id");
                p.Property(u => u.FirstName).HasColumnName("FirstName");
                p.Property(u => u.LastName).HasColumnName("LastName");
                p.Property(u => u.Email).HasColumnName("Email");
                p.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
                p.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
                p.Property(u => u.Status).HasColumnName("Status");
                p.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType");
                p.HasMany(c => c.UserOperationClaims);
                p.HasMany(c => c.RefreshTokens);
            });

            modelBuilder.Entity<OperationClaim>(p =>
            {
                p.ToTable("OperationClaims").HasKey(o => o.Id);
                p.Property(o => o.Id).HasColumnName("Id");
                p.Property(o => o.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(p =>
            {
                p.ToTable("UserOperationClaims").HasKey(u => u.Id);
                p.Property(u => u.Id).HasColumnName("Id");
                p.Property(u => u.UserId).HasColumnName("UserId");
                p.Property(u => u.OperationClaimId).HasColumnName("OperationClaimId");
                p.HasOne(u => u.User);
                p.HasOne(u => u.OperationClaim);
            });

            ProgrammingLanguage[] programmingLanguageSeeds = { new(1, "C#"), new(2, "Java"), new(3, "Go") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageSeeds);

            Technology[] technologySeeds = { new(1, 1, "asp.net"),new(2,1,"Wpf"),new(3,2,"spring") };
            modelBuilder.Entity<Technology>().HasData(technologySeeds);

            OperationClaim[] operationClaimsEntitySeeds = { new(1, "admin") ,new(2, "moderator") };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaimsEntitySeeds);

        }
    }
}
