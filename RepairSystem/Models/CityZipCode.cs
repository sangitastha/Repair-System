//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RepairSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CityZipCode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CityZipCode()
        {
            this.ServiceProviders = new HashSet<ServiceProvider>();
        }
    
        public int ZipId { get; set; }
        public int CityId { get; set; }
        public string ZipNumber { get; set; }
    
        public virtual CountryCity CountryCity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }
    }
}