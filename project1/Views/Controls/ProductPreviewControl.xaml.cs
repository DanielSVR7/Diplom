using project1.Models;
using project1.Views.Windows;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

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

        private Products _Product;
        private string _ProductImage;
        public string ProductImage { get => _ProductImage; set => Set(ref _ProductImage, value); }
        public Products Product { get => _Product; set => Set(ref _Product, value); }
        private string _ProductTitle;
        public string ProductTitle { get => _ProductTitle; set => Set(ref _ProductTitle, value); }

        private bool _IsButtonEnabled = true;
        public bool IsButtonEnabled { get => _IsButtonEnabled; set => Set(ref _IsButtonEnabled, value); }

        readonly CatalogWindow Owner;
        public ProductPreviewControl(Products product, CatalogWindow owner)
        {
            Owner = owner;
            Product = product;
            ProductImage = Directory.GetCurrentDirectory() + '/' + Product.Image ?? "Data/Images/default.png";
            DataContext = this;
            InitializeComponent();
            if (Product != null)
            {
                ManufacturerName.Text = Product.Manufacturers.CompanyName;
                Warranty.Text = Product.Warranty.ToString() + " месяцев";
                switch (Product.Category)
                {

                    case 1:
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
                            break;
                        }
                    case 2:
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
                            break;
                        }
                    case 3:
                        {
                            ProductTitle = "Стиральная машина " + Product.Manufacturers.CompanyName + ' ' + Product.Model +
                                " [стирка -  " + Product.LaundryLoad + " кг, расход " + Product.WaterConsumption + " л, отжим - " +
                                Product.MaximumSpinSpeed + " об/мин, программ - " + Product.NumberOfPrograms + ']';
                            Property1.Text = "Потребление воды";
                            Value1.Text = Product.WaterConsumption + " л";
                            Property2.Text = "Отжим";
                            Value2.Text = Product.MaximumSpinSpeed + " об/мин";
                            Property3.Text = "Диапазон температур";
                            Value3.Text = Product.TemperatureRange + " °C";
                            break;
                        }
                    case 4:
                        {
                            ProductTitle = "Микроволновая печь " + Product.Manufacturers.CompanyName + ' ' + Product.Model + ' ' +
                                Product.Colors.ColorName + "[" + Product.InternalVolume + " л]";
                            Property1.Text = "Страна-производитель";
                            Value1.Text = Product.Manufacturers.Country;
                            Property2.Text = "Внутренний объём";
                            Value2.Text = Product.InternalVolume + " л";
                            Property3.Text = "Наличие гриля";
                            if (Product.Grill == false || Product.Grill == null)
                                Value3.Text = "Нет";
                            else
                                Value3.Text = "Есть";
                            break;
                        }
                    default:
                        {
                            ProductTitle = Product.Manufacturers.CompanyName + ' ' + Product.Model + ' ' + Product.Colors.ColorName +
                                " [" + Product.Width + " см x " + Product.Height + " см x " + Product.Depth + " см]";
                            Property1.Text = "Категория";
                            Property2.Text = "Энергетический класс";
                            Property3.Text = "Потребление энергии";
                            Value1.Text = Product.Categories.CategoryName;
                            Value2.Text = "нет данных";
                            Value3.Text = "нет данных";
                            break;
                        }
                }
                if (Owner.IsAdmin)
                {
                    EditButton.Visibility = Visibility.Visible;
                    DeleteButton.Visibility = Visibility.Visible;
                }
            }

        }

        private void MainBorder_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DetailedProductWindow d = new DetailedProductWindow(Product, this)
            {
                Owner = Owner
            };
            d.ShowDialog();
        }
        public void AddToShoppingCartButton_Click(object sender, RoutedEventArgs e)
        {
            Owner.ShoppingCartList.Add(Product);
            IsButtonEnabled = false;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow ew = new EditWindow(Product);
            ew.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить данный товар?", "Требуется подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                var deleted_product = (from p in ApplianceStoreEntities.Context.Products where Product.ProductID == p.ProductID select p).First();
                try
                {
                    var deleted_purchases = (from p in ApplianceStoreEntities.Context.PurchaseItems where p.ProductID == deleted_product.ProductID select p).ToList();
                    if (MessageBox.Show("Вместе с удалением данного товара будут удалено записей о заказах: " + deleted_purchases.Count + ". Подтверждаете удаление?", "Требуется подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                    {
                        foreach (var p in deleted_purchases)
                        {
                            ApplianceStoreEntities.Context.PurchaseItems.Remove(p);
                            
                        }
                        ApplianceStoreEntities.Context.Products.Remove(deleted_product);
                        ApplianceStoreEntities.Context.SaveChanges();
                    }
                    else return;
                }
                catch
                {
                    ApplianceStoreEntities.Context.Products.Remove(deleted_product);
                    ApplianceStoreEntities.Context.SaveChanges();
                }
                Owner.ChangeCategory(Owner.SelectedCategory.CategoryID);
            }
        }
    }
}
