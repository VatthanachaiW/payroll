using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Domains.Audits;

namespace Payroll.Connections.Mappings
{
    public class AuditTailMapper
    {
        public static void Config(EntityTypeBuilder<AuditTrail> builder)
        {
            builder.ToTable("tb_audit_trails");

            builder.HasKey(k => k.AuditId);
            builder.Property(p => p.AuditId)
                .HasColumnName("audit_trail_id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.TimeStamp).HasColumnName("audit_trail_timestamp");
            builder.Property(p => p.Action).HasColumnName("audit_trail_action");
            builder.Property(p => p.TableName).HasColumnName("audit_trail_table_name");
            builder.Property(p => p.KeyValues).HasColumnName("audit_trail_key_values");
            builder.Property(p => p.OldValues).HasColumnName("audit_trail_old_values");
            builder.Property(p => p.NewValues).HasColumnName("audit_trail_new_values");
        }
    }
}