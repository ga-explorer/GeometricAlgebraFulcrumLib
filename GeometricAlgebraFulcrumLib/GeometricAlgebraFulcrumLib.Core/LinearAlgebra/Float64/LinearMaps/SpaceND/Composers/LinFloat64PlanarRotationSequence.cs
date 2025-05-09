using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.SpaceND.Composers;

public sealed class LinFloat64PlanarRotationSequence :
    LinFloat64Rotation,
    IReadOnlyList<LinFloat64PlanarRotation>
{
    //public static VectorToVectorRotationSequence CreateFromRotationMatrix(Matrix<double> matrix)
    //{
    //    // Make sure it's a rotation matrix
    //    Debug.Assert(
    //        matrix.RowCount == matrix.ColumnCount &&
    //        matrix.Determinant().IsNearOne()
    //    );

    //    var rotationSequence = 
    //        new VectorToVectorRotationSequence(matrix.RowCount);

    //    var eigenPairsCount = MatrixEigenDecomposition(
    //        matrix,
    //        out var realPairs,
    //        out var imagPairs
    //    );

    //    var eigenValueList = new List<System.Numerics.Complex>(eigenPairsCount / 2);
    //    for (var i = 0; i < eigenPairsCount; i++)
    //    {
    //        var realValue = realPairs[i].Item1;
    //        var imagValue = imagPairs[i].Item1;

    //        var realVector = realPairs[i].Item2;
    //        var imagVector = imagPairs[i].Item2;

    //        //Console.WriteLine($"Real Eigen Value {i + 1}: {realValue}");
    //        //Console.WriteLine($"Imag Eigen Value {i + 1}: {imagValue}");
    //        //Console.WriteLine();

    //        //Console.WriteLine($"Real Eigen Vector {i + 1}: {realVector.CreateTuple()}");
    //        //Console.WriteLine($"Imag Eigen Vector {i + 1}: {imagVector.CreateTuple()}");
    //        //Console.WriteLine();

    //        // Ignore identity rotations
    //        if ((realValue - 1d).IsNearZero() && imagValue.IsNearZero())
    //            continue;

    //        // Ignore complex conjugate eigen values (only take first one of the pair)
    //        var conjIndex = eigenValueList.FindIndex(
    //            c => c.IsNearConjugateTo(realValue, imagValue)
    //        );

    //        if (conjIndex >= 0)
    //        {
    //            eigenValueList.RemoveAt(conjIndex);

    //            continue;
    //        }

    //        eigenValueList.Add(
    //            new System.Numerics.Complex(realValue, imagValue)
    //        );

    //        var rotation =
    //            ComplexEigenPairToSimpleVectorRotation(
    //                realValue,
    //                imagValue,
    //                realVector,
    //                imagVector
    //            );

    //        rotationSequence.AppendMap(rotation);
    //    }

    //    return rotationSequence;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotationSequence CreateFromRotationMatrix(Matrix<double> matrix)
    {
        return matrix.GetVectorToVectorRotationSequence();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotationSequence Create(int dimensions)
    {
        return new LinFloat64PlanarRotationSequence(dimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotationSequence Create(LinFloat64PlanarRotation rotation)
    {
        var rotationSequence =
            new LinFloat64PlanarRotationSequence(rotation.VSpaceDimensions);

        rotationSequence.AppendMap(rotation);

        return rotationSequence;
    }

    public static LinFloat64PlanarRotationSequence CreateRandom(Random random, int dimensions, int count)
    {
        var rotationSequence = new LinFloat64PlanarRotationSequence(dimensions);

        for (var i = 0; i < count; i++)
        {
            var u = random.GetLinVector(dimensions).CreateUnitLinVector();
            var v = random.GetLinVector(dimensions).CreateUnitLinVector();
            var angle = random.GetPolarAngle();

            rotationSequence.AppendMap(
                LinFloat64VectorToVectorRotation.CreateFromSpanningVectors(u, v, angle)
            );
        }

        return rotationSequence;
    }

    public static LinFloat64PlanarRotationSequence CreateRandomOrthogonal(Random random, int dimensions, int count)
    {
        if (count > dimensions / 2)
            throw new ArgumentOutOfRangeException(nameof(count));

        var rotationSequence = new LinFloat64PlanarRotationSequence(dimensions);

        var vectorList =
            random.GetMathNetOrthonormalVectors(dimensions, 2 * count);

        for (var i = 0; i < count; i++)
        {
            var u = vectorList[2 * i].CreateLinVector();
            var v = vectorList[2 * i + 1].CreateLinVector();
            var angle = random.GetPolarAngle();

            rotationSequence.AppendMap(
                LinFloat64VectorToVectorRotation.CreateFromSpanningVectors(u, v, angle)
            );
        }

        return rotationSequence;
    }


    private readonly List<LinFloat64PlanarRotation> _mapList
        = new List<LinFloat64PlanarRotation>();


    public override int VSpaceDimensions { get; }

    public int Count
        => _mapList.Count;

    public LinFloat64PlanarRotation this[int index]
        => _mapList[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64PlanarRotationSequence(int dimensions)
    {
        VSpaceDimensions = dimensions;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64PlanarRotationSequence(int dimensions, List<LinFloat64PlanarRotation> rotationList)
    {
        VSpaceDimensions = dimensions;
        _mapList = rotationList;
    }


    //private static Triplet<double[]> GetRotationVectorsTriplet(double[] sourceVector, double[] targetVector)
    //{
    //    Debug.Assert(
    //        sourceVector.Length == targetVector.Length &&
    //        sourceVector.GetVectorNormSquared().IsNearOne() &&
    //        targetVector.GetVectorNormSquared().IsNearOne()
    //    );

    //    var angleCos = targetVector.VectorDot(sourceVector).Clamp(-1d, 1d);

    //    Debug.Assert(
    //        !angleCos.IsNearMinusOne()
    //    );

    //    var targetOrthogonalVector = 
    //        targetVector
    //            .VectorSubtract(sourceVector.VectorTimes(angleCos))
    //            .VectorDivideInPlace(1d + angleCos);

    //    return new Triplet<double[]>(
    //        sourceVector,
    //        targetOrthogonalVector,
    //        targetVector
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotationSequence AppendMap(LinFloat64Vector sourceVector, LinFloat64Vector targetVector)
    {
        _mapList.Add(
            LinFloat64VectorToVectorRotation.CreateFromRotatedVector(sourceVector, targetVector)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotationSequence AppendMap(LinFloat64Vector sourceVector, LinFloat64Vector targetVector, LinFloat64PolarAngle angle)
    {
        _mapList.Add(
            LinFloat64VectorToVectorRotation.CreateFromSpanningVectors(sourceVector, targetVector, angle)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotationSequence AppendMap(LinFloat64PlanarRotation rotation)
    {
        if (rotation.VSpaceDimensions != VSpaceDimensions)
            throw new ArgumentException();

        var r2 =
            rotation as LinFloat64VectorToVectorRotation
            ?? LinFloat64VectorToVectorRotation.CreateFromRotatedVector(rotation.BasisVector1, rotation.MapBasisVector1());

        _mapList.Add(r2);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotationSequence PrependMap(LinFloat64Vector sourceVector, LinFloat64Vector targetVector)
    {
        _mapList.Insert(
            0,
            LinFloat64VectorToVectorRotation.CreateFromRotatedVector(sourceVector, targetVector)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotationSequence PrependMap(LinFloat64PlanarRotation rotation)
    {
        if (rotation.VSpaceDimensions != VSpaceDimensions)
            throw new ArgumentException();

        var r2 =
            rotation as LinFloat64VectorToVectorRotation
            ?? LinFloat64VectorToVectorRotation.CreateFromRotatedVector(rotation.BasisVector1, rotation.MapBasisVector1());

        _mapList.Insert(0, r2);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotationSequence InsertMap(int index, LinFloat64Vector sourceVector, LinFloat64Vector targetVector)
    {
        _mapList.Insert(
            index,
            LinFloat64VectorToVectorRotation.CreateFromRotatedVector(sourceVector, targetVector)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotationSequence InsertMap(int index, LinFloat64PlanarRotation rotation)
    {
        if (rotation.VSpaceDimensions != VSpaceDimensions)
            throw new ArgumentException();

        var r2 =
            rotation as LinFloat64VectorToVectorRotation
            ?? LinFloat64VectorToVectorRotation.CreateFromRotatedVector(rotation.BasisVector1, rotation.MapBasisVector1());

        _mapList.Insert(index, r2);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotationSequence AppendMaps(IEnumerable<LinFloat64VectorToVectorRotation> rotationList)
    {
        _mapList.AddRange(rotationList);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotationSequence PrependMaps(IEnumerable<LinFloat64VectorToVectorRotation> rotationList)
    {
        _mapList.InsertRange(0, rotationList);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotationSequence InsertMaps(int index, IEnumerable<LinFloat64VectorToVectorRotation> rotationList)
    {
        _mapList.InsertRange(index, rotationList);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _mapList.All(r => r.IsValid());
    }

    public override bool IsIdentity()
    {
        for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
        {
            var isSameVectorBasis =
                MapBasisVector(basisIndex).IsVectorBasis(basisIndex);

            if (!isSameVectorBasis) return false;
        }

        return true;
    }

    public override bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
        {
            var isSameVectorBasis =
                MapBasisVector(basisIndex).IsNearVectorBasis(basisIndex, zeroEpsilon);

            if (!isSameVectorBasis) return false;
        }

        return true;
    }

    /// <summary>
    /// Test if all rotation planes in this sequence are nearly pair-wise orthogonal
    /// </summary>
    /// <returns></returns>
    public bool IsNearOrthogonalRotationsSequence(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (_mapList.Count > VSpaceDimensions / 2)
            return false;

        for (var i = 0; i < _mapList.Count; i++)
        {
            var u1 = _mapList[i].BasisVector1;
            var v1 = _mapList[i].MapBasisVector1();

            for (var j = i + 1; j < _mapList.Count; j++)
            {
                var u2 = _mapList[j].BasisVector1;
                var v2 = _mapList[j].MapBasisVector1();

                if (!u1.IsNearOrthogonalTo(u2, zeroEpsilon)) return false;
                if (!u1.IsNearOrthogonalTo(v2, zeroEpsilon)) return false;
                if (!v1.IsNearOrthogonalTo(u2, zeroEpsilon)) return false;
                if (!v1.IsNearOrthogonalTo(v2, zeroEpsilon)) return false;
            }
        }

        return true;
    }


    public double[] MapVectorInPlace(double[] vector)
    {
        foreach (var rotation in _mapList)
        {
            var u = rotation.BasisVector1;
            var t = rotation.BasisVector2;
            var v = rotation.MapBasisVector1();

            //var r = vector.VectorDot(TargetOrthogonalVector);
            //var s = vector.VectorDot(SourceVector);

            //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

            var r = vector.VectorDot(t);
            var s = vector.VectorDot(u);
            var rsPlus = r + s;
            var rsMinus = r - s;

            for (var i = 0; i < VSpaceDimensions; i++)
                vector[i] -= rsPlus * u[i] + rsMinus * v[i];
        }

        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        if (_mapList.Count == 0)
            return basisIndex.CreateLinVector();

        var composer = LinFloat64VectorComposer
            .Create()
            .SetTerm(basisIndex, 1d);

        foreach (var rotation in _mapList)
        {
            var u = rotation.BasisVector1;
            var t = rotation.BasisVector2;
            var v = rotation.MapBasisVector1();

            //var r = vector.VectorDot(TargetOrthogonalVector);
            //var s = vector.VectorDot(SourceVector);

            //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

            var r = composer.VectorDot(t);
            var s = composer.VectorDot(u);
            var rsPlus = r + s;
            var rsMinus = r - s;

            composer
                .AddVector(u, -rsPlus)
                .AddVector(v, -rsMinus);
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        if (_mapList.Count == 0)
            return vector;

        var composer = LinFloat64VectorComposer
            .Create()
            .SetVector(vector);

        foreach (var rotation in _mapList)
        {
            var u = rotation.BasisVector1;
            var t = rotation.BasisVector2;
            var v = rotation.MapBasisVector1();

            //var r = vector.VectorDot(TargetOrthogonalVector);
            //var s = vector.VectorDot(SourceVector);

            //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

            var r = composer.VectorDot(t);
            var s = composer.VectorDot(u);
            var rsPlus = r + s;
            var rsMinus = r - s;

            composer
                .AddVector(u, -rsPlus)
                .AddVector(v, -rsMinus);
        }

        return composer.GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotationSequence GetVectorToVectorRotationSequenceInverse()
    {
        if (_mapList.Count == 0)
            return this;

        var rotationList =
            ((IEnumerable<LinFloat64PlanarRotation>)_mapList)
            .Reverse()
            .Select(r => r.GetInversePlanarRotation())
            .ToList();

        return new LinFloat64PlanarRotationSequence(VSpaceDimensions, rotationList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Rotation GetInverseRotation()
    {
        return GetVectorToVectorRotationSequenceInverse();
    }

    /// <summary>
    /// Create a new sequence containing the minimum number of pair-wise
    /// orthogonal rotations equivalent to this one
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotationSequence ReduceSequence()
    {
        return ToMatrix(VSpaceDimensions, VSpaceDimensions).GetVectorToVectorRotationSequence();
    }

    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        var reflection =
            LinFloat64HyperPlaneNormalReflectionSequence.Create(VSpaceDimensions);

        foreach (var rotation in _mapList)
        {
            var (r1, r2) =
                rotation.GetHyperPlaneReflectionPair();

            reflection
                .AppendMap(r1)
                .AppendMap(r2);
        }

        return reflection;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64PlanarRotationSequence ToVectorToVectorRotationSequence()
    {
        return this;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override SimpleRotationSequence ToSimpleVectorRotationSequence()
    //{
    //    var rotation = SimpleRotationSequence.Create(Dimensions);

    //    foreach (var rotationVectors in _rotationList)
    //        rotation.AppendRotation(rotationVectors);

    //    return rotation;
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Matrix<double> GetRotationMatrix()
    //{
    //    var columnList =
    //        Dimensions
    //            .GetRange()
    //            .Select(i => MapVectorBasis(i).MathNetVector);

    //    return Matrix<double>
    //        .Build
    //        .SparseOfColumnVectors(columnList);
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<LinFloat64PlanarRotation> GetEnumerator()
    {
        return _mapList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}