
namespace SpaceInvaders
{
    public class Animation
    {
        private Pos StartPosition;
        public Pos CurrentPosition;
        private Pos EndPosition;

        private double StartTime;
        private double EndTime;

        public bool IsDone = false;
        public Animation(Pos startPosition, Pos endPosition, double startTime, double endTime)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            StartTime = startTime;
            EndTime = endTime;
        }

        public void UpdateAnimation(double t)
        {
            double currentTimeNormalized = EasingFunctions.Normalize(t, StartTime, EndTime);

            double deltaX = EndPosition.x - StartPosition.x;
            double deltaY = EndPosition.y - StartPosition.y;

            this.CurrentPosition = new Pos (
                StartPosition.x + EasingFunctions.EaseInOutCubic(currentTimeNormalized) * deltaX,
                StartPosition.y + EasingFunctions.EaseInOutCubic(currentTimeNormalized) * deltaY);

            if (currentTimeNormalized >= 0.98)
            {
                IsDone = true;
            }
        }
    }
}
