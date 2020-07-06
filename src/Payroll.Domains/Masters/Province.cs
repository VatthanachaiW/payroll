using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Province : BaseClassWithAuditAndIsActive<int>
    {
        public virtual string ProvinceName { get; set; }

        public HashSet<District> Districts { get; set; }
        public HashSet<SubDistrict> SubDistricts { get; set; }

        public virtual int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}