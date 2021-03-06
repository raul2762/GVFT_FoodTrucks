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
    
    public partial class Employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employees()
        {
            this.Paysheet = new HashSet<Paysheet>();
            this.Login_FT = new HashSet<Login_FT>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Last_name { get; set; }
        public string Cedula { get; set; }
        public int Department { get; set; }
        public int Salary { get; set; }
        public Nullable<System.DateTime> Entry_date { get; set; }
        public string Phone { get; set; }
        public int Pay_mode { get; set; }
    
        public virtual Departments Departments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paysheet> Paysheet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Login_FT> Login_FT { get; set; }
        public virtual PaysheetModePay PaysheetModePay { get; set; }
    }
}
