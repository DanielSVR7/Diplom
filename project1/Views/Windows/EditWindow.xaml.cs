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
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch
            {
               MessageBox.Show("Введите корректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
