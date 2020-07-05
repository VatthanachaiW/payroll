using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Payroll.Domains.Bases;
using Payroll.Domains.Masters;

namespace Payroll.Domains.Transitions
{
    [Serializable, JsonObject]
    public class CheckInDetail : BaseClassWithAuditAndIsActive<int>
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public HashSet<DateTime> CheckInTimes { get; set; }
    }
}