using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachineKata.Models
{
    public class Coins
    {
        //Coin, amount in cents
        //Need to take in account for diffrent era of coins? I.E. Copper pennies and shield nickles
        private Dictionary<Coin, int> coinsUnitedStates = new Dictionary<Coin, int> {
            {new Coin(0.08, 0.75, "Penny"),1},
            {new Coin(0.1607, 0.835, "Nickle"),5},
            {new Coin(0.0729, 0.705, "Dime"),10},
            {new Coin(0.1823, 0.955, "Quarter"),25},
            {new Coin(0.365, 1.205, "Half-Dollar"),50},
            {new Coin(0.260, 1.043, "Dollar-Coin"),100}
        };

        private Dictionary<Coin, int> currentCoinValue = new Dictionary<Coin, int>();

        //Coin key then number of coins in dispenser
        private Dictionary<Coin, int> coinInventory = new Dictionary<Coin, int> {
            {new Coin(0.08, 0.75, "Penny"),0},
            {new Coin(0.1607, 0.835, "Nickle"),2},
            {new Coin(0.0729, 0.705, "Dime"),20},
            {new Coin(0.1823, 0.955, "Quarter"),5},
            {new Coin(0.365, 1.205, "Half-Dollar"),0},
            {new Coin(0.260, 1.043, "Dollar-Coin"),0}
        };

        private List<Coin> bannedCoins = new List<Coin> { new Coin(0.08, 0.75, "Penny") };

        public int? getCoinValue(Coin coin)
        {
            if (coinsUnitedStates.ContainsKey(coin)) return coinsUnitedStates[coin];
            return null;
        }

        public bool isBannedCoin(Coin coin)
        {
            if (coinsUnitedStates.ContainsKey(coin)) return bannedCoins.Contains(coin);
            return false;
        }

        public Dictionary<Coin, int> getValidCoinList(string coinListName)
        {            
            if (coinListName.Equals("UnitedStates"))
            {
                currentCoinValue = coinsUnitedStates;
                foreach (Coin banndedCoin in bannedCoins)
                {
                    currentCoinValue.Remove(banndedCoin);
                }
                foreach (KeyValuePair<Coin, int> missingCoin in coinInventory)
                {
                    if (missingCoin.Value <= 0) currentCoinValue.Remove(missingCoin.Key);
                }
                return currentCoinValue;
            }
            return null;            
        }

        public void addCoinsToInventory(List<Coin> deposited)
        {
            foreach (Coin coin in deposited)
            {
                ++coinInventory[coin];
                if (!currentCoinValue.ContainsKey(coin))
                {
                    if (!currentCoinValue.ContainsKey(coin)) currentCoinValue.Add(coin, coinsUnitedStates[coin]);
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

        public void setCoinInventory(Dictionary<Coin, int> newInventory)
        {
            this.coinInventory = newInventory;
        }

    }
}
