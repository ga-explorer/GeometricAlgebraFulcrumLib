using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using MathNet.Numerics.LinearAlgebra;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Scaling;

public sealed class VectorDirectionalScalingSequence :
    ILinearMap,
    IReadOnlyList<VectorDirectionalScaling>
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static VectorDirectionalScalingSequence CreateFromMatrix(Matrix<double> matrix)
    //{
    //    // Make sure it's a square matrix
    //    Debug.Assert(
    //        matrix.RowCount == matrix.ColumnCount
    //    );

    //    var mapList = 
    //        matrix
    //            .GetSimpleEigenSubspaces()
    //            .SelectMany(s => s.GetVectorDirectionalScalingMaps())
    //            //.Select(s => s.ToVectorDirectionalScaling())
    //            .ToList();

    //    return new VectorDirectionalScalingSequence(matrix.RowCount, mapList);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorDirectionalScalingSequence CreateFromMatrix(Matrix<double> matrix)
    {
        return matrix.GetVectorDirectionalScalingSequence();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorDirectionalScalingSequence Create(int dimensions)
    {
        return new VectorDirectionalScalingSequence(dimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorDirectionalScalingSequence Create(VectorDirectionalScaling scaling)
    {
        var reflectionSequence =
            new VectorDirectionalScalingSequence(scaling.Dimensions);

        reflectionSequence.AppendMap(scaling);

        return reflectionSequence;
    }


    private readonly List<VectorDirectionalScaling> _mapList
        = new List<VectorDirectionalScaling>();


    public int Count
        => _mapList.Count;

    public VectorDirectionalScaling this[int index] 
        => _mapList[index];
    
    public int Dimensions { get; }

    public bool SwapsHandedness
        => _mapList
            .Count(m => m.SwapsHandedness)
            .IsOdd();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VectorDirectionalScalingSequence(int dimensions)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        Dimensions = dimensions;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VectorDirectionalScalingSequence(int dimensions, List<VectorDirectionalScaling> scalingFactorVectorList)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        Dimensions = dimensions;
        _mapList = scalingFactorVectorList;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorDirectionalScalingSequence AppendMap(double scalingFactor, Float64Tuple scalingVector)
    {
        if (scalingVector.Dimensions != Dimensions)
            throw new ArgumentException();

        _mapList.Add(
            VectorDirectionalScaling.Create(scalingFactor, scalingVector)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorDirectionalScalingSequence AppendMap(VectorDirectionalScaling scaling)
    {
        if (scaling.Dimensions != Dimensions)
            throw new ArgumentException();

        _mapList.Add(scaling);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorDirectionalScalingSequence AppendMaps(IEnumerable<VectorDirectionalScaling> scalingList)
    {
        foreach (var scaling in scalingList)
            AppendMap(scaling);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorDirectionalScalingSequence PrependMap(VectorDirectionalScaling scaling)
    {
        if (scaling.Dimensions != Dimensions)
            throw new ArgumentException();

        _mapList.Insert(0, scaling);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorDirectionalScalingSequence InsertMap(int index, VectorDirectionalScaling scaling)
    {
        if (scaling.Dimensions != Dimensions)
            throw new ArgumentException();

        _mapList.Insert(index, scaling);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _mapList.All(a => a.IsValid());
    }

    public bool IsIdentity()
    {
        if (_mapList.Count == 0)
            return true;

        for (var basisIndex = 0; basisIndex < Dimensions; basisIndex++)
        {
            var isSameVectorBasis =
                MapVectorBasis(basisIndex).IsVectorBasis(basisIndex);

            if (!isSameVectorBasis) return false;
        }

        return true;
    }

    public bool IsNearIdentity(double epsilon = 1E-12)
    {
        for (var basisIndex = 0; basisIndex < Dimensions; basisIndex++)
        {
            var isSameVectorBasis =
                MapVectorBasis(basisIndex).IsNearVectorBasis(basisIndex, epsilon);

            if (!isSameVectorBasis) return false;
        }

        return true;
    }

    public double[] MapVectorInPlace(double[] vector)
    {
        foreach (var scaling in _mapList)
        {
            var u = scaling.ScalingVector.ScalarArray;
            var s = (scaling.ScalingFactor - 1d) * vector.VectorDot(u);

            for (var i = 0; i < Dimensions; i++)
                vector[i] += s * u[i];
        }

        return vector;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple MapVectorBasis(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0 && basisIndex < Dimensions
        );

        if (_mapList.Count == 0)
            return Float64Tuple.CreateBasis(Dimensions, basisIndex);

        var x = new double[Dimensions];
        x[basisIndex] = 1d;

        return Float64Tuple.Create(
            MapVectorInPlace(x)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple MapVector(Float64Tuple vector)
    {
        Debug.Assert(
            vector.Dimensions == Dimensions
        );

        if (_mapList.Count == 0)
            return vector;

        var x = vector.GetScalarArrayCopy();

        return Float64Tuple.Create(
            MapVectorInPlace(x)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix<double> GetMatrix()
    {
        var columnList =
            Dimensions
                .GetRange()
                .Select(i => MapVectorBasis(i).ScalarArray);

        return Matrix<double>
            .Build
            .DenseOfColumnArrays(columnList);
    }

    public double[,] GetArray()
    {
        var array = new double[Dimensions, Dimensions];

        for (var j = 0; j < Dimensions; j++)
        {
            var columnVector = MapVectorBasis(j).ScalarArray;

            for (var i = 0; i < Dimensions; i++) 
                array[i, j] = columnVector[i];
        }

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorDirectionalScalingSequence GetDirectionalScalingSequenceInverse()
    {
        if (_mapList.Count == 0)
            return this;

        var scalingFactorVectorList =
            ((IEnumerable<VectorDirectionalScaling>) _mapList)
                .Reverse()
                .Select(t => t.GetVectorDirectionalScalingInverse())
                .ToList();

        return new VectorDirectionalScalingSequence(Dimensions, scalingFactorVectorList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinearMap GetLinearMapInverse()
    {
        return GetDirectionalScalingSequenceInverse();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<VectorDirectionalScaling> GetEnumerator()
    {
        return _mapList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}