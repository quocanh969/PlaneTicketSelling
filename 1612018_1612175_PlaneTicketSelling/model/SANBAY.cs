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
    
    public partial class SANBAY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANBAY()
        {
            this.CHUYENBAYs = new HashSet<CHUYENBAY>();
            this.CHUYENBAYs1 = new HashSet<CHUYENBAY>();
            this.SBTRUNGGIANs = new HashSet<SBTRUNGGIAN>();
        }
    
        public string MaSB { get; set; }
        public string TenSB { get; set; }
        public string DiaDiem { get; set; }
        public Nullable<bool> IsAvailable { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHUYENBAY> CHUYENBAYs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHUYENBAY> CHUYENBAYs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SBTRUNGGIAN> SBTRUNGGIANs { get; set; }
    }
}
