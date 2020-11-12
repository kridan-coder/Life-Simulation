using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Map
    {
        public Random random = new Random();
        public Cell[,] map;

        public List<Herbivore> herbivores = new List<Herbivore>();
        public List<Predatory> predators = new List<Predatory>();
        public List<Omnivore> omnivores = new List<Omnivore>();

        public List<Plant> plants = new List<Plant>();

        public Meteorite meteorite;

        public int rows;
        public int cols;
        public int herbivoresAmount;
        public int predatorsAmount;
        public int plantsAmount;
        public int plantsGrowth;
        public int lastOrgID = 0;
        public int untilDayOrNight;
        public int dayOrNightLastsFor = 0;
        public bool day = true;
        public int minOrgRange;
        public int orgRollBackReproduce;
        public int orgDeadBeforeBecomingGrass;

        public Map(int _herbivores, int _predators, int _plants, int _plantsGrowth, int _rows, int _cols, int _dayNightChange, int _minOrgRange, int _orgRollBackReproduce, int _orgDeadBeforeBecomingGrass)
        {
            plantsAmount = _plants;
            herbivoresAmount = _herbivores;
            predatorsAmount = _predators;
            plantsGrowth = _plantsGrowth;
            rows = _rows;
            cols = _cols;
            untilDayOrNight = _dayNightChange;
            minOrgRange = _minOrgRange;
            orgRollBackReproduce = _orgRollBackReproduce;
            orgDeadBeforeBecomingGrass = _orgDeadBeforeBecomingGrass;
            map = new Cell[cols, rows];
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    map[i, j] = new Cell();
        }

        // check if day should be changed and increment dayOrNightLastsFor counter if needed
        private bool timeToChangeDayOrNight()
        {
            if (dayOrNightLastsFor >= untilDayOrNight)
            {
                dayOrNightLastsFor = 0;
                return true;
            }
            else
            {
                dayOrNightLastsFor++;
                return false;
            }
        }
        private void addPlants()
        {
            int rand_ind;
            Plant? potentialPlant;
            for (int i = 0; i < plantsGrowth;)
            {
                rand_ind = random.Next(plants.Count);
                potentialPlant = plants[rand_ind].Grow();
                if (potentialPlant != null)
                {
                    PlantWasMade(potentialPlant);
                    i++;
                }
            }
        }

        public bool PlantWasEaten(int x, int y)
        {
            if(DeletePlantOnCell(x, y))
            {
                plants.Remove(plants.Find(plant => plant.x == x && plant.y == y));
                return true;
            }
            return false;

        }

        public bool HerbivoreWasEaten(int x, int y)
        {
            if (DeleteOrgOnCell(herbivores.Find(herbivore => herbivore.x == x && herbivore.y == y)))
            {
                herbivores.Remove(herbivores.Find(herbivore => herbivore.x == x && herbivore.y == y));
                return true;
            }
            return false;

        }


        public void HerbivoreBabyWasMade(Herbivore baby)
        {
            herbivores.Add(baby);
            map[baby.x, baby.y].OnCell.Add(herbivores[herbivores.Count - 1]);
        }
        public void PredatorBabyWasMade(Predatory baby)
        {
            predators.Add(baby);
            map[baby.x, baby.y].OnCell.Add(predators[predators.Count - 1]);
        }
        public void OmnivoreBabyWasMade(Omnivore baby)
        {
            omnivores.Add(baby);
            map[baby.x, baby.y].OnCell.Add(omnivores[omnivores.Count - 1]);
        }
        public void PlantWasMade(Plant plant)
        {
            plants.Add(plant);
            map[plant.x, plant.y].OnCell.Add(plants[plants.Count - 1]);
        }
        public void OrganismMadeItsMove<TFood>(Organism<TFood> organism) where TFood : Edible
        {
            map[organism.x, organism.y].OnCell.Add(organism);
        }
        //public void OrganismBecamePlant<TFood>(Organism<TFood> organism) where TFood : Entity
        //{
        //    // create plant
        //    if (!IsOnCell<Plant>(organism.x, organism.y))
        //    {
        //        plants.Add(new Plant(organism.x, organism.y, this));
        //        map[organism.x, organism.y].OnCell.Add(plants[plants.Count - 1]);
        //    }

        //    // delete org
        //    DeleteOrgOnCell(organism);
        //    organisms.Remove(organism);
        //}

        private void createPlantIfNeeded(int x, int y) {
            if (!IsOnCell<Plant>(x, y))
            {
                plants.Add(new Plant(x, y, this));
                map[x, y].OnCell.Add(plants[plants.Count - 1]);
            }
        }
        public void PredatorBecamePlant(Predatory organism)
        {
            createPlantIfNeeded(organism.x, organism.y);
            DeleteOrgOnCell(organism);
            predators.Remove(organism);
        }
        public void HerbivoreBecamePlant(Herbivore organism)
        {
            createPlantIfNeeded(organism.x, organism.y);
            DeleteOrgOnCell(organism);
            herbivores.Remove(organism);
        }
        public void OmnivoreBecamePlant(Omnivore organism)
        {
            createPlantIfNeeded(organism.x, organism.y);
            DeleteOrgOnCell(organism);
            omnivores.Remove(organism);
        }

        //public void OrganismBecamePlant(Organism<Edible> organism)
        //{
        //    createPlantIfNeeded(organism.x, organism.y);
        //    DeleteOrgOnCell(organism);
        //    if (organism is Predatory)
        //    predators.Remove(organism);
        //}

        public Organism<TFood> FindMyPartner<TFood>(int x, int y, bool mySex) where TFood : Edible
        {
            for (int i = 0; i < map[x, y].OnCell.Count; i++)
                if (map[x, y].OnCell[i] is Organism<TFood>)
                {
                    Organism<TFood> potentialPartner = (Organism<TFood>)map[x, y].OnCell[i];
                    if (potentialPartner.isAlive && potentialPartner.male != mySex && potentialPartner.wantReproduce)
                        return potentialPartner;
                }
            // should never come to this line actually
            return null;
        }
        public Herbivore GetHerbivoreOnCell(int x, int y)
        {
            for (int i = 0; i < map[x, y].OnCell.Count; i++)
                if (map[x, y].OnCell[i] is Herbivore)
                    return (Herbivore)map[x, y].OnCell[i];
            return null;
        }
        public Omnivore GetOmnivoreOnCell(int x, int y)
        {
            for (int i = 0; i < map[x, y].OnCell.Count; i++)
                if (map[x, y].OnCell[i] is Omnivore)
                    return (Omnivore)map[x, y].OnCell[i];
            return null;
        }
        public Predatory GetPredatoryOnCell(int x, int y)
        {
            for (int i = 0; i < map[x, y].OnCell.Count; i++)
                if (map[x, y].OnCell[i] is Predatory)
                    return (Predatory)map[x, y].OnCell[i];
            return null;
        }

        public void PlaceMeteoriteShard(MeteoriteShard shard)
        {
            meteorite.meteoriteShards.Add(shard);
            map[shard.x, shard.y].OnCell.Add(meteorite.meteoriteShards[meteorite.meteoriteShards.Count - 1]);

        }
        public void DeleteEverythingExceptShard(MeteoriteShard shard)
        {
            for (int i = 0; i < map[shard.x, shard.y].OnCell.Count; i++)
                if (!(map[shard.x, shard.y].OnCell[i] is MeteoriteShard))
                {
                    if (map[shard.x, shard.y].OnCell[i] is Plant)
                    {
                        plants.Remove(plants.Find(plant => plant.x == shard.x && plant.y == shard.y));
                    }
                    if (map[shard.x, shard.y].OnCell[i] is Herbivore)
                    {
                        herbivores.Remove(herbivores.Find(herbivore => herbivore.x == shard.x && herbivore.y == shard.y));
                    }
                    if (map[shard.x, shard.y].OnCell[i] is Omnivore)
                    {
                        omnivores.Remove(omnivores.Find(omnivore => omnivore.x == shard.x && omnivore.y == shard.y));
                    }
                    if (map[shard.x, shard.y].OnCell[i] is Predatory)
                    {
                        predators.Remove(predators.Find(predator => predator.x == shard.x && predator.y == shard.y));
                    }
                    map[shard.x, shard.y].OnCell.Remove(map[shard.x, shard.y].OnCell[i]);
                }

        }
        public bool CheckBorders(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < cols && y < rows)
                return true;
            return false;
        }
        public bool DeleteOrgOnCell<TFood>(Organism<TFood> me) where TFood : Edible
        {
            for (int i = 0; i < map[me.x, me.y].OnCell.Count; i++)
            {
                if (map[me.x, me.y].OnCell[i] is Organism<TFood>)
                {
                    Organism<TFood> isItMe = (Organism<TFood>)map[me.x, me.y].OnCell[i];
                    if (isItMe.orgID == me.orgID)
                    {
                        map[me.x, me.y].OnCell.Remove(map[me.x, me.y].OnCell[i]);
                        return true;
                    }
                }
            }
            return false;
        }



        public bool DeletePlantOnCell(int x, int y)
        {
            for (int i = 0; i < map[x, y].OnCell.Count; i++)
                if (map[x, y].OnCell[i] is Plant)
                {
                    map[x, y].OnCell.Remove(map[x, y].OnCell[i]);
                    return true;
                }
            return false;
        }
        public bool OrganismHasOppositeSex<TFood>(int x, int y, bool sex) where TFood : Edible
        {
            for (int i = 0; i < map[x, y].OnCell.Count; i++)
                if (map[x, y].OnCell[i] is Organism<TFood>)
                {
                    Organism<TFood> potentialPartner = (Organism<TFood>)map[x, y].OnCell[i];
                    if (potentialPartner.male != sex && potentialPartner.isAlive && potentialPartner.wantReproduce)
                        return true; 
                }
            return false;
        }
        public bool IsOnCell<T>(int x, int y)
        {
            for (int i = 0; i < map[x, y].OnCell.Count; i++)
                if (map[x, y].OnCell[i] is T)
                    return true;
            return false;
        }
        public Cell[,] CreateWorld()
        {
            // set plants
            for (int i = 0; i < plantsAmount; i++)
                PlantWasMade(Plant.RandSpawn(this));
            // set herbivores
            for (int i = 0; i < herbivoresAmount; i++)
                HerbivoreBabyWasMade(Herbivore.RandSpawn(this));
            // set predators
            for (int i = 0; i < predatorsAmount; i++)
                PredatorBabyWasMade(Predatory.RandSpawn(this));
            // set omnivores
            for (int i = 0; i < 0; i++)
                OmnivoreBabyWasMade(Omnivore.RandSpawn(this));
            meteorite = new Meteorite(this);
            meteorite.meteoriteIsComing(15, 15, 20, 50, 50);
            meteorite.FirstTick();

            return map;
        }
        public Cell[,] UpdateWorld()
        {
            addPlants();
            for (int i = predators.Count - 1; i >= 0; i--)
                predators[i].NextMove();
            for (int i = omnivores.Count - 1; i >= 0; i--)
                omnivores[i].NextMove();
            for (int i = herbivores.Count - 1; i >= 0; i--)
                herbivores[i].NextMove();
            meteorite.nextTick();
            if (timeToChangeDayOrNight())
                day = !day;
            return map;
        }
    }
}
