﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ApplianceStoreEntities : DbContext
    {
        public ApplianceStoreEntities()
            : base("name=ApplianceStoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BacklightTypes> BacklightTypes { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<DiscountLevels> DiscountLevels { get; set; }
        public DbSet<EnergyClasses> EnergyClasses { get; set; }
        public DbSet<FreezerLocations> FreezerLocations { get; set; }
        public DbSet<Managers> Managers { get; set; }
        public DbSet<Manufacturers> Manufacturers { get; set; }
        public DbSet<OperatingSystems> OperatingSystems { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<PurchaseItems> PurchaseItems { get; set; }
        public DbSet<Purchases> Purchases { get; set; }
        public DbSet<ScreenResolutions> ScreenResolutions { get; set; }
        public DbSet<ScreenSizes> ScreenSizes { get; set; }
    }
}
