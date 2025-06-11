using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Subspaces;

public sealed class XGaSubspace<T> 
    : IXGaElement<T>
{
    private readonly XGaKVector<T> _blade;
    private readonly XGaKVector<T> _bladeInverse;
    private readonly XGaKVector<T> _bladePseudoInverse;


    public XGaProcessor<T> Processor 
        => _blade.Processor;

    public IScalarProcessor<T> ScalarProcessor 
        => _blade.ScalarProcessor;

    public XGaMetric Metric 
        => _blade.Metric;

    public int SubspaceDimension 
        => _blade.Grade;

    //public bool IsDirect { get; }

    //public bool IsDual 
    //    => !IsDirect;

    public Scalar<T> BladeSignature { get; }

    public bool IsValid
        => true;

    public bool IsInvalid 
        => false;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaSubspace(XGaKVector<T> blade)
    {
        _blade = blade;
        BladeSignature = blade.SpSquared();
        _bladeInverse = blade / BladeSignature;
        _bladePseudoInverse = blade.PseudoInverse();
    }
        

    bool IAlgebraicElement.IsValid()
    {
        return IsValid;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> GetBlade()
    {
        return _blade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> GetBladeInverse()
    {
        return _bladeInverse;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> GetBladePseudoInverse()
    {
        return _bladePseudoInverse;
    }

    public XGaSubspace<T> Project(XGaSubspace<T> subspace)
    {
        var a = GetBlade();
        var aInv = GetBladePseudoInverse();
        var x = subspace.GetBlade();

        var blade =
            x.Lcp(aInv).Lcp(a);
            
        return new XGaSubspace<T>(blade);
    }

    public XGaSubspace<T> Reflect(XGaSubspace<T> subspace)
    {
        var a = GetBlade();
        var aInv = GetBladeInverse();
        var x = subspace.GetBlade();

        // This assumes this subspace and the reflected subspace are represented using
        // direct blades
        //TODO: Implement all cases in table 7.1 page 201 in "Geometric Algebra for Computer Science"
        var isNegative = (x.Grade * (a.Grade + 1)).IsOdd();

        var axa = a.Gp(x).Gp(aInv).GetKVectorPart(subspace.SubspaceDimension);

        var blade = 
            isNegative ? -axa : axa;

        return new XGaSubspace<T>(blade);
    }

    //public IGeoSubspace<T> Rotate(IGeoSubspace<T> subspace)
    //{
    //    if (Blade.Grade.IsOdd())
    //        throw new InvalidOperationException();

    //    //Debug.Assert(ScalarProcessor.IsOne(BladeSignature));

    //    var rotatedMv =
    //        GeometricProcessor.Gp(
    //            Blade,
    //            subspace.Blade, 
    //            BladeInverse
    //        ).GetKVectorPart(subspace.Blade.Grade);

    //    var blade = Blade.Grade.GradeHasNegativeReverse()
    //        ? GeometricProcessor.Negative(rotatedMv)
    //        : rotatedMv;

    //    return new GeoSubspace<T>(GeometricProcessor, blade, subspace.IsDirect);
    //}

    public XGaSubspace<T> VersorProduct(XGaSubspace<T> subspace)
    {
        var a = GetBlade();
        var aInv = GetBladeInverse();
        var x = subspace.GetBlade();

        var subspaceBlade = subspace.GetBlade();

        var blade = 
            a.Gp(x).Gp(aInv).GetKVectorPart(subspaceBlade.Grade);

        return new XGaSubspace<T>(blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaSubspace<T> Complement(XGaSubspace<T> subspace)
    {
        if (subspace.SubspaceDimension > SubspaceDimension)
            throw new InvalidOperationException();

        var blade = 
            subspace.GetBlade().Lcp(GetBladeInverse());

        return new XGaSubspace<T>(blade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(XGaVector<T> vector, bool nearZeroFlag = false)
    {
        var processor = ScalarProcessor;

        var mv2 = vector.Op(GetBlade());

        return SubspaceDimension >= 2 &&
               (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(XGaBivector<T> mv, bool nearZeroFlag = false)
    {
        var processor = ScalarProcessor;

        var mv2 = mv - Project(mv);

        return SubspaceDimension >= 2 &&
               (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(XGaKVector<T> mv, bool nearZeroFlag = false)
    {
        var processor = ScalarProcessor;
            
        var mv2 = mv - Project(mv);

        return SubspaceDimension >= 2 &&
               (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(XGaSubspace<T> mv, bool nearZeroFlag = false)
    {
        return Contains(mv.GetBlade(), nearZeroFlag);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Project(XGaVector<T> blade)
    {
        return blade
            .Lcp(GetBladePseudoInverse())
            .Lcp(GetBlade())
            .AsVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> Project(XGaBivector<T> blade)
    {
        return blade
            .Lcp(GetBladePseudoInverse())
            .Lcp(GetBlade())
            .AsBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> Project(XGaKVector<T> blade)
    {
        return blade
            .Lcp(GetBladePseudoInverse())
            .Lcp(GetBlade());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Reflect(XGaVector<T> blade)
    {
        return GetBlade()
            .Gp(-blade)
            .Gp(GetBladeInverse())
            .GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> Reflect(XGaBivector<T> blade)
    {
        return GetBlade()
            .Gp(blade)
            .Gp(GetBladeInverse())
            .GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> Reflect(XGaKVector<T> blade)
    {
        return GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(GetBladeInverse())
            .GetKVectorPart(blade.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> VersorProduct(XGaVector<T> blade)
    {
        return GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(GetBladeInverse())
            .GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> VersorProduct(XGaBivector<T> blade)
    {
        return GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(GetBladeInverse())
            .GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> VersorProduct(XGaKVector<T> blade)
    {
        return GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(GetBladeInverse())
            .GetKVectorPart(blade.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> Complement(XGaVector<T> blade)
    {
        return blade.Lcp(GetBlade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> Complement(XGaBivector<T> blade)
    {
        return blade.Lcp(GetBlade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> Complement(XGaKVector<T> blade)
    {
        return blade.Lcp(GetBlade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Complement(XGaMultivector<T> blade)
    {
        return blade.Lcp(GetBlade());
    }


    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaVector<T>> ProjectVectors(params XGaVector<T>[] vectorsList)
    {
        return vectorsList.Select(Project);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaVector<T>> Project(IEnumerable<XGaVector<T>> vectorsList)
    {
        return vectorsList.Select(Project);
    }

}