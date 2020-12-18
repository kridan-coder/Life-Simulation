using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1
{
    public class AnimalSentry
    {
        public List<Organism> Organisms;

        private int humansAmount;
        public int TicksHumanStutter;
        private int deersAmount;
        public int TicksDeerStutter;
        private int miceAmount;
        public int TicksMouseStutter;
        private int rabbitsAmount;
        public int TicksRabbitStutter;
        private int bearsAmount;
        public int TicksBearStutter;
        private int pigsAmount;
        public int TicksPigStutter;
        private int raccoonsAmount;
        public int TicksRaccoonStutter;
        private int foxesAmount;
        public int TicksFoxStutter;
        private int lionsAmount;
        public int TicksLionStutter;
        private int wolvesAmount;
        public int TicksWolfStutter;

        public int MaxOrgVisionRange;
        public int MaxOrgTicksBeforeReproducing;
        public int MaxOrgTicksBeforeBecomingGrass;

        public Random Random;

        private MainSentry mainSentry;
        public AnimalSentry(
            int humans, int ticksHumanStutter,
            int deers, int ticksDeerStutter,
            int mice, int ticksMouseStutter,
            int rabbits, int ticksRabbitStutter,
            int bears, int ticksBearStutter,
            int pigs, int ticksPigStutter,
            int raccoons, int ticksRaccoonStutter,
            int foxes, int ticksFoxStutter,
            int lions, int ticksLionStutter,
            int wolves, int ticksWolfStutter,
            int maxOrgVisionRange,
            int maxOrgTicksBeforeReproducing,
            int maxOrgTicksBeforeBecomingGrass,
            MainSentry mainSentry)
        {
            humansAmount = humans;
            TicksHumanStutter = ticksHumanStutter;

            deersAmount = deers;
            TicksDeerStutter = ticksDeerStutter;

            miceAmount = mice;
            TicksMouseStutter = ticksMouseStutter;

            rabbitsAmount = rabbits;
            TicksRabbitStutter = ticksRabbitStutter;

            bearsAmount = bears;
            TicksBearStutter = ticksBearStutter;

            pigsAmount = pigs;
            TicksPigStutter = ticksPigStutter;

            raccoonsAmount = raccoons;
            TicksRaccoonStutter = ticksRaccoonStutter;

            foxesAmount = foxes;
            TicksFoxStutter = ticksFoxStutter;

            lionsAmount = lions;
            TicksDeerStutter = ticksLionStutter;

            wolvesAmount = wolves;
            TicksWolfStutter = ticksWolfStutter;

            MaxOrgVisionRange = maxOrgVisionRange;
            MaxOrgTicksBeforeReproducing = maxOrgTicksBeforeReproducing;
            MaxOrgTicksBeforeBecomingGrass = maxOrgTicksBeforeBecomingGrass;

            this.mainSentry = mainSentry;

            Random = mainSentry.Random;

            Organisms = new List<Organism>();
        }

        public void FirstTick()
        {
            setStutters();
            setOrganismsRandomly();
        }

        public void NextTick()
        {
            makeMoves();
        }

        private void makeMoves()
        {
            for (int i = Organisms.Count - 1; i >= 0; i--)
                Organisms[i].NextMove();
        }

        private void setStutters()
        {
            Organism<Human, Edible>.StutterUntil = TicksHumanStutter;
            Organism<Deer, Edible>.StutterUntil = TicksDeerStutter;
            Organism<Mouse, Edible>.StutterUntil = TicksMouseStutter;
            Organism<Rabbit, Edible>.StutterUntil = TicksRabbitStutter;
            Organism<Bear, Edible>.StutterUntil = TicksBearStutter;
            Organism<Pig, Edible>.StutterUntil = TicksPigStutter;
            Organism<Raccoon, Edible>.StutterUntil = TicksRaccoonStutter;
            Organism<Fox, Edible>.StutterUntil = TicksFoxStutter;
            Organism<Lion, Edible>.StutterUntil = TicksLionStutter;
            Organism<Wolf, Edible>.StutterUntil = TicksWolfStutter;
        }
        private void setOrganismsRandomly()
        {
            for (int i = 0; i < humansAmount; i++)
                setOrganismRandomly<Human>();
            for (int i = 0; i < deersAmount; i++)
                setOrganismRandomly<Deer>();
            for (int i = 0; i < miceAmount; i++)
                setOrganismRandomly<Mouse>();
            for (int i = 0; i < rabbitsAmount; i++)
                setOrganismRandomly<Rabbit>();
            for (int i = 0; i < bearsAmount; i++)
                setOrganismRandomly<Bear>();
            for (int i = 0; i < pigsAmount; i++)
                setOrganismRandomly<Pig>();
            for (int i = 0; i < raccoonsAmount; i++)
                setOrganismRandomly<Raccoon>();
            for (int i = 0; i < foxesAmount; i++)
                setOrganismRandomly<Fox>();
            for (int i = 0; i < lionsAmount; i++)
                setOrganismRandomly<Lion>();
            for (int i = 0; i < wolvesAmount; i++)
                setOrganismRandomly<Wolf>();
        }

        private void setOrganismRandomly<T>()
            where T : Organism
        {
            CreateOrganism(Organism<T, Edible>.RandSpawn(this));
        }

        //public int GetStutter<T>()
        //    where T : Edible
        //{
        //    if (typeof(EdibleForHuman) is T)
        //    {
        //        return TicksHumanStutter;
        //    }
        //    else if (typeof(Bear) is T)
        //    {
        //        return TicksBearStutter;
        //    }
        //    else if (typeof(Pig) is T)
        //    {
        //        return TicksPigStutter;
        //    }
        //    else if (typeof(Raccoon) is T)
        //    {
        //        return TicksRaccoonStutter;
        //    }
        //    else if (typeof(Deer) is T)
        //    {
        //        return TicksDeerStutter;
        //    }
        //    else if (typeof(Mouse) is T)
        //    {
        //        return TicksMouseStutter;
        //    }
        //    else if (typeof(Rabbit) is T)
        //    {
        //        return TicksRabbitStutter;
        //    }
        //    else if (typeof(Fox) is T)
        //    {
        //        return TicksFoxStutter;
        //    }
        //    else if (typeof(Lion) is T)
        //    {
        //        return TicksLionStutter;
        //    }
        //    else if (typeof(Wolf) is T)
        //    {
        //        return TicksWolfStutter;
        //    }
        //    return 0;
        //}


        public bool IsItDayToday()
        {
            return mainSentry.IsItDayToday();
        }
        public (int, int) GetRandCoordsOnMap()
        {
            return mainSentry.GetRandCoordsOnMap();
        }
        public bool CellIsEmpty((int, int) XY)
        {
            return mainSentry.CellIsEmpty(XY);
        }
        public bool IsOnCell<T>((int, int) XY)
        {
            return mainSentry.IsOnCell<T>(XY);
        }

        public bool CanStepOnCell((int, int) XY)
        {
            return mainSentry.CheckBorders(XY);
        }

        public bool CellIsAppropriate<T, TFood>((int, int) XY, Sex? sex)
            where T : Organism
            where TFood : Edible
        {
            if (!mainSentry.CheckBorders(XY))
            {
                return false;
            }
            if (sex != null)
            {
                Organism<T, TFood> potentialPartner = mainSentry.FindOrganismPartnerOnCell<T,TFood>(XY, (Sex)sex);
                if (potentialPartner != null)
                    return true;
                return false;
            }
            return mainSentry.IsOnCell<TFood>(XY);
        }

        public Organism<T, TFood> FindOrganismPartner<T, TFood>((int, int) XY, Sex sex)
            where T : Organism
            where TFood : Edible
        {
            return mainSentry.FindOrganismPartnerOnCell<T, TFood>(XY, sex);
        }

        public void OrganismWasDestroyedOnCell(Organism organism)
        {
            mainSentry.EntityWasDestroyedOnCell(organism);
        }

        public void OrganismWasMadeOnCell(Organism organism)
        {
            mainSentry.EntityWasMadeOnCell(organism);
        }

        public void CreateOrganism(Organism organism)
        {
            Organisms.Add(organism);
            mainSentry.EntityWasMadeOnCell(organism);
        }

        public void DeleteOrganism(Organism organism)
        {
            mainSentry.EntityWasDestroyedOnCell(organism);
            Organisms.Remove(Organisms.Find(probablyThisOrganism => probablyThisOrganism.ID == organism.ID));
        }

        public void OrganismBecamePlant(Organism organism)
        {
            mainSentry.OrganismBecamePlant(organism);
            DeleteOrganism(organism);
        }

        public void EntityWasEaten<TFood>((int, int) XY)
            where TFood : Edible
        {
            mainSentry.EntityWasEaten<TFood>(XY);
        }

        public void SetOrganismOnCurrentCell<T>((int, int) XY)
    where T : Organism
        {
            CreateOrganism(Organism<T, Edible>.MakeBaby(XY, this));
        }
    }
}
