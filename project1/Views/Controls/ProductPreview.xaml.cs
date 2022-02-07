using project1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace project1.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для ProductPreview.xaml
    /// </summary>
    public partial class ProductPreview : UserControl, INotifyPropertyChanged
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

        private string _Title;
        public string Title 
        { 
            get => _Title;
            set => Set(ref _Title, value);
        }

        private string _ImagePath;
        public string ImagePath 
        { 
            get => _ImagePath;
            set => Set(ref _ImagePath, value);
        }

        private decimal? _Price;
        public decimal? Price
        {
            get => _Price;
            set => Set(ref _Price, value);
        }
        public ProductPreview(Products product)
        {
            DataContext = this;
            InitializeComponent();
            if (product != null)
            {
                img.Height = 120;
                Price = product.Price;
                ManufacturerName.Text = product.Manufacturers.CompanyName;
                Warranty.Text = product.Warranty.ToString() + " месяцев";
                ImagePath = @"pack://application:,,,/" + product.Image;
                if (product.Category == 1)
                {
                    Title = product.ScreenSizes.ScreenSizeInInches.ToString() + '"' +
                         " (" + product.ScreenSizes.ScreenSizeInCentimeters.ToString() + " см) " +
                         "Телевизор " + product.Manufacturers.CompanyName + ' ' + product.Model + ' ' +
                         product.Colors.ColorName + " [" + product.ScreenResolutions.ScreenResolutionName + ", " +
                         product.ScreenResolutions.ScreenResolution + ", " + product.BacklightTypes.BacklightTypeName + ", " +
                         product.OperatingSystems.OperatingSystemName + ']';
                    Property1.Text = "Разрешение экрана";
                    Property2.Text = "Поддержка SmartTV";
                    Property3.Text = "Поддержка HDR";
                    Value1.Text = product.ScreenResolutions.ScreenResolutionName + " (" + product.ScreenResolutions.ScreenResolution + ')';
                    if (product.SmartTVSupport == false || product.SmartTVSupport == null)
                        Value2.Text = "Нет";
                    else
                        Value2.Text = "Есть";
                    if (product.HDRSupport == false || product.HDRSupport == null)
                        Value3.Text = "Нет";
                    else
                        Value3.Text = "Есть";
                }
                if (product.Category == 2)
                {
                    img.Height = 180;
                    Title = "Холодильник " + product.FreezerLocations.FreezerLocationName + ' ' + product.Manufacturers.CompanyName + ' ' +
                        product.Model + ' ' + product.Colors.ColorName + " [" + (product.RefrigeratorVolume + product.FreezerVolume).ToString() + " л, " +
                        product.Width + " см x " + product.Height + " см x " + product.Depth + " см]";
                    Property1.Text = "Компоновка";
                    Property2.Text = "Инверторный компрессор";
                    Property3.Text = "Зона свежести";
                    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                    Value1.Text = ti.ToTitleCase(product.FreezerLocations.FreezerLocationName);
                    if (product.InverterCompressor == false || product.InverterCompressor == null)
                        Value2.Text = "Нет";
                    else
                        Value2.Text = "Есть";
                    if (product.FreshnessZone == false || product.FreshnessZone == null)
                        Value3.Text = "Нет";
                    else
                        Value3.Text = "Есть";
                }
            }

        }

        private void MainBorder_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
