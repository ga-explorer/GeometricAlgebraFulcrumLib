using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;

public sealed class LinFloat64VectorDirectionalScalingSequence4D :
    ILinFloat64UnilinearMap4D,
    IReadOnlyList<LinFloat64VectorDirectionalScaling4D>
{
    //
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

    
    public static LinFloat64VectorDirectionalScalingSequence4D CreateFromMatrix(SquareMatrix4 matrix)
    {
        return matrix.GetVectorDirectionalScalingSequence4D();
    }

    
    public static LinFloat64VectorDirectionalScalingSequence4D Create()
    {
        return new LinFloat64VectorDirectionalScalingSequence4D();
    }

    
    public static LinFloat64VectorDirectionalScalingSequence4D Create(LinFloat64VectorDirectionalScaling4D scaling)
    {
        var reflectionSequence =
            new LinFloat64VectorDirectionalScalingSequence4D();

        reflectionSequence.AppendMap(scaling);

        return reflectionSequence;
    }


    private readonly List<LinFloat64VectorDirectionalScaling4D> _mapList
        = new List<LinFloat64VectorDirectionalScaling4D>();


    public int Count
        => _mapList.Count;

    public LinFloat64VectorDirectionalScaling4D this[int index]
        => _mapList[index];

    public int VSpaceDimensions { get; }

    public bool SwapsHandedness
        => _mapList
            .Count(m => m.SwapsHandedness)
            .IsOdd();


    
    private LinFloat64VectorDirectionalScalingSequence4D()
    {
    }

    
    private LinFloat64VectorDirectionalScalingSequence4D(int dimensions, List<LinFloat64VectorDirectionalScaling4D> scalingFactorVectorList)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        VSpaceDimensions = dimensions;
        _mapList = scalingFactorVectorList;

        Debug.Assert(IsValid());
    }


    
    public LinFloat64VectorDirectionalScalingSequence4D AppendMap(double scalingFactor, LinFloat64Vector4D scalingVector)
    {
        _mapList.Add(
            LinFloat64VectorDirectionalScaling4D.Create(scalingFactor, scalingVector)
        );

        return this;
    }

    
    public LinFloat64VectorDirectionalScalingSequence4D AppendMap(LinFloat64VectorDirectionalScaling4D scaling)
    {
        _mapList.Add(scaling);

        return this;
    }

    
    public LinFloat64VectorDirectionalScalingSequence4D AppendMaps(IEnumerable<LinFloat64VectorDirectionalScaling4D> scalingList)
    {
        foreach (var scaling in scalingList)
            AppendMap(scaling);

        return this;
    }

    
    public LinFloat64VectorDirectionalScalingSequence4D PrependMap(LinFloat64VectorDirectionalScaling4D scaling)
    {
        _mapList.Insert(0, scaling);

        return this;
    }

    
    public LinFloat64VectorDirectionalScalingSequence4D InsertMap(int index, LinFloat64VectorDirectionalScaling4D scaling)
    {
        _mapList.Insert(index, scaling);

        return this;
    }


    
    public bool IsValid()
    {
        return _mapList.All(a => a.IsValid());
    }

    public bool IsIdentity()
    {
        if (_mapList.Count == 0)
            return true;

        for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
        {
            var isSameVectorBasis =
                MapBasisVector(basisIndex).IsVectorBasis(basisIndex);

            if (!isSameVectorBasis) return false;
        }

        return true;
    }

    public bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
        {
            var isSameVectorBasis =
                MapBasisVector(basisIndex).IsNearVectorBasis(basisIndex, zeroEpsilon);

            if (!isSameVectorBasis) return false;
        }

        return true;
    }

    public double[] MapVectorInPlace(double[] vector)
    {
        foreach (var scaling in _mapList)
        {
            var u = scaling.ScalingVector;
            var s = (scaling.ScalingFactor - 1d) * vector.VectorDot(u);

            if (s.IsZero()) continue;

            vector[0] += s * u.X;
            vector[1] += s * u.Y;
            vector[2] += s * u.Z;
        }

        return vector;
    }

    
    public LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        if (_mapList.Count == 0)
            return LinFloat64Vector4D.BasisVectors[basisIndex];

        var composer = LinFloat64Vector4DComposer.Create();

        composer.SetTerm(basisIndex, 1d);

        foreach (var scaling in _mapList)
        {
            var u = scaling.ScalingVector;
            var s = (scaling.ScalingFactor - 1d) * composer.VectorESp(u);

            if (s.IsZero()) continue;

            composer.AddVector(u, s);
        }

        return composer.GetVector();
    }

    
    public LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        if (_mapList.Count == 0)
            return LinFloat64Vector4DUtils.ToLinVector4D(vector);

        var composer = LinFloat64Vector4DComposer.Create();

        composer.SetVector(vector);

        foreach (var scaling in _mapList)
        {
            var u = scaling.ScalingVector;
            var s = (scaling.ScalingFactor - 1d) * composer.VectorESp(u);

            if (s.IsZero()) continue;

            composer.AddVector(u, s);
        }

        return composer.GetVector();
    }

    
    public LinFloat64VectorDirectionalScalingSequence4D GetDirectionalScalingSequenceInverse()
    {
        if (_mapList.Count == 0)
            return this;

        var scalingFactorVectorList =
            ((IEnumerable<LinFloat64VectorDirectionalScaling4D>)_mapList)
            .Reverse()
            .Select(t => t.GetVectorDirectionalScalingInverse())
            .ToList();

        return new LinFloat64VectorDirectionalScalingSequence4D(VSpaceDimensions, scalingFactorVectorList);
    }

    
    public ILinFloat64UnilinearMap4D GetInverseMap()
    {
        return GetDirectionalScalingSequenceInverse();
    }


    
    public IEnumerator<LinFloat64VectorDirectionalScaling4D> GetEnumerator()
    {
        return _mapList.GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}