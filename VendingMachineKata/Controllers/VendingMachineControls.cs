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
        private Inventory vendingInventory = new Inventory();
        private Coins coinModel = new Coins();
        private Products productModel = new Products();

        public VendingMachineDisplay coinDisplay = new VendingMachineDisplay();
        private VendingMachineChangeControls coinChange = new VendingMachineChangeControls();
        private decimal currentlyHeldValue = 0.0m;
        private List<string> ReturnedCoins = new List<string>();
        private List<Coin> insertedCoins = new List<Coin>();

        public VendingMachineControls()
        {
            coinModel.setCoinInventory(vendingInventory);
        }

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
                vendingInventory.addCoinsToInventory(coin);
                insertedCoins.Add(coin);
            }
            else
            {
                ReturnedCoins.Add(coin.name);
            }
        }

        public string pressCoinReturn()
        {
            makeChange();
            currentlyHeldValue = 0.0m;
            coinDisplay.updateCurrentDisplay("INSERT COIN");
            string returnedCoins = coinsInReturn();
            ReturnedCoins = new List<string>();
            return returnedCoins;
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
            if (vendingInventory.getProductLevel(code) <= 0) return "SOLD OUT";
            if (checkForExactChange()) return "EXACT CHANGE ONLY";
            if (checkFundsForBuying(currentlyHeldValue, productPrice))
            {                
                currentlyHeldValue -= productPrice;
                vendingInventory.addCoinsToInventory(insertedCoins);
                insertedCoins = new List<Coin>();
                coinDisplay.updateCurrentDisplay("INSERT COIN");
                if (currentlyHeldValue > 0) makeChange();
                int itemBought = vendingInventory.getProductLevel(code);
                vendingInventory.setProductLevel(code, --itemBought);
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
            Dictionary<Coin, int> returnedCoins = coinChange.breakChange(coinList, (int)(currentlyHeldValue * 100));
            List<Coin> removedCoins = new List<Coin>();
            foreach (KeyValuePair<Coin, int> coins in returnedCoins)
            {
                for (int i = 0; i < coins.Value; i++)
                {
                    removedCoins.Add(coins.Key);
                }
            }
            vendingInventory.removeCoinsFromInventory(removedCoins);
            ReturnedCoins.AddRange(coinChange.convertToListStringCoin(returnedCoins));
        }

        public decimal getCurrentHeldValue()
        {
            return this.currentlyHeldValue;
        }

        public List<string> getHeldProducts()
        {
            return this.productModel.getProducts();
        }

        public string getProductInfo(Product product)
        {
            return product.getName() + ": $" + product.getCost();
        }

        public bool checkForExactChange()
        {
            Dictionary<Coin,int> coinList = coinModel.getValidCoinList("UnitedStates");
            bool useExact = false;
            foreach(Product product in productModel.getProduct())
            {
                Dictionary<Coin,int> coinCheck = coinChange.breakChange(coinList, (int)(product.getCost() * 100));

                if (currentlyHeldValue > 0m)
                {
                    coinDisplay.updateCurrentDisplay(currentlyHeldValue.ToString());
                    if (coinCheck == null) useExact = true;
                }
                else if (coinCheck == null)
                {
                    coinDisplay.updateCurrentDisplay("EXACT CHANGE ONLY");
                    useExact = true;
                    break;
                }
                else coinDisplay.updateCurrentDisplay("INSERT COIN");
            }
            return useExact;
        }

        public void emptyCoinInvintory(List<Coin> coinRemoval)
        {
            vendingInventory.removeCoinsFromInventory(coinRemoval);
        }
    }
}
