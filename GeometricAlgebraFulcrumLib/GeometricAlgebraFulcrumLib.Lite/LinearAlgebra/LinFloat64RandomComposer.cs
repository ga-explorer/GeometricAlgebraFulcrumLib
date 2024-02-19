﻿using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;

public class LinFloat64RandomComposer :
    ScalarRandomComposer<double>
{
    public int VSpaceDimensions { get; }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinFloat64RandomComposer(int vSpaceDimensions)
        : base(ScalarProcessorOfFloat64.DefaultProcessor)
    {
        if (vSpaceDimensions is < 2 or > 31)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinFloat64RandomComposer(int vSpaceDimensions, int seed)
        : base(ScalarProcessorOfFloat64.DefaultProcessor, seed)
    {
        if (vSpaceDimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinFloat64RandomComposer(int vSpaceDimensions, System.Random randomGenerator)
        : base(ScalarProcessorOfFloat64.DefaultProcessor, randomGenerator)
    {
        if (vSpaceDimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetBasisVectorIndex()
    {
        return RandomGenerator.Next(VSpaceDimensions);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Dictionary<int, double> GetVectorIndexScalarDictionary()
    {
        return Enumerable
            .Range(0, VSpaceDimensions)
            .ToDictionary(
                index => index,
                _ => GetScalarValue()
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Dictionary<int, double> GetVectorIndexScalarDictionary(double minValue, double maxValue)
    {
        return Enumerable
            .Range(0, VSpaceDimensions)
            .ToDictionary(
                index => index,
                _ => GetScalarValue(minValue, maxValue)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector GetBasisVector()
    {
        var id = GetBasisVectorIndex();

        return new LinBasisVector(id);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinSignedBasisVector GetSignedBasisVector()
    {
        var sign = RandomGenerator.GetSign();

        return GetBasisVector().ToSignedBasisVector(sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64VectorTerm GetVectorTerm()
    {
        var scalar = GetScalarValue();
        var basis = GetBasisVector();

        return basis.ToTerm(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64VectorTerm GetVectorTerm(int index)
    {
        var scalar = GetScalarValue();

        return index.ToLinBasisVector().ToTerm(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector GetVector(bool sparseVector = true)
    {
        if (sparseVector)
        {
            var termsCount = RandomGenerator.Next(VSpaceDimensions);

            var indexScalarDictionary =
                VSpaceDimensions
                    .GetRange()
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .ToDictionary(
                        p => p,
                        _ => GetScalarValue()
                    );

            return indexScalarDictionary.CreateLinVector();
        }

        var scalarArray =
            VSpaceDimensions
                .GetRange()
                .Select(_ => GetScalarValue());

        return scalarArray.CreateLinVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector GetSparseVector(int termsCount)
    {
        if (termsCount > VSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(termsCount));

        var indexScalarDictionary =
            VSpaceDimensions
                .GetRange()
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .ToDictionary(
                    p => p,
                    _ => GetScalarValue()
                );

        return indexScalarDictionary.CreateLinVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetScalars(int count)
    {
        return Enumerable
            .Range(0, count)
            .Select(_ => GetScalarValue());
    }

    public double[,] GetArray(int rowsCount, int columnsCount)
    {
        var array = new double[rowsCount, columnsCount];

        for (var i = 0; i < rowsCount; i++)
        for (var j = 0; j < columnsCount; j++)
            array[i, j] = GetScalarValue();

        return array;
    }

    public double[,] GetPermutationArray(int size)
    {
        var array = new double[size, size];

        var indexList = Enumerable
            .Range(0, size)
            .Shuffled(RandomGenerator);

        var i = 0;
        foreach (var colIndex in indexList)
        {
            for (var j = 0; j < size; j++)
                array[i, j] = j == colIndex
                    ? 1d
                    : 0d;

            i++;
        }

        return array;
    }
}