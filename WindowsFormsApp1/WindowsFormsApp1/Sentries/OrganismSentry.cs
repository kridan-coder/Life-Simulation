using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1
{
    public class OrganismSentry
    {
        public List<Organism> Organisms;

        private int humansAmount, ticksHumanStutter;
        private int deersAmount, ticksDeerStutter;
        private int miceAmount, ticksMouseStutter;
        private int rabbitsAmount, ticksRabbitStutter;
        private int bearsAmount, ticksBearStutter;
        private int pigsAmount, ticksPigStutter;
        private int raccoonsAmount, ticksRaccoonStutter;
        private int foxesAmount, ticksFoxStutter;
        private int lionsAmount, ticksLionStutter;
        private int wolvesAmount, ticksWolfStutter;

        public int MaxOrgVisionRange;
        public int MaxOrgTicksBeforeReproducing;
        public int MaxOrgTicksBeforeBecomingGrass;

        public Random Random;

        private MainSentry mainSentry;
        public OrganismSentry(
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
            this.ticksHumanStutter = ticksHumanStutter;

            deersAmount = deers;
            this.ticksDeerStutter = ticksDeerStutter;

            miceAmount = mice;
            this.ticksMouseStutter = ticksMouseStutter;

            rabbitsAmount = rabbits;
            this.ticksRabbitStutter = ticksRabbitStutter;

            bearsAmount = bears;
            this.ticksBearStutter = ticksBearStutter;

            pigsAmount = pigs;
            this.ticksPigStutter = ticksPigStutter;

            raccoonsAmount = raccoons;
            this.ticksRaccoonStutter = ticksRaccoonStutter;

            foxesAmount = foxes;
            this.ticksFoxStutter = ticksFoxStutter;

            lionsAmount = lions;
            this.ticksDeerStutter = ticksLionStutter;

            wolvesAmount = wolves;
            this.ticksWolfStutter = ticksWolfStutter;

            MaxOrgVisionRange = maxOrgVisionRange;
            MaxOrgTicksBeforeReproducing = maxOrgTicksBeforeReproducing;
            MaxOrgTicksBeforeBecomingGrass = maxOrgTicksBeforeBecomingGrass;

            this.mainSentry = mainSentry;

            Random = mainSentry.Random;

            Organisms = new List<Organism>();
        }

        public void FirstTick()
        {
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

        public bool CellIsAppropriate<T, TFood>((int, int) XY, bool? sex)
            where T : Organism
            where TFood : Edible
        {
            if (!mainSentry.CheckBorders(XY))
            {
                return false;
            }
            if (sex != null)
            {
                Organism<T, TFood> potentialPartner = mainSentry.FindOrganismPartnerOnCell<T,TFood>(XY, (bool)sex);
                if (potentialPartner != null)
                    return true;
                return false;
            }
            return mainSentry.IsOnCell<TFood>(XY);
        }

        public Organism<T, TFood> FindOrganismPartner<T, TFood>((int, int) XY, bool male)
            where T : Organism
            where TFood : Edible
        {
            return mainSentry.FindOrganismPartnerOnCell<T, TFood>(XY, male);
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
            CreateOrganism(Organism<T, Edible>.MakeBaby(XY));
        }
    }
}
