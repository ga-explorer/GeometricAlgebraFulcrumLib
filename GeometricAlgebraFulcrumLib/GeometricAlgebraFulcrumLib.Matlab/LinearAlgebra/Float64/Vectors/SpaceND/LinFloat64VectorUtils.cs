using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Random;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;

public static class LinFloat64VectorUtils
{
    
    public static bool IsValidLinVectorDictionary(this IReadOnlyDictionary<int, double> indexScalarDictionary)
    {
        return indexScalarDictionary.Count switch
        {
            0 => indexScalarDictionary is EmptyDictionary<int, double>,

            1 => indexScalarDictionary is SingleItemDictionary<int, double> dict &&
                 dict.Key >= 0 &&
                 dict.Value.IsValid() &&
                 !dict.Value.IsZero(),

            _ => indexScalarDictionary.All(p =>
                p.Key >= 0 &&
                p.Value.IsValid() &&
                !p.Value.IsZero()
            )
        };
    }

    
    public static double GetVectorTermScalar(this IReadOnlyList<double> vector, int index)
    {
        return index < 0 || index >= vector.Count
            ? 0d : vector[index];
    }

    
    public static double GetVectorTermScalar(this IReadOnlyDictionary<int, double> vector, int index)
    {
        return vector.TryGetValue(index, out var scalar)
            ? scalar
            : 0d;
    }


    
    public static LinFloat64RandomComposer CreateLinRandomComposer(this int vSpaceDimensions)
    {
        return new LinFloat64RandomComposer(vSpaceDimensions);
    }

    
    public static LinFloat64RandomComposer CreateLinRandomComposer(this int vSpaceDimensions, int seed)
    {
        return new LinFloat64RandomComposer(vSpaceDimensions, seed);
    }

    
    public static LinFloat64RandomComposer CreateLinRandomComposer(this int vSpaceDimensions, Random randomGenerator)
    {
        return new LinFloat64RandomComposer(vSpaceDimensions, randomGenerator);
    }



    //
    //public static Color ToColorRgba(this Float64Vector vector)
    //{
    //    Debug.Assert(
    //        vector[0] >= 0d && vector[0] <= 1d &&
    //        vector[1] >= 0d && vector[1] <= 1d &&
    //        vector[2] >= 0d && vector[2] <= 1d &&
    //        vector[3] >= 0d && vector[3] <= 1d
    //    );

    //    return Color.FromRgba(
    //        (byte)(vector[0] * 255),
    //        (byte)(vector[1] * 255),
    //        (byte)(vector[2] * 255),
    //        (byte)(vector[3] * 255)
    //    );
    //}

    //
    //public static Color ToColorRgb(this Float64Vector vector)
    //{
    //    Debug.Assert(
    //        vector[0] >= 0d && vector[0] <= 1d &&
    //        vector[1] >= 0d && vector[1] <= 1d &&
    //        vector[2] >= 0d && vector[2] <= 1d
    //    );

    //    return Color.FromRgb(
    //        (byte)(vector[0] * 255),
    //        (byte)(vector[1] * 255),
    //        (byte)(vector[2] * 255)
    //    );
    //}

    
    public static LinFloat64Vector GetLinVector(this Random random, int dimensions)
    {
        var vector =
            MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(
                dimensions,
                _ => random.NextDouble()
            );

        return LinFloat64Vector.Create(vector);
    }

    
    public static LinFloat64Vector GetLinVector(this Random random, int dimensions, double minValue, double maxValue)
    {
        var vector =
            MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(
                dimensions,
                _ => random.NextDouble(minValue, maxValue)
            );

        return LinFloat64Vector.Create(vector);
    }

    
    public static LinFloat64Vector GetSparseLinVector(this Random random, int dimensions)
    {
        var vector =
            MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Sparse(dimensions);

        var count = random.Next(1, dimensions);
        var indexList = random.GetDistinctIndices(count, dimensions);

        foreach (var index in indexList)
            vector[index] = random.NextDouble();

        return LinFloat64Vector.Create(vector);
    }

    
    public static LinFloat64Vector GetSparseLinVector(this Random random, int dimensions, int count)
    {
        if (count > dimensions)
            count = dimensions;

        var vector =
            MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Sparse(dimensions);

        var indexList = random.GetDistinctIndices(count, dimensions);

        foreach (var index in indexList)
            vector[index] = random.NextDouble();

        return LinFloat64Vector.Create(vector);
    }

    
    public static LinFloat64Vector GetSparseLinVector(this Random random, int dimensions, double minValue, double maxValue)
    {
        var vector =
            MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Sparse(dimensions);

        var count = random.Next(1, dimensions);
        var indexList = random.GetDistinctIndices(count, dimensions);

        foreach (var index in indexList)
            vector[index] = random.NextDouble(minValue, maxValue);

        return LinFloat64Vector.Create(vector);
    }


    
    public static LinFloat64Vector ToLinVector(this double[] itemArray, bool normalize = false)
    {
        if (normalize)
            itemArray.VectorNormalizeInPlace();

        return LinFloat64Vector.Create(itemArray);
    }


    
    public static Tuple<bool, double, LinBasisVector> TryVectorToAxis(this IReadOnlyDictionary<int, double> itemArray)
    {
        if (itemArray.Count != 1)
            return new Tuple<bool, double, LinBasisVector>(
                false,
                0d,
                LinBasisVector.Px
            );

        var (basisIndex, scalar) = itemArray.First().ToTuple();

        return new Tuple<bool, double, LinBasisVector>(
            true,
            scalar.Abs(),
            LinBasisVector.Create(basisIndex, scalar < 0)
        );
    }

