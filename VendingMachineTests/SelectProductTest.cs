using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineKata.Controllers;
using VendingMachineKata.Models;
using VendingMachineKata.Views;

namespace VendingMachineTests
{
    [TestClass]
    public class SelectProductTest
    {
        Product item1 = new Product(1.00m, "Cola");
        Product item2 = new Product(0.50m, "Chips");
        Product item3 = new Product(0.65m, "Candy");
        Coin insertedQuarter = new Coin(0.1823, 0.955, "Quarter");

        VendingMachineControls machineContorls = new VendingMachineControls();
        Coins coinModel = new Coins();
        Products productModel = new Products();

        [TestMethod]
        public void displayAllProducts()
        {
            List<Product> products = productModel.getProduct();

            string productSelection = machineContorls.getProductInfo(products[0]);
            Assert.AreEqual("Cola: $1.00", productSelection);
            productSelection = machineContorls.getProductInfo(products[1]);
            Assert.AreEqual("Chips: $0.50", productSelection);
            productSelection = machineContorls.getProductInfo(products[2]);
            Assert.AreEqual("Candy: $0.65", productSelection);
        }

        [TestMethod]
        public void selectProduct()
        {            
            Assert.AreEqual("PRICE: $1.00", machineContorls.pressButtonCode("A1"));
            Assert.AreEqual("INVALID/SELECT", machineContorls.pressButtonCode("C4"));
        }

        [TestMethod]
        public void enoughMoney()
        {
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual("THANK YOU", machineContorls.pressButtonCode("A1"));
            Assert.AreEqual<decimal>(0.00m, machineContorls.getCurrentHeldValue());
            Assert.AreEqual("INSERT COIN", machineContorls.coinDisplay.getCurrentDisplay());
        }

        [TestMethod]
        public void notEnoughMoney()
        {
            Assert.AreEqual("PRICE: $1.00", machineContorls.pressButtonCode("A1"));
            Assert.AreEqual("INSERT COIN", machineContorls.coinDisplay.getCurrentDisplay());

            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual("0.25", machineContorls.coinDisplay.getCurrentDisplay());

            Assert.AreEqual("PRICE: $1.00", machineContorls.pressButtonCode("A1"));
            Assert.AreEqual("0.25", machineContorls.coinDisplay.getCurrentDisplay());
        }

        [TestMethod]
        public void moreThenEnoughMoney()
        {
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual("THANK YOU", machineContorls.pressButtonCode("A1"));
            Assert.AreEqual<decimal>(0.25m, machineContorls.getCurrentHeldValue());
            Assert.AreEqual("0.25", machineContorls.coinDisplay.getCurrentDisplay());
        }
    }
}
