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
using COMP123_Final.Services;
using COMP123_Final.Models;

namespace COMP123_Final // by Jeffrey Sy - 980045498
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();            
            btnAddCart.Click += this.OnAddCart;
            btnUpdateOrder.Click += this.OnUpdate;
            btnDeleteOrder.Click += this.OnDelete;
            btnLoadOrder.Click += this.OnLoadOrder;
            
            //Establish grid rules
            gridShopCart.AutoGenerateColumns = true;
            gridShopCart.IsReadOnly = false;
            gridShopCart.RowHeight = 20;
            gridShopCart.ColumnWidth = 85.5;
            gridShopCart.ItemsSource = LoadCartList();
        } 
        
        
        private void OnLoadOrder(object sender, RoutedEventArgs e)
        {
            gridShopCart.ItemsSource = LoadCartList();
        }

        public List<ShoppingCart> LoadCartList()
        {
            var cartService = new CartService();
            var carts = cartService.Load();

            return carts;
        }
            
        private void OnAddCart(object sender, RoutedEventArgs e)
        {
            //Adds order to orders of the day, and creates a file for data persistence
            gridShopCart.ItemsSource = null;
            string foodQ = boxFoodQty.Text;
            string toyQ = boxToyQty.Text;
            if (string.IsNullOrWhiteSpace(txtCustName.Text))
            {
                MessageBox.Show("Please enter at least a customer name. Email is not mandatory.");                
            }
            else
            {
                if (boxFoodQty.SelectedItem != null || boxToyQty.SelectedItem != null)
                {
                    var currentOrder = new ShoppingCart()
                    {
                        FoodQuantity = Convert.ToInt32(foodQ),
                        ToyQuantity = Convert.ToInt32(toyQ),
                        Price = (Convert.ToInt32(foodQ) * 21.32) + (Convert.ToInt32(toyQ) * 6.56),
                        FullName = txtCustName.Text,
                        Email = txtCustEmail.Text
                    };

                    //Save to drive
                    var cartService = new CartService();
                    cartService.Save(currentOrder);

                    List<ShoppingCart> list = new List<ShoppingCart>();
                    list.Add(currentOrder);
                    gridShopCart.ItemsSource = LoadCartList();

                    //Reset fields
                    boxFoodQty.Text = null;
                    boxToyQty.Text = null;
                    txtCustEmail.Text = String.Empty;
                    txtCustName.Text = String.Empty;
                    MessageBox.Show("Order added to database.");
                }
                else
                {
                    gridShopCart.ItemsSource = LoadCartList();
                    MessageBox.Show("Please indicate the quantity of each item. Select '0' if necessary");
                }
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            //Deletes the selected order
            ShoppingCart selectedCart = gridShopCart.SelectedItem as ShoppingCart;
            if (selectedCart != null)
            {
                IList<ShoppingCart> carts = gridShopCart.ItemsSource as IList<ShoppingCart>;
                if (carts != null)
                    carts.Remove(selectedCart);
                gridShopCart.ItemsSource = null;
                gridShopCart.ItemsSource = carts;

                //Deletes file
                CartService cart = new CartService();
                cart.Delete(selectedCart);
            }
        }        
       
        private void OnUpdate(object sender, RoutedEventArgs e) //Sorry for the code smell, ran out of time. 
        {            
            string foodQ = boxFoodQty.Text;
            string toyQ = boxToyQty.Text;
            if (string.IsNullOrWhiteSpace(txtCustName.Text))
            {
                MessageBox.Show("Please enter at least a customer name. Email is not mandatory.");
            }
            else
            {
                ShoppingCart selectedCart = gridShopCart.SelectedItem as ShoppingCart;
                if (selectedCart != null)
                {
                    IList<ShoppingCart> carts = gridShopCart.ItemsSource as IList<ShoppingCart>;
                    if (carts != null)
                        carts.Remove(selectedCart);
                    gridShopCart.ItemsSource = null;
                    gridShopCart.ItemsSource = carts;
                                        
                    if (boxFoodQty.SelectedItem != null || boxToyQty.SelectedItem != null)
                    {
                        var currentOrder = new ShoppingCart()
                        {
                            FoodQuantity = Convert.ToInt32(foodQ),
                            ToyQuantity = Convert.ToInt32(toyQ),
                            Price = (Convert.ToInt32(foodQ) * 21.32) + (Convert.ToInt32(toyQ) * 6.56),
                            FullName = txtCustName.Text,
                            Email = txtCustEmail.Text
                        };

                        //Save to drive
                        var cartService = new CartService();
                        cartService.Save(currentOrder);

                        List<ShoppingCart> list = new List<ShoppingCart>();
                        list.Add(currentOrder);
                        gridShopCart.ItemsSource = LoadCartList();

                        //Reset fields
                        boxFoodQty.Text = null;
                        boxToyQty.Text = null;
                        txtCustEmail.Text = String.Empty;
                        txtCustName.Text = String.Empty;
                        MessageBox.Show("Order has been updated.");

                        //Deletes old file
                        CartService cart = new CartService();
                        cart.Delete(selectedCart);
                    }
                    else
                    {
                        gridShopCart.ItemsSource = LoadCartList();
                        MessageBox.Show("Please indicate the quantity of each item. Select '0' if necessary");
                    }
                }                             
            }
        }
    }
}
