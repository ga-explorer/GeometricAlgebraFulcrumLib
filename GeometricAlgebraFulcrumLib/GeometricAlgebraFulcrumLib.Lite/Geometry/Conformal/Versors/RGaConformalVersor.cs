using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Versors;

public sealed record RGaConformalVersor
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor operator -(RGaConformalVersor blade)
    {
        return blade.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor operator +(RGaConformalVersor blade1, RGaConformalVersor blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor operator -(RGaConformalVersor blade1, RGaConformalVersor blade2)
    {
        return blade1.Subtract(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor operator *(double scalar, RGaConformalVersor blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor operator *(RGaConformalVersor blade, double scalar)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor operator /(RGaConformalVersor blade, double scalar)
    {
        return blade.Divide(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor operator /(double scalar, RGaConformalVersor blade)
    {
        return blade.Inverse().Times(scalar);
    }


    public RGaConformalSpace ConformalSpace { get; }

    public RGaGeometrySpaceBasisSpecs BasisSpecs 
        => ConformalSpace.BasisSpecs;

    public RGaFloat64Multivector InternalMultivector { get; }

    public RGaFloat64Multivector InternalMultivectorInverse { get; }
    
    public RGaFloat64ConformalProcessor ConformalProcessor
        => ConformalSpace.ConformalProcessor;
    
    public int VSpaceDimensions
        => ConformalSpace.VSpaceDimensions;

    public double this[int i]
        => InternalMultivector[i];
    
    public double this[int i, int j]
        => InternalMultivector[i, j];

    public double this[int i, int j, int k]
        => InternalMultivector[i, j, k];
    
    public double this[params int[] indexList]
        => InternalMultivector[indexList];
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalVersor(RGaConformalSpace conformalSpace, RGaFloat64Multivector multivector)
    {
        Debug.Assert(
            multivector.Processor.HasSameSignature(conformalSpace.ConformalProcessor) &&
            conformalSpace.IsValidElement(multivector)
        );

        InternalMultivector = multivector;
        InternalMultivectorInverse = multivector.Inverse();
        ConformalSpace = conformalSpace;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalVersor(RGaConformalSpace conformalSpace, RGaFloat64Multivector multivector, RGaFloat64Multivector multivectorInverse)
    {
        Debug.Assert(
            multivector.Processor.HasSameSignature(conformalSpace.ConformalProcessor) &&
            multivectorInverse.Processor.HasSameSignature(conformalSpace.ConformalProcessor) &&
            multivector.Gp(multivectorInverse).Subtract(conformalSpace.ConformalProcessor.CreateOneScalar()).IsNearZero() &&
            conformalSpace.IsValidElement(multivector)
        );

        InternalMultivector = multivector;
        InternalMultivectorInverse = multivector.Inverse();
        ConformalSpace = conformalSpace;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out RGaConformalSpace conformalSpace, out RGaFloat64Multivector multivector, out RGaFloat64Multivector multivectorInverse)
    {
        conformalSpace = ConformalSpace;
        multivector = InternalMultivector;
        multivectorInverse = InternalMultivectorInverse;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return InternalMultivector.IsZero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double epsilon = 1e-12)
    {
        return InternalMultivector.IsNearZero(epsilon);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double SpSquared()
    {
        return InternalMultivector.SpSquared().ScalarValue();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double NormSquared()
    {
        return InternalMultivector.NormSquared().ScalarValue();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Norm()
    {
        return InternalMultivector.NormSquared().ScalarValue().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Negative()
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Reverse()
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor GradeInvolution()
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.GradeInvolution()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor CliffordConjugate()
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.CliffordConjugate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Inverse()
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Inverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Times(double scalar)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Times(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Divide(double scalar)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Times(1d / scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor DivideByNorm()
    {
        var norm = InternalMultivector.Norm();

        return norm.IsNearZero()
            ? this
            : Divide(norm.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor EGaDual()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalMultivector)
        );

        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Lcp(ConformalSpace.IeInvKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector EGaDualMultivector()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalMultivector)
        );

        return InternalMultivector.Lcp(ConformalSpace.IeInvKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor EGaUnDual()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalMultivector)
        );

        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Lcp(ConformalSpace.IeKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector EGaUnDualMultivector()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalMultivector)
        );

        return InternalMultivector.Lcp(ConformalSpace.IeKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor PGaDual()
    {
        return new RGaConformalVersor(
            ConformalSpace,
            PGaDualMultivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector PGaDualMultivector()
    {
        Debug.Assert(
            ConformalSpace.IsValidPGaElement(InternalMultivector)
        );

        return ConformalSpace.MusicalIsomorphism.OmMap(
            InternalMultivector.Op(ConformalSpace.EiVector).Lcp(ConformalSpace.IcInvKVector)
            // Also can be mv.Op(Ei).Lcp(Ic)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor PGaUnDual()
    {
        return new RGaConformalVersor(
            ConformalSpace,
            PGaUnDualMultivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector PGaUnDualMultivector()
    {
        Debug.Assert(
            ConformalSpace.IsValidPGaElement(InternalMultivector)
        );

        return ConformalSpace.MusicalIsomorphism.OmMap(
            InternalMultivector.Op(ConformalSpace.EiVector).Lcp(ConformalSpace.IcInvKVector)
            // Also can be mv.Op(Ei).Lcp(Ic)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor CGaDual()
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Lcp(ConformalSpace.IcInvKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor CGaUnDual()
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Lcp(ConformalSpace.IcKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Add(RGaConformalVersor blade2)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Add(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Subtract(RGaConformalVersor blade2)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Subtract(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Sp(RGaConformalVersor blade2)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Sp(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Sp(RGaFloat64KVector blade2)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Sp(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Op(RGaConformalVersor blade2)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Op(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Op(RGaFloat64KVector blade2)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Op(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Lcp(RGaConformalVersor blade2)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Lcp(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Lcp(RGaFloat64KVector blade2)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Lcp(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Rcp(RGaConformalVersor blade2)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Rcp(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Rcp(RGaFloat64KVector blade2)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Rcp(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Fdp(RGaConformalVersor blade2)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Fdp(blade2.InternalMultivector).GetFirstKVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor Fdp(RGaFloat64KVector blade2)
    {
        return new RGaConformalVersor(
            ConformalSpace,
            InternalMultivector.Fdp(blade2).GetFirstKVectorPart()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Gp(RGaConformalVersor mv2)
    {
        return InternalMultivector.Gp(mv2.InternalMultivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Gp(RGaFloat64Multivector mv2)
    {
        return InternalMultivector.Gp(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor ScaleBy(double scalingFactor)
    {
        Debug.Assert(
            scalingFactor.IsValid() &&
            scalingFactor > 0
        );

        var g = 0.5 * scalingFactor.LogE();
        var s0 = Math.Cosh(g);
        var s2 = Math.Sinh(g) * ConformalSpace.EoiBivector;

        var kVector =
            (s0 + s2).Gp(InternalMultivector).Gp(s0 - s2);

        return new RGaConformalVersor(ConformalSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalBlade MapBlade(RGaFloat64KVector blade)
    {
        var mappedBlade =
            InternalMultivector
                .Gp(blade)
                .Gp(InternalMultivectorInverse)
                .GetKVectorPart(blade.Grade);
        
        return new RGaConformalBlade(ConformalSpace, mappedBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade MapBlade(RGaConformalBlade blade)
    {
        return MapBlade(blade.InternalKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalVersor MapVersor(RGaFloat64Multivector versor)
    {
        var mappedBlade =
            InternalMultivector
                .Gp(versor)
                .Gp(InternalMultivectorInverse);
        
        return new RGaConformalVersor(ConformalSpace, mappedBlade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor MapVersor(RGaConformalVersor versor)
    {
        return MapVersor(versor.InternalMultivector);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalBlade InverseMapBlade(RGaFloat64KVector blade)
    {
        var mappedBlade =
            InternalMultivectorInverse
                .Gp(blade)
                .Gp(InternalMultivector)
                .GetKVectorPart(blade.Grade);
        
        return new RGaConformalBlade(ConformalSpace, mappedBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade InverseMapBlade(RGaConformalBlade blade)
    {
        return InverseMapBlade(blade.InternalKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalVersor InverseMapVersor(RGaFloat64Multivector versor)
    {
        var mappedBlade =
            InternalMultivectorInverse
                .Gp(versor)
                .Gp(InternalMultivector);
        
        return new RGaConformalVersor(ConformalSpace, mappedBlade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalVersor InverseMapVersor(RGaConformalVersor versor)
    {
        return InverseMapVersor(versor.InternalMultivector);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX()
    {
        return BasisSpecs.ToLaTeX(InternalMultivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(RGaGeometrySpaceBasisSpecs basisSpecs)
    {
        return basisSpecs.ToLaTeX(InternalMultivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return ToLaTeX();
    }
}