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
    
    public partial class ScreenResolutions
    {
        public ScreenResolutions()
        {
            this.Products = new HashSet<Products>();
        }
    
        public byte ScreenResolutionID { get; set; }
        public string ScreenResolutionName { get; set; }
        public string ScreenResolution { get; set; }
    
        public virtual ICollection<Products> Products { get; set; }
    }
}
