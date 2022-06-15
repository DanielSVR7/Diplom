using Microsoft.Win32;
using project1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;

namespace project1.Views.Windows
{
    public partial class EditWindow : Window, INotifyPropertyChanged
    {
        private Products _Product;
        public Products Product { get => _Product; set => Set(ref _Product, value); }
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
        public EditWindow(Products product)
        {
            DataContext = this;
            _Product = product;
            InitializeComponent();
            FillBoxes();
        }
        private void FillBoxes()
        {
            CategoriesComboBox.ItemsSource = ApplianceStoreEntities.Context.Categories.ToList();
            ManufacturersComboBox.ItemsSource = ApplianceStoreEntities.Context.Manufacturers.ToList();
            ColorsComboBox.ItemsSource = ApplianceStoreEntities.Context.Colors.ToList();
            EnergyClassesComboBox.ItemsSource = ApplianceStoreEntities.Context.EnergyClasses.ToList();
            ScreenSizesComboBox.ItemsSource = ApplianceStoreEntities.Context.ScreenSizes.ToList();
            ScreenResolutionsComboBox.ItemsSource = ApplianceStoreEntities.Context.ScreenResolutions.ToList();
            BacklightTypesComboBox.ItemsSource = ApplianceStoreEntities.Context.BacklightTypes.ToList();
            OperatingSystemsComboBox.ItemsSource = ApplianceStoreEntities.Context.OperatingSystems.ToList();
            FreezerLocationsComboBox.ItemsSource = ApplianceStoreEntities.Context.FreezerLocations.ToList();
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файлы изображений|*.bmp;*.png;*.jpg";
            if (openDialog.ShowDialog() != true)
                return;
            int found;
            found = openDialog.FileName.Replace('\\', '/').IndexOf("/Data/Images/");
            ImageTB.Text = openDialog.FileName.Replace('\\', '/').Substring(found);
            Product.Image = ImageTB.Text;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();
            if (CategoriesComboBox.SelectedIndex == -1)
                errors.AppendLine("Необходимо выбрать категорию товара");
            if (ManufacturersComboBox.SelectedIndex == -1)
                errors.AppendLine("Необходимо выбрать производителя");
            if (string.IsNullOrEmpty(Product.Model))
                errors.AppendLine("Необходимо указать модель товара");
            if (Product.Price == null)
                errors.AppendLine("Необходимо указать цену товара");
            if (string.IsNullOrEmpty(Product.Description))
                errors.AppendLine("Необходимо заполнить описание товара");
            if (string.IsNullOrEmpty(Product.Warranty.ToString()))
                errors.AppendLine("Необходимо указать срок гарантии");
            switch (CategoriesComboBox.SelectedIndex)
            {
                case -1: break;
                case 0:
                    {
                        if (ScreenSizesComboBox.SelectedIndex == -1)
                            errors.AppendLine("Необходимо выбрать диагональ экрана");
                        if (ScreenResolutionsComboBox.SelectedIndex == -1)
                            errors.AppendLine("Необходимо выбрать разрешение экрана");
                        if (BacklightTypesComboBox.SelectedIndex == -1)
                            errors.AppendLine("Необходимо выбрать тип подсветки");
                        if (OperatingSystemsComboBox.SelectedIndex == -1)
                            errors.AppendLine("Необходимо выбрать операционную систему");
                        if (Product.SmartTVSupport == null)
                            errors.AppendLine("Необходимо указать поддержку SmartTV");
                        if (Product.Bluetooth == null)
                            errors.AppendLine("Необходимо указать проддержку Bluetooth");
                        if (Product.HDRSupport == null)
                            errors.AppendLine("Необходимо указать поддержку HDR");
                    }
                    break;
                 case 1:
                    {
                        if (FreezerLocationsComboBox.SelectedIndex == -1)
                            errors.AppendLine("Необходимо выбрать компоновку");
                        if (string.IsNullOrEmpty(Product.FreezerVolume.ToString()))
                            errors.AppendLine("Необходимо указать объем морозильной камеры");
                        if (string.IsNullOrEmpty(Product.RefrigeratorVolume.ToString()))
                            errors.AppendLine("Необходимо указать объём холодильной камеры");
                        if (Product.FreshnessZone == null)
                            errors.AppendLine("Необходимо указать наличие зоны свежести");
                        if (Product.InverterCompressor == null)
                            errors.AppendLine("Необходимо указать наличие инверторного компрессора");
                        if (Product.TemperatureDisplay == null)
                            errors.AppendLine("Необходимо указать наличие температурного дисплея");
                    }
                    break;
                case 2:
                    {
                        if (string.IsNullOrEmpty(Product.LaundryLoad.ToString()))
                            errors.AppendLine("Необходимо указать максимальную загрузку");
                        if (string.IsNullOrEmpty(Product.NumberOfPrograms.ToString()))
                            errors.AppendLine("Необходимо указать количество программ");
                        if (string.IsNullOrEmpty(Product.MaximumSpinSpeed.ToString()))
                            errors.AppendLine("Необходимо указать максимальную скорость отжима");
                        if (string.IsNullOrEmpty(Product.TemperatureRange))
                            errors.AppendLine("Необходимо указать диапазон температур");
                        if (string.IsNullOrEmpty(Product.WaterConsumption.ToString()))
                            errors.AppendLine("Необходимо указать потребление воды");
                        if (Product.DirectDrive == null)
                            errors.AppendLine("Необходимо указать наличие прямого привода");
                    }
                    break;
                case 3:
                    {
                        if (string.IsNullOrEmpty(Product.InternalVolume.ToString()))
                            errors.AppendLine("Необходимо указать внутренний объём");
                        if (Product.Grill == null)
                            errors.AppendLine("Необходимо указать наличие гриля");
                    }
                    break;
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Обнаружены ошибки", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Product.ProductID == 0)
            {
                Product.ProductID = (from p in ApplianceStoreEntities.Context.Products select p).ToList().Last().ProductID + 1;
                ApplianceStoreEntities.Context.Products.Add(Product);
            }
            if (string.IsNullOrEmpty(Product.Image))
                Product.Image = @"Data\Images\default.png";
            try
            {
                ApplianceStoreEntities.Context.SaveChanges();
                MessageBox.Show("Данные о товаре успешно сохранены.", "Успешное сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)    
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CategoriesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ForTVs.Visibility = Visibility.Collapsed;
            ForRefregerators.Visibility = Visibility.Collapsed;
            ForWashingMachines.Visibility = Visibility.Collapsed;
            ForMicrowawes.Visibility = Visibility.Collapsed;
            switch (CategoriesComboBox.SelectedIndex)
            {
                case 0: ForTVs.Visibility = Visibility.Visible; break;
                case 1: ForRefregerators.Visibility = Visibility.Visible; break;
                case 2: ForWashingMachines.Visibility = Visibility.Visible; break;
                case 3: ForMicrowawes.Visibility = Visibility.Visible; break;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}