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
    public class SoldOutProductTest
    {
        Coin insertedQuarter = new Coin(0.1823, 0.955, "Quarter");

        VendingMachineControls machineContorls = new VendingMachineControls();
        Coins coinModel = new Coins();
        VendingMachineDisplay coinDisplay = new VendingMachineDisplay();

        [TestMethod]
        public void selectSoildOutItem()
        {
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual("SOLD OUT", machineContorls.pressButtonCode("D4"));
            Assert.AreEqual<decimal>(1.00m, machineContorls.getCurrentHeldValue());
            Assert.AreEqual("1.00", machineContorls.coinDisplay.getCurrentDisplay());
        }

        [TestMethod]
        public void checkIfSoildOutItem()
        {
            Assert.AreEqual("SOLD OUT", machineContorls.pressButtonCode("D4"));
            Assert.AreEqual("INSERT COIN", machineContorls.coinDisplay.getCurrentDisplay());
        }

        [TestMethod]
        public void buyToSoildOutItem()
        {
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual("THANK YOU", machineContorls.pressButtonCode("B2"));
            Assert.AreEqual<decimal>(0.5m, machineContorls.getCurrentHeldValue());
            Assert.AreEqual("0.50", machineContorls.coinDisplay.getCurrentDisplay());
            Assert.AreEqual("SOLD OUT", machineContorls.pressButtonCode("B2"));
            Assert.AreEqual("0.50", machineContorls.coinDisplay.getCurrentDisplay());
        }
    }
}
