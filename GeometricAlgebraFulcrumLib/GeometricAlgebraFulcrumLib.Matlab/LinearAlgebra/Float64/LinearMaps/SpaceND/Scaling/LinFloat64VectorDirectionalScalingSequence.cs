using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;

public sealed class LinFloat64VectorDirectionalScalingSequence :
    ILinFloat64UnilinearMap,
    IReadOnlyList<LinFloat64VectorDirectionalScaling>
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

    
    public static LinFloat64VectorDirectionalScalingSequence CreateFromMatrix(Matrix<double> matrix)
    {
        return matrix.GetVectorDirectionalScalingSequence();
    }

    
    public static LinFloat64VectorDirectionalScalingSequence Create(int dimensions)
    {
        return new LinFloat64VectorDirectionalScalingSequence(dimensions);
    }

    
    public static LinFloat64VectorDirectionalScalingSequence Create(LinFloat64VectorDirectionalScaling scaling)
    {
        var reflectionSequence =
            new LinFloat64VectorDirectionalScalingSequence(scaling.VSpaceDimensions);

        reflectionSequence.AppendMap(scaling);

        return reflectionSequence;
    }


    private readonly List<LinFloat64VectorDirectionalScaling> _mapList
        = new List<LinFloat64VectorDirectionalScaling>();


    public int Count
        => _mapList.Count;

    public LinFloat64VectorDirectionalScaling this[int index]
        => _mapList[index];

    public int VSpaceDimensions { get; }

    public bool SwapsHandedness
        => _mapList
            .Count(m => m.SwapsHandedness)
            .IsOdd();


    
    private LinFloat64VectorDirectionalScalingSequence(int dimensions)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        VSpaceDimensions = dimensions;
    }

    
    private LinFloat64VectorDirectionalScalingSequence(int dimensions, List<LinFloat64VectorDirectionalScaling> scalingFactorVectorList)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        VSpaceDimensions = dimensions;
        _mapList = scalingFactorVectorList;

        Debug.Assert(IsValid());
    }


    
    public LinFloat64VectorDirectionalScalingSequence AppendMap(double scalingFactor, LinFloat64Vector scalingVector)
    {
        _mapList.Add(
            LinFloat64VectorDirectionalScaling.Create(scalingFactor, scalingVector)
        );

        return this;
    }

    
    public LinFloat64VectorDirectionalScalingSequence AppendMap(LinFloat64VectorDirectionalScaling scaling)
    {
        _mapList.Add(scaling);

        return this;
    }

    
    public LinFloat64VectorDirectionalScalingSequence AppendMaps(IEnumerable<LinFloat64VectorDirectionalScaling> scalingList)
    {
        foreach (var scaling in scalingList)
            AppendMap(scaling);

        return this;
    }

    
    public LinFloat64VectorDirectionalScalingSequence PrependMap(LinFloat64VectorDirectionalScaling scaling)
    {
        _mapList.Insert(0, scaling);

        return this;
    }

    
    public LinFloat64VectorDirectionalScalingSequence InsertMap(int index, LinFloat64VectorDirectionalScaling scaling)
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

    public bool IsReflection()
    {
        throw new NotImplementedException();
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

    public bool IsNearReflection(double zeroEpsilon = 1E-12)
    {
        throw new NotImplementedException();
    }

    public double[] MapVectorInPlace(double[] vector)
    {
        foreach (var scaling in _mapList)
        {
            var u = scaling.ScalingVector;
            var s = (scaling.ScalingFactor - 1d) * vector.VectorDot(u);

            if (s.IsZero()) continue;

            foreach (var (i, uScalar) in u.ToTuples())
                vector[i] += s * uScalar;
        }

        return vector;
    }

    
    public LinFloat64Vector MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        if (_mapList.Count == 0)
            return basisIndex.CreateLinVector();

        var composer = LinFloat64VectorComposer.Create();

        composer.SetTerm(basisIndex, 1d);

        foreach (var scaling in _mapList)
        {
            var u = scaling.ScalingVector;
            var s = (scaling.ScalingFactor - 1d) * composer.VectorDot(u);

            if (s.IsZero()) continue;

            composer.AddVector(u, s);
        }

        return composer.GetVector();
    }

    
    public LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        if (_mapList.Count == 0)
            return vector;

        var composer = LinFloat64VectorComposer.Create();

        composer.SetVector(vector);

        foreach (var scaling in _mapList)
        {
            var u = scaling.ScalingVector;
            var s = (scaling.ScalingFactor - 1d) * composer.VectorDot(u);

            if (s.IsZero()) continue;

            composer.AddVector(u, s);
        }

        return composer.GetVector();
    }

    public IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetMappedBasisVectors(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }

    
    public Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        var columnList =
            colCount
                .GetRange()
                .Select(i => MapBasisVector(i).ToArray(rowCount));

        return Matrix<double>
            .Build
            .DenseOfColumnArrays(columnList);
    }

    public LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }

    public double[,] ToArray(int rowCount, int colCount)
    {
        var array = new double[rowCount, colCount];

        for (var j = 0; j < colCount; j++)
        {
            var columnVector = MapBasisVector(j);

            foreach (var (i, s) in columnVector.ToTuples())
                array[i, j] = s;
        }

        return array;
    }

    
    public LinFloat64VectorDirectionalScalingSequence GetDirectionalScalingSequenceInverse()
    {
        if (_mapList.Count == 0)
            return this;

        var scalingFactorVectorList =
            ((IEnumerable<LinFloat64VectorDirectionalScaling>)_mapList)
            .Reverse()
            .Select(t => t.GetVectorDirectionalScalingInverse())
            .ToList();

        return new LinFloat64VectorDirectionalScalingSequence(VSpaceDimensions, scalingFactorVectorList);
    }

    
    public ILinFloat64UnilinearMap GetInverseMap()
    {
        return GetDirectionalScalingSequenceInverse();
    }


    
    public IEnumerator<LinFloat64VectorDirectionalScaling> GetEnumerator()
    {
        return _mapList.GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}