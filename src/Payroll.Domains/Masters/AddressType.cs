using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class AddressType:BaseClassWithIsActive<int>
    {
        public virtual string AddressTypeName { get; set; }

        public HashSet<Address> Addresses { get; set; }
    }
}