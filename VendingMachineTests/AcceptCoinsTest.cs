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
    public class AcceptCoinsTest
    {
        Coin insertedPenny = new Coin(0.08, 0.75, "Penny");
        Coin insertedNickle = new Coin(0.1607, 0.835, "Nickle");
        Coin insertedDime = new Coin(0.0729, 0.705, "Dime");
        Coin insertedQuarter = new Coin(0.1823, 0.955, "Quarter");

        Coin insertedFakeCoin = new Coin(0.1825, 0.955, "Quarter");
        VendingMachineControls machineContorls = new VendingMachineControls();
        Coins coinModel = new Coins();
        VendingMachineDisplay coinDisplay = new VendingMachineDisplay();

        [TestMethod]
        public void AcceptAnyCoin()
        {
            int? coinValue = coinModel.getCoinValue(insertedPenny);
            Assert.IsTrue(coinValue.HasValue);
        }

        [TestMethod]
        public void AcceptOnlyknownCoins()
        {
            int? coinValue = coinModel.getCoinValue(insertedQuarter);
            Assert.IsTrue(coinValue.HasValue);
            coinValue = coinModel.getCoinValue(insertedFakeCoin);
            Assert.IsFalse(coinValue.HasValue);
        }

        [TestMethod]
        public void AcceptOnlyValidCoins()
        {
            Assert.IsTrue(coinModel.isBannedCoin(insertedPenny));
            Assert.IsFalse(coinModel.isBannedCoin(insertedNickle));
            Assert.IsFalse(coinModel.isBannedCoin(insertedDime));
            Assert.IsFalse(coinModel.isBannedCoin(insertedQuarter));
        }

        [TestMethod]
        public void coinValuesReturned()
        {
            Assert.AreEqual<int?>(1, coinModel.getCoinValue(insertedPenny));
            Assert.AreEqual<int?>(5, coinModel.getCoinValue(insertedNickle));
            Assert.AreEqual<int?>(10, coinModel.getCoinValue(insertedDime));
            Assert.AreEqual<int?>(25, coinModel.getCoinValue(insertedQuarter));
        }

        [TestMethod]
        public void displayValueOfInsertedCoins()
        {
            Assert.AreEqual("INSERT COIN", machineContorls.currentCoinTotal());
            machineContorls.insertCoin(insertedPenny);
            Assert.AreEqual("INSERT COIN", machineContorls.currentCoinTotal());
            machineContorls.insertCoin(insertedNickle);
            Assert.AreEqual("0.05", machineContorls.currentCoinTotal());
            machineContorls.insertCoin(insertedDime);
            Assert.AreEqual("0.15", machineContorls.currentCoinTotal());
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual("0.40", machineContorls.currentCoinTotal());
        }

        [TestMethod]
        public void showThatCoinsReturned()
        {
            Assert.AreEqual("", machineContorls.coinsInReturn());
            machineContorls.insertCoin(insertedPenny);
            Assert.AreEqual("1:Penny", machineContorls.coinsInReturn().TrimEnd());
            machineContorls.insertCoin(insertedNickle);
            Assert.AreEqual("1:Penny", machineContorls.coinsInReturn().TrimEnd());
            machineContorls.insertCoin(insertedDime);
            Assert.AreEqual("1:Penny", machineContorls.coinsInReturn().TrimEnd());
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual("1:Penny", machineContorls.coinsInReturn().TrimEnd());
            machineContorls.insertCoin(insertedPenny);
            Assert.AreEqual("2:Penny", machineContorls.coinsInReturn().TrimEnd());
            machineContorls.insertCoin(insertedFakeCoin);
            Assert.AreEqual("2:Penny 1:Quarter", machineContorls.coinsInReturn().TrimEnd());
        }
    }
}
