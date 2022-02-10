using project1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace project1.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для ShoppingCartProduct.xaml
    /// </summary>
    public partial class ShoppingCartProduct : UserControl, INotifyPropertyChanged
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
        private string _ImagePath;
        public string ImagePath { get => _ImagePath; set => Set(ref _ImagePath, value); }

        private decimal? _Price;
        public decimal? Price { get => _Price; set => Set(ref _Price, value); }

        private string _ProductTitle;
        public string ProductTitle { get => _ProductTitle; set => Set(ref _ProductTitle, value); }
        public ShoppingCartProduct(Products product)
        {
            DataContext = this;
            InitializeComponent();
            ImagePath = @"pack://application:,,,/" + product.Image;
            Price = product.Price;
            if (product.Category == 1)
            {
                ProductTitle = product.ScreenSizes.ScreenSizeInInches + "\" " + "Телевизор ";
            }
            else if (product.Category == 2)
            {
                ProductTitle = product.FreezerLocations.FreezerLocationName + "Холодильник ";
            }
            else { ProductTitle = "fucked up"; }
            ProductTitle += product.Manufacturers.CompanyName + ' ' + product.Model + ' ' + product.Colors.ColorName;
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            CountTextBox.Text = (int.Parse(CountTextBox.Text) + 1).ToString();
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (CountTextBox.Text != "1")
            {
                CountTextBox.Text = (int.Parse(CountTextBox.Text) - 1).ToString();
            }
        }
    }
}
