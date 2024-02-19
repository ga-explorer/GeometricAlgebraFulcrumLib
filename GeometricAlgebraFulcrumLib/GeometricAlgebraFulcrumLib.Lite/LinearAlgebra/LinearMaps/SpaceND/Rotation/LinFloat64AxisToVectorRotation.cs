using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Rotation;

public sealed class LinFloat64AxisToVectorRotation :
    LinFloat64PlanarRotation
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AxisToVectorRotation CreateFromSpanningVector(ILinSignedBasisVector basisAxis1, Float64Vector spanningVector2, Float64PlanarAngle rotationAngle)
    {
        Debug.Assert(
            !spanningVector2.IsNearParallelTo(basisAxis1)
        );
            
        var basisVector2 = 
            spanningVector2.IsNearOppositeToUnit(basisAxis1)
                ? basisAxis1.GetUnitNormal()
                : spanningVector2.RejectOnUnitVector(basisAxis1).ToUnitVector();
            
        return new LinFloat64AxisToVectorRotation(
            basisAxis1, 
            basisVector2, 
            rotationAngle
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AxisToVectorRotation CreateFromOrthogonalVector(ILinSignedBasisVector basisAxis1, Float64Vector spanningVector2, Float64PlanarAngle rotationAngle)
    {
        var basisVector2 = spanningVector2.DivideByENorm();

        return new LinFloat64AxisToVectorRotation(
            basisAxis1, 
            basisVector2, 
            rotationAngle
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AxisToVectorRotation CreateFromOrthonormalVector(ILinSignedBasisVector basisAxis1, Float64Vector basisVector2, Float64PlanarAngle rotationAngle)
    {
        return new LinFloat64AxisToVectorRotation(
            basisAxis1, 
            basisVector2, 
            rotationAngle
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AxisToVectorRotation CreateFromRotatedVector(ILinSignedBasisVector basisAxis1, Float64Vector rotatedVector, bool useShortArc = true)
    {
        var rotationAngle = 
            useShortArc
                ? rotatedVector.GetAngleWithUnit(basisAxis1)
                : Float64PlanarAngle.Angle360 - rotatedVector.GetAngleWithUnit(basisAxis1);
            
        if (rotationAngle.IsNearStraight() || rotationAngle.IsNearZeroOrFullRotation())
            return new LinFloat64AxisToVectorRotation(
                basisAxis1, 
                basisAxis1.GetUnitNormal(), 
                rotationAngle
            );

        var basisVector2 = 
            useShortArc
                ? rotatedVector.RejectOnUnitVector(basisAxis1).ToUnitVector() 
                : rotatedVector.RejectOnUnitVector(basisAxis1).ToNegativeUnitVector();
            
        return new LinFloat64AxisToVectorRotation(
            basisAxis1, 
            basisVector2, 
            rotationAngle
        );
    }


    public ILinSignedBasisVector BasisAxis1 { get; }

    public override Float64Vector BasisVector1 { get; }

    public override Float64Vector BasisVector2 { get; }


    private LinFloat64AxisToVectorRotation(ILinSignedBasisVector basisAxis1, Float64Vector basisVector2, Float64PlanarAngle rotationAngle)
        : base(rotationAngle)
    {
        BasisAxis1 = basisAxis1;
        BasisVector1 = BasisAxis1.ToVector();
        BasisVector2 = basisVector2;

        Debug.Assert(IsValid());
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Pair<double> BasisESp(int axisIndex)
    {
        return new Pair<double>(
            axisIndex == BasisAxis1.Index ? BasisAxis1.Sign.ToFloat64() : 0d,
            BasisVector2.GetComponent(axisIndex)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Pair<double> BasisESp(ILinSignedBasisVector axis)
    {
        return new Pair<double>(
            axis.Index == BasisAxis1.Index ? (axis.Sign * BasisAxis1.Sign).ToFloat64() : 0d,
            BasisVector2.ESp(axis)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Pair<double> BasisESp(Float64Vector vector)
    {
        return new Pair<double>(
            vector.ESp(BasisAxis1),
            vector.ESp(BasisVector2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector MapBasisVector(int basisIndex)
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
        return Float64VectorComposer
            .Create()
            .SetVector(BasisAxis1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .AddTerm(basisIndex, 1d)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector MapVector(Float64Vector vector)
    {
        // Compute the projection components of the given vector on
        // the orthonormal basis vectors defining the plane of rotation
        var (vpx, vpy) = BasisESp(vector);
        
        var rotationAngleCosMinusOne = RotationAngleCos - 1d;

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCosMinusOne * vpx - RotationAngleSin * vpy;
        var u2Scalar = rotationAngleCosMinusOne * vpy + RotationAngleSin * vpx;

        // The final rotated vector
        return Float64VectorComposer
            .Create()
            .SetVector(vector)
            .AddVector(BasisAxis1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector MapBasisVector1()
    {
        return Float64VectorComposer
            .Create()
            .SetVector(BasisAxis1, RotationAngleCos)
            .AddVector(BasisVector2, RotationAngleSin)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector MapBasisVector2()
    {
        return Float64VectorComposer
            .Create()
            .SetVector(BasisAxis1, -RotationAngleSin)
            .AddVector(BasisVector2, RotationAngleCos)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector MapBasisVector1(Float64PlanarAngle rotationAngle)
    {
        return Float64VectorComposer
            .Create()
            .SetVector(BasisAxis1, rotationAngle.Cos())
            .AddVector(BasisVector2, rotationAngle.Sin())
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector MapBasisVector2(Float64PlanarAngle rotationAngle)
    {
        return Float64VectorComposer
            .Create()
            .SetVector(BasisAxis1, -rotationAngle.Sin())
            .AddVector(BasisVector2, rotationAngle.Cos())
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector GetVectorProjection(Float64Vector vector)
    {
        return Float64VectorComposer
            .Create()
            .SetTerm(BasisAxis1.Index, vector[BasisAxis1.Index])
            .AddVector(BasisVector2, vector.ESp(BasisVector2))
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector GetVectorRejection(Float64Vector vector)
    {
        return Float64VectorComposer
            .Create()
            .SetVector(vector)
            .AddTerm(BasisAxis1.Index, -vector[BasisAxis1.Index])
            .AddVector(BasisVector2, -vector.ESp(BasisVector2))
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64PlanarRotation GetInversePlanarRotation()
    {
        return LinFloat64VectorToAxisRotation.CreateFromOrthonormalVector(
            BasisVector2, 
            BasisAxis1, 
            RotationAngle
        );
    }
}