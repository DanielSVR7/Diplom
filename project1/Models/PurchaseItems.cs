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
    
    public partial class PurchaseItems
    {
        public int PurchaseID { get; set; }
        public int ProductID { get; set; }
        public short ProductCount { get; set; }
    
        public virtual Products Products { get; set; }
        public virtual Purchases Purchases { get; set; }

        public Purchases Purchases1
        {
            get => default;
            set
            {
            }
        }

        public Products p
        {
            get => default;
            set
            {
            }
        }
    }
}
