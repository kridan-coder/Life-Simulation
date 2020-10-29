using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Plant : Entity {
        public Plant(int _x, int _y):base(_x, _y)
        {
        }


        public (int,int) Grow(Map map_)
        {
            (int, int) answer = (-1, -1);
            Plant plant = this;
            int cols = map_.cols;
            int rows = map_.rows;
            Random random = new Random();
            int direction = random.Next(4);
            switch (direction)
            {
                case 0:
                    if (map_.CheckBorders(plant.x - 1, plant.y, cols, rows) && !(map_.OrganismIsOnCell(plant.x - 1, plant.y)) && !(map_.PlantIsOnCell(plant.x - 1, plant.y)))
                        answer = (plant.x - 1, plant.y);
                    break;
                case 1:
                    if (map_.CheckBorders(plant.x + 1, plant.y, cols, rows) && !(map_.OrganismIsOnCell(plant.x + 1, plant.y)) && !(map_.PlantIsOnCell(plant.x + 1, plant.y)))
                        answer = (plant.x + 1, plant.y);
                    break;
                case 2:
                    if (map_.CheckBorders(plant.x, plant.y - 1, cols, rows) && !(map_.OrganismIsOnCell(plant.x, plant.y - 1)) && !(map_.PlantIsOnCell(plant.x, plant.y - 1)))
                        answer = (plant.x, plant.y - 1);
                    break;
                case 3:
                    if (map_.CheckBorders(plant.x, plant.y + 1, cols, rows) && !(map_.OrganismIsOnCell(plant.x, plant.y + 1)) && !(map_.PlantIsOnCell(plant.x, plant.y + 1)))
                        answer = (plant.x, plant.y + 1);
                    break;
            }
            return answer;
        }

    }
}
