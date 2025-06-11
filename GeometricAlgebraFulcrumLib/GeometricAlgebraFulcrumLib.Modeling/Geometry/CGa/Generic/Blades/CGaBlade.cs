using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

/// <summary>
/// This class is a CGA Blade that can be used to encode:
/// - Euclidean subspaces (scalars, vectors, 2-blades, etc.)
/// - Euclidean Projective GA flats (points, lines, planes, etc.)
///   (see paper Projective Geometric Algebra as a Subalgebra of Conformal Geometric algebra)
/// - Conformal GA Directions, Tangents, Flats, and Round (see chapter 14 in Geometric Algebra for Computer Science)
/// </summary>
public sealed record CGaBlade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator -(CGaBlade<T> blade)
    {
        return blade.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator +(CGaBlade<T> blade1, XGaKVector<T> blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator +(XGaKVector<T> blade1, CGaBlade<T> blade2)
    {
        return blade2.Add(blade1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator +(CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator -(CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return blade1.Subtract(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator *(int scalar, CGaBlade<T> blade)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator *(float scalar, CGaBlade<T> blade)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator *(double scalar, CGaBlade<T> blade)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator *(T scalar, CGaBlade<T> blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator *(Scalar<T> scalar, CGaBlade<T> blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator *(IScalar<T> scalar, CGaBlade<T> blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator *(CGaBlade<T> blade, int scalar)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator *(CGaBlade<T> blade, float scalar)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator *(CGaBlade<T> blade, double scalar)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator *(CGaBlade<T> blade, T scalar)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator *(CGaBlade<T> blade, IScalar<T> scalar)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator /(CGaBlade<T> blade, int scalar)
    {
        return blade.Divide(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator /(CGaBlade<T> blade, float scalar)
    {
        return blade.Divide(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator /(CGaBlade<T> blade, double scalar)
    {
        return blade.Divide(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator /(CGaBlade<T> blade, T scalar)
    {
        return blade.Divide(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator /(CGaBlade<T> blade, IScalar<T> scalar)
    {
        return blade.Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator /(int scalar, CGaBlade<T> blade)
    {
        return blade.Inverse().Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator /(float scalar, CGaBlade<T> blade)
    {
        return blade.Inverse().Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator /(double scalar, CGaBlade<T> blade)
    {
        return blade.Inverse().Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator /(IScalar<T> scalar, CGaBlade<T> blade)
    {
        return blade.Inverse().Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> operator /(T scalar, CGaBlade<T> blade)
    {
        return blade.Inverse().Times(scalar);
    }


    public CGaGeometricSpace<T> GeometricSpace { get; }

    public GaGeometricSpaceBasisSpecs<T> BasisSpecs
        => GeometricSpace.BasisSpecs;

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
        => GeometricSpace.ConformalProcessor;

    public IScalarProcessor<T> ScalarProcessor
        => GeometricSpace.ConformalProcessor.ScalarProcessor;

    public int Grade
        => InternalKVector.Grade;

    public int VSpaceDimensions
        => GeometricSpace.VSpaceDimensions;

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
        => InternalKVector.Grade == GeometricSpace.VSpaceDimensions - 1;

    public bool IsPseudoScalar
        => InternalKVector.Grade == GeometricSpace.VSpaceDimensions;


    private CGaBladeDecoder<T>? _decoder;
    public CGaBladeDecoder<T> Decode 
        => _decoder ??= new CGaBladeDecoder<T>(this);

    public CGaVGaDirectionBladeDecoder<T> DecodeVGaDirection 
        => Decode.VGaDirection;
    
    public CGaPGaFlatBladeDecoder<T> DecodePGaFlat
        => Decode.PGaFlat;
    
    public CGaIpnsDirectionBladeDecoder<T> DecodeIpnsDirection 
        => Decode.IpnsDirection;
    
    public CGaIpnsTangentBladeDecoder<T> DecodeIpnsTangent 
        => Decode.IpnsTangent;
    
    public CGaIpnsFlatBladeDecoder<T> DecodeIpnsFlat 
        => Decode.IpnsFlat;
    
    public CGaIpnsRoundBladeDecoder<T> DecodeIpnsRound 
        => Decode.IpnsRound;
    
    public CGaOpnsDirectionBladeDecoder<T> DecodeOpnsDirection 
        => Decode.OpnsDirection;
    
    public CGaOpnsTangentBladeDecoder<T> DecodeOpnsTangent 
        => Decode.OpnsTangent;
    
    public CGaOpnsFlatBladeDecoder<T> DecodeOpnsFlat 
        => Decode.OpnsFlat;
    
    public CGaOpnsRoundBladeDecoder<T> DecodeOpnsRound 
        => Decode.OpnsRound;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaBlade(CGaGeometricSpace<T> cgaGeometricSpace, XGaKVector<T> kVector)
    {
        Debug.Assert(
            kVector.Processor.HasSameSignature(cgaGeometricSpace.ConformalProcessor) &&
            cgaGeometricSpace.IsValidElement(kVector)
        );

        // This is to reduce some numerical errors by removing very small terms
        // relative to the max-magnitude scalar term of the k-vector
        //InternalKVector = kVector.RemoveSmallTerms(); 
        InternalKVector = kVector;
        GeometricSpace = cgaGeometricSpace;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out CGaGeometricSpace<T> cgaGeometricSpace, out XGaKVector<T> kVector)
    {
        cgaGeometricSpace = GeometricSpace;
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
    public bool IsNearEqual(CGaBlade<T> blade2)
    {
        return InternalKVector.Subtract(blade2.InternalKVector).IsNearZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> GetVGaPartAsXGaKVector(bool removeEi)
    {
        var kVector1 = removeEi 
            ? GeometricSpace.RemoveEi(InternalKVector) 
            : InternalKVector;

        var termList =
            kVector1.IdScalarPairs.Where(
                term =>
                    !term.Key.SetContains(0) && 
                    !term.Key.SetContains(1)
            );

        return ConformalProcessor
            .CreateKVectorComposer(kVector1.Grade)
            .SetTerms(termList)
            .GetKVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> GetVGaPart(bool removeEi)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            GetVGaPartAsXGaKVector(removeEi)
        );
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
    public CGaBlade<T> Negative()
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Reverse()
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> GradeInvolution()
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.GradeInvolution()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> CliffordConjugate()
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.CliffordConjugate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Inverse()
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Inverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Times(T scalar)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Times(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Times(Scalar<T> scalar)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Times(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Times(IScalar<T> scalar)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Times(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Divide(T scalar)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Divide(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Divide(Scalar<T> scalar)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Divide(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Divide(IScalar<T> scalar)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Divide(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> DivideByNorm()
    {
        var norm = InternalKVector.Norm();

        return norm.IsNearZero()
            ? this
            : Divide(norm.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> SetNorm(T norm)
    {
        Debug.Assert(norm is not null);

        var oldNorm = Norm();

        return oldNorm.IsZero()
            ? this
            : Times(norm / oldNorm);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> SetNorm(Scalar<T> norm)
    {
        var oldNorm = Norm();

        return oldNorm.IsZero()
            ? this
            : Times(norm / oldNorm);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> SetNorm(IScalar<T> norm)
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
    public CGaBlade<T> VGaNormal()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalKVector)
        );

        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Inverse().Lcp(GeometricSpace.Ie.InternalKVector)
        );
    }

    /// <summary>
    /// The EGA un-normal of this EGA normal blade (equal to Ie.Rcp(blade.Inverse()))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaUnNormal()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalKVector)
        );

        return new CGaBlade<T>(
            GeometricSpace,
            GeometricSpace.Ie.InternalKVector.Rcp(InternalKVector.Inverse())
        );
    }

    /// <summary>
    /// The EGA dual of this EGA blade (equal to blade.Lcp(Ie.Inverse()))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaDual()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalKVector)
        );

        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Lcp(GeometricSpace.IeInv.InternalKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> VGaDualKVector()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalKVector)
        );

        return InternalKVector.Lcp(GeometricSpace.IeInv.InternalKVector);
    }

    /// <summary>
    /// The EGA un-dual of this EGA blade (equal to blade.Lcp(Ie))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaUnDual()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalKVector)
        );

        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Lcp(GeometricSpace.Ie.InternalKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> VGaUnDualKVector()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalKVector)
        );

        return InternalKVector.Lcp(GeometricSpace.Ie.InternalKVector);
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
    public CGaBlade<T> PGaNormalize()
    {
        return Divide(PGaNorm().ScalarValue);
    }

    /// <summary>
    /// The PGA dual of this PGA Blade (equal to #[blade.Op(Ei).Lcp(Ic.Inverse())])
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PGaDual()
    {
        return new CGaBlade<T>(
            GeometricSpace,
            PGaDualKVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> PGaDualKVector()
    {
        Debug.Assert(
            GeometricSpace.IsValidPGaElement(InternalKVector)
        );

        return GeometricSpace.MusicalIsomorphism.OmMap(
            InternalKVector.Op(GeometricSpace.EiVector).Lcp(GeometricSpace.IcInvKVector)
        // Also can be mv.Op(Ei).Lcp(Ic)
        );
    }

    /// <summary>
    /// The PGA un-dual of this PGA Blade (equal to #[blade.Op(Ei).Lcp(Ic.Inverse())])
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PGaUnDual()
    {
        return new CGaBlade<T>(
            GeometricSpace,
            PGaUnDualKVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> PGaUnDualKVector()
    {
        Debug.Assert(
            GeometricSpace.IsValidPGaElement(InternalKVector)
        );

        return GeometricSpace.MusicalIsomorphism.OmMap(
            InternalKVector.Op(GeometricSpace.EiVector).Lcp(GeometricSpace.IcInvKVector)
        // Also can be mv.Op(Ei).Lcp(Ic)
        );
    }

    /// <summary>
    /// The CGA dual of this Blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> CGaDual()
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Lcp(GeometricSpace.IcInv.InternalKVector)
        );
    }

    /// <summary>
    /// The CGA un-dual of this Blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> CGaUnDual()
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Lcp(GeometricSpace.Ic.InternalKVector)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Add(XGaKVector<T> blade2)
    {
        if (InternalKVector.Grade != blade2.Grade)
            throw new InvalidOperationException();

        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Add(blade2).GetKVectorPart(InternalKVector.Grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Add(CGaBlade<T> blade2)
    {
        if (InternalKVector.Grade != blade2.InternalKVector.Grade)
            throw new InvalidOperationException();

        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Add(blade2.InternalKVector).GetKVectorPart(InternalKVector.Grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Subtract(CGaBlade<T> blade2)
    {
        if (InternalKVector.Grade != blade2.InternalKVector.Grade)
            throw new InvalidOperationException();

        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Subtract(blade2.InternalKVector).GetKVectorPart(InternalKVector.Grade)
        );
    }

    /// <summary>
    /// The scalar product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Sp(CGaBlade<T> blade2)
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
    public CGaBlade<T> Op(CGaBlade<T> blade2)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Op(blade2.InternalKVector)
        );
    }

    /// <summary>
    /// The outer product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Op(XGaKVector<T> blade2)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Op(blade2)
        );
    }

    /// <summary>
    /// The left-contraction product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Lcp(CGaBlade<T> blade2)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Lcp(blade2.InternalKVector)
        );
    }

    /// <summary>
    /// The left-contraction product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Lcp(XGaKVector<T> blade2)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Lcp(blade2)
        );
    }

    /// <summary>
    /// The right-contraction product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Rcp(CGaBlade<T> blade2)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Rcp(blade2.InternalKVector)
        );
    }

    /// <summary>
    /// The right-contraction product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Rcp(XGaKVector<T> blade2)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Rcp(blade2)
        );
    }

    /// <summary>
    /// The fat-dot product (PGA inner product) of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Fdp(CGaBlade<T> blade2)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Fdp(blade2.InternalKVector).GetFirstKVectorPart()
        );
    }

    /// <summary>
    /// The fat-dot product (PGA inner product) of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Fdp(XGaKVector<T> blade2)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            InternalKVector.Fdp(blade2).GetFirstKVectorPart()
        );
    }

    /// <summary>
    /// The geometric product of this blade with another
    /// </summary>
    /// <param name="mv2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Gp(CGaBlade<T> mv2)
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


    public CGaBlade<T> GetEuclideanMeet(CGaBlade<T> blade2)
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
            .ToConformalBlade(GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> GetEuclideanJoin(CGaBlade<T> blade2)
    {
        return InternalKVector
            .BladeToVectors()
            .Concat(blade2.InternalKVector.BladeToVectors())
            .SpanToBlade(ConformalProcessor)
            .ToConformalBlade(GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<CGaBlade<T>> GetEuclideanMeetJoin(CGaBlade<T> blade2)
    {
        var meet =
            GetEuclideanMeet(blade2);

        var join =
            InternalKVector.Op(
                meet.InternalKVector.Lcp(blade2.InternalKVector)
            ).DivideByENorm().ToConformalBlade(GeometricSpace);

        return new Pair<CGaBlade<T>>(meet, join);
    }


    /// <summary>
    /// Get the IPNS distance of this blade with another (equal to -2 * blade1.Sp(blade2)
    /// Each of the two blades must be either a CGA vector or a pseudo-vector
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    public Scalar<T> GetIpnsDistance(CGaBlade<T> blade2)
    {
        Debug.Assert(
            (IsVector || IsPseudoVector) &&
            (blade2.IsVector || blade2.IsPseudoVector)
        );

        var vector1 = GeometricSpace.ZeroVectorBlade.InternalVector;
        var vector2 = GeometricSpace.ZeroVectorBlade.InternalVector;

        if (IsVector)
            vector1 = InternalVector;

        else if (IsPseudoVector)
            vector1 = InternalKVector.Lcp(GeometricSpace.IcInvKVector).GetVectorPart();

        if (blade2.IsVector)
            vector2 = blade2.InternalVector;

        else if (blade2.IsPseudoVector)
            vector2 = blade2.InternalKVector.Lcp(GeometricSpace.IcInvKVector).GetVectorPart();

        return -2 * vector1.Sp(vector2).Scalar();
    }

    /// <summary>
    /// Set the weight to 1 of a CGA IPNS vector (a IPNS hyper-sphere of a hyper-plane)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public CGaBlade<T> NormalizeIpnsVector()
    {
        Debug.Assert(IsVector);

        var vector = InternalVector;

        var eoScalar = vector[0] + vector[1];

        // IPNS vector encoding a sphere or point
        if (!eoScalar.IsNearZero())
            return new CGaBlade<T>(
                GeometricSpace,
                vector / eoScalar
            );

        // IPNS vector encoding a hyper-plane
        var normal = vector.GetVectorPart(index => index >= 2);
        var normalNorm = normal.Norm();

        if (normalNorm.IsNearZero())
            throw new InvalidOperationException();

        var normalNormInv = 1d / normalNorm;
        var distance = 0.5 * (vector[0] - vector[1]) * normalNormInv;

        return new CGaBlade<T>(
            GeometricSpace,
            normal.Times(normalNormInv) + distance * GeometricSpace.EiVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX()
    {
        return BasisSpecs.ToLaTeX(InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(GaGeometricSpaceBasisSpecs<T> basisSpecs)
    {
        return basisSpecs.ToLaTeX(InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return ToLaTeX();
    }
}