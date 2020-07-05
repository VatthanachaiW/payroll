using System;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Role : IdentityRole<int>
    {
        public virtual string Permissions { get; set; }
    }
}