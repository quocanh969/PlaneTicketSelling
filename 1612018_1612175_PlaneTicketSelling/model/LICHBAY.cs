//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _1612018_1612175_PlaneTicketSelling.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class LICHBAY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LICHBAY()
        {
            this.HOADONs = new HashSet<HOADON>();
            this.SBTRUNGGIANs = new HashSet<SBTRUNGGIAN>();
        }
    
        public int MaCB { get; set; }
        public Nullable<System.TimeSpan> GioDi { get; set; }
        public System.DateTime NgayDi { get; set; }
        public Nullable<int> SoGheVip { get; set; }
        public Nullable<int> SoGheThuong { get; set; }
        public Nullable<bool> IsAvailable { get; set; }
    
        public virtual CHUYENBAY CHUYENBAY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SBTRUNGGIAN> SBTRUNGGIANs { get; set; }
    }
}