//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
//using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
//using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

//namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.PGa.Blades;

///// <summary>
///// This class is a CGA Blade that can be used to encode:
///// - Euclidean subspaces (scalars, vectors, 2-blades, etc.)
///// - Euclidean Projective GA flats (points, lines, planes, etc.)
/////   (see paper Projective Geometric Algebra as a Subalgebra of Conformal Geometric algebra)
///// - Conformal GA Directions, Tangents, Flats, and Round (see chapter 14 in Geometric Algebra for Computer Science)
///// </summary>
//public sealed record PGaFloat64Blade
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade operator -(PGaFloat64Blade blade)
//    {
//        return blade.Negative();
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade operator +(PGaFloat64Blade blade1, XGaFloat64KVector blade2)
//    {
//        return blade1.Add(blade2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade operator +(XGaFloat64KVector blade1, PGaFloat64Blade blade2)
//    {
//        return blade2.Add(blade1);
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade operator +(PGaFloat64Blade blade1, PGaFloat64Blade blade2)
//    {
//        return blade1.Add(blade2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade operator -(PGaFloat64Blade blade1, PGaFloat64Blade blade2)
//    {
//        return blade1.Subtract(blade2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade operator *(double scalar, PGaFloat64Blade blade)
//    {
//        return blade.Times(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade operator *(PGaFloat64Blade blade, double scalar)
//    {
//        return blade.Times(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade operator /(PGaFloat64Blade blade, double scalar)
//    {
//        return blade.Divide(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade operator /(double scalar, PGaFloat64Blade blade)
//    {
//        return blade.Inverse().Times(scalar);
//    }


//    public PGaFloat64GeometricSpace GeometricSpace { get; }

//    public GaFloat64GeometricSpaceBasisSpecs BasisSpecs 
//        => GeometricSpace.BasisSpecs;

//    public PGaFloat64Visualizer Visualizer 
//        => GeometricSpace switch
//        {
//            PGaFloat64GeometricSpace4D space => space.Visualizer,
//            PGaFloat64GeometricSpace5D space => space.Visualizer,
//            _ => throw new InvalidOperationException()
//        };

//    public XGaFloat64KVector InternalKVector { get; }
    
//    public XGaFloat64Scalar InternalScalar
//        => InternalKVector.GetScalarPart();
    
//    public double InternalScalarValue
//        => InternalKVector.GetScalarPart().ScalarValue;

//    public XGaFloat64Vector InternalVector
//        => InternalKVector.GetVectorPart();

//    public XGaFloat64Bivector InternalBivector
//        => InternalKVector.GetBivectorPart();

//    public XGaFloat64ConformalProcessor ConformalProcessor
//        => GeometricSpace.ConformalProcessor;

//    public int Grade
//        => InternalKVector.Grade;
    
//    public int VSpaceDimensions
//        => GeometricSpace.VSpaceDimensions;

//    public double this[int i]
//        => InternalKVector[i];
    
//    public double this[int i, int j]
//        => InternalKVector[i, j];

//    public double this[int i, int j, int k]
//        => InternalKVector[i, j, k];
    
//    public double this[params int[] indexList]
//        => InternalKVector[indexList];

//    public bool IsScalar
//        => InternalKVector.Grade == 0;

//    public bool IsVector
//        => InternalKVector.Grade == 1;

//    public bool IsBivector
//        => InternalKVector.Grade == 2;

//    public bool IsTrivector
//        => InternalKVector.Grade == 3;

//    public bool IsPseudoVector
//        => InternalKVector.Grade == GeometricSpace.VSpaceDimensions - 1;

//    public bool IsPseudoScalar
//        => InternalKVector.Grade == GeometricSpace.VSpaceDimensions;


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    internal PGaFloat64Blade(PGaFloat64GeometricSpace cgaGeometricSpace, XGaFloat64KVector kVector)
//    {
//        Debug.Assert(
//            kVector.Processor.HasSameSignature(cgaGeometricSpace.ConformalProcessor) &&
//            cgaGeometricSpace.IsValidElement(kVector)
//        );

