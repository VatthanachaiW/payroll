using System;
using Newtonsoft.Json;

namespace Payroll.Domains.Bases
{
    [Serializable, JsonObject]
    public class BaseClassWithAudit<T> : BaseClass<T>
    {
        public virtual DateTime CreateOn { get; set; }
        public virtual string CreateBy { get; set; }
        public virtual DateTime UpdateOn { get; set; }
        public virtual string UpdateBy { get; set; }
    }
}