using project1.Models;
using project1.Views.Controls;
using System.Collections.Generic;
using System.Windows;


namespace project1.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ShoppingCart.xaml
    /// </summary>
    public partial class ShoppingCart : Window
    {
        public ShoppingCart(List<Products> ShoppingCartList)
        {
            InitializeComponent();
            foreach (var product in ShoppingCartList)
            {
                ProductsPanel.Children.Add(new ShoppingCartProduct(product));
            }
        }
    }
}
