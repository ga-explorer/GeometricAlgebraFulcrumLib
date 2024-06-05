using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal;

/// <summary>
/// This class is a computational context for Conformal GA Geometry
/// Itt can be used to encode and manipulate the following types of geometric objects:
/// - Euclidean subspaces (scalars, vectors, 2-blades, etc.) and versors (rotations and reflections)
/// - Euclidean Projective GA flats (points, lines, planes, etc.) and versors (translations, rotations, reflections)
///   (see paper Projective Geometric Algebra as a Subalgebra of Conformal Geometric algebra)
/// - Conformal GA Directions, Tangents, Flats, and Round (see chapter 14 in Geometric Algebra for Computer Science)
///   and versors (Translation, Rotation, Uniform Scaling, and Transversion) (see chapter 16 in Geometric Algebra for Computer Science)
/// </summary>
public class XGaConformalSpace<T> :
    XGaGeometrySpace<T>
{
    public static XGaConformalSpace4D<T> Create4D(IScalarProcessor<T> scalarProcessor) 
        => XGaConformalSpace4D<T>.Create(scalarProcessor);

    public static XGaConformalSpace5D<T> Create5D(IScalarProcessor<T> scalarProcessor) 
        => XGaConformalSpace5D<T>.Create(scalarProcessor);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalSpace<T> Create(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
    {
        return vSpaceDimensions switch
        {
            < 4 => throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions)),
            4 => XGaConformalSpace4D<T>.Create(scalarProcessor),
            5 => XGaConformalSpace5D<T>.Create(scalarProcessor),
            _ => new XGaConformalSpace<T>(scalarProcessor, vSpaceDimensions)
        };
    }


    public XGaConformalProcessor<T> ConformalProcessor { get; }

    /// <summary>
    /// This isomorphism is used for converting CGA multivectors to and from PGA subspace
    /// </summary>
    public XGaMusicalAutomorphism<T> MusicalIsomorphism { get; }

    public bool Is4D 
        => VSpaceDimensions == 4;
    
    public bool Is5D 
        => VSpaceDimensions == 5;
    
    /// <summary>
    /// The Unit Scalar CGA 0-Blade
    /// </summary>
    public XGaConformalBlade<T> OneScalarBlade { get; }

    /// <summary>
    /// The Zero Vector CGA 1-Blade
    /// </summary>
    public XGaConformalBlade<T> ZeroVectorBlade { get; }
    
    /// <summary>
    /// The CGA Negative Basis 1-Blade e_{-}
    /// </summary>
    public XGaConformalBlade<T> En { get; }

    /// <summary>
    /// The CGA Negative Basis 1-Blade e_{-} internal vector
    /// </summary>
    public XGaVector<T> EnVector 
        => En.InternalVector;

    /// <summary>
    /// The CGA Positive Basis 1-Blade  e_{+}
    /// </summary>
    public XGaConformalBlade<T> Ep { get; }
    
    /// <summary>
    /// The CGA Positive Basis 1-Blade  e_{+} internal vector
    /// </summary>
    public XGaVector<T> EpVector 
        => Ep.InternalVector;

    /// <summary>
    /// The First EGA Basis 1-Blade e_{1}
    /// </summary>
    public XGaConformalBlade<T> E1 { get; }
    
    public XGaVector<T> E1Vector 
        => E1.InternalVector;

    /// <summary>
    /// The Second EGA Basis 1-Blade e_{1}
    /// </summary>
    public XGaConformalBlade<T> E2 { get; }
    
    public XGaVector<T> E2Vector 
        => E2.InternalVector;

    /// <summary>
    /// The CGA Origin Basis 1-Blade e_{o}
    /// </summary>
    public XGaConformalBlade<T> Eo { get; }
    
    /// <summary>
    /// The CGA Origin Basis 1-Blade e_{o} internal vector
    /// </summary>
    public XGaVector<T> EoVector 
        => Eo.InternalVector;

    /// <summary>
    /// The CGA Infinity Basis 1-Blade e_{\infty}
    /// </summary>
    public XGaConformalBlade<T> Ei { get; }
    
    /// <summary>
    /// Half the CGA Infinity Basis 1-Blade e_{\infty} / 2
    /// </summary>
    public XGaConformalBlade<T> EiByTwo { get; }

    /// <summary>
    /// The CGA Infinity Basis 1-Blade e_{\infty} internal vector
    /// </summary>
    public XGaVector<T> EiVector 
        => Ei.InternalVector;

    /// <summary>
    /// The CGA Basis 2-Blade e_{\infty} \wedge e_{o}
    /// </summary>
    public XGaConformalBlade<T> Eoi { get; }
    
    public XGaBivector<T> EoiBivector 
        => Eoi.InternalBivector;

    /// <summary>
    /// The EGA Basis 2-Blade e_{1} \wedge e_{2}
    /// </summary>
    public XGaConformalBlade<T> E12 { get; }
    
    public XGaBivector<T> E12Bivector 
        => E12.InternalBivector;

    /// <summary>
    /// The EGA Basis Pseudo-Scalar
    /// </summary>
    public XGaConformalBlade<T> Ie { get; }
    
    public XGaKVector<T> IeKVector 
        => Ie.InternalKVector;

    /// <summary>
    /// The Inverse of the EGA Basis Pseudo-Scalar
    /// </summary>
    public XGaConformalBlade<T> IeInv { get; }
    
    public XGaKVector<T> IeInvKVector 
        => IeInv.InternalKVector;

    /// <summary>
    /// The Reverse of the EGA Basis Pseudo-Scalar
    /// </summary>
    public XGaConformalBlade<T> IeRev { get; }
    
    public XGaKVector<T> IeRevKVector 
        => IeRev.InternalKVector;

    /// <summary>
    /// The PGA Origin point
    /// </summary>
    public XGaConformalBlade<T> EoIe { get; }
    
    public XGaKVector<T> EoIeKVector 
        => EoIe.InternalKVector;

    /// <summary>
    /// The CGA Direction encoding the EGA Basis Pseudo-Scalar Subspace
    /// </summary>
    public XGaConformalBlade<T> IeEi { get; }
    
    public XGaKVector<T> IeEiKVector 
        => IeEi.InternalKVector;

    /// <summary>
    /// The CGA Basis Pseudo-Scalar
    /// </summary>
    public XGaConformalBlade<T> Ic { get; }
    
    public XGaKVector<T> IcKVector 
        => Ic.InternalKVector;

    /// <summary>
    /// The Inverse of the CGA Basis Pseudo-Scalar
    /// </summary>
    public XGaConformalBlade<T> IcInv { get; }
    
    public XGaKVector<T> IcInvKVector 
        => IcInv.InternalKVector;

    /// <summary>
    /// The Reverse of the CGA Basis Pseudo-Scalar
    /// </summary>
    public XGaConformalBlade<T> IcRev { get; }
    
    public XGaKVector<T> IcRevKVector 
        => IcRev.InternalKVector;
    

    protected XGaConformalSpace(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
        : base(XGaGeometrySpaceBasisSpecs<T>.CreateCGa(scalarProcessor, vSpaceDimensions))
    {
        if (vSpaceDimensions < 4)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        //EuclideanProcessor = XGaProcessor<T>.CreateEuclidean(scalarProcessor);
        ConformalProcessor = XGaProcessor<T>.CreateConformal(scalarProcessor);

        MusicalIsomorphism = XGaMusicalAutomorphism<T>.Create(ConformalProcessor);

        OneScalarBlade = new XGaConformalBlade<T>(this, ConformalProcessor.ScalarOne);
        ZeroVectorBlade = new XGaConformalBlade<T>(this, ConformalProcessor.VectorZero);

        En = new XGaConformalBlade<T>(this, ConformalProcessor.VectorTerm(0));
        Ep = new XGaConformalBlade<T>(this, ConformalProcessor.VectorTerm(1));
        E1 = new XGaConformalBlade<T>(this, ConformalProcessor.VectorTerm(2));
        E2 = new XGaConformalBlade<T>(this, ConformalProcessor.VectorTerm(3));

        var scalarOneOver2 = scalarProcessor.Divide(
            scalarProcessor.OneValue,
            scalarProcessor.TwoValue
        );

        Eo = new XGaConformalBlade<T>(this, ConformalProcessor.Vector(scalarOneOver2, scalarOneOver2));
        Ei = new XGaConformalBlade<T>(this, ConformalProcessor.Vector(ScalarProcessor.OneValue, ScalarProcessor.MinusOneValue));
        EiByTwo = Ei / 2;
        Eoi = Eo.Op(Ei);

        E12 = new XGaConformalBlade<T>(this, ConformalProcessor.BivectorTerm(2, 3));

        Ie = new XGaConformalBlade<T>(this, ConformalProcessor.KVectorTerm((VSpaceDimensions - 2).GetRange(2).ToImmutableArray()));
        IeInv = Ie.Inverse();
        IeRev = Ie.Reverse();

        EoIe = Eo.Op(Ie);
        IeEi = Ie.Op(Ei);

        Ic = new XGaConformalBlade<T>(this, ConformalProcessor.KVectorTerm(VSpaceDimensions.GetRange().ToImmutableArray()));
        IcInv = Ic.Inverse();
        IcRev = Ic.Reverse();
    }


    protected IEnumerable<string> GetCGaVectorSubscripts(bool orthogonalBasis = false)
    {
        if (orthogonalBasis)
        {
            yield return "+";

            for (var i = 0; i < VSpaceDimensions - 2; i++)
                yield return (i + 1).ToString();

            yield return "-";
        }
        else
        {
            yield return "o";

            for (var i = 0; i < VSpaceDimensions - 2; i++)
                yield return (i + 1).ToString();

            yield return @"\infty";
        }
    }

    protected XGaLinearMapOutermorphism<T> GetCGaBasisMap()
    {
        // If linearly independent basis F = <f1, f2, f3> is related to
        // orthonormal basis E = <e1, e2, e3> via matrix M (F = M E), then
        // the scalars of a multivector expressed using E (Ae) are related
        // to the scalars of the same multivectors expressed using basis F
        // (Af) using the inverse transpose of M: Af = inv(transpose(M)) Ae
        // Thus if M is an orthogonal matrix (or as a special case, a permutation)
        // they are related using M itself: Af = M Ae.

        var vectorMapArray = new T[VSpaceDimensions, VSpaceDimensions];

        for (var i = 0; i < VSpaceDimensions - 2; i++)
            vectorMapArray[i + 1, i + 2] = ScalarProcessor.OneValue;

        vectorMapArray[0, 0] = ScalarProcessor.OneValue;
        vectorMapArray[0, 1] = ScalarProcessor.OneValue;

        vectorMapArray[VSpaceDimensions - 1, 0] = ScalarProcessor.Divide(ScalarProcessor.OneValue, ScalarProcessor.TwoValue).ScalarValue;
        vectorMapArray[VSpaceDimensions - 1, 1] = ScalarProcessor.Divide(ScalarProcessor.MinusOneValue, ScalarProcessor.TwoValue).ScalarValue;

        return vectorMapArray
            .ColumnsToLinVectors(ScalarProcessor)
            .ToLinUnilinearMap(ScalarProcessor)
            .ToOutermorphism(Processor);
    }

    protected XGaLinearMapOutermorphism<T> GetCGaBasisMapInverse()
    {
        // If linearly independent basis F = <f1, f2, f3> is related to
        // orthonormal basis E = <e1, e2, e3> via matrix M (F = M E), then
        // the scalars of a multivector expressed using E (Ae) are related
        // to the scalars of the same multivectors expressed using basis F
        // (Af) using the inverse transpose of M: Af = inv(transpose(M)) Ae
        // Thus if M is an orthogonal matrix (or as a special case, a permutation)
        // they are related using M itself: Af = M Ae.

        var vectorMapArray = new T[VSpaceDimensions, VSpaceDimensions];

        for (var i = 0; i < VSpaceDimensions - 2; i++)
            vectorMapArray[i + 2, i + 1] = ScalarProcessor.OneValue;

        vectorMapArray[0, 0] = ScalarProcessor.Divide(ScalarProcessor.OneValue, ScalarProcessor.TwoValue).ScalarValue;
        vectorMapArray[1, 0] = ScalarProcessor.Divide(ScalarProcessor.OneValue, ScalarProcessor.TwoValue).ScalarValue;

        vectorMapArray[0, VSpaceDimensions - 1] = ScalarProcessor.OneValue;
        vectorMapArray[1, VSpaceDimensions - 1] = ScalarProcessor.MinusOneValue;

        return vectorMapArray
            .ColumnsToLinVectors(ScalarProcessor)
            .ToLinUnilinearMap(ScalarProcessor)
            .ToOutermorphism(Processor);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidEGaElement(XGaMultivector<T> mv)
    {
        if (!IsValidElement(mv)) return false;

        // EGA elements only contain Euclidean basis vectors, but never E-, E+
        const int enIndex = 0;
        const int epIndex = 1;

        return mv.IsZero ||
               mv.Ids.All(id => !id.Contains(enIndex) && !id.Contains(epIndex));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidPGaElement(XGaMultivector<T> mv)
    {
        if (!IsValidElement(mv)) return false;

        // PGA (= CGAo) elements only contain Eo and Euclidean basis vectors, but never Ei
        var eiIndex = VSpaceDimensions - 1;

        return mv.IsZero ||
               BasisSpecs.BasisMap.OmMap(mv).Ids.All(id => !id.Contains(eiIndex));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidCGaInfElement(XGaMultivector<T> mv)
    {
        if (!IsValidElement(mv)) return false;

        // CGAi elements only contain Ei and Euclidean basis vectors, but never Eo
        const int eoIndex = 0;

        return mv.IsZero ||
               BasisSpecs.BasisMap.OmMap(mv).Ids.All(id => !id.Contains(eoIndex));
    }

    
    /// <summary>
    /// Create a CGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> EncodeScalar(int s)
    {
        return new XGaConformalBlade<T>(
            this,
            Processor.Scalar(s)
        );
    }

    /// <summary>
    /// Create a CGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> EncodeScalar(Scalar<T> s)
    {
        return new XGaConformalBlade<T>(
            this,
            Processor.Scalar(s)
        );
    }
    
    /// <summary>
    /// Create a CGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> EncodeScalar(T s)
    {
        return new XGaConformalBlade<T>(
            this,
            Processor.Scalar(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(LinVector2D<T> vector)
    {
        return BasisSpecs.ToLaTeX(
            this.EncodeEGaVector(vector).InternalKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(LinVector3D<T> vector)
    {
        return BasisSpecs.ToLaTeX(
            this.EncodeEGaVector(vector).InternalKVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(LinVector<T> vector)
    {
        return BasisSpecs.ToLaTeX(
            vector.ToXGaVector(Processor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(LinBivector2D<T> vector)
    {
        return BasisSpecs.ToLaTeX(
            this.EncodeEGaBivector(vector).InternalKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(LinBivector3D<T> vector)
    {
        return BasisSpecs.ToLaTeX(
            this.EncodeEGaBivector(vector).InternalKVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(LinTrivector3D<T> vector)
    {
        return BasisSpecs.ToLaTeX(
            this.EncodeEGaTrivector(vector.Scalar123).InternalKVector
        );
    }
}