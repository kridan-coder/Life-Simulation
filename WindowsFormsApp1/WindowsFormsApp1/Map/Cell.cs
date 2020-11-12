using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Cell
    {
        public Cell()
        {
#nullable enable
            OnCell = new List<Entity?>();
        }
#nullable enable
        public List<Entity?> OnCell;
    }
}
