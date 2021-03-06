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
    
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            this.Detail_invoice = new HashSet<Detail_invoice>();
            this.Paid_orders = new HashSet<Paid_orders>();
        }
    
        public int Id { get; set; }
        public Nullable<int> Id_client { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Id_user { get; set; }
        public string Name_order { get; set; }
        public string Comment_order { get; set; }
        public string Comment_paymaent { get; set; }
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detail_invoice> Detail_invoice { get; set; }
        public virtual Login_FT Login_FT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paid_orders> Paid_orders { get; set; }
    }
}
