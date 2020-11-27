using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class PlantSentry
    {
        public List<Plant> Plants;

        private int applesAmount, applesGrowth;
        private int carrotsAmount, carrotsGrowth;
        private int oatsAmount, oatsGrowth;

        public Random Random;

        private MainSentry mainSentry;
        public PlantSentry(
            int apples, int applesGrowth,
            int carrots, int carrotsGrowth,
            int oats, int oatsGrowth,
            MainSentry mainSentry)
        {
            applesAmount = apples;
            carrotsAmount = carrots;
            oatsAmount = oats;
            this.applesGrowth = applesGrowth;
            this.carrotsGrowth = carrotsGrowth;
            this.oatsGrowth = oatsGrowth;
            this.mainSentry = mainSentry;
            Random = mainSentry.Random;
            Plants = new List<Plant>();
        }

        public void FirstTick()
        {
            setPlantsRandomly();
        }

        public void NextTick()
        {
            growPlants();
        }

        public (int, int) GetRandCoordsOnMap()
        {
            return mainSentry.GetRandCoordsOnMap();
        }

        public bool CellIsEmpty((int, int) XY)
        {
            return mainSentry.CellIsEmpty(XY);
        }

        public bool CanGrowOnCell((int, int) XY)
        {
            return mainSentry.CheckBorders(XY) && !(mainSentry.IsOnCell<Organism>(XY)) && !(mainSentry.IsOnCell<Plant>(XY));
        }

        private void setPlantsRandomly()
        {
            for (int i = 0; i < applesAmount; i++)
                setPlantRandomly<Apple>();
            for (int i = 0; i < carrotsAmount; i++)
                setPlantRandomly<Carrot>();
            for (int i = 0; i < oatsAmount; i++)
                setPlantRandomly<Oat>();
        }

        private void growPlants()
        {
            for (int i = 0; i < applesGrowth;)
                if (growPlant<Apple>())
                    i++;
            for (int i = 0; i < carrotsGrowth;)
                if (growPlant<Carrot>())
                    i++;
            for (int i = 0; i < oatsGrowth;)
                if (growPlant<Oat>())
                    i++;
        }

        private void setPlantRandomly<T>()
            where T : Plant
        {
            CreatePlant(Plant.RandSpawn<T>(this));
            //if (typeof(Apple) is T)
            //    CreatePlant(Plant.RandSpawn<Apple>(this));
            //else if (typeof(Carrot) is T)
            //    CreatePlant(Plant.RandSpawn<Carrot>(this));
            //else if (typeof(Oat) is T)
            //    CreatePlant(Plant.RandSpawn<Oat>(this));
        }

        private bool growPlant<T>()
            where T : Plant
        {
            int rand_ind;
            Plant potentialPlant;

            List<Plant> exactPlants = new List<Plant>();
            for (int i = 0; i < Plants.Count; i++)
            {
                if (Plants[i] is T)
                    exactPlants.Add(Plants[i]);
            }
            
            rand_ind = Random.Next(exactPlants.Count);

            potentialPlant = exactPlants[rand_ind].Grow<T>();
            if (potentialPlant != null)
            {
                CreatePlant(potentialPlant);
                return true;
            }
            return false;
        }

        public void CreatePlant(Plant plant)
        {
            Plants.Add(plant);
            mainSentry.EntityWasMadeOnCell(plant);
        }
        public void DeletePlant(Plant plant)
        {
            mainSentry.EntityWasDestroyedOnCell(plant);
            Plants.Remove(Plants.Find(probablyThisPlant => probablyThisPlant.ID == plant.ID));
        }

        public void SetPlantOnCurrentCell<T>((int, int) XY)
            where T : Plant
        {
            CreatePlant(Plant.SetPlant<T>(XY, this));
        }
    }
}
