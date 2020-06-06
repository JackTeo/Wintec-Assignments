using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overall_Project
{
    class Goat : Livestock //subclass of Livestock
    {
        private double amountOfMilk;

        public Goat(int ID, String type, double amountOfWater, double dailyCost, double weight, int age, string colour, double amountOfMilk)
            : base(ID, type, amountOfWater, dailyCost, weight, age, colour)
        {
            this.amountOfMilk = amountOfMilk;
        }

        public override double getAmountOfMilk() { return this.amountOfMilk; } //amount of milk getter

        public override double getProfit() //profit getter
        {
            return (amountOfMilk * RatesAndPrices.milkPriceGoat) - (getAmountOfWater() * RatesAndPrices.waterPrice) - getDailyCost();
        }

        public override double getGovTax() //government tax getter
        {
            return (amountOfMilk * RatesAndPrices.tax * 30);
        }
    }
}
