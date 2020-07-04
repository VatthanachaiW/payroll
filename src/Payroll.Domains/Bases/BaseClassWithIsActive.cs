using System;
using Newtonsoft.Json;

namespace Payroll.Domains.Bases
{
    [Serializable, JsonObject]
    public class BaseClassWithIsActive<T> : BaseClass<T>
    {
        public bool IsActive { get; set; }
    }
}