using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class EnnemyBullet : Entity
    {
        public EnnemyBullet(Sprite image, double x, double y, int width, int height) : base(image, x, y, width, height) { }

        public override void DoCustomMovement(double t, double dt, DirtyRandom r)
        {
            this.Translate(0,20.0d*dt);
        }
    }
}
