using System;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Profile : IdentityUser<int>
    {
        public virtual string EmployeeCode { get; set; }
        public virtual int InitialId { get; set; }
        public virtual Initial Initial { get; set; }
        public virtual Sex Sex { get; set; }
        public virtual string IdCard { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual DateTime FistDateToWork { get; set; }
        public virtual DateTime CreateOn { get; set; }
        public virtual string CreateBy { get; set; }
        public virtual DateTime UpdateOn { get; set; }
        public virtual string UpdateBy { get; set; }
        public virtual bool IsLeave { get; set; }
        public virtual int PositionId { get; set; }
        public virtual Position Position { get; set; }
        public string ProfileImagePath { get; set; }
    }
}