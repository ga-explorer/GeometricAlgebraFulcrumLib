using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Versors;

public sealed record CGaFloat64Versor
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor operator -(CGaFloat64Versor blade)
    {
        return blade.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor operator +(CGaFloat64Versor blade1, CGaFloat64Versor blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor operator -(CGaFloat64Versor blade1, CGaFloat64Versor blade2)
    {
        return blade1.Subtract(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor operator *(double scalar, CGaFloat64Versor blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor operator *(CGaFloat64Versor blade, double scalar)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor operator /(CGaFloat64Versor blade, double scalar)
    {
        return blade.Divide(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor operator /(double scalar, CGaFloat64Versor blade)
    {
        return blade.Inverse().Times(scalar);
    }


    public CGaFloat64GeometricSpace GeometricSpace { get; }

    public GaFloat64GeometricSpaceBasisSpecs BasisSpecs
        => GeometricSpace.BasisSpecs;

    public RGaFloat64Multivector InternalMultivector { get; }

    public RGaFloat64Multivector InternalMultivectorInverse { get; }

    public RGaFloat64ConformalProcessor ConformalProcessor
        => GeometricSpace.ConformalProcessor;

    public int VSpaceDimensions
        => GeometricSpace.VSpaceDimensions;

    public double this[int i]
        => InternalMultivector[i];

    public double this[int i, int j]
        => InternalMultivector[i, j];

    public double this[int i, int j, int k]
        => InternalMultivector[i, j, k];

    public double this[params int[] indexList]
        => InternalMultivector[indexList];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFloat64Versor(CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Multivector multivector)
        : this(cgaGeometricSpace, multivector, multivector.Inverse())
    {
        //Debug.Assert(
        //    multivector.Processor.HasSameSignature(cgaGeometricSpace.ConformalProcessor) &&
        //    cgaGeometricSpace.IsValidElement(multivector)
        //);

        //InternalMultivector = multivector;
        //InternalMultivectorInverse = multivector.Inverse();
        //ConformalSpace = cgaGeometricSpace;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFloat64Versor(CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Multivector multivector, RGaFloat64Multivector multivectorInverse)
    {
        Debug.Assert(
            multivector.Processor.HasSameSignature(cgaGeometricSpace.ConformalProcessor) &&
            multivectorInverse.Processor.HasSameSignature(cgaGeometricSpace.ConformalProcessor) &&
            multivector.Gp(multivectorInverse).Subtract(cgaGeometricSpace.ConformalProcessor.ScalarOne).IsNearZero() &&
            cgaGeometricSpace.IsValidElement(multivector)
        );

        InternalMultivector = multivector;
        InternalMultivectorInverse = multivectorInverse;
        GeometricSpace = cgaGeometricSpace;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out CGaFloat64GeometricSpace cgaGeometricSpace, out RGaFloat64Multivector multivector, out RGaFloat64Multivector multivectorInverse)
    {
        cgaGeometricSpace = GeometricSpace;
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
        return InternalMultivector.SpSquared().ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double NormSquared()
    {
        return InternalMultivector.NormSquared().ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Norm()
    {
        return InternalMultivector.NormSquared().ScalarValue.SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Negative()
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Reverse()
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor GradeInvolution()
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.GradeInvolution()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor CliffordConjugate()
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.CliffordConjugate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Inverse()
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Inverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Times(double scalar)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Times(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Divide(double scalar)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Times(1d / scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor DivideByNorm()
    {
        var norm = InternalMultivector.Norm();

        return norm.IsNearZero()
            ? this
            : Divide(norm.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor VGaDual()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalMultivector)
        );

        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Lcp(GeometricSpace.IeInvKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector VGaDualMultivector()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalMultivector)
        );

        return InternalMultivector.Lcp(GeometricSpace.IeInvKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor VGaUnDual()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalMultivector)
        );

        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Lcp(GeometricSpace.IeKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector VGaUnDualMultivector()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalMultivector)
        );

        return InternalMultivector.Lcp(GeometricSpace.IeKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor PGaDual()
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            PGaDualMultivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector PGaDualMultivector()
    {
        Debug.Assert(
            GeometricSpace.IsValidPGaElement(InternalMultivector)
        );

        return GeometricSpace.MusicalIsomorphism.OmMap(
            InternalMultivector.Op(GeometricSpace.EiVector).Lcp(GeometricSpace.IcInvKVector)
        // Also can be mv.Op(Ei).Lcp(Ic)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor PGaUnDual()
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            PGaUnDualMultivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector PGaUnDualMultivector()
    {
        Debug.Assert(
            GeometricSpace.IsValidPGaElement(InternalMultivector)
        );

        return GeometricSpace.MusicalIsomorphism.OmMap(
            InternalMultivector.Op(GeometricSpace.EiVector).Lcp(GeometricSpace.IcInvKVector)
        // Also can be mv.Op(Ei).Lcp(Ic)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor CGaDual()
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Lcp(GeometricSpace.IcInvKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor CGaUnDual()
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Lcp(GeometricSpace.IcKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Add(CGaFloat64Versor blade2)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Add(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Subtract(CGaFloat64Versor blade2)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Subtract(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Sp(CGaFloat64Versor blade2)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Sp(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Sp(RGaFloat64KVector blade2)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Sp(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Op(CGaFloat64Versor blade2)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Op(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Op(RGaFloat64KVector blade2)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Op(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Lcp(CGaFloat64Versor blade2)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Lcp(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Lcp(RGaFloat64KVector blade2)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Lcp(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Rcp(CGaFloat64Versor blade2)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Rcp(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Rcp(RGaFloat64KVector blade2)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Rcp(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Fdp(CGaFloat64Versor blade2)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Fdp(blade2.InternalMultivector).GetFirstKVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Fdp(RGaFloat64KVector blade2)
    {
        return new CGaFloat64Versor(
            GeometricSpace,
            InternalMultivector.Fdp(blade2).GetFirstKVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Gp(CGaFloat64Versor mv2)
    {
        return InternalMultivector.Gp(mv2.InternalMultivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Gp(RGaFloat64Multivector mv2)
    {
        return InternalMultivector.Gp(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor ScaleBy(double scalingFactor)
    {
        Debug.Assert(
            scalingFactor.IsValid() &&
            scalingFactor > 0
        );

        var g = 0.5 * scalingFactor.LogE();
        var s0 = Math.Cosh(g);
        var s2 = Math.Sinh(g) * GeometricSpace.EoiBivector;

        var kVector =
            (s0 + s2).Gp(InternalMultivector).Gp(s0 - s2);

        return new CGaFloat64Versor(GeometricSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFloat64Blade MapBlade(RGaFloat64KVector blade)
    {
        var mappedBlade =
            InternalMultivector
                .Gp(blade)
                .Gp(InternalMultivectorInverse)
                .GetKVectorPart(blade.Grade);

        return new CGaFloat64Blade(GeometricSpace, mappedBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade MapBlade(CGaFloat64Blade blade)
    {
        return MapBlade(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFloat64Versor MapVersor(RGaFloat64Multivector versor)
    {
        var mappedBlade =
            InternalMultivector
                .Gp(versor)
                .Gp(InternalMultivectorInverse);

        return new CGaFloat64Versor(GeometricSpace, mappedBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor MapVersor(CGaFloat64Versor versor)
    {
        return MapVersor(versor.InternalMultivector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFloat64Blade InverseMapBlade(RGaFloat64KVector blade)
    {
        var mappedBlade =
            InternalMultivectorInverse
                .Gp(blade)
                .Gp(InternalMultivector)
                .GetKVectorPart(blade.Grade);

        return new CGaFloat64Blade(GeometricSpace, mappedBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade InverseMapBlade(CGaFloat64Blade blade)
    {
        return InverseMapBlade(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFloat64Versor InverseMapVersor(RGaFloat64Multivector versor)
    {
        var mappedBlade =
            InternalMultivectorInverse
                .Gp(versor)
                .Gp(InternalMultivector);

        return new CGaFloat64Versor(GeometricSpace, mappedBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor InverseMapVersor(CGaFloat64Versor versor)
    {
        return InverseMapVersor(versor.InternalMultivector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX()
    {
        return BasisSpecs.ToLaTeX(InternalMultivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(GaFloat64GeometricSpaceBasisSpecs basisSpecs)
    {
        return basisSpecs.ToLaTeX(InternalMultivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return ToLaTeX();
    }
}