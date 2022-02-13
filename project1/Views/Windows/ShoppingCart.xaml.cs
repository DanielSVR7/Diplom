using project1.Models;
using project1.Views.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;
using System.Data;

namespace project1.Views.Windows
{
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
        private decimal DisplayedSum = 0;
        private int DisplayedNum = 0;
        private List<ShoppingCartProduct> ShoppingCartList = new List<ShoppingCartProduct>();
        public List<Products> Products = new List<Products>();
        ApplianceStoreEntities db = new ApplianceStoreEntities();
        public ShoppingCart(List<Products> products)
        {
            InitializeComponent();
            Products = products;
            UpdateProducts();
        }
        public void UpdateProducts()
        {
            ProductsPanel.Children.Clear();
            ShoppingCartList.Clear();
            foreach (var product in Products)
            {
                var p = new ShoppingCartProduct(product, this);
                ProductsPanel.Children.Add(p);
                ShoppingCartList.Add(p);
            }
            UpdateInfo();
        }
        public void UpdateInfo()
        {
            DisplayedSum = 0;
            DisplayedNum = 0;
            if (ShoppingCartList.Count != 0)
            {
                foreach (var item in ShoppingCartList)
                {
                    if (item.IsSelected)
                    {
                        DisplayedNum += item.ProductCount;
                        DisplayedSum += item.Product.Price * item.ProductCount ?? 0;
                    }
                    NumTextBox.Text = DisplayedNum.ToString() + ' ' + Generate(DisplayedNum, "товар", "товара", "товаров");
                    SumTextBox.Text = "на " + DisplayedSum + " ₽";
                }
            }
        }
        public static string Generate(int number, string nominativ, string genetiv, string plural)
        {
            var titles = new[] { nominativ, genetiv, plural };
            var cases = new[] { 2, 0, 1, 1, 1, 2 };
            return titles[number % 100 > 4 && number % 100 < 20 ? 2 : cases[(number % 10 < 5) ? number % 10 : 5]];
        }

        private void SelectAllCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == true)
            {
                foreach (var item in ShoppingCartList)
                {
                    item.IsSelected = true;
                }
            }
            else if (((CheckBox)sender).IsChecked == false)
            {
                foreach (var item in ShoppingCartList)
                {
                    item.IsSelected = false;
                }
            }
            else
            {
                foreach (var item in ShoppingCartList)
                {
                    item.IsSelected = true;
                }
            }
            UpdateInfo();
        }
        public void CheckCheckBoxes()
        {
            for (int i = 0; i < ShoppingCartList.Count - 1; i++)
            {
                if (ShoppingCartList[i].IsSelected == ShoppingCartList[i + 1].IsSelected)
                    continue;
                else
                {
                    SelectAllCheckBox.IsChecked = null;
                    return;
                }
            }
            SelectAllCheckBox.IsChecked = ShoppingCartList[0].IsSelected;
        }

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите распечатать этот заказ?", "Подтвердите действие",
                MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                try
                {
                    int _ClientID;
                    if (((Catalog)Owner).IsAdmin)
                        _ClientID = 0;
                    else
                        _ClientID = ((Catalog)Owner).Client.ClientID;
                    var purchase = new Purchases
                    {
                        PurchaseID = (from p in db.Purchases select p.PurchaseID).ToList().Last() + 1,
                        PurchaseDate = System.DateTime.Now,
                        ClientID = _ClientID
                    };
                    db.Purchases.Add(purchase);
                    db.SaveChanges();
                    foreach (ShoppingCartProduct p in ShoppingCartList)
                    {
                        PurchaseItems item = new PurchaseItems
                        {
                            PurchaseID = (from pur in db.Purchases select pur).ToList().Last().PurchaseID,
                            ProductID = p.Product.ProductID,
                            ProductCount = p.ProductCount
                        };
                        db.PurchaseItems.Add(item);

                        db.SaveChanges();
                        MessageBox.Show("Успешно");
                        this.Close();
                        ((Catalog)Owner).ShoppingCartList.Clear();
                    }
                }
                catch
                {
                    MessageBox.Show("Fucked up");
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
