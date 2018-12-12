using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ChessMath.UI.Components
{
    public class CellLabel
    {
        public int CellWidth;
        public int CellHeight;
        public int CellNumber;
        public Label LabelObject;
        public CellLabel()
        {
            LabelObject = new Label();
            LabelObject.Font = new Font("Arial", 25);
            LabelObject.Width = CellWidth;
            LabelObject.Height = CellHeight;
            LabelObject.TextAlign = ContentAlignment.MiddleCenter;
            LabelObject.BackColor = Color.Black;
            LabelObject.BorderStyle = BorderStyle.FixedSingle;
            LabelObject.Enabled = true;
            

            //LabelObject.Click += GridCellInsertNext_Click;
        }

        public void GridCellInsertNext_Click(object sender, EventArgs e)
        {
            Label clickedLabel = (Label)sender;
            if (clickedLabel.Text == string.Empty)
            {
                clickedLabel.Text = GameArea.numbers.Min().ToString();
                GameArea.numbers.Remove(GameArea.numbers.Min());
            }
            else
            {
                if (int.TryParse(clickedLabel.Text, out int number))
                {
                    if (number + 1 == GameArea.numbers.Min())
                    {
                        clickedLabel.Text = string.Empty;
                        GameArea.numbers.Add(number);
                    }
                    else
                    {
                        // DO some error thing 
                    }
                }
            }
        }

    }
}
