using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D;

/// <summary>
/// A basic linear map is either a simple vector-to-vector rotation or a
/// directional vector scaling
/// </summary>
public sealed class LinFloat64OrthogonalLinearMapSequence4D :
    ILinFloat64UnilinearMap4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64OrthogonalLinearMapSequence4D Create()
    {
        return new LinFloat64OrthogonalLinearMapSequence4D(
            LinFloat64VectorToVectorRotationSequence4D.Create(),
            LinFloat64HyperPlaneNormalReflectionSequence4D.Create()
        );
    }

    public static LinFloat64OrthogonalLinearMapSequence4D CreateFromMatrix(SquareMatrix4 matrix)
    {
        Debug.Assert(
            matrix.Determinant.Abs().IsNearOne()
        );

        var rotationSequence = LinFloat64VectorToVectorRotationSequence4D.Create();
        var reflectionSequence = LinFloat64HyperPlaneNormalReflectionSequence4D.Create();

        var subspaceList =
            matrix.GetSimpleEigenSubspaces();

        foreach (var subspace in subspaceList)
        {
            if (subspace.SubspaceDimensions == 2)
            {
                var angle = subspace.EigenValue.GetPhaseAsPolarAngle();

                if (angle.IsNearZero())
                    continue;

                var planeSubspace =
                    (LinFloat64PlaneSubspace4D)subspace.Subspace;

                var sourceVector = planeSubspace.BasisVector1;
                var targetVector = planeSubspace.BasisVector2;

                rotationSequence.AppendMap(sourceVector, targetVector, angle);
            }
            else if (subspace.SubspaceDimensions == 1)
            {
                var scalingFactor =
                    subspace.EigenValue.Real;

                var scalingVector =
                    ((LinFloat64LineSubspace4D)subspace.Subspace).BasisVector;

                if (scalingFactor.IsNearMinusOne())
                    reflectionSequence.AppendMap(scalingVector);
                else
                    throw new InvalidOperationException();
            }
            else
                throw new InvalidOperationException();
        }

        return new LinFloat64OrthogonalLinearMapSequence4D(
            rotationSequence,
            reflectionSequence
        );
    }

    public static LinFloat64OrthogonalLinearMapSequence4D CreateRandom(Random random, int count)
    {
        var matrix =
            LinFloat64HyperPlaneNormalReflectionSequence4D
                .CreateRandom(random, count)
                .ToSquareMatrix3();

        return CreateFromMatrix(matrix);
    }

    public static LinFloat64OrthogonalLinearMapSequence4D CreateRandomOrthogonal(Random random, int count)
    {
        var mapSequence = Create();

        var vectorList =
            random.GetMathNetOrthonormalVectors(3, count);

        for (var i = 0; i < count / 2; i++)
        {
            var u = vectorList[2 * i].ToLinVector4D();
            var v = vectorList[2 * i + 1].ToLinVector4D();
            var angle = random.GetPolarAngle();

            mapSequence.RotationSequence.AppendMap(u, v, angle);
        }

        if (count.IsOdd())
        {
            var u = vectorList[^1].ToLinVector4D();

            mapSequence.ReflectionSequence.AppendMap(u);
        }

        return mapSequence;
    }


    public int VSpaceDimensions
        => RotationSequence.VSpaceDimensions;

    public bool SwapsHandedness
        => ReflectionSequence.Count.IsOdd();

    public bool IsRotation
        => ReflectionSequence.Count == 0;

    public bool IsReflection
        => RotationSequence.Count == 0;

    public bool IsScaling
        => RotationSequence.Count == 0 &&
           ReflectionSequence.Count == 0;

    public bool HasRotation
        => RotationSequence.Count > 0;

    public bool HasReflection
        => ReflectionSequence.Count > 0;

    public LinFloat64VectorToVectorRotationSequence4D RotationSequence { get; }

    public LinFloat64HyperPlaneNormalReflectionSequence4D ReflectionSequence { get; }

    public int MapCount
        => RotationSequence.Count +
           ReflectionSequence.Count;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64OrthogonalLinearMapSequence4D(LinFloat64VectorToVectorRotationSequence4D rotationSequence, LinFloat64HyperPlaneNormalReflectionSequence4D reflectionSequence)
    {
        RotationSequence = rotationSequence;
        ReflectionSequence = reflectionSequence;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return RotationSequence.IsValid() &&
               ReflectionSequence.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdentity()
    {
        if (RotationSequence.Count == 0 && ReflectionSequence.Count == 0)
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

    /// <summary>
    /// Test if all rotation planes and reflection normals in this sequence
    /// are nearly pair-wise orthogonal
    /// </summary>
    /// <returns></returns>
    public bool IsNearOrthogonalMapsSequence(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (ReflectionSequence.Count + 2 * RotationSequence.Count > VSpaceDimensions)
            return false;

        for (var i = 0; i < RotationSequence.Count; i++)
        {
            var u1 = RotationSequence[i].SourceVector;
            var v1 = RotationSequence[i].TargetVector;

            foreach (var reflection in ReflectionSequence)
            {
                var u2 = reflection.ReflectionNormal;

                if (!u1.IsNearOrthogonalTo(u2, zeroEpsilon))
                    return false;
                if (!v1.IsNearOrthogonalTo(u2, zeroEpsilon))
                    return false;
            }

            for (var j = i + 1; j < RotationSequence.Count; j++)
            {
                var u2 = RotationSequence[j].SourceVector;
                var v2 = RotationSequence[j].TargetVector;

                if (!u1.IsNearOrthogonalTo(u2, zeroEpsilon))
                    return false;
                if (!u1.IsNearOrthogonalTo(v2, zeroEpsilon))
                    return false;
                if (!v1.IsNearOrthogonalTo(u2, zeroEpsilon))
                    return false;
                if (!v1.IsNearOrthogonalTo(v2, zeroEpsilon))
                    return false;
            }
        }

        for (var i = 0; i < ReflectionSequence.Count; i++)
        {
            var u1 = ReflectionSequence[i].ReflectionNormal;

            for (var j = i + 1; j < ReflectionSequence.Count; j++)
            {
                var u2 = ReflectionSequence[j].ReflectionNormal;

                if (!u1.IsNearOrthogonalTo(u2, zeroEpsilon))
                    return false;
            }
        }

        return true;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] MapVectorInPlace(double[] vector)
    {
        if (RotationSequence.Count > 0)
            vector = RotationSequence.MapVectorInPlace(vector);

        if (ReflectionSequence.Count > 0)
            vector = ReflectionSequence.MapVectorInPlace(vector);

        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        var vector =
            LinFloat64Vector4D.BasisVectors[basisIndex];

        if (RotationSequence.Count > 0)
            vector = RotationSequence.MapVector(vector);

        if (ReflectionSequence.Count > 0)
            vector = ReflectionSequence.MapVector(vector);

        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        var vector1 =
            LinFloat64Vector4DUtils.ToLinVector4D(vector);

        if (RotationSequence.Count > 0)
            vector1 = RotationSequence.MapVector(vector1);

        if (ReflectionSequence.Count > 0)
            vector1 = ReflectionSequence.MapVector(vector1);

        return vector1;
    }

    /// <summary>
    /// Create a new sequence containing the minimum number of pair-wise
    /// orthogonal rotations and reflections equivalent to this one
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64OrthogonalLinearMapSequence4D ReduceSequence()
    {
        return CreateFromMatrix(this.ToSquareMatrix3());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64OrthogonalLinearMapSequence4D GetBasicLinearMapSequenceInverse()
    {
        var rotationSequence =
            RotationSequence.GetVectorToVectorRotationSequenceInverse();

        var reflectionSequence =
            ReflectionSequence.GetHyperPlaneReflectionSequenceInverse();

        return new LinFloat64OrthogonalLinearMapSequence4D(
            rotationSequence,
            reflectionSequence
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64UnilinearMap4D GetInverseMap()
    {
        return GetBasicLinearMapSequenceInverse();
    }
}