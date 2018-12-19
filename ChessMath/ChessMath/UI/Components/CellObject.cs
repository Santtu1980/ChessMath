﻿using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ChessMath.Maths;
using ChessMath.UI.Helpers;

namespace ChessMath.UI.Components
{
    public class CellLabel
    {
        public Point CellCoordinate { get; set; }
        public int? NumberSetInCell {get;set;}
        public Label LabelObject { get; }
        public CellLabel(int cellSize)
        {
            LabelObject = new Label();
            LabelObject.Font = new Font("Arial", 15);
            LabelObject.Width = cellSize;
            LabelObject.Height = cellSize;
            LabelObject.TextAlign = ContentAlignment.MiddleCenter;
            LabelObject.BackColor = Color.Wheat;
            LabelObject.BorderStyle = BorderStyle.FixedSingle;
            LabelObject.Enabled = true;
            
            LabelObject.Click += GridCellInsertNext_Click;

            NumberSetInCell = null;
        }

        public void GridCellInsertNext_Click(object sender, EventArgs e)
        {
            Label clickedLabel = (Label)sender;
            if (clickedLabel.Text == string.Empty)
            {
                // Check if possible
                if (PossibleCells.CheckIfPossible(clickedLabel, ))
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
