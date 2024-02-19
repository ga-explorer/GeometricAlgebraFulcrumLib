using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Subspaces;

public sealed class RGaSubspace<T> 
    : IRGaSubspace<T>
{
    private readonly RGaKVector<T> _blade;
    private readonly RGaKVector<T> _bladeInverse;
    private readonly RGaKVector<T> _bladePseudoInverse;


    public RGaProcessor<T> Processor 
        => _blade.Processor;

    public IScalarProcessor<T> ScalarProcessor 
        => _blade.ScalarProcessor;

    public RGaMetric Metric 
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
    internal RGaSubspace(RGaKVector<T> blade)
    {
        _blade = blade;
        BladeSignature = blade.SpSquared().Scalar();
        _bladeInverse = blade / BladeSignature;
        _bladePseudoInverse = blade.PseudoInverse();
    }
        

    bool IGeometricElement.IsValid()
    {
        throw new NotImplementedException();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> GetBlade()
    {
        return _blade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> GetBladeInverse()
    {
        return _bladeInverse;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> GetBladePseudoInverse()
    {
        return _bladePseudoInverse;
    }

    public IRGaSubspace<T> Project(IRGaSubspace<T> subspace)
    {
        var a = GetBlade();
        var aInv = GetBladePseudoInverse();
        var x = subspace.GetBlade();

        var blade =
            x.Lcp(aInv).Lcp(a).GetKVectorPart(x.Grade);
            
        return new RGaSubspace<T>(blade);
    }

    public IRGaSubspace<T> Reflect(IRGaSubspace<T> subspace)
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

        return new RGaSubspace<T>(blade);
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

    public IRGaSubspace<T> VersorProduct(IRGaSubspace<T> subspace)
    {
        var a = GetBlade();
        var aInv = GetBladeInverse();
        var x = subspace.GetBlade();

        var subspaceBlade = subspace.GetBlade();

        var blade = 
            a.Gp(x).Gp(aInv).GetKVectorPart(subspaceBlade.Grade);

        return new RGaSubspace<T>(blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSubspace<T> Complement(IRGaSubspace<T> subspace)
    {
        if (subspace.SubspaceDimension > SubspaceDimension)
            throw new InvalidOperationException();

        var blade = 
            subspace.GetBlade().Lcp(GetBladeInverse()).GetKVectorPart(subspace.SubspaceDimension);

        return new RGaSubspace<T>(blade);
    }
}