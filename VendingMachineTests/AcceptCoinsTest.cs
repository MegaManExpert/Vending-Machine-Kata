using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineKata;

namespace VendingMachineTests
{
    [TestClass]
    public class AcceptCoinsTest
    {
        [TestMethod]
        public void AcceptAnyCoin()
        {
            Coin insertedCoin = new Coin(0.1823, 0.955, "Quarter");
            VendingMachine machine = new VendingMachine();

            Assert.IsTrue(machine.isUnitedStatesCoin(insertedCoin));
        }

        [TestMethod]
        public void AcceptOnlyknownCoins()
        {
            Coin insertedCoin = new Coin(0.1823, 0.955, "Quarter");
            Coin insertedFakeCoin = new Coin(0.1825, 0.955, "Quarter");
            VendingMachine machine = new VendingMachine();

            Assert.IsTrue(machine.isUnitedStatesCoin(insertedCoin));
            Assert.IsFalse(machine.isUnitedStatesCoin(insertedFakeCoin));
        }

        [TestMethod]
        public void AcceptOnlyValidCoins()
        {
            Coin insertedPenny = new Coin(0.08, 0.75, "Penny");
            Coin insertedNickle = new Coin(0.1607, 0.835, "Nickle");
            Coin insertedDime = new Coin(0.0729, 0.705, "Dime");
            Coin insertedQuarter = new Coin(0.1823, 0.955, "Quarter");
            
            VendingMachine machine = new VendingMachine();

            Assert.IsFalse(machine.isAcceptedCoin(insertedPenny));
            Assert.IsTrue(machine.isAcceptedCoin(insertedNickle));
            Assert.IsTrue(machine.isAcceptedCoin(insertedDime));
            Assert.IsTrue(machine.isAcceptedCoin(insertedQuarter));
        }

        [TestMethod]
        public void coinValuesReturned()
        {
            Coin insertedPenny = new Coin(0.08, 0.75, "Penny");
            Coin insertedNickle = new Coin(0.1607, 0.835, "Nickle");
            Coin insertedDime = new Coin(0.0729, 0.705, "Dime");
            Coin insertedQuarter = new Coin(0.1823, 0.955, "Quarter");

            VendingMachine machine = new VendingMachine();

            Assert.AreEqual<int>(machine.readCoinValues(insertedPenny),1);
            Assert.AreEqual<int>(machine.readCoinValues(insertedNickle), 5);
            Assert.AreEqual<int>(machine.readCoinValues(insertedDime), 10);
            Assert.AreEqual<int>(machine.readCoinValues(insertedQuarter), 25);
        }

        [TestMethod]
        public void displayValueOfInsertedCoins()
        {
            Coin insertedPenny = new Coin(0.08, 0.75, "Penny");
            Coin insertedNickle = new Coin(0.1607, 0.835, "Nickle");
            Coin insertedDime = new Coin(0.0729, 0.705, "Dime");
            Coin insertedQuarter = new Coin(0.1823, 0.955, "Quarter");

            VendingMachine machine = new VendingMachine();

            Assert.AreEqual<string>(machine.currentCoinTotal(), "INSERT COIN");
            machine.insertCoin(insertedPenny);
            Assert.AreEqual<string>(machine.currentCoinTotal(), "INSERT COIN");
            machine.insertCoin(insertedNickle);
            Assert.AreEqual<string>(machine.currentCoinTotal(), "0.05");
            machine.insertCoin(insertedDime);
            Assert.AreEqual<string>(machine.currentCoinTotal(), "0.15");
            machine.insertCoin(insertedQuarter);
            Assert.AreEqual<string>(machine.currentCoinTotal(), "0.40");
        }
    }
}
