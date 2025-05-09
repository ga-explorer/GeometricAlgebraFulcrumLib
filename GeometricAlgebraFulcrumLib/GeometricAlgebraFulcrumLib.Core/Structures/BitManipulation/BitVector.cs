using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;

public sealed class BitVector :
    IReadOnlyList<bool>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BitVector CreateAllTrue(int wordCount)
    {
        return new BitVector(
            Enumerable.Repeat(ulong.MaxValue, wordCount).ToArray()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BitVector CreateAllFalse(int wordCount)
    {
        return new BitVector(
            new ulong[wordCount]
        );
    }

    public static BitVector Create(IEnumerable<bool> booleanList)
    {
        var wordArray = new List<ulong>();
        var word = 0UL;

        var i = 0;

        foreach (var booleanValue in booleanList)
        {
            if (booleanValue) 
                word |= 1UL << i;

            i++;

            if (i < 64) continue;

            i = 0;
            wordArray.Add(word);
        }
        
        if (i > 0)
            wordArray.Add(word);

        return new BitVector(wordArray.ToArray());
    }
    
    public static BitVector CreateRandom(int wordCount, System.Random randomGenerator)
    {
        var wordArray = new ulong[wordCount];

        for (var i = 0; i < wordCount; i++)
            wordArray[i] = (ulong)randomGenerator.NextInt64();

        return new BitVector(wordArray);
    }


    public static BitVector operator ~(BitVector v1)
    {
        var wordArray = new ulong[v1.WordCount];

        for (var i = 0; i < v1.WordCount; i++)
            wordArray[i] = ~v1._wordArray[i];

        return new BitVector(wordArray);
    }

    public static BitVector operator &(BitVector v1, BitVector v2)
    {
        Debug.Assert(
            v1.WordCount == v2.WordCount
        );

        var wordArray = new ulong[v1.WordCount];

        for (var i = 0; i < v1.WordCount; i++)
            wordArray[i] = v1._wordArray[i] & v2._wordArray[i];

        return new BitVector(wordArray);
    }

    public static BitVector operator |(BitVector v1, BitVector v2)
    {
        Debug.Assert(
            v1.WordCount == v2.WordCount
        );

        var wordArray = new ulong[v1.WordCount];

        for (var i = 0; i < v1.WordCount; i++)
            wordArray[i] = v1._wordArray[i] | v2._wordArray[i];

        return new BitVector(wordArray);
    }

    public static BitVector operator ^(BitVector v1, BitVector v2)
    {
        Debug.Assert(
            v1.WordCount == v2.WordCount
        );

        var wordArray = new ulong[v1.WordCount];

        for (var i = 0; i < v1.WordCount; i++)
            wordArray[i] = v1._wordArray[i] ^ v2._wordArray[i];

        return new BitVector(wordArray);
    }


    private readonly ulong[] _wordArray;


    public bool IsAllTrue
        => _wordArray.All(w => w == ulong.MaxValue);

    public bool IsAllFalse
        => _wordArray.All(w => w == 0UL);

    public int TrueCount
        => _wordArray.Sum(BitOperations.PopCount);

    public int FalseCount
        => Count - TrueCount;

    public int WordCount 
        => _wordArray.Length;

    public int Count
        => WordCount << 6;

    public bool this[int index]
    {
        get
        {
            var mask = 1UL << (index & 63);

            return (_wordArray[index >> 6] & mask) != 0;
        }
        set
        {
            var mask = 1UL << (index & 63);

            if (value)
                _wordArray[index >> 6] |= mask;
            else
                _wordArray[index >> 6] &= ~mask;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal BitVector(ulong[] wordArray)
    {
        _wordArray = wordArray;
    }


    public IEnumerable<int> GetTruePositions()
    {
        for (var i = 0; i < WordCount; i++)
        {
            var firstPosition = i << 6;

            var positionList =
                _wordArray[i]
                    .GetSetBitPositions()
                    .Select(p => p + firstPosition);

            foreach (var position in positionList)
                yield return position;
        }
    }

    public IEnumerable<int> GetFalsePositions()
    {
        for (var i = 0; i < WordCount; i++)
        {
            var firstPosition = i << 6;

            var positionList =
                (~_wordArray[i])
                    .GetSetBitPositions()
                    .Select(p => p + firstPosition);

            foreach (var position in positionList)
                yield return position;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BitVector GetCopy()
    {
        var wordArray = new ulong[WordCount];

        _wordArray.CopyTo(wordArray, 0);

        return new BitVector(wordArray);
    }
    
    public BitVector SetAllBits(bool value)
    {
        var word = value ? ulong.MaxValue : 0UL;

        for (var i = 0; i < WordCount; i++)
            _wordArray[i] = word;

        return this;
    }

    public BitVector InPlaceNot()
    {
        for (var i = 0; i < WordCount; i++)
            _wordArray[i] = ~_wordArray[i];

        return this;
    }

    public BitVector InPlaceOr(BitVector v2, bool notFlag = false)
    {
        if (notFlag)
        {
            for (var i = 0; i < WordCount; i++)
                _wordArray[i] |= ~v2._wordArray[i];
        }
        else
        {
            for (var i = 0; i < WordCount; i++)
                _wordArray[i] |= v2._wordArray[i];
        }

        return this;
    }

    public BitVector InPlaceAnd(BitVector v2, bool notFlag = false)
    {
        if (notFlag)
        {
            for (var i = 0; i < WordCount; i++)
                _wordArray[i] &= ~v2._wordArray[i];
        }
        else
        {
            for (var i = 0; i < WordCount; i++)
                _wordArray[i] &= v2._wordArray[i];
        }

        return this;
    }

    public BitVector InPlaceXor(BitVector v2, bool notFlag = false)
    {
        if (notFlag)
        {
            for (var i = 0; i < WordCount; i++)
                _wordArray[i] ^= ~v2._wordArray[i];
        }
        else
        {
            for (var i = 0; i < WordCount; i++)
                _wordArray[i] ^= v2._wordArray[i];
        }

        return this;
    }

    public IEnumerator<bool> GetEnumerator()
    {
        foreach (var word in _wordArray)
            for (var i = 0; i < 64; i++)
                yield return (word & 1UL << i) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}