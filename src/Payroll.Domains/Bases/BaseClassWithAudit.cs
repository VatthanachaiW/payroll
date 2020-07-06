using System;
using Newtonsoft.Json;

namespace Payroll.Domains.Bases
{
    [Serializable, JsonObject]
    public class BaseClassWithAudit<T> : BaseClass<T>
    {
        public virtual DateTime CreatedOn { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? UpdatedOn { get; set; }
        public virtual string UpdatedBy { get; set; }
    }
}