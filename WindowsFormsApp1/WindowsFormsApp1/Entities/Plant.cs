using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Plant : Entity {
        public int plantID;
        private static int lastPlantID;
        public Plant(int _x, int _y, Map _map):base(_x, _y, _map)
        {
            plantID = lastPlantID++;
        }

        private bool canGrowHere(int x, int y)
        {
            return map.CheckBorders(x, y)
                && !(map.IsOnCell<Organism>(x, y))
                && !(map.IsOnCell<Plant>(x, y));
        }
        public Plant? Grow()
        {
            Direction direction = randomDirection4();
            switch (direction)
            {
                case Direction.Left:
                    if (canGrowHere(x - 1, y))
                        return new Plant(x - 1, y, map);
                    break;
                case Direction.Right:
                    if (canGrowHere(x + 1, y))
                        return new Plant(x + 1, y, map);
                    break;
                case Direction.Top:
                    if (canGrowHere(x, y - 1))
                        return new Plant(x, y - 1, map);
                    break;
                case Direction.Bottom:
                    if (canGrowHere(x, y + 1))
                        return new Plant(x, y + 1, map);
                    break;
            }
            return null;
        }

        public static Plant RandSpawn(Map map)
        {
            int x, y;
            while(true)
            {
                x = map.random.Next(map.cols);
                y = map.random.Next(map.rows);
                if (map.map[x, y].on_cell.Count == 0)
                    return new Plant(x, y, map);
            }

        }

    }
}
