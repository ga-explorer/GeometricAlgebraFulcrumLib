using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;

public sealed class LinFloat64VectorToVectorRotationSequence4D :
    LinFloat64RotationBase4D,
    IReadOnlyList<LinFloat64VectorToVectorRotation4D>
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
    public static LinFloat64VectorToVectorRotationSequence4D CreateFromRotationMatrix(SquareMatrix4 matrix)
    {
        return matrix.GetVectorToVectorRotationSequence4D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotationSequence4D Create()
    {
        return new LinFloat64VectorToVectorRotationSequence4D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotationSequence4D Create(LinFloat64VectorToVectorRotationBase4D rotation)
    {
        var rotationSequence =
            new LinFloat64VectorToVectorRotationSequence4D();

        rotationSequence.AppendMap(rotation);

        return rotationSequence;
    }

    public static LinFloat64VectorToVectorRotationSequence4D CreateRandom(Random random, int dimensions, int count)
    {
        var rotationSequence = new LinFloat64VectorToVectorRotationSequence4D();

        for (var i = 0; i < count; i++)
        {
            var u = random.GetLinVector4D();
            var v = random.GetLinVector4D();
            var angle = random.GetPolarAngle();

            rotationSequence.AppendMap(
                LinFloat64VectorToVectorRotation4D.Create(u, v, angle)
            );
        }

        return rotationSequence;
    }

    public static LinFloat64VectorToVectorRotationSequence4D CreateRandomOrthogonal(Random random, int dimensions, int count)
    {
        if (count > dimensions / 2)
            throw new ArgumentOutOfRangeException(nameof(count));

        var rotationSequence = new LinFloat64VectorToVectorRotationSequence4D();

        var vectorList =
            random.GetMathNetOrthonormalVectors(dimensions, 2 * count);

        for (var i = 0; i < count; i++)
        {
            var u = vectorList[2 * i].ToLinVector4D();
            var v = vectorList[2 * i + 1].ToLinVector4D();
            var angle = random.GetPolarAngle();

            rotationSequence.AppendMap(
                LinFloat64VectorToVectorRotation4D.Create(u, v, angle)
            );
        }

        return rotationSequence;
    }


    private readonly List<LinFloat64VectorToVectorRotation4D> _mapList
        = new List<LinFloat64VectorToVectorRotation4D>();


    public int Count
        => _mapList.Count;

    public LinFloat64VectorToVectorRotation4D this[int index]
        => _mapList[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64VectorToVectorRotationSequence4D()
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64VectorToVectorRotationSequence4D(List<LinFloat64VectorToVectorRotation4D> rotationList)
    {
        _mapList = rotationList;
    }


    //private static Triplet<double[]> GetRotationVectorsTriplet(double[] sourceVector, double[] targetVector)
    //{
    //    Debug.Assert(
    //        sourceVector.Length == targetVector.Length &&
    //        sourceVector.GetVectorNormSquared().IsNearOne() &&
    //        targetVector.GetVectorNormSquared().IsNearOne()
    //    );

    //    var angleCos = targetVector.ESp(sourceVector).Clamp(-1d, 1d);

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
    public LinFloat64VectorToVectorRotationSequence4D AppendMap(LinFloat64Vector4D sourceVector, LinFloat64Vector4D targetVector)
    {
        _mapList.Add(
            LinFloat64VectorToVectorRotation4D.Create(sourceVector, targetVector)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorToVectorRotationSequence4D AppendMap(LinFloat64Vector4D sourceVector, LinFloat64Vector4D targetVector, LinFloat64Angle angle)
    {
        _mapList.Add(
            LinFloat64VectorToVectorRotation4D.Create(sourceVector, targetVector, angle)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorToVectorRotationSequence4D AppendMap(LinFloat64VectorToVectorRotationBase4D rotation)
    {
        if (rotation.VSpaceDimensions != VSpaceDimensions)
            throw new ArgumentException();

        var r2 =
            rotation as LinFloat64VectorToVectorRotation4D
            ?? LinFloat64VectorToVectorRotation4D.Create(rotation.SourceVector, rotation.TargetVector);

        _mapList.Add(r2);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorToVectorRotationSequence4D PrependMap(LinFloat64Vector4D sourceVector, LinFloat64Vector4D targetVector)
    {
        _mapList.Insert(
            0,
            LinFloat64VectorToVectorRotation4D.Create(sourceVector, targetVector)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorToVectorRotationSequence4D PrependMap(LinFloat64VectorToVectorRotationBase4D rotation)
    {
        if (rotation.VSpaceDimensions != VSpaceDimensions)
            throw new ArgumentException();

        var r2 =
            rotation as LinFloat64VectorToVectorRotation4D
            ?? LinFloat64VectorToVectorRotation4D.Create(rotation.SourceVector, rotation.TargetVector);

        _mapList.Insert(0, r2);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorToVectorRotationSequence4D InsertMap(int index, LinFloat64Vector4D sourceVector, LinFloat64Vector4D targetVector)
    {
        _mapList.Insert(
            index,
            LinFloat64VectorToVectorRotation4D.Create(sourceVector, targetVector)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorToVectorRotationSequence4D InsertMap(int index, LinFloat64VectorToVectorRotationBase4D rotation)
    {
        if (rotation.VSpaceDimensions != VSpaceDimensions)
            throw new ArgumentException();

        var r2 =
            rotation as LinFloat64VectorToVectorRotation4D
            ?? LinFloat64VectorToVectorRotation4D.Create(rotation.SourceVector, rotation.TargetVector);

        _mapList.Insert(index, r2);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorToVectorRotationSequence4D AppendMaps(IEnumerable<LinFloat64VectorToVectorRotation4D> rotationList)
    {
        _mapList.AddRange(rotationList);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorToVectorRotationSequence4D PrependMaps(IEnumerable<LinFloat64VectorToVectorRotation4D> rotationList)
    {
        _mapList.InsertRange(0, rotationList);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorToVectorRotationSequence4D InsertMaps(int index, IEnumerable<LinFloat64VectorToVectorRotation4D> rotationList)
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
            var u1 = _mapList[i].SourceVector;
            var v1 = _mapList[i].TargetVector;

            for (var j = i + 1; j < _mapList.Count; j++)
            {
                var u2 = _mapList[j].SourceVector;
                var v2 = _mapList[j].TargetVector;

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
            var u = rotation.SourceVector;
            var t = rotation.TargetOrthogonalVector;
            var v = rotation.TargetVector;

            //var r = vector.ESp(TargetOrthogonalVector);
            //var s = vector.ESp(SourceVector);

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
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        if (_mapList.Count == 0)
            return LinFloat64Vector4D.BasisVectors[basisIndex];

        var composer = LinFloat64Vector4DComposer
            .Create()
            .SetTerm(basisIndex, 1d);

        foreach (var rotation in _mapList)
        {
            var u = rotation.SourceVector;
            var t = rotation.TargetOrthogonalVector;
            var v = rotation.TargetVector;

            //var r = vector.ESp(TargetOrthogonalVector);
            //var s = vector.ESp(SourceVector);

            //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

            var r = composer.VectorESp(t);
            var s = composer.VectorESp(u);
            var rsPlus = r + s;
            var rsMinus = r - s;

            composer
                .AddVector(u, -rsPlus)
                .AddVector(v, -rsMinus);
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        if (_mapList.Count == 0)
            return LinFloat64Vector4DUtils.ToLinVector4D(vector);

        var composer = LinFloat64Vector4DComposer
            .Create()
            .SetVector(vector);

        foreach (var rotation in _mapList)
        {
            var u = rotation.SourceVector;
            var t = rotation.TargetOrthogonalVector;
            var v = rotation.TargetVector;

            //var r = vector.ESp(TargetOrthogonalVector);
            //var s = vector.ESp(SourceVector);

            //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

            var r = composer.VectorESp(t);
            var s = composer.VectorESp(u);
            var rsPlus = r + s;
            var rsMinus = r - s;

            composer
                .AddVector(u, -rsPlus)
                .AddVector(v, -rsMinus);
        }

        return composer.GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorToVectorRotationSequence4D GetVectorToVectorRotationSequenceInverse()
    {
        if (_mapList.Count == 0)
            return this;

        var rotationList =
            ((IEnumerable<LinFloat64VectorToVectorRotation4D>)_mapList)
            .Reverse()
            .Select(r => r.GetVectorToVectorRotationInverse())
            .ToList();

        return new LinFloat64VectorToVectorRotationSequence4D(rotationList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64RotationBase4D GetVectorRotationInverse()
    {
        return GetVectorToVectorRotationSequenceInverse();
    }

    /// <summary>
    /// Create a new sequence containing the minimum number of pair-wise
    /// orthogonal rotations equivalent to this one
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorToVectorRotationSequence4D ReduceSequence()
    {
        return this.ToSquareMatrix3().GetVectorToVectorRotationSequence4D();
    }

    public override LinFloat64HyperPlaneNormalReflectionSequence4D ToHyperPlaneReflectionSequence()
    {
        var reflection =
            LinFloat64HyperPlaneNormalReflectionSequence4D.Create();

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
    public override LinFloat64VectorToVectorRotationSequence4D ToVectorToVectorRotationSequence()
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
    public IEnumerator<LinFloat64VectorToVectorRotation4D> GetEnumerator()
    {
        return _mapList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}