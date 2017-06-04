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
    
    public partial class Organization
    {
        public Organization()
        {
            this.Owners = new HashSet<Owners>();
            this.Rentals = new HashSet<Rentals>();
            this.TransactionLogs = new HashSet<TransactionLogs>();
        }
    
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public byte[] Logo { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual ICollection<Owners> Owners { get; set; }
        public virtual ICollection<Rentals> Rentals { get; set; }
        public virtual ICollection<TransactionLogs> TransactionLogs { get; set; }
    }
}