using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.LinearMaps.Versors;

/// <summary>
/// This class represents a Householder reflection on a hyper-space represented
/// using its dual unit vector
/// </summary>
public sealed class XGaFloat64PureVersor 
    : XGaFloat64VersorBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64PureVersor Create(XGaFloat64Vector unitVectorStorage)
    {
        return new XGaFloat64PureVersor(unitVectorStorage);
    }


    public XGaFloat64Vector Vector { get; }

    public XGaFloat64Vector VectorInverse { get; }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaFloat64PureVersor(XGaFloat64Vector vector)
        : base(vector.Processor)
    {
        Vector = vector;
        VectorInverse = vector.Inverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaFloat64PureVersor(XGaFloat64Vector vector, XGaFloat64Vector vectorInverse)
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
    public XGaFloat64PureVersor GetPureDualVersorInverse()
    {
        return new XGaFloat64PureVersor(VectorInverse, Vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64Versor GetVersorInverse()
    {
        return GetPureDualVersorInverse();
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
    {
        return Vector.Gp(-mv).Gp(VectorInverse).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
    {
        return Vector.Gp(mv).Gp(VectorInverse).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        return Vector.Gp(kVector.GradeInvolution()).Gp(VectorInverse).GetHigherKVectorPart(kVector.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
    {
        return Vector.Gp(mv.GradeInvolution()).Gp(VectorInverse);
    }
}