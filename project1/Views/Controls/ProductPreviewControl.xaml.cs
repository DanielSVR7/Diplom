using project1.Models;
using project1.Views.Windows;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace project1.Views.Controls
{
    public partial class ProductPreviewControl : UserControl, INotifyPropertyChanged
    {

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        private bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
        #endregion 
        
        ApplianceStoreEntities db = new ApplianceStoreEntities();
        private Products _Product;
        private string _ProductImage;
        public string ProductImage { get => _ProductImage; set => Set(ref _ProductImage, value); }
        public Products Product { get => _Product; set => Set(ref _Product, value); }
        private string _ProductTitle;
        public string ProductTitle { get => _ProductTitle; set => Set(ref _ProductTitle, value); }

        private bool _IsButtonEnabled = true;
        public bool IsButtonEnabled { get => _IsButtonEnabled; set => Set(ref _IsButtonEnabled, value);}
        CatalogWindow Owner;
        public ProductPreviewControl(Products product, CatalogWindow owner)
        {
            Owner = owner;
            Product = product;
            ProductImage = Directory.GetCurrentDirectory() + '/' + Product.Image;
            DataContext = this;
            InitializeComponent();
            if (Product != null)
            {
                ManufacturerName.Text = Product.Manufacturers.CompanyName;
                Warranty.Text = Product.Warranty.ToString() + " месяцев";
                if (Product.Category == 1)
                {
                    ProductTitle = Product.ScreenSizes.ScreenSizeInInches.ToString() + "\" (" + 
                        Product.ScreenSizes.ScreenSizeInCentimeters.ToString() + " см) " +
                         "Телевизор " + Product.Manufacturers.CompanyName + ' ' + Product.Model + ' ' +
                         Product.Colors.ColorName + " [" + Product.ScreenResolutions.ScreenResolutionName + ", " +
                         Product.ScreenResolutions.ScreenResolution + ", " + Product.BacklightTypes.BacklightTypeName + ", " +
                         Product.OperatingSystems.OperatingSystemName + ']';
                    Property1.Text = "Разрешение экрана";
                    Property2.Text = "Поддержка SmartTV";
                    Property3.Text = "Поддержка HDR";
                    Value1.Text = Product.ScreenResolutions.ScreenResolutionName + " (" + Product.ScreenResolutions.ScreenResolution + ')';
                    if (Product.SmartTVSupport == false || Product.SmartTVSupport == null)
                        Value2.Text = "Нет";
                    else
                        Value2.Text = "Есть";
                    if (Product.HDRSupport == false || Product.HDRSupport == null)
                        Value3.Text = "Нет";
                    else
                        Value3.Text = "Есть";
                }
                else if (Product.Category == 2)
                {
                    ProductTitle = "Холодильник " + Product.FreezerLocations.FreezerLocationName + ' ' + Product.Manufacturers.CompanyName + ' ' +
                        Product.Model + ' ' + Product.Colors.ColorName + " [" + (Product.RefrigeratorVolume + Product.FreezerVolume).ToString() + " л, " +
                        Product.Width + " см x " + Product.Height + " см x " + Product.Depth + " см]";
                    Property1.Text = "Компоновка";
                    Property2.Text = "Инверторный компрессор";
                    Property3.Text = "Зона свежести";
                    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                    Value1.Text = ti.ToTitleCase(Product.FreezerLocations.FreezerLocationName);
                    if (Product.InverterCompressor == false || Product.InverterCompressor == null)
                        Value2.Text = "Нет";
                    else
                        Value2.Text = "Есть";
                    if (Product.FreshnessZone == false || Product.FreshnessZone == null)
                        Value3.Text = "Нет";
                    else
                        Value3.Text = "Есть";
                }
                else
                {
                    ProductTitle = Product.Manufacturers.CompanyName + ' ' + Product.Model + ' ' + Product.Colors.ColorName +
                        " [" + Product.Width + " см x " + Product.Height + " см x " + Product.Depth + " см]";
                    Property1.Text = "Категория";
                    Property2.Text = "Энергетический класс";
                    Property3.Text = "Потребление энергии";
                    Value1.Text = Product.Categories.CategoryName;
                    Value2.Text = Product.EnergyClasses.EnergyClassName;
                    Value3.Text = Product.PowerConsumption.ToString();
                }
                if(Owner.IsAdmin)
                {
                    EditButton.Visibility = Visibility.Visible;
                    DeleteButton.Visibility = Visibility.Visible;
                }
            }

        }

        private void MainBorder_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DetailedProductWindow d = new DetailedProductWindow(Product, this);
            d.Owner = Owner; 
            d.Show();
        }
        public void AddToShoppingCartButton_Click(object sender, RoutedEventArgs e)
        {
            Owner.ShoppingCartList.Add(Product);
            IsButtonEnabled = false;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow ew = new EditWindow(Product, Owner.db);
            ew.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы уверены, что хотите удалить данный товар?", "Требуется подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                var deleted_product = (from p in db.Products where Product.ProductID == p.ProductID select p).First();
                db.Products.Remove(deleted_product);
                db.SaveChanges();
                Owner.ChangeCategory(Owner.SelectedCategory.CategoryID);
            }
        }
    }
}
