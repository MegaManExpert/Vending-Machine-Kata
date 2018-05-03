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
        public static readonly Dictionary<Coin, int> coinsUnitedStates = new Dictionary<Coin, int> {
            {new Coin(0.08, 0.75, "Penny"),1},
            {new Coin(0.1607, 0.835, "Nickle"),5},
            {new Coin(0.0729, 0.705, "Dime"),10},
            {new Coin(0.1823, 0.955, "Quarter"),25},
            {new Coin(0.365, 1.205, "Half-Dollar"),50},
            {new Coin(0.260, 1.043, "Dollar-Coin"),100}
        };

        private List<Coin> bannedCoins = new List<Coin> { new Coin(0.08, 0.75, "Penny") };

        private Inventory coinInventory = new Inventory();

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
                coinInventory.setCurrentCoinValue(new Dictionary<Coin, int>(coinsUnitedStates));
                foreach (Coin banndedCoin in bannedCoins)
                {
                    coinInventory.removeCurrentCoinValue(banndedCoin);
                }
                foreach (KeyValuePair<Coin, int> missingCoin in coinInventory.getCurrentCoinInventory())
                {
                    if (missingCoin.Value <= 0) coinInventory.removeCurrentCoinValue(missingCoin.Key);
                }
                return coinInventory.getCurrentCoinValue();
            }
            return null;            
        }

        public void setCoinInventory(Inventory passedCoinInventory)
        {
            this.coinInventory = passedCoinInventory;
        }
    }
}
