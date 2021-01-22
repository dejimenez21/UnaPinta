using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Api.Entities
{
    public partial class UnaPintaDBContext : IdentityDbContext<User, UserType, string>
    {
        public UnaPintaDBContext()
        {
        }

        public UnaPintaDBContext(DbContextOptions<UnaPintaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Condition> Conditions { get; set; }
        public virtual DbSet<BloodComponent> BloodComponents { get; set; }
        public virtual DbSet<WaitList> WaitLists { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<BloodType> BloodTypes { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ConfirmationCode> ConfirmationCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
