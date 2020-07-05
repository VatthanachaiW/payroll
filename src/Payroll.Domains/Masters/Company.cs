using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Company : BaseClassWithAudit<int>
    {
        public virtual string CompanyNameTH { get; set; }
        public virtual string CompanyNameEN { get; set; }
        public virtual string LegalEntityNumber { get; set; }
        public virtual string TaxNumber { get; set; }
        public virtual string BankAccountNumber { get; set; }
        public virtual string SocialNumber { get; set; }
        public virtual bool IsSSOPaid { get; set; }
        public virtual string SSONumber { get; set; }
        public virtual string Branch { get; set; }
        public virtual DateTime AccountStartDate { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual int SubDistrictId { get; set; }
        public virtual SubDistrict SubDistrict { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
        public HashSet<Department> Departments { get; set; }
    }
}