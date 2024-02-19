using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using DataStructuresLib.Basic;
using MathNet.Numerics;
using MathNet.Numerics.Statistics;

namespace DataStructuresLib.Random;

/// <summary>
/// Contains helpers and extensions for working with random number generators
/// https://github.com/madelson/MedallionUtilities/blob/master/MedallionRandom/Rand.cs
/// </summary>
public static class RandomGeneratorUtils
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign GetSign(this System.Random randomGenerator)
    {
        return IntegerSign.CreateFromInt32(
            ((randomGenerator.Next() & 1) << 1) - 1
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign GetTriSign(this System.Random randomGenerator)
    {
        return IntegerSign.CreateFromInt32(
            randomGenerator.Next(-1, 2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this System.Random randomGenerator, T item1, T item2)
    {
        return (randomGenerator.Next() & 1) == 0 ? item1 : item2;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this System.Random randomGenerator, T item1, T item2, T item3)
    {
        return (randomGenerator.Next() % 3) switch
        {
            0 => item1,
            1 => item2,
            _ => item3
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this System.Random randomGenerator, IPair<T> items)
    {
        return (randomGenerator.Next() & 1) == 0 ? items.Item1 : items.Item2;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this IPair<T> items, System.Random randomGenerator)
    {
        return (randomGenerator.Next() & 1) == 0 ? items.Item1 : items.Item2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this System.Random randomGenerator, ITriplet<T> items)
    {
        return (randomGenerator.Next() % 3) switch
        {
            0 => items.Item1,
            1 => items.Item2,
            _ => items.Item3
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this ITriplet<T> items, System.Random randomGenerator)
    {
        return (randomGenerator.Next() % 3) switch
        {
            0 => items.Item1,
            1 => items.Item2,
            _ => items.Item3
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this System.Random randomGenerator, IQuad<T> items)
    {
        return (randomGenerator.Next() % 4) switch
        {
            0 => items.Item1,
            1 => items.Item2,
            2 => items.Item3,
            _ => items.Item4
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this IQuad<T> items, System.Random randomGenerator)
    {
        return (randomGenerator.Next() % 4) switch
        {
            0 => items.Item1,
            1 => items.Item2,
            2 => items.Item3,
            _ => items.Item4
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this System.Random randomGenerator, IQuint<T> items)
    {
        return (randomGenerator.Next() % 5) switch
        {
            0 => items.Item1,
            1 => items.Item2,
            2 => items.Item3,
            3 => items.Item4,
            _ => items.Item5
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this IQuint<T> items, System.Random randomGenerator)
    {
        return (randomGenerator.Next() % 5) switch
        {
            0 => items.Item1,
            1 => items.Item2,
            2 => items.Item3,
            3 => items.Item4,
            _ => items.Item5
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this System.Random randomGenerator, IHexad<T> items)
    {
        return (randomGenerator.Next() % 5) switch
        {
            0 => items.Item1,
            1 => items.Item2,
            2 => items.Item3,
            3 => items.Item4,
            4 => items.Item5,
            _ => items.Item6
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this IHexad<T> items, System.Random randomGenerator)
    {
        return (randomGenerator.Next() % 5) switch
        {
            0 => items.Item1,
            1 => items.Item2,
            2 => items.Item3,
            3 => items.Item4,
            4 => items.Item5,
            _ => items.Item6
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetNumber(this System.Random randomGenerator)
    {
        return randomGenerator.NextDouble();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetNumber(this System.Random randomGenerator, double maxValue)
    {
        return maxValue * randomGenerator.NextDouble();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetNumber(this System.Random randomGenerator, double minValue, double maxValue)
    {
        return (maxValue - minValue) * randomGenerator.NextDouble() + minValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetScaledNumber(this System.Random randomGenerator, double scalingFactor)
    {
        return scalingFactor * randomGenerator.NextDouble();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetOffsetScaledNumber(this System.Random randomGenerator, double offsetFactor, double scalingFactor)
    {
        return offsetFactor + scalingFactor * randomGenerator.NextDouble();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetLinearMappedNumber(this System.Random randomGenerator, double minValue, double maxValue)
    {
        return minValue + (maxValue - minValue) * randomGenerator.NextDouble();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetMappedNumber<T>(this System.Random randomGenerator, Func<double, T> mappingFunc)
    {
        return mappingFunc(randomGenerator.NextDouble());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetNumbers(this System.Random randomGenerator, int count)
    {
        while (count > 0)
        {
            yield return randomGenerator.GetNumber();
            count--;
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetNumbers(this System.Random randomGenerator, int count, double minValue, double maxValue)
    {
        while (count > 0)
        {
            yield return randomGenerator.GetNumber(minValue, maxValue);
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetScaledNumbers(this System.Random randomGenerator, int count, double scalingFactor)
    {
        while (count > 0)
        {
            yield return randomGenerator.GetScaledNumber(scalingFactor);
            count--;
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetOffsetScaledNumbers(this System.Random randomGenerator, int count, double offsetFactor, double scalingFactor)
    {
        while (count > 0)
        {
            yield return randomGenerator.GetOffsetScaledNumber(offsetFactor, scalingFactor);
            count--;
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetLinearMappedNumbers(this System.Random randomGenerator, int count, double minValue, double maxValue)
    {
        while (count > 0)
        {
            yield return randomGenerator.GetLinearMappedNumber(minValue, maxValue);
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetMappedNumbers<T>(this System.Random randomGenerator, int count, Func<double, T> mappingFunc)
    {
        while (count > 0)
        {
            yield return randomGenerator.GetMappedNumber(mappingFunc);
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInteger(this System.Random randomGenerator)
    {
        return randomGenerator.Next();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInteger(this System.Random randomGenerator, int maxValue)
    {
        return maxValue < 0
            ? -randomGenerator.Next(-maxValue)
            : randomGenerator.Next(maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInteger(this System.Random randomGenerator, int minValue, int maxValue)
    {
        return randomGenerator.Next(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetMappedInteger<T>(this System.Random randomGenerator, Func<int, T> mappingFunc)
    {
        return mappingFunc(randomGenerator.GetInteger());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetMappedInteger<T>(this System.Random randomGenerator, int maxValue, Func<int, T> mappingFunc)
    {
        return mappingFunc(randomGenerator.GetInteger(maxValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetMappedInteger<T>(this System.Random randomGenerator, int minValue, int maxValue, Func<int, T> mappingFunc)
    {
        return mappingFunc(randomGenerator.GetInteger(minValue, maxValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GetBoolean(this System.Random randomGenerator)
    {
        return randomGenerator.Next() % 2 == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GetBoolean(this System.Random randomGenerator, double trueRatio)
    {
        return randomGenerator.NextDouble() < trueRatio;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetItem<T>(this System.Random randomGenerator, IReadOnlyList<T> itemsList)
    {
        return itemsList[randomGenerator.GetInteger(itemsList.Count)];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> GetIntegers(this System.Random randomGenerator, int count)
    {
        while (count > 0)
        {
            yield return randomGenerator.GetInteger();
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> GetIntegers(this System.Random randomGenerator, int count, int maxValue)
    {
        while (count > 0)
        {
            yield return randomGenerator.GetInteger(maxValue);
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> GetIntegers(this System.Random randomGenerator, int count, int minValue, int maxValue)
    {
        while (count > 0)
        {
            yield return randomGenerator.GetInteger(minValue, maxValue);
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetMappedIntegers<T>(this System.Random randomGenerator, int count, Func<int, T> mappingFunc)
    {
        while (count > 0)
        {
            yield return randomGenerator.GetMappedInteger(mappingFunc);
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetMappedIntegers<T>(this System.Random randomGenerator, int count, int maxValue, Func<int, T> mappingFunc)
    {
        while (count > 0)
        {
            yield return randomGenerator.GetMappedInteger(maxValue, mappingFunc);
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetMappedIntegers<T>(this System.Random randomGenerator, int count, int minValue, int maxValue, Func<int, T> mappingFunc)
    {
        while (count > 0)
        {
            yield return randomGenerator.GetMappedInteger(minValue, maxValue, mappingFunc);
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetItems<T>(this System.Random randomGenerator, int count, IReadOnlyList<T> itemsList)
    {
        var itemsCount = itemsList.Count;

        while (count > 0)
        {
            yield return itemsList[randomGenerator.GetInteger(itemsCount)];
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> GetUniqueIndices(this System.Random randomGenerator, int indicesCount, int itemsCount)
    {
        return Enumerable
            .Range(0, itemsCount)
            .Shuffled(randomGenerator)
            .Take(indicesCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetUniqueItems<T>(this System.Random randomGenerator, int indicesCount, IEnumerable<T> itemsList)
    {
        return itemsList
            .Shuffled(randomGenerator)
            .Take(indicesCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetPermutation<T>(this System.Random randomGenerator, IEnumerable<T> itemsList)
    {
        return itemsList.SelectPermutation(randomGenerator);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex GetComplex(this System.Random randomGenerator)
    {
        return new Complex(
            randomGenerator.NextDouble(),
            randomGenerator.NextDouble()
        );
    }


    public static double GaussianPdf(this double x, double mean = 0, double standardDeviation = 1)
    {
        var d = 1d / (Math.Sqrt(2 * Math.PI) * standardDeviation);

        return d * Math.Exp(-0.5d * Math.Pow((x - mean) / standardDeviation, 2));
    }

    public static IEnumerable<double> GaussianPdf(this IEnumerable<double> xValues, double mean = 0, double standardDeviation = 1)
    {
        var d = 1d / (Math.Sqrt(2 * Math.PI) * standardDeviation);

        return xValues.Select(x => d * Math.Exp(-0.5d * Math.Pow((x - mean) / standardDeviation, 2)));
    }

    public static double CauchyPdf(this double x, double peakLocation = 0, double scale = 1)
    {
        var t = (x - peakLocation) / scale;

        return 1 / (Math.PI * scale * (1 + t * t));
    }

    public static IEnumerable<double> CauchyPdf(this IEnumerable<double> xValues, double peakLocation = 0, double scale = 1)
    {
        var d = 1d / (Math.PI * scale);
        var v = 1d / scale;

        return xValues.Select(x =>
        {
            var t = (x - peakLocation) * v;

            return d / (1 + t * t);
        });
    }
        
    public static double LevyPdf(this double x, double peakLocation = 0, double scale = 1)
    {
        x -= peakLocation;

        return x < 0d
            ? 0d
            : Math.Sqrt(0.5d * scale / Math.PI) *
              Math.Exp(-0.5d * scale / x) /
              Math.Pow(x, 1.5d);
    }
        
    public static IEnumerable<double> LevyPdf(this IEnumerable<double> xValues, double peakLocation = 0, double scale = 1)
    {
        var d = Math.Sqrt(0.5d * scale / Math.PI);

        return xValues.Select(x =>
        {
            x -= peakLocation;

            return x < 0d
                ? 0d
                : d *
                  Math.Exp(-0.5d * scale / x) /
                  Math.Pow(x, 1.5d);
        });
    }

    public static Histogram CreateHistogram(this System.Random randomGenerator, double lowerLimit = -10d, double upperLimit = 10d, int pinsCount = 1024, int samplesCount = 10000000)
    {
        var dataValues = 
            Enumerable
                .Repeat(0, samplesCount)
                .Select(_ => randomGenerator.NextDouble())
                .Where(v => v >= lowerLimit && v <= upperLimit);

        return new Histogram(
            dataValues, 
            pinsCount, 
            lowerLimit, 
            upperLimit
        );
    }

    public static Tuple<double[], double[]> CreateDataArrays(this Histogram hist)
    {
        var xValues = new double[hist.BucketCount];
        var yValues = new double[hist.BucketCount];

        for (var i = 0; i < hist.BucketCount; i++)
        {
            var bucket = hist[i];
                
            xValues[i] = 0.5d * (bucket.UpperBound + bucket.LowerBound);
            yValues[i] = bucket.Count;
        }

        return new Tuple<double[], double[]>(
            xValues,
            yValues
        );
    }

    public static double MaxBucketCount(this Histogram hist)
    {
        var maxValue = double.NegativeInfinity;

        for (var i = 0; i < hist.BucketCount; i++)
        {
            var value = hist[i].Count;
            if (maxValue < value)
                maxValue = value;
        }

        return maxValue;
    }


    #region ---- Java Extensions ----
    ///// <summary>
    ///// Returns a random boolean value
    ///// </summary>
    //public static bool NextBoolean(this System.Random random)
    //{
    //    if (random == null) { throw new ArgumentNullException(nameof(random)); }

    //    return random.NextBits(1) != 0;
    //}

    /// <summary>
    /// Returns a random 32-bit integer
    /// </summary>
    public static int NextInt32(this System.Random random)
    {
        if (random == null) { throw new ArgumentNullException(nameof(random)); }

        return random.NextBits(32);
    }

    /// <summary>
    /// Returns a random 64-bit integer
    /// </summary>
    public static long NextInt64(this System.Random random)
    {
        if (random == null) { throw new ArgumentNullException(nameof(random)); }

        if (random is NextBitsRandom nextBitsRandom)
        {
            return ((long)nextBitsRandom.NextBits(32) << 32) + nextBitsRandom.NextBits(32);
        }

        // NextBits(32) for regular Random requires 2 calls to Next(), or 4 calls
        // total using the method above. Thus, we instead use an approach that requires
        // only 3 calls
        return ((long)random.Next30OrFewerBits(22) << 42)
               + ((long)random.Next30OrFewerBits(21) << 21)
               + random.Next30OrFewerBits(21);
    }

    /// <summary>
    /// Returns a random <see cref="float"/> value in [0, 1)
    /// </summary>
    public static float NextSingle(this System.Random random)
    {
        if (random == null) { throw new ArgumentNullException(nameof(random)); }

        return random.NextBits(24) / ((float)(1 << 24));
    }

    /// <summary>
    /// Return a random number from an exponential distribution with a given mean
    /// </summary>
    /// <param name="random"></param>
    /// <param name="mean"></param>
    /// <returns></returns>
    public static double NextExponential(this System.Random random, double mean)
    {
        if (random == null) { throw new ArgumentNullException(nameof(random)); }

        //https://en.wikipedia.org/wiki/Exponential_distribution#Computational_methods
        var u = random.NextDouble();

        return -mean * Math.Log(u);
    }

    /// <summary>
    /// Returns the sequence of values that would be generated by repeated
    /// calls to <see cref="Random.NextDouble"/>
    /// </summary>
    public static IEnumerable<double> NextDoubles(this System.Random random)
    {
        if (random == null) { throw new ArgumentNullException(nameof(random)); }

        return NextDoublesIterator(random);
    }

    private static IEnumerable<double> NextDoublesIterator(System.Random random)
    {
        while (true)
        {
            yield return random.NextDouble();
        }
    }

    private static int NextBits(this System.Random random, int bits)
    {
        if (random is NextBitsRandom nextBitsRandom)
        {
            return nextBitsRandom.NextBits(bits);
        }

        // simulate with native random methods. 32 bits requires [int.MinValue, int.MaxValue]
        // and 31 bits requires [0, int.MaxValue]

        // 30 or fewer bits needs only one call
        if (bits <= 30)
        {
            return random.Next30OrFewerBits(bits);
        }
            
        var upperBits = random.Next30OrFewerBits(bits - 16) << 16;
        var lowerBits = random.Next30OrFewerBits(16);
        return upperBits | lowerBits;
    }
        
    private static int Next30OrFewerBits(this System.Random random, int bits)
    {
        // a range of bits is [0, 2^bits)
        var maxValue = 1 << bits;

        // Next() returns a value in [0, 2^31 - 1)
        const int nextMaxValue = int.MaxValue;

        // thus to avoid bias, we must throw away values at the top of the range.
        // E. g. if Next() returned [0, 10) we were looking to produce [0, 4), we'd
        // throw away samples of 8 or 9 since these would bias us towards results of 
        // 0 and 1 respectively.
        // NOTE: since maxValue is always a power of 2, this is equivalent to
        // NextMaxValue - (NextMaxValue % maxValue)
        var firstBiasedValue = nextMaxValue - (nextMaxValue & (maxValue - 1));

        int sample;
        do { sample = random.Next(); }
        while (sample >= firstBiasedValue);

        // NOTE: because maxValue is a power of 2, this is equivalent to 
        // sample % maxValue
        return sample & (maxValue - 1);
    }
    #endregion

    #region ---- Weighted Coin Flip ----
    /// <summary>
    /// Returns true with probability <paramref name="probability"/>
    /// </summary>
    public static bool NextBoolean(this System.Random random, double probability)
    {
        if (random == null) 
            throw new ArgumentNullException(nameof(random));

        if (probability == 0) 
            return false;

        if (probability == 1) 
            return true;

        if (probability is < 0 or > 1)
            throw new ArgumentOutOfRangeException(nameof(probability),
                $"{nameof(probability)} must be in [0, 1]. Found {probability}");
            
        if (double.IsNaN(probability)) 
            throw new ArgumentException("must not be NaN", nameof(probability));

        return random.NextDouble() < probability;
    }
    #endregion

    #region ---- Byte Stream ----
    /// <summary>
    /// Returns the sequence of bytes that would be returned by repeated calls
    /// to <see cref="Random.NextBytes(byte[])"/>
    /// </summary>
    public static IEnumerable<byte> NextBytes(this System.Random random)
    {
        if (random == null) { throw new ArgumentNullException(nameof(random)); }

        return NextBytesIterator(random);
    }

    private static IEnumerable<byte> NextBytesIterator(System.Random random)
    {
        var buffer = new byte[256];
        while (true)
        {
            random.NextBytes(buffer);

            for (var i = 0; i < buffer.Length; ++i)
            {
                yield return buffer[i];
            }
        }
    }
    #endregion

    #region ---- Shuffling ----
    /// <summary>
    /// Returns <paramref name="source"/> randomly shuffled using 
    /// <paramref name="random"/> or else <see cref="RandomGeneratorUtils.Current"/>.
    /// 
    /// This method performs a lazy "streaming" shuffle: when the first
    /// element of the returned <see cref="IEnumerable{T}"/> is requested, the
    /// entire <paramref name="source"/> sequence is enumerated. The <paramref name="random"/>
    /// is then used lazily to shuffle the next element into place as the result
    /// sequence is enumerated
    /// </summary>
    public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> source, System.Random random = null)
    {
        if (source == null) { throw new ArgumentNullException(nameof(source)); }

        // note that it is vital that we use SingletonRandom.Instance over ThreadLocalRandom.Instance here,
        // since the iterator could be advanced on different threads thus violating thread-safety
        return ShuffledIterator(source, random ?? SingletonRandom.Instance);
    }

    private static IEnumerable<T> ShuffledIterator<T>(IEnumerable<T> source, System.Random random)
    {
        var list = source.ToList();
        if (list.Count == 0)
        {
            yield break;
        }

        for (var i = 0; i < list.Count - 1; ++i)
        {
            // swap i with a random index and yield the swapped value
            var randomIndex = random.Next(minValue: i, maxValue: list.Count);
            var randomValue = list[randomIndex];
            list[randomIndex] = list[i];
            // note that we don't even have to put randomValue in list[i], because this is a throwaway list!
            yield return randomValue;
        }

        // yield the last value
        yield return list[^1];
    }

    /// <summary>
    /// Shuffles the given <paramref name="list"/> using <paramref name="random"/> 
    /// if provided or else <see cref="RandomGeneratorUtils.Current"/>
    /// </summary>
    public static void Shuffle<T>(this IList<T> list, System.Random random = null)
    {
        if (list == null) { throw new ArgumentNullException(nameof(list)); }

        var rand = random ?? ThreadLocalRandom.Current;

        for (var i = 0; i < list.Count - 1; ++i)
        {
            // swap i with a random index
            var randomIndex = rand.Next(minValue: i, maxValue: list.Count);
            var randomValue = list[randomIndex];
            list[randomIndex] = list[i];
            list[i] = randomValue;
        }
    }
    #endregion

    #region ---- Geoussian ----
    /// <summary>
    /// Returns a normally-distributed double value with mean 0 and standard deviation 1
    /// </summary>
    public static double NextGaussian(this System.Random random)
    {
        if (random == null) 
            throw new ArgumentNullException(nameof(random));

        if (random is INextGaussianRandom nextGaussianRandom)
            return nextGaussianRandom.NextGaussian();

        random.NextTwoGaussians(out var result, out _);
        return result;
    }

    private interface INextGaussianRandom
    {
        double NextGaussian();
    }

    private static void NextTwoGaussians(this System.Random random, out double value1, out double value2)
    {
        double v1, v2, s;
        do
        {
            v1 = 2 * random.NextDouble() - 1; // between -1.0 and 1.0
            v2 = 2 * random.NextDouble() - 1; // between -1.0 and 1.0
            s = v1 * v1 + v2 * v2;
        } while (s is >= 1 or 0);
        var multiplier = Math.Sqrt(-2 * Math.Log(s) / s);

        value1 = v1* multiplier;
        value2 = v2 * multiplier; 
    }
    #endregion

    #region ---- Bounded Doubles ----
    /// <summary>
    /// Returns a random double value uniformly in [0, <paramref name="max"/>). The underlying randomness is
    /// provided by <see cref="Random.NextDouble"/>, which may be unsuitable for very large ranges
    /// </summary>
    public static double NextDouble(this System.Random random, double max)
    {
        if (random == null) { throw new ArgumentNullException(nameof(random)); }
        if (max < 0) { throw new ArgumentOutOfRangeException(nameof(max), max, "must be non-negative"); }
        if (double.IsNaN(max) || double.IsInfinity(max)) { throw new ArgumentException("must not be infinity or NaN", nameof(max)); }

        if (max == 0) { return 0; } // consistent with Next(int)

        return max * random.NextDouble();
    }

    /// <summary>
    /// Returns a random double value uniformly in [<paramref name="min"/>, <paramref name="max"/>). The 
    /// underlying randomness is provided by <see cref="Random.NextDouble"/>, which may be unsuitable for 
    /// very large ranges
    /// </summary>
    public static double NextDouble(this System.Random random, double min, double max)
    {
        if (random == null) { throw new ArgumentNullException(nameof(random)); }
        var range = max - min;
        if (double.IsNaN(range) || double.IsInfinity(range))
        {
            // these are all checked inside a block for both conditions to handle things like
            // Inf - Inf = NaN

            if (double.IsNaN(min)) { throw new ArgumentException("must not be NaN", nameof(min)); };
            if (double.IsNaN(max)) { throw new ArgumentException("must not be NaN", nameof(max)); };
            if (double.IsInfinity(min)) { throw new ArgumentOutOfRangeException(nameof(min), min, "must not be infinite"); }
            if (double.IsInfinity(max)) { throw new ArgumentOutOfRangeException(nameof(max), max, "must not be infinite"); }
            throw new ArgumentOutOfRangeException(nameof(max), max, $"difference between {min} and {max} is too large to be represented by {typeof(double)}");
        }
        if (range < 0) { throw new ArgumentOutOfRangeException(nameof(max), max, "must be greater than or equal to " + nameof(min)); }

        if (range == 0) { return min; } // consistent with Next(int, int)

        return min + (range * random.NextDouble());
    }
    #endregion

    #region ---- ThreadLocal ----  
    /// <summary>
    /// Returns a thread-safe <see cref="Random"/> instance which can be used 
    /// for static random calls
    /// </summary>
    public static System.Random Current => SingletonRandom.Instance;

    /// <summary>
    /// Returns a double value in [0, 1)
    /// </summary>
    public static double NextDouble() => ThreadLocalRandom.Current.NextDouble();

    /// <summary>
    /// Returns an int value in [<paramref name="minValue"/>, <paramref name="maxValue"/>)
    /// </summary>
    public static int Next(int minValue, int maxValue) => ThreadLocalRandom.Current.Next(minValue, maxValue);

    private sealed class SingletonRandom : System.Random, INextGaussianRandom
    {
        public static readonly SingletonRandom Instance = new SingletonRandom();

        private SingletonRandom() : base(0) { }

        public override int Next() => ThreadLocalRandom.Current.Next();

        public override int Next(int maxValue) => ThreadLocalRandom.Current.Next(maxValue);

        public override int Next(int minValue, int maxValue) => ThreadLocalRandom.Current.Next(minValue, maxValue);

        public override void NextBytes(byte[] buffer) => ThreadLocalRandom.Current.NextBytes(buffer);

        public override double NextDouble() => ThreadLocalRandom.Current.NextDouble();

        protected override double Sample() => ThreadLocalRandom.Current.NextDouble();

        double INextGaussianRandom.NextGaussian() => ThreadLocalRandom.Current.NextGaussian();
    }

    private sealed class ThreadLocalRandom : System.Random, INextGaussianRandom
    {
        private static readonly DateTime SeedTime = DateTime.UtcNow;

        [ThreadStatic]
        private static ThreadLocalRandom _currentInstance;

        public static ThreadLocalRandom Current => _currentInstance ?? (_currentInstance = new ThreadLocalRandom());

        private ThreadLocalRandom()
            : base(Seed: HashCombine(HashCombine(SeedTime.GetHashCode(), Thread.CurrentThread.ManagedThreadId), Environment.TickCount))
        {
        }
            
        private double? _nextNextGeoussian;

        public double NextGaussian()
        {
            if (_nextNextGeoussian.HasValue)
            {
                var result = _nextNextGeoussian.Value;
                _nextNextGeoussian = null;
                return result;
            }

            double next, nextNext;
            this.NextTwoGaussians(out next, out nextNext);
            _nextNextGeoussian = nextNext;
            return next;
        }
    }

    // based on Tuple.CombineHashCodes
    private static int HashCombine(int hash1, int hash2) => unchecked(((hash1 << 5) + hash1) ^ hash2);
    #endregion

    #region ---- Factory ---- 
    /// <summary>
    /// Comparable to <code>new Random()</code>, but seeds the <see cref="Random"/> with
    /// a time-dependent value that will still vary greatly across calls to <see cref="Create"/>.
    /// This avoids the problem of many <see cref="Random"/>s created close together being seeded
    /// with the same value
    /// </summary>
    public static System.Random Create() => new System.Random(HashCombine(Environment.TickCount, ThreadLocalRandom.Current.Next()));
    #endregion
        
    #region ---- NextBits Random ----
    private abstract class NextBitsRandom : System.Random, INextGaussianRandom
    {
        // pass through the seed just in case
        protected NextBitsRandom(int seed) : base(seed) { }

        internal abstract int NextBits(int bits);

        #region ---- .NET Random Methods ----
        public sealed override int Next()
        {
            return Next(int.MaxValue);
        }

        public sealed override int Next(int maxValue)
        {
            // see remarks for this special case in the docs:
            // https://msdn.microsoft.com/en-us/library/zd1bc8e5%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
            if (maxValue == 0)
            {
                return 0;
            }
            if (maxValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(maxValue)} must be positive.");
            }

            unchecked
            {
                if ((maxValue & -maxValue) == maxValue)  // i.e., bound is a power of 2
                {
                    return (int)((maxValue * (long)NextBits(31)) >> 31);
                }

                int bits, val;
                do
                {
                    bits = NextBits(31);
                    val = bits % maxValue;
                } while (bits - val + (maxValue - 1) < 0);
                return val;
            }
        }

        public sealed override int Next(int minValue, int maxValue)
        {
            if (minValue == maxValue)
            {
                return minValue;
            }
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue), $"{nameof(minValue)} ({minValue}) must not be > {nameof(maxValue)} ({maxValue})");
            }

            var range = (long)maxValue - minValue;

            // if the range is small, we can use Next(int)
            if (range <= int.MaxValue)
            {
                return minValue + Next(maxValue: (int)range);
            }

            // otherwise, we use java's implementation for 
            // nextLong(long, long)
            var r = NextInt64();
            var m = range - 1;

            // power of two
            if ((range & m) == 0L)
            {
                r = (r & m);
            }
            else
            {
                // reject over-represented candidates
                for (
                    var u = unchecked((long)((ulong)r >> 1)); // ensure non-negative
                    u + m - (r = u % range) < 0; // rejection check
                    u = unchecked((long)((ulong)NextInt64() >> 1)) // retry
                ) ; 
            }

            return checked((int)(r + minValue));
        }

        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }

            for (var i = 0; i < buffer.Length;)
            {
                for (int rand = NextBits(32), n = Math.Min(buffer.Length - i, 4);
                     n-- > 0; 
                     rand >>= 8)
                {
                    buffer[i++] = unchecked((byte)rand);
                }
            }
        }

        public sealed override double NextDouble()
        {
            return Sample();
        }

        protected sealed override double Sample()
        {
            return (((long)NextBits(26) << 27) + NextBits(27)) / (double)(1L << 53);
        }
        #endregion

        private double? _nextNextGeoussian;

        double INextGaussianRandom.NextGaussian()
        {
            if (_nextNextGeoussian.HasValue)
            {
                var result = _nextNextGeoussian.Value;
                _nextNextGeoussian = null;
                return result;
            }

            double next, nextNext;
            this.NextTwoGaussians(out next, out nextNext);
            _nextNextGeoussian = nextNext;
            return next;
        }
    }
    #endregion

    #region ---- Java Random ----
    /// <summary>
    /// Creates a <see cref="Random"/> that uses the same algorithm as the JRE. The <see cref="Random"/>
    /// is seeded with a time-dependent value which will vary greatly even across close-together calls to
    /// <see cref="CreateJavaRandom()"/>
    /// </summary>
    public static System.Random CreateJavaRandom() => CreateJavaRandom(ThreadLocalRandom.Current.NextInt64() ^ Environment.TickCount);

    /// <summary>
    /// Creates a <see cref="Random"/> which replicates the same random sequence as is produced by
    /// the standard random number generator in the JRE using the same <paramref name="seed"/>
    /// </summary>
    public static System.Random CreateJavaRandom(long seed) => new JavaRandom(seed);

    private sealed class JavaRandom : NextBitsRandom
    {
        private long _seed;

        public JavaRandom(long seed)
            // we shouldn't need the seed, but passing it through
            // just in case new Random() methods are added in the future
            // that don't call anything we've overloaded
            : base(unchecked((int)seed))
        {
            // this is based on "initialScramble()" in the Java implementation
            _seed = (seed ^ 0x5DEECE66DL) & ((1L << 48) - 1);
        }

        internal override int NextBits(int bits)
        {
            unchecked
            {
                _seed = ((_seed * 0x5DEECE66DL) + 0xBL) & ((1L << 48) - 1);
                return (int)((ulong)_seed >> (48 - bits));
            }
        }
    }
    #endregion

    #region ---- RandomNumberGenerator Interop ----
    /// <summary>
    /// Returns a <see cref="Random"/> instance which uses the given <paramref name="randomNumberGenerator"/>
    /// as a source of randomness
    /// </summary>
    public static System.Random AsRandom(this RandomNumberGenerator randomNumberGenerator)
    {
        if (randomNumberGenerator == null) { throw new ArgumentNullException(nameof(randomNumberGenerator)); }
            
        return new RandomNumberGeneratorRandom(randomNumberGenerator);
    }
        
    private sealed class RandomNumberGeneratorRandom : NextBitsRandom
    {
        private const int BufferLength = 512;

        private readonly RandomNumberGenerator _rand;
        private readonly byte[] _buffer = new byte[BufferLength];
        private int _nextByteIndex = BufferLength;

        internal RandomNumberGeneratorRandom(RandomNumberGenerator randomNumberGenerator)
            : base(seed: 0) // avoid having to generate a time-based seed 
        {
            _rand = randomNumberGenerator;
        }
            
        internal override int NextBits(int bits)
        {
            // unsigned so we can unsigned shift below
            uint result = 0;
            var i = 0;
            while (true)
            {
                if (_nextByteIndex == BufferLength)
                {
                    _rand.GetBytes(_buffer);
                    _nextByteIndex = 0;
                }
                checked { result += (uint)_buffer[_nextByteIndex++] << i; };

                i += 8;
                if (i >= bits)
                {
                    var nextBits = result >> (i - bits);
                    return unchecked((int)nextBits);
                }
            }
        }

        // we override this for performance reasons, since we can call the underlying RNG's NextBytes() method directly
        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }

            if (buffer.Length <= (BufferLength - _nextByteIndex))
            {
                for (var i = _nextByteIndex; i < buffer.Length; ++i)
                {
                    buffer[i] = _buffer[i];
                }
                _nextByteIndex += buffer.Length;
            }
            else
            {
                _rand.GetBytes(buffer);
            }
        }
    }
    #endregion
}