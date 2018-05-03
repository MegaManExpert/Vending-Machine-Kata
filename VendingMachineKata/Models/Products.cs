using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachineKata.Models
{
    public class Products
    {
        //Product, amount in vending machine
        private Dictionary<string, Product> vendingProducts = new Dictionary<string, Product> {
            {"A1", new Product(1.00m, "Cola")},
            {"B2", new Product(0.50m, "Chips")},
            {"C3", new Product(0.65m, "Candy")},
            {"D4", new Product(0.75m, "Cookies")}
        };

        private Dictionary<string, int> vendingProductLevel = new Dictionary<string, int> {
            {"A1", 2},
            {"B2", 1},
            {"C3", 5},
            {"D4", 0}
        };

        public List<Product> getProduct()
        {
            return getProduct("");
        }

        public List<Product> getProduct(string code)
        {
            List<Product> listProducts = new List<Product>();
            if (code.Equals(""))
            {
                foreach (Product p in vendingProducts.Values)
                {
                    listProducts.Add(p);
                }
            }
            else if (vendingProducts.ContainsKey(code))
            {
                listProducts.Add(vendingProducts[code]);
            }

            return listProducts;
        }

        public int getProductLevel(string keyCode)
        {
            return vendingProductLevel[keyCode];
        }

        public void setProductLevel(string keyCode, int productLevel)
        {
            vendingProductLevel[keyCode] = productLevel;
        }
    }
}
