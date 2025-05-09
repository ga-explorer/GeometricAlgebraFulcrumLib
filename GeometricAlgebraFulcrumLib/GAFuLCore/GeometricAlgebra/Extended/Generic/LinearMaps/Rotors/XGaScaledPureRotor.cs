using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;

/// <summary>
/// A pure rotor is the exponential of a 2-blade. The geometric product of
/// the rotor with its reverse is one. The squared norm of the 2-blade could either
/// be positive, zero, or negative. Each case has its own formulation for the exponential
/// See Section 7.4 of "Geometric Algebra for Computer Science"
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class XGaScaledPureRotor<T>
    : XGaScaledRotorBase<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaScaledPureRotor<T> CreateIdentity(XGaProcessor<T> processor)
    {
        return new XGaScaledPureRotor<T>(processor, processor.ScalarProcessor.OneValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaScaledPureRotor<T> Create(XGaProcessor<T> processor, T scalarPart)
    {
        return new XGaScaledPureRotor<T>(processor, scalarPart);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaScaledPureRotor<T> Create(T scalarPart, XGaBivector<T> bivectorPart)
    {
        Debug.Assert(scalarPart != null, nameof(scalarPart) + " != null");
        
        return new XGaScaledPureRotor<T>(
            scalarPart + bivectorPart,
            scalarPart - bivectorPart
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaScaledPureRotor<T> Create(IScalar<T> scalarPart, XGaBivector<T> bivectorPart)
    {
        Debug.Assert(scalarPart.ScalarValue != null, "scalarPart.ScalarValue != null");
        
        return new XGaScaledPureRotor<T>(
            scalarPart.ScalarValue + bivectorPart,
            scalarPart.ScalarValue - bivectorPart
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaScaledPureRotor<T> Create(XGaMultivector<T> multivector)
    {
        return new XGaScaledPureRotor<T>(
            multivector,
            multivector.Reverse()
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator XGaMultivector<T>(XGaScaledPureRotor<T> rotor)
    {
        return rotor.Multivector;
    }


    public XGaMultivector<T> Multivector { get; }

    public XGaMultivector<T> MultivectorReverse { get; }


    private XGaScaledPureRotor(XGaProcessor<T> processor, [NotNull] T scalarPart)
        : base(processor)
    {
        Debug.Assert(scalarPart != null, nameof(scalarPart) + " != null");
        
        Multivector = processor.Scalar(scalarPart);
        MultivectorReverse = Multivector;
    }

    private XGaScaledPureRotor([NotNull] T scalarPart, XGaBivector<T> bivectorPart)
        : base(bivectorPart.Processor)
    {
        Debug.Assert(scalarPart != null, nameof(scalarPart) + " != null");
        
        Multivector = scalarPart + bivectorPart;
        MultivectorReverse = scalarPart - bivectorPart;
    }

    private XGaScaledPureRotor(XGaMultivector<T> multivector, XGaMultivector<T> multivectorReverse)
        : base(multivector.Processor)
    {
        Multivector = multivector;
        MultivectorReverse = multivectorReverse;
    }


    public override bool IsValid()
    {
        // Make sure the storage and its reverse are correct
        if (!(Multivector.Reverse() - MultivectorReverse).IsNearZero())
            return false;

        // Make sure storage contains only terms of grades 0,2
        if (Multivector.GetMaxGrade() <= 2 && !Multivector.ContainsKVectorPart(1))
            return false;

        // Make sure storage gp reverse(storage) == scalar
        var gp = 
            Multivector.Gp(MultivectorReverse);

        return gp.IsScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScaledPureRotor<T> GetPureScaledRotorInverse()
    {
        var scalingFactor = GetScalingFactor();
            
        return new XGaScaledPureRotor<T>(
            MultivectorReverse / scalingFactor,
            Multivector / scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetScalingFactor()
    {
        return Multivector.Sp(MultivectorReverse).Scalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureRotor<T> GetPureRotor()
    {
        var mv = Metric.IsEuclidean
            ? Multivector / Multivector.Sp(MultivectorReverse).Sqrt()
            : Multivector / Multivector.Sp(MultivectorReverse).SqrtOfAbs();

        return XGaPureRotor<T>.Create(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaScaledRotor<T> GetScaledRotorInverse()
    {
        return GetPureScaledRotorInverse();
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> OmMap(XGaVector<T> mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> OmMap(XGaBivector<T> mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetHigherKVectorPart(mv.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> OmMap(XGaMultivector<T> mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivector()
    {
        return Multivector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivectorReverse()
    {
        return MultivectorReverse;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivectorInverse()
    {
        return MultivectorReverse;
    }
        
}