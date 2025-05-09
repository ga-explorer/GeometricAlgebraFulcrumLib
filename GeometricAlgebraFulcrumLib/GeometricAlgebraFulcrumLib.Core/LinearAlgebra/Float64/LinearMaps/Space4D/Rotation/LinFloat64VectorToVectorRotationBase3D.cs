using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;

public abstract class LinFloat64VectorToVectorRotationBase4D :
    LinFloat64SimpleRotationBase4D,
    ILinFloat64SimpleVectorToVectorRotation4D
{
    /// <summary>
    /// The unit vector where the rotation starts
    /// </summary>
    public abstract LinFloat64Vector4D SourceVector { get; }

    /// <summary>
    /// A scaled version of the orthogonal component (rejection) of
    /// TargetVector relative to SourceVector
    /// </summary>
    public abstract LinFloat64Vector4D TargetOrthogonalVector { get; }

    /// <summary>
    /// The unit vector where the rotation ends
    /// </summary>
    public abstract LinFloat64Vector4D TargetVector { get; }

    /// <summary>
    /// The dot product of TargetVector and SourceVector
    /// </summary>
    public abstract double AngleCos { get; }

    /// <summary>
    /// The smallest angle between TargetVector and SourceVector
    /// </summary>
    public abstract LinFloat64PolarAngle Angle { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsIdentity()
    {
        return (AngleCos - 1d).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsNearIdentity(double zeroEpsilon = 1e-12d)
    {
        return (AngleCos - 1d).IsNearZero(zeroEpsilon);
    }


    public abstract LinFloat64Vector4D ProjectOnRotationPlane(LinFloat64Vector4D vector);

    public abstract LinFloat64Vector4D MapVectorProjection(LinFloat64Vector4D vector);

    //public virtual Float64Tuple4D MapVectorProjection(Float64Tuple4D vector)
    //{
    //    return MapVector(ProjectOnRotationPlane(vector));
    //}

    /// <summary>
    /// Construct middle unit vector between SourceVector and TargetVector
    /// </summary>
    /// <returns></returns>
    public LinFloat64Vector4D GetMiddleUnitVector()
    {
        var wLengthInv = 1d / (2d + 2d * AngleCos).Sqrt();

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(SourceVector)
            .AddVector(TargetVector)
            .Times(wLengthInv)
            .GetVector();
    }

    public Pair<LinFloat64HyperPlaneNormalReflection4D> GetHyperPlaneReflectionPair()
    {
        return new Pair<LinFloat64HyperPlaneNormalReflection4D>(
            LinFloat64HyperPlaneNormalReflection4D.Create(SourceVector),
            LinFloat64HyperPlaneNormalReflection4D.Create(GetMiddleUnitVector())
        );
    }

    /// <summary>
    /// Compute a rotated version of u in the u-v rotational plane by the given angle
    /// </summary>
    /// <param name="angle1"></param>
    /// <returns></returns>
    public LinFloat64Vector4D GetRotatedSourceVector(LinFloat64DirectedAngle angle1)
    {
        var scalar1 = angle1.Cos();
        var scalar2 = angle1.Sin() / TargetOrthogonalVector.VectorENorm();

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(SourceVector, scalar1)
            .AddVector(TargetOrthogonalVector, scalar2)
            .GetVector();
    }

    public Pair<LinFloat64Vector4D> GetRotatedSourceVectorPair(LinFloat64DirectedAngle angle1, LinFloat64DirectedAngle angle2)
    {
        var norm = TargetOrthogonalVector.VectorENorm();

        var scalar1 = angle1.Cos();
        var scalar2 = angle1.Sin() / norm;

        var v1 =
            LinFloat64Vector4DComposer
                .Create()
                .SetVector(SourceVector, scalar1)
                .AddVector(TargetOrthogonalVector, scalar2)
                .GetVector();

        scalar1 = angle2.Cos();
        scalar2 = angle2.Sin() / norm;

        var v2 =
            LinFloat64Vector4DComposer
                .Create()
                .SetVector(SourceVector, scalar1)
                .AddVector(TargetOrthogonalVector, scalar2)
                .GetVector();

        return new Pair<LinFloat64Vector4D>(v1, v2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64HyperPlaneNormalReflectionSequence4D ToHyperPlaneReflectionSequence()
    {
        var reflection =
            LinFloat64HyperPlaneNormalReflectionSequence4D.Create();

        var (r1, r2) =
            GetHyperPlaneReflectionPair();

        reflection
            .AppendMap(r1)
            .AppendMap(r2);

        return reflection;
    }
}