using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overall_Project
{
    class Livestock //Parent class
    {
        private int ID; //No duplicates
        private String type; //Type of animal
        private double amountOfWater; //Water needed per day
        private double dailyCost; //Local currency
        private double weight; //In KG
        private int age; //In years
        private String colour; //Animal colour
        public Livestock left; //binary tree left
        public Livestock right; //binary tree right
        private Livestock livestock;

        public Livestock()
        {
            return;
        }

        public Livestock(int ID, String type, double amountOfWater, double dailyCost, double weight, int age, String colour) //constructor
        {
            this.ID = ID;
            this.type = type;
            this.amountOfWater = amountOfWater;
            this.dailyCost = dailyCost;
            this.weight = weight;
            this.age = age;
            this.colour = colour;
            left = null;
            right = null;
        }
        public int getID() { return this.ID; } //ID getter
        public String getType() { return this.type; } //type getter
        public double getAmountOfWater() { return this.amountOfWater; } //Amount of water getter
        public double getDailyCost() { return this.dailyCost; } //Daily cost getter
        public double getWeight() { return this.weight; } //Weight getter
        public int getAge() { return this.age; } //Age getter
        public String getColour() { return this.colour; } //Colour getter
        public virtual double getAmountOfMilk() { return 0; } //Amount of milk getter
        public virtual Boolean getIsJersy() { return false; }  //Is or not Jersy getter
        public virtual double getAmountOfWool() { return 0; } //Amount of wool getter
        public virtual double getProfit() { return 0; } //Profit getter
        public virtual double getGovTax() { return 0; } //Government tax getter

        public void insertToTree(Livestock newLivestock) //To insert livestock into binary tree
        {
            Livestock pointer = livestock; //set livestock to pointer

            if (pointer == null)
            {
                livestock = newLivestock; //if tree is empty, newLivestock will become the root.
                return;
            }

            while (true)
            {
                if (newLivestock.getProfit() < pointer.getProfit()) //if inserted profit is < pointer
                {
                    if (pointer.left == null) //insert to left if pointer.left is empty
                    {
                        pointer.left = newLivestock;
                        break;
                    }
                    else { pointer = pointer.left; } //reset pointer.left to pointer
                }
                else //if inserted data is > pointer
                {
                    if (pointer.right == null) //insert to right if pointer.right is empty
                    {
                        pointer.right = newLivestock;
                        break;
                    }
                    else { pointer = pointer.right; } //reset pointer.right to pointer
                }
            }

            
        }

        public String profitInOrder() //Retreive profit in desending order from binary tree
        {
            string str = "ID\t|  Profit\r\n--------------------------\r\n";
            profitInOrder(livestock);
            return str;

            void profitInOrder(Livestock livestock) //recursive function
            {
                if (livestock == null)
                {
                    return;
                }
                profitInOrder(livestock.right); //most right is the biggest number
                str += livestock.getID().ToString() + "\t|   " + Math.Round(livestock.getProfit()).ToString() + "\r\n";
                profitInOrder(livestock.left); //if right is null, get left
            }
        }
    }
}
