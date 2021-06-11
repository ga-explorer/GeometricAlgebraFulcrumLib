using System;

namespace DataStructuresLib.Random
{
    public class UniformRandomGenerator : RandomGenerator
    {
        private System.Random _randGen;


        public UniformRandomGenerator()
        {
            _randGen = new System.Random();
        }

        public UniformRandomGenerator(int seed)
        {
            _randGen = new System.Random(seed);
        }


        public void ResetSeed()
        {
            _randGen = new System.Random(DateTime.Now.Millisecond);
        }

        public void ResetSeed(int seed)
        {
            _randGen = new System.Random(seed);
        }


        public override double GetNumber() 
            => _randGen.NextDouble();

        public override double GetNumber(double maxLimit) 
            => _randGen.NextDouble() * maxLimit;

        public override double GetNumber(double minLimit, double maxLimit) 
            => _randGen.NextDouble() * (maxLimit - minLimit) + minLimit;

        public override int GetInteger() 
            => _randGen.Next();

        public override int GetInteger(int maxLimit) 
            => maxLimit > 0
                ? _randGen.Next(maxLimit - 1)
                : -_randGen.Next(-maxLimit - 1);

        public override int GetInteger(int minLimit, int maxLimit)
        {
            if (minLimit > maxLimit)
            {
                var s = minLimit;
                minLimit = maxLimit;
                maxLimit = s;
            }

            return _randGen.Next(minLimit, maxLimit - 1);
        }
    }
}
