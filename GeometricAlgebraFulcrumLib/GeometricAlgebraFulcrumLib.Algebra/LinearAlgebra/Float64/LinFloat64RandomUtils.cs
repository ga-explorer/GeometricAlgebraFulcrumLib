﻿using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64;

public static class LinFloat64RandomUtils
{
    public static double[] GetFloat64Array1D(this Random random, int size, double minValue, double maxValue)
    {
        var array = new double[size];

        for (var i = 0; i < size; i++)
            array[i] = random.GetFloat64(minValue, maxValue);

        return array;
    }

    public static double[,] GetFloat64Array2D(this Random random, int rowCount, int colCount, double minValue = -1, double maxValue = 1)
    {
        var array = new double[rowCount, colCount];

        for (var i = 0; i < rowCount; i++)
            for (var j = 0; j < rowCount; j++)
                array[i, j] = random.GetFloat64(minValue, maxValue);

        return array;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] GetCirculantColumnArray(this Random random, int size, double minValue = -1d, double maxValue = 1d)
    {
        return Float64ArrayUtils.CreateCirculantColumnArray(
            random.GetFloat64Numbers(size, minValue, maxValue).ToImmutableArray()
        );
    }

}