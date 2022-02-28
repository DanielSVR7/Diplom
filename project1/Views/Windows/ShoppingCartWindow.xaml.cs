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
using System.Collections.ObjectModel;
using System;

namespace project1.Views.Windows
{
    public partial class ShoppingCartWindow : Window, INotifyPropertyChanged
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
        private decimal DisplayedDiscount = 0;
        public List<ShoppingCartProductControl> ShoppingCartList = new List<ShoppingCartProductControl>();
        public ObservableCollection<Products> Products = new ObservableCollection<Products>();
        ApplianceStoreEntities db = new ApplianceStoreEntities();
        public ShoppingCartWindow(ObservableCollection<Products> products, Clients client)
        {
            InitializeComponent();
            Products = products;
            DisplayedDiscount = client.DiscountLevels.PercentDiscount;
            DiscountTextBlock.Text = DisplayedDiscount.ToString();
            UpdateProducts();
        }
        public void UpdateProducts()
        {
            ProductsPanel.Children.Clear();
            ShoppingCartList.Clear();
            foreach (var product in Products)
            {
                var p = new ShoppingCartProductControl(product, this);
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
                        DisplayedSum += item.Product.Price / 100 * (100 - DisplayedDiscount) * item.ProductCount ?? 0;
                    }
                    NumTextBox.Text = DisplayedNum.ToString() + ' ' + RussianCases.GenerateNumEnding(DisplayedNum, "товар", "товара", "товаров");
                    SumTextBox.Text = String.Format("на {0:#.00} ₽", DisplayedSum);
                }
            }
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
            if (ShoppingCartList.Count != 0)
            {
                if (MessageBox.Show("Вы уверены что хотите распечатать этот заказ?", "Подтвердите действие",
                    MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    try
                    {
                        int _ClientID;
                        if (((CatalogWindow)Owner).IsAdmin)
                            _ClientID = 0;
                        else
                            _ClientID = ((CatalogWindow)Owner).Client.ClientID;
                        var purchase = new Purchases
                        {
                            PurchaseID = (from p in db.Purchases
                                          select p.PurchaseID).ToList().Last() + 1,
                            ClientID = _ClientID
                        };
                        db.Purchases.Add(purchase);
                        db.SaveChanges();
                        decimal sum = 0;
                        foreach (ShoppingCartProductControl p in ShoppingCartList)
                        {
                            if (p.IsSelected)
                            {
                                PurchaseItems item = new PurchaseItems
                                {
                                    PurchaseID = (from pur in db.Purchases
                                                  select pur).ToList().Last().PurchaseID,
                                    ProductID = p.Product.ProductID,
                                    ProductCount = p.ProductCount
                                };
                                db.PurchaseItems.Add(item);
                                sum += item.ProductCount *
                                    (from product in db.Products
                                     where product.ProductID == item.ProductID
                                     select product)
                                     .Single().Price ?? 0;
                            }
                        }
                        var client = (from c in db.Clients
                                      where c.ClientID == _ClientID
                                      select c)
                                      .Single();
                        client.Account += sum;
                        List<DiscountLevels> lvls = (from l in db.DiscountLevels select l).ToList();
                        bool IsUpgraded = false;
                        if (client.DiscountLevel != lvls.Count - 1)
                        {
                            for (int i = 0; i < lvls.Count; i++)
                            {
                                if (client.Account >= lvls[i].AmountOfPurchases)
                                {
                                    IsUpgraded = true;
                                    client.DiscountLevel = lvls[i].LevelID;
                                }
                            }
                            if (IsUpgraded)
                                MessageBox.Show("Для получения товарного чека подойдите на кассу. " +
                                    "\nУровень повышен! Теперь уровень вашего дисконта - " + lvls[client.DiscountLevel].Name +
                                    ".\nВаша скидка составляет " + lvls[client.DiscountLevel - 1].PercentDiscount + "%.",
                                    "Заказ оформлен успешно.", MessageBoxButton.OK, MessageBoxImage.Information);
                            else
                                MessageBox.Show("Для получения товарного чека подойдите на кассу. " +
                                    "\nДо повышения уровня осталось приобрести товаров на " +
                                    (lvls[client.DiscountLevel + 1].AmountOfPurchases - client.Account),
                                    "Заказ оформлен успешно.", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                            MessageBox.Show("Для получения товарного чека подойдите на кассу",
                                "Заказ оформлен успешно.", MessageBoxButton.OK, MessageBoxImage.Information);
                        db.SaveChanges();
                        this.Close();
                        AuthorizationWindow a = new AuthorizationWindow();
                        a.Show();
                        ((CatalogWindow)Owner).Close();
                        PrintToWord();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Ошибка - " + ex.Message);
                    }
                }
                else
                    MessageBox.Show("Заказ отменён");
            }
            else
                MessageBox.Show("Корзина пуста!");
        }
        private void PrintToWord()
        {
            try
            {
                Word.Application app = new Word.Application();
                string source = Environment.CurrentDirectory + @"\CheckoutTemplate.dotx";

                Word.Document doc = app.Documents.Add(source);
                doc.Activate();

                Word.Bookmarks bookmarks = doc.Bookmarks;

                var lastPurchase = (from purchase 
                                    in db.Purchases 
                                    select purchase).ToList().Last();
                decimal sum = 0;
                foreach (var item in lastPurchase.PurchaseItems)
                    sum += Math.Round(item.Products.Price / 100 * 
                        (100 - DisplayedDiscount) * item.ProductCount ?? 0, 2);

                foreach (Word.Bookmark bookmark in bookmarks)
                {
                    switch (bookmark.Name)
                    {
                        case "Адрес": bookmark.Range.Text = "Адрес"; break;
                        case "Дата": bookmark.Range.Text = (from p in db.Purchases select p.PurchaseDate).ToList().Last().ToString(); break;
                        case "КоличествоНаименований": bookmark.Range.Text = lastPurchase.PurchaseItems.Count.ToString(); break;
                        case "НомерЧека": bookmark.Range.Text = lastPurchase.PurchaseID.ToString(); break;
                        case "Продавец": bookmark.Range.Text = "Наименование магазина - продавца"; break;
                        case "Сумма": bookmark.Range.Text = sum.ToString(); break;
                        case "СуммаТекстом": bookmark.Range.Text = RussianCases.RubPhrase(sum); break;
                        case "Телефон": bookmark.Range.Text = "Контактный телефон"; break;
                    }
                }

                Word.Table table = doc.Tables[1];
                int i = 2;
                foreach(var item in lastPurchase.PurchaseItems)
                {
                    table.Cell(i, 1).Range.Text = (i - 1).ToString();
                    table.Cell(i, 2).Range.Text = item.ProductID.ToString();
                    table.Cell(i, 3).Range.Text = new ShoppingCartProductControl((from p in Products 
                                                                           where p.ProductID == item.ProductID 
                                                                           select p).Single(), this).ProductTitle;
                    table.Cell(i, 4).Range.Text = item.Products.Price.ToString();
                    table.Cell(i, 5).Range.Text = item.ProductCount.ToString();
                    table.Cell(i, 6).Range.Text = DisplayedDiscount.ToString() + '%';
                    table.Cell(i, 7).Range.Text = Math.Round((item.Products.Price / 100 * (100 - DisplayedDiscount) * item.ProductCount ?? 0), 2).ToString();
                    table.Rows.Add();
                    i++;
                }
                table.Rows[i].Cells[1].Merge(table.Rows[i].Cells[6]);
                table.Rows[i].Range.Font.Bold = 1;
                table.Cell(i, 2).Range.Text = sum.ToString();
                doc.Close();
                doc = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка - " + ex.Message);
            }
        }
    }
}