//        // This is to reduce some numerical errors by removing very small terms
//        // relative to the max-magnitude scalar term of the k-vector
//        InternalKVector = kVector.RemoveSmallTerms(); 
//        GeometricSpace = cgaGeometricSpace;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public void Deconstruct(out PGaFloat64GeometricSpace cgaGeometricSpace, out XGaFloat64KVector kVector)
//    {
//        cgaGeometricSpace = GeometricSpace;
//        kVector = InternalKVector;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool IsZero()
//    {
//        return InternalKVector.IsZero;
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool IsNearZero(double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        return InternalKVector.IsNearZero(zeroEpsilon);
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool IsNearEqual(XGaFloat64Multivector blade2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        return InternalKVector.Subtract(blade2).IsNearZero(zeroEpsilon);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool IsNearEqual(PGaFloat64Blade blade2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        return InternalKVector.Subtract(blade2.InternalKVector).IsNearZero(zeroEpsilon);
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    internal PGaFloat64Blade RemoveEi()
//    {
//        var eiIdMask = (1UL << (VSpaceDimensions - 1)) - 1UL;

//        var kVector = BasisSpecs.BasisMapInverse.OmMap(
//            BasisSpecs
//                .BasisMap
//                .OmMap(InternalKVector)
//                .MapBasisBlades(id => id & eiIdMask)
//                .GetFirstKVectorPart()
//        );

//        return new PGaFloat64Blade(GeometricSpace, kVector);
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double DecodeScalar()
//    {
//        return InternalScalar.ScalarValue;
//    }


//    /// <summary>
//    /// The Scalar Product of this blade with itself
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double SpSquared()
//    {
//        return InternalKVector.SpSquared().ScalarValue;
//    }

//    /// <summary>
//    /// The CGA Squared Norm of this blade (equal to blade.Sp(blade.Reverse))
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double NormSquared()
//    {
//        return InternalKVector.NormSquared().ScalarValue;
//    }

//    /// <summary>
//    /// The square root of the absolute value of the CGA Squared Norm of this blade
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double Norm()
//    {
//        return InternalKVector.NormSquared().ScalarValue.SqrtOfAbs();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Negative()
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Negative()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Reverse()
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Reverse()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade GradeInvolution()
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.GradeInvolution()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade CliffordConjugate()
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.CliffordConjugate()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Inverse()
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Inverse()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Times(double scalar)
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Times(scalar)
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Divide(double scalar)
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Times(1d / scalar)
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade DivideByNorm()
//    {
//        var norm = InternalKVector.Norm();

//        return norm.IsNearZero()
//            ? this
//            : Divide(norm.ScalarValue);
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade SetNorm(double norm)
//    {
//        var oldNorm = Norm();

//        return oldNorm.IsZero()
//            ? this
//            : Times(norm / oldNorm);
//    }
    
//    /// <summary>
//    /// The EGA normal of this EGA blade (equal to blade.Inverse().Lcp(Ie))
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade VGaNormal()
//    {
//        Debug.Assert(
//            GeometricSpace.IsValidVGaElement(InternalKVector)
//        );

//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Inverse().Lcp(GeometricSpace.Ie.InternalKVector)
//        );
//    }
    
//    /// <summary>
//    /// The EGA un-normal of this EGA normal blade (equal to Ie.Rcp(blade.Inverse()))
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade VGaUnNormal()
//    {
//        Debug.Assert(
//            GeometricSpace.IsValidVGaElement(InternalKVector)
//        );

//        return new PGaFloat64Blade(
//            GeometricSpace,
//            GeometricSpace.Ie.InternalKVector.Rcp(InternalKVector.Inverse())
//        );
//    }

//    /// <summary>
//    /// The EGA dual of this EGA blade (equal to blade.Lcp(Ie.Inverse()))
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade VGaDual()
//    {
//        Debug.Assert(
//            GeometricSpace.IsValidVGaElement(InternalKVector)
//        );

//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Lcp(GeometricSpace.IeInv.InternalKVector)
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public XGaFloat64KVector VGaDualKVector()
//    {
//        Debug.Assert(
//            GeometricSpace.IsValidVGaElement(InternalKVector)
//        );

//        return InternalKVector.Lcp(GeometricSpace.IeInv.InternalKVector);
//    }

//    /// <summary>
//    /// The EGA un-dual of this EGA blade (equal to blade.Lcp(Ie))
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade VGaUnDual()
//    {
//        Debug.Assert(
//            GeometricSpace.IsValidVGaElement(InternalKVector)
//        );

//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Lcp(GeometricSpace.Ie.InternalKVector)
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public XGaFloat64KVector VGaUnDualKVector()
//    {
//        Debug.Assert(
//            GeometricSpace.IsValidVGaElement(InternalKVector)
//        );

//        return InternalKVector.Lcp(GeometricSpace.Ie.InternalKVector);
//    }
    
//    /// <summary>
//    /// The Squared Norm of this PGA Blade (equal to blade.Sp(blade.CliffordConjugate()))
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double PGaNormSquared()
//    {
//        Debug.Assert(this.IsPGaBlade());

//        return InternalKVector.Sp(
//            InternalKVector.CliffordConjugate()
//        ).ScalarValue;
//    }

//    /// <summary>
//    /// The Squared Root of the Absolute Value of the Squared Norm of this PGA Blade
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double PGaNorm()
//    {
//        return PGaNormSquared().SqrtOfAbs();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade PGaNormalize()
//    {
//        return Divide(PGaNorm());
//    }

//    /// <summary>
//    /// The PGA dual of this PGA Blade (equal to #[blade.Op(Ei).Lcp(Ic.Inverse())])
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade PGaDual()
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            PGaDualKVector()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public XGaFloat64KVector PGaDualKVector()
//    {
//        Debug.Assert(
//            GeometricSpace.IsValidPGaElement(InternalKVector)
//        );

//        return GeometricSpace.MusicalIsomorphism.OmMap(
//            InternalKVector.Op(GeometricSpace.EiVector).Lcp(GeometricSpace.IcInvKVector)
//            // Also can be mv.Op(Ei).Lcp(Ic)
//        );
//    }

//    /// <summary>
//    /// The PGA un-dual of this PGA Blade (equal to #[blade.Op(Ei).Lcp(Ic.Inverse())])
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade PGaUnDual()
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            PGaUnDualKVector()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public XGaFloat64KVector PGaUnDualKVector()
//    {
//        Debug.Assert(
//            GeometricSpace.IsValidPGaElement(InternalKVector)
//        );

//        return GeometricSpace.MusicalIsomorphism.OmMap(
//            InternalKVector.Op(GeometricSpace.EiVector).Lcp(GeometricSpace.IcInvKVector)
//            // Also can be mv.Op(Ei).Lcp(Ic)
//        );
//    }

//    /// <summary>
//    /// The CGA dual of this Blade
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade PGaDual()
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Lcp(GeometricSpace.IcInv.InternalKVector)
//        );
//    }

//    /// <summary>
//    /// The CGA un-dual of this Blade
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade PGaUnDual()
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Lcp(GeometricSpace.Ic.InternalKVector)
//        );
//    }
    

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Add(XGaFloat64KVector blade2)
//    {
//        if (InternalKVector.Grade != blade2.Grade)
//            throw new InvalidOperationException();

//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Add(blade2).GetKVectorPart(InternalKVector.Grade)
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Add(PGaFloat64Blade blade2)
//    {
//        if (InternalKVector.Grade != blade2.InternalKVector.Grade)
//            throw new InvalidOperationException();

//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Add(blade2.InternalKVector).GetKVectorPart(InternalKVector.Grade)
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Subtract(PGaFloat64Blade blade2)
//    {
//        if (InternalKVector.Grade != blade2.InternalKVector.Grade)
//            throw new InvalidOperationException();

//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Subtract(blade2.InternalKVector).GetKVectorPart(InternalKVector.Grade)
//        );
//    }

//    /// <summary>
//    /// The scalar product of this blade with another
//    /// </summary>
//    /// <param name="blade2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double Sp(PGaFloat64Blade blade2)
//    {
//        return InternalKVector.Sp(blade2.InternalKVector).ScalarValue;
//    }

//    /// <summary>
//    /// The scalar product of this blade with another
//    /// </summary>
//    /// <param name="blade2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double Sp(XGaFloat64KVector blade2)
//    {
//        return InternalKVector.Sp(blade2).ScalarValue;
//    }

//    /// <summary>
//    /// The outer product of this blade with another
//    /// </summary>
//    /// <param name="blade2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Op(PGaFloat64Blade blade2)
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Op(blade2.InternalKVector)
//        );
//    }

//    /// <summary>
//    /// The outer product of this blade with another
//    /// </summary>
//    /// <param name="blade2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Op(XGaFloat64KVector blade2)
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Op(blade2)
//        );
//    }

//    /// <summary>
//    /// The left-contraction product of this blade with another
//    /// </summary>
//    /// <param name="blade2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Lcp(PGaFloat64Blade blade2)
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Lcp(blade2.InternalKVector)
//        );
//    }
    
//    /// <summary>
//    /// The left-contraction product of this blade with another
//    /// </summary>
//    /// <param name="blade2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade ELcp(PGaFloat64Blade blade2)
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.ELcp(blade2.InternalKVector)
//        );
//    }

//    /// <summary>
//    /// The left-contraction product of this blade with another
//    /// </summary>
//    /// <param name="blade2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Lcp(XGaFloat64KVector blade2)
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Lcp(blade2)
//        );
//    }

//    /// <summary>
//    /// The right-contraction product of this blade with another
//    /// </summary>
//    /// <param name="blade2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Rcp(PGaFloat64Blade blade2)
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Rcp(blade2.InternalKVector)
//        );
//    }

//    /// <summary>
//    /// The right-contraction product of this blade with another
//    /// </summary>
//    /// <param name="blade2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Rcp(XGaFloat64KVector blade2)
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Rcp(blade2)
//        );
//    }

//    /// <summary>
//    /// The fat-dot product (PGA inner product) of this blade with another
//    /// </summary>
//    /// <param name="blade2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Fdp(PGaFloat64Blade blade2)
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Fdp(blade2.InternalKVector).GetFirstKVectorPart()
//        );
//    }

//    /// <summary>
//    /// The fat-dot product (PGA inner product) of this blade with another
//    /// </summary>
//    /// <param name="blade2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade Fdp(XGaFloat64KVector blade2)
//    {
//        return new PGaFloat64Blade(
//            GeometricSpace,
//            InternalKVector.Fdp(blade2).GetFirstKVectorPart()
//        );
//    }
    
//    /// <summary>
//    /// The geometric product of this blade with another
//    /// </summary>
//    /// <param name="mv2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public XGaFloat64Multivector Gp(PGaFloat64Blade mv2)
//    {
//        return InternalKVector.Gp(mv2.InternalKVector);
//    }

//    /// <summary>
//    /// The geometric product of this blade with a multivector
//    /// </summary>
//    /// <param name="mv2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public XGaFloat64Multivector Gp(XGaFloat64Multivector mv2)
//    {
//        return InternalKVector.Gp(mv2);
//    }
    

//    public PGaFloat64Blade GetEuclideanMeet(PGaFloat64Blade blade2)
//    {
//        var a = InternalKVector;
//        var b = blade2.InternalKVector;

//        var meetVectorsList = new List<XGaFloat64Vector>(
//            Math.Min(a.Grade, b.Grade)
//        );

//        meetVectorsList.AddRange(
//            Grade <= blade2.Grade
//                ? a.BladeToVectors().Where(vector => vector.Op(b).IsNearZero())
//                : b.BladeToVectors().Where(vector => a.Op(vector).IsNearZero())
//        );

//        return meetVectorsList
//            .Op(ConformalProcessor)
//            .ToConformalBlade(GeometricSpace);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public PGaFloat64Blade GetEuclideanJoin(PGaFloat64Blade blade2)
//    {
//        return InternalKVector
//            .BladeToVectors()
//            .Concat(blade2.InternalKVector.BladeToVectors())
//            .SpanToBlade(ConformalProcessor)
//            .ToConformalBlade(GeometricSpace);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Pair<PGaFloat64Blade> GetEuclideanMeetJoin(PGaFloat64Blade blade2)
//    {
//        var meet = 
//            GetEuclideanMeet(blade2);
        
//        var join = 
//            InternalKVector.Op(
//                meet.InternalKVector.Lcp(blade2.InternalKVector)
//            ).DivideByENorm().ToConformalBlade(GeometricSpace);

//        return new Pair<PGaFloat64Blade>(meet, join);
//    }

    
//    /// <summary>
//    /// Get the IPNS distance of this blade with another (equal to -2 * blade1.Sp(blade2)
//    /// Each of the two blades must be either a CGA vector or a pseudo-vector
//    /// </summary>
//    /// <param name="blade2"></param>
//    /// <returns></returns>
//    public double GetIpnsDistance(PGaFloat64Blade blade2)
//    {
//        Debug.Assert(
//            (IsVector || IsPseudoVector) &&
//            (blade2.IsVector || blade2.IsPseudoVector)
//        );

//        var vector1 = GeometricSpace.ZeroVectorBlade.InternalVector;
//        var vector2 = GeometricSpace.ZeroVectorBlade.InternalVector;

//        if (IsVector) 
//            vector1 = InternalVector;

//        else if (IsPseudoVector)
//            vector1 = InternalKVector.Lcp(GeometricSpace.IcInvKVector).GetVectorPart();

//        if (blade2.IsVector) 
//            vector2 = blade2.InternalVector;

//        else if (blade2.IsPseudoVector)
//            vector2 = blade2.InternalKVector.Lcp(GeometricSpace.IcInvKVector).GetVectorPart();

//        return -2 * vector1.Sp(vector2).ScalarValue;
//    }
    
//    /// <summary>
//    /// Set the weight to 1 of a CGA IPNS vector (a IPNS hyper-sphere of a hyper-plane)
//    /// </summary>
//    /// <returns></returns>
//    /// <exception cref="InvalidOperationException"></exception>
//    public PGaFloat64Blade NormalizeIpnsVector()
//    {
//        Debug.Assert(IsVector);

//        var vector = InternalVector;

//        var eoScalar = vector[0] + vector[1];

//        // IPNS vector encoding a sphere or point
//        if (!eoScalar.IsNearZero())
//            return new PGaFloat64Blade(
//                GeometricSpace, 
//                (vector / eoScalar)
//            );

//        // IPNS vector encoding a hyper-plane
//        var normal = vector.GetVectorPart((int index) => index >= 2);
//        var normalNorm = normal.Norm();

//        if (normalNorm.IsNearZero())
//            throw new InvalidOperationException();

//        var normalNormInv = 1d / normalNorm;
//        var distance = 0.5 * (vector[0] - vector[1]) * normalNormInv;

//        return new PGaFloat64Blade(
//            GeometricSpace,
//            normal.Times(normalNormInv) + distance * GeometricSpace.EiVector
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public string ToLaTeX()
//    {
//        return BasisSpecs.ToLaTeX(InternalKVector);
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public string ToLaTeX(GaFloat64GeometricSpaceBasisSpecs basisSpecs)
//    {
//        return basisSpecs.ToLaTeX(InternalKVector);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override string ToString()
//    {
//        return ToLaTeX();
//    }
//}