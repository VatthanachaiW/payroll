using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Position : BaseClassWithAuditAndIsActive<int>
    {
        public virtual string PositionCode { get; set; }
        public virtual string PositionName { get; set; }
        public virtual int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public HashSet<Profile> Profiles { get; set; }
    }
}