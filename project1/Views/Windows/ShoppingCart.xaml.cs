using project1.Models;
using project1.Views.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;

namespace project1.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ShoppingCart.xaml
    /// </summary>
    public partial class ShoppingCart : Window, INotifyPropertyChanged
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
        ApplianceStoreEntities db = new ApplianceStoreEntities();
        private decimal DisplayedSum = 0;
        private int DisplayedNum = 0;
        private List<ShoppingCartProduct> ShoppingCartList = new List<ShoppingCartProduct>();
        private List<Products> Products = new List<Products>();
        public ShoppingCart(List<Products> products)
        {
            InitializeComponent();
            Products = products;
            foreach (var product in Products)
            {
                var p = new ShoppingCartProduct(product, this);
                ProductsPanel.Children.Add(p);
                ShoppingCartList.Add(p);
            }
            foreach (var item in ShoppingCartList)
            {
                if (item.IsSelected)
                {
                    DisplayedSum += item.Price ?? 0;
                }
                NumTextBox.Text = DisplayedNum.ToString() + ' ' + Generate(DisplayedNum, "товар", "товара", "товаров");
                SumTextBox.Text = "на " + DisplayedSum + " ₽";
            }
            SelectAllCheckBox.IsChecked = true;
        }
        public void AddProduct(decimal? sum, int count)
        {
            DisplayedSum += sum ?? 0;
            DisplayedNum += count;
            UpdateInfo();
        }
        public void RemoveProduct(decimal? sum, int count)
        {
            DisplayedSum -= sum ?? 0;
            DisplayedNum -= count;
            UpdateInfo();
        }
        private void UpdateInfo()
        {
            NumTextBox.Text = DisplayedNum.ToString() + ' ' + Generate(DisplayedNum, "товар", "товара", "товаров");
            SumTextBox.Text = "на " + DisplayedSum + " ₽";
        }
        public static string Generate(int number, string nominativ, string genetiv, string plural)
        {
            var titles = new[] { nominativ, genetiv, plural };
            var cases = new[] { 2, 0, 1, 1, 1, 2 };
            return titles[number % 100 > 4 && number % 100 < 20 ? 2 : cases[(number % 10 < 5) ? number % 10 : 5]];
        }

        private void SelectAllCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in ProductsPanel.Children)
            {
                ((ShoppingCartProduct)item).IsSelected = false;
            }
        }

        private void SelectAllCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in ProductsPanel.Children)
            {
                ((ShoppingCartProduct)item).IsSelected = true;
            }
        }

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите распечатать этот заказ?", "Подтвердите действие", 
                MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                Purchases purchase = new Purchases { };
                foreach(ShoppingCartProduct p in ShoppingCartList)
                {
                    PurchaseItems item = new PurchaseItems
                    {
                        ProductID = p.ProductID,
                        ProductCount = p.ProductCount
                    };
                    db.PurchaseItems.Add(item);
                }
                //PrintToWord();
            }

        }
        private void PrintToWord()
        {
            Word.Application app = new Word.Application();
            string Source = @"..\Data\WordTemplates\CheckoutTemplate.dotx";
            Word.Document doc = app.Documents.Add(Source);
            Word.Bookmarks bookmarks = doc.Bookmarks;
            foreach (Word.Bookmark bookmark in bookmarks)
            {
                switch (bookmark.Name)
                {
                    
                }
            }
        }
    }
}
