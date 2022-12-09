using System;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Scaling;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND;

public sealed class IdentityLinearMap :
    VectorToVectorRotationLinearMap,
    IDirectionalScalingLinearMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IdentityLinearMap Create(int dimensions)
    {
        return new IdentityLinearMap(dimensions);
    }


    public override int Dimensions { get; }

    public override Float64Tuple SourceVector { get; }

    public override Float64Tuple TargetOrthogonalVector { get; }

    public override Float64Tuple TargetVector
        => SourceVector;

    public override double AngleCos
        => 1d;

    public override PlanarAngle Angle
        => PlanarAngle.Angle0;

    public double ScalingFactor
        => 1d;

    public Float64Tuple ScalingVector
        => SourceVector;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IdentityLinearMap(int dimensions)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        Dimensions = dimensions;
        SourceVector = Float64Tuple.CreateBasis(Dimensions, 0);
        TargetOrthogonalVector = Float64Tuple.CreateZero(Dimensions);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsIdentity()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsNearIdentity(double epsilon = 1e-12d)
    {
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple ProjectOnRotationPlane(Float64Tuple vector)
    {
        return Float64Tuple.CreateZero(Dimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple MapVectorBasis(int axisIndex)
    {
        return Float64Tuple.CreateBasis(Dimensions, axisIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple MapVector(Float64Tuple x)
    {
        return Float64Tuple.CreateCopy(x);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple MapVectorProjection(Float64Tuple vector)
    {
        return Float64Tuple.CreateZero(Dimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override SimpleRotationLinearMap GetSimpleVectorRotationInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IDirectionalScalingLinearMap GetDirectionalScalingInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorDirectionalScaling ToVectorDirectionalScaling()
    {
        return VectorDirectionalScaling.Create(
            1d, 
            Float64Tuple.CreateBasis(Dimensions, 0)
        );
    }
}