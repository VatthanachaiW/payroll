using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Domains.Masters;

namespace Payroll.Connections.Mappings
{
    public class AddressMapper
    {
        public static void Config(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("tb_addresses");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("address_id").ValueGeneratedOnAdd();

            builder.Property(p => p.ProfileId).HasColumnName("profile_id").ValueGeneratedOnAdd();
            builder.Property(p => p.AddressLine1).HasColumnName("address_line1").ValueGeneratedOnAdd();
            builder.Property(p => p.AddressLine2).HasColumnName("address_line2").ValueGeneratedOnAdd();
            builder.Property(p => p.SubDistrictId).HasColumnName("subDistrict_id").ValueGeneratedOnAdd();
            builder.Property(p => p.ZipCode).HasColumnName("zipcode").ValueGeneratedOnAdd();
            builder.Property(p => p.ZipCode).HasColumnName("zipcode").ValueGeneratedOnAdd();
            builder.Property(p => p.AddressTypeId).HasColumnName("address_type_id").ValueGeneratedOnAdd();

            builder.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true).ValueGeneratedOnAdd();
            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedOn).HasColumnName("created_on").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by").IsRequired(false);
            builder.Property(p => p.UpdatedOn).HasColumnName("updated_on").IsRequired(false);

            builder.HasOne(o => o.Profile).WithMany(m => m.Addresses).HasForeignKey(fk => fk.ProfileId).IsRequired();
            builder.HasOne(o => o.SubDistrict).WithMany(m => m.Addresses).HasForeignKey(fk => fk.SubDistrictId).IsRequired();
            builder.HasOne(o => o.AddressType).WithMany(m => m.Addresses).HasForeignKey(fk => fk.AddressTypeId).IsRequired();
        }
    }
}