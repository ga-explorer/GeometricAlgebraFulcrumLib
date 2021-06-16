using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using MathNet.Numerics;

namespace DataStructuresLib.Random
{
    public class RandomComposer
    {
        private static double ClampAngle(double value)
        {
            const double maxValue = 2 * Math.PI;

            //value < -maxValue
            if (value < -maxValue)
                return value + Math.Ceiling(-value / maxValue) * maxValue;

            //-maxValue <= value < 0
            if (value < 0)
                return value + maxValue;

            //value > maxValue
            if (value > maxValue)
                return value - Math.Truncate(value / maxValue) * maxValue;

            //0 <= value <= maxValue
            return value;
        }


        public System.Random RandomGenerator { get; }


        public RandomComposer()
        {
            RandomGenerator = 
                RandomGeneratorUtils.Current 
                ?? new System.Random();
        }

        public RandomComposer(int seed)
        {
            RandomGenerator = new System.Random(seed);
        }

        public RandomComposer([NotNull] System.Random randomGenerator)
        {
            RandomGenerator = randomGenerator;
        }


        public double GetNumber()
        {
            return RandomGenerator.NextDouble();
        }

        public double GetScaledNumber(double scalingFactor)
        {
            return scalingFactor * RandomGenerator.NextDouble();
        }

        public double GetOffsetScaledNumber(double offsetFactor, double scalingFactor)
        {
            return offsetFactor + scalingFactor * RandomGenerator.NextDouble();
        }

        public double GetLinearMappedNumber(double minValue, double maxValue)
        {
            return minValue + (maxValue - minValue) * RandomGenerator.NextDouble();
        }

        public T GetMappedNumber<T>(Func<double, T> mappingFunc)
        {
            return mappingFunc(RandomGenerator.NextDouble());
        }

        public IEnumerable<double> GetNumbers(int count)
        {
            while (count > 0)
            {
                yield return GetNumber();
                count--;
            }
        }
        
        public IEnumerable<double> GetScaledNumbers(int count, double scalingFactor)
        {
            while (count > 0)
            {
                yield return GetScaledNumber(scalingFactor);
                count--;
            }
        }
        
        public IEnumerable<double> GetOffsetScaledNumbers(int count, double offsetFactor, double scalingFactor)
        {
            while (count > 0)
            {
                yield return GetOffsetScaledNumber(offsetFactor, scalingFactor);
                count--;
            }
        }
        
        public IEnumerable<double> GetLinearMappedNumbers(int count, double minValue, double maxValue)
        {
            while (count > 0)
            {
                yield return GetLinearMappedNumber(minValue, maxValue);
                count--;
            }
        }

        public IEnumerable<T> GetMappedNumbers<T>(int count, Func<double, T> mappingFunc)
        {
            while (count > 0)
            {
                yield return GetMappedNumber(mappingFunc);
                count--;
            }
        }

        public int GetInteger()
        {
            return RandomGenerator.Next();
        }

        public int GetInteger(int maxValue)
        {
            return maxValue < 0
                ? -RandomGenerator.Next(-maxValue)
                : RandomGenerator.Next(maxValue);
        }

        public int GetInteger(int minValue, int maxValue)
        {
            return RandomGenerator.Next(minValue, maxValue);
        }

        public T GetMappedInteger<T>(Func<int, T> mappingFunc)
        {
            return mappingFunc(GetInteger());
        }

        public T GetMappedInteger<T>(int maxValue, Func<int, T> mappingFunc)
        {
            return mappingFunc(GetInteger(maxValue));
        }

        public T GetMappedInteger<T>(int minValue, int maxValue, Func<int, T> mappingFunc)
        {
            return mappingFunc(GetInteger(minValue, maxValue));
        }

        public T GetItem<T>(IReadOnlyList<T> itemsList)
        {
            return itemsList[GetInteger(itemsList.Count)];
        }

        public IEnumerable<int> GetIntegers(int count)
        {
            while (count > 0)
            {
                yield return GetInteger();
                count--;
            }
        }

        public IEnumerable<int> GetIntegers(int count, int maxValue)
        {
            while (count > 0)
            {
                yield return GetInteger(maxValue);
                count--;
            }
        }

        public IEnumerable<int> GetIntegers(int count, int minValue, int maxValue)
        {
            while (count > 0)
            {
                yield return GetInteger(minValue, maxValue);
                count--;
            }
        }

        public IEnumerable<T> GetMappedIntegers<T>(int count, Func<int, T> mappingFunc)
        {
            while (count > 0)
            {
                yield return GetMappedInteger(mappingFunc);
                count--;
            }
        }

        public IEnumerable<T> GetMappedIntegers<T>(int count, int maxValue, Func<int, T> mappingFunc)
        {
            while (count > 0)
            {
                yield return GetMappedInteger(maxValue, mappingFunc);
                count--;
            }
        }

        public IEnumerable<T> GetMappedIntegers<T>(int count, int minValue, int maxValue, Func<int, T> mappingFunc)
        {
            while (count > 0)
            {
                yield return GetMappedInteger(minValue, maxValue, mappingFunc);
                count--;
            }
        }

        public IEnumerable<T> GetItems<T>(int count, IReadOnlyList<T> itemsList)
        {
            var itemsCount = itemsList.Count;

            while (count > 0)
            {
                yield return itemsList[GetInteger(itemsCount)];
                count--;
            }
        }

        public IEnumerable<int> GetUniqueIndices(int indicesCount, int itemsCount)
        {
            return Enumerable
                .Range(0, itemsCount)
                .Shuffled(RandomGenerator)
                .Take(indicesCount);
        }

        public IEnumerable<T> GetUniqueItems<T>(int indicesCount, IEnumerable<T> itemsList)
        {
            return itemsList
                .Shuffled(RandomGenerator)
                .Take(indicesCount);
        }

        public IEnumerable<T> GetPermutation<T>(IEnumerable<T> itemsList)
        {
            return itemsList.SelectPermutation(RandomGenerator);
        }

        public Complex GetComplex()
        {
            return new Complex(
                RandomGenerator.NextDouble(),
                RandomGenerator.NextDouble()
            );
        }

        public double GetAngle()
        {
            return GetScaledNumber(Constants.Pi2);
        }

        public double GetAngle(double maxAngle)
        {
            return GetScaledNumber(ClampAngle(maxAngle));
        }

        public double GetAngle(double minAngle, double maxAngle)
        {
            return GetLinearMappedNumber(
                ClampAngle(minAngle), 
                ClampAngle(maxAngle)
            );
        }
    }
}