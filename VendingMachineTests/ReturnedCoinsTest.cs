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
    public class ReturnedCoinsTest
    {
        Coin insertedQuarter = new Coin(0.1823, 0.955, "Quarter");
        Coin insertedNickle = new Coin(0.1607, 0.835, "Nickle");

        VendingMachineControls machineContorls = new VendingMachineControls();
        Coins coinModel = new Coins();
        Products productModel = new Products();
        VendingMachineDisplay productDisplay = new VendingMachineDisplay();

        [TestMethod]
        public void returnCoinsButtonPressed()
        {
            machineContorls.insertCoin(insertedNickle);
            machineContorls.insertCoin(insertedNickle);
            machineContorls.insertCoin(insertedNickle);
            machineContorls.insertCoin(insertedNickle);
            machineContorls.insertCoin(insertedNickle);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual("0.75", machineContorls.currentCoinTotal());
            Assert.AreEqual("3:Quarter", machineContorls.pressCoinReturn().TrimEnd());
            Assert.AreEqual("INSERT COIN", machineContorls.coinDisplay.getCurrentDisplay());
        }

        [TestMethod]
        public void returnCoinsButtonPressedNoChange()
        {
            Assert.AreEqual("INSERT COIN", machineContorls.currentCoinTotal());
            Assert.AreEqual("", machineContorls.pressCoinReturn().TrimEnd());
            Assert.AreEqual("INSERT COIN", machineContorls.coinDisplay.getCurrentDisplay());
        }
    }
}
