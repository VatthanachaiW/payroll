using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Company : BaseClassWithAudit<int>
    {
        public virtual string CompanyName { get; set; }
        public virtual string SocialTax { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual int SubDistrictId { get; set; }
        public virtual SubDistrict SubDistrict { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
        public HashSet<Department> Departments { get; set; }
    }
}