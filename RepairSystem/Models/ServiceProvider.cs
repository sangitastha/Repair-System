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
    
    public partial class ServiceProvider
    {
        public int ServiceProviderId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Price { get; set; }
        public string ContactNumber { get; set; }
        public int CategoryId { get; set; }
        public string Location { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int ZipId { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual CityZipCode CityZipCode { get; set; }
        public virtual Country Country { get; set; }
        public virtual CountryCity CountryCity { get; set; }
    }
}
