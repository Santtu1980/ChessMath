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
using ChessMath.Maths;
using ChessMath.UI.Helpers;

namespace ChessMath.UI
{
    public partial class GameArea : Form
    {
        public static Label HelperLabel = new Label();
        public static GridHelper.GridForm SelectedGridForm { get; set; }
        public static TreadShape SelectedTreadShape { get; set; }
        public static List<ExtentedLabel> Cells { get; set; }

        public GameArea()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            int gridWidth = 4;
            int gridHeight = 4;
            SelectedGridForm = GridHelper.GridForm.Square;
            SelectedTreadShape = TreadShape.upIsBetter;
            InitializeMainForm(gridWidth, gridHeight);
            CreateGrid(gridWidth, gridHeight, SelectedGridForm, SelectedTreadShape);
            CreateHelperLabel(gridWidth, gridHeight);

        }

        private void CreateHelperLabel(int gridWidth, int gridHeight)
        {
            HelperLabel.Top = 10;
            HelperLabel.Left = gridWidth * GridHelper.CELLWIDTH + 10;
            HelperLabel.Height = Height - 2 * 10;
            HelperLabel.Text = "Press start";
            Controls.Add(HelperLabel);
        }


        private void InitializeMainForm(int width, int height)
        {
            Width = width * GridHelper.CELLWIDTH + 200 + 20;
            Height = height * GridHelper.CELLHEIGHT + 20 + 40;
            Text = "Chess Math";
            BackColor = Color.White;
        }

        public static void SetHelperText(string text)
        {
            HelperLabel.Text = "Next:" + text;
        }

        public void CreateGrid(int width, int height, GridHelper.GridForm form, TreadShape treadShape)
        {
            Cells = GridHelper.CreateGrid(width, height, form, treadShape);
            if (Cells.Any())
            {
                Cells = SetPossibleCellsToCell(Cells);
                Controls.AddRange(Cells.Select(x => x).ToArray());
            }
        }

        private List<ExtentedLabel> SetPossibleCellsToCell(List<ExtentedLabel> cells)
        {
            foreach (var cell in cells)
            {
                cell.possibleCells = PossibleCells.GetPossibleCells(cell.CellCoordinate, SelectedGridForm, SelectedTreadShape);
            }

            return cells;
        }

        private static ExtentedLabel _clickedLabel;

        delegate void ImpossibleClickDelegate(Color color);
        public void ImpossibleCellClicked(object sender)
        {
            // Start reddish background for a 2 secs
            Thread thread1 = new Thread(ImpossibleClick);
            thread1.Start(sender);
        }
        public void ImpossibleClick(object sender)
        {
            if (sender.GetType() != typeof(ExtentedLabel)) return;
            _clickedLabel = (ExtentedLabel)sender;
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
                ImpossibleClickDelegate d = SetColor;
                Invoke(d, color);
            }
            else
            {
                _clickedLabel.LabelObject.BackColor = color;
            }
        }
    }
}
