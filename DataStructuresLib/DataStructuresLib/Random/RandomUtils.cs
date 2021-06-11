using System.Collections.Generic;

namespace DataStructuresLib.Random
{
    public static class RandomUtils
    {
        public static IEnumerable<double> GetNumbers(this RandomGenerator randGen, int count)
        {
            while (count > 0)
            {
                yield return randGen.GetNumber();

                count--;
            }
        }

        public static IEnumerable<double> GetNumbers(this RandomGenerator randGen, int count, double maxNumber)
        {
            while (count > 0)
            {
                yield return randGen.GetNumber(maxNumber);

                count--;
            }
        }

        public static IEnumerable<double> GetNumbers(this RandomGenerator randGen, int count, double minNumber, double maxNumber)
        {
            while (count > 0)
            {
                yield return randGen.GetNumber(minNumber, maxNumber);

                count--;
            }
        }
    }
}
