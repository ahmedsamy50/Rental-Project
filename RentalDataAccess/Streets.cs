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
    
    public partial class Streets
    {
        public Streets()
        {
            this.Units = new HashSet<Units>();
        }
    
        public int StreetId { get; set; }
        public string EnglishName { get; set; }
        public Nullable<int> DistricId { get; set; }
        public string ArabicName { get; set; }
    
        public virtual Districts Districts { get; set; }
        public virtual ICollection<Units> Units { get; set; }
    }
}
