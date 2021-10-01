using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP123_Final.Models;
using COMP123_Final.Services;
using System.Xml.Serialization;
using System.IO;

namespace COMP123_Final.Services
{
    class CartService : IService<ShoppingCart>
    {
        private string directory = @"C:\_test\Finalobj\";        

        public List<ShoppingCart> Load()
        {
            List<ShoppingCart> carts = null;
            var files = Directory.GetFiles(this.directory);
            if (files != null && files.Length > 0)
            {
                carts = new List<ShoppingCart>();
                foreach (string file in files)
                {
                    ShoppingCart cart = null;
                    var serializer = new XmlSerializer(typeof(ShoppingCart));
                    using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        cart = (ShoppingCart)serializer.Deserialize(stream);
                    }
                    carts.Add(cart);
                }
            }
            return carts;
        }

        public void Save(ShoppingCart obj)
        {
            var serializer = new XmlSerializer(typeof(ShoppingCart));
            using (var stream = new FileStream(this.directory + obj.CustomerID + ".xml", FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(stream, obj);
            }
        }

        public void Update(ShoppingCart obj)
        {
            string filename = this.directory + obj.CustomerID + ".xml";
            if (File.Exists(filename))
                File.Delete(filename);
            this.Save(obj);
        }

        public void Delete(ShoppingCart obj)
        {
            string filename = this.directory + obj.CustomerID + ".xml";
            if (File.Exists(filename))
                File.Delete(filename);            
        }
    }
}
