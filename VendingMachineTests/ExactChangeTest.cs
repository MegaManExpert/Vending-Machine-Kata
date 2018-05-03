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
    public class ExactChangeTest
    {
        Product item3 = new Product(0.65m, "Candy");

        Coin insertedNickle = new Coin(0.1607, 0.835, "Nickle");
        Coin insertedDime = new Coin(0.0729, 0.705, "Dime");
        Coin insertedQuarter = new Coin(0.1823, 0.955, "Quarter");

        VendingMachineControls machineContorls = new VendingMachineControls();
        Coins coinModel = new Coins();
        VendingMachineDisplay coinDisplay = new VendingMachineDisplay();

        [TestMethod]
        public void unableToMakeChange()
        {
            List<Coin> tempCoinRemoval = new List<Coin> { new Coin(0.1607, 0.835, "Nickle"), new Coin(0.1607, 0.835, "Nickle"), new Coin(0.0729, 0.705, "Dime"), new Coin(0.0729, 0.705, "Dime") };
            machineContorls.emptyCoinInvintory(tempCoinRemoval);
            machineContorls.checkForExactChange();
            Assert.AreEqual("EXACT CHANGE ONLY", machineContorls.coinDisplay.getCurrentDisplay());
        }

        [TestMethod]
        public void makeUnableToMakeChangeWithBuying()
        {
            List<Coin> tempCoinRemoval = new List<Coin> { new Coin(0.1607, 0.835, "Nickle"), new Coin(0.0729, 0.705, "Dime") };
            machineContorls.emptyCoinInvintory(tempCoinRemoval);
            machineContorls.checkForExactChange();
            Assert.AreEqual("INSERT COIN", machineContorls.coinDisplay.getCurrentDisplay());
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual("THANK YOU", machineContorls.pressButtonCode("C3"));
            machineContorls.pressCoinReturn();
            machineContorls.checkForExactChange();
            Assert.AreEqual("EXACT CHANGE ONLY", machineContorls.coinDisplay.getCurrentDisplay());
        }

        [TestMethod]
        public void makeAbleToMakeChange()
        {
            List<Coin> tempCoinRemoval = new List<Coin> { new Coin(0.1607, 0.835, "Nickle"), new Coin(0.1607, 0.835, "Nickle"), new Coin(0.0729, 0.705, "Dime"), new Coin(0.0729, 0.705, "Dime") };
            machineContorls.emptyCoinInvintory(tempCoinRemoval);
            machineContorls.checkForExactChange();
            Assert.AreEqual("EXACT CHANGE ONLY", machineContorls.coinDisplay.getCurrentDisplay());
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedQuarter);
            machineContorls.insertCoin(insertedNickle);
            machineContorls.insertCoin(insertedDime);
            Assert.AreEqual("0.65", machineContorls.currentCoinTotal());
            Assert.AreEqual("THANK YOU", machineContorls.pressButtonCode("C3"));
            machineContorls.checkForExactChange();
            Assert.AreEqual("INSERT COIN", machineContorls.coinDisplay.getCurrentDisplay());
        }
    }
}
