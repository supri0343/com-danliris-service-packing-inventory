﻿
using Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalMDSalesNote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.LocalMDSalesNote
{
    public class GarmentMDLocalSalesNoteDetailConfig : IEntityTypeConfiguration<GarmentMDLocalSalesNoteDetailModel>
    {
        public void Configure(EntityTypeBuilder<GarmentMDLocalSalesNoteDetailModel> builder)
        {
            /* StandardEntity */
            builder.HasKey(s => s.Id);
            builder.Property(s => s.CreatedAgent).HasMaxLength(128);
            builder.Property(s => s.CreatedBy).HasMaxLength(128);
            builder.Property(s => s.LastModifiedAgent).HasMaxLength(128);
            builder.Property(s => s.LastModifiedBy).HasMaxLength(128);
            builder.Property(s => s.DeletedAgent).HasMaxLength(128);
            builder.Property(s => s.DeletedBy).HasMaxLength(128);
            builder.HasQueryFilter(f => !f.IsDeleted);
            /* StandardEntity */


            builder
                .Property(s => s.BonNo)
                .HasMaxLength(250);

            builder
                .Property(s => s.UomUnit)
                .HasMaxLength(250);

            builder
                .Property(s => s.BonFrom)
                .HasMaxLength(250);

            builder
               .Property(s => s.ComodityCode)
               .HasMaxLength(25);

            builder
               .Property(s => s.ComodityName)
               .HasMaxLength(100);


            builder
               .Property(s => s.RONo)
               .HasMaxLength(15);

            builder
              .HasMany(h => h.DetailItems)
             .WithOne()
             .HasForeignKey(f => f.LocalSalesNoteDetailId);


        }
    }
}
