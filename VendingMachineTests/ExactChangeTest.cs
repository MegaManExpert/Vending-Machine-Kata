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
        Coin insertedQuarter = new Coin(0.1823, 0.955, "Quarter");

        VendingMachineControls machineContorls = new VendingMachineControls();
        Coins coinModel = new Coins();
        VendingMachineDisplay coinDisplay = new VendingMachineDisplay();

        [TestMethod]
        public void unableToMakeChange()
        {
            List<Coin> tempCoinRemoval = new List<Coin> {new Coin(0.1607, 0.835, "Nickle"), new Coin(0.1607, 0.835, "Nickle"), new Coin(0.1607, 0.835, "Nickle")};
            machineContorls.emptyCoinInvintory(tempCoinRemoval);
            machineContorls.checkForExactChange();
            Assert.AreEqual("EXACT CHANGE ONLY", machineContorls.coinDisplay.getCurrentDisplay());
        }

        [TestMethod]
        public void makeUnableToMakeChange()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void makeAbleToMakeChange()
        {
            Assert.Fail();
        }
    }
}
