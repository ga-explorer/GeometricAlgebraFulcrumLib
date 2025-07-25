﻿using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;

public sealed class LinFloat64AxisToVectorRotation :
    LinFloat64PlanarRotation
{
    
    public static LinFloat64AxisToVectorRotation CreateFromSpanningVector(LinBasisVector basisAxis1, LinFloat64Vector spanningVector2, LinFloat64PolarAngle rotationAngle)
    {
        Debug.Assert(
            !spanningVector2.IsNearParallelTo(basisAxis1)
        );

        var vSpaceDimensions = 
            Math.Max(
                basisAxis1.VSpaceDimensions, 
                spanningVector2.VSpaceDimensions
            );

        var basisVector2 =
            spanningVector2.IsNearOppositeToUnit(basisAxis1)
                ? basisAxis1.GetUnitNormal(vSpaceDimensions).ToLinVector()
                : spanningVector2.RejectOnUnitVector(basisAxis1).ToUnitLinVector();

        return new LinFloat64AxisToVectorRotation(
            basisAxis1,
            basisVector2,
            rotationAngle
        );
    }

    
    public static LinFloat64AxisToVectorRotation CreateFromOrthogonalVector(LinBasisVector basisAxis1, LinFloat64Vector spanningVector2, LinFloat64PolarAngle rotationAngle)
    {
        var basisVector2 = spanningVector2.DivideByENorm();

        return new LinFloat64AxisToVectorRotation(
            basisAxis1,
            basisVector2,
            rotationAngle
        );
    }

    
    public static LinFloat64AxisToVectorRotation CreateFromOrthonormalVector(LinBasisVector basisAxis1, LinFloat64Vector basisVector2, LinFloat64PolarAngle rotationAngle)
    {
        return new LinFloat64AxisToVectorRotation(
            basisAxis1,
            basisVector2,
            rotationAngle
        );
    }

    
    public static LinFloat64AxisToVectorRotation CreateFromRotatedVector(LinBasisVector basisAxis1, LinFloat64Vector rotatedVector, bool useShortArc = true)
    {
        var rotationAngle =
            useShortArc
                ? rotatedVector.GetAngleWithUnit(basisAxis1)
                : LinFloat64DirectedAngle.Angle360.AngleSubtract(rotatedVector.GetAngleWithUnit(basisAxis1).RadiansValue).ToPolarAngle();
        
        var vSpaceDimensions = 
            Math.Max(
                basisAxis1.VSpaceDimensions, 
                rotatedVector.VSpaceDimensions
            );

        if (rotationAngle.IsNearStraight() || rotationAngle.IsNearZeroOrFull())
            return new LinFloat64AxisToVectorRotation(
                basisAxis1,
                basisAxis1.GetUnitNormal(vSpaceDimensions).ToLinVector(),
                rotationAngle
            );

        var basisVector2 =
            useShortArc
                ? rotatedVector.RejectOnUnitVector(basisAxis1).ToUnitLinVector()
                : rotatedVector.RejectOnUnitVector(basisAxis1).VectorNegativeUnit();

        return new LinFloat64AxisToVectorRotation(
            basisAxis1,
            basisVector2,
            rotationAngle
        );
    }


    public LinBasisVector BasisAxis1 { get; }

    public override LinFloat64Vector BasisVector1 { get; }

    public override LinFloat64Vector BasisVector2 { get; }


    private LinFloat64AxisToVectorRotation(LinBasisVector basisAxis1, LinFloat64Vector basisVector2, LinFloat64PolarAngle rotationAngle)
        : base(rotationAngle)
    {
        BasisAxis1 = basisAxis1;
        BasisVector1 = BasisAxis1.ToLinVector();
        BasisVector2 = basisVector2;

        Debug.Assert(IsValid());
    }


    
    public override Pair<double> BasisESp(int axisIndex)
    {
        return new Pair<double>(
            axisIndex == BasisAxis1.Index ? BasisAxis1.Sign.ToFloat64() : 0d,
            BasisVector2.GetComponent(axisIndex)
        );
    }

    
    public override Pair<double> BasisESp(LinBasisVector axis)
    {
        return new Pair<double>(
            axis.Index == BasisAxis1.Index ? (axis.Sign * BasisAxis1.Sign).ToFloat64() : 0d,
            BasisVector2.ESp(axis)
        );
    }

    
    public override Pair<double> BasisESp(LinFloat64Vector vector)
    {
        return new Pair<double>(
            vector.ESp(BasisAxis1),
            vector.ESp(BasisVector2)
        );
    }

    
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
            .SetVector(BasisAxis1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .AddTerm(basisIndex, 1d)
            .GetVector();
    }

    
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
            .AddVector(BasisAxis1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    
    public override LinFloat64Vector MapBasisVector1()
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisAxis1, RotationAngleCos)
            .AddVector(BasisVector2, RotationAngleSin)
            .GetVector();
    }

    
    public override LinFloat64Vector MapBasisVector2()
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisAxis1, -RotationAngleSin)
            .AddVector(BasisVector2, RotationAngleCos)
            .GetVector();
    }

    
    public override LinFloat64Vector MapBasisVector1(LinFloat64Angle rotationAngle)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisAxis1, rotationAngle.Cos())
            .AddVector(BasisVector2, rotationAngle.Sin())
            .GetVector();
    }

    
    public override LinFloat64Vector MapBasisVector2(LinFloat64Angle rotationAngle)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisAxis1, -rotationAngle.Sin())
            .AddVector(BasisVector2, rotationAngle.Cos())
            .GetVector();
    }

    
    public override LinFloat64Vector GetVectorProjection(LinFloat64Vector vector)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetTerm(BasisAxis1.Index, vector[BasisAxis1.Index])
            .AddVector(BasisVector2, vector.ESp(BasisVector2))
            .GetVector();
    }

    
    public override LinFloat64Vector GetVectorRejection(LinFloat64Vector vector)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(vector)
            .AddTerm(BasisAxis1.Index, -vector[BasisAxis1.Index])
            .AddVector(BasisVector2, -vector.ESp(BasisVector2))
            .GetVector();
    }

    
    public override LinFloat64PlanarRotation GetInversePlanarRotation()
    {
        return LinFloat64VectorToAxisRotation.CreateFromOrthonormalVector(
            BasisVector2,
            BasisAxis1,
            RotationAngle
        );
    }
}