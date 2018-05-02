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
    public class MakeChangeTest
    {
        Product item1 = new Product(1.00m, "Cola");
        Product item2 = new Product(0.50m, "Chips");
        Product item3 = new Product(0.65m, "Candy");
        Coin insertedQuarter = new Coin(0.1823, 0.955, "Quarter");
        Coin insertedNickle = new Coin(0.1607, 0.835, "Nickle");

        VendingMachineControls machineContorls = new VendingMachineControls();
        Coins coinModel = new Coins();
        Products productModel = new Products();
        VendingMachineDisplay productDisplay = new VendingMachineDisplay();

        [TestMethod]
        public void makeChange()
        {
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual("THANK YOU", machineContorls.pressButtonCode("C3"));
            Assert.AreEqual<decimal>(0.10m, machineContorls.getCurrentHeldValue());
            Assert.AreEqual("0.10", machineContorls.coinDisplay.getCurrentDisplay());
            Assert.AreEqual("1:Dime", machineContorls.coinsInReturn().TrimEnd());
        }

        [TestMethod]
        public void makeExcessiveChange()
        {
            machineContorls.insertCoin(insertedNickle);
            machineContorls.insertCoin(insertedNickle);
            machineContorls.insertCoin(insertedNickle);
            machineContorls.insertCoin(insertedNickle);
            machineContorls.insertCoin(insertedNickle);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual("THANK YOU", machineContorls.pressButtonCode("C3"));
            Assert.AreEqual<decimal>(0.10m, machineContorls.getCurrentHeldValue());
            Assert.AreEqual("0.10", machineContorls.coinDisplay.getCurrentDisplay());
            Assert.AreEqual("1:Dime", machineContorls.coinsInReturn().TrimEnd());
        }

        [TestMethod]
        public void noChange()
        {
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual("THANK YOU", machineContorls.pressButtonCode("B2"));
            Assert.AreEqual<decimal>(0.00m, machineContorls.getCurrentHeldValue());
            Assert.AreEqual("INSERT COIN", machineContorls.coinDisplay.getCurrentDisplay());
            Assert.AreEqual("", machineContorls.coinsInReturn().TrimEnd());
        }
    }
}
