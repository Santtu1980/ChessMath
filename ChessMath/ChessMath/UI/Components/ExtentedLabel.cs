using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ChessMath.Maths;
using ChessMath.UI.Helpers;

namespace ChessMath.UI.Components
{
    public class ExtentedLabel : Label
    {
        public Point CellCoordinate { get; set; }
        public int? NumberSetInCell {get;set;}
        public Label LabelObject { get; }
        public bool LeftEdge {get; set;} = false;
        public bool RightEdge {get; set;} = false;
        public bool TopEdge {get; set;} = false;
        public bool BottomEdge { get; set;} = false;
        public List<ExtentedLabel> possibleCells { get; set; } = new List<ExtentedLabel>();

        public ExtentedLabel(int cellSize)
        {
            Font = new Font("Arial", 15);
            Width = cellSize;
            Height = cellSize;
            TextAlign = ContentAlignment.MiddleCenter;
            BackColor = Color.Wheat;
            BorderStyle = BorderStyle.FixedSingle;
            Enabled = true;
            Text = string.Empty;
            
            Click += GridCellInsertNext_Click;

            NumberSetInCell = null;

        }

        public void GridCellInsertNext_Click(object sender, EventArgs e)
        {
            ExtentedLabel clickedLabel = (ExtentedLabel)sender;
            if (clickedLabel.Text == string.Empty)
            {
                var biggestCell = GetBiggestNumber();
                if (biggestCell == null || biggestCell.possibleCells.Any(x => x.CellCoordinate == clickedLabel.CellCoordinate))
                {
                    clickedLabel.Text = GridHelper.numbers.Min().ToString();
                    GridHelper.numbers.Remove(GridHelper.numbers.Min());

                    //DEbug
                    //set helper text
                    if(biggestCell != null)
                        GameArea.SetHelperText(string.Join("|", biggestCell.possibleCells.Select(i => i.CellCoordinate.X + "," + i.CellCoordinate.Y).ToArray()));

                }
                else
                    NotPossibleClick(clickedLabel);
            }
            else
            {
                RemoveLastNumber(clickedLabel);
            }
            GameArea.SetHelperText(GridHelper.numbers.Min().ToString());
        }

        private ExtentedLabel GetBiggestNumber()
        {
            var filledCells = GameArea.Cells.Where(x => x != null && x.Text != string.Empty);
            if (!filledCells.Any()) return null;
            int biggestNumber = filledCells.Max(x => int.Parse(x.Text));
            var biggest = filledCells.First(x => x.Text == biggestNumber.ToString());
            return biggest;
        }

        private void NotPossibleClick(ExtentedLabel clickedLabel)
        {
            MessageBox.Show("Not possible: " + clickedLabel.CellCoordinate.X + "," + clickedLabel.CellCoordinate.Y);
        }

        private void RemoveLastNumber(ExtentedLabel clickedLabel)
        {
            if(int.TryParse(clickedLabel.Text, out int number))
            {
                if(number + 1 == GridHelper.numbers.Min())
                {
                    clickedLabel.Text = string.Empty;
                    GridHelper.numbers.Add(number);
                }
                else
                {
                    // DO some error thing 
                }
            }
        }
    }
}
