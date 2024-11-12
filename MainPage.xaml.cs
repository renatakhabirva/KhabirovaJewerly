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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KhabirovaJewerly
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        int CountRecords;
        
        public MainPage()
        {
            InitializeComponent();
            var Jewerly = KhabirovaTradeEntities.GetContext().Product.ToList();
            MainListView.ItemsSource = Jewerly;
            CountRecords = Jewerly.Count;
           
            UpdateJewerly();
            
            
        }
        
        
        private void UpdateJewerly()
        {
            var Jewerly = KhabirovaTradeEntities.GetContext().Product.ToList();
            Jewerly = Jewerly.Where(p => p.ProductName.ToLower().Contains(SearchTB.Text.ToLower())).ToList();
            MainListView.ItemsSource = Jewerly;
            if (DiscountSortCB.SelectedIndex == 1)
            {
                Jewerly = Jewerly.Where(p => (p.ProductDiscount >= 0 && p.ProductDiscount <= 9.99)).ToList();
            }
            if (DiscountSortCB.SelectedIndex == 2)
            {
                Jewerly = Jewerly.Where(p => (p.ProductDiscount >= 10 && p.ProductDiscount <= 14.99)).ToList();
            }
            if (DiscountSortCB.SelectedIndex == 3)
            {
                Jewerly = Jewerly.Where(p => (p.ProductDiscount >= 15)).ToList();
            }
            if (SortCB.SelectedIndex == 1)
            {
                Jewerly = Jewerly.OrderByDescending(p => p.ProductCost).ToList();
            }
            if (SortCB.SelectedIndex == 0)
            {
                Jewerly = Jewerly.OrderBy(p => p.ProductCost).ToList();
            }
            MainListView.ItemsSource = Jewerly;
            MainListView.Items.Refresh();
            CountTB.Text = "Выведено " + Jewerly.Count.ToString() + " из " + CountRecords.ToString() + "страниц";

        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateJewerly();
        }

        private void DiscountSortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateJewerly();
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateJewerly();
        }

        private void SearchTB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateJewerly();
        }
    }
}
