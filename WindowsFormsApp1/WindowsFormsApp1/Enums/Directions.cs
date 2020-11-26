using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{       // return [0-8]{1}. 0 means that no partner was found. [1-8] means directions.
        // 1 2 3
        // 8   4
        // 7 6 5
    public enum Direction
    {
        Top,
        Right,
        Bottom,
        Left,

        None,

        TopLeft,
        TopRight,
        BottomRight,
        BottomLeft
    }
}
