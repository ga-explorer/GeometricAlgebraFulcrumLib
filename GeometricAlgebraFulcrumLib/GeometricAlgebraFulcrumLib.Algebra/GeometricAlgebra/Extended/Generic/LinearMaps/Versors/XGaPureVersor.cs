using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;

/// <summary>
/// This class represents a Householder reflection on a hyper-space represented
/// using its dual unit vector
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class XGaPureVersor<T> 
    : XGaVersorBase<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaPureVersor<T> Create(XGaVector<T> unitVectorStorage)
    {
        return new XGaPureVersor<T>(unitVectorStorage);
    }


    public XGaVector<T> Vector { get; }

    public XGaVector<T> VectorInverse { get; }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaPureVersor(XGaVector<T> vector)
        : base(vector.Processor)
    {
        Vector = vector;
        VectorInverse = vector.Inverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaPureVersor(XGaVector<T> vector, XGaVector<T> vectorInverse)
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

        // Make sure storage gp reverse(storage) == 1
        var gp = 
            Vector.Gp(VectorInverse);

        if (!gp.IsScalar())
            return false;

        var diff = gp.Scalar().Abs() - 1;

        return diff.IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureVersor<T> GetPureDualVersorInverse()
    {
        return new XGaPureVersor<T>(VectorInverse, Vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaVersor<T> GetVersorInverse()
    {
        return GetPureDualVersorInverse();
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> OmMap(XGaVector<T> mv)
    {
        return Vector.Gp(-mv).Gp(VectorInverse).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> OmMap(XGaBivector<T> mv)
    {
        return Vector.Gp(mv).Gp(VectorInverse).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
    {
        return Vector.Gp(kVector.GradeInvolution()).Gp(VectorInverse).GetHigherKVectorPart(kVector.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> OmMap(XGaMultivector<T> mv)
    {
        return Vector.Gp(mv.GradeInvolution()).Gp(VectorInverse);
    }
}