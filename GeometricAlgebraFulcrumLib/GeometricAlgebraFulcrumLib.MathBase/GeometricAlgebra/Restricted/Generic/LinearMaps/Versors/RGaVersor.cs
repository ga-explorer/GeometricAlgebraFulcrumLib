using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;

public class RGaVersor<T> : 
    RGaVersorBase<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaVersor<T> Create(RGaMultivector<T> multivector)
    {
        return new RGaVersor<T>(multivector);
    }


    public int Grade { get; }

    public bool IsEven 
        => Grade.IsEven();

    public bool IsOdd 
        => Grade.IsOdd();

    public RGaMultivector<T> Multivector { get; }

    public RGaMultivector<T> MultivectorInverse { get; }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaVersor(RGaMultivector<T> multivector) 
        : base(multivector.Processor)
    {
        Grade = multivector.GetMaxGrade();
        Multivector = multivector;
        MultivectorInverse = Multivector.Inverse();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaVersor(int grade, RGaMultivector<T> multivector, RGaMultivector<T> multivectorInverse) 
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
    public override RGaVector<T> OmMap(RGaVector<T> mv)
    {
        return IsEven 
            ? Multivector.Gp(mv).Gp(MultivectorInverse).GetVectorPart() 
            : Multivector.Gp(-mv).Gp(MultivectorInverse).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaBivector<T> OmMap(RGaBivector<T> mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorInverse).GetBivectorPart();
    }

    public override RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector)
    {
        throw new NotImplementedException();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> OmMap(RGaMultivector<T> mv)
    {
        var v = 
            Multivector.Gp(mv).Gp(MultivectorInverse);

        if (IsEven)
            return v;

        var (evenMv, oddMv) = 
            v.GetEvenOddParts();

        return evenMv - oddMv;
    }

    public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaVersor<T> GetVersorInverse()
    {
        return new RGaVersor<T>(
            Grade,
            MultivectorInverse, 
            Multivector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> GetMultivector()
    {
        return Multivector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> GetMultivectorReverse()
    {
        return Multivector.Reverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> GetMultivectorInverse()
    {
        return MultivectorInverse;
    }
}