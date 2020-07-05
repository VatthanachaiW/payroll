using System;
using System.Collections.Generic;
using Payroll.Domains.Bases;
using Payroll.Domains.Masters;

namespace Payroll.Domains.Transitions
{
    public class CheckInDetail : BaseClassWithAuditAndIsActive<int>
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public HashSet<DateTime> CheckInTimes { get; set; }
    }

    public class Salary : BaseClassWithAuditAndIsActive<int>
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public decimal Decimal { get; set; }
        public decimal CurrentSalary { get; set; }
        public decimal OT { get; set; }
        
    }
}