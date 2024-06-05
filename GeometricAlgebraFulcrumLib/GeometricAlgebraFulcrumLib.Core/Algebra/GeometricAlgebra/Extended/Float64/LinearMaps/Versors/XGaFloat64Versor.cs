using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Versors;

public class XGaFloat64Versor : 
    XGaFloat64VersorBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64Versor Create(XGaFloat64Multivector multivector)
    {
        return new XGaFloat64Versor(multivector);
    }


    public int Grade { get; }

    public bool IsEven 
        => Grade.IsEven();

    public bool IsOdd 
        => Grade.IsOdd();

    public XGaFloat64Multivector Multivector { get; }

    public XGaFloat64Multivector MultivectorInverse { get; }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaFloat64Versor(XGaFloat64Multivector multivector) 
        : base(multivector.Processor)
    {
        Grade = multivector.GetMaxGrade();
        Multivector = multivector;
        MultivectorInverse = Multivector.Inverse();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaFloat64Versor(int grade, XGaFloat64Multivector multivector, XGaFloat64Multivector multivectorInverse) 
        : base(multivector.Processor)
    {
        Grade = grade;
        Multivector = multivector;
        MultivectorInverse = multivectorInverse;
    }


    public override bool IsValid()
    {
        // Make sure the storage and its inverse are correct
        if (!(Multivector.Inverse() - MultivectorInverse).IsNearZero())
            return false;

        // Make sure storage contains terms of only even or only odd grade
        var grades = Multivector.KVectorGrades.ToArray();
        if (!grades.All(g => g.IsEven()) && !grades.All(g => g.IsOdd()))
            return false;

        // Make sure storage gp versorInverse(storage) == 1
        var gp = 
            Multivector.Gp(MultivectorInverse);

        if (!gp.IsScalar())
            return false;

        var diff = gp.Scalar() - 1;

        return diff.IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
    {
        return IsEven 
            ? Multivector.Gp(mv).Gp(MultivectorInverse).GetVectorPart() 
            : Multivector.Gp(-mv).Gp(MultivectorInverse).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorInverse).GetBivectorPart();
    }

    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        throw new NotImplementedException();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
    {
        var v = 
            Multivector.Gp(mv).Gp(MultivectorInverse);

        if (IsEven)
            return v;

        var (evenMv, oddMv) = 
            v.GetEvenOddParts();

        return evenMv - oddMv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64Versor GetVersorInverse()
    {
        return new XGaFloat64Versor(
            Grade,
            MultivectorInverse, 
            Multivector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivector()
    {
        return Multivector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivectorReverse()
    {
        return Multivector.Reverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivectorInverse()
    {
        return MultivectorInverse;
    }
}