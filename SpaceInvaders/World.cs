

using System.Collections.Generic;

namespace SpaceInvaders
{
    public class World
    {
        public Shooter Shooter;
        private Entity Alien;
        public List<Entity> Bullets;
        public List<EnnemyBullet> EnnemyBullets = new List<EnnemyBullet>();
        public List<Entity> Ennemies;
        public List<Entity> UFOs = new List<Entity>();
        public Dictionary<string, Sprite> Sprites;

        public UFO ufo;

        public AnimatedSprite AnimatedSprite;
        public Entity AnimatedAlien;

        public int ClientWidth;
        public int ClientHeight;

        double bulletSpeed = 500.0d;

        Spawner spawner;

        private CollisionResolver CollisionResolver = new CollisionResolver();

        private DirtyRandom Random = new DirtyRandom(4214);


        public World(int clientWidth, int clientHeight)
        {
            ClientWidth = clientWidth;
            ClientHeight = clientHeight;

            spawner = new Spawner(this, Random);


            Sprites = new Dictionary<string, Sprite>();

            Sprites["Shooter"] = new Sprite("Shooter.png");
            Sprites["Bullet"] = new Sprite("Bullet.png");
            Sprites["Alien"] = new Sprite("Alien.png");
            Sprites["UFO"] = new Sprite("UFO.png");

            AnimatedSprite = new AnimatedSprite("AlienAnimated.png", 32, 4);

            Shooter = new Shooter(Sprites["Shooter"], ClientWidth/2, ClientHeight - Sprites["Shooter"].Height, 32, 32);
            Alien = new Entity(Sprites["Alien"], 100, 100, 32, 32);
            AnimatedAlien = new Entity(AnimatedSprite, 200, 100, 32, 32);
            Bullets = new List<Entity>();
            Bullets.Capacity = 20;

            Ennemies = new List<Entity>();

        }

        public void SpawnBullet()
        {
            Bullets.Add(new Entity(Sprites["Bullet"], Shooter.Position.x + (32 / 2) - 4, Shooter.Position.y - 12,8,8));
        }

        public void PreviousIsCurrent(double t, double dt)
        {
            Shooter.PreviousPosition = new Pos(Shooter.Position.x, Shooter.Position.y);

            if (Bullets.Count > 0)
            {
                foreach (Entity Bullet in Bullets)
                {
                    Bullet.PreviousPosition = new Pos(Bullet.Position.x, Bullet.Position.y);
                }
            }

            if (Ennemies.Count > 0)
            {
                foreach (Entity Ennemy in Ennemies)
                {
                    Ennemy.PreviousPosition = new Pos(Ennemy.Position.x, Ennemy.Position.y);
                }
            }

            if (UFOs.Count > 0)
            {
                foreach (UFO ufo in UFOs)
                {
                    ufo.PreviousPosition = new Pos(ufo.Position.x, ufo.Position.y);
                }
            }

        }

        public void Step(double t, double dt)
        {
            CollisionResolver.Resolve(Bullets, UFOs);
            CollisionResolver.Resolve(Bullets, Ennemies);
            CollisionResolver.ResolveOutsideBounds(Bullets);

            spawner.Process(t, dt);



            if (Input.PressedKeys[Keys.Right])
            {
                Shooter.Translate(150.0d * dt, 0);
                Random.TranformSeed(3);
            }
            if (Input.PressedKeys[Keys.Left])
            {
                Shooter.Translate(-(150.0d * dt), 0);
                Random.TranformSeed(1);
            }
            if (Input.PressedKeys[Keys.Space] && Shooter.CanShoot(t))
            {
                Shooter.Shoot(this, t);
                Random.TranformSeed(2);
            }


            if (!Input.PressedKeys[Keys.Right] && !Input.PressedKeys[Keys.Left])
            {

            }

            if (Bullets.Count > 0)
            {
                foreach (Entity Bullet in Bullets)
                {
                    Bullet.Translate(0, -(bulletSpeed * dt));
                }
            }

            if (EnnemyBullets.Count > 0)
            {
                foreach (EnnemyBullet ennemyBullet in EnnemyBullets)
                {
                    ennemyBullet.DoCustomMovement(t, dt, Random);
                }
            }

            if (Ennemies.Count > 0)
            {
                foreach (Entity Ennemy in Ennemies)
                {
                    Ennemy.Translate(0, 20.0d*dt);
                }
            }

            if (UFOs.Count > 0)
            {
                foreach (UFO ufo in UFOs)
                {
                    ufo.DoCustomMovement(t, dt, Random);
                }
            }

            AnimatedSprite.Update(dt);
        }

        public void Interpolate(double alpha)
        {
            Shooter.Interpolate(alpha);

            if (Bullets.Count > 0)
            {
                foreach (Entity Bullet in Bullets)
                {
                    Bullet.Interpolate(alpha);
                }
            }

            if (Ennemies.Count > 0)
            {
                foreach (Entity Ennemy in Ennemies)
                {
                    Ennemy.Interpolate(alpha);
                }
            }

            if (UFOs.Count > 0)
            {
                foreach (UFO ufo in UFOs)
                {
                    ufo.Interpolate(alpha);
                }
            }



            
        }

        public void DrawLists(PaintEventArgs e, params List<Entity>[] listsOfEntities)
        {
            for (int i = 0; i < listsOfEntities.Length; i++)
            {
                // Loop through every list in the entityLists array
                foreach (List<Entity> EntityList in listsOfEntities)
                {
                    // Loop through every entity in the current list
                    foreach (Entity Entity in EntityList)
                    {
                        e.Graphics.DrawImage(
                        Entity.Sprite.Image,
                        (float)Entity.InterpolatedPosition.x,
                        (float)Entity.InterpolatedPosition.y,
                        Entity.Sprite.SourceRect,
                        GraphicsUnit.Pixel);
                    }
                }
            }

        }

        public void Render(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            e.Graphics.DrawImage(Shooter.Sprite.Image, (float)Shooter.InterpolatedPosition.x, (float)Shooter.InterpolatedPosition.y);

            DrawLists(e, Bullets, Ennemies, UFOs);

        }
    }
}
