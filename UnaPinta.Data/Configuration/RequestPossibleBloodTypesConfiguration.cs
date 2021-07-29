using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;

namespace UnaPinta.Data.Configuration
{
    public class RequestPossibleBloodTypesConfiguration : IEntityTypeConfiguration<RequestPossibleBloodTypes>
    {
        public void Configure(EntityTypeBuilder<RequestPossibleBloodTypes> builder)
        {
            builder.HasKey(rb => new { rb.RequestId, rb.BloodTypeId });

            builder.HasOne(r => r.RequestNav)
                .WithMany(p => p.PossibleBloodTypes)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.BloodTypeNav)
                .WithMany(p => p.Requests)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
