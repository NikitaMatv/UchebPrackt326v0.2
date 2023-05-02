using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using UchebPrackt326.Compnent;

namespace UchebPrackt326.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPages.xaml
    /// </summary>
    public partial class MainPages : Page
    {
        int OldAdmin = App.Admin;
        IEnumerable<Service> filterProduct = App.db.Service.Where(x => x.IsDell != 1).ToList();
        public MainPages()
        {
            InitializeComponent();
            if(App.Admin == 1)
            {
                BtAddServ.Visibility = Visibility.Visible;
                BtServList.Visibility = Visibility.Visible;
            }
            else
            {
                BtAddServ.Visibility = Visibility.Collapsed;
                BtServList.Visibility = Visibility.Collapsed;
            }
       
            CbDiscount.SelectedIndex = 0;
            CbSort.SelectedIndex = 0;
            Update();
            TbPages.Text = $" {App.db.Service.Where(x => x.IsDell != 1).Count()} из {App.db.Service.Count()} ";
        }

       

        private void EditBt_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Button).DataContext as Service;
             NavigationService.Navigate(new EditServPages(select));
        }

        private void DellBt_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Button).DataContext as Service;
            select.IsDell = 1;
            App.db.SaveChanges();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
        public void Update()
        {
            
                
                if (CbSort.SelectedIndex == 1)
                    filterProduct = filterProduct.OrderBy(x => x.CostDisc);
                else if (CbDiscount.SelectedIndex == 2)
                    filterProduct = filterProduct.OrderByDescending(x => x.CostDisc);
                if (CbDiscount.SelectedIndex > 0)
                {
                   
                    if (CbDiscount.SelectedIndex == 1)
                        filterProduct = filterProduct.Where(x => x.Discount >= 0 && x.Discount < 0.05 || x.Discount == null).ToList();
                    else if (CbDiscount.SelectedIndex == 2)
                        filterProduct = filterProduct.Where(x => x.Discount >= 0.05 && x.Discount < 0.15).ToList();
                    else if (CbDiscount.SelectedIndex == 3)
                        filterProduct = filterProduct.Where(x => x.Discount >= 0.15 && x.Discount < 0.30).ToList();
                    else if (CbDiscount.SelectedIndex == 4)
                        filterProduct = filterProduct.Where(x => x.Discount >= 0.30 && x.Discount < 0.70).ToList();
                    else if (CbDiscount.SelectedIndex == 5)
                        filterProduct = filterProduct.Where(x => x.Discount >= 0.70 && x.Discount < 1).ToList();

                }
                if (TbSelect.Text.Length > 0)
                {
                    filterProduct = filterProduct.Where(x => x.Title.ToLower().StartsWith(TbSelect.Text.ToLower()) || x.Description.ToLower().StartsWith(TbSelect.Text.ToLower())); 
                }
                LvSecv.ItemsSource = filterProduct.ToList();

          

        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void CbDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void BtAddServ_Click(object sender, RoutedEventArgs e)
        {
         
            NavigationService.Navigate(new EditServPages(new Service()));
        }

        private void BrAddClintInServ_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Button).DataContext as Service;
            NavigationService.Navigate(new AddClientInServPages(select));
        }

        private void BtServList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientServListPages());
        }
    }
}
