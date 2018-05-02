using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VendingMachineKata.Models;
using VendingMachineKata.Views;

namespace VendingMachineKata.Controllers
{
    class VendingMachineChangeControls
    {
        //Convert current remaning quantity to full cents 
        public List<string> breakChange(Dictionary<Coin, int> usableCoins, int currentQuantity)
        {
            //Split the key and value up for later remerging
            List<Coin> arrCoins = new List<Coin>();
            List<int> arrCoinValues = new List<int>();

            // Order the list from largest bundle size to smallest size
            foreach (KeyValuePair<Coin, int> coinValue in usableCoins.OrderByDescending(e => e.Value))
            {
                arrCoins.Add(coinValue.Key);
                arrCoinValues.Add(coinValue.Value);
            }

            // Key: Bundles | Value: Quantity
            Dictionary<Coin, int> hmBundleToQuantity = new Dictionary<Coin, int>();

            // Create the hashmap table structure
            foreach (Coin Bundle in arrCoins)
            {
                hmBundleToQuantity.Add(Bundle, 0);
            }

            // Keep track of our index of the bundles we need to figure out
            Stack<int> stBundleIndex = new Stack<int>();

            // Used to calculate the left and right of bundle list
            int ixCursor;

            // Our remaining quantity after calculations
            int iRemaining;

            /*
                This will act as the midpoint of the bundle list
                Will update the right of the cursor, decrement the
                cursor, don’t touch the left, and since the loop 
                hasn’t started we’ll consider everything updatable
                and on the right of the cursor
            */
            stBundleIndex.Push(-1);

            // Keep working till there is no more ways to solve the solution
            while (stBundleIndex.Count > 0)
            {
                // The current cursor is always removed and needs to
                // be added back if we want to check it again
                ixCursor = stBundleIndex.Pop();

                iRemaining = currentQuantity;

                for (int ixBundle = 0; ixBundle < arrCoinValues.Count; ++ixBundle)
                {
                    //Left of current scope, values remain the same
                    if (ixBundle < ixCursor)
                    {
                        iRemaining -= (hmBundleToQuantity[arrCoins[ixBundle]] * arrCoinValues[ixBundle]);
                    }

                    //At the cursor stack scope – decrement current quantity
                    if (ixBundle == ixCursor)
                    {
                        --hmBundleToQuantity[arrCoins[ixBundle]];
                        iRemaining -= (hmBundleToQuantity[arrCoins[ixBundle]] * arrCoinValues[ixBundle]);
                    }

                    //Right of current scope gets calculated
                    if (ixBundle > ixCursor)
                    {
                        hmBundleToQuantity[arrCoins[ixBundle]] += iRemaining / arrCoinValues[ixBundle];
                        iRemaining = iRemaining % arrCoinValues[ixBundle];
                    }
                }

                if (iRemaining == 0) return convertToListStringCoin(hmBundleToQuantity);

                // Otherwise… We have nothing left on the stack we’ll check
                // back to the beginning for non-zero values
                int iNonEmptyStart = 0;

                //Keep the current scope on the stack if the quantity is still bigger than zero
                if (ixCursor >= 0 && hmBundleToQuantity[arrCoins[ixCursor]] > 0)
                {
                    stBundleIndex.Push(ixCursor);

                    // Maximum cursor on the stack
                    // (this way we don’t decrement further back than we want)
                    iNonEmptyStart = stBundleIndex.Max();
                }

                //Add last non-empty value to the stack to decrement and recalculate from there
                for (int ixNonEmpty = arrCoins.Count - 1; ixNonEmpty >= iNonEmptyStart; ixNonEmpty--)
                {
                    if (hmBundleToQuantity[arrCoins[ixNonEmpty]] > 0)
                    {
                        stBundleIndex.Push(ixNonEmpty);
                        break;
                    }
                }
            }
            return null;
        }

        private List<string> convertToListStringCoin(Dictionary<Coin, int> hmCoinCollection)
        {
            List<string> arrListOfCoinsReturned = new List<string>();
            foreach (KeyValuePair<Coin, int> coinValue in hmCoinCollection)
            {
                for (int ixCoins = 0; ixCoins < coinValue.Value; ++ixCoins)
                {
                    arrListOfCoinsReturned.Add(coinValue.Key.name);
                }
            }
            return arrListOfCoinsReturned;
        }
    }
}
