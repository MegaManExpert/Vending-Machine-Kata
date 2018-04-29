using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachineKata.Views
{
    public class VendingMachineDisplay
    {
        private string VendingDisplayOutput = "INSERT COIN";

        public string getCurrentDisplay()
        {
            return this.VendingDisplayOutput;
        }

        public void updateCurrentDisplay(string valueUpdate)
        {
            this.VendingDisplayOutput = valueUpdate;
        }
    }
}
