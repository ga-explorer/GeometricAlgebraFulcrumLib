using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public abstract class RGaConformalIpnsVector<T> :
    RGaConformalBlade<T>
{
    protected bool AssumeUnitWeight { get; private set; }

    public override RGaKVector<T> Blade 
        => Vector;

    public RGaVector<T> Vector { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected RGaConformalIpnsVector(RGaConformalSpace<T> space, RGaVector<T> vector)
        : base(space)
    {
        Debug.Assert(
            vector.VSpaceDimensions <= space.VSpaceDimensions
        );

        Vector = vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected RGaConformalIpnsVector(RGaConformalSpace<T> space, RGaVector<T> vector, bool assumeUnitWeight)
        : base(space)
    {
        Debug.Assert(
            vector.VSpaceDimensions <= space.VSpaceDimensions
        );

        AssumeUnitWeight = assumeUnitWeight;
        Vector = vector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Square()
    {
        return Vector.SpSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> Weight()
    {
        return AssumeUnitWeight
            ? Processor.ScalarOne
            : -Space.InfinityBasisVector.Sp(Vector);
    }

    public RGaVector<T> GetUnitWeightVector()
    {
        if (AssumeUnitWeight)
            return Vector;

        var weight = Weight();

        if (weight.IsZero)
            return Vector;

        if (!weight.IsOne) 
            return Vector.Divide(weight);

        AssumeUnitWeight = true;

        return Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasUnitWeight()
    {
        return AssumeUnitWeight || 
               (AssumeUnitWeight = (Weight() - 1d).IsZero);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasZeroWeight()
    {
        return !AssumeUnitWeight && 
               Weight().IsZero;
    }
}