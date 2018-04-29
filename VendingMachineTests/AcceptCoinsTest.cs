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
        VendingMachineContorls machineContorls = new VendingMachineContorls();
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
            Assert.AreEqual<int?>(coinModel.getCoinValue(insertedPenny), 1);
            Assert.AreEqual<int?>(coinModel.getCoinValue(insertedNickle), 5);
            Assert.AreEqual<int?>(coinModel.getCoinValue(insertedDime), 10);
            Assert.AreEqual<int?>(coinModel.getCoinValue(insertedQuarter), 25);
        }

        [TestMethod]
        public void displayValueOfInsertedCoins()
        {
            Assert.AreEqual<string>(machineContorls.currentCoinTotal(), "INSERT COIN");
            machineContorls.insertCoin(insertedPenny);
            Assert.AreEqual<string>(machineContorls.currentCoinTotal(), "INSERT COIN");
            machineContorls.insertCoin(insertedNickle);            
            Assert.AreEqual<string>(machineContorls.currentCoinTotal(), "0.05");
            machineContorls.insertCoin(insertedDime);
            Assert.AreEqual<string>(machineContorls.currentCoinTotal(), "0.15");
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual<string>(machineContorls.currentCoinTotal(), "0.40");
        }

        [TestMethod]
        public void showThatCoinsReturned()
        {
            Assert.AreEqual<string>(machineContorls.coinsInReturn(), "");
            machineContorls.insertCoin(insertedPenny);
            Assert.AreEqual<string>(machineContorls.coinsInReturn().TrimEnd(), "1:Penny");
            machineContorls.insertCoin(insertedNickle);
            Assert.AreEqual<string>(machineContorls.coinsInReturn().TrimEnd(), "1:Penny");
            machineContorls.insertCoin(insertedDime);
            Assert.AreEqual<string>(machineContorls.coinsInReturn().TrimEnd(), "1:Penny");
            machineContorls.insertCoin(insertedQuarter);
            Assert.AreEqual<string>(machineContorls.coinsInReturn().TrimEnd(), "1:Penny");
            machineContorls.insertCoin(insertedPenny);
            Assert.AreEqual<string>(machineContorls.coinsInReturn().TrimEnd(), "2:Penny");
            machineContorls.insertCoin(insertedFakeCoin);
            string test = machineContorls.coinsInReturn().TrimEnd();
            Assert.AreEqual<string>(machineContorls.coinsInReturn().TrimEnd(), "2:Penny 1:Quarter");
        }
    }
}
