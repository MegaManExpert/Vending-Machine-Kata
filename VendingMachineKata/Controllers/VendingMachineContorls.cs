using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VendingMachineKata.Models;
using VendingMachineKata.Views;

namespace VendingMachineKata.Controllers
{
    public class VendingMachineContorls
    {
        private Coins coinModel = new Coins();
        private VendingMachineDisplay coinDisplay = new VendingMachineDisplay();
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
    }
}
