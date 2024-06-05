using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Subspaces;

public sealed class XGaSubspace<T> 
    : IXGaSubspace<T>
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
        throw new NotImplementedException();
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

    public IXGaSubspace<T> Project(IXGaSubspace<T> subspace)
    {
        var a = GetBlade();
        var aInv = GetBladePseudoInverse();
        var x = subspace.GetBlade();

        var blade =
            x.Lcp(aInv).Lcp(a);
            
        return new XGaSubspace<T>(blade);
    }

    public IXGaSubspace<T> Reflect(IXGaSubspace<T> subspace)
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

    public IXGaSubspace<T> VersorProduct(IXGaSubspace<T> subspace)
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
    public IXGaSubspace<T> Complement(IXGaSubspace<T> subspace)
    {
        if (subspace.SubspaceDimension > SubspaceDimension)
            throw new InvalidOperationException();

        var blade = 
            subspace.GetBlade().Lcp(GetBladeInverse());

        return new XGaSubspace<T>(blade);
    }
}