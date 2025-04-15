using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public enum EnnemyType
    {
        UFO,
        ReconnaissanceStarship,
        SlaveAlien,
        UntrainedSoldier,
        GeneticallyAlteredSoldier,
        Bombership,
        Asteroid,
        Abomination,
        Mother,
        Mothership
    }

    public class Spawner
    {
        private World World;
        private DirtyRandom Random;
        private double SpawnRate = 3.0d;
        private double CurrentTime = 0.0d;
        private double LastSpawnTime = 0.0d;
        private int Level = 1;
        public Spawner(World world, DirtyRandom random)
        { 
            World = world;
            Random = random;

        }

        public void Process(double t, double dt)
        {
            
            if (GlobalData.XP < 10)
            {
                if (CanSpawn(t))
                {
                    SpawnRate = 10000000000.0d;
                    Spawn(EnnemyType.UFO);
                    LastSpawnTime = t;
                }
            }
            if(GlobalData.XP>=10)
            {
                SpawnRate = 1.0d;
                if (CanSpawn(t))
                {
                    Spawn(EnnemyType.SlaveAlien);
                    LastSpawnTime = t;
                }
            }
            

        }

        public void Spawn(EnnemyType ennemyType)
        {

            switch (ennemyType)
            {
                case EnnemyType.UFO:
                    World.UFOs.Add(new UFO(World.Sprites["UFO"], 100, 100, 64, 32));
                    break;
                case EnnemyType.SlaveAlien:
                    World.Ennemies.Add(new Entity(World.AnimatedSprite, Random.NextRandom(0, GlobalData.ClientWidth-32), 30, 32, 32));
                    break;
            }

        }

        public bool CanSpawn(double t)
        {
            return (t - LastSpawnTime) >= SpawnRate;
        }
    }
}
