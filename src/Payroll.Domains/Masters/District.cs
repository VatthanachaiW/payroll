using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class District : BaseClassWithAuditAndIsActive<int>
    {
        public virtual string DistrictName { get; set; }

        public HashSet<SubDistrict> SubDistricts { get; set; }
        public virtual int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
    }
}