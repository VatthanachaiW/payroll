using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class SubDistrict : BaseClassWithAuditAndIsActive<int>
    {
        public virtual string SubDistrictName { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual int DistrictId { get; set; }
        public virtual District District { get; set; }
        public virtual int ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        public HashSet<Address> Addresses { get; set; }
        public HashSet<Company> Companies { get; set; }
    }
}