using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;

public sealed class LinFloat64VectorDirectionalScalingSequence3D :
    ILinFloat64UnilinearMap3D,
    IReadOnlyList<LinFloat64VectorDirectionalScaling3D>
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
    public static LinFloat64VectorDirectionalScalingSequence3D CreateFromMatrix(SquareMatrix3 matrix)
    {
        return matrix.GetVectorDirectionalScalingSequence3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorDirectionalScalingSequence3D Create()
    {
        return new LinFloat64VectorDirectionalScalingSequence3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorDirectionalScalingSequence3D Create(LinFloat64VectorDirectionalScaling3D scaling)
    {
        var reflectionSequence =
            new LinFloat64VectorDirectionalScalingSequence3D();

        reflectionSequence.AppendMap(scaling);

        return reflectionSequence;
    }


    private readonly List<LinFloat64VectorDirectionalScaling3D> _mapList
        = new List<LinFloat64VectorDirectionalScaling3D>();


    public int Count
        => _mapList.Count;

    public LinFloat64VectorDirectionalScaling3D this[int index]
        => _mapList[index];

    public int VSpaceDimensions { get; }

    public bool SwapsHandedness
        => _mapList
            .Count(m => m.SwapsHandedness)
            .IsOdd();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64VectorDirectionalScalingSequence3D()
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64VectorDirectionalScalingSequence3D(int dimensions, List<LinFloat64VectorDirectionalScaling3D> scalingFactorVectorList)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        VSpaceDimensions = dimensions;
        _mapList = scalingFactorVectorList;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorDirectionalScalingSequence3D AppendMap(double scalingFactor, LinFloat64Vector3D scalingVector)
    {
        _mapList.Add(
            LinFloat64VectorDirectionalScaling3D.Create(scalingFactor, scalingVector)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorDirectionalScalingSequence3D AppendMap(LinFloat64VectorDirectionalScaling3D scaling)
    {
        _mapList.Add(scaling);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorDirectionalScalingSequence3D AppendMaps(IEnumerable<LinFloat64VectorDirectionalScaling3D> scalingList)
    {
        foreach (var scaling in scalingList)
            AppendMap(scaling);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorDirectionalScalingSequence3D PrependMap(LinFloat64VectorDirectionalScaling3D scaling)
    {
        _mapList.Insert(0, scaling);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorDirectionalScalingSequence3D InsertMap(int index, LinFloat64VectorDirectionalScaling3D scaling)
    {
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        if (_mapList.Count == 0)
            return LinFloat64Vector3D.BasisVectors[basisIndex];

        var composer = LinFloat64Vector3DComposer.Create();

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        if (_mapList.Count == 0)
            return vector.ToLinVector3D();

        var composer = LinFloat64Vector3DComposer.Create();

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorDirectionalScalingSequence3D GetDirectionalScalingSequenceInverse()
    {
        if (_mapList.Count == 0)
            return this;

        var scalingFactorVectorList =
            ((IEnumerable<LinFloat64VectorDirectionalScaling3D>)_mapList)
            .Reverse()
            .Select(t => t.GetVectorDirectionalScalingInverse())
            .ToList();

        return new LinFloat64VectorDirectionalScalingSequence3D(VSpaceDimensions, scalingFactorVectorList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64UnilinearMap3D GetInverseMap()
    {
        return GetDirectionalScalingSequenceInverse();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<LinFloat64VectorDirectionalScaling3D> GetEnumerator()
    {
        return _mapList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}