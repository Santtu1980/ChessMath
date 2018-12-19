using ChessMath.UI;
using System;
using System.Drawing;

namespace ChessMath.Maths
{
    public static class PossibleCells
    {
        public static bool CheckIsPossible(Point clickedCell, UI.Helpers.GridHelper.GridForm gridForm, TreadShape treadShape)
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
            return false;
        }
        private static bool ClosestEdgesIsPossible(Point clickedCell, UI.Helpers.GridHelper.GridForm gridForm, TreadShape treadShape)
        {
            var allCells = GameArea.Cells;
            //possible cells
            /*
            X|X|X
            X|o|X
            X|X|X
             */
            if (!clickedCell.IsEmpty) return false;

            return false;
        }
    }
}
