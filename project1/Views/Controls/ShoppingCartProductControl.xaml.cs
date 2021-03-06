using project1.Models;
using project1.Views.Windows;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace project1.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для ShoppingCartProduct.xaml
    /// </summary>
    public partial class ShoppingCartProductControl : UserControl, INotifyPropertyChanged
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

        private short _ProductCount = 1;
        public short ProductCount { get => _ProductCount; set => Set(ref _ProductCount, value); }

        private string _ProductImage;
        public string ProductImage { get => _ProductImage; set => Set(ref _ProductImage, value); }
        private string _ProductTitle;
        public string ProductTitle { get => _ProductTitle; set => Set(ref _ProductTitle, value); }

        private bool _IsSelected = true;
        public bool IsSelected { get => _IsSelected; set => Set(ref _IsSelected, value); }

        private Products _Product;
        public Products Product { get => _Product; set => Set(ref _Product, value); }

        ShoppingCartWindow Owner;
        public ShoppingCartProductControl(Products product, ShoppingCartWindow owner)
        {
            DataContext = this;
            Owner = owner;
            Product = product;
            ProductImage = Directory.GetCurrentDirectory() + '/' + Product.Image;
            InitializeComponent();
            switch (Product.Category)
            {
                case 1:
                    {
                        ProductTitle = Product.ScreenSizes.ScreenSizeInInches + "\" " + "Телевизор ";
                        break;
                    }
                case 2:
                    {
                        ProductTitle = "Холодильник " + Product.FreezerLocations.FreezerLocationName + ' ';
                        break;
                    }
                case 3:
                    {
                        ProductTitle = "Стиральная машина " + (Product.DirectDrive ?? false ? "с прямым приводом " : "без прямого привода");
                        break;
                    }
                case 4:
                    {
                        ProductTitle = "Микроволновая печь " + Product.InternalVolume + " л. ";
                        break;
                    }
                default:
                    {
                        ProductTitle = "Товар  ";
                        break;
                    }
            }
            ProductTitle += Product.Manufacturers.CompanyName + ' ' + Product.Model + ' ' + Product.Colors.ColorName;
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            ProductCount++;
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductCount != 1)
            {
                ProductCount--;
            }
        }

        private void CountTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ProductCount == 0)
                IsSelected = false;
            else
                IsSelected = true;
            Owner.UpdateInfo();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Owner.Products.Remove(Product);
            Owner.UpdateProducts();
            ((CatalogWindow)Owner.Owner).DisplayProducts();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            Owner.UpdateInfo();
            Owner.CheckCheckBoxes();
        }
    }
}
