using project1.Models;
using project1.Views.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace project1.Views.Windows
{
    public partial class DetailedProductWindow : Window, INotifyPropertyChanged
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

        private string _ProductTitle;
        public string ProductTitle { get => _ProductTitle; set => Set(ref _ProductTitle, value); }

        private decimal? _Price;
        public decimal? Price { get => _Price; set => Set(ref _Price, value); }

        private string _Description;
        public string Description { get => _Description; set => Set(ref _Description, value); }

        private bool _IsButtonEnabled = true;
        public bool IsButtonEnabled { get => _IsButtonEnabled; set => Set(ref _IsButtonEnabled, value); }
        private Products Product;
        ProductPreviewControl owner;
        public DetailedProductWindow(Products product, ProductPreviewControl pw)
        {
            Product = product;
            this.owner = pw;
            DataContext = this;
            InitializeComponent();
            ImagePath = product.Image;
            ProductTitle = pw.ProductTitle;
            Price = product.Price;
            Description = product.Description;
            IsButtonEnabled = pw.IsButtonEnabled;

            DataGrid dg = new DataGrid();
            dg.Columns.Add(new DataGridTextColumn { Binding = new Binding() { Path = new PropertyPath("P") }, Width = 400 });
            dg.Columns.Add(new DataGridTextColumn { Binding = new Binding() { Path = new PropertyPath("V") } });
            dg.Items.Add(new { P = "Тип", V = product.Categories.CategoryName });
            dg.Items.Add(new { P = "Производитель", V = product.Manufacturers.CompanyName });
            dg.Items.Add(new { P = "Модель", V = product.Model });
            dg.Items.Add(new { P = "Основной цвет", V = product.Colors.ColorName });
            ParametersPanel.Children.Add(dg);

            if (product.Category == 1)
            {
                TextBlock tb = new TextBlock() { Style = (Style)FindResource("ParametersHeader"), Text = "Экран" };
                ParametersPanel.Children.Add(tb);
                dg = new DataGrid();
                dg.Columns.Add(new DataGridTextColumn { Binding = new Binding() { Path = new PropertyPath("P") }, Width = 400 });
                dg.Columns.Add(new DataGridTextColumn { Binding = new Binding() { Path = new PropertyPath("V") } });
                dg.Items.Add(new { P = "Тип подсветки", V = product.BacklightTypes.BacklightTypeName });
                dg.Items.Add(new { P = "Диагональ экрана (дюйм)", V = product.ScreenSizes.ScreenSizeInInches + '"' });
                dg.Items.Add(new { P = "Диагональ экрана (сантиметр)", V = product.ScreenSizes.ScreenSizeInCentimeters + " см" });
                dg.Items.Add(new { P = "Разрешение экрана", V = product.ScreenResolutions.ScreenResolution });
                if (product.HDRSupport == false || product.HDRSupport == null)
                    dg.Items.Add(new { P = "Поддержка HDR", V = "Нет" });
                else
                    dg.Items.Add(new { P = "Поддержка HDR", V = "Есть" });
                ParametersPanel.Children.Add(dg);

                tb = new TextBlock() { Style = (Style)FindResource("ParametersHeader"), Text = "Смарт-функции" };
                ParametersPanel.Children.Add(tb);
                dg = new DataGrid();
                dg.Columns.Add(new DataGridTextColumn { Binding = new Binding() { Path = new PropertyPath("P") }, Width = 400 });
                dg.Columns.Add(new DataGridTextColumn { Binding = new Binding() { Path = new PropertyPath("V") } });
                if (product.SmartTVSupport == false || product.SmartTVSupport == null)
                    dg.Items.Add(new { P = "Поддержка SmartTV", V = "Нет" });
                else
                {
                    dg.Items.Add(new { P = "Поддержка SmartTV", V = "Есть" });
                    dg.Items.Add(new { P = "Операционная система", V = product.OperatingSystems.OperatingSystemName });
                }
                ParametersPanel.Children.Add(dg);
            }
        }
        private void AddToShoppingCartButton_Click(object sender, RoutedEventArgs e)
        {
            owner.AddToShoppingCartButton_Click(sender, e);
        }
    }
}
