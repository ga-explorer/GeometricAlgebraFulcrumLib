using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Versors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Visualizer;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;

/// <summary>
/// This class is a CGA Blade that can be used to encode:
/// - Euclidean subspaces (scalars, vectors, 2-blades, etc.)
/// - Euclidean Projective GA flats (points, lines, planes, etc.)
///   (see paper Projective Geometric Algebra as a Subalgebra of Conformal Geometric algebra)
/// - Conformal GA Directions, Tangents, Flats, and Round (see chapter 14 in Geometric Algebra for Computer Science)
/// </summary>
public sealed record RGaConformalBlade
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade operator -(RGaConformalBlade blade)
    {
        return blade.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade operator +(RGaConformalBlade blade1, RGaFloat64KVector blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade operator +(RGaFloat64KVector blade1, RGaConformalBlade blade2)
    {
        return blade2.Add(blade1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade operator +(RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return blade1.Add(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade operator -(RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return blade1.Subtract(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade operator *(double scalar, RGaConformalBlade blade)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade operator *(RGaConformalBlade blade, double scalar)
    {
        return blade.Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade operator /(RGaConformalBlade blade, double scalar)
    {
        return blade.Divide(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade operator /(double scalar, RGaConformalBlade blade)
    {
        return blade.Inverse().Times(scalar);
    }


    public RGaConformalSpace ConformalSpace { get; }

    public RGaConformalVisualizer Visualizer 
        => ConformalSpace switch
        {
            RGaConformalSpace4D space => space.Visualizer,
            RGaConformalSpace5D space => space.Visualizer,
            _ => throw new InvalidOperationException()
        };

    public RGaFloat64KVector InternalKVector { get; }
    
    public RGaFloat64Scalar InternalScalar
        => InternalKVector.GetScalarPart();
    
    public double InternalScalarValue
        => InternalKVector.GetScalarPart().ScalarValue();

    public RGaFloat64Vector InternalVector
        => InternalKVector.GetVectorPart();

    public RGaFloat64Bivector InternalBivector
        => InternalKVector.GetBivectorPart();

    public RGaFloat64ConformalProcessor ConformalProcessor
        => ConformalSpace.ConformalProcessor;

    public int Grade
        => InternalKVector.Grade;
    
    public int VSpaceDimensions
        => ConformalSpace.VSpaceDimensions;

    public double this[int i]
        => InternalKVector[i];
    
    public double this[int i, int j]
        => InternalKVector[i, j];

    public double this[int i, int j, int k]
        => InternalKVector[i, j, k];
    
    public double this[params int[] indexList]
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
    internal RGaConformalBlade(RGaConformalSpace conformalSpace, RGaFloat64KVector kVector)
    {
        Debug.Assert(
            kVector.Processor.HasSameSignature(conformalSpace.ConformalProcessor) &&
            conformalSpace.IsValidElement(kVector)
        );

        // This is to reduce some numerical errors by removing very small terms
        // relative to the max-magnitude scalar term of the k-vector
        InternalKVector = kVector.RemoveSmallTerms(); 
        ConformalSpace = conformalSpace;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out RGaConformalSpace conformalSpace, out RGaFloat64KVector kVector)
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
    public bool IsNearZero(double epsilon = 1e-12)
    {
        return InternalKVector.IsNearZero(epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqual(RGaFloat64Multivector blade2, double epsilon = 1e-12)
    {
        return InternalKVector.Subtract(blade2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqual(RGaConformalBlade blade2, double epsilon = 1e-12)
    {
        return InternalKVector.Subtract(blade2.InternalKVector).IsNearZero(epsilon);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalBlade RemoveEi()
    {
        var eiIdMask = (1UL << (VSpaceDimensions - 1)) - 1UL;

        var kVector = ConformalSpace.LaTeXBasisMapInverse.OmMap(
            ConformalSpace.LaTeXBasisMap
                .OmMap(InternalKVector)
                .MapBasisBlades(id => id & eiIdMask)
                .GetFirstKVectorPart()
        );

        return new RGaConformalBlade(ConformalSpace, kVector);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double DecodeScalar()
    {
        return InternalScalar.ScalarValue();
    }


    /// <summary>
    /// The Scalar Product of this blade with itself
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double SpSquared()
    {
        return InternalKVector.SpSquared().ScalarValue();
    }

    /// <summary>
    /// The CGA Squared Norm of this blade (equal to blade.Sp(blade.Reverse))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double NormSquared()
    {
        return InternalKVector.NormSquared().ScalarValue();
    }

    /// <summary>
    /// The square root of the absolute value of the CGA Squared Norm of this blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Norm()
    {
        return InternalKVector.NormSquared().ScalarValue().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade Negative()
    {
        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade Reverse()
    {
        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade GradeInvolution()
    {
        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.GradeInvolution()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade CliffordConjugate()
    {
        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.CliffordConjugate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade Inverse()
    {
        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.Inverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade Times(double scalar)
    {
        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.Times(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade Divide(double scalar)
    {
        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.Times(1d / scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade DivideByNorm()
    {
        var norm = InternalKVector.Norm();

        return norm.IsNearZero()
            ? this
            : Divide(norm.ScalarValue());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade SetNorm(double norm)
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
    public RGaConformalBlade EGaNormal()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalKVector)
        );

        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.Inverse().Lcp(ConformalSpace.Ie.InternalKVector)
        );
    }
    
    /// <summary>
    /// The EGA un-normal of this EGA normal blade (equal to Ie.Rcp(blade.Inverse()))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade EGaUnNormal()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalKVector)
        );

        return new RGaConformalBlade(
            ConformalSpace,
            ConformalSpace.Ie.InternalKVector.Rcp(InternalKVector.Inverse())
        );
    }

    /// <summary>
    /// The EGA dual of this EGA blade (equal to blade.Lcp(Ie.Inverse()))
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade EGaDual()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalKVector)
        );

        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.Lcp(ConformalSpace.IeInv.InternalKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector EGaDualKVector()
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
    public RGaConformalBlade EGaUnDual()
    {
        Debug.Assert(
            ConformalSpace.IsValidEGaElement(InternalKVector)
        );

        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.Lcp(ConformalSpace.Ie.InternalKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector EGaUnDualKVector()
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
    public double PGaNormSquared()
    {
        Debug.Assert(this.IsPGaBlade());

        return InternalKVector.Sp(
            InternalKVector.CliffordConjugate()
        ).ScalarValue();
    }

    /// <summary>
    /// The Squared Root of the Absolute Value of the Squared Norm of this PGA Blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double PGaNorm()
    {
        return PGaNormSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade PGaNormalize()
    {
        return Divide(PGaNorm());
    }

    /// <summary>
    /// The PGA dual of this PGA Blade (equal to #[blade.Op(Ei).Lcp(Ic.Inverse())])
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade PGaDual()
    {
        return new RGaConformalBlade(
            ConformalSpace,
            PGaDualKVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector PGaDualKVector()
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
    public RGaConformalBlade PGaUnDual()
    {
        return new RGaConformalBlade(
            ConformalSpace,
            PGaUnDualKVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector PGaUnDualKVector()
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
    public RGaConformalBlade CGaDual()
    {
        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.Lcp(ConformalSpace.IcInv.InternalKVector)
        );
    }

    /// <summary>
    /// The CGA un-dual of this Blade
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade CGaUnDual()
    {
        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.Lcp(ConformalSpace.Ic.InternalKVector)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade Add(RGaFloat64KVector blade2)
    {
        if (InternalKVector.Grade != blade2.Grade)
            throw new InvalidOperationException();

        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.Add(blade2).GetKVectorPart(InternalKVector.Grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade Add(RGaConformalBlade blade2)
    {
        if (InternalKVector.Grade != blade2.InternalKVector.Grade)
            throw new InvalidOperationException();

        return new RGaConformalBlade(
            ConformalSpace,
            InternalKVector.Add(blade2.InternalKVector).GetKVectorPart(InternalKVector.Grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade Subtract(RGaConformalBlade blade2)
    {
        if (InternalKVector.Grade != blade2.InternalKVector.Grade)
            throw new InvalidOperationException();

        return new RGaConformalBlade(
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
    public double Sp(RGaConformalBlade blade2)
    {
        return InternalKVector.Sp(blade2.InternalKVector).ScalarValue();
    }

    /// <summary>
    /// The scalar product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Sp(RGaFloat64KVector blade2)
    {
        return InternalKVector.Sp(blade2).ScalarValue();
    }

    /// <summary>
    /// The outer product of this blade with another
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade Op(RGaConformalBlade blade2)
    {
        return new RGaConformalBlade(
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
    public RGaConformalBlade Op(RGaFloat64KVector blade2)
    {
        return new RGaConformalBlade(
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
    public RGaConformalBlade Lcp(RGaConformalBlade blade2)
    {
        return new RGaConformalBlade(
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
    public RGaConformalBlade Lcp(RGaFloat64KVector blade2)
    {
        return new RGaConformalBlade(
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
    public RGaConformalBlade Rcp(RGaConformalBlade blade2)
    {
        return new RGaConformalBlade(
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
    public RGaConformalBlade Rcp(RGaFloat64KVector blade2)
    {
        return new RGaConformalBlade(
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
    public RGaConformalBlade Fdp(RGaConformalBlade blade2)
    {
        return new RGaConformalBlade(
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
    public RGaConformalBlade Fdp(RGaFloat64KVector blade2)
    {
        return new RGaConformalBlade(
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
    public RGaFloat64Multivector Gp(RGaConformalBlade mv2)
    {
        return InternalKVector.Gp(mv2.InternalKVector);
    }

    /// <summary>
    /// The geometric product of this blade with a multivector
    /// </summary>
    /// <param name="mv2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Gp(RGaFloat64Multivector mv2)
    {
        return InternalKVector.Gp(mv2);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade MapUsingMusicalIsomorphism()
    {
        return new RGaConformalBlade(
            ConformalSpace,
            ConformalSpace.MusicalIsomorphism.OmMap(InternalKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade MapUsing(IRGaFloat64Outermorphism outerMorphism)
    {
        return new RGaConformalBlade(
            ConformalSpace,
            outerMorphism.OmMap(InternalKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade MapUsing(RGaConformalVersor versor)
    {
        return versor.MapBlade(InternalKVector);
    }


    /// <summary>
    /// Apply a uniform scaling to this CGA blade
    /// </summary>
    /// <param name="scalingFactor"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade ScaleBy(double scalingFactor)
    {
        Debug.Assert(
            scalingFactor.IsValid() &&
            scalingFactor > 0
        );

        var g = 0.5 * scalingFactor.LogE();
        var s0 = Math.Cosh(g);
        var s2 = Math.Sinh(g) * ConformalSpace.Eoi.InternalKVector;

        var kVector =
            (s0 + s2).Gp(InternalKVector).Gp(s0 - s2).GetKVectorPart(InternalKVector.Grade);

        return new RGaConformalBlade(ConformalSpace, kVector);
    }


    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade TranslateBy(Float64Vector2D egaTranslationVector)
    {
        return TranslateBy(
            ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade TranslateBy(Float64Vector3D egaTranslationVector)
    {
        return TranslateBy(
            ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade TranslateBy(Float64Vector egaTranslationVector)
    {
        return TranslateBy(
            ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }
    
    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade TranslateBy(RGaFloat64Vector egaTranslationVector)
    {
        return TranslateBy(
            ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade TranslateBy(RGaConformalBlade egaTranslationVector)
    {
        Debug.Assert(
            egaTranslationVector.IsEGaVector()
        );

        var eit =
            0.5d * ConformalSpace.Ei.InternalKVector.Op(egaTranslationVector.InternalKVector);

        var kVector =
            (1 + eit).Gp(InternalKVector).Gp(1 - eit).GetKVectorPart(Grade);

        return new RGaConformalBlade(ConformalSpace, kVector);
    }
    

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade TranslatePGaBy(Float64Vector2D egaTranslationVector)
    {
        return TranslatePGaBy(
            ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade TranslatePGaBy(Float64Vector3D egaTranslationVector)
    {
        return TranslatePGaBy(
            ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade TranslatePGaBy(Float64Vector egaTranslationVector)
    {
        return TranslatePGaBy(
            ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade TranslatePGaBy(RGaFloat64Vector egaTranslationVector)
    {
        return TranslatePGaBy(
            ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade TranslatePGaBy(RGaConformalBlade egaTranslationVector)
    {
        Debug.Assert(
            this.IsPGaBlade() &&
            egaTranslationVector.IsEGaBlade()
        );

        var eot = 
            0.5d * ConformalSpace.EoVector.Op(egaTranslationVector.InternalVector);

        var kVector = 
            (1 - eot).Gp(InternalKVector).Gp(1 + eot).GetKVectorPart(InternalKVector.Grade);

        return new RGaConformalBlade(ConformalSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalBlade ProjectOn(RGaFloat64KVector subspace)
    {
        var projectedBlade =
            InternalKVector
                .Fdp(subspace)
                .Gp(subspace)
                .GetKVectorPart(InternalKVector.Grade);

        var scalarFactor = subspace.SpSquared();

        var kVector = scalarFactor.IsNearZero()
            ? projectedBlade
            : projectedBlade / scalarFactor;

        return new RGaConformalBlade(ConformalSpace, kVector);
    }
    
    /// <summary>
    /// Project this EGA Blade on another EGA Blade
    /// </summary>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade ProjectEGaOnEGa(RGaConformalBlade subspace)
    {
        Debug.Assert(
            this.IsEGaBlade() &&
            subspace.IsEGaBlade()
        );

        return ProjectOn(subspace.InternalKVector);
    }

    /// <summary>
    /// Project this CGA OPNS Blade on another CGA OPNS Blade
    /// </summary>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade ProjectOpnsOnOpns(RGaConformalBlade subspace)
    {
        return ProjectOn(subspace.InternalKVector);
    }
    
    /// <summary>
    /// Project this CGA IPNS Blade on another CGA IPNS Blade
    /// </summary>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade ProjectIpnsOnIpns(RGaConformalBlade subspace)
    {
        return ProjectOn(subspace.InternalKVector);
    }

    /// <summary>
    /// Project this PGA Blade on another PGA Blade
    /// </summary>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade ProjectPGaOnPGa(RGaConformalBlade subspace)
    {
        Debug.Assert(
            this.IsPGaBlade() &&
            subspace.IsPGaBlade()
        );

        return ProjectOn(subspace.InternalKVector);
    }
    

    /// <summary>
    /// Intersect this CGA OPNS Blade with another CGA OPNS Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade IntersectOpnsWithOpns(RGaConformalBlade blade2)
    {
        return Op(blade2);
    }
    
    /// <summary>
    /// Intersect this CGA OPNS Blade with two other CGA OPNS Blades
    /// </summary>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade IntersectOpnsWithOpns(RGaConformalBlade blade2, RGaConformalBlade blade3)
    {
        return Op(blade2).Op(blade3);
    }

    
    /// <summary>
    /// Intersect this CGA IPNS Blade with another CGA IPNS Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade IntersectIpnsWithIpns(RGaConformalBlade blade2)
    {
        return blade2.Op(this);
    }
    
    /// <summary>
    /// Intersect this CGA IPNS Blade with two other CGA IPNS Blades
    /// </summary>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade IntersectIpnsWithIpns(RGaConformalBlade blade2, RGaConformalBlade blade3)
    {
        return blade3.Op(blade2).Op(this);
    }
    
    
    /// <summary>
    /// Reflect this CGA OPNS Blade on another CGA OPNS Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade ReflectOpnsOnOpns(RGaConformalBlade blade2)
    {
        return new RGaConformalBlade(
            ConformalSpace,
            ConformalSpace.ReflectOpnsOnOpns(InternalKVector, blade2.InternalKVector)
        );
    }
    
    /// <summary>
    /// Reflect this CGA OPNS Blade on another CGA IPNS Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade ReflectOpnsOnIpns(RGaConformalBlade blade2)
    {
        return new RGaConformalBlade(
            ConformalSpace,
            ConformalSpace.ReflectOpnsOnIpns(InternalKVector, blade2.InternalKVector)
        );
    }

    /// <summary>
    /// Reflect this CGA IPNS Blade on another CGA OPNS Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade ReflectIpnsOnOpns(RGaConformalBlade blade2)
    {
        return new RGaConformalBlade(
            ConformalSpace,
            ConformalSpace.ReflectIpnsOnOpns(InternalKVector, blade2.InternalKVector)
        );
    }
    
    /// <summary>
    /// Reflect this CGA IPNS Blade on another CGA IPNS Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade ReflectIpnsOnIpns(RGaConformalBlade blade2)
    {
        return new RGaConformalBlade(
            ConformalSpace,
            ConformalSpace.ReflectIpnsOnIpns(InternalKVector, blade2.InternalKVector)
        );
    }


    /// <summary>
    /// Intersect this PGA Blade with another PGA Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade IntersectPGaWithPGa(RGaConformalBlade blade2)
    {
        Debug.Assert(
            this.IsPGaBlade() &&
            blade2.IsPGaBlade()
        );

        return blade2.Op(this);
    }
    
    /// <summary>
    /// Intersect this PGA Blade with two other PGA Blades
    /// </summary>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade IntersectPGaWithPGa(RGaConformalBlade blade2, RGaConformalBlade blade3)
    {
        Debug.Assert(
            this.IsPGaBlade() &&
            blade2.IsPGaBlade() &&
            blade3.IsPGaBlade()
        );

        return blade3.Op(blade2).Op(this);
    }


    /// <summary>
    /// Get the IPNS distance of this blade with another (equal to -2 * blade1.Sp(blade2)
    /// Each of the two blades must be either a CGA vector or a pseudo-vector
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    public double GetIpnsDistance(RGaConformalBlade blade2)
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

        return -2 * vector1.Sp(vector2).ScalarValue();
    }
    
    /// <summary>
    /// Set the weight to 1 of a CGA IPNS vector (a IPNS hyper-sphere of a hyper-plane)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public RGaConformalBlade NormalizeIpnsVector()
    {
        Debug.Assert(IsVector);

        var vector = InternalVector;

        var eoScalar = vector[0] + vector[1];

        // IPNS vector encoding a sphere or point
        if (!eoScalar.IsNearZero())
            return new RGaConformalBlade(
                ConformalSpace, 
                (vector / eoScalar)
            );

        // IPNS vector encoding a hyper-plane
        var normal = vector.GetVectorPart((int index) => index >= 2);
        var normalNorm = normal.Norm();

        if (normalNorm.IsNearZero())
            throw new InvalidOperationException();

        var normalNormInv = 1d / normalNorm;
        var distance = 0.5 * (vector[0] - vector[1]) * normalNormInv;

        return new RGaConformalBlade(
            ConformalSpace,
            normal.Times(normalNormInv) + distance * ConformalSpace.EiVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX()
    {
        return ConformalSpace.ToLaTeX(InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return ToLaTeX();
    }
}