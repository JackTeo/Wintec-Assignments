using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.IO;

namespace Overall_Project
{
    public partial class Form1 : Form
    {
        private static Dictionary<int, Livestock> allAnimals = new Dictionary<int, Livestock>(); //hash table
        private Livestock livestock; //place holder
        private Livestock livestockTree; //binary tree
        
        public Form1()
        {
            InitializeComponent();
        }

        private void checkButton_Click(object sender, EventArgs e) //get data from hash table according to ID
        {
            try //try catching errors
            {
                if (inputBox.Text == null || inputBox.TextLength == 0) //alert if input is empty
                {
                    MessageBox.Show("ID cannot be empty.");
                }
                else if (int.TryParse(inputBox.Text, out int isNumeric)) //alert if input is not numeric
                {
                    if (allAnimals.TryGetValue(isNumeric, out livestock)) //get data of ID from hash table
                    {
                        animalIDLabel.Text = livestock.getID().ToString(); //get id
                        if (livestock.getIsJersy() == true) //display jersey or not jersey
                        {
                            animalTypeLabel.Text = livestock.getType() + " - Jersey";
                        }
                        else animalTypeLabel.Text = livestock.getType();
                        amountOfWaterLabel.Text = livestock.getAmountOfWater().ToString(); //get amount of water
                        dailyCostLabel.Text = livestock.getDailyCost().ToString(); //get daily cost
                        weightLabel.Text = livestock.getWeight().ToString(); //get weight
                        ageLabel.Text = livestock.getAge().ToString(); //get age
                        colourLabel.Text = livestock.getColour().ToString(); //get colour
                        if (livestock.getAmountOfMilk() == 0) //display n/a if not milk animal
                        {
                            amountOfMilkLabel.Text = "N/A";
                        }
                        else amountOfMilkLabel.Text = livestock.getAmountOfMilk().ToString();
                        if (livestock.getAmountOfWool() == 0) //display n/a if not sheep
                        {
                            amountOfWoolLabel.Text = "N/A";
                        }
                        else amountOfWoolLabel.Text = livestock.getAmountOfWool().ToString();
                    }
                    else { MessageBox.Show("Animal not registered."); } //alert if ID not found
                }
                else { MessageBox.Show("Please enter ID as integer."); }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message); //show error type
            }
        }

        private void Form1_Load(object sender, EventArgs e) //auto load
        {
            readData(); //read data

            double totalProfit = 0; //Report 2: Display the total profitability/loose of the farm per day
            double totalTax = 0; //Report 3: Display the total tax paid to the governement per month
            double totalMilk = 0; //Report 4: Display the total amount of milk per day for goats and cows
            double totalAge = 0; //Report 5: Display the average age of all animal farms (DOG excluded)
            int excludeDogCount = 0; //Report 5 & 8
            double totalMilkProfit = 0; //Report 6: Display the average profitability of “Goats and Cow” Vs. Sheep
            double totalWoolProfit = 0; //Report 6
            int milkAnimalCount = 0; //Report 6
            int woolAnimalCount = 0; //Report 6
            double dogCost = 0; //Report 7: Display the ratio of Dogs’ cost compared to the total cost
            double animalCost = 0; //Report 7
            double redCount = 0; //Report 9: Display the ratio of livestock with the color red
            double taxPaid = 0; //Report 10: Display the total tax paid for Jersey Cows
            double profit = 0; //Report 12: Display the total profitability of all Jersey Cows.

            foreach (var animal in allAnimals) 
            {
                if (allAnimals.TryGetValue(animal.Key, out livestock))
                {
                    totalProfit += livestock.getProfit(); //Report 2
                    totalTax += livestock.getGovTax(); //Report 3
                    totalMilk += livestock.getAmountOfMilk(); //Report 4

                    switch (livestock.getType())
                    {
                        case "Dogs":
                            dogCost += livestock.getDailyCost(); //Report 7
                            break;
                        case "Sheep":
                            totalAge += livestock.getAge(); //Report 5
                            totalWoolProfit += livestock.getProfit(); //Report 6
                            animalCost += livestock.getDailyCost(); //Report 7
                            excludeDogCount++; //Report 5 & 8
                            woolAnimalCount++; //Report 6
                            break;
                        default:
                            totalAge += livestock.getAge(); //Report 5
                            totalMilkProfit += livestock.getProfit(); //Report 6
                            animalCost += livestock.getDailyCost(); //Report 7
                            excludeDogCount++; //Report 5 & 8
                            milkAnimalCount++; //Report 6
                            break;
                    }

                    switch (livestock.getColour().ToLower())
                    {
                        case "red":
                            redCount++; //Report 9
                            break;
                    }

                    if (livestock.getIsJersy() == true)
                    {
                        taxPaid += (livestock.getAmountOfMilk() * RatesAndPrices.jersyCowTax); //Report 10
                        profit += livestock.getProfit(); //Report 12
                    }
                }
            }
            farmProfitLabel.Text = totalProfit.ToString(); //Report 2
            taxPaidLabel.Text = totalTax.ToString(); //Report 3
            totalMilkLabel.Text = totalMilk.ToString(); //Report 4
            aveAgeLabel.Text = Math.Round((totalAge / excludeDogCount), 2).ToString(); //Report 5
            aveProfitRLabel.Text = Math.Round(((totalMilkProfit / milkAnimalCount) / (totalWoolProfit / woolAnimalCount)), 2).ToString(); //Report 6
            dogCostRLabel.Text = Math.Round((dogCost / (animalCost + dogCost)), 2).ToString(); //Report 7
            redLSRLabel.Text = Math.Round((redCount / allAnimals.Count), 2).ToString(); //Report 9
            totalTaxJerLabel.Text = Math.Round((taxPaid * 30), 2).ToString(); //Report 10
            totalProfitJerLabel.Text = Math.Round((profit * 30), 2).ToString(); //Report 12

            /* Report 8: Generate a file that contains the ID of all animal ordered by their
             * profitability(You are not allowed to use built -in sorting algorithm – Your
             * code must do the sorting). Dogs are excluded.
            */
            livestockTree = bst();
        }

        public void readData() //read data from database
        {
            String[] farmTable = new String[] { "Commodity Prices", "Cows", "Dogs", "Goats", "Sheep" }; //table names
            String conn_string = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\jinki\\Documents\\GitHub\\Wintec-Assignments\\Livestock Management Application" +
                "\\FarmInfomation.accdb; Persist Security Info = False"; //connection address
            String q = ""; //query
            OleDbConnection conn = new OleDbConnection(conn_string); //set up connection
            conn.Open(); //open connection

            for (int j = 0; j < farmTable.Length; j++) //run through all table
            {
                String str = "";
                q = $"SELECT * FROM [{farmTable[j]}]"; //set up query
                String table = farmTable[j].ToString(); //set table name
                OleDbCommand cmd = new OleDbCommand(q, conn); //set command
                using (OleDbDataReader reader = cmd.ExecuteReader()) //read data from commend
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++) //reads and store data of same table
                        {
                            str += reader[i].ToString();
                            if (i < reader.FieldCount - 1) // use "," as seperator between data
                            {
                                str += ",";
                            }
                        }
                        str += " "; //ends every line with spacing
                    }

                    switch (table) //read from tables, store into place holder
                    {
                        case "Cows":
                            string[] content1 = str.Trim().Split(' ');
                            foreach (String line in content1)
                            {
                                string[] trimmed = line.Trim().Split(',');
                                int ID = int.Parse(trimmed[0].Trim());
                                double amountOfWater = double.Parse(trimmed[1].Trim());
                                double dailyCost = double.Parse(trimmed[2].Trim());
                                int weight = int.Parse(trimmed[3].Trim());
                                int age = int.Parse(trimmed[4].Trim());
                                string colour = trimmed[5].Trim();
                                double amountOfMilk = double.Parse(trimmed[6].Trim());
                                bool isJersy = bool.Parse(trimmed[7].Trim());
                                String type = farmTable[j].ToString();

                                Livestock farmAnimal;
                                farmAnimal = new Cow(ID, type, amountOfWater, dailyCost, weight, age, colour, amountOfMilk, isJersy);
                                allAnimals.Add(ID, farmAnimal);
                            }
                            break;
                        case "Dogs":
                            string[] content2 = str.Trim().Split(' ');
                            foreach (String line in content2)
                            {
                                string[] trimmed = line.Trim().Split(',');
                                int ID = int.Parse(trimmed[0].Trim());
                                double amountOfWater = double.Parse(trimmed[1].Trim());
                                double weight = double.Parse(trimmed[2].Trim());
                                int age = int.Parse(trimmed[3].Trim());
                                string colour = trimmed[4].Trim();
                                double dailyCost = double.Parse(trimmed[5].Trim());
                                String type = farmTable[j].ToString();

                                Livestock farmAnimal;
                                farmAnimal = new Dog(ID, type, amountOfWater, weight, age, colour, dailyCost);
                                allAnimals.Add(ID, farmAnimal);
                            }
                            break;
                        case "Goats":
                            string[] content3 = str.Trim().Split(' ');
                            foreach (String line in content3)
                            {
                                string[] trimmed = line.Trim().Split(',');
                                int ID = int.Parse(trimmed[0].Trim());
                                double amountOfWater = double.Parse(trimmed[1].Trim());
                                double dailyCost = double.Parse(trimmed[2].Trim());
                                double weight = double.Parse(trimmed[3].Trim());
                                int age = int.Parse(trimmed[4].Trim());
                                string colour = trimmed[5].Trim();
                                double amountOfMilk = double.Parse(trimmed[6].Trim());
                                String type = farmTable[j].ToString();

                                Livestock farmAnimal;
                                farmAnimal = new Goat(ID, type, amountOfWater, dailyCost, weight, age, colour, amountOfMilk);
                                allAnimals.Add(ID, farmAnimal);
                            }
                            break;
                        case "Sheep":
                            string[] content4 = str.Trim().Split(' ');
                            foreach (String line in content4)
                            {
                                string[] trimmed = line.Trim().Split(',');
                                int ID = int.Parse(trimmed[0].Trim());
                                double amountOfWater = double.Parse(trimmed[1].Trim());
                                double dailyCost = double.Parse(trimmed[2].Trim());
                                double weight = double.Parse(trimmed[3].Trim());
                                int age = int.Parse(trimmed[4].Trim());
                                string colour = trimmed[5].Trim();
                                double amountOfWool = double.Parse(trimmed[6].Trim());
                                String type = farmTable[j].ToString();

                                Livestock farmAnimal;
                                farmAnimal = new Sheep(ID, type, amountOfWater, dailyCost, weight, age, colour, amountOfWool);
                                allAnimals.Add(ID, farmAnimal);
                            }
                            break;
                        case "Commodity Prices":
                            string temp = Regex.Replace(str, "[^0-9.,]", "").Trim(','); //remove everything except numbers and "." ","
                            string[] content5 = temp.Split(',');
                            RatesAndPrices.milkPriceGoat = double.Parse(content5[0]);
                            RatesAndPrices.woolPriceSheep = double.Parse(content5[1]);
                            RatesAndPrices.waterPrice = double.Parse(content5[2]);
                            RatesAndPrices.tax = double.Parse(content5[3]);
                            RatesAndPrices.jersyCowTax = double.Parse(content5[4]);
                            RatesAndPrices.milkPriceCow = double.Parse(content5[5]);
                            break;
                    }
                }
            }
            conn.Close(); //close connection
        }

        public void profitAsc_Click(object sender, EventArgs e) //Report 8
        {
            try //try catching errors
            {
                TextWriter tw = new StreamWriter("C:/Users/jinki/Documents/GitHub/Wintec-Assignments/Livestock Management Application/Profit List.txt", true);
                tw.WriteLine(livestockTree.profitInOrder().ToString());
                tw.Close();
                MessageBox.Show(livestockTree.profitInOrder(),"Profit List(ASC)"); //get data from binary tree
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message); //show error type
            }
        }

        private Livestock bst() //Report 8 - store hash table data into binary tree
        {
            List<Livestock> livestocksList = allAnimals.Values.ToList(); //retrieve data from hashtable and store into list
            Livestock livestockTree = new Livestock(); 
            for (int i = 0; i < livestocksList.Count; i++)
            {
                if (livestocksList[i].getType() == "Dogs") { }
                else
                {
                    livestockTree.insertToTree(livestocksList[i]);
                }
            }
            return livestockTree;
        }

        /*Report 11: The user enter a threshold (number of years), 
         * and the program displays the ratio of the number of animal farm 
         * where the age is above this threshold.
         */
        private void thresholdButton_Click(object sender, EventArgs e)
        {
            try //try catching errors
            {
                if (thresholdBox.Text == null || thresholdBox.TextLength == 0) //alert when input is empty
                {
                    MessageBox.Show("ID cannot be empty.");
                }
                else if (int.TryParse(thresholdBox.Text, out int isNumeric)) //check if input is numeric
                {
                    ratioAboveThreshold(int.Parse(thresholdBox.Text)); //run calculation
                }
                else { MessageBox.Show("Please enter threshold in number."); } //alert if not numeric
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message); //show error type
            }
        }
    

        private void ratioAboveThreshold(int age) //Report 11 - threshold calculation
        {
            double count = 0;
            foreach (var animal in allAnimals)
            {
                if (allAnimals.TryGetValue(animal.Key, out livestock))
                {
                    if (livestock.getAge() > age) //count if age is > then input age
                    {
                        count++;
                    }
                }
            }
            MessageBox.Show($"The ratio of the number of animal farm\nwhere the age is above " +
                $"{thresholdBox.Text} is: " + Math.Round((count/allAnimals.Count), 2).ToString(), 
                $"Ratio of age above {thresholdBox.Text}");
        }
    }
}
