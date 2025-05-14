using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Reflectors;

/// <summary>
/// A pure (direct) reflector is a single vector.
/// The reflection happens in the unit vector itself, not its dual hyperplane
/// like the case for pure versors.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class XGaPureReflector<T> : 
    XGaReflectorBase<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaPureReflector<T> Create(XGaVector<T> vector)
    {
        return new XGaPureReflector<T>(vector);
    }
        

    public XGaVector<T> Vector { get; }

    public XGaVector<T> VectorInverse { get; }


    private XGaPureReflector(XGaVector<T> vector)
        : base(vector.Processor)
    {
        Vector = vector;
        VectorInverse = vector.Inverse();
    }

    private XGaPureReflector(XGaVector<T> vector, XGaVector<T> vectorInverse)
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
    public XGaPureReflector<T> GetPureReflectorInverse()
    {
        return new XGaPureReflector<T>(
            VectorInverse, 
            Vector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaReflector<T> GetReflectorInverse()
    {
        return GetPureReflectorInverse();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> OmMap(XGaVector<T> mv)
    {
        return Vector.Gp(mv).Gp(VectorInverse).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> OmMap(XGaBivector<T> mv)
    {
        return Vector.Gp(mv).Gp(VectorInverse).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
    {
        return Vector.Gp(kVector).Gp(VectorInverse).GetHigherKVectorPart(kVector.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> OmMap(XGaMultivector<T> mv)
    {
        return Vector.Gp(mv).Gp(VectorInverse);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivector()
    {
        return Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivectorReverse()
    {
        return Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivectorInverse()
    {
        return VectorInverse;
    }
        
}