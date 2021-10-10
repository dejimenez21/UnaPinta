using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UnaPinta.Data.Configuration;
using UnaPinta.Data.Entities;

#nullable disable

namespace UnaPinta.Data
{
    public partial class UnaPintaDBContext : IdentityDbContext<User, Role, long>
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
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<StringDate> StringDates { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Case> Cases { get; set; }

        
    }
}
