﻿using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;

public sealed class LinFloat64AxisToAxisRotation :
    LinFloat64PlanarRotation
{
    
    public static LinFloat64AxisToAxisRotation Create(LinBasisVector basisAxis1, LinBasisVector basisAxis2, LinFloat64PolarAngle rotationAngle)
    {
        return new LinFloat64AxisToAxisRotation(
            basisAxis1,
            basisAxis2,
            rotationAngle
        );
    }


    public LinBasisVector BasisAxis1 { get; }

    public LinBasisVector BasisAxis2 { get; }

    public override LinFloat64Vector BasisVector1 { get; }

    public override LinFloat64Vector BasisVector2 { get; }


    
    private LinFloat64AxisToAxisRotation(LinBasisVector basisAxis1, LinBasisVector basisAxis2, LinFloat64PolarAngle rotationAngle)
        : base(rotationAngle)
    {
        BasisAxis1 = basisAxis1;
        BasisAxis2 = basisAxis2;

        BasisVector1 = BasisAxis1.ToLinVector();
        BasisVector2 = BasisAxis2.ToLinVector();

        Debug.Assert(IsValid());
    }


    
    public override Pair<double> BasisESp(int axisIndex)
    {
        return new Pair<double>(
            axisIndex == BasisAxis1.Index ? BasisAxis1.Sign.ToFloat64() : 0d,
            axisIndex == BasisAxis2.Index ? BasisAxis2.Sign.ToFloat64() : 0d
        );
    }

    
    public override Pair<double> BasisESp(LinBasisVector axis)
    {
        return new Pair<double>(
            axis.Index == BasisAxis1.Index ? (axis.Sign * BasisAxis1.Sign).ToFloat64() : 0d,
            axis.Index == BasisAxis2.Index ? (axis.Sign * BasisAxis2.Sign).ToFloat64() : 0d
        );
    }

    
    public override Pair<double> BasisESp(LinFloat64Vector vector)
    {
        return new Pair<double>(
            vector.GetComponent(BasisAxis1),
            vector.GetComponent(BasisAxis2)
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
            .AddVector(BasisAxis2, u2Scalar)
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
            .AddVector(BasisAxis2, u2Scalar)
            .GetVector();
    }

    
    public override LinFloat64Vector MapBasisVector1()
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisAxis1, RotationAngleCos)
            .AddVector(BasisAxis2, RotationAngleSin)
            .GetVector();
    }

    
    public override LinFloat64Vector MapBasisVector2()
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisAxis1, -RotationAngleSin)
            .AddVector(BasisAxis2, RotationAngleCos)
            .GetVector();
    }

    
    public override LinFloat64Vector MapBasisVector1(LinFloat64Angle rotationAngle)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisAxis1, rotationAngle.Cos())
            .AddVector(BasisAxis2, rotationAngle.Sin())
            .GetVector();
    }

    
    public override LinFloat64Vector MapBasisVector2(LinFloat64Angle rotationAngle)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisAxis1, -rotationAngle.Sin())
            .AddVector(BasisAxis2, rotationAngle.Cos())
            .GetVector();
    }

    
    public override LinFloat64Vector GetVectorProjection(LinFloat64Vector vector)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetTerm(BasisAxis1.Index, vector[BasisAxis1.Index])
            .SetTerm(BasisAxis2.Index, vector[BasisAxis2.Index])
            .GetVector();
    }

    
    public override LinFloat64Vector GetVectorRejection(LinFloat64Vector vector)
    {
        return LinFloat64VectorComposer
            .Create()
            .SetVector(vector)
            .AddTerm(BasisAxis1.Index, -vector[BasisAxis1.Index])
            .AddTerm(BasisAxis2.Index, -vector[BasisAxis2.Index])
            .GetVector();
    }

    
    public override LinFloat64PlanarRotation GetInversePlanarRotation()
    {
        return new LinFloat64AxisToAxisRotation(
            BasisAxis2,
            BasisAxis1,
            RotationAngle
        );
    }
}