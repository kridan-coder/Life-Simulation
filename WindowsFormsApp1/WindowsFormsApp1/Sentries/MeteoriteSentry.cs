using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class MeteoriteSentry
    {
        public int meteoritePieceID;
        private static int lastMeteoritePieceID;

        public int midX, midY;

        public int howManyTicksFall;
        public int howManyTicksShards;
        public int howManyTicksBeforeDissolving;

        public int chanceOfHumanToSpawn, chanceOfPlantToSpawn;

        private int currFalling = 0, currShardsMaking = 0, currDissolving = 0;

        private int currMeteoriteRange = 0;
        public bool hasFallen;
        public bool becameCold;
        public bool dissolved;
        public List<MeteoriteShard> meteoriteShards = new List<MeteoriteShard>();


        Map map;
        public MeteoriteSentry(Map _map)
        {
            map = _map;
        }
        public void FirstTick()
        {
            MeteoriteShard currShard = new MeteoriteShard(midX, midY, map, this);
            map.PlaceMeteoriteShard(currShard);
        }
        public void nextTick()
        {
            if (!dissolved)
            {
                if (currFalling < howManyTicksFall)
                {
                    currFalling++;
                    makeShards(++currMeteoriteRange, false);
                }
                else if (!hasFallen)
                {
                    hasFallen = true;
                    killEverything();
                }

                if (hasFallen)
                {
                    currShardsMaking++;

                    if (currShardsMaking < howManyTicksShards)
                    {
                        makeShards(++currMeteoriteRange, true);
                        killEverything();
                    }
                    else
                    {
                        becameCold = true;
                    }
                }

                if (becameCold)
                {
                    currDissolving++;

                    if (currDissolving >= howManyTicksBeforeDissolving)
                    {
                        dissolved = true;
                        dissolve();
                    }
                }
            }
        }

        private void dissolve()
        {
            for (int i = meteoriteShards.Count - 1; i >= 0; i--)
            {
                int randNum = map.random.Next(100);
                map.DeleteEverythingExceptShard(meteoriteShards[i]);

                if (randNum < chanceOfHumanToSpawn)
                {
                    Omnivore baby = new Omnivore(meteoriteShards[i].x, meteoriteShards[i].y, false, 0, 100, 100, map);
                    baby.makeBaby();
                }
                else if (randNum < chanceOfHumanToSpawn + chanceOfPlantToSpawn)
                {
                    Plant plant = new Plant(meteoriteShards[i].x, meteoriteShards[i].y, map);
                    map.PlantWasMade(plant);
                }
                map.DeleteOnCell<MeteoriteShard>(meteoriteShards[i].x, meteoriteShards[i].y);
                meteoriteShards.Remove(meteoriteShards[i]);
            }
            map.MeteoriteIsNotActive();
        }
        private void killEverything()
        {
            for (int i = 0; i < meteoriteShards.Count; i++)
                map.DeleteEverythingExceptShard(meteoriteShards[i]);
        }


        private void makeShards(int currMeteoriteRange, bool flyOff)
        {
            // top
            for (int i = midX - currMeteoriteRange; i < midX + currMeteoriteRange; i++)
            {
                MeteoriteShard currShard = new MeteoriteShard(0, 0, map, this);
                currShard = currShard.TryToPlace(i, midY - currMeteoriteRange);

                if (currShard != null)
                    if (flyOff)
                    {
                        if (map.random.Next(100) < 15)
                        {
                            map.PlaceMeteoriteShard(currShard);
                        }
                    }
                    else
                    {
                        map.PlaceMeteoriteShard(currShard);
                    }

            }
            // right
            for (int i = midY - currMeteoriteRange; i < midY + currMeteoriteRange; i++)
            {
                MeteoriteShard currShard = new MeteoriteShard(0, 0, map, this);
                currShard = currShard.TryToPlace(midX + currMeteoriteRange, i);
                if (currShard != null)
                    if (flyOff)
                    {
                        if (map.random.Next(100) < 25)
                        {
                            map.PlaceMeteoriteShard(currShard);
                        }
                    }
                    else
                    {
                        map.PlaceMeteoriteShard(currShard);
                    }
            }
            // bottom
            for (int i = midX + currMeteoriteRange; i > midX - currMeteoriteRange; i--)
            {
                MeteoriteShard currShard = new MeteoriteShard(0, 0, map, this);
                currShard = currShard.TryToPlace(i, midY + currMeteoriteRange);
                if (currShard != null)
                    if (flyOff)
                    {
                        if (map.random.Next(100) < 25)
                        {
                            map.PlaceMeteoriteShard(currShard);
                        }
                    }
                    else
                    {
                        map.PlaceMeteoriteShard(currShard);
                    }
            }
            // left
            for (int i = midY + currMeteoriteRange; i > midY - currMeteoriteRange; i--)
            {
                MeteoriteShard currShard = new MeteoriteShard(0, 0, map, this);
                currShard = currShard.TryToPlace(midX - currMeteoriteRange, i);
                if (currShard != null)
                    if (flyOff)
                    {
                        if (map.random.Next(100) < 25)
                        {
                            map.PlaceMeteoriteShard(currShard);
                        }
                    }
                    else
                    {
                        map.PlaceMeteoriteShard(currShard);
                    }
            }
        }

        public void meteoriteIsComing(int _chanceOfHumanToSpawn, int _chanceOfPlantToSpawn, int _howManyTicksFall, int _howManyTicksShards, int _howManyTicksBeforeDissolving, int _midX, int _midY)
        {
            chanceOfHumanToSpawn = _chanceOfHumanToSpawn;
            chanceOfPlantToSpawn = _chanceOfPlantToSpawn;
            midX = _midX;
            midY = _midY;
            howManyTicksFall = _howManyTicksFall;
            howManyTicksShards = _howManyTicksShards;
            howManyTicksBeforeDissolving = _howManyTicksBeforeDissolving;
            dissolved = false;
            becameCold = false;
            hasFallen = false;
        }


    }
}
