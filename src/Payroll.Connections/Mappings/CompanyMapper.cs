using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Domains.Masters;

namespace Payroll.Connections.Mappings
{
    public class CompanyMapper
    {
        public static void Config(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("tb_address_types");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("address_type_id").ValueGeneratedOnAdd();

            builder.Property(p => p.CompanyNameTH).HasColumnName("company_name_th").IsRequired();
            builder.Property(p => p.CompanyNameEN).HasColumnName("company_name_en").IsRequired();

            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedOn).HasColumnName("created_on").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by").IsRequired(false);
            builder.Property(p => p.UpdatedOn).HasColumnName("updated_on").IsRequired(false);
        }
    }
}