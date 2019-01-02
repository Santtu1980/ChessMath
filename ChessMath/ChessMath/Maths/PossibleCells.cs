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
                    return ClosestEdgesIsPossible(clickedCell);

                case TreadShape.closestNotCorners:
                    return ClosestEdgesNotCornersIsPossible(clickedCell);
                case TreadShape.chessKnight:
                    return ChessKnightIsPossible(clickedCell);
                case TreadShape.closestCorners:
                    return ClosestCornersIsPossible(clickedCell);
                case TreadShape.upIsBetter:
                    return UpIsBetterIsPossible(clickedCell);
            }
            return new List<ExtentedLabel>();
        }

        private static List<ExtentedLabel> RemoveCellsOutSideGameArea(List<ExtentedLabel> cells)
        {
            var possibleCells = cells.Where(x => x.CellCoordinate.X >= 0 && x.CellCoordinate.Y >= 0);
            return possibleCells.Where(x => x.CellCoordinate.X <= 3 && x.CellCoordinate.Y <= 3).ToList(); // TODO Check this and get the width and height of the gamearea
        }

        //possible cells
        /*
        X|X|X
        X|o|X
        X|X|X
         */
        private static List<ExtentedLabel> ClosestEdgesIsPossible(Point cellToCompare)
        {
            // Surrounding cells
            var possibleCells = GameArea.Cells.Where(
                x => x.CellCoordinate.X >= cellToCompare.X - 1 && 
                     x.CellCoordinate.X <= cellToCompare.X + 1 &&
                     x.CellCoordinate.Y >= cellToCompare.Y - 1 &&
                     x.CellCoordinate.Y <= cellToCompare.Y + 1);
            // Remove cells outside grid
            return RemoveCellsOutSideGameArea(possibleCells.ToList());
        }

        //possible cells
        /*
         |X| 
        X|o|X
         |X| 
         */
        private static List<ExtentedLabel> ClosestEdgesNotCornersIsPossible(Point cellToCompare)
        {
            // Surrounding cells
            var possibleCells = GameArea.Cells.Where(
                x => x.CellCoordinate.X >= cellToCompare.X - 1 && x.CellCoordinate.X <= cellToCompare.X + 1 && x.CellCoordinate.Y == cellToCompare.Y ||
                     x.CellCoordinate.X == cellToCompare.X && x.CellCoordinate.Y >= cellToCompare.Y - 1 && x.CellCoordinate.Y <= cellToCompare.Y + 1);

            // Remove cells outside grid
            return RemoveCellsOutSideGameArea(possibleCells.ToList());
        }
        //possible cells
        /*
        X|X|X
         |o|
         |X| 
         */
        private static List<ExtentedLabel> UpIsBetterIsPossible(Point cellToCompare)
        {
            // Surrounding cells
            var possibleCells = GameArea.Cells.Where(
                x => (x.CellCoordinate.Y == cellToCompare.Y && x.CellCoordinate.X == cellToCompare.X + 1) ||
                     (x.CellCoordinate.Y >= cellToCompare.Y - 1 && x.CellCoordinate.Y <= cellToCompare.Y + 1 && x.CellCoordinate.X == cellToCompare.X - 1));

            // Remove cells outside grid
            return RemoveCellsOutSideGameArea(possibleCells.ToList());
        }
        //possible cells
        /*
         |X| |X|
        X| | | |X
         | |o| | 
        X| | | |X 
         |X| |X|
         */
        private static List<ExtentedLabel> ChessKnightIsPossible(Point cellToCompare)
        {
            // Surrounding cells
            var possibleCells = GameArea.Cells.Where(
                x => (x.CellCoordinate.X == cellToCompare.X - 1 || x.CellCoordinate.X == cellToCompare.X + 1) && (x.CellCoordinate.Y == cellToCompare.Y + 2 || x.CellCoordinate.Y == cellToCompare.Y - 2) ||
                     (x.CellCoordinate.X == cellToCompare.X - 2 || x.CellCoordinate.X == cellToCompare.X + 2) && (x.CellCoordinate.Y == cellToCompare.Y + 1 || x.CellCoordinate.Y == cellToCompare.Y - 1));
            // Remove cells outside grid
            return RemoveCellsOutSideGameArea(possibleCells.ToList());
        }

        //possible cells
        /*
        X| |X
         |o| 
        X| |X 
         */
        private static List<ExtentedLabel> ClosestCornersIsPossible(Point cellToCompare)
        {
            // Surrounding cells
            var possibleCells = GameArea.Cells.Where(
                x => (x.CellCoordinate.X == cellToCompare.X - 1 || x.CellCoordinate.X == cellToCompare.X + 1) && (x.CellCoordinate.Y == cellToCompare.Y + 1 || x.CellCoordinate.Y == cellToCompare.Y - 1));
            // Remove cells outside grid
            return RemoveCellsOutSideGameArea(possibleCells.ToList());
        }
    }
}
