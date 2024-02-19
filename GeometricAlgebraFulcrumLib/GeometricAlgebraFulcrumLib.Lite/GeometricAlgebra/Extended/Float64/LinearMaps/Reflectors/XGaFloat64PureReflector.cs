﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Reflectors;

/// <summary>
/// A pure (direct) reflector is a single vector.
/// The reflection happens in the unit vector itself, not its dual hyperplane
/// like the case for pure versors.
/// </summary>
public sealed class XGaFloat64PureReflector : 
    XGaFloat64ReflectorBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64PureReflector Create(XGaFloat64Vector vector)
    {
        return new XGaFloat64PureReflector(vector);
    }
        

    public XGaFloat64Vector Vector { get; }

    public XGaFloat64Vector VectorInverse { get; }


    private XGaFloat64PureReflector(XGaFloat64Vector vector)
        : base(vector.Processor)
    {
        Vector = vector;
        VectorInverse = vector.Inverse();
    }

    private XGaFloat64PureReflector(XGaFloat64Vector vector, XGaFloat64Vector vectorInverse)
        : base(vector.Processor)
    {
        Vector = vector;
        VectorInverse = vectorInverse;
    }


    public override bool IsValid()
    {
        // Make sure the storage and its reverse are correct
        if (!(Vector.Inverse() - VectorInverse).IsNearZero())
            return false;

        //// Make sure storage gp inverse(storage) == 1
        //var gp = 
        //    GeometricProcessor.Gp(Vector, VectorInverse);

        //if (!gp.IsScalar())
        //    return false;

        //var diff =
        //    GeometricProcessor.Subtract(
        //        GeometricProcessor.GetTermScalar(gp, 0),
        //        GeometricProcessor.ScalarOne
        //    );

        //return GeometricProcessor.IsNearZero(diff);

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64PureReflector GetPureReflectorInverse()
    {
        return new XGaFloat64PureReflector(
            VectorInverse, 
            Vector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64Reflector GetReflectorInverse()
    {
        return GetPureReflectorInverse();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
    {
        return Vector.Gp(mv).Gp(VectorInverse).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
    {
        return Vector.Gp(mv).Gp(VectorInverse).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        return Vector.Gp(kVector).Gp(VectorInverse).GetHigherKVectorPart(kVector.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
    {
        return Vector.Gp(mv).Gp(VectorInverse);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivector()
    {
        return Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivectorReverse()
    {
        return Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivectorInverse()
    {
        return VectorInverse;
    }
        
}