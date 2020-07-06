using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Domains.Masters;

namespace Payroll.Connections.Mappings
{
    public class CountryMapper
    {
        public static void Config(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("tb_countries");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("country_id").ValueGeneratedOnAdd();

            builder.Property(p => p.CountryName).HasColumnName("country_name").IsRequired();

            builder.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedOn).HasColumnName("created_on").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by").IsRequired(false);
            builder.Property(p => p.UpdatedOn).HasColumnName("updated_on").IsRequired(false);
        }
    }
}