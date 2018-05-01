using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachineKata.Models
{
    public class Product
    {
        //Cost of product in currency (US Dollar)
        private decimal cost;
        //Product name
        private string name;

        public Product(decimal productCost, string productName)
        {
            this.cost = productCost;
            this.name = productName;
        }

        public decimal getCost()
        {
            return this.cost;
        }

        public string getName()
        {
            return this.name;
        }

    }
}
