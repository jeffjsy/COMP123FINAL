using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP123_Final;

namespace COMP123_Final.Models
{
    public class ShoppingCart : Customer
    {
        public string CustomerID { get; set; }
        public int CartID { get; set; }      
        public int ToyQuantity { get; set; }
        public int FoodQuantity { get; set; }        
        public double Price { get; set; }
        public DateTime DateOrdered
        {
            get
            {
                var now = DateTime.Now;
                return now;
            }
        }
        public DateTime ShippingDate
        {
            get
            {
                return this.DateOrdered;
            }
            set
            {
                this.DateOrdered.AddDays(2);
            }
        }
        public ShoppingCart()
        {            
            this.CustomerID = Guid.NewGuid().ToString();
            this.CartID = new Random().Next(1000, 9999);
        }

        public ShoppingCart(int toyQuantity, int foodQuantity, double price)
        {             
            this.FoodQuantity = foodQuantity;
            this.ToyQuantity = toyQuantity;            
            this.Price = price;
        }

    }
}
