using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    public class Address : BaseClassWithAuditAndIsActive<int>
    {
        public virtual int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }

        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual int SubDistrictID { get; set; }
        public virtual SubDistrict SubDistrict { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual AddressType AddressType { get; set; }
    }
}