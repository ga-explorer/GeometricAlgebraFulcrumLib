using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;

/// <summary>
/// This class is a CGA Blade that can be used to encode:
/// - Euclidean subspaces (scalars, vectors, 2-blades, etc.)
/// - Euclidean Projective GA flats (points, lines, planes, etc.)
///   (see paper Projective Geometric Algebra as a Subalgebra of Conformal Geometric algebra)
/// - Conformal GA Directions, Tangents, Flats, and Round (see chapter 14 in Geometric Algebra for Computer Science)
/// </summary>
public sealed record XGaConformalBlade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator -(XGaConformalBlade<T> blade)
    {
        return blade.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator +(XGaConformalBlade<T> blade1, XGaKVector<T> blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator +(XGaKVector<T> blade1, XGaConformalBlade<T> blade2)
    {
        return blade2.Add(blade1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator +(XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator -(XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return blade1.Subtract(blade2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator *(int scalar, XGaConformalBlade<T> blade)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator *(float scalar, XGaConformalBlade<T> blade)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator *(double scalar, XGaConformalBlade<T> blade)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator *(T scalar, XGaConformalBlade<T> blade)
    {
        return blade.Times(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator *(Scalar<T> scalar, XGaConformalBlade<T> blade)
    {
        return blade.Times(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator *(IScalar<T> scalar, XGaConformalBlade<T> blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator *(XGaConformalBlade<T> blade, int scalar)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator *(XGaConformalBlade<T> blade, float scalar)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator *(XGaConformalBlade<T> blade, double scalar)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator *(XGaConformalBlade<T> blade, T scalar)
    {
        return blade.Times(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator *(XGaConformalBlade<T> blade, IScalar<T> scalar)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator /(XGaConformalBlade<T> blade, int scalar)
    {
        return blade.Divide(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator /(XGaConformalBlade<T> blade, float scalar)
    {
        return blade.Divide(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator /(XGaConformalBlade<T> blade, double scalar)
    {
        return blade.Divide(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator /(XGaConformalBlade<T> blade, T scalar)
    {
        return blade.Divide(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator /(XGaConformalBlade<T> blade, IScalar<T> scalar)
    {
        return blade.Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator /(int scalar, XGaConformalBlade<T> blade)
    {
        return blade.Inverse().Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator /(float scalar, XGaConformalBlade<T> blade)
    {
        return blade.Inverse().Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator /(double scalar, XGaConformalBlade<T> blade)
    {
        return blade.Inverse().Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator /(IScalar<T> scalar, XGaConformalBlade<T> blade)
    {
        return blade.Inverse().Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> operator /(T scalar, XGaConformalBlade<T> blade)
    {
        return blade.Inverse().Times(scalar);
    }


    public XGaConformalSpace<T> ConformalSpace { get; }

    public XGaGeometrySpaceBasisSpecs<T> BasisSpecs 
        => ConformalSpace.BasisSpecs;

    public XGaKVector<T> InternalKVector { get; }
    
    public XGaScalar<T> InternalScalar
        => InternalKVector.GetScalarPart();
    
    public T InternalScalarValue
        => InternalKVector.GetScalarPart().ScalarValue;

    public XGaVector<T> InternalVector
        => InternalKVector.GetVectorPart();

    public XGaBivector<T> InternalBivector
        => InternalKVector.GetBivectorPart();

    public XGaConformalProcessor<T> ConformalProcessor
        => ConformalSpace.ConformalProcessor;
    
    public IScalarProcessor<T> ScalarProcessor
        => ConformalSpace.ConformalProcessor.ScalarProcessor;

    public int Grade
        => InternalKVector.Grade;
    
    public int VSpaceDimensions
        => ConformalSpace.VSpaceDimensions;

    public Scalar<T> this[int i]
        => InternalKVector[i];
    
    public Scalar<T> this[int i, int j]
        => InternalKVector[i, j];

    public Scalar<T> this[int i, int j, int k]
        => InternalKVector[i, j, k];
    
    public Scalar<T> this[params int[] indexList]
        => InternalKVector[indexList];

    public bool IsScalar
        => InternalKVector.Grade == 0;

    public bool IsVector
        => InternalKVector.Grade == 1;

    public bool IsBivector
        => InternalKVector.Grade == 2;

    public bool IsTrivector
        => InternalKVector.Grade == 3;

    public bool IsPseudoVector
        => InternalKVector.Grade == ConformalSpace.VSpaceDimensions - 1;

    public bool IsPseudoScalar
        => InternalKVector.Grade == ConformalSpace.VSpaceDimensions;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalBlade(XGaConformalSpace<T> conformalSpace, XGaKVector<T> kVector)
    {
        Debug.Assert(
            kVector.Processor.HasSameSignature(conformalSpace.ConformalProcessor) &&
            conformalSpace.IsValidElement(kVector)
        );

        // This is to reduce some numerical errors by removing very small terms
        // relative to the max-magnitude scalar term of the k-vector
        //InternalKVector = kVector.RemoveSmallTerms(); 
        InternalKVector = kVector;
        ConformalSpace = conformalSpace;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out XGaConformalSpace<T> conformalSpace, out XGaKVector<T> kVector)
    {
        conformalSpace = ConformalSpace;
        kVector = InternalKVector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return InternalKVector.IsZero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero()
    {
        return InternalKVector.IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqual(XGaMultivector<T> blade2)
    {
        return InternalKVector.Subtract(blade2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqual(XGaConformalBlade<T> blade2)
    {
        return InternalKVector.Subtract(blade2.InternalKVector).IsNearZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalBlade<T> RemoveEi()
    {
        //var eiIdMask = (1UL << (VSpaceDimensions - 1)) - 1UL;
        var eiIndex = VSpaceDimensions - 1;

        var kVector = BasisSpecs.BasisMapInverse.OmMap(
            BasisSpecs
                .BasisMap
                .OmMap(InternalKVector)
                .MapBasisBlades(id => id.Remove(eiIndex))
                .GetFirstKVectorPart()
        );

        return new XGaConformalBlade<T>(ConformalSpace, kVector);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> DecodeScalar()
    {
        return InternalScalar.Scalar();
    }


    /// <summary>
    /// The Scalar Product of this blade with itself
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> SpSquared()
    {
        return InternalKVector.SpSquared();
    }

    /// <summary>
    /// The CGA Squared Norm of this blade (equal to blade.Sp(blade.Reverse))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        return InternalKVector.NormSquared();
    }

    /// <summary>
    /// The square root of the absolute value of the CGA Squared Norm of this blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return InternalKVector.NormSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Negative()
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Reverse()
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> GradeInvolution()
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.GradeInvolution()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> CliffordConjugate()
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.CliffordConjugate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Inverse()
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Inverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Times(T scalar)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Times(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Times(Scalar<T> scalar)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Times(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Times(IScalar<T> scalar)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Times(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Divide(T scalar)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Divide(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Divide(Scalar<T> scalar)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Divide(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Divide(IScalar<T> scalar)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Divide(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> DivideByNorm()
    {
        var norm = InternalKVector.Norm();

        return norm.IsNearZero()
            ? this
            : Divide(norm.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> SetNorm(T norm)
    {
        Debug.Assert(norm is not null);

        var oldNorm = Norm();

        return oldNorm.IsZero()
            ? this
            : Times(norm / oldNorm);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> SetNorm(Scalar<T> norm)
    {
        var oldNorm = Norm();

        return oldNorm.IsZero()
            ? this
            : Times(norm / oldNorm);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> SetNorm(IScalar<T> norm)
    {
        var oldNorm = Norm();

        return oldNorm.IsZero()
            ? this
            : Times(norm / oldNorm);
    }

    /// <summary>
    /// The EGA normal of this EGA blade (equal to blade.Inverse().Lcp(Ie))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> EGaNormal()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalKVector)
        );

        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Inverse().Lcp(ConformalSpace.Ie.InternalKVector)
        );
    }
    
    /// <summary>
    /// The EGA un-normal of this EGA normal blade (equal to Ie.Rcp(blade.Inverse()))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> EGaUnNormal()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalKVector)
        );

        return new XGaConformalBlade<T>(
            ConformalSpace,
            ConformalSpace.Ie.InternalKVector.Rcp(InternalKVector.Inverse())
        );
    }

    /// <summary>
    /// The EGA dual of this EGA blade (equal to blade.Lcp(Ie.Inverse()))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> EGaDual()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalKVector)
        );

        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Lcp(ConformalSpace.IeInv.InternalKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> EGaDualKVector()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalKVector)
        );

        return InternalKVector.Lcp(ConformalSpace.IeInv.InternalKVector);
    }

    /// <summary>
    /// The EGA un-dual of this EGA blade (equal to blade.Lcp(Ie))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> EGaUnDual()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalKVector)
        );

        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Lcp(ConformalSpace.Ie.InternalKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> EGaUnDualKVector()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalKVector)
        );

        return InternalKVector.Lcp(ConformalSpace.Ie.InternalKVector);
    }
    
    /// <summary>
    /// The Squared Norm of this PGA Blade (equal to blade.Sp(blade.CliffordConjugate()))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> PGaNormSquared()
    {
        Debug.Assert(this.IsPGaBlade());

        return InternalKVector.Sp(
            InternalKVector.CliffordConjugate()
        ).Scalar();
    }

    /// <summary>
    /// The Squared Root of the Absolute Value of the Squared Norm of this PGA Blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> PGaNorm()
    {
        return PGaNormSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> PGaNormalize()
    {
        return Divide(PGaNorm().ScalarValue);
    }

    /// <summary>
    /// The PGA dual of this PGA Blade (equal to #[blade.Op(Ei).Lcp(Ic.Inverse())])
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> PGaDual()
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            PGaDualKVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> PGaDualKVector()
    {
        Debug.Assert(
            ConformalSpace.IsValidPGaElement(InternalKVector)
        );

        return ConformalSpace.MusicalIsomorphism.OmMap(
            InternalKVector.Op(ConformalSpace.EiVector).Lcp(ConformalSpace.IcInvKVector)
            // Also can be mv.Op(Ei).Lcp(Ic)
        );
    }

    /// <summary>
    /// The PGA un-dual of this PGA Blade (equal to #[blade.Op(Ei).Lcp(Ic.Inverse())])
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> PGaUnDual()
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            PGaUnDualKVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> PGaUnDualKVector()
    {
        Debug.Assert(
            ConformalSpace.IsValidPGaElement(InternalKVector)
        );

        return ConformalSpace.MusicalIsomorphism.OmMap(
            InternalKVector.Op(ConformalSpace.EiVector).Lcp(ConformalSpace.IcInvKVector)
            // Also can be mv.Op(Ei).Lcp(Ic)
        );
    }

    /// <summary>
    /// The CGA dual of this Blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> CGaDual()
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Lcp(ConformalSpace.IcInv.InternalKVector)
        );
    }

    /// <summary>
    /// The CGA un-dual of this Blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> CGaUnDual()
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Lcp(ConformalSpace.Ic.InternalKVector)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Add(XGaKVector<T> blade2)
    {
        if (InternalKVector.Grade != blade2.Grade)
            throw new InvalidOperationException();

        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Add(blade2).GetKVectorPart(InternalKVector.Grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Add(XGaConformalBlade<T> blade2)
    {
        if (InternalKVector.Grade != blade2.InternalKVector.Grade)
            throw new InvalidOperationException();

        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Add(blade2.InternalKVector).GetKVectorPart(InternalKVector.Grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Subtract(XGaConformalBlade<T> blade2)
    {
        if (InternalKVector.Grade != blade2.InternalKVector.Grade)
            throw new InvalidOperationException();

        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Subtract(blade2.InternalKVector).GetKVectorPart(InternalKVector.Grade)
        );
    }

    /// <summary>
    /// The scalar product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Sp(XGaConformalBlade<T> blade2)
    {
        return InternalKVector.Sp(blade2.InternalKVector).ScalarValue;
    }

    /// <summary>
    /// The scalar product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Sp(XGaKVector<T> blade2)
    {
        return InternalKVector.Sp(blade2).ScalarValue;
    }

    /// <summary>
    /// The outer product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Op(XGaConformalBlade<T> blade2)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Op(blade2.InternalKVector)
        );
    }

    /// <summary>
    /// The outer product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Op(XGaKVector<T> blade2)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Op(blade2)
        );
    }

    /// <summary>
    /// The left-contraction product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Lcp(XGaConformalBlade<T> blade2)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Lcp(blade2.InternalKVector)
        );
    }

    /// <summary>
    /// The left-contraction product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Lcp(XGaKVector<T> blade2)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Lcp(blade2)
        );
    }

    /// <summary>
    /// The right-contraction product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Rcp(XGaConformalBlade<T> blade2)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Rcp(blade2.InternalKVector)
        );
    }

    /// <summary>
    /// The right-contraction product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Rcp(XGaKVector<T> blade2)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Rcp(blade2)
        );
    }

    /// <summary>
    /// The fat-dot product (PGA inner product) of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Fdp(XGaConformalBlade<T> blade2)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Fdp(blade2.InternalKVector).GetFirstKVectorPart()
        );
    }

    /// <summary>
    /// The fat-dot product (PGA inner product) of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> Fdp(XGaKVector<T> blade2)
    {
        return new XGaConformalBlade<T>(
            ConformalSpace,
            InternalKVector.Fdp(blade2).GetFirstKVectorPart()
        );
    }
    
    /// <summary>
    /// The geometric product of this blade with another
    /// </summary>
    /// <param name="mv2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Gp(XGaConformalBlade<T> mv2)
    {
        return InternalKVector.Gp(mv2.InternalKVector);
    }

    /// <summary>
    /// The geometric product of this blade with a multivector
    /// </summary>
    /// <param name="mv2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Gp(XGaMultivector<T> mv2)
    {
        return InternalKVector.Gp(mv2);
    }
    

    public XGaConformalBlade<T> GetEuclideanMeet(XGaConformalBlade<T> blade2)
    {
        var a = InternalKVector;
        var b = blade2.InternalKVector;

        var meetVectorsList = new List<XGaVector<T>>(
            Math.Min(a.Grade, b.Grade)
        );

        meetVectorsList.AddRange(
            Grade <= blade2.Grade
                ? a.BladeToVectors().Where(vector => vector.Op(b).IsNearZero())
                : b.BladeToVectors().Where(vector => a.Op(vector).IsNearZero())
        );

        return meetVectorsList
            .Op(ConformalProcessor)
            .ToConformalBlade(ConformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> GetEuclideanJoin(XGaConformalBlade<T> blade2)
    {
        return InternalKVector
            .BladeToVectors()
            .Concat(blade2.InternalKVector.BladeToVectors())
            .SpanToBlade(ConformalProcessor)
            .ToConformalBlade(ConformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<XGaConformalBlade<T>> GetEuclideanMeetJoin(XGaConformalBlade<T> blade2)
    {
        var meet = 
            GetEuclideanMeet(blade2);
        
        var join = 
            InternalKVector.Op(
                meet.InternalKVector.Lcp(blade2.InternalKVector)
            ).DivideByENorm().ToConformalBlade(ConformalSpace);

        return new Pair<XGaConformalBlade<T>>(meet, join);
    }

    
    /// <summary>
    /// Get the IPNS distance of this blade with another (equal to -2 * blade1.Sp(blade2)
    /// Each of the two blades must be either a CGA vector or a pseudo-vector
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    public Scalar<T> GetIpnsDistance(XGaConformalBlade<T> blade2)
    {
        Debug.Assert(
            (IsVector || IsPseudoVector) &&
            (blade2.IsVector || blade2.IsPseudoVector)
        );

        var vector1 = ConformalSpace.ZeroVectorBlade.InternalVector;
        var vector2 = ConformalSpace.ZeroVectorBlade.InternalVector;

        if (IsVector) 
            vector1 = InternalVector;

        else if (IsPseudoVector)
            vector1 = InternalKVector.Lcp(ConformalSpace.IcInvKVector).GetVectorPart();

        if (blade2.IsVector) 
            vector2 = blade2.InternalVector;

        else if (blade2.IsPseudoVector)
            vector2 = blade2.InternalKVector.Lcp(ConformalSpace.IcInvKVector).GetVectorPart();

        return -2 * vector1.Sp(vector2).Scalar();
    }
    
    /// <summary>
    /// Set the weight to 1 of a CGA IPNS vector (a IPNS hyper-sphere of a hyper-plane)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public XGaConformalBlade<T> NormalizeIpnsVector()
    {
        Debug.Assert(IsVector);

        var vector = InternalVector;

        var eoScalar = vector[0] + vector[1];

        // IPNS vector encoding a sphere or point
        if (!eoScalar.IsNearZero())
            return new XGaConformalBlade<T>(
                ConformalSpace, 
                (vector / eoScalar)
            );

        // IPNS vector encoding a hyper-plane
        var normal = vector.GetVectorPart(index => index >= 2);
        var normalNorm = normal.Norm();

        if (normalNorm.IsNearZero())
            throw new InvalidOperationException();

        var normalNormInv = 1d / normalNorm;
        var distance = 0.5 * (vector[0] - vector[1]) * normalNormInv;

        return new XGaConformalBlade<T>(
            ConformalSpace,
            normal.Times(normalNormInv) + distance * ConformalSpace.EiVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX()
    {
        return BasisSpecs.ToLaTeX(InternalKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(XGaGeometrySpaceBasisSpecs<T> basisSpecs)
    {
        return basisSpecs.ToLaTeX(InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return ToLaTeX();
    }
}