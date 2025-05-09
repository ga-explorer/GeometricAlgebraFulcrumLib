using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Subspaces;

public sealed class RGaFloat64Subspace 
    : IRGaFloat64Subspace
{
    private readonly RGaFloat64KVector _blade;
    private readonly RGaFloat64KVector _bladeInverse;
    private readonly RGaFloat64KVector _bladePseudoInverse;

        
    public RGaFloat64Processor Processor 
        => _blade.Processor;

    public RGaMetric Metric 
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
    internal RGaFloat64Subspace(RGaFloat64KVector blade)
    {
        _blade = blade;
        BladeSignature = blade.SpSquared();
        _bladeInverse = blade / BladeSignature;
        _bladePseudoInverse = blade.PseudoInverse();
    }
        

    bool IAlgebraicElement.IsValid()
    {
        throw new NotImplementedException();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector GetBlade()
    {
        return _blade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector GetBladeInverse()
    {
        return _bladeInverse;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector GetBladePseudoInverse()
    {
        return _bladePseudoInverse;
    }

    public IRGaFloat64Subspace Project(IRGaFloat64Subspace subspace)
    {
        var a = GetBlade();
        var aInv = GetBladePseudoInverse();
        var x = subspace.GetBlade();

        var blade =
            x.Lcp(aInv).Lcp(a).GetKVectorPart(x.Grade);
            
        return new RGaFloat64Subspace(blade);
    }

    public IRGaFloat64Subspace Reflect(IRGaFloat64Subspace subspace)
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

        return new RGaFloat64Subspace(blade);
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

    public IRGaFloat64Subspace VersorProduct(IRGaFloat64Subspace subspace)
    {
        var a = GetBlade();
        var aInv = GetBladeInverse();
        var x = subspace.GetBlade();

        var subspaceBlade = subspace.GetBlade();

        var blade = 
            a.Gp(x).Gp(aInv).GetKVectorPart(subspaceBlade.Grade);

        return new RGaFloat64Subspace(blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaFloat64Subspace Complement(IRGaFloat64Subspace subspace)
    {
        if (subspace.SubspaceDimension > SubspaceDimension)
            throw new InvalidOperationException();

        var blade = 
            subspace.GetBlade().Lcp(GetBladeInverse()).GetKVectorPart(subspace.SubspaceDimension);

        return new RGaFloat64Subspace(blade);
    }
}