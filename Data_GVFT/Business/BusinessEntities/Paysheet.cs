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
    
    public partial class Paysheet
    {
        public int Id { get; set; }
        public int Id_employee { get; set; }
        public int Amount { get; set; }
        public System.DateTime Payment_date { get; set; }
    
        public virtual Employees Employees { get; set; }
    }
}
