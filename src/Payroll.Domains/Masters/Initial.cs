using System;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Initial : BaseClassWithAuditAndIsActive<int>
    {
        public virtual string InitialName { get; set; }
    }
}