    public static Tuple<bool, double, LinBasisVector> TryVectorToAxis(this IReadOnlyList<double> itemArray)
    {
        var dimensions = itemArray.Count;

        // Find if the given scaling vector is parallel to a basis vector
        var basisIndex = -1;
        for (var i = 0; i < dimensions; i++)
        {
            if (itemArray[i].IsZero()) continue;

            if (basisIndex >= 0)
            {
                basisIndex = -2;
                break;
            }

            basisIndex = i;
        }

        if (basisIndex < 0)
            return new Tuple<bool, double, LinBasisVector>(
                false,
                0d,
                LinBasisVector.Px
            );

        var scalar = itemArray[basisIndex];

        return new Tuple<bool, double, LinBasisVector>(
            true,
            scalar.Abs(),
            LinBasisVector.Create(basisIndex, scalar < 0)
        );
    }

    public static Tuple<bool, double, LinBasisVector> TryVectorToNearAxis(this double[] itemArray, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var dimensions = itemArray.Length;

        // Find if the given scaling vector is parallel to a basis vector
        var basisIndex = -1;
        for (var i = 0; i < dimensions; i++)
        {
            if (itemArray[i].IsNearZero(zeroEpsilon)) continue;

            if (basisIndex >= 0)
            {
                basisIndex = -2;
                break;
            }

            basisIndex = i;
        }

        if (basisIndex < 0)
            return new Tuple<bool, double, LinBasisVector>(
                false,
                0d,
                LinBasisVector.Px
            );

        var scalar = itemArray[basisIndex];
        return new Tuple<bool, double, LinBasisVector>(
            true,
            scalar.Abs(),
            LinBasisVector.Create(basisIndex, scalar < 0)
        );
    }


    
    public static LinFloat64Vector Lerp(this double t, LinFloat64Vector v1, LinFloat64Vector v2)
    {
        Debug.Assert(
            v1.IsValid() &&
            v2.IsValid() && t.IsValid()
        );

        return (1.0d - t) * v1 + t * v2;
    }


    
    public static Pair<double> GetTupleItemPair(this IReadOnlyList<LinFloat64Vector> itemArray, int index, int itemIndex)
    {
        return new Pair<double>(
            itemArray[index][itemIndex],
            itemArray[index + 1][itemIndex]
        );
    }

    
    public static Triplet<double> GetTupleItemTriplet(this IReadOnlyList<LinFloat64Vector> itemArray, int index, int itemIndex)
    {
        return new Triplet<double>(
            itemArray[index][itemIndex],
            itemArray[index + 1][itemIndex],
            itemArray[index + 2][itemIndex]
        );
    }

    
    public static Quad<double> GetTupleItemQuad(this IReadOnlyList<LinFloat64Vector> itemArray, int index, int itemIndex)
    {
        return new Quad<double>(
            itemArray[index][itemIndex],
            itemArray[index + 1][itemIndex],
            itemArray[index + 2][itemIndex],
            itemArray[index + 3][itemIndex]
        );
    }

    
    public static Quint<double> GetTupleItemQuint(this IReadOnlyList<LinFloat64Vector> itemArray, int index, int itemIndex)
    {
        return new Quint<double>(
            itemArray[index][itemIndex],
            itemArray[index + 1][itemIndex],
            itemArray[index + 2][itemIndex],
            itemArray[index + 3][itemIndex],
            itemArray[index + 4][itemIndex]
        );
    }

    
    public static Hexad<double> GetTupleItemHexad(this IReadOnlyList<LinFloat64Vector> itemArray, int index, int itemIndex)
    {
        return new Hexad<double>(
            itemArray[index][itemIndex],
            itemArray[index + 1][itemIndex],
            itemArray[index + 2][itemIndex],
            itemArray[index + 3][itemIndex],
            itemArray[index + 4][itemIndex],
            itemArray[index + 5][itemIndex]
        );
    }
}