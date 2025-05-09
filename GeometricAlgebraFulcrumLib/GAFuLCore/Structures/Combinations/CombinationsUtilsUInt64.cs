using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

public static class CombinationsUtilsUInt64
{
    private static PascalTriangleUInt64 PascalTriangle { get; }
        = new PascalTriangleUInt64(64);


    public static int MaxSetSize 
        => PascalTriangle.RowsCount - 1;


    public static int GetMaximalRowIndex(ulong value, int columnIndex)
    {
        for (var rowIndex = columnIndex; rowIndex <= MaxSetSize; rowIndex++)
        {
            if (PascalTriangle[rowIndex, columnIndex] > value)
                return rowIndex - 1;
        }

        return MaxSetSize;
    }

    public static IEnumerable<int> IndexToCombinadic(this ulong index, int digitsCount)
    {
        if (digitsCount < 0 || digitsCount > MaxSetSize)
            throw new InvalidOperationException();

        if (index >= GetBinomialCoefficient(MaxSetSize, digitsCount))
            throw new InvalidOperationException();


        if (digitsCount == 0 && index == 0)
            yield break;

        if (digitsCount == MaxSetSize && index == 0)
        {
            for (var i = 0; i < MaxSetSize; i++)
                yield return i;

            yield break;
        }


        //var combinadicArray = new int[digitsCount];

        while (digitsCount > 0)
        {
            var n = GetMaximalRowIndex(index, digitsCount);

            index -= GetBinomialCoefficient(n, digitsCount);

            digitsCount--;

            yield return n;
            //combinadicArray[digitsCount] = n;
        }

        //return combinadicArray;
    }

    public static ulong IndexToCombinadicPattern(this ulong index, int digitsCount)
    {
        if (digitsCount < 0 || digitsCount > MaxSetSize)
            throw new InvalidOperationException();

        if (index >= GetBinomialCoefficient(MaxSetSize, digitsCount))
            throw new InvalidOperationException();


        if (digitsCount == 0 && index == 0)
            return 0UL;

        if (digitsCount == MaxSetSize && index == 0)
            return ulong.MaxValue;


        var combinadicPattern = 0UL;

        while (digitsCount > 0)
        {
            var n = GetMaximalRowIndex(index, digitsCount);

            index -= GetBinomialCoefficient(n, digitsCount);

            digitsCount--;

            combinadicPattern |= 1ul << n;
        }

        return combinadicPattern;
    }

    public static ulong CombinadicPatternToIndex(this ulong combinadicPattern)
    {
        var index = 0UL;
        var onesCount = 0;
        var n = 0;

        while (combinadicPattern > 0)
        {
            if ((combinadicPattern & 1UL) != 0)
            {
                onesCount++;

                index += GetBinomialCoefficient(n, onesCount);
            }

            n++;
            combinadicPattern >>= 1;
        }

        return index;
    }
        
    public static void CombinadicPatternToIndex(this ulong combinadicPattern, out int onesCount, out ulong index)
    {
        index = 0UL;
        onesCount = 0;

        var n = 0;
        while (combinadicPattern > 0)
        {
            if ((combinadicPattern & 1UL) != 0)
            {
                onesCount++;

                index += GetBinomialCoefficient(n, onesCount);
            }

            n++;
            combinadicPattern >>= 1;
        }
    }

    /// <summary>
    /// Compute the binomial coefficient C(n, k) where n is the set size and k is the subset size
    /// https://www.developertyrone.com/blog/generating-the-mth-lexicographical-element-of-a-mathematical-combination/
    /// </summary>
    /// <param name="setSize"></param>
    /// <param name="subsetSize"></param>
    /// <returns></returns>
    public static ulong ComputeBinomialCoefficient(this int setSize, int subsetSize)
    {
        if (setSize < 0)
            throw new ArgumentOutOfRangeException(nameof(setSize));

        if (subsetSize < 0)
            throw new ArgumentOutOfRangeException(nameof(subsetSize));

        if (setSize < subsetSize)
            return 0;

        if (subsetSize == 0 || setSize == subsetSize)
            return 1;

        ulong delta, iMax;

        if (subsetSize < setSize - subsetSize) // ex: Choose(100,3)
        {
            delta = (ulong)(setSize - subsetSize);
            iMax = (ulong)subsetSize;
        }
        else         // ex: Choose(100,97)
        {
            delta = (ulong)subsetSize;
            iMax = (ulong)(setSize - subsetSize);
        }

        var ans = delta + 1;

        for (var i = 2ul; i <= iMax; ++i)
        {
            checked { ans = (ans * (delta + i)) / i; }
        }

        return ans;
    }

    /// <summary>
    /// Compute the binomial coefficient C(n, 2) where n is the set size using the simple relation:
    /// C(n, 2) = n * (n - 1) / 2
    /// </summary>
    /// <param name="setSize"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ComputeTake2BinomialCoefficient(int setSize)
    {
        if (setSize < 0)
            throw new ArgumentOutOfRangeException(nameof(setSize));

        if (setSize < 2)
            return 0;

        var n = (ulong) setSize;
        return (n * (n - 1UL)) >> 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetBinomialCoefficient(this uint setSize, uint subsetSize)
    {
        if (setSize < subsetSize) return 0UL;

        return setSize > MaxSetSize 
            ? ComputeBinomialCoefficient((int) setSize, (int) subsetSize) 
            : PascalTriangle[(int) setSize, (int) subsetSize];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetBinomialCoefficient(this int setSize, int subsetSize)
    {
        if (setSize < 0 || subsetSize < 0)
            throw new ArgumentOutOfRangeException();

        if (setSize < subsetSize) return 0UL;

        return setSize > MaxSetSize 
            ? ComputeBinomialCoefficient(setSize, subsetSize) 
            : PascalTriangle[setSize, subsetSize];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetMaxBinomialCoefficient(this int setSize)
    {
        if (setSize < 0)
            throw new ArgumentOutOfRangeException();

        var subsetSize = setSize.IsEven()
            ? setSize / 2
            : (setSize - 1) / 2;

        return setSize > MaxSetSize 
            ? ComputeBinomialCoefficient(setSize, subsetSize) 
            : PascalTriangle[setSize, subsetSize];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<ulong> GetBinomialCoefficients(this int setSize)
    {
        if (setSize < 0)
            throw new ArgumentOutOfRangeException();

        var halfRow = 
            setSize <= MaxSetSize
                ? PascalTriangle.GetRow(setSize)
                : (setSize / 2 + 1).GetRange().Select(subsetSize => ComputeBinomialCoefficient(setSize, subsetSize)).ToImmutableArray();
            
        var fullRow = new ulong[setSize + 1];

        for (var k = 0; k < halfRow.Count; k++)
        {
            var c = halfRow[k];

            fullRow[k] = c;
            fullRow[setSize - k] = c;
        }

        return fullRow;
    }
}