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
    
    public partial class PaysheetModePay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PaysheetModePay()
        {
            this.Employees = new HashSet<Employees>();
        }
    
        public int Id { get; set; }
        public string PayMode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employees> Employees { get; set; }
        public virtual PaysheetModePay PaysheetModePay1 { get; set; }
        public virtual PaysheetModePay PaysheetModePay2 { get; set; }
    }
}
