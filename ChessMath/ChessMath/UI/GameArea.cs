using ChessMath.UI.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessMath.UI
{
    public partial class GameArea : Form
    {
        public GameArea()
        {
            InitializeComponent();
        }
        private static int CELLHEIGHT = 50;
        private static int CELLWIDTH = 50;
        private int numberOfCells = 0;
        private static List<int> numbers = new List<int>();

        private void InitializeComponent()
        {
            int gridWidth = 4;
            int gridHeight = 4;
            InitializeMainForm(gridWidth, gridHeight);
            CreateGrid(gridWidth, gridHeight);

        }
        private void InitializeMainForm(int width, int height)
        {
            Width = width * CELLWIDTH + 200 + 20;
            Height = height * CELLHEIGHT + 20 + 40;
            Text = "Chess Math";
            BackColor = Color.White;
        }
        public void CreateGrid(int width, int height)
        {
            for (int row = 0; row < width; row++)
            {
                for (int column = 0; column < height; column++)
                {
                    var cell = new CellLabel();
                    cell.Top = row * CELLHEIGHT + 10;
                    cell.Left = column * CELLHEIGHT + 10;
                    cell.CellNumber = row * column;
                    Controls.Add(cell);

                    numberOfCells++;
                    numbers.Add(numberOfCells);
                }
            }
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

        private static Label _clickedLabel;
        delegate void ImpossibleClickDelegate(Color color);
        public void ImpossibleClick(object sender)
        {
            if (sender.GetType() != typeof(Label)) return;
            _clickedLabel = (Label)sender;
            int colorPart = 510;

            while(colorPart > 1)
            {
                colorPart--;
                if (colorPart > 255)
                    SetColor(Color.FromArgb(255, 255 - (colorPart - 255), 255 - (colorPart - 255)));
                else
                    SetColor(Color.FromArgb(255, colorPart, colorPart));
            }
            SetColor(Color.WhiteSmoke);
        }

        private void SetColor(Color color)
        {
            if (_clickedLabel.InvokeRequired)
            {
                ImpossibleClickDelegate d = new ImpossibleClickDelegate(SetColor);
                Invoke(d, new object[] { color });
            }
            else
            {
                _clickedLabel.BackColor = color;
            }
        }
    }
}
