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
    
    public partial class TransactionLogTypes
    {
        public TransactionLogTypes()
        {
            this.TransactionLogs = new HashSet<TransactionLogs>();
        }
    
        public int TransactionLogITypeId { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
    
        public virtual ICollection<TransactionLogs> TransactionLogs { get; set; }
    }
}
