//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Manufacturers
    {
        public Manufacturers()
        {
            this.Products = new HashSet<Products>();
        }
    
        public short ManufacturerID { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string TechnicalSupportNumber { get; set; }
    
        public virtual ICollection<Products> Products { get; set; }

        public Products p
        {
            get => default;
            set
            {
            }
        }
    }
}
