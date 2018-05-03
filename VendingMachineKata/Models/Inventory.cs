using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachineKata.Models
{
    public class Inventory
    {
        //Coin key then number of coins in dispenser
        private Dictionary<Coin, int> coinInventory = new Dictionary<Coin, int> {
            {new Coin(0.08, 0.75, "Penny"),0},
            {new Coin(0.1607, 0.835, "Nickle"),2},
            {new Coin(0.0729, 0.705, "Dime"),2},
            {new Coin(0.1823, 0.955, "Quarter"),5},
            {new Coin(0.365, 1.205, "Half-Dollar"),0},
            {new Coin(0.260, 1.043, "Dollar-Coin"),0}
        };

        private Dictionary<Coin, int> currentCoinValue = new Dictionary<Coin, int>();

        private Dictionary<string, int> vendingProductLevel = new Dictionary<string, int> {
            {"A1", 2},
            {"B2", 1},
            {"C3", 5},
            {"D4", 0}
        };

        internal void addCoinsToInventory(Coin coin)
        {
            List<Coin> depositedCoin = new List<Coin>();
            depositedCoin.Add(coin);
            addCoinsToInventory(depositedCoin);
        }

        public void addCoinsToInventory(List<Coin> deposited)
        {
            foreach (Coin coin in deposited)
            {
                ++coinInventory[coin];
                if (!currentCoinValue.ContainsKey(coin))
                {
                    if (!Coins.coinsUnitedStates.ContainsKey(coin)) currentCoinValue.Add(coin, Coins.coinsUnitedStates[coin]);
                }
            }
        }

        public void removeCoinsFromInventory(List<Coin> taken)
        {
            foreach (Coin coin in taken)
            {
                if (--coinInventory[coin] > 0)
                {
                    currentCoinValue.Remove(coin);
                }
            }
        }

        public void setCurrentCoinValue(Dictionary<Coin, int> coinTypes)
        {
            this.currentCoinValue = coinTypes;
        }

        public void removeCurrentCoinValue(Coin removedCoin)
        {
            this.currentCoinValue.Remove(removedCoin);
        }

        public Dictionary<Coin, int> getCurrentCoinValue()
        {
            return this.currentCoinValue;
        }

        public Dictionary<Coin, int> getCurrentCoinInventory()
        {
            return this.coinInventory;
        }

        public void setCoinInventory(Dictionary<Coin, int> newInventory)
        {
            this.coinInventory = newInventory;
        }

        public int getProductLevel(string keyCode)
        {
            return vendingProductLevel[keyCode];
        }

        public void setProductLevel(string keyCode, int productLevel)
        {
            vendingProductLevel[keyCode] = productLevel;
        }
    }
}
