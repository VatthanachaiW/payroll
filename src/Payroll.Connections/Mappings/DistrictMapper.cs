using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Domains.Masters;

namespace Payroll.Connections.Mappings
{
    public class DistrictMapper
    {
        public static void Config(EntityTypeBuilder<District> builder)
        {
            builder.ToTable("tb_districts");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("district_id").ValueGeneratedOnAdd();

            builder.Property(p => p.DistrictName).HasColumnName("district_name");
            
            builder.Property(p => p.ProvinceId).HasColumnName("province_id");


            builder.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true).ValueGeneratedOnAdd();
            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedOn).HasColumnName("created_on").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by").IsRequired(false);
            builder.Property(p => p.UpdatedOn).HasColumnName("updated_on").IsRequired(false);

            builder.HasOne(o => o.Province).WithMany(m => m.Districts).HasForeignKey(fk => fk.ProvinceId);
        }
    }
}