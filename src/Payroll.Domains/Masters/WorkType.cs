using System;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class WorkType : BaseClassWithAuditAndIsActive<int>
    {
        public virtual string WorkTypeName { get; set; }
    }
}