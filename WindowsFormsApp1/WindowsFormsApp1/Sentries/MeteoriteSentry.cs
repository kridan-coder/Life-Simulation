using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class MeteoriteSentry
    {
        public static int AmountOfActiveMeteorites;

        public int midX, midY;

        public int HowManyTicksFall;
        public int HowManyTicksShards;
        public int HowManyTicksBeforeDissolving;

        private int currFalling = 0, currShardsMaking = 0, currDissolving = 0;

        private int currMeteoriteRange = 0;

        public bool HasFallen;
        public bool BecameCold;
        public bool Dissolved;
        public List<MeteoriteShard> MeteoriteShards = new List<MeteoriteShard>();


        MainSentry mainSentry;
        public MeteoriteSentry(MainSentry mainSentry)
        {
            this.mainSentry = mainSentry;
        }

        public void CreateShard(MeteoriteShard shard)
        {
            MeteoriteShards.Add(shard);
            mainSentry.EntityWasMadeOnCell(shard);
        }

        public void DeleteShard(MeteoriteShard shard)
        {
            mainSentry.EntityWasDestroyedOnCell(shard);
            MeteoriteShards.Remove(MeteoriteShards.Find(probablyThisShard => probablyThisShard.ID == shard.ID));
        }
        public void FirstTick()
        {
            MeteoriteShard firstShard = new MeteoriteShard(midX, midY, this);
            CreateShard(firstShard);
        }
        public void nextTick()
        {
            if (!Dissolved)
            {
                if (currFalling < HowManyTicksFall)
                {
                    currFalling++;
                    makeShards(++currMeteoriteRange, false);
                }
                else if (!HasFallen)
                {
                    HasFallen = true;
                    killEverything();
                }

                if (HasFallen)
                {
                    currShardsMaking++;

                    if (currShardsMaking < HowManyTicksShards)
                    {
                        makeShards(++currMeteoriteRange, true);
                        killEverything();
                    }
                    else
                    {
                        BecameCold = true;
                    }
                }

                if (BecameCold)
                {
                    currDissolving++;

                    if (currDissolving >= HowManyTicksBeforeDissolving)
                    {
                        Dissolved = true;
                        dissolve();
                    }
                }
            }
        }

        private void dissolve()
        {
            for (int i = MeteoriteShards.Count - 1; i >= 0; i--)
            {
                shardDissolve(MeteoriteShards[i]);
            }
            AmountOfActiveMeteorites--;
        }

        private void shardDissolve(MeteoriteShard shard)
        {
            int randNum = mainSentry.Random.Next(100);
            mainSentry.ShardKilledEverything((shard.X, shard.Y));
            mainSentry.ShardBecameSomething((shard.X, shard.Y));
            DeleteShard(shard);
        }

        public bool canLieOnCell((int, int) XY)
        {
            return (mainSentry.CheckBorders(XY) && !mainSentry.IsOnCell<MeteoriteShard>(XY));
        }

        private void makeShards(int currMeteoriteRange, bool flyOff)
        {
            // top
            for (int i = midX - currMeteoriteRange; i < midX + currMeteoriteRange; i++)
            {
                if (canLieOnCell((i, midY - currMeteoriteRange)))
                {
                    if (flyOff)
                    {
                        if (mainSentry.Random.Next(100) < 15)
                        {
                            CreateShard(new MeteoriteShard(i, midY - currMeteoriteRange, this));
                        }
                    }
                    else
                    {
                        CreateShard(new MeteoriteShard(i, midY - currMeteoriteRange, this));
                    }
                }
            }
            // right
            for (int i = midY - currMeteoriteRange; i < midY + currMeteoriteRange; i++)
            {
                if (canLieOnCell((midX + currMeteoriteRange, i)))
                {
                    if (flyOff)
                    {
                        if (mainSentry.Random.Next(100) < 15)
                        {
                            CreateShard(new MeteoriteShard(midX + currMeteoriteRange, i, this));
                        }
                    }
                    else
                    {
                        CreateShard(new MeteoriteShard(midX + currMeteoriteRange, i, this));
                    }
                }
            }
            // bottom
            for (int i = midX + currMeteoriteRange; i > midX - currMeteoriteRange; i--)
            {
                if (canLieOnCell((i, midY + currMeteoriteRange)))
                {
                    if (flyOff)
                    {
                        if (mainSentry.Random.Next(100) < 15)
                        {
                            CreateShard(new MeteoriteShard(i, midY + currMeteoriteRange, this));
                        }
                    }
                    else
                    {
                        CreateShard(new MeteoriteShard(i, midY + currMeteoriteRange, this));
                    }
                }
            }
            // left
            for (int i = midY + currMeteoriteRange; i > midY - currMeteoriteRange; i--)
            {
                if (canLieOnCell((midX - currMeteoriteRange, i)))
                {
                    if (flyOff)
                    {
                        if (mainSentry.Random.Next(100) < 15)
                        {
                            CreateShard(new MeteoriteShard(midX - currMeteoriteRange, i, this));
                        }
                    }
                    else
                    {
                        CreateShard(new MeteoriteShard(midX - currMeteoriteRange, i, this));
                    }
                }
            }
        }

        public void MeteoriteSummon(int _howManyTicksFall, int _howManyTicksShards, int _howManyTicksBeforeDissolving, int _midX, int _midY)
        {
            AmountOfActiveMeteorites++;
            midX = _midX;
            midY = _midY;
            HowManyTicksFall = _howManyTicksFall;
            HowManyTicksShards = _howManyTicksShards;
            HowManyTicksBeforeDissolving = _howManyTicksBeforeDissolving;
            Dissolved = false;
            BecameCold = false;
            HasFallen = false;
        }

        public bool CanPlaceShardOnCell((int, int) XY)
        {
            return mainSentry.CheckBorders(XY) && !mainSentry.IsOnCell<MeteoriteShard>(XY);
        }
    }
}
