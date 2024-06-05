using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Spaces.Conformal;

public abstract class RGaFloat64ConformalIpnsVector :
    RGaFloat64ConformalBlade
{
    protected bool AssumeUnitWeight { get; private set; }

    public override RGaFloat64KVector Blade 
        => Vector;

    public RGaFloat64Vector Vector { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected RGaFloat64ConformalIpnsVector(RGaFloat64ConformalSpace space, RGaFloat64Vector vector)
        : base(space)
    {
        Debug.Assert(
            vector.VSpaceDimensions <= space.VSpaceDimensions
        );

        Vector = vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected RGaFloat64ConformalIpnsVector(RGaFloat64ConformalSpace space, RGaFloat64Vector vector, bool assumeUnitWeight)
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
    public RGaFloat64Scalar Weight()
    {
        return AssumeUnitWeight
            ? Processor.ScalarOne
            : -Space.InfinityBasisVector.Sp(Vector);
    }

    public RGaFloat64Vector GetUnitWeightVector()
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
}