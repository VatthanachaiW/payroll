using System;
using Payroll.Domains.Bases;
using Payroll.Domains.Masters;

namespace Payroll.Domains.Transitions
{
    public class WorkDetail : BaseClassWithAuditAndIsActive<int>
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

    }
}