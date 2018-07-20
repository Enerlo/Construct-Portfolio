using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enerlion
{
    public class PlayerCore : Core
    {
        public CellComponent[] GetCellInfo()
        {
            return _cells;
        }
    }
}