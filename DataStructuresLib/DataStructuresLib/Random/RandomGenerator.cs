namespace DataStructuresLib.Random
{
    public abstract class RandomGenerator
    {
        public abstract double GetNumber();

        public abstract double GetNumber(double maxLimit);

        public abstract double GetNumber(double minLimit, double maxLimit);

        public abstract int GetInteger();

        public abstract int GetInteger(int maxLimit);

        public abstract int GetInteger(int minLimit, int maxLimit);
    }
}