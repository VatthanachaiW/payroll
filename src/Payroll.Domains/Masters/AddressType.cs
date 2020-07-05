using System.Collections.Generic;
using Payroll.Domains.Bases;

namespace Payroll.Domains.Masters
{
    public class AddressType:BaseClassWithIsActive<int>
    {
        public virtual string AddressTypeName { get; set; }

        public HashSet<Address> Addresses { get; set; }
    }
}