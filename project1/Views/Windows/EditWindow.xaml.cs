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
        ApplianceStoreEntities db;
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
        public EditWindow(Products product, ApplianceStoreEntities DB)
        {
            db = DB;
            DataContext = this;
            _Product = product;
            InitializeComponent();
            Refresh();
        }
        private void Refresh()
        {
            CategoriesCB.ItemsSource = db.Categories.ToArray();
            ManufacturersCB.ItemsSource = db.Manufacturers.ToArray();
            ColorsCB.ItemsSource = db.Colors.ToArray();
            if (Product.Category != 0)
            {
                CategoriesCB.SelectedIndex = Product.Category - 1;
                ManufacturersCB.SelectedIndex = (int)Product.Manufacturer - 1;
                ColorsCB.SelectedIndex = (int)Product.Color - 1;
                ImageTB.Text = Product.Image.Substring(22);
            }
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файлы изображений|*.bmp;*.png;*.jpg";
            if (openDialog.ShowDialog() != true)
                return;
            int found = 0;
            found = openDialog.FileName.Replace('\\', '/').IndexOf("/Data/Images/");
            ImageTB.Text = openDialog.FileName.Replace('\\', '/').Substring(found);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CategoriesCB.SelectedIndex != -1 && ManufacturersCB.SelectedIndex != -1 && ModelTB.Text != string.Empty)
                {
                    Product.Category = ((Categories)CategoriesCB.SelectedItem).CategoryID;
                    Product.Manufacturer = ((Manufacturers)ManufacturersCB.SelectedItem).ManufacturerID;
                    Product.Model = ModelTB.Text;
                    Product.Price = decimal.Parse(PriceTB.Text.Replace('.',','));
                    Product.Width = decimal.Parse(WidthTB.Text.Replace('.', ','));
                    Product.Height = decimal.Parse(HeightTB.Text.Replace('.', ','));
                    Product.Depth = decimal.Parse(DepthTB.Text.Replace('.', ','));
                    Product.Warranty = short.Parse(WarrantyTB.Text);
                    Product.Color = ((Models.Colors)ColorsCB.SelectedItem).ColorID;
                    Product.Image = "pack://application:,,," + ImageTB.Text;
                    db.SaveChanges();
                    MessageBox.Show("Успешно!");
                }
                else
                    MessageBox.Show("Заполните обязательные поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
               MessageBox.Show("Введите корректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
