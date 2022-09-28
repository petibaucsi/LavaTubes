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
        String _input;
        String _solution;

        int _rowNumber = 0;
        int _columnNumber = 0;
        int _riskFactor = 0;
        
        List<Coordinate> _map = new List<Coordinate>();
        
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
                Solve();
            }
        }

        //Input validation
        //Checks if each character is an integer between 0 and 9, and the are the same amount of integers in each row.
        //Informs the user if the input is not valid.
        //Also populates the Map.
        //Returns true if the input is valid.
        public bool ValidateInput() 
        {
            _map.Clear();
            TxtSolution.Text = "";
            _input = TxtInput.Text;
            int _mapWidth = 0;
            bool _valid = true;
            string[] rows = _input.Split("\n");
            _rowNumber = rows.Length;


            for (int i = 0; i < rows.Length; i++)
            {
                if (_valid == false)
                {
                    break;
                }

                char[] columns = rows[i].Trim().ToCharArray();

                _columnNumber = columns.Length;

                if (i == 0)
                {
                    _mapWidth = columns.Length;
                }
                else
                {
                    if (_mapWidth != columns.Length)
                    {
                        MessageBox.Show("Input must be the same amount of integers in each row!");
                        _valid = false;
                        break;
                    }
                }

                for (int j = 0;j< columns.Length; j++)
                {
                    try
                    {
                        _map.Add(new Coordinate(i, j, Int32.Parse(columns[j].ToString())));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Input must be integers between 0 and 9!");
                        _valid = false;
                        break;
                    }
                }
            }
            return _valid;
        }

        //Checks each adjacent coordinate. If all of them are higher than the examined coordinate (number of adjacent = number of higher), its a lowpoint.
        //Creates the solution string and puts it into the output field of the GUI.
        public void Solve()
        {
            
            _solution = "The lowpoints are: \n";
            _riskFactor = 0;
            int numOfAdjacent = 0;
            int numOfHigher = 0;

            for (int k = 0; k < _rowNumber; k++)
            {

                for (int l = 0; l < _columnNumber; l++)
                {
                     numOfAdjacent = 0;
                     numOfHigher = 0;

                    Coordinate coordinate = _map.FirstOrDefault(c => c.x == k && c.y == l);

                    Coordinate coordinateUp = _map.FirstOrDefault(c => c.x == k-1 && c.y == l);
                    if (coordinateUp != null)
                    {
                        numOfAdjacent += 1;

                        if (coordinate.height < coordinateUp.height)
                        {
                            numOfHigher += 1;
                        }
                    }
                    Coordinate coordinateDown = _map.FirstOrDefault(c => c.x == k+1 && c.y == l);
                    if (coordinateDown != null)
                    {
                        numOfAdjacent += 1;
                        if (coordinate.height < coordinateDown.height)
                        {
                            numOfHigher += 1;
                        }
                    }
                    Coordinate coordinateLeft = _map.FirstOrDefault(c => c.x == k && c.y == l-1);
                    if (coordinateLeft != null)
                    {
                        numOfAdjacent += 1;
                        if (coordinate.height < coordinateLeft.height)
                        {
                            numOfHigher += 1;
                        }
                    }
                    Coordinate coordinateRight = _map.FirstOrDefault(c => c.x == k && c.y == l+1);
                    if (coordinateRight != null)
                    {
                        numOfAdjacent += 1;
                        if (coordinate.height < coordinateRight.height)
                        {
                            numOfHigher += 1;
                        }
                    }

                    if (numOfHigher == numOfAdjacent)
                    {
                        _solution += "Row " + (k + 1).ToString() + "., column " + (l + 1).ToString() + ". Value: " + coordinate.height.ToString() + "\n";
                        _riskFactor += (coordinate.height + 1);
                    }
                    
                }
            }
            TxtSolution.Text = _solution + "\nRisk level of the area = " + _riskFactor;
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
            else if (comboTest.Text == "Nontrivial case")
            {
                TxtInput.Text = "5555555555\n" +
                                "5555555555\n" +
                                "5555555555\n" +
                                "5555555555\n" +
                                "5555555555";
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
