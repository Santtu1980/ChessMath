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
using ChessMath.UI.Helpers;

namespace ChessMath.UI
{
    public partial class GameArea : Form
    {
        public GameArea()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            int gridWidth = 4;
            int gridHeight = 4;
            InitializeMainForm(gridWidth, gridHeight);
            CreateGrid(gridWidth, gridHeight, GridHelper.GridForm.Square);
            CreateHelperLabel(gridWidth, gridHeight);

        }

        private void CreateHelperLabel(int gridWidth, int gridHeight)
        {
            Label helperLabel = new Label();
            helperLabel.Top = 10;
            helperLabel.Left = gridWidth * GridHelper.CELLWIDTH + 10;
            helperLabel.Height = Height - 2 * 10;
            helperLabel.Text = "ku";
            Controls.Add(helperLabel);
        }


        private void InitializeMainForm(int width, int height)
        {
            Width = width * GridHelper.CELLWIDTH + 200 + 20;
            Height = height * GridHelper.CELLHEIGHT + 20 + 40;
            Text = "Chess Math";
            BackColor = Color.White;
        }

        public static void SetHelperText()
        {
            
        }
        public void CreateGrid(int width, int height, GridHelper.GridForm form)
        {
            var cells = GridHelper.CreateGrid(width, height, form);
            if(cells.Any())
                Controls.AddRange(cells.Select(x => x.LabelObject).ToArray());
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
