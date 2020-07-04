using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Department : BaseClassWithAuditAndIsActive<int>
    {
        public virtual string DepartmentCode { get; set; }
        public virtual string DepartmentName { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public HashSet<Position> Positions { get; set; }
    }
}