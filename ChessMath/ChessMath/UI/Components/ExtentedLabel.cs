using System;
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
        public ExtentedLabel(int cellSize)
        {
            Font = new Font("Arial", 15);
            Width = cellSize;
            Height = cellSize;
            TextAlign = ContentAlignment.MiddleCenter;
            BackColor = Color.Wheat;
            BorderStyle = BorderStyle.FixedSingle;
            Enabled = true;
            
            Click += GridCellInsertNext_Click;

            NumberSetInCell = null;
        }

        public void GridCellInsertNext_Click(object sender, EventArgs e)
        {
            ExtentedLabel clickedLabel = (ExtentedLabel)sender;
            if (clickedLabel.Text == string.Empty)
            {
                // Check if possible
                if (PossibleCells.CheckIsPossible(clickedLabel.CellCoordinate, GameArea.SelectedGridForm, GameArea.SelectedTreadShape))
                {
                    clickedLabel.Text = GridHelper.numbers.Min().ToString();
                    GridHelper.numbers.Remove(GridHelper.numbers.Min());
                }
                else
                    MessageBox.Show("Not possible");
            }
            else
            {
                if (int.TryParse(clickedLabel.Text, out int number))
                {
                    if (number + 1 == GridHelper.numbers.Min())
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
            GameArea.SetHelperText(GridHelper.numbers.Min().ToString());
        }

    }
}
