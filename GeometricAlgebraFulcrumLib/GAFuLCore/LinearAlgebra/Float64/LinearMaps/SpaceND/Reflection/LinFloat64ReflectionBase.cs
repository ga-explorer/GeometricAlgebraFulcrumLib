﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;

public abstract class LinFloat64ReflectionBase :
    ILinFloat64UnilinearMap
{
    public abstract int VSpaceDimensions { get; }

    public abstract bool SwapsHandedness { get; }

    public abstract bool IsValid();

    public abstract bool IsIdentity();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsReflection()
    {
        return true;
    }

    public abstract bool IsNearIdentity(double zeroEpsilon = 1e-12d);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearReflection(double zeroEpsilon = 1E-12)
    {
        return true;
    }

    public abstract LinFloat64Vector MapBasisVector(int basisIndex);

    public abstract LinFloat64Vector MapVector(LinFloat64Vector vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange()
            .Select(i => new KeyValuePair<int, LinFloat64Vector>(i, MapBasisVector(i)))
            .Where(pair => !pair.Value.IsZero);
    }

    public abstract LinFloat64ReflectionBase GetReflectionLinearMapInverse();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        var columnList =
            colCount
                .GetRange()
                .Select(i => MapBasisVector(i).ToArray(rowCount));

        return Matrix<double>
            .Build
            .DenseOfColumnArrays(columnList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions)
    {
        return vSpaceDimensions.CreateLinUnilinearMap(MapBasisVector);
    }

    public virtual double[,] ToArray(int rowCount, int colCount)
    {
        var array = new double[VSpaceDimensions, VSpaceDimensions];

        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var columnVector = MapBasisVector(j);

            if (columnVector.IsZero)
                continue;

            foreach (var (i, s) in columnVector)
                array[i, j] = s;
        }

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64UnilinearMap GetInverseMap()
    {
        return GetReflectionLinearMapInverse();
    }

    public abstract LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence();
}