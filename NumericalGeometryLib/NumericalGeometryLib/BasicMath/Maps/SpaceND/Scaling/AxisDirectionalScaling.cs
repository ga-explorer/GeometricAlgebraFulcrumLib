using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Scaling;

public sealed class AxisDirectionalScaling :
    DirectionalScalingLinearMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AxisDirectionalScaling Create(int dimensions, double scalingFactor, int scalingBasisIndex)
    {
        return new AxisDirectionalScaling(
            scalingFactor,
            dimensions,
            scalingBasisIndex
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AxisDirectionalScaling Create(double scalingFactor, Axis scalingAxis)
    {
        return new AxisDirectionalScaling(
            scalingFactor,
            scalingAxis.Dimensions,
            scalingAxis.Index
        );
    }


    public override double ScalingFactor { get; }

    public Axis ScalingAxis { get; }

    public override int Dimensions 
        => ScalingAxis.Dimensions;

    public override Float64Tuple ScalingVector
        => ScalingAxis.ToTuple();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AxisDirectionalScaling(double factor, int dimensions, int basisIndex)
    {
        Debug.Assert(
            factor.IsNotExactZero()
        );

        ScalingFactor = factor;
        ScalingAxis = new Axis(dimensions, basisIndex, false);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return 
            ScalingVector.IsNearUnit() &&
            ScalingFactor.IsNotExactZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple MapVectorBasis(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0 && basisIndex < Dimensions
        );

        var y = new double[Dimensions];
        y[basisIndex] = 1d;

        if (basisIndex == ScalingAxis.Index)
            y[basisIndex] += ScalingFactor - 1d;

        return Float64Tuple.Create(y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple MapVector(Float64Tuple vector)
    {
        var x = vector.ScalarArray;
        var s = (ScalingFactor - 1d) * x[ScalingAxis.Index];

        var y = vector.GetScalarArrayCopy();

        y[ScalingAxis.Index] += s;

        return Float64Tuple.Create(y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IDirectionalScalingLinearMap GetDirectionalScalingInverse()
    {
        return GetAxisScalingInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AxisDirectionalScaling GetAxisScalingInverse()
    {
        return new AxisDirectionalScaling(
            1d / ScalingFactor,
            Dimensions,
            ScalingAxis.Index
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override VectorDirectionalScaling ToVectorDirectionalScaling()
    {
        return VectorDirectionalScaling.Create(
            1d, 
            ScalingVector
        );
    }
}