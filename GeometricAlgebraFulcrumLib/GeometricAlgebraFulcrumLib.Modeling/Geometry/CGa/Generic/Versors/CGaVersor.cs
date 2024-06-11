using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Versors;

public sealed record CGaVersor<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> operator -(CGaVersor<T> blade)
    {
        return blade.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> operator +(CGaVersor<T> blade1, CGaVersor<T> blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> operator -(CGaVersor<T> blade1, CGaVersor<T> blade2)
    {
        return blade1.Subtract(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> operator *(T scalar, CGaVersor<T> blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> operator *(CGaVersor<T> blade, T scalar)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> operator /(CGaVersor<T> blade, T scalar)
    {
        return blade.Divide(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> operator /(T scalar, CGaVersor<T> blade)
    {
        return blade.Inverse().Times(scalar);
    }


    public CGaGeometricSpace<T> GeometricSpace { get; }

    public GaGeometricSpaceBasisSpecs<T> BasisSpecs
        => GeometricSpace.BasisSpecs;

    public XGaMultivector<T> InternalMultivector { get; }

    public XGaMultivector<T> InternalMultivectorInverse { get; }

    public XGaConformalProcessor<T> ConformalProcessor
        => GeometricSpace.ConformalProcessor;

    public int VSpaceDimensions
        => GeometricSpace.VSpaceDimensions;

    public Scalar<T> this[int i]
        => InternalMultivector[i];

    public Scalar<T> this[int i, int j]
        => InternalMultivector[i, j];

    public Scalar<T> this[int i, int j, int k]
        => InternalMultivector[i, j, k];

    public Scalar<T> this[params int[] indexList]
        => InternalMultivector[indexList];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaVersor(CGaGeometricSpace<T> cgaGeometricSpace, XGaMultivector<T> multivector)
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
    internal CGaVersor(CGaGeometricSpace<T> cgaGeometricSpace, XGaMultivector<T> multivector, XGaMultivector<T> multivectorInverse)
    {
        //Debug.Assert(
        //    multivector.Processor.HasSameSignature(cgaGeometricSpace.ConformalProcessor) &&
        //    multivectorInverse.Processor.HasSameSignature(cgaGeometricSpace.ConformalProcessor) &&
        //    multivector.Gp(multivectorInverse).Subtract(cgaGeometricSpace.ConformalProcessor.ScalarOne).IsNearZero() &&
        //    cgaGeometricSpace.IsValidElement(multivector)
        //);

        Debug.Assert(
            multivector.Processor.HasSameSignature(cgaGeometricSpace.ConformalProcessor) &&
            multivectorInverse.Processor.HasSameSignature(cgaGeometricSpace.ConformalProcessor) &&
            //multivector.Gp(multivectorInverse).Subtract(cgaGeometricSpace.ConformalProcessor.ScalarOne).IsNearZero() &&
            cgaGeometricSpace.IsValidElement(multivector)
        );

        InternalMultivector = multivector;
        InternalMultivectorInverse = multivectorInverse;
        GeometricSpace = cgaGeometricSpace;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out CGaGeometricSpace<T> cgaGeometricSpace, out XGaMultivector<T> multivector, out XGaMultivector<T> multivectorInverse)
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
    public bool IsNearZero()
    {
        return InternalMultivector.IsNearZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> SpSquared()
    {
        return InternalMultivector.SpSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        return InternalMultivector.NormSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return InternalMultivector.NormSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Negative()
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Reverse()
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> GradeInvolution()
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.GradeInvolution()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> CliffordConjugate()
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.CliffordConjugate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Inverse()
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Inverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Times(T scalar)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Times(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Divide(T scalar)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Divide(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> DivideByNorm()
    {
        var norm = InternalMultivector.Norm();

        return norm.IsNearZero()
            ? this
            : Divide(norm.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> VGaDual()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalMultivector)
        );

        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Lcp(GeometricSpace.IeInvKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> VGaDualMultivector()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalMultivector)
        );

        return InternalMultivector.Lcp(GeometricSpace.IeInvKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> VGaUnDual()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalMultivector)
        );

        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Lcp(GeometricSpace.IeKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> VGaUnDualMultivector()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalMultivector)
        );

        return InternalMultivector.Lcp(GeometricSpace.IeKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> PGaDual()
    {
        return new CGaVersor<T>(
            GeometricSpace,
            PGaDualMultivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> PGaDualMultivector()
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
    public CGaVersor<T> PGaUnDual()
    {
        return new CGaVersor<T>(
            GeometricSpace,
            PGaUnDualMultivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> PGaUnDualMultivector()
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
    public CGaVersor<T> CGaDual()
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Lcp(GeometricSpace.IcInvKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> CGaUnDual()
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Lcp(GeometricSpace.IcKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Add(CGaVersor<T> blade2)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Add(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Subtract(CGaVersor<T> blade2)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Subtract(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Sp(CGaVersor<T> blade2)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Sp(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Sp(XGaKVector<T> blade2)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Sp(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Op(CGaVersor<T> blade2)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Op(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Op(XGaKVector<T> blade2)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Op(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Lcp(CGaVersor<T> blade2)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Lcp(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Lcp(XGaKVector<T> blade2)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Lcp(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Rcp(CGaVersor<T> blade2)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Rcp(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Rcp(XGaKVector<T> blade2)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Rcp(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Fdp(CGaVersor<T> blade2)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Fdp(blade2.InternalMultivector).GetFirstKVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Fdp(XGaKVector<T> blade2)
    {
        return new CGaVersor<T>(
            GeometricSpace,
            InternalMultivector.Fdp(blade2).GetFirstKVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Gp(CGaVersor<T> mv2)
    {
        return InternalMultivector.Gp(mv2.InternalMultivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Gp(XGaMultivector<T> mv2)
    {
        return InternalMultivector.Gp(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> ScaleBy(Scalar<T> scalingFactor)
    {
        Debug.Assert(
            scalingFactor.IsValid() &&
            scalingFactor > 0
        );

        var g = scalingFactor.LogE() / 2;
        var s0 = g.Cosh();
        var s2 = g.Sinh() * GeometricSpace.EoiBivector;

        var kVector =
            (s0 + s2).Gp(InternalMultivector).Gp(s0 - s2);

        return new CGaVersor<T>(GeometricSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaBlade<T> MapBlade(XGaKVector<T> blade)
    {
        var mappedBlade =
            InternalMultivector
                .Gp(blade)
                .Gp(InternalMultivectorInverse)
                .GetKVectorPart(blade.Grade);

        return new CGaBlade<T>(GeometricSpace, mappedBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> MapBlade(CGaBlade<T> blade)
    {
        return MapBlade(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaVersor<T> MapVersor(XGaMultivector<T> versor)
    {
        var mappedBlade =
            InternalMultivector
                .Gp(versor)
                .Gp(InternalMultivectorInverse);

        return new CGaVersor<T>(GeometricSpace, mappedBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> MapVersor(CGaVersor<T> versor)
    {
        return MapVersor(versor.InternalMultivector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaBlade<T> InverseMapBlade(XGaKVector<T> blade)
    {
        var mappedBlade =
            InternalMultivectorInverse
                .Gp(blade)
                .Gp(InternalMultivector)
                .GetKVectorPart(blade.Grade);

        return new CGaBlade<T>(GeometricSpace, mappedBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> InverseMapBlade(CGaBlade<T> blade)
    {
        return InverseMapBlade(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaVersor<T> InverseMapVersor(XGaMultivector<T> versor)
    {
        var mappedBlade =
            InternalMultivectorInverse
                .Gp(versor)
                .Gp(InternalMultivector);

        return new CGaVersor<T>(GeometricSpace, mappedBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> InverseMapVersor(CGaVersor<T> versor)
    {
        return InverseMapVersor(versor.InternalMultivector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX()
    {
        return BasisSpecs.ToLaTeX(InternalMultivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(GaGeometricSpaceBasisSpecs<T> basisSpecs)
    {
        return basisSpecs.ToLaTeX(InternalMultivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return ToLaTeX();
    }
}