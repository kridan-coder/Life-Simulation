using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Cell
    {
        public Cell(float noiseValue)
        {
            NoiseValue = noiseValue;
#nullable enable
            OnCell = new List<Entity?>();
        }



        public float NoiseValue;
        public CellState CellState;

#nullable enable
        public List<Entity?> OnCell;
    }
}
