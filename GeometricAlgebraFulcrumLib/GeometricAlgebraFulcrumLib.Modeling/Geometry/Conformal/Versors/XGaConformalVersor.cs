using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Versors;

public sealed record XGaConformalVersor<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> operator -(XGaConformalVersor<T> blade)
    {
        return blade.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> operator +(XGaConformalVersor<T> blade1, XGaConformalVersor<T> blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> operator -(XGaConformalVersor<T> blade1, XGaConformalVersor<T> blade2)
    {
        return blade1.Subtract(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> operator *(T scalar, XGaConformalVersor<T> blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> operator *(XGaConformalVersor<T> blade, T scalar)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> operator /(XGaConformalVersor<T> blade, T scalar)
    {
        return blade.Divide(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> operator /(T scalar, XGaConformalVersor<T> blade)
    {
        return blade.Inverse().Times(scalar);
    }


    public XGaConformalSpace<T> ConformalSpace { get; }

    public XGaGeometrySpaceBasisSpecs<T> BasisSpecs 
        => ConformalSpace.BasisSpecs;

    public XGaMultivector<T> InternalMultivector { get; }

    public XGaMultivector<T> InternalMultivectorInverse { get; }
    
    public XGaConformalProcessor<T> ConformalProcessor
        => ConformalSpace.ConformalProcessor;
    
    public int VSpaceDimensions
        => ConformalSpace.VSpaceDimensions;

    public Scalar<T> this[int i]
        => InternalMultivector[i];
    
    public Scalar<T> this[int i, int j]
        => InternalMultivector[i, j];

    public Scalar<T> this[int i, int j, int k]
        => InternalMultivector[i, j, k];
    
    public Scalar<T> this[params int[] indexList]
        => InternalMultivector[indexList];
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalVersor(XGaConformalSpace<T> conformalSpace, XGaMultivector<T> multivector)
        : this(conformalSpace, multivector, multivector.Inverse())
    {
        //Debug.Assert(
        //    multivector.Processor.HasSameSignature(conformalSpace.ConformalProcessor) &&
        //    conformalSpace.IsValidElement(multivector)
        //);

        //InternalMultivector = multivector;
        //InternalMultivectorInverse = multivector.Inverse();
        //ConformalSpace = conformalSpace;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalVersor(XGaConformalSpace<T> conformalSpace, XGaMultivector<T> multivector, XGaMultivector<T> multivectorInverse)
    {
        //Debug.Assert(
        //    multivector.Processor.HasSameSignature(conformalSpace.ConformalProcessor) &&
        //    multivectorInverse.Processor.HasSameSignature(conformalSpace.ConformalProcessor) &&
        //    multivector.Gp(multivectorInverse).Subtract(conformalSpace.ConformalProcessor.ScalarOne).IsNearZero() &&
        //    conformalSpace.IsValidElement(multivector)
        //);

        Debug.Assert(
            multivector.Processor.HasSameSignature(conformalSpace.ConformalProcessor) &&
            multivectorInverse.Processor.HasSameSignature(conformalSpace.ConformalProcessor) &&
            //multivector.Gp(multivectorInverse).Subtract(conformalSpace.ConformalProcessor.ScalarOne).IsNearZero() &&
            conformalSpace.IsValidElement(multivector)
        );

        InternalMultivector = multivector;
        InternalMultivectorInverse = multivectorInverse;
        ConformalSpace = conformalSpace;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out XGaConformalSpace<T> conformalSpace, out XGaMultivector<T> multivector, out XGaMultivector<T> multivectorInverse)
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
    public XGaConformalVersor<T> Negative()
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Reverse()
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> GradeInvolution()
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.GradeInvolution()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> CliffordConjugate()
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.CliffordConjugate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Inverse()
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Inverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Times(T scalar)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Times(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Divide(T scalar)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Divide(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> DivideByNorm()
    {
        var norm = InternalMultivector.Norm();

        return norm.IsNearZero()
            ? this
            : Divide(norm.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> EGaDual()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalMultivector)
        );

        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Lcp(ConformalSpace.IeInvKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> EGaDualMultivector()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalMultivector)
        );

        return InternalMultivector.Lcp(ConformalSpace.IeInvKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> EGaUnDual()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalMultivector)
        );

        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Lcp(ConformalSpace.IeKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> EGaUnDualMultivector()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalMultivector)
        );

        return InternalMultivector.Lcp(ConformalSpace.IeKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> PGaDual()
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            PGaDualMultivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> PGaDualMultivector()
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
    public XGaConformalVersor<T> PGaUnDual()
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            PGaUnDualMultivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> PGaUnDualMultivector()
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
    public XGaConformalVersor<T> CGaDual()
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Lcp(ConformalSpace.IcInvKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> CGaUnDual()
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Lcp(ConformalSpace.IcKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Add(XGaConformalVersor<T> blade2)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Add(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Subtract(XGaConformalVersor<T> blade2)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Subtract(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Sp(XGaConformalVersor<T> blade2)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Sp(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Sp(XGaKVector<T> blade2)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Sp(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Op(XGaConformalVersor<T> blade2)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Op(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Op(XGaKVector<T> blade2)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Op(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Lcp(XGaConformalVersor<T> blade2)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Lcp(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Lcp(XGaKVector<T> blade2)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Lcp(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Rcp(XGaConformalVersor<T> blade2)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Rcp(blade2.InternalMultivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Rcp(XGaKVector<T> blade2)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Rcp(blade2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Fdp(XGaConformalVersor<T> blade2)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Fdp(blade2.InternalMultivector).GetFirstKVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> Fdp(XGaKVector<T> blade2)
    {
        return new XGaConformalVersor<T>(
            ConformalSpace,
            InternalMultivector.Fdp(blade2).GetFirstKVectorPart()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Gp(XGaConformalVersor<T> mv2)
    {
        return InternalMultivector.Gp(mv2.InternalMultivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Gp(XGaMultivector<T> mv2)
    {
        return InternalMultivector.Gp(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> ScaleBy(Scalar<T> scalingFactor)
    {
        Debug.Assert(
            scalingFactor.IsValid() &&
            scalingFactor > 0
        );

        var g = scalingFactor.LogE() / 2;
        var s0 = g.Cosh();
        var s2 = g.Sinh() * ConformalSpace.EoiBivector;

        var kVector =
            (s0 + s2).Gp(InternalMultivector).Gp(s0 - s2);

        return new XGaConformalVersor<T>(ConformalSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalBlade<T> MapBlade(XGaKVector<T> blade)
    {
        var mappedBlade =
            InternalMultivector
                .Gp(blade)
                .Gp(InternalMultivectorInverse)
                .GetKVectorPart(blade.Grade);
        
        return new XGaConformalBlade<T>(ConformalSpace, mappedBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> MapBlade(XGaConformalBlade<T> blade)
    {
        return MapBlade(blade.InternalKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalVersor<T> MapVersor(XGaMultivector<T> versor)
    {
        var mappedBlade =
            InternalMultivector
                .Gp(versor)
                .Gp(InternalMultivectorInverse);
        
        return new XGaConformalVersor<T>(ConformalSpace, mappedBlade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> MapVersor(XGaConformalVersor<T> versor)
    {
        return MapVersor(versor.InternalMultivector);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalBlade<T> InverseMapBlade(XGaKVector<T> blade)
    {
        var mappedBlade =
            InternalMultivectorInverse
                .Gp(blade)
                .Gp(InternalMultivector)
                .GetKVectorPart(blade.Grade);
        
        return new XGaConformalBlade<T>(ConformalSpace, mappedBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> InverseMapBlade(XGaConformalBlade<T> blade)
    {
        return InverseMapBlade(blade.InternalKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalVersor<T> InverseMapVersor(XGaMultivector<T> versor)
    {
        var mappedBlade =
            InternalMultivectorInverse
                .Gp(versor)
                .Gp(InternalMultivector);
        
        return new XGaConformalVersor<T>(ConformalSpace, mappedBlade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalVersor<T> InverseMapVersor(XGaConformalVersor<T> versor)
    {
        return InverseMapVersor(versor.InternalMultivector);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX()
    {
        return BasisSpecs.ToLaTeX(InternalMultivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(XGaGeometrySpaceBasisSpecs<T> basisSpecs)
    {
        return basisSpecs.ToLaTeX(InternalMultivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return ToLaTeX();
    }
}