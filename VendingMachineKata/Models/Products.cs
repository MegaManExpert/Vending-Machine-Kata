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

        public List<string> getProducts()
        {
            List<string> productList = new List<string>();
            foreach (KeyValuePair<string, Product> product in vendingProducts)
            {
                productList.Add(product.Key + " - " + product.Value.getName() + ": $" + product.Value.getCost());
            }
            return productList;
        }

        public List<Product> getProduct()
        {
            return getProduct("All");
        }

        public List<Product> getProduct(string code)
        {
            List<Product> listProducts = new List<Product>();
            if (code.Equals("All"))
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
    }
}
