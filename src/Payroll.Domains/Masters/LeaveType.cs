using System;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class LeaveType : BaseClassWithAuditAndIsActive<int>
    {
        public virtual string LeaveName { get; set; }
    }
}