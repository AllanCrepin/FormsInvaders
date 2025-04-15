using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class DirtyRandom
    {

        private long seed;
        public long Seed
        {
            get { return seed; }
            private set 
            {
                if ((seed + value) >= 9223000000000000) {
                    seed = 0;
                }
                else
                {
                    seed = value;
                }
            }
        }

        private List<short> RandomNumbers;


        public DirtyRandom(long seed)
        {
            Seed = seed;

            RandomNumbers = new List<short>();
            RandomNumbers.Capacity = 100;

            RandomNumbers.AddRange(new short[10] { 7, 5, 1, 9, 2, 3, 6, 8, 4, 0 });
            RandomNumbers.AddRange(new short[10] { 3, 0, 2, 8, 1, 7, 6, 9, 5, 4 });
            RandomNumbers.AddRange(new short[10] { 1, 2, 6, 4, 8, 0, 9, 7, 3, 5 });

            RandomNumbers.AddRange(new short[10] { 8, 8, 4, 6, 6, 4, 0, 8, 6, 8 });
            RandomNumbers.AddRange(new short[10] { 4, 1, 3, 6, 1, 2, 0, 9, 8, 7});

            RandomNumbers.AddRange(new short[10] { 4, 1, 7, 5, 0, 9, 3, 8, 6, 2 });
            RandomNumbers.AddRange(new short[10] { 5, 2, 9, 5, 2, 3, 1, 1, 8, 5 });
            RandomNumbers.AddRange(new short[10] { 3, 6, 0, 7, 8, 1, 5, 9, 4, 2 });
            RandomNumbers.AddRange(new short[10] { 8, 2, 3, 6, 7, 9, 4, 5, 1, 0 });
            RandomNumbers.AddRange(new short[10] { 3, 8, 6, 4, 9, 5, 1, 2, 7, 0 });
        }

        public DirtyRandom() : this(0){}

        public void TranformSeed(int value)
        {
            if (Seed >= 9223000000000000) { Seed = 0; }

            Seed += Math.Abs(value);
        }

        public void SetSeed(int value)
        {
            if (Seed >= 9223000000000000) { Seed = 0; }

            Seed = Math.Abs(value);
        }

        private double ToRange(double number, int a, int b)
        {

            number = Math.Abs(number);
            if (number < 0 || number > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Input number must be between 0 and 1.");
            }

            return a + (number * (b - a));
        }

        private double Random0To1()
        {
            int index = (int) Seed;
            string rand = "";
            rand+= "0.";

            for(int i = 0; i<10; i++)
            {
                index += i;
                rand += RandomNumbers[index % RandomNumbers.Count];
            }

            return double.Parse(rand, System.Globalization.CultureInfo.InvariantCulture);

        }

        public double NextRandom(int a, int b)
        {
            if (Seed >= 9223000000000000) { Seed = 0; }
            Seed++;
            return ToRange(Random0To1(), a, b);

        }

    }
}
