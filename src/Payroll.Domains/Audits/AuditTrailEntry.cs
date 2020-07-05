using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace Payroll.Domains.Audits
{
    [ExcludeFromCodeCoverage]
    public class AuditTrailEntry
    {
        public AuditTrailEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public virtual EntityEntry Entry { get; }
        public virtual string Action { get; set; }
        public virtual string TableName { get; set; }

        public virtual Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public virtual Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public virtual Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();

        public virtual List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();
        public virtual bool HasTemporaryProperties => TemporaryProperties.Any();

        public AuditTrail ToAuditTrail() => new AuditTrail
        {
            TableName = TableName,
            Action = Action,

            TimeStamp = DateTime.UtcNow,
            KeyValues = JsonConvert.SerializeObject(KeyValues),
            OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
            NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues)
        };
    }
}