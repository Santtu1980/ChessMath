using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMath.UI.Components;

namespace ChessMath.UI.Helpers
{
    public static class GridHelper
    {
        public static int CELLHEIGHT = 50;
        public static int CELLWIDTH = 50;
        public static List<int> numbers = new List<int>();
        public static int gridWidth;
        public static int gridHeight;

        public enum GridForm
        {
            Square
        }

        public static List<ExtentedLabel> CreateGrid(int width, int height, GridForm form)
        {
            int numberOfCells = 0;
            List<ExtentedLabel> cells = new List<ExtentedLabel>();
            for(int row = 0; row < width; row++)
            {
                for(int column = 0; column < height; column++)
                {
                    var cell = new ExtentedLabel(50);
                    cell.Top = row * CELLHEIGHT + 10;
                    cell.Left = column * CELLHEIGHT + 10;
                    cell.CellCoordinate = new Point(row, column);
                    cells.Add(cell);

                    numberOfCells++;
                    numbers.Add(numberOfCells);
                }
            }

            return cells;
        }
    }
}
