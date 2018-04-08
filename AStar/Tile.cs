using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class Tile
    {
        #region Properties
        public int X { get; set; }

        public int Y { get; set; }

        // 장애물 여부
        public bool IsBlock { get; set; }

        // Draw를 위한 값
        public Rectangle Region { get; set; }

        // Draw를 위한 값
        public string Text { get; set; }

        // G + H
        public int F { get { return G + H; } }

        // START ~ 현재
        public int G { get; private set; }

        // 현재 ~ END
        public int H { get; private set; }

        public Tile Parent { get; private set; }
        #endregion

        #region Public Method
        public void Execute(Tile parent, Tile endTile)
        {
            Parent = parent;
            G = CalcGValue(parent, this);

            int diffX = Math.Abs(endTile.X - X);
            int diffY = Math.Abs(endTile.Y - Y);
            H = (diffX + diffY) * 10;
        }
        #endregion

        #region Static Method
        public static int CalcGValue(Tile parent, Tile current)
        {
            int diffX = Math.Abs(parent.X - current.X);
            int diffY = Math.Abs(parent.Y - current.Y);
            int value = 10;

            if (diffX == 1 && diffY == 0) value = 10;
            else if (diffX == 0 && diffY == 1) value = 10;
            else if (diffX == 1 && diffY == 1) value = 14;

            return parent.G + value;
        }
        #endregion
    }
}