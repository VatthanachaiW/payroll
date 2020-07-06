using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Country : BaseClassWithAuditAndIsActive<int>
    {
        public string CountryName { get; set; }

        public HashSet<Province> Provinces { get; set; }
    }
}