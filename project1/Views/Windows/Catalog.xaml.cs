using project1.Models;
using project1.Views.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace project1.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window, INotifyPropertyChanged
    {
        bool IsManufacturersShowed = false;
        ApplianceStoreEntities db = new ApplianceStoreEntities();
        Categories _SelectedCategory;
        List<Products> DisplayedProducts;
        List<Manufacturers> SelectedManufacturers = new List<Manufacturers>();

        private string _ShoppingCartButtonContent = "Корзина";
        public string ShoppingCartButtonContent 
        { 
            get => _ShoppingCartButtonContent; 
            set => Set(ref _ShoppingCartButtonContent, value);
        }
        public Categories SelectedCategory
        {
            get => _SelectedCategory;
            set => Set(ref _SelectedCategory, value);
        }

        private List<Products> ShoppingCartList = new List<Products>();
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

        public Catalog()
        {
            InitializeComponent();
            Refresh();
            DataContext = this;
        }

        public void Refresh()
        {
            foreach (var c in db.Categories)
            {
                Button btn = new Button { Margin = new Thickness(5, 5, 5, 0), Content = c, Tag = c.CategoryID };
                btn.Click += new RoutedEventHandler(CtgrsClick);
                CategoriesPanel.Children.Add(btn);
            }
            foreach (var m in db.Manufacturers)
            {
                var cb = new CheckBox();
                cb.Content = m;
                cb.Checked += new RoutedEventHandler(ManufacturersCheckBox_Checked);
                cb.Unchecked += new RoutedEventHandler(ManufacturersCheckBox_Unchecked);
                ManufacturersPanel.Children.Add(cb);
            }
        }

        public void ManufacturersCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MainBox.Children.Clear();
            SelectedManufacturers.Add((Manufacturers)((CheckBox)sender).Content);
            if (DisplayedProducts != null)
            {
                List<Products> RemovingList = new List<Products>();
                foreach (var p in DisplayedProducts)
                {
                    if (SelectedManufacturers.Contains(p.Manufacturers))
                    {
                        AddProductToDisplay(p);
                    }
                    else
                        RemovingList.Add(p);
                }
                foreach(var p in RemovingList)
                    DisplayedProducts.Remove(p);
            }
        }
        public void ManufacturersCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SelectedManufacturers.Remove((Manufacturers)((CheckBox)sender).Content);
            DisplayedProducts.Remove((from d in DisplayedProducts where d.Manufacturers == (Manufacturers)((CheckBox)sender).Content select d).Single());
            if (DisplayedProducts != null)
            {
                MainBox.Children.Clear();
                foreach (var p in DisplayedProducts)
                {
                    if (SelectedManufacturers.Contains(p.Manufacturers))
                        AddProductToDisplay(p);
                }
            }
            if (SelectedManufacturers.Count == 0)
            { 
                DisplayedProducts = (from p in db.Products where p.Category == SelectedCategory.CategoryID select p).ToList();
                DisplayProducts(DisplayedProducts);
            }
        }
        public void CtgrsClick(object sender, RoutedEventArgs e)
        {
            int tag = int.Parse(((Button)sender).Tag.ToString());
            SelectedCategory = (from c in db.Categories where c.CategoryID == tag select c).Single();
            DisplayedProducts = (from p in db.Products where p.Category == SelectedCategory.CategoryID select p).ToList();
            DisplayProducts(DisplayedProducts);
        }
        public void DisplayProducts(List<Products> products)
        {
            if (products != null)
            {
                MainBox.Children.Clear();
                foreach (var p in products)
                {
                    ProductPreview pw = new ProductPreview(p, this);
                    MainBox.Children.Add(pw);
                }
            }
        }
        public void AddProductToDisplay(Products product)
        {
            if (product != null)
            {
                ProductPreview pw = new ProductPreview(product, this);
                MainBox.Children.Add(pw);
            }
        }
        private void ManufacturersShow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsManufacturersShowed)
            {
                ManufacturersPanel.MaxHeight = 40;
                ManufacturersShow.Text = "Показать ещё..";
                IsManufacturersShowed = false;
            }
            else
            {
                ManufacturersPanel.MaxHeight = 1000;
                ManufacturersShow.Text = "Свернуть..";
                IsManufacturersShowed = true;
            }
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox cb in ManufacturersPanel.Children)
            {
                if (cb.IsChecked == true)
                {

                }
            }
        }

        private void ShoppingCartButton_Click(object sender, RoutedEventArgs e)
        {
            ShoppingCart s = new ShoppingCart(ShoppingCartList);
            s.Owner = this;
            s.Show();
        }
        public bool AddToShoppingCart(Products product)
        {
            if (ShoppingCartList.Contains(product))
                return true;
            else
            {
                ShoppingCartButtonContent = "Корзина (" + (ShoppingCartList.Count + 1) + ')';
                ShoppingCartList.Add(product);
                return true;
            }
        }
    }
}
