using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Payroll.Domains.Audits
{
    [Serializable, JsonObject]
    [ExcludeFromCodeCoverage]
    public class AuditTrail
    {
        public virtual int AuditId { get; set; }
        public virtual DateTime TimeStamp { get; set; }
        public virtual string Action { get; set; }
        public virtual string TableName { get; set; }
        public virtual string KeyValues { get; set; }
        public virtual string OldValues { get; set; }
        public virtual string NewValues { get; set; }
    }
}