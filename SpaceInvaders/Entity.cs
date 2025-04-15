using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class Entity
    {
        public ISprite Sprite { get; set; }
        public Pos PreviousPosition { get; set; }
        public Pos Position { get; set; }
        public Pos InterpolatedPosition { get; set; }

        public int Width;
        public int Height;
        public Entity(ISprite sprite, double x, double y, int width, int height)
        {
            this.Sprite = sprite;
            this.PreviousPosition = new Pos(x, y);
            this.Position = new Pos(x, y);

            Width = width;
            Height = height;
        }

        public void Interpolate(double alpha)
        {
            this.InterpolatedPosition = new Pos(Position.x * alpha + PreviousPosition.x * (1.0 - alpha), Position.y * alpha + PreviousPosition.y * (1.0 - alpha));
        }

        public void NewPosition(double x, double y)
        {
            this.Position = new Pos(x, y);
        }

        public void Translate(double x, double y)
        {
            Position = new Pos(Position.x + x, Position.y + y);
        }

        public void AnimationUpdate(double dt)
        {
            this.Sprite.Update(dt);
        }

        public virtual void DoCustomMovement(double t, double dt, DirtyRandom r)
        {
            
        }
    }
}
