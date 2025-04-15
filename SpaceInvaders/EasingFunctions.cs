using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public static class EasingFunctions
    {
        public static double EaseInOutCubic(double x)
        {
            return x < 0.5 ? 4 * x * x * x : 1 - Math.Pow(-2 * x + 2, 3) / 2;
        }
        public static double Normalize(double v, double x, double y)
        {
            if (y == x)
            {
                throw new ArgumentException("y must not be equal to x to avoid division by zero.");
            }

            if (v >= y) { return 1; }

            return (v - x) / (y - x);
        }

        public static double EaseInSine(double x)
        {
            return 1 - Math.Cos((x * Math.PI) / 2);
        }
    }
}
