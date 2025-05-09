using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;

public sealed class LinFloat64VectorToAxisRotation :
    LinFloat64PlanarRotation
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToAxisRotation CreateFromOrthogonalVector(LinFloat64Vector spanningVector1, ILinSignedBasisVector basisAxis2, LinFloat64PolarAngle rotationAngle)
    {
        var basisVector1 = spanningVector1.DivideByENorm();

        return new LinFloat64VectorToAxisRotation(
            basisVector1,
            basisAxis2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToAxisRotation CreateFromOrthonormalVector(LinFloat64Vector basisVector1, ILinSignedBasisVector basisAxis2, LinFloat64PolarAngle rotationAngle)
    {
        return new LinFloat64VectorToAxisRotation(
            basisVector1,
            basisAxis2,
            rotationAngle
        );
    }


    public ILinSignedBasisVector BasisAxis2 { get; }

    public override LinFloat64Vector BasisVector1 { get; }

    public override LinFloat64Vector BasisVector2 { get; }


    private LinFloat64VectorToAxisRotation(LinFloat64Vector basisVector1, ILinSignedBasisVector basisAxis2, LinFloat64PolarAngle rotationAngle)
        : base(rotationAngle)
    {
        BasisVector1 = basisVector1;
        BasisAxis2 = basisAxis2;
        BasisVector2 = basisAxis2.ToLinVector();

        Debug.Assert(IsValid());
    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Pair<double> BasisESp(int axisIndex)
    {
        return new Pair<double>(
            BasisVector1.GetComponent(axisIndex),
            axisIndex == BasisAxis2.Index ? BasisAxis2.Sign.ToFloat64() : 0d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Pair<double> BasisESp(ILinSignedBasisVector axis)
    {
        return new Pair<double>(
            BasisVector1.ESp(axis),
            axis.Index == BasisAxis2.Index ? (axis.Sign * BasisAxis2.Sign).ToFloat64() : 0d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Pair<double> BasisESp(LinFloat64Vector vector)
    {
        return new Pair<double>(
            vector.ESp(BasisVector1),
            vector.ESp(BasisAxis2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        // Compute the projection components of the given vector on
        // the orthonormal basis vectors defining the plane of rotation
        var (vpx, vpy) = BasisESp(basisIndex);

        var rotationAngleCosMinusOne = RotationAngleCos - 1d;

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCosMinusOne * vpx - RotationAngleSin * vpy;
        var u2Scalar = rotationAngleCosMinusOne * vpy + RotationAngleSin * vpx;

        // The final rotated vector
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, u1Scalar)
            .AddVector(BasisAxis2, u2Scalar)
            .AddTerm(basisIndex, 1d)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        // Compute the projection components of the given vector on
        // the orthonormal basis vectors defining the plane of rotation
        var (vpx, vpy) = BasisESp(vector);

        var rotationAngleCosMinusOne = RotationAngleCos - 1d;

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCosMinusOne * vpx - RotationAngleSin * vpy;
        var u2Scalar = rotationAngleCosMinusOne * vpy + RotationAngleSin * vpx;

        // The final rotated vector
        return LinFloat64VectorComposer
            .Create()
            .SetVector(vector)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisAxis2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector1()
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, RotationAngleCos)
            .AddVector(BasisAxis2, RotationAngleSin)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector2()
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, -RotationAngleSin)
            .AddVector(BasisAxis2, RotationAngleCos)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector1(LinFloat64Angle rotationAngle)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, rotationAngle.Cos())
            .AddVector(BasisAxis2, rotationAngle.Sin())
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector2(LinFloat64Angle rotationAngle)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, -rotationAngle.Sin())
            .AddVector(BasisAxis2, rotationAngle.Cos())
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector GetVectorProjection(LinFloat64Vector vector)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, vector.ESp(BasisVector1))
            .AddTerm(BasisAxis2.Index, vector[BasisAxis2.Index])
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector GetVectorRejection(LinFloat64Vector vector)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(vector)
            .AddVector(BasisVector1, -vector.ESp(BasisVector1))
            .AddTerm(BasisAxis2.Index, -vector[BasisAxis2.Index])
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64PlanarRotation GetInversePlanarRotation()
    {
        return LinFloat64AxisToVectorRotation.CreateFromOrthonormalVector(
            BasisAxis2,
            BasisVector1,
            RotationAngle
        );
    }
}