//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data_GVFT.Business.BusinessEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Paid_orders
    {
        public int Id { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<int> Payment_type { get; set; }
        public Nullable<int> Id_order { get; set; }
        public Nullable<int> Id_credit { get; set; }
        public string Percent_discount { get; set; }
        public Nullable<decimal> Discount { get; set; }
    
        public virtual Invoice Invoice { get; set; }
        public virtual Method_of_payment Method_of_payment { get; set; }
    }
}
