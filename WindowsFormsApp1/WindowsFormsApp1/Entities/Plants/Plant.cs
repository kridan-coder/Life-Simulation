using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1
{
    public abstract class Plant : Entity//, EdibleForHerbivore, EdibleForOmnivore
    {
        private static PlantSentry plantSentry;
        private static void setPlantSentry(PlantSentry _plantSentry)
        {
            plantSentry = _plantSentry;
        }
        public Plant(int _x, int _y, PlantSentry plantSentry) : base(_x, _y)
        {
            setPlantSentry(plantSentry);
        }

        public Plant Grow<T>()
            where T : Plant
        {
            Direction direction = RandomDirection4(plantSentry.Random);
            switch (direction)
            {
                case Direction.Left:
                    if (canGrowOnCell((X - 1, Y)))
                        return SetPlant<T>((X - 1, Y), plantSentry);
                    break;
                case Direction.Right:
                    if (canGrowOnCell((X + 1, Y)))
                        return SetPlant<T>((X + 1, Y), plantSentry);
                    break;
                case Direction.Top:
                    if (canGrowOnCell((X, Y - 1)))
                        return SetPlant<T>((X, Y - 1), plantSentry);
                    break;
                case Direction.Bottom:
                    if (canGrowOnCell((X, Y + 1)))
                        return SetPlant<T>((X, Y + 1), plantSentry);
                    break;
            }
            return null;
        }

        public static Plant RandSpawn<T>(PlantSentry plantSentry)
            where T : Plant
        {
            (int, int) randXY;
            while(true)
            {
                randXY = plantSentry.GetRandCoordsOnMap();
                if (plantSentry.CellIsEmpty(randXY))
                    return SetPlant<T>(randXY, plantSentry);
            }
        }

        public static Plant SetPlant<T>((int, int) XY, PlantSentry plantSentry)
            where T : Plant
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { XY.Item1, XY.Item2, plantSentry });
        }


        private bool canGrowOnCell((int, int) XY)
        {
            return plantSentry.CanGrowOnCell(XY);
        }
    }
}
