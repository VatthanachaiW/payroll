using System;
using Newtonsoft.Json;

namespace Payroll.Domains.Bases
{
    [Serializable, JsonObject]
    public class BaseClass<T>
    {
        public T Id { get; set; }
    }
}