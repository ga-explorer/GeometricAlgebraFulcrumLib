using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Subspaces;

/// <summary>
/// Initially use OPNS (i.e. direct) representation of subspaces
/// </summary>
public sealed class XGaFloat64Subspace :
    IXGaElement
{
    private readonly XGaFloat64KVector _blade;
    private readonly XGaFloat64KVector _bladeInverse;
    private readonly XGaFloat64KVector _bladePseudoInverse;

        
    public XGaFloat64Processor Processor 
        => _blade.Processor;

    public XGaMetric Metric 
        => _blade.Metric;

    public int SubspaceDimension 
        => _blade.Grade;

    //public bool IsDirect { get; }

    //public bool IsDual 
    //    => !IsDirect;

    public double BladeSignature { get; }

    public bool IsValid
        => true;

    public bool IsInvalid 
        => false;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64Subspace(XGaFloat64KVector blade)
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
    public XGaFloat64KVector GetBlade()
    {
        return _blade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector GetBladeInverse()
    {
        return _bladeInverse;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector GetBladePseudoInverse()
    {
        return _bladePseudoInverse;
    }

    public XGaFloat64Subspace Project(XGaFloat64Subspace subspace)
    {
        var a = GetBlade();
        var aInv = GetBladePseudoInverse();
        var x = subspace.GetBlade();

        var blade =
            x.Lcp(aInv).Lcp(a);
            
        return new XGaFloat64Subspace(blade);
    }

    public XGaFloat64Subspace Reflect(XGaFloat64Subspace subspace)
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

        return new XGaFloat64Subspace(blade);
    }

    //public IGeoSubspace Rotate(IGeoSubspace subspace)
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

    //    return new GeoSubspace(GeometricProcessor, blade, subspace.IsDirect);
    //}

    public XGaFloat64Subspace VersorProduct(XGaFloat64Subspace subspace)
    {
        var a = GetBlade();
        var aInv = GetBladeInverse();
        var x = subspace.GetBlade();

        var subspaceBlade = subspace.GetBlade();

        var blade = 
            a.Gp(x).Gp(aInv).GetKVectorPart(subspaceBlade.Grade);

        return new XGaFloat64Subspace(blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Subspace Complement(XGaFloat64Subspace subspace)
    {
        if (subspace.SubspaceDimension > SubspaceDimension)
            throw new InvalidOperationException();

        var blade = 
            subspace.GetBlade().Lcp(GetBladeInverse());

        return new XGaFloat64Subspace(blade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(XGaFloat64Vector vector, bool nearZeroFlag = false)
    {
        var mv2 = vector.Op(GetBlade());

        return SubspaceDimension >= 2 &&
               (mv2.IsZero || mv2.Scalars.All(s => s.IsZero(nearZeroFlag)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(XGaFloat64Bivector mv, bool nearZeroFlag = false)
    {
        var mv2 = mv - Project(mv);

        return SubspaceDimension >= 2 &&
               (mv2.IsZero || mv2.Scalars.All(s => s.IsZero(nearZeroFlag)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(XGaFloat64KVector mv, bool nearZeroFlag = false)
    {
        var mv2 = mv - Project(mv);

        return SubspaceDimension >= 2 &&
               (mv2.IsZero || mv2.Scalars.All(s => s.IsZero(nearZeroFlag)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(XGaFloat64Subspace mv, bool nearZeroFlag = false)
    {
        return Contains(mv.GetBlade(), nearZeroFlag);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Project(XGaFloat64Vector blade)
    {
        return blade
            .Lcp(GetBladePseudoInverse())
            .Lcp(GetBlade())
            .GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Project(XGaFloat64Bivector blade)
    {
        return blade
            .Lcp(GetBladePseudoInverse())
            .Lcp(GetBlade())
            .GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Project(XGaFloat64KVector blade)
    {
        return blade
            .Lcp(GetBladePseudoInverse())
            .Lcp(GetBlade());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Reflect(XGaFloat64Vector blade)
    {
        return GetBlade()
            .Gp(-blade)
            .Gp(GetBladeInverse())
            .GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Reflect(XGaFloat64Bivector blade)
    {
        return GetBlade()
            .Gp(blade)
            .Gp(GetBladeInverse())
            .GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Reflect(XGaFloat64KVector blade)
    {
        return GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(GetBladeInverse())
            .GetKVectorPart(blade.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector VersorProduct(XGaFloat64Vector blade)
    {
        return GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(GetBladeInverse())
            .GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector VersorProduct(XGaFloat64Bivector blade)
    {
        return GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(GetBladeInverse())
            .GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector VersorProduct(XGaFloat64KVector blade)
    {
        return GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(GetBladeInverse())
            .GetKVectorPart(blade.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Complement(XGaFloat64Vector blade)
    {
        return blade.Lcp(GetBlade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Complement(XGaFloat64Bivector blade)
    {
        return blade.Lcp(GetBlade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Complement(XGaFloat64KVector blade)
    {
        return blade.Lcp(GetBlade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Complement(XGaFloat64Multivector blade)
    {
        return blade.Lcp(GetBlade());
    }


    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaFloat64Vector> ProjectVectors(params XGaFloat64Vector[] vectorsList)
    {
        return vectorsList.Select(Project);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaFloat64Vector> Project(IEnumerable<XGaFloat64Vector> vectorsList)
    {
        return vectorsList.Select(Project);
    }

}