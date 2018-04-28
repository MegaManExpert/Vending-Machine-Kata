using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachineKata
{
    public class VendingMachine
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

        public int readCoinValues(Coin coin)
        {
            return coinsUnitedStates[coin];
        }

        public bool isAcceptedCoin(Coin coin)
        {
            if (coinsUnitedStates.ContainsKey(coin))
            {
                List<Coin> bannedCoins = coinsUnitedStates.Keys.Where(e => e.name == "Penny").ToList();
                if (!bannedCoins.Contains(coin)) return true;
            }
            return false;
        }

        public bool isUnitedStatesCoin(Coin coin)
        {
            return coinsUnitedStates.ContainsKey(coin);
        }

        public string currentCoinTotal()
        {
            return "INSERT COIN";
        }
        public void insertCoin(Coin coin)
        {
            //add to overall coin total
        }
    }
}
