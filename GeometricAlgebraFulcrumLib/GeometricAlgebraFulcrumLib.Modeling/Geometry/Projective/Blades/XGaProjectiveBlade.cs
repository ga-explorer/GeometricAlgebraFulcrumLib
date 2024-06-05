using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Blades;

/// <summary>
/// This class is a PGA Blade that can be used to encode:
/// - Euclidean subspaces (scalars, vectors, 2-blades, etc.)
/// - Euclidean Projective GA flats (points, lines, planes, etc.)
/// - Projective GA Directions and Flats
/// </summary>
public sealed record XGaProjectiveBlade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator -(XGaProjectiveBlade<T> blade)
    {
        return blade.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator +(XGaProjectiveBlade<T> blade1, XGaKVector<T> blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator +(XGaKVector<T> blade1, XGaProjectiveBlade<T> blade2)
    {
        return blade2.Add(blade1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator +(XGaProjectiveBlade<T> blade1, XGaProjectiveBlade<T> blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator -(XGaProjectiveBlade<T> blade1, XGaProjectiveBlade<T> blade2)
    {
        return blade1.Subtract(blade2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator *(int scalar, XGaProjectiveBlade<T> blade)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator *(float scalar, XGaProjectiveBlade<T> blade)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator *(double scalar, XGaProjectiveBlade<T> blade)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator *(T scalar, XGaProjectiveBlade<T> blade)
    {
        return blade.Times(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator *(Scalar<T> scalar, XGaProjectiveBlade<T> blade)
    {
        return blade.Times(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator *(IScalar<T> scalar, XGaProjectiveBlade<T> blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator *(XGaProjectiveBlade<T> blade, int scalar)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator *(XGaProjectiveBlade<T> blade, float scalar)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator *(XGaProjectiveBlade<T> blade, double scalar)
    {
        return blade.Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator *(XGaProjectiveBlade<T> blade, T scalar)
    {
        return blade.Times(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator *(XGaProjectiveBlade<T> blade, IScalar<T> scalar)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator /(XGaProjectiveBlade<T> blade, int scalar)
    {
        return blade.Divide(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator /(XGaProjectiveBlade<T> blade, float scalar)
    {
        return blade.Divide(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator /(XGaProjectiveBlade<T> blade, double scalar)
    {
        return blade.Divide(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator /(XGaProjectiveBlade<T> blade, T scalar)
    {
        return blade.Divide(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator /(XGaProjectiveBlade<T> blade, IScalar<T> scalar)
    {
        return blade.Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator /(int scalar, XGaProjectiveBlade<T> blade)
    {
        return blade.Inverse().Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator /(float scalar, XGaProjectiveBlade<T> blade)
    {
        return blade.Inverse().Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator /(double scalar, XGaProjectiveBlade<T> blade)
    {
        return blade.Inverse().Times(blade.ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator /(IScalar<T> scalar, XGaProjectiveBlade<T> blade)
    {
        return blade.Inverse().Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> operator /(T scalar, XGaProjectiveBlade<T> blade)
    {
        return blade.Inverse().Times(scalar);
    }


    public XGaProjectiveSpace<T> ProjectiveSpace { get; }

    public XGaGeometrySpaceBasisSpecs<T> BasisSpecs 
        => ProjectiveSpace.BasisSpecs;

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
        => ProjectiveSpace.ProjectiveProcessor;
    
    public IScalarProcessor<T> ScalarProcessor
        => ProjectiveSpace.ProjectiveProcessor.ScalarProcessor;

    public int Grade
        => InternalKVector.Grade;
    
    public int VSpaceDimensions
        => ProjectiveSpace.VSpaceDimensions;

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
        => InternalKVector.Grade == ProjectiveSpace.VSpaceDimensions - 1;

    public bool IsPseudoScalar
        => InternalKVector.Grade == ProjectiveSpace.VSpaceDimensions;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaProjectiveBlade(XGaProjectiveSpace<T> projectiveSpace, XGaKVector<T> kVector)
    {
        Debug.Assert(
            kVector.Processor.HasSameSignature(projectiveSpace.ProjectiveProcessor) &&
            projectiveSpace.IsValidElement(kVector)
        );

        // This is to reduce some numerical errors by removing very small terms
        // relative to the max-magnitude scalar term of the k-vector
        //InternalKVector = kVector.RemoveSmallTerms(); 
        InternalKVector = kVector;
        ProjectiveSpace = projectiveSpace;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out XGaProjectiveSpace<T> projectiveSpace, out XGaKVector<T> kVector)
    {
        projectiveSpace = ProjectiveSpace;
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
        return ProjectiveSpace.IsValidPGaPoint(InternalKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsPGaIdealPoint()
    {
        return ProjectiveSpace.IsValidPGaVector(InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsHGaPoint()
    {
        return ProjectiveSpace.IsValidHGaPoint(InternalKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsHGaIdealPoint()
    {
        return ProjectiveSpace.IsValidHGaVector(InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqual(XGaMultivector<T> blade2)
    {
        return InternalKVector.Subtract(blade2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqual(XGaProjectiveBlade<T> blade2)
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
    public XGaProjectiveBlade<T> Negative()
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Reverse()
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> GradeInvolution()
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.GradeInvolution()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> CliffordConjugate()
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.CliffordConjugate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Inverse()
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Inverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Times(T scalar)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Times(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Times(Scalar<T> scalar)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Times(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Times(IScalar<T> scalar)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Times(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Divide(T scalar)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Divide(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Divide(Scalar<T> scalar)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Divide(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Divide(IScalar<T> scalar)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Divide(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> DivideByNorm()
    {
        var norm = InternalKVector.Norm();

        return norm.IsNearZero()
            ? this
            : Divide(norm.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> SetPGaNorm(T norm)
    {
        Debug.Assert(norm is not null);

        var oldNorm = PGaNorm();

        return oldNorm.IsZero()
            ? this
            : Times(norm / oldNorm);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> SetPGaNorm(Scalar<T> norm)
    {
        var oldNorm = PGaNorm();

        return oldNorm.IsZero()
            ? this
            : Times(norm / oldNorm);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> SetPGaNorm(IScalar<T> norm)
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
    public XGaProjectiveBlade<T> EGaNormal()
    {
        Debug.Assert(
            ProjectiveSpace.IsValidEGaElement(InternalKVector)
        );

        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Inverse().Lcp(ProjectiveSpace.Ie.InternalKVector)
        );
    }
    
    /// <summary>
    /// The EGA un-normal of this EGA normal blade (equal to Ie.Rcp(blade.Inverse()))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> EGaUnNormal()
    {
        Debug.Assert(
            ProjectiveSpace.IsValidEGaElement(InternalKVector)
        );

        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            ProjectiveSpace.Ie.InternalKVector.Rcp(InternalKVector.Inverse())
        );
    }

    /// <summary>
    /// The EGA dual of this EGA blade (equal to blade.Lcp(Ie.Inverse()))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> EGaDual()
    {
        Debug.Assert(
            ProjectiveSpace.IsValidEGaElement(InternalKVector)
        );

        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Lcp(ProjectiveSpace.IeInv.InternalKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> EGaDualKVector()
    {
        Debug.Assert(
            ProjectiveSpace.IsValidEGaElement(InternalKVector)
        );

        return InternalKVector.Lcp(ProjectiveSpace.IeInv.InternalKVector);
    }

    /// <summary>
    /// The EGA un-dual of this EGA blade (equal to blade.Lcp(Ie))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> EGaUnDual()
    {
        Debug.Assert(
            ProjectiveSpace.IsValidEGaElement(InternalKVector)
        );

        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Lcp(ProjectiveSpace.Ie.InternalKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> EGaUnDualKVector()
    {
        Debug.Assert(
            ProjectiveSpace.IsValidEGaElement(InternalKVector)
        );

        return InternalKVector.Lcp(ProjectiveSpace.Ie.InternalKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> PGaNormalize()
    {
        return Divide(PGaNorm().ScalarValue);
    }

    /// <summary>
    /// The PGA dual of this PGA Blade (equal to #[blade.Op(Ei).Lcp(Ic.Inverse())])
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> PGaDual()
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            PGaDualKVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> PGaDualKVector()
    {
        Debug.Assert(
            ProjectiveSpace.IsValidElement(InternalKVector)
        );

        return ProjectiveSpace
            .ProjectiveProcessor
            .PGaDual(InternalKVector, VSpaceDimensions);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Add(XGaKVector<T> blade2)
    {
        if (InternalKVector.Grade != blade2.Grade)
            throw new InvalidOperationException();

        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Add(blade2).GetKVectorPart(InternalKVector.Grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Add(XGaProjectiveBlade<T> blade2)
    {
        if (InternalKVector.Grade != blade2.InternalKVector.Grade)
            throw new InvalidOperationException();

        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Add(blade2.InternalKVector).GetKVectorPart(InternalKVector.Grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Subtract(XGaProjectiveBlade<T> blade2)
    {
        if (InternalKVector.Grade != blade2.InternalKVector.Grade)
            throw new InvalidOperationException();

        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Subtract(blade2.InternalKVector).GetKVectorPart(InternalKVector.Grade)
        );
    }

    /// <summary>
    /// The scalar product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Sp(XGaProjectiveBlade<T> blade2)
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
    public XGaProjectiveBlade<T> Op(XGaProjectiveBlade<T> blade2)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Op(blade2.InternalKVector)
        );
    }

    /// <summary>
    /// The outer product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Op(XGaKVector<T> blade2)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Op(blade2)
        );
    }

    /// <summary>
    /// The left-contraction product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Lcp(XGaProjectiveBlade<T> blade2)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Lcp(blade2.InternalKVector)
        );
    }

    /// <summary>
    /// The left-contraction product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Lcp(XGaKVector<T> blade2)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Lcp(blade2)
        );
    }

    /// <summary>
    /// The right-contraction product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Rcp(XGaProjectiveBlade<T> blade2)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Rcp(blade2.InternalKVector)
        );
    }

    /// <summary>
    /// The right-contraction product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Rcp(XGaKVector<T> blade2)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Rcp(blade2)
        );
    }

    /// <summary>
    /// The fat-dot product (PGA inner product) of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Fdp(XGaProjectiveBlade<T> blade2)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Fdp(blade2.InternalKVector).GetFirstKVectorPart()
        );
    }

    /// <summary>
    /// The fat-dot product (PGA inner product) of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> Fdp(XGaKVector<T> blade2)
    {
        return new XGaProjectiveBlade<T>(
            ProjectiveSpace,
            InternalKVector.Fdp(blade2).GetFirstKVectorPart()
        );
    }
    
    /// <summary>
    /// The geometric product of this blade with another
    /// </summary>
    /// <param name="mv2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Gp(XGaProjectiveBlade<T> mv2)
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
    

    public XGaProjectiveBlade<T> GetEuclideanMeet(XGaProjectiveBlade<T> blade2)
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
            .ToProjectiveBlade(ProjectiveSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> GetEuclideanJoin(XGaProjectiveBlade<T> blade2)
    {
        return InternalKVector
            .BladeToVectors()
            .Concat(blade2.InternalKVector.BladeToVectors())
            .SpanToBlade(ProjectiveProcessor)
            .ToProjectiveBlade(ProjectiveSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<XGaProjectiveBlade<T>> GetEuclideanMeetJoin(XGaProjectiveBlade<T> blade2)
    {
        var meet = 
            GetEuclideanMeet(blade2);
        
        var join = 
            InternalKVector.Op(
                meet.InternalKVector.Lcp(blade2.InternalKVector)
            ).DivideByENorm().ToProjectiveBlade(ProjectiveSpace);

        return new Pair<XGaProjectiveBlade<T>>(meet, join);
    }

    
    ///// <summary>
    ///// Get the IPNS distance of this blade with another (equal to -2 * blade1.Sp(blade2)
    ///// Each of the two blades must be either a PGA vector or a pseudo-vector
    ///// </summary>
    ///// <param name="blade2"></param>
    ///// <returns></returns>
    //public Scalar<T> GetIpnsDistance(XGaProjectiveBlade<T> blade2)
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
    //public XGaProjectiveBlade<T> NormalizeIpnsVector()
    //{
    //    Debug.Assert(IsVector);

    //    var vector = InternalVector;

    //    var eoScalar = vector[0] + vector[1];

    //    // IPNS vector encoding a sphere or point
    //    if (!eoScalar.IsNearZero())
    //        return new XGaProjectiveBlade<T>(
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

    //    return new XGaProjectiveBlade<T>(
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