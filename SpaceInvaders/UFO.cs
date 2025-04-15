using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace SpaceInvaders
{
    public class UFO : Entity
    {
        public double FireRate { get; private set; } = 0;
        public double LastShotTime { get; private set; } = 0;

        private DirtyRandom random = new DirtyRandom();


        public Pos StartPosition;
        public Pos EndPosition;
        public double AnimationDuration = 10f;

        private double ElapsedTimeSinceStart = 0f;

        private bool isAlreadyAnimated = false;
        private Animation CurrentAnimation = null;




        public UFO(Sprite image, double x, double y, int width, int height) : base(image, x, y, width, height) 
        {
        }
        public bool CanShoot(double t)
        {
            return (t - LastShotTime) >= 1;
        }

        public void Shoot(World w, double t)
        {
            if (CanShoot(t))
            {
                LastShotTime = t;
            }
        }

        public override void DoCustomMovement(double t, double dt, DirtyRandom r)
        {
            if (CurrentAnimation == null)
            {
                CurrentAnimation = new Animation(
                    new Pos(this.Position.x,this.Position.y),
                    new Pos(r.NextRandom(5,GlobalData.ClientWidth-Width),r.NextRandom(5, GlobalData.ClientHeight - Height-50)),
                    t,
                    t+(r.NextRandom(2,5))
                    );
            }
            else
            {
                CurrentAnimation.UpdateAnimation(t);
                this.NewPosition(CurrentAnimation.CurrentPosition.x, CurrentAnimation.CurrentPosition.y);

                if (CurrentAnimation.IsDone) { CurrentAnimation = null; }

            }
            
        }
    }
}
