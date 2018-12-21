using ChessMath.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using ChessMath.UI.Components;

namespace ChessMath.Maths
{
    public static class PossibleCells
    {
        public static List<ExtentedLabel> GetPossibleCells(Point clickedCell, UI.Helpers.GridHelper.GridForm gridForm, TreadShape treadShape)
        {
            switch (treadShape)
            {
                case TreadShape.closestEdges:
                    return ClosestEdgesIsPossible(clickedCell, gridForm, treadShape);

                case TreadShape.closestNotCorners:
                    break;
                default:
                    break;
            }
            return new List<ExtentedLabel>();
        }
        private static List<ExtentedLabel> ClosestEdgesIsPossible(Point cellToCompare, UI.Helpers.GridHelper.GridForm gridForm, TreadShape treadShape)
        {
            var allCells = GameArea.Cells;
            //possible cells
            /*
            X|X|X
            X|o|X
            X|X|X
             */
            // Surrounding cells
            var possibleCells = GameArea.Cells.Where(
                x => x.CellCoordinate.X >= cellToCompare.X - 1 && 
                     x.CellCoordinate.X <= cellToCompare.X + 1 &&
                     x.CellCoordinate.Y >= cellToCompare.Y - 1 &&
                     x.CellCoordinate.Y >= cellToCompare.Y + 1);
            // Remove cells outside grid
            possibleCells = possibleCells.Where(x => x.CellCoordinate.X >= 0 && x.CellCoordinate.Y >= 0);
            possibleCells = possibleCells.Where(x => x.CellCoordinate.X <= 4 && x.CellCoordinate.Y <= 4); // TODO Check this and get the width and height of the gamearea

            return possibleCells.ToList();
        }
    }
}
