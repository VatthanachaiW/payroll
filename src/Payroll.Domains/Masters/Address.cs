using System;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Address : BaseClassWithAuditAndIsActive<int>
    {
        public virtual int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual int SubDistrictId { get; set; }
        public virtual SubDistrict SubDistrict { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual int AddressTypeId { get; set; }
        public virtual AddressType AddressType { get; set; }
    }
}