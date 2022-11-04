using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps;

public sealed class ClarkeRotation
{
    /// <summary>
    /// See the paper "Generalized Clarke Components for Poly-phase Networks", 1969
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    private static double[,] CreateClarkeArrayOdd(int size)
    {
        var clarkeArray = new double[size, size];

        var s = (2d / size).Sqrt(); 
        //$"Sqrt[2 / {m}]";

        // m is odd, fill all rows except the last
        var n = (size - 1) / 2;
        for (var k = 0; k < n; k++)
        {
            var rowIndex1 = 2 * k;
            var rowIndex2 = 2 * k + 1;

            clarkeArray[rowIndex1, 0] = s;
            clarkeArray[rowIndex2, 0] = 0d;

            for (var colIndex = 1; colIndex < size; colIndex++)
            {
                var angle = 2d * Math.PI * (k + 1) * colIndex / size; 
                // $"2 * Pi * {k + 1} * {i} / {m}";

                var cosAngle = s * Math.Cos(angle); // $"{s} * Cos[{angle}]";
                var sinAngle = s * Math.Sin(angle); // $"{s} * Sin[{angle}]";

                clarkeArray[rowIndex1, colIndex] = cosAngle;
                clarkeArray[rowIndex2, colIndex] = sinAngle;
            }
        }

        //Fill the last column
        var v = 1d / Math.Sqrt(size); 
        // $"1 / Sqrt[{m}]";

        for (var colIndex = 0; colIndex < size; colIndex++)
            clarkeArray[size - 1, colIndex] = v;

        return clarkeArray;
    }

    /// <summary>
    /// See the paper "Generalized Clarke Components for Poly-phase Networks", 1969
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    private static double[,] CreateClarkeArrayEven(int size)
    {
        var clarkeArray = new double[size, size];

        var s = Math.Sqrt(2d / size); 
        //$"Sqrt[2 / {m}]";

        //m is even, fill all rows except the last two
        var n = (size - 1) / 2;
        for (var k = 0; k < n; k++)
        {
            var rowIndex1 = 2 * k;
            var rowIndex2 = 2 * k + 1;

            clarkeArray[rowIndex1, 0] = s;
            clarkeArray[rowIndex2, 0] = 0d;

            for (var colIndex = 1; colIndex < size; colIndex++)
            {
                var angle = 2d * Math.PI * (k + 1) * colIndex / size; 
                // $"2 * Pi * {k + 1} * {i} / {m}";

                var cosAngle = s * Math.Cos(angle); // $"{s} * Cos[{angle}]";
                var sinAngle = s * Math.Sin(angle); // $"{s} * Sin[{angle}]";
                
                clarkeArray[rowIndex1, colIndex] = cosAngle;
                clarkeArray[rowIndex2, colIndex] = sinAngle;
            }
        }

        //Fill the last two rows
        var v0 = 1d / Math.Sqrt(size); 
        // $"1 / Sqrt[{m}]";

        var v1 = -v0; 
        // $"-1 / Sqrt[{m}]";

        for (var colIndex = 0; colIndex < size; colIndex++)
        {
            clarkeArray[size - 2, colIndex] = colIndex % 2 == 0 ? v0 : v1;
            clarkeArray[size - 1, colIndex] = v0;
        }

        return clarkeArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] CreateClarkeArray(int size)
    {
        return size % 2 == 0
            ? CreateClarkeArrayEven(size)
            : CreateClarkeArrayOdd(size);
    }


    private readonly double[,] _clarkeArray;

    public int Size 
        => _clarkeArray.GetLength(0);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ClarkeRotation(int size)
    {
        _clarkeArray = CreateClarkeArray(size);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple Rotate(Float64DenseTuple x)
    {
        return new Float64DenseTuple(
            _clarkeArray.MatrixProduct(x)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Float64DenseTuple> Rotate(params Float64DenseTuple[] xList)
    {
        return xList.Select(Rotate).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64DenseTuple> Rotate(IEnumerable<Float64DenseTuple> xList)
    {
        return xList.Select(Rotate);
    }
}