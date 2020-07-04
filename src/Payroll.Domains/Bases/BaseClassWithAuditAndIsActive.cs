using System;
using Newtonsoft.Json;

namespace Payroll.Domains.Bases
{
    [Serializable, JsonObject]
    public class BaseClassWithAuditAndIsActive<T> : BaseClassWithAudit<T>
    {
        public bool IsActive { get; set; }
    }
}