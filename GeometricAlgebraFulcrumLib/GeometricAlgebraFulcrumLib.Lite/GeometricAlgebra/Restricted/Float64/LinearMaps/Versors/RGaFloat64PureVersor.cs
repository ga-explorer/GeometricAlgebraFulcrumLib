using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;

/// <summary>
/// This class represents a Householder reflection on a hyper-space represented
/// using its dual unit vector
/// </summary>
public sealed class RGaFloat64PureVersor 
    : RGaFloat64VersorBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64PureVersor Create(RGaFloat64Vector unitVectorStorage)
    {
        return new RGaFloat64PureVersor(unitVectorStorage);
    }


    public RGaFloat64Vector Vector { get; }

    public RGaFloat64Vector VectorInverse { get; }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaFloat64PureVersor(RGaFloat64Vector vector)
        : base(vector.Processor)
    {
        Vector = vector;
        VectorInverse = vector.Inverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaFloat64PureVersor(RGaFloat64Vector vector, RGaFloat64Vector vectorInverse)
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
    public RGaFloat64PureVersor GetPureDualVersorInverse()
    {
        return new RGaFloat64PureVersor(VectorInverse, Vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaFloat64Versor GetVersorInverse()
    {
        return GetPureDualVersorInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetMultivector()
    {
        return Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetMultivectorReverse()
    {
        return Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetMultivectorInverse()
    {
        return VectorInverse;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector OmMap(RGaFloat64Vector mv)
    {
        return Vector.Gp(-mv).Gp(VectorInverse).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector OmMap(RGaFloat64Bivector mv)
    {
        return Vector.Gp(mv).Gp(VectorInverse).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
    {
        return Vector.Gp(kVector.GradeInvolution()).Gp(VectorInverse).GetHigherKVectorPart(kVector.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector OmMap(RGaFloat64Multivector mv)
    {
        return Vector.Gp(mv.GradeInvolution()).Gp(VectorInverse);
    }

    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }
}