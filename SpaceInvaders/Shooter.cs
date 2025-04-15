using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class Shooter : Entity
    {
        public double FireRate { get; private set; }
        public double LastShotTime {  get; private set; }
        public Shooter(Sprite image, double x, double y, int width, int height): base(image, x, y, width, height)
        {
            FireRate = .8d;
            LastShotTime = -1000;
        }

        public bool CanShoot(double currentTime)
        {
            return (currentTime - LastShotTime) >= FireRate;
        }

        public void Shoot(World w, double currentTime)
        {
            if (CanShoot(currentTime))
            {
                w.SpawnBullet();
                LastShotTime = currentTime;
            }
        }
    }
}
