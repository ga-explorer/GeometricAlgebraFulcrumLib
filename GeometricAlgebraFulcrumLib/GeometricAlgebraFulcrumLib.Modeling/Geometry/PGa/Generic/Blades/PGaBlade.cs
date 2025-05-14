using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;

/// <summary>
/// This class is a PGA Blade that can be used to encode:
/// - Euclidean subspaces (scalars, vectors, 2-blades, etc.)
/// - Euclidean Projective GA flats (points, lines, planes, etc.)
/// - Projective GA Directions and Flats
/// </summary>
public sealed record PGaBlade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator -(PGaBlade<T> blade)
    {
        return blade.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator +(PGaBlade<T> blade1, XGaKVector<T> blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator +(XGaKVector<T> blade1, PGaBlade<T> blade2)
    {
        return blade2.Add(blade1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator +(PGaBlade<T> blade1, PGaBlade<T> blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator -(PGaBlade<T> blade1, XGaKVector<T> blade2)
    {
        return blade1.Subtract(new PGaBlade<T>(blade1.GeometricSpace, blade2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator -(XGaKVector<T> blade1, PGaBlade<T> blade2)
    {
        return new PGaBlade<T>(blade2.GeometricSpace, blade1).Subtract(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator -(PGaBlade<T> blade1, PGaBlade<T> blade2)
    {
        return blade1.Subtract(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator *(int scalar, PGaBlade<T> blade)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator *(float scalar, PGaBlade<T> blade)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator *(double scalar, PGaBlade<T> blade)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator *(T scalar, PGaBlade<T> blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator *(Scalar<T> scalar, PGaBlade<T> blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator *(IScalar<T> scalar, PGaBlade<T> blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator *(PGaBlade<T> blade, int scalar)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator *(PGaBlade<T> blade, float scalar)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator *(PGaBlade<T> blade, double scalar)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator *(PGaBlade<T> blade, T scalar)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator *(PGaBlade<T> blade, IScalar<T> scalar)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator /(PGaBlade<T> blade, int scalar)
    {
        return blade.Divide(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator /(PGaBlade<T> blade, float scalar)
    {
        return blade.Divide(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator /(PGaBlade<T> blade, double scalar)
    {
        return blade.Divide(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator /(PGaBlade<T> blade, T scalar)
    {
        return blade.Divide(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator /(PGaBlade<T> blade, IScalar<T> scalar)
    {
        return blade.Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator /(int scalar, PGaBlade<T> blade)
    {
        return blade.Inverse().Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator /(float scalar, PGaBlade<T> blade)
    {
        return blade.Inverse().Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator /(double scalar, PGaBlade<T> blade)
    {
        return blade.Inverse().Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator /(IScalar<T> scalar, PGaBlade<T> blade)
    {
        return blade.Inverse().Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> operator /(T scalar, PGaBlade<T> blade)
    {
        return blade.Inverse().Times(scalar);
    }


    public PGaGeometricSpace<T> GeometricSpace { get; }

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

    public XGaProjectiveProcessor<T> ProjectiveProcessor
        => GeometricSpace.ProjectiveProcessor;

    public IScalarProcessor<T> ScalarProcessor
        => GeometricSpace.ProjectiveProcessor.ScalarProcessor;

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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal PGaBlade(PGaGeometricSpace<T> pgaGeometricSpace, XGaKVector<T> kVector)
    {
        Debug.Assert(
            kVector.Processor.HasSameSignature(pgaGeometricSpace.ProjectiveProcessor) &&
            pgaGeometricSpace.IsValidElement(kVector)
        );

        // This is to reduce some numerical errors by removing very small terms
        // relative to the max-magnitude scalar term of the k-vector
        //InternalKVector = kVector.RemoveSmallTerms(); 
        InternalKVector = kVector;
        GeometricSpace = pgaGeometricSpace;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out PGaGeometricSpace<T> pgaGeometricSpace, out XGaKVector<T> kVector)
    {
        pgaGeometricSpace = GeometricSpace;
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

    /// <summary>
    /// The square of a flat ideal element is zero
    /// See section 7.1 in SIGGRAPH 2019 Course notes "Geometric Algebra for Computer Graphics"
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdealFlat()
    {
        return InternalKVector.SpSquared().IsZero();
    }

    /// <summary>
    /// The square of a flat Euclidean element is non-zero
    /// See section 7.1 in SIGGRAPH 2019 Course notes "Geometric Algebra for Computer Graphics"
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEuclideanFlat()
    {
        return InternalKVector.SpSquared().IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsPGaPoint()
    {
        return GeometricSpace.IsValidPGaPoint(InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsPGaIdealPoint()
    {
        return GeometricSpace.IsValidPGaVector(InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsHGaPoint()
    {
        return GeometricSpace.IsValidHGaPoint(InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsHGaIdealPoint()
    {
        return GeometricSpace.IsValidHGaVector(InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqual(XGaMultivector<T> blade2)
    {
        return InternalKVector.Subtract(blade2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqual(PGaBlade<T> blade2)
    {
        return InternalKVector.Subtract(blade2.InternalKVector).IsNearZero();
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
    /// The PGA Squared Norm of this blade (equal to blade.Sp(blade.Reverse))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        return InternalKVector.NormSquared();
    }

    /// <summary>
    /// The square root of the absolute value of the PGA Squared Norm of this blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return InternalKVector.NormSquared().SqrtOfAbs();
    }

    /// <summary>
    /// See section 7.1 in SIGGRAPH 2019 Course notes "Geometric Algebra for Computer Graphics"
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> PGaNormSquared()
    {
        return IsEuclideanFlat() ? NormSquared() : PGaDual().NormSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> PGaNorm()
    {
        return PGaNormSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> Negative()
    {
        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> Reverse()
    {
        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> GradeInvolution()
    {
        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.GradeInvolution()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> CliffordConjugate()
    {
        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.CliffordConjugate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> Inverse()
    {
        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.Inverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> Times(T scalar)
    {
        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.Times(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> Times(Scalar<T> scalar)
    {
        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.Times(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> Times(IScalar<T> scalar)
    {
        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.Times(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> Divide(T scalar)
    {
        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.Divide(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> Divide(Scalar<T> scalar)
    {
        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.Divide(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> Divide(IScalar<T> scalar)
    {
        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.Divide(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> DivideByNorm()
    {
        var norm = InternalKVector.Norm();

        return norm.IsNearZero()
            ? this
            : Divide(norm.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> SetPGaNorm(T norm)
    {
        Debug.Assert(norm is not null);

        var oldNorm = PGaNorm();

        return oldNorm.IsZero()
            ? this
            : Times(norm / oldNorm);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> SetPGaNorm(Scalar<T> norm)
    {
        var oldNorm = PGaNorm();

        return oldNorm.IsZero()
            ? this
            : Times(norm / oldNorm);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> SetPGaNorm(IScalar<T> norm)
    {
        var oldNorm = PGaNorm();

        return oldNorm.IsZero()
            ? this
            : Times(norm / oldNorm);
    }

    /// <summary>
    /// The EGA normal of this EGA blade (equal to blade.Inverse().Lcp(Ie))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> VGaNormal()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalKVector)
        );

        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.Inverse().Lcp(GeometricSpace.Ie.InternalKVector)
        );
    }

    /// <summary>
    /// The EGA un-normal of this EGA normal blade (equal to Ie.Rcp(blade.Inverse()))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> VGaUnNormal()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalKVector)
        );

        return new PGaBlade<T>(
            GeometricSpace,
            GeometricSpace.Ie.InternalKVector.Rcp(InternalKVector.Inverse())
        );
    }

    /// <summary>
    /// The EGA dual of this EGA blade (equal to blade.Lcp(Ie.Inverse()))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> VGaDual()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalKVector)
        );

        return new PGaBlade<T>(
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
    public PGaBlade<T> VGaUnDual()
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(InternalKVector)
        );

        return new PGaBlade<T>(
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> PGaNormalize()
    {
        return Divide(PGaNorm().ScalarValue);
    }

    /// <summary>
    /// The PGA dual of this Blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> PGaDual()
    {
        return new PGaBlade<T>(
            GeometricSpace,
            PGaDualKVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> PGaDualKVector()
    {
        Debug.Assert(
            GeometricSpace.IsValidElement(InternalKVector)
        );

        return GeometricSpace
            .ProjectiveProcessor
            .PGaDual(InternalKVector, VSpaceDimensions);
    }

    /// <summary>
    /// The PGA un-dual of this Blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> PGaUnDual()
    {
        return new PGaBlade<T>(
            GeometricSpace,
            PGaUnDualKVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> PGaUnDualKVector()
    {
        Debug.Assert(
            GeometricSpace.IsValidElement(InternalKVector)
        );

        return GeometricSpace
            .ProjectiveProcessor
            .PGaUnDual(InternalKVector, VSpaceDimensions);
    }

    /// <summary>
    /// The PGA polarity blade of this Blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> PGaPolarity()
    {
        return new PGaBlade<T>(
            GeometricSpace,
            PGaPolarityKVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> PGaPolarityKVector()
    {
        Debug.Assert(
            GeometricSpace.IsValidElement(InternalKVector)
        );

        return GeometricSpace
            .ProjectiveProcessor
            .PGaPolarity(InternalKVector, VSpaceDimensions);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> Add(XGaKVector<T> blade2)
    {
        if (InternalKVector.Grade != blade2.Grade)
            throw new InvalidOperationException();

        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.Add(blade2).GetKVectorPart(InternalKVector.Grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> Add(PGaBlade<T> blade2)
    {
        if (InternalKVector.Grade != blade2.InternalKVector.Grade)
            throw new InvalidOperationException();

        return new PGaBlade<T>(
            GeometricSpace,
            InternalKVector.Add(blade2.InternalKVector).GetKVectorPart(InternalKVector.Grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> Subtract(PGaBlade<T> blade2)
    {
        if (InternalKVector.Grade != blade2.InternalKVector.Grade)
            throw new InvalidOperationException();

        return new PGaBlade<T>(
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
    public T Sp(PGaBlade<T> blade2)
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
    public PGaBlade<T> Op(PGaBlade<T> blade2)
    {
        return new PGaBlade<T>(
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
    public PGaBlade<T> Op(XGaKVector<T> blade2)
    {
        return new PGaBlade<T>(
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
    public PGaBlade<T> Lcp(PGaBlade<T> blade2)
    {
        return new PGaBlade<T>(
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
    public PGaBlade<T> Lcp(XGaKVector<T> blade2)
    {
        return new PGaBlade<T>(
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
    public PGaBlade<T> Rcp(PGaBlade<T> blade2)
    {
        return new PGaBlade<T>(
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
    public PGaBlade<T> Rcp(XGaKVector<T> blade2)
    {
        return new PGaBlade<T>(
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
    public PGaBlade<T> Fdp(PGaBlade<T> blade2)
    {
        return new PGaBlade<T>(
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
    public PGaBlade<T> Fdp(XGaKVector<T> blade2)
    {
        return new PGaBlade<T>(
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
    public XGaMultivector<T> Gp(PGaBlade<T> mv2)
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


    public PGaBlade<T> GetEuclideanMeet(PGaBlade<T> blade2)
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
            .Op(ProjectiveProcessor)
            .ToProjectiveBlade(GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PGaBlade<T> GetEuclideanJoin(PGaBlade<T> blade2)
    {
        return InternalKVector
            .BladeToVectors()
            .Concat(blade2.InternalKVector.BladeToVectors())
            .SpanToBlade(ProjectiveProcessor)
            .ToProjectiveBlade(GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<PGaBlade<T>> GetEuclideanMeetJoin(PGaBlade<T> blade2)
    {
        var meet =
            GetEuclideanMeet(blade2);

        var join =
            InternalKVector.Op(
                meet.InternalKVector.Lcp(blade2.InternalKVector)
            ).DivideByENorm().ToProjectiveBlade(GeometricSpace);

        return new Pair<PGaBlade<T>>(meet, join);
    }


    ///// <summary>
    ///// Get the IPNS distance of this blade with another (equal to -2 * blade1.Sp(blade2)
    ///// Each of the two blades must be either a PGA vector or a pseudo-vector
    ///// </summary>
    ///// <param name="blade2"></param>
    ///// <returns></returns>
    //public Scalar<T> GetIpnsDistance(PGaBlade<T> blade2)
    //{
    //    Debug.Assert(
    //        (IsVector || IsPseudoVector) &&
    //        (blade2.IsVector || blade2.IsPseudoVector)
    //    );

    //    var vector1 = ProjectiveSpace.ZeroVectorBlade.InternalVector;
    //    var vector2 = ProjectiveSpace.ZeroVectorBlade.InternalVector;

    //    if (IsVector) 
    //        vector1 = InternalVector;

    //    else if (IsPseudoVector)
    //        vector1 = InternalKVector.Lcp(ProjectiveSpace.IcInvKVector).GetVectorPart();

    //    if (blade2.IsVector) 
    //        vector2 = blade2.InternalVector;

    //    else if (blade2.IsPseudoVector)
    //        vector2 = blade2.InternalKVector.Lcp(ProjectiveSpace.IcInvKVector).GetVectorPart();

    //    return -2 * vector1.Sp(vector2).Scalar();
    //}

    ///// <summary>
    ///// Set the weight to 1 of a PGA IPNS vector (a IPNS hyper-sphere of a hyper-plane)
    ///// </summary>
    ///// <returns></returns>
    ///// <exception cref="InvalidOperationException"></exception>
    //public PGaBlade<T> NormalizeIpnsVector()
    //{
    //    Debug.Assert(IsVector);

    //    var vector = InternalVector;

    //    var eoScalar = vector[0] + vector[1];

    //    // IPNS vector encoding a sphere or point
    //    if (!eoScalar.IsNearZero())
    //        return new PGaBlade<T>(
    //            ProjectiveSpace, 
    //            (vector / eoScalar)
    //        );

    //    // IPNS vector encoding a hyper-plane
    //    var normal = vector.GetVectorPart(index => index >= 2);
    //    var normalNorm = normal.Norm();

    //    if (normalNorm.IsNearZero())
    //        throw new InvalidOperationException();

    //    var normalNormInv = 1d / normalNorm;
    //    var distance = 0.5 * (vector[0] - vector[1]) * normalNormInv;

    //    return new PGaBlade<T>(
    //        ProjectiveSpace,
    //        normal.Times(normalNormInv) + distance * ProjectiveSpace.EiVector
    //    );
    //}


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