using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VendingMachineKata.Models;
using VendingMachineKata.Views;

namespace VendingMachineKata.Controllers
{
    public class VendingMachineControls
    {
        private Coins coinModel = new Coins();
        private Products productModel = new Products();

        public VendingMachineDisplay coinDisplay = new VendingMachineDisplay();
        private VendingMachineChangeControls coinChange = new VendingMachineChangeControls();
        private decimal currentlyHeldValue = 0.0m;
        private List<string> ReturnedCoins = new List<string>();

        public string currentCoinTotal()
        {
            return coinDisplay.getCurrentDisplay();
        }

        public void insertCoin(Coin coin)
        {
            int? coinValue = coinModel.getCoinValue(coin);
            if (!coinModel.isBannedCoin(coin) && coinValue.HasValue)
            {
                this.currentlyHeldValue += ((decimal)coinValue.Value / 100);
                coinDisplay.updateCurrentDisplay(currentlyHeldValue.ToString());
            }
            else
            {
                ReturnedCoins.Add(coin.name);
            }
        }

        public string coinsInReturn()
        {
            string coinReturn = "";
            var grpReturns = ReturnedCoins.GroupBy(e => e).Select(e => new { name = e.Key, cnt = e.Count() }).ToList();
            foreach(var returns in grpReturns)
            {
                coinReturn += returns.cnt + ":" + returns.name + " ";
            }
            return coinReturn;
        }

        public string pressButtonCode(string code)
        {
            string selectionText = "PRICE: $";
            List<Product> selectedProduct = productModel.getProduct(code);
            if (selectedProduct.Count <= 0) return "INVALID/SELECT";

            decimal productPrice = selectedProduct.First().getCost();
            if (checkFundsForBuying(currentlyHeldValue, productPrice))
            {
                currentlyHeldValue -= productPrice;
                coinDisplay.updateCurrentDisplay("INSERT COIN");
                if (currentlyHeldValue > 0) makeChange();
                return "THANK YOU";
            }
            return selectionText + productPrice;            
        }

        private bool checkFundsForBuying(decimal funds, decimal price)
        {
            if (funds >= price) return true;
            return false;
        }

        private void makeChange()
        {
            coinDisplay.updateCurrentDisplay(currentlyHeldValue.ToString());
            Dictionary<Coin,int> coinList = coinModel.getValidCoinList("UnitedStates");
            ReturnedCoins.AddRange(coinChange.breakChange(coinList, (int)(currentlyHeldValue * 100)));
        }

        public decimal getCurrentHeldValue()
        {
            return this.currentlyHeldValue;
        }

        public string getProductInfo(Product product)
        {
            return product.getName() + ": $" + product.getCost();
        }
    }
}
