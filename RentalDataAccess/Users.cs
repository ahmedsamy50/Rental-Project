//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentalDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public Users()
        {
            this.Owners = new HashSet<Owners>();
            this.Rentals = new HashSet<Rentals>();
            this.TransactionLogs = new HashSet<TransactionLogs>();
            this.UnitPrices = new HashSet<UnitPrices>();
            this.Modules = new HashSet<Modules>();
        }
    
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public bool Exist { get; set; }
        public System.DateTime Dated { get; set; }
        public int OrganizationId { get; set; }
        public int UserRegId { get; set; }
        public bool Deleted { get; set; }
        public int NationalityId { get; set; }
    
        public virtual Nationalities Nationalities { get; set; }
        public virtual ICollection<Owners> Owners { get; set; }
        public virtual ICollection<Rentals> Rentals { get; set; }
        public virtual ICollection<TransactionLogs> TransactionLogs { get; set; }
        public virtual ICollection<UnitPrices> UnitPrices { get; set; }
        public virtual ICollection<Modules> Modules { get; set; }
    }
}