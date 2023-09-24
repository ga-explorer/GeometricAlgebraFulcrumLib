using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal;

/// <summary>
/// This class is a computational context for Conformal GA Geometry
/// Itt can be used to encode and manipulate the following types of geometric objects:
/// - Euclidean subspaces (scalars, vectors, 2-blades, etc.) and versors (rotations and reflections)
/// - Euclidean Projective GA flats (points, lines, planes, etc.) and versors (translations, rotations, reflections)
///   (see paper Projective Geometric Algebra as a Subalgebra of Conformal Geometric algebra)
/// - Conformal GA Directions, Tangents, Flats, and Round (see chapter 14 in Geometric Algebra for Computer Science)
///   and versors (Translation, Rotation, Uniform Scaling, and Transversion) (see chapter 16 in Geometric Algebra for Computer Science)
/// </summary>
public class RGaConformalSpace :
    RGaGeometrySpace
{
    public static RGaConformalSpace4D Space4D 
        => RGaConformalSpace4D.Instance;

    public static RGaConformalSpace5D Space5D 
        => RGaConformalSpace5D.Instance;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalSpace Create(int vSpaceDimensions)
    {
        return vSpaceDimensions switch
        {
            < 4 => throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions)),
            4 => RGaConformalSpace4D.Instance,
            5 => RGaConformalSpace5D.Instance,
            _ => new RGaConformalSpace(vSpaceDimensions)
        };
    }


    public RGaFloat64EuclideanProcessor EuclideanProcessor
        => RGaFloat64EuclideanProcessor.Instance;

    public RGaFloat64ConformalProcessor ConformalProcessor
        => RGaFloat64ConformalProcessor.Instance;

    public override RGaFloat64Processor Processor
        => ConformalProcessor;

    /// <summary>
    /// This isomorphism is used for converting CGA multivectors to and from PGA subspace
    /// </summary>
    public RGaFloat64MusicalAutomorphism MusicalIsomorphism
        => RGaFloat64MusicalAutomorphism.Instance;

    public override IReadOnlyList<string> LaTeXVectorSubscripts { get; }

    public override IRGaFloat64Outermorphism LaTeXBasisMap { get; }

    public override IRGaFloat64Outermorphism LaTeXBasisMapInverse { get; }

    public bool Is4D 
        => VSpaceDimensions == 4;
    
    public bool Is5D 
        => VSpaceDimensions == 5;
    
    /// <summary>
    /// The Unit Scalar CGA 0-Blade
    /// </summary>
    public RGaConformalBlade OneScalarBlade { get; }

    /// <summary>
    /// The Zero Vector CGA 1-Blade
    /// </summary>
    public RGaConformalBlade ZeroVectorBlade { get; }
    
    /// <summary>
    /// The CGA Negative Basis 1-Blade e_{-}
    /// </summary>
    public RGaConformalBlade En { get; }

    /// <summary>
    /// The CGA Negative Basis 1-Blade e_{-} internal vector
    /// </summary>
    public RGaFloat64Vector EnVector 
        => En.InternalVector;

    /// <summary>
    /// The CGA Positive Basis 1-Blade  e_{+}
    /// </summary>
    public RGaConformalBlade Ep { get; }
    
    /// <summary>
    /// The CGA Positive Basis 1-Blade  e_{+} internal vector
    /// </summary>
    public RGaFloat64Vector EpVector 
        => Ep.InternalVector;

    /// <summary>
    /// The First EGA Basis 1-Blade e_{1}
    /// </summary>
    public RGaConformalBlade E1 { get; }
    
    public RGaFloat64Vector E1Vector 
        => E1.InternalVector;

    /// <summary>
    /// The Second EGA Basis 1-Blade e_{1}
    /// </summary>
    public RGaConformalBlade E2 { get; }
    
    public RGaFloat64Vector E2Vector 
        => E2.InternalVector;

    /// <summary>
    /// The CGA Origin Basis 1-Blade e_{o}
    /// </summary>
    public RGaConformalBlade Eo { get; }
    
    /// <summary>
    /// The CGA Origin Basis 1-Blade e_{o} internal vector
    /// </summary>
    public RGaFloat64Vector EoVector 
        => Eo.InternalVector;

    /// <summary>
    /// The CGA Infinity Basis 1-Blade e_{\infty}
    /// </summary>
    public RGaConformalBlade Ei { get; }
    
    /// <summary>
    /// The CGA Infinity Basis 1-Blade e_{\infty} internal vector
    /// </summary>
    public RGaFloat64Vector EiVector 
        => Ei.InternalVector;

    /// <summary>
    /// The CGA Basis 2-Blade e_{\infty} \wedge e_{o}
    /// </summary>
    public RGaConformalBlade Eoi { get; }
    
    public RGaFloat64Bivector EoiBivector 
        => Eoi.InternalBivector;

    /// <summary>
    /// The EGA Basis 2-Blade e_{1} \wedge e_{2}
    /// </summary>
    public RGaConformalBlade E12 { get; }
    
    public RGaFloat64Bivector E12Bivector 
        => E12.InternalBivector;

    /// <summary>
    /// The EGA Basis Pseudo-Scalar
    /// </summary>
    public RGaConformalBlade Ie { get; }
    
    public RGaFloat64KVector IeKVector 
        => Ie.InternalKVector;

    /// <summary>
    /// The Inverse of the EGA Basis Pseudo-Scalar
    /// </summary>
    public RGaConformalBlade IeInv { get; }
    
    public RGaFloat64KVector IeInvKVector 
        => IeInv.InternalKVector;

    /// <summary>
    /// The Reverse of the EGA Basis Pseudo-Scalar
    /// </summary>
    public RGaConformalBlade IeRev { get; }
    
    public RGaFloat64KVector IeRevKVector 
        => IeRev.InternalKVector;

    /// <summary>
    /// The PGA Origin point
    /// </summary>
    public RGaConformalBlade EoIe { get; }
    
    public RGaFloat64KVector EoIeKVector 
        => EoIe.InternalKVector;

    /// <summary>
    /// The CGA Direction encoding the EGA Basis Pseudo-Scalar Subspace
    /// </summary>
    public RGaConformalBlade IeEi { get; }
    
    public RGaFloat64KVector IeEiKVector 
        => IeEi.InternalKVector;

    /// <summary>
    /// The CGA Basis Pseudo-Scalar
    /// </summary>
    public RGaConformalBlade Ic { get; }
    
    public RGaFloat64KVector IcKVector 
        => Ic.InternalKVector;

    /// <summary>
    /// The Inverse of the CGA Basis Pseudo-Scalar
    /// </summary>
    public RGaConformalBlade IcInv { get; }
    
    public RGaFloat64KVector IcInvKVector 
        => IcInv.InternalKVector;

    /// <summary>
    /// The Reverse of the CGA Basis Pseudo-Scalar
    /// </summary>
    public RGaConformalBlade IcRev { get; }
    
    public RGaFloat64KVector IcRevKVector 
        => IcRev.InternalKVector;
    

    protected RGaConformalSpace(int vSpaceDimensions)
        : base(vSpaceDimensions)
    {
        if (vSpaceDimensions < 4)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        LaTeXVectorSubscripts = GetCGaVectorSubscripts().ToImmutableArray();
        LaTeXBasisMap = GetCGaBasisMap();
        LaTeXBasisMapInverse = GetCGaBasisMapInverse();
        
        OneScalarBlade = new RGaConformalBlade(this, ConformalProcessor.CreateOneScalar());
        ZeroVectorBlade = new RGaConformalBlade(this, ConformalProcessor.CreateZeroVector());

        En = new RGaConformalBlade(this, ConformalProcessor.CreateTermVector(0));
        Ep = new RGaConformalBlade(this, ConformalProcessor.CreateTermVector(1));
        E1 = new RGaConformalBlade(this, ConformalProcessor.CreateTermVector(2));
        E2 = new RGaConformalBlade(this, ConformalProcessor.CreateTermVector(3));

        Eo = new RGaConformalBlade(this, ConformalProcessor.CreateVector(0.5d, 0.5d));
        Ei = new RGaConformalBlade(this, ConformalProcessor.CreateVector(1d, -1d));
        Eoi = Eo.Op(Ei);

        E12 = new RGaConformalBlade(this, ConformalProcessor.CreateTermBivector(2, 3));

        Ie = new RGaConformalBlade(this, ConformalProcessor.CreateTermKVector((VSpaceDimensions - 2).GetRange(2).ToImmutableArray()));
        IeInv = Ie.Inverse();
        IeRev = Ie.Reverse();

        EoIe = Eo.Op(Ie);
        IeEi = Ie.Op(Ei);

        Ic = new RGaConformalBlade(this, ConformalProcessor.CreateTermKVector(VSpaceDimensions.GetRange().ToImmutableArray()));
        IcInv = Ic.Inverse();
        IcRev = Ic.Reverse();
    }


    protected IEnumerable<string> GetCGaVectorSubscripts()
    {
        yield return "o";

        for (var i = 0; i < VSpaceDimensions - 2; i++)
            yield return (i + 1).ToString();

        yield return @"\infty";
    }

    protected RGaFloat64LinearMapOutermorphism GetCGaBasisMap()
    {
        // If linearly independent basis F = <f1, f2, f3> is related to
        // orthonormal basis E = <e1, e2, e3> via matrix M (F = M E), then
        // the scalars of a multivector expressed using E (Ae) are related
        // to the scalars of the same multivectors expressed using basis F
        // (Af) using the inverse transpose of M: Af = inv(transpose(M)) Ae
        // Thus if M is an orthogonal matrix (or as a special case, a permutation)
        // they are related using M itself: Af = M Ae.

        var vectorMapArray = new double[VSpaceDimensions, VSpaceDimensions];

        for (var i = 0; i < VSpaceDimensions - 2; i++)
            vectorMapArray[i + 1, i + 2] = 1d;

        vectorMapArray[0, 0] = 1d;
        vectorMapArray[0, 1] = 1d;

        vectorMapArray[VSpaceDimensions - 1, 0] = 0.5d;
        vectorMapArray[VSpaceDimensions - 1, 1] = -0.5d;

        return vectorMapArray
            .ColumnsToLinVectors()
            .ToLinUnilinearMap()
            .ToOutermorphism(Processor);
    }

    protected RGaFloat64LinearMapOutermorphism GetCGaBasisMapInverse()
    {
        // If linearly independent basis F = <f1, f2, f3> is related to
        // orthonormal basis E = <e1, e2, e3> via matrix M (F = M E), then
        // the scalars of a multivector expressed using E (Ae) are related
        // to the scalars of the same multivectors expressed using basis F
        // (Af) using the inverse transpose of M: Af = inv(transpose(M)) Ae
        // Thus if M is an orthogonal matrix (or as a special case, a permutation)
        // they are related using M itself: Af = M Ae.

        var vectorMapArray = new double[VSpaceDimensions, VSpaceDimensions];

        for (var i = 0; i < VSpaceDimensions - 2; i++)
            vectorMapArray[i + 2, i + 1] = 1d;

        vectorMapArray[0, 0] = 0.5d;
        vectorMapArray[1, 0] = 0.5d;

        vectorMapArray[0, VSpaceDimensions - 1] = 1d;
        vectorMapArray[1, VSpaceDimensions - 1] = -1d;

        return vectorMapArray
            .ColumnsToLinVectors()
            .ToLinUnilinearMap()
            .ToOutermorphism(Processor);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidEGaElement(RGaFloat64Multivector mv)
    {
        if (!IsValidElement(mv)) return false;

        // EGA elements only contain Euclidean basis vectors, but never E-, E+
        const ulong maskEnp = 3UL;

        return mv.IsZero ||
               mv.Ids.All(id => (id & maskEnp) == 0);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidPGaElement(RGaFloat64Multivector mv)
    {
        if (!IsValidElement(mv)) return false;

        // PGA (= CGAo) elements only contain Eo and Euclidean basis vectors, but never Ei
        var maskEi = 1UL << VSpaceDimensions - 1;

        return mv.IsZero ||
               LaTeXBasisMap.OmMap(mv).Ids.All(id => (id & maskEi) == 0);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidCGaInfElement(RGaFloat64Multivector mv)
    {
        if (!IsValidElement(mv)) return false;

        // CGAi elements only contain Ei and Euclidean basis vectors, but never Eo
        const ulong maskEo = 1UL;

        return mv.IsZero ||
               LaTeXBasisMap.OmMap(mv).Ids.All(id => (id & maskEo) == 0);
    }


    /// <summary>
    /// Create a CGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade EncodeScalar(double s)
    {
        return new RGaConformalBlade(
            this,
            Processor.CreateScalar(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(Float64Vector2D vector)
    {
        return ToLaTeX(
            this.EncodeEGaVector(vector).InternalKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(Float64Vector3D vector)
    {
        return ToLaTeX(
            this.EncodeEGaVector(vector).InternalKVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(Float64Vector vector)
    {
        return ToLaTeX(
            vector.ToRGaFloat64Vector()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(Float64Bivector2D vector)
    {
        return ToLaTeX(
            this.EncodeEGaBivector(vector).InternalKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(Float64Bivector3D vector)
    {
        return ToLaTeX(
            this.EncodeEGaBivector(vector).InternalKVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(Float64Trivector3D vector)
    {
        return ToLaTeX(
            this.EncodeEGaTrivector(vector.Scalar123).InternalKVector
        );
    }
}