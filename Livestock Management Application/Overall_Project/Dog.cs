using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overall_Project
{
    class Dog : Livestock //subclass of Livestock
    {
        public Dog(int ID, String type, double amountOfWater, double weight, int age, string colour, double dailyCost)
            : base(ID, type, amountOfWater, dailyCost, weight, age, colour) { }

        public override double getProfit() //profit getter
        {
            return 0 - (getAmountOfWater() * RatesAndPrices.waterPrice) - getDailyCost();
        }
    }
}
