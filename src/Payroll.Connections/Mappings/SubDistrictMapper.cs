using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Domains.Masters;

namespace Payroll.Connections.Mappings
{
    public class SubDistrictMapper
    {
        public static void Config(EntityTypeBuilder<SubDistrict> builder)
        {
            builder.ToTable("tb_sub_districts");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("sub_district_id").ValueGeneratedOnAdd();

            builder.Property(p => p.SubDistrictName).HasColumnName("sub_district_name").ValueGeneratedOnAdd();
            builder.Property(p => p.ZipCode).HasColumnName("zipcode").ValueGeneratedOnAdd();
            builder.Property(p => p.DistrictId).HasColumnName("district_id").ValueGeneratedOnAdd();
            builder.Property(p => p.ProvinceId).HasColumnName("province_id").ValueGeneratedOnAdd();

            builder.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true).ValueGeneratedOnAdd();
            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedOn).HasColumnName("created_on").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by").IsRequired(false);
            builder.Property(p => p.UpdatedOn).HasColumnName("updated_on").IsRequired(false);


            builder.HasOne<District>(o => o.District).WithMany(m => m.SubDistricts).HasForeignKey(fk => fk.DistrictId);
            builder.HasOne<Province>(o => o.Province).WithMany(m => m.SubDistricts).HasForeignKey(fk => fk.ProvinceId);
        }
    }
}