using System;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Salary : BaseClassWithAuditAndIsActive<int>
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public decimal Decimal { get; set; }
        public decimal CurrentSalary { get; set; }
    }
}