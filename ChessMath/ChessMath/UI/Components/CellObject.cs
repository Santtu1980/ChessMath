using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ChessMath.UI.Components
{
    public class CellLabel : Label
    {
        public int CellWidth { get; set; }
        public int CellHeight { get; set; }
        public int CellNumber { get; set; }
        public Label LabelObject { get; set; }
        public CellLabel()
        {
            Font = new Font("Arial", 25);
            Width = CellWidth;
            Height = CellHeight;
            TextAlign = ContentAlignment.MiddleCenter;
            BackColor = Color.WhiteSmoke;
            BorderStyle = BorderStyle.FixedSingle;
            Enabled = true;

            Click += GridCellInsertNext_Click;
        }

        public void GridCellInsertNext_Click(object sender, EventArgs e)
        {
            Label clickedLabel = (Label)sender;
            if (clickedLabel.Text == string.Empty)
            {
                clickedLabel.Text = numbers.Min().ToString();
                numbers.Remove(numbers.Min());
            }
            else
            {
                if (int.TryParse(clickedLabel.Text, out int number))
                {
                    if (number + 1 == numbers.Min())
                    {
                        clickedLabel.Text = string.Empty;
                        numbers.Add(number);
                    }
                    else
                    {
                        // Start reddish background for a 2 secs
                        Thread thread1 = new Thread(ImpossibleClick);
                        thread1.Start(sender);
                    }
                }
            }
        }

    }
}
