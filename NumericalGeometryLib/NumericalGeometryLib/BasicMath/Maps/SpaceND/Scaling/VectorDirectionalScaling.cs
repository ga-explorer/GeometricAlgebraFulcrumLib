using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Scaling;

public sealed class VectorDirectionalScaling :
    DirectionalScalingLinearMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorDirectionalScaling Create(double scalingFactor, Float64Tuple scalingVector)
    {
        return new VectorDirectionalScaling(scalingFactor, scalingVector);
    }


    public override int Dimensions 
        => ScalingVector.Dimensions;
    
    public override double ScalingFactor { get; }

    public override Float64Tuple ScalingVector { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VectorDirectionalScaling(double factor, Float64Tuple vector)
    {
        Debug.Assert(
            vector.IsNearUnit() &&
            factor.IsNotNaN() &&
            factor.IsNotExactZero()
        );

        ScalingFactor = factor;
        ScalingVector = vector;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return 
            ScalingVector.IsNearUnit() &&
            ScalingFactor.IsNotNaN() && 
            ScalingFactor.IsNotExactZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple MapVectorBasis(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0 && basisIndex < Dimensions);

        var u = ScalingVector.ScalarArray;
        var s = (ScalingFactor - 1d) * u[basisIndex];

        var y = new double[Dimensions];
        y[basisIndex] = 1d;

        for (var i = 0; i < Dimensions; i++)
            y[i] += s * u[i];

        return Float64Tuple.Create(y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple MapVector(Float64Tuple vector)
    {
        var x = vector.ScalarArray;
        var u = ScalingVector.ScalarArray;
        var s = (ScalingFactor - 1d) * x.VectorDot(u);

        var y = vector.GetScalarArrayCopy();

        for (var i = 0; i < Dimensions; i++)
            y[i] += s * u[i];

        return Float64Tuple.Create(y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IDirectionalScalingLinearMap GetDirectionalScalingInverse()
    {
        return GetVectorDirectionalScalingInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorDirectionalScaling GetVectorDirectionalScalingInverse()
    {
        return new VectorDirectionalScaling(
            1d / ScalingFactor,
            ScalingVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override VectorDirectionalScaling ToVectorDirectionalScaling()
    {
        return this;
    }
}