using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Payroll.Domains.Transitions;

namespace Payroll.Domains.Masters
{
    [Serializable, JsonObject]
    public class Profile : IdentityUser<int>
    {
        public virtual string EmployeeCode { get; set; }
        public virtual int InitialId { get; set; }
        public virtual Initial Initial { get; set; }
        public virtual int SexId { get; set; }
        public virtual Sex Sex { get; set; }
        public virtual string IdCard { get; set; }
        public virtual string FirstNameTH { get; set; }
        public virtual string LastNameTH { get; set; }
        public virtual string FirstNameEN { get; set; }
        public virtual string LastNameEN { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual DateTime FistDateToWork { get; set; }
        public virtual DateTime CreateOn { get; set; }
        public virtual string CreateBy { get; set; }
        public virtual DateTime UpdateOn { get; set; }
        public virtual string UpdateBy { get; set; }
        public virtual bool IsLeave { get; set; }
        public virtual int PositionId { get; set; }
        public virtual Position Position { get; set; }
        public virtual string ProfileImagePath { get; set; }

        public virtual HashSet<Address> Addresses { get; set; }
        public virtual HashSet<CheckInDetail> CheckInDetails { get; set; }
    }
}