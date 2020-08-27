using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public enum LevelModuleBottomType
    {
        Null = 0,
        Closed = 1,
        Open = 2
    }

    [System.Flags]
    public enum LevelModuleEdgePosition
    {
        None = 0,
        Top = 1,
        Left = 2,
        Bottom = 4,
        Right = 8
    }
}
