using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overall_Project
{
    class Cow : Livestock //subclass of Livestock
    {
        private double amountOfMilk; //Amount of milk per day
        private bool isJersy; //Jersy cow or not

        public Cow(int ID, String type, double amountOfWater, double dailyCost, double weight, int age, string colour, double amountOfMilk, bool isJersy)
            : base(ID, type, amountOfWater, dailyCost, weight, age, colour)
        {
            this.amountOfMilk = amountOfMilk;
            this.isJersy = isJersy;
        }

        public override double getAmountOfMilk() { return this.amountOfMilk; } //amount of milk getter

        public override Boolean getIsJersy() { return this.isJersy; } // is or not jersy cow getter

        public override double getProfit() //profit getter
        {
            return (amountOfMilk * RatesAndPrices.milkPriceCow) - (getAmountOfWater() * RatesAndPrices.waterPrice) - getDailyCost();
        }

        public override double getGovTax() //government tax getter
        {
            return (amountOfMilk * RatesAndPrices.tax * 30);
        }

        
    }
}
