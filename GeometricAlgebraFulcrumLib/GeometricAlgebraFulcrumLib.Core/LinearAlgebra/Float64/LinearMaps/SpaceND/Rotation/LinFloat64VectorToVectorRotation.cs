using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;

public sealed class LinFloat64VectorToVectorRotation :
    LinFloat64PlanarRotation
{
    public static LinFloat64VectorToVectorRotation Identity { get; }
        = new LinFloat64VectorToVectorRotation(
            0.CreateLinVector(),
            1.CreateLinVector(),
            LinFloat64PolarAngle.Angle0
        );

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation CreateFromSpanningVectors(LinFloat64Vector spanningVector1, LinFloat64Vector spanningVector2, LinFloat64PolarAngle rotationAngle)
    {
        Debug.Assert(
            !spanningVector1.IsNearParallelTo(spanningVector2)
        );

        var basisVector1 =
            spanningVector1.ToUnitLinVector();

        var basisVector2 =
            spanningVector2.IsNearOppositeToUnit(basisVector1)
                ? basisVector1.GetUnitNormal()
                : spanningVector2.RejectOnUnitVector(basisVector1).ToUnitLinVector();

        return new LinFloat64VectorToVectorRotation(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation CreateFromOrthogonalVectors(LinFloat64Vector spanningVector1, LinFloat64Vector spanningVector2, LinFloat64PolarAngle rotationAngle)
    {
        var basisVector1 = spanningVector1.DivideByENorm();
        var basisVector2 = spanningVector2.DivideByENorm();

        return new LinFloat64VectorToVectorRotation(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation CreateFromOrthonormalVectors(LinFloat64Vector basisVector1, LinFloat64Vector basisVector2, LinFloat64PolarAngle rotationAngle)
    {
        return new LinFloat64VectorToVectorRotation(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation CreateFromRotatedVector(LinFloat64Vector vector, LinFloat64Vector rotatedVector, bool useShortArc = true)
    {
        var basisVector1 =
            vector.ToUnitLinVector();

        var rotationAngle =
            useShortArc
                ? rotatedVector.GetAngleWithUnit(basisVector1).ToPolarAngle()
                : LinFloat64PolarAngle.Angle360.AngleSubtract(rotatedVector.GetAngleWithUnit(basisVector1).RadiansValue).ToPolarAngle();

        if (rotationAngle.IsNearStraight() || rotationAngle.IsNearZeroOrFull())
            return new LinFloat64VectorToVectorRotation(
                basisVector1,
                basisVector1.GetUnitNormal(),
                rotationAngle
            );

        var basisVector2 =
            useShortArc
                ? rotatedVector.RejectOnUnitVector(basisVector1).ToUnitLinVector()
                : rotatedVector.RejectOnUnitVector(basisVector1).VectorNegativeUnit();

        return new LinFloat64VectorToVectorRotation(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation CreateFromComplexEigenPair(Complex eigenValue, MathNet.Numerics.LinearAlgebra.Vector<Complex> eigenVector)
    {
        var rotationAngle = eigenValue.GetPhaseAsPolarAngle();

        //TODO: Why is this the correct one, but not the reverse??!!
        var basisVector1 = eigenVector.Imaginary().ToArray().CreateUnitLinVector();
        var basisVector2 = eigenVector.Real().ToArray().CreateUnitLinVector();

        return new LinFloat64VectorToVectorRotation(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation CreateFromComplexEigenPair(double realValue, double imagValue, double[] realVector, double[] imagVector)
    {
        var rotationAngle = LinFloat64PolarAngle.CreateFromVector(realValue, imagValue);

        //TODO: Why is this the correct one, but not the reverse??!!
        var basisVector1 = imagVector.CreateUnitLinVector();
        var basisVector2 = realVector.CreateUnitLinVector();

        return new LinFloat64VectorToVectorRotation(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }


    public override LinFloat64Vector BasisVector1 { get; }

    public override LinFloat64Vector BasisVector2 { get; }


    private LinFloat64VectorToVectorRotation(LinFloat64Vector basisVector1, LinFloat64Vector basisVector2, LinFloat64PolarAngle rotationAngle)
        : base(rotationAngle)
    {
        BasisVector1 = basisVector1;
        BasisVector2 = basisVector2;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Pair<double> BasisESp(int axisIndex)
    {
        return new Pair<double>(
            BasisVector1[axisIndex],
            BasisVector2[axisIndex]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Pair<double> BasisESp(ILinSignedBasisVector axis)
    {
        return new Pair<double>(
            BasisVector1.GetComponent(axis),
            BasisVector2.GetComponent(axis)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Pair<double> BasisESp(LinFloat64Vector vector)
    {
        return new Pair<double>(
            vector.ESp(BasisVector1),
            vector.ESp(BasisVector2)
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
            .AddVector(BasisVector2, u2Scalar)
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
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector1()
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, RotationAngleCos)
            .AddVector(BasisVector2, RotationAngleSin)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector2()
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, -RotationAngleSin)
            .AddVector(BasisVector2, RotationAngleCos)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector1(LinFloat64Angle rotationAngle)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, rotationAngle.Cos())
            .AddVector(BasisVector2, rotationAngle.Sin())
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector2(LinFloat64Angle rotationAngle)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, -rotationAngle.Sin())
            .AddVector(BasisVector2, rotationAngle.Cos())
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector GetVectorProjection(LinFloat64Vector vector)
    {
        var (vpx, vpy) = BasisESp(vector);

        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, vpx)
            .AddVector(BasisVector2, vpy)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector GetVectorRejection(LinFloat64Vector vector)
    {
        var (vpx, vpy) = BasisESp(vector);

        return LinFloat64VectorComposer
            .Create()
            .SetVector(vector)
            .AddVector(BasisVector1, -vpx)
            .AddVector(BasisVector2, -vpy)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64PlanarRotation GetInversePlanarRotation()
    {
        return new LinFloat64VectorToVectorRotation(
            BasisVector2,
            BasisVector1,
            RotationAngle
        );
    }

}