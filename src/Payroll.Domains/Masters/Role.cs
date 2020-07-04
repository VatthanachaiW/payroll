using Microsoft.AspNetCore.Identity;

namespace Payroll.Domains.Masters
{
    public class Role : IdentityRole<int>
    {
        public virtual string Permissions { get; set; }
    }
}