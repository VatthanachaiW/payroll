using System;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class SalaryType : BaseClassWithAuditAndIsActive<int>
    {
        public string SalaryTypeName { get; set; }
    }
}