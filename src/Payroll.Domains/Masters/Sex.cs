using System;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Sex : BaseClassWithAuditAndIsActive<int>
    {
        public virtual string SexName { get; set; }
    }
}