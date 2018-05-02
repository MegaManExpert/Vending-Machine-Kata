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

        private List<Coin> BannedCoins = new List<Coin> { new Coin(0.08, 0.75, "Penny") };

        public int? getCoinValue(Coin coin)
        {
            if (coinsUnitedStates.ContainsKey(coin)) return coinsUnitedStates[coin];
            return null;
        }

        public bool isBannedCoin(Coin coin)
        {
            if (coinsUnitedStates.ContainsKey(coin)) return BannedCoins.Contains(coin);
            return false;
        }

        public Dictionary<Coin, int> getValidCoinList(string coinListName)
        {
            if (coinListName.Equals("UnitedStates"))
            {
                foreach (Coin banndedCoin in BannedCoins)
                {
                    coinsUnitedStates.Remove(banndedCoin);
                }
                return coinsUnitedStates;
            }
            return null;            
        }
    }
}
