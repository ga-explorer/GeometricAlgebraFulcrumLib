﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Spaces.Conformal;

public abstract class XGaFloat64ConformalIpnsVector :
    XGaFloat64ConformalBlade
{
    protected bool AssumeUnitWeight { get; private set; }

    public override XGaFloat64KVector Blade 
        => Vector;

    public XGaFloat64Vector Vector { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaFloat64ConformalIpnsVector(XGaFloat64ConformalSpace space, XGaFloat64Vector vector)
        : base(space)
    {
        Debug.Assert(
            vector.VSpaceDimensions <= space.VSpaceDimensions
        );

        Vector = vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaFloat64ConformalIpnsVector(XGaFloat64ConformalSpace space, XGaFloat64Vector vector, bool assumeUnitWeight)
        : base(space)
    {
        Debug.Assert(
            vector.VSpaceDimensions <= space.VSpaceDimensions
        );

        AssumeUnitWeight = assumeUnitWeight;
        Vector = vector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Square()
    {
        return Vector.SpSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Weight()
    {
        return AssumeUnitWeight
            ? Processor.ScalarOne
            : -Space.InfinityBasisVector.Sp(Vector);
    }

    public XGaFloat64Vector GetUnitWeightVector()
    {
        if (AssumeUnitWeight)
            return Vector;

        var weight = Weight().ScalarValue;

        if (weight.IsZero())
            return Vector;

        if (!weight.IsOne()) 
            return Vector.Divide(weight);

        AssumeUnitWeight = true;

        return Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasUnitWeight()
    {
        return AssumeUnitWeight || 
               (AssumeUnitWeight = (Weight().ScalarValue - 1d).IsZero());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasZeroWeight()
    {
        return !AssumeUnitWeight && 
               Weight().ScalarValue.IsZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDistance(XGaFloat64Vector positionVector)
    {
        var distance = 
            GetUnitWeightVector().Sp(
                Space.CreateIpnsPoint(positionVector).Vector
            ).ScalarValue;

        return HasZeroWeight()
            ? distance
            : -2d * distance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDistance(XGaFloat64ConformalIpnsVector ipnsVector2)
    {
        var distance =
            GetUnitWeightVector().Sp(ipnsVector2.GetUnitWeightVector()).ScalarValue;

        return HasZeroWeight() || ipnsVector2.HasZeroWeight()
            ? distance
            : -2d * distance;
    }
}