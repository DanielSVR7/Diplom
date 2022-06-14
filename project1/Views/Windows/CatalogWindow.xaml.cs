using project1.Models;
using project1.Views.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace project1.Views.Windows
{
    public partial class CatalogWindow : Window, INotifyPropertyChanged
    {
        public ApplianceStoreEntities db = new ApplianceStoreEntities();
        
        List<Products> DisplayedProducts = new List<Products>();
        private Clients _Client;
        public Clients Client { get => _Client; set => Set(ref _Client, value); }
        public bool IsAdmin = false;

        Categories _SelectedCategory;
        public Categories SelectedCategory { get => _SelectedCategory; set => Set(ref _SelectedCategory, value); }

        private ObservableCollection<Products> _ShoppingCartList = new ObservableCollection<Products>();
        public ObservableCollection<Products> ShoppingCartList { get => _ShoppingCartList; set => Set(ref _ShoppingCartList, value); }
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

        public CatalogWindow()
        {
            DataContext = this;
            InitializeComponent();
            ChangeCategory(1);
            Refresh();
        }

        public void Refresh()
        {
            foreach (var c in db.Categories)
            {
                Button btn = new Button { Margin = new Thickness(5, 5, 5, 0), Content = c, Tag = c.CategoryID };
                btn.Click += new RoutedEventHandler(CtgrsClick);
                CategoriesPanel.Children.Add(btn);
            }
        }
        public void CtgrsClick(object sender, RoutedEventArgs e)
        {
            ChangeCategory(int.Parse(((Button)sender).Tag.ToString()));
        }
        public void ChangeCategory(int categoryID)
        {
            DisplayedProducts.Clear();
            SelectedCategory = (from c in db.Categories
                                where c.CategoryID == categoryID
                                select c).Single();
            DisplayedProducts = (from p in db.Products
                                 where p.Category == SelectedCategory.CategoryID
                                 select p).ToList();

            DisplayProducts();
        }
        public void DisplayProducts()
        {
            if (DisplayedProducts != null)
            {
                MainBox.Children.Clear();
                foreach (var p in DisplayedProducts)
                {
                    ProductPreviewControl pw = new ProductPreviewControl(p, this);
                    if (ShoppingCartList.Contains(p))
                        pw.IsButtonEnabled = false;
                    MainBox.Children.Add(pw);
                }
            }
        }
        public void FilterProducts()
        {
            string text = SearchTB.Text;
            if (SearchTB.Text == string.Empty && PriceFromTextBox.Text == string.Empty && PriceToTextBox.Text == string.Empty)
            {
                ChangeCategory(SelectedCategory.CategoryID);
                return;
            }
            else
            {
                ChangeCategory(SelectedCategory.CategoryID);
                if (text != string.Empty)
                {
                    DisplayedProducts = DisplayedProducts.Where(
                        p => p.Model.ToLower().StartsWith(text.ToLower()) ||
                        (p.Manufacturers?.CompanyName.ToLower().StartsWith(text.ToLower()) ?? false) ||
                        (p.BacklightTypes?.BacklightTypeName.ToLower().Contains(text.ToLower()) ?? false) ||
                        p.Categories.CategoryName.ToLower().StartsWith(text.ToLower()) ||
                        (p.Colors.ColorName.ToLower()?.StartsWith(text.ToLower()) ?? false) ||
                        (p.FreezerLocations?.FreezerLocationName.ToLower().Contains(text.ToLower()) ?? false) ||
                        (p.OperatingSystems?.OperatingSystemName.ToLower().Contains(text.ToLower()) ?? false) ||
                        (p.ScreenResolutions?.ScreenResolutionName.ToLower().Contains(text.ToLower()) ?? false) ||
                        (p.ScreenSizes?.ScreenSizeInCentimeters.ToString().StartsWith(text.ToLower()) ?? false) ||
                        (p.ScreenSizes?.ScreenSizeInInches.ToString().StartsWith(text.ToLower()) ?? false) ||
                        (p.Manufacturers?.Country.ToString().StartsWith(text.ToLower()) ?? false)

                    ).ToList();
                }
                if (PriceFromTextBox.Text != string.Empty)
                {
                    decimal priceFrom = decimal.Parse(PriceFromTextBox.Text);
                    DisplayedProducts = DisplayedProducts.Where(p => p.Price >= priceFrom).ToList(); ;
                }
                if (PriceToTextBox.Text != string.Empty)
                {
                    decimal priceTo = decimal.Parse(PriceToTextBox.Text);
                    DisplayedProducts = DisplayedProducts.Where(p => p.Price <= priceTo).ToList(); ;
                }
                DisplayProducts();
            }
        }
        public void AddProductToDisplay(Products product)
        {
            if (product != null)
            {
                ProductPreviewControl pw = new ProductPreviewControl(product, this);
                MainBox.Children.Add(pw);
            }
        }

        private void ShoppingCartButton_Click(object sender, RoutedEventArgs e)
        {
            ShoppingCartWindow s = new ShoppingCartWindow(ShoppingCartList, Client) { Owner = this };
            s.Show();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow ew = new EditWindow(new Products(), db);
            ew.Show();
        }

        private void DropFilterButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTB.Clear();
            PriceFromTextBox.Clear();
            PriceToTextBox.Clear();
            ChangeCategory(SelectedCategory.CategoryID);
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterProducts();
        }

        private void PriceFromTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterProducts();
        }

        private void PriceToTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterProducts();
        }
    }
}
