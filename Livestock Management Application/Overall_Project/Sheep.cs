using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overall_Project
{
    class Sheep : Livestock //subclass of Livestock
    {
        private double amountOfWool;

        public Sheep(int ID, String type, double amountOfWater, double dailyCost, double weight, int age, string colour, double amountOfWool)
            : base(ID, type, amountOfWater, dailyCost, weight, age, colour)
        {
            this.amountOfWool = amountOfWool;
        }

        public override double getAmountOfWool() { return this.amountOfWool; } //amount of wool getter

        public override double getProfit() //profit getter
        {
            return (amountOfWool * RatesAndPrices.woolPriceSheep) - (getAmountOfWater() * RatesAndPrices.waterPrice) - getDailyCost();
        }

        public override double getGovTax() //government tax getter
        {
            return (amountOfWool * RatesAndPrices.tax * 30);
        }
    }
}
