using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace VendingMachineKata.Models
{
    public class Coin
    {
        //Troy oz.
        public double weight;
        //Inches
        public double diameter;
        //Offical name, no nicknames
        public string name;

        public Coin(double coinWeight, double coinDiameter, string coinName)
        {
            this.weight = coinWeight;
            this.diameter = coinDiameter;
            this.name = coinName;
        }

        public override bool Equals(Object obj)
        {
            Coin other = obj as Coin;
            return this.weight == other.weight && this.diameter == other.diameter && this.name == other.name;
        }

        public override int GetHashCode()
        {
            return this.weight.GetHashCode() + this.diameter.GetHashCode() + this.name.GetHashCode();
        }
    }
}
