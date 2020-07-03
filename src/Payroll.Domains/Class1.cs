using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Payroll.Domains
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
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
        public string ProfileImagePath { get; set; }
    }

    [Serializable, JsonObject]
    public class BaseClass<T>
    {
        public T Id { get; set; }
    }

    [Serializable, JsonObject]
    public class BaseClassWithAudit<T> : BaseClass<T>
    {
        public virtual DateTime CreateOn { get; set; }
        public virtual string CreateBy { get; set; }
        public virtual DateTime UpdateOn { get; set; }
        public virtual string UpdateBy { get; set; }
    }

    [Serializable, JsonObject]
    public class BaseClassWithIsActive<T> : BaseClass<T>
    {
        public bool IsActive { get; set; }
    }

    [Serializable, JsonObject]
    public class BaseClassWithAuditAndIsActive<T> : BaseClassWithAudit<T>
    {
        public bool IsActive { get; set; }
    }

    [Serializable, JsonObject]
    public class Initial : BaseClassWithAuditAndIsActive<int>
    {
        public string InitialName { get; set; }
    }

    [Serializable, JsonObject]
    public class Sex : BaseClassWithAuditAndIsActive<int>
    {
        public string SexName { get; set; }
    }

    [Serializable, JsonObject]
    public class SubDistrict : BaseClassWithAuditAndIsActive<int>
    {
        public string SubDistrictName { get; set; }
        public string ZipCode { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public int ProvinceId { get; set; }
        public Province Province { get; set; }
    }

    [Serializable, JsonObject]
    public class District : BaseClassWithAuditAndIsActive<int>
    {
        public string DistrictName { get; set; }

        public HashSet<SubDistrict> SubDistricts { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
    }

    [Serializable, JsonObject]
    public class Province : BaseClassWithAuditAndIsActive<int>
    {
        public string ProvinceName { get; set; }
        public HashSet<District> Districts { get; set; }
        public HashSet<SubDistrict> SubDistricts { get; set; }
    }

    [Serializable, JsonObject]
    public class Company : BaseClassWithAudit<int>
    {
        public string CompanyName { get; set; }
        public string SocialTax { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int SubDistrictId { get; set; }
        public SubDistrict SubDistrict { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public HashSet<Department> Departments { get; set; }
    }

    [Serializable, JsonObject]
    public class Department : BaseClassWithAuditAndIsActive<int>
    {
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public HashSet<Position> Positions { get; set; }
    }

    [Serializable, JsonObject]
    public class Position : BaseClassWithAuditAndIsActive<int>
    {
        public string PositionCode { get; set; }
        public string PositionName { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public HashSet<Profile> Profiles { get; set; }
    }

    [Serializable, JsonObject]
    public class WorkType : BaseClassWithAuditAndIsActive<int>
    {
        public string WorkTypeName { get; set; }
    }

    [Serializable, JsonObject]
    public class LeaveType : BaseClassWithAuditAndIsActive<int>
    {
        public string LeaveName { get; set; }
    }
}