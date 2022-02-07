using project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Логика взаимодействия для DetailedProductView.xaml
    /// </summary>
    public partial class DetailedProductView : Window
    {
        public DetailedProductView(Products product)
        {
            InitializeComponent();
        }
    }
}
