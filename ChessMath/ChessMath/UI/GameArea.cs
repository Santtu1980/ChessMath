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
        public static List<int> numbers = new List<int>();

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
                    cell.CellHeight = 50;
                    cell.CellWidth = 50;
                    cell.LabelObject = new Label();
                    cell.LabelObject.Top = row * CELLHEIGHT + 10;
                    cell.LabelObject.Left = column * CELLHEIGHT + 10;
                    cell.CellNumber = row * column;
                    Controls.Add(cell.LabelObject);

                    numberOfCells++;
                    numbers.Add(numberOfCells);
                }
            }
        }


        private static CellLabel _clickedLabel;
        delegate void ImpossibleClickDelegate(Color color);
        public void ImpossibleCellClicked(object sender)
        {
            // Start reddish background for a 2 secs
            Thread thread1 = new Thread(ImpossibleClick);
            thread1.Start(sender);
        }
        public void ImpossibleClick(object sender)
        {
            if (sender.GetType() != typeof(CellLabel)) return;
            _clickedLabel = (CellLabel)sender;
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
            if (_clickedLabel.LabelObject.InvokeRequired)
            {
                ImpossibleClickDelegate d = new ImpossibleClickDelegate(SetColor);
                Invoke(d, new object[] { color });
            }
            else
            {
                _clickedLabel.LabelObject.BackColor = color;
            }
        }
    }
}
