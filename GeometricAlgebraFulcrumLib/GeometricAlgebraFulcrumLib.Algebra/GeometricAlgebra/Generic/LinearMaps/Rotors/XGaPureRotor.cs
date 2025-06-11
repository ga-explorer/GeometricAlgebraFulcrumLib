using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;

/// <summary>
/// A pure rotor is the exponential of a 2-blade. The geometric product of
/// the rotor with its reverse is one. The squared norm of the 2-blade could either
/// be positive, zero, or negative. Each case has its own formulation for the exponential
/// See Section 7.4 of "Geometric Algebra for Computer Science"
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class XGaPureRotor<T>
    : XGaRotorBase<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureRotor<T> Create(T scalarPart, XGaBivector<T> bivectorPart)
    {
        Debug.Assert(scalarPart != null, nameof(scalarPart) + " != null");

        return new XGaPureRotor<T>(
            scalarPart + bivectorPart,
            scalarPart - bivectorPart
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureRotor<T> Create(XGaMultivector<T> multivector)
    {
        return new XGaPureRotor<T>(
            multivector,
            multivector.Reverse()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator XGaMultivector<T>(XGaPureRotor<T> rotor)
    {
        return rotor.Multivector;
    }


    public XGaMultivector<T> Multivector { get; }

    public XGaMultivector<T> MultivectorReverse { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaPureRotor(T scalarPart, XGaBivector<T> bivectorPart)
        : base(bivectorPart.Processor)
    {
        Debug.Assert(scalarPart != null, nameof(scalarPart) + " != null");

        Multivector = scalarPart + bivectorPart;
        MultivectorReverse = scalarPart - bivectorPart;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaPureRotor(XGaMultivector<T> multivector, XGaMultivector<T> multivectorReverse)
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
        if (Multivector.IsEven(2))
            return false;

        // Make sure storage gp reverse(storage) == 1
        var gp = 
            Multivector.Gp(MultivectorReverse);

        if (!gp.IsScalar())
            return false;

        var diff = gp.Scalar() - 1;

        return diff.IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureRotor<T> GetPureRotorInverse()
    {
        return new XGaPureRotor<T>(
            MultivectorReverse, 
            Multivector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaRotor<T> GetRotorInverse()
    {
        return GetPureRotorInverse();
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetScalingFactor()
    {
        return ScalarProcessor.One;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<LinPolarAngle<T>, XGaBivector<T>> GetEuclideanAngleBivector()
    {
        var halfAngle = 
            Multivector.GetScalarPart().ArcCos();

        var angle = 
            halfAngle.DoublePolarAngle();

        var bivector = 
            Multivector.GetBivectorPart() / halfAngle.Sin();

        return new Tuple<LinPolarAngle<T>, XGaBivector<T>>(angle, bivector);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureScalingRotor<T> CreatePureScalingRotor(T scalingFactor)
    {
        var processor = ScalarProcessor;

        var s = processor.Sqrt(scalingFactor);
        var scalarPart = s * Multivector.Scalar();
        var bivectorPart = s * Multivector.GetBivectorPart();

        return XGaPureScalingRotor<T>.Create(
            scalarPart.ScalarValue,
            bivectorPart
        );
    }

}