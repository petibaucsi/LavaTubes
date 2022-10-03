using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LavaTubes
{
    public partial class Main : Form
    {
        String input;
        String solution;

        int rowNumber = 0;
        int columnNumber = 0;
        int riskFactor = 0;
        
        List<Coordinate> map = new List<Coordinate>();
        
        //At start we put the Main case in the input field
        public Main()
        {
            InitializeComponent();
            TxtInput.Text = "2199943210\n" +
                            "3987894921\n" +
                            "9856789892\n" +
                            "8767896789\n" +
                            "9899965678";
        }

        //The button click event does the validation of the input string and solves the map if valid.
        private void BtnSolve_Click(object sender, EventArgs e)
        {
            if (ValidateInput() == true)
            {
                FindLowPoints();
                SizeOfThreeLargest();

                TxtSolution.Text = solution;

            }
        }

        //Input validation
        //Checks if each character is an integer between 0 and 9, and the are the same amount of integers in each row.
        //Informs the user if the input is not valid.
        //Also populates the Map.
        //Returns true if the input is valid.
        public bool ValidateInput() 
        {
            map.Clear();
            TxtSolution.Text = "";
            input = TxtInput.Text;
            int mapWidth = 0;
            bool valid = true;
            string[] rows = input.Split("\n");
            rowNumber = rows.Length;


            for (int i = 0; i < rows.Length; i++)
            {
                if (valid == false)
                {
                    break;
                }

                char[] columns = rows[i].Trim().ToCharArray();

                columnNumber = columns.Length;

                if (i == 0)
                {
                    mapWidth = columns.Length;
                }
                else
                {
                    if (mapWidth != columns.Length)
                    {
                        MessageBox.Show("Input must be the same amount of integers in each row!");
                        valid = false;
                        break;
                    }
                }

                for (int j = 0;j< columns.Length; j++)
                {
                    try
                    {
                        map.Add(new Coordinate(i, j, Int32.Parse(columns[j].ToString())));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Input must be integers between 0 and 9!");
                        valid = false;
                        break;
                    }
                }
            }
            return valid;
        }

        //Checks each adjacent coordinate. If all of them are higher than the examined coordinate (number of adjacent = number of higher), its a lowpoint.
        //If it is a lowpoint, mark every adjacent as Basin of this. Maintain the size of the basin.
        //Creates the solution string and puts it into the output field of the GUI.
        public void FindLowPoints()
        {
            TxtSolution.Text = "";
           solution = "The lowpoints are: \n";
            riskFactor = 0;
            int numOfAdjacent = 0;
            int numOfHigher = 0;

            for (int k = 0; k < rowNumber; k++)
            {

                for (int l = 0; l < columnNumber; l++)
                {
                    numOfAdjacent = 0;
                    numOfHigher = 0;

                    Coordinate coordinate = map.FirstOrDefault(c => c.x == k && c.y == l);

                    Coordinate coordinateUp = map.FirstOrDefault(c => c.x == k - 1 && c.y == l);
                    if (coordinateUp != null)
                    {
                        numOfAdjacent += 1;

                        if (coordinate.height < coordinateUp.height)
                        {
                            numOfHigher += 1;
                        }
                    }
                    Coordinate coordinateDown = map.FirstOrDefault(c => c.x == k + 1 && c.y == l);
                    if (coordinateDown != null)
                    {
                        numOfAdjacent += 1;
                        if (coordinate.height < coordinateDown.height)
                        {
                            numOfHigher += 1;
                        }
                    }
                    Coordinate coordinateLeft = map.FirstOrDefault(c => c.x == k && c.y == l - 1);
                    if (coordinateLeft != null)
                    {
                        numOfAdjacent += 1;
                        if (coordinate.height < coordinateLeft.height)
                        {
                            numOfHigher += 1;
                        }
                    }
                    Coordinate coordinateRight = map.FirstOrDefault(c => c.x == k && c.y == l + 1);
                    if (coordinateRight != null)
                    {
                        numOfAdjacent += 1;
                        if (coordinate.height < coordinateRight.height)
                        {
                            numOfHigher += 1;
                        }
                    }

                    //if all adjacent coordinate is higher then mark it as lowpoint. Marks every adjacent as Basin of this.
                    if (numOfHigher == numOfAdjacent)
                    {


                        coordinate.isLowpoint = true;
                        if (coordinateDown != null && coordinateDown.height != 9) { coordinateDown.BasinOf = coordinate; }
                        if (coordinateUp != null && coordinateUp.height != 9) { coordinateUp.BasinOf = coordinate; }
                        if (coordinateLeft != null && coordinateLeft.height != 9) { coordinateLeft.BasinOf = coordinate; }
                        if (coordinateRight != null && coordinateRight.height != 9) { coordinateRight.BasinOf = coordinate; }
                        coordinate.BasinOf = coordinate;
                        coordinate.sizeOfBazin += 1;

                        coordinate.CalculatBasin(map);

                        solution += "Row " + (k + 1).ToString() + "., column " + (l + 1).ToString() + ". Value: " + coordinate.height.ToString() + "., SizeOfBasin: " + coordinate.sizeOfBazin.ToString() + "\n";
                        riskFactor += (coordinate.height + 1);

                    }
                }
            }

            solution += "\nRisk level of the area = " + riskFactor +"\n";
        }

        //Calculates the multiplied value. Sorts the map by the size of its basin, then multiplies the first three
        public void SizeOfThreeLargest()
        {
            List<Coordinate> SortedList = map.OrderByDescending(o => o.sizeOfBazin).ToList();

            solution += "Multiplied value of the the three largest basin: " + (SortedList.ElementAt(0).sizeOfBazin * SortedList.ElementAt(1).sizeOfBazin * SortedList.ElementAt(2).sizeOfBazin).ToString();
           
        }
        
        //Default test cases in a combobox
        private void comboTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboTest.Text == "Main case")
            {
                TxtInput.Text = "2199943210\n" +
                                "3987894921\n" +
                                "9856789892\n" +
                                "8767896789\n" +
                                "9899965678";
            }
            else if (comboTest.Text == "Nontrivial case (same number)")
            {
                TxtInput.Text = "5555555555\n" +
                                "5555555555\n" +
                                "5555555555\n" +
                                "5555555555\n" +
                                "5555555555";
            }
            else if (comboTest.Text == "Nontrivial case (one line)")
            {
                TxtInput.Text = "1235953124987654";
            }
            else if (comboTest.Text == "Simple case")
            {
                TxtInput.Text = "55555\n" +
                                "55555\n" +
                                "55155\n" +
                                "55555\n" +
                                "55555";
            }
            else if (comboTest.Text == "Error case (letter)")
            {
                TxtInput.Text = "151353452\n" +
                                "2146f7631\n" +
                                "554353234\n" +
                                "72671hg66\n" +
                                "462435465";
            }
            else if (comboTest.Text == "Error case (different length)")
            {
                TxtInput.Text = "2199943210\n" +
                                "3987894921\n" +
                                "985678945892\n" +
                                "8767896789\n" +
                                "9899965678";
            }
        }
    }
}
