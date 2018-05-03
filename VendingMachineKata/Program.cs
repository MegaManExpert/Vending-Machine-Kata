using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VendingMachineKata.Controllers;
using VendingMachineKata.Models;

namespace VendingMachineKata
{
    class Program
    {
        private static VendingMachineControls vendingControler = new VendingMachineControls();
        static string input = "";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to VendMaster2000! Please select a choice below:");
            List<string> heldProducts = vendingControler.getHeldProducts();
            foreach (string info in heldProducts)
            {
                Console.WriteLine(info);
            }
            Console.WriteLine("Type the coin name to insert, product code to vend, return to return all coins.");
            while (!input.Equals("exit"))
            {
                vendingControler.checkForExactChange();
                Console.WriteLine(vendingControler.coinDisplay.getCurrentDisplay());
                input = Console.ReadLine();
                switch (input)
                {
                    case "Penny":
                        Coin insertedPenny = new Coin(0.08, 0.75, "Penny");
                        vendingControler.insertCoin(insertedPenny);
                        break;
                    case "Nickle":
                        Coin insertedNickle = new Coin(0.1607, 0.835, "Nickle");
                        vendingControler.insertCoin(insertedNickle);
                        break;
                    case "Dime":
                        Coin insertedDime = new Coin(0.0729, 0.705, "Dime");
                        vendingControler.insertCoin(insertedDime);
                        break;
                    case "Quarter":
                        Coin insertedQuarter = new Coin(0.1823, 0.955, "Quarter");
                        vendingControler.insertCoin(insertedQuarter);
                        break;
                    case "A1":
                        Console.WriteLine(vendingControler.pressButtonCode("A1"));
                        break;
                    case "B2":
                        Console.WriteLine(vendingControler.pressButtonCode("B2"));
                        break;
                    case "C3":
                        Console.WriteLine(vendingControler.pressButtonCode("C3"));
                        break;
                    case "D4":
                        Console.WriteLine(vendingControler.pressButtonCode("D4"));
                        break;
                    case "Return":
                        Console.WriteLine(vendingControler.pressCoinReturn());
                        break;
                    default:
                        Console.WriteLine(vendingControler.pressButtonCode(input));
                        break;
                }
            }
        }
    }
}
