using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Spaces.Conformal;

public abstract class XGaConformalIpnsVector<T> :
    XGaConformalBlade<T>
{
    protected bool AssumeUnitWeight { get; private set; }

    public override XGaKVector<T> Blade 
        => Vector;

    public XGaVector<T> Vector { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaConformalIpnsVector(XGaConformalSpace<T> space, XGaVector<T> vector)
        : base(space)
    {
        Debug.Assert(
            vector.VSpaceDimensions <= space.VSpaceDimensions
        );

        Vector = vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaConformalIpnsVector(XGaConformalSpace<T> space, XGaVector<T> vector, bool assumeUnitWeight)
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
    public Scalar<T> Weight()
    {
        return AssumeUnitWeight
            ? ScalarProcessor.One
            : -Space.InfinityBasisVector.Sp(Vector).ToScalar();
    }

    public XGaVector<T> GetUnitWeightVector()
    {
        if (AssumeUnitWeight)
            return Vector;

        var weight = Weight();

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
               (AssumeUnitWeight = (Weight() - 1d).IsZero());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasZeroWeight()
    {
        return !AssumeUnitWeight && 
               Weight().IsZero();
    }
}