using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic;

/// <summary>
/// This class is a computational context for Conformal GA Geometry
/// Itt can be used to encode and manipulate the following types of geometric objects:
/// - Euclidean subspaces (scalars, vectors, 2-blades, etc.) and versors (rotations and reflections)
/// - Euclidean Projective GA flats (points, lines, planes, etc.) and versors (translations, rotations, reflections)
///   (see paper Projective Geometric Algebra as a Subalgebra of Conformal Geometric algebra)
/// - Conformal GA Directions, Tangents, Flats, and Round (see chapter 14 in Geometric Algebra for Computer Science)
///   and versors (Translation, Rotation, Uniform Scaling, and Transversion) (see chapter 16 in Geometric Algebra for Computer Science)
/// </summary>
public class CGaGeometricSpace<T> :
    GaGeometricSpace<T>
{
    public static CGaGeometricSpace4D<T> Create4D(IScalarProcessor<T> scalarProcessor)
        => CGaGeometricSpace4D<T>.Create(scalarProcessor);

    public static CGaGeometricSpace5D<T> Create5D(IScalarProcessor<T> scalarProcessor)
        => CGaGeometricSpace5D<T>.Create(scalarProcessor);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaGeometricSpace<T> Create(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
    {
        return vSpaceDimensions switch
        {
            < 4 => throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions)),
            4 => CGaGeometricSpace4D<T>.Create(scalarProcessor),
            5 => CGaGeometricSpace5D<T>.Create(scalarProcessor),
            _ => new CGaGeometricSpace<T>(scalarProcessor, vSpaceDimensions)
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
    public CGaBlade<T> OneScalarBlade { get; }

    /// <summary>
    /// The Zero Vector CGA 1-Blade
    /// </summary>
    public CGaBlade<T> ZeroVectorBlade { get; }

    /// <summary>
    /// The CGA Negative Basis 1-Blade e_{-}
    /// </summary>
    public CGaBlade<T> En { get; }

    /// <summary>
    /// The CGA Negative Basis 1-Blade e_{-} internal vector
    /// </summary>
    public XGaVector<T> EnVector
        => En.InternalVector;

    /// <summary>
    /// The CGA Positive Basis 1-Blade  e_{+}
    /// </summary>
    public CGaBlade<T> Ep { get; }

    /// <summary>
    /// The CGA Positive Basis 1-Blade  e_{+} internal vector
    /// </summary>
    public XGaVector<T> EpVector
        => Ep.InternalVector;

    /// <summary>
    /// The First EGA Basis 1-Blade e_{1}
    /// </summary>
    public CGaBlade<T> E1 { get; }

    public XGaVector<T> E1Vector
        => E1.InternalVector;

    /// <summary>
    /// The Second EGA Basis 1-Blade e_{1}
    /// </summary>
    public CGaBlade<T> E2 { get; }

    public XGaVector<T> E2Vector
        => E2.InternalVector;

    /// <summary>
    /// The CGA Origin Basis 1-Blade e_{o}
    /// </summary>
    public CGaBlade<T> Eo { get; }

    /// <summary>
    /// The CGA Origin Basis 1-Blade e_{o} internal vector
    /// </summary>
    public XGaVector<T> EoVector
        => Eo.InternalVector;

    /// <summary>
    /// The CGA Infinity Basis 1-Blade e_{\infty}
    /// </summary>
    public CGaBlade<T> Ei { get; }

    /// <summary>
    /// Half the CGA Infinity Basis 1-Blade e_{\infty} / 2
    /// </summary>
    public CGaBlade<T> EiByTwo { get; }

    /// <summary>
    /// The CGA Infinity Basis 1-Blade e_{\infty} internal vector
    /// </summary>
    public XGaVector<T> EiVector
        => Ei.InternalVector;

    /// <summary>
    /// The CGA Basis 2-Blade e_{\infty} \wedge e_{o}
    /// </summary>
    public CGaBlade<T> Eoi { get; }

    public XGaBivector<T> EoiBivector
        => Eoi.InternalBivector;

    /// <summary>
    /// The EGA Basis 2-Blade e_{1} \wedge e_{2}
    /// </summary>
    public CGaBlade<T> E12 { get; }

    public XGaBivector<T> E12Bivector
        => E12.InternalBivector;

    /// <summary>
    /// The EGA Basis Pseudo-Scalar
    /// </summary>
    public CGaBlade<T> Ie { get; }

    public XGaKVector<T> IeKVector
        => Ie.InternalKVector;

    /// <summary>
    /// The Inverse of the EGA Basis Pseudo-Scalar
    /// </summary>
    public CGaBlade<T> IeInv { get; }

    public XGaKVector<T> IeInvKVector
        => IeInv.InternalKVector;

    /// <summary>
    /// The Reverse of the EGA Basis Pseudo-Scalar
    /// </summary>
    public CGaBlade<T> IeRev { get; }

    public XGaKVector<T> IeRevKVector
        => IeRev.InternalKVector;

    /// <summary>
    /// The PGA Origin point
    /// </summary>
    public CGaBlade<T> EoIe { get; }

    public XGaKVector<T> EoIeKVector
        => EoIe.InternalKVector;

    /// <summary>
    /// The CGA Direction encoding the EGA Basis Pseudo-Scalar Subspace
    /// </summary>
    public CGaBlade<T> IeEi { get; }

    public XGaKVector<T> IeEiKVector
        => IeEi.InternalKVector;

    /// <summary>
    /// The CGA Basis Pseudo-Scalar
    /// </summary>
    public CGaBlade<T> Ic { get; }

    public XGaKVector<T> IcKVector
        => Ic.InternalKVector;

    /// <summary>
    /// The Inverse of the CGA Basis Pseudo-Scalar
    /// </summary>
    public CGaBlade<T> IcInv { get; }

    public XGaKVector<T> IcInvKVector
        => IcInv.InternalKVector;

    /// <summary>
    /// The Reverse of the CGA Basis Pseudo-Scalar
    /// </summary>
    public CGaBlade<T> IcRev { get; }

    public XGaKVector<T> IcRevKVector
        => IcRev.InternalKVector;

    
    public CGaEncoder<T> Encode { get; }

    public CGaVGaEncoder<T> EncodeVGa 
        => Encode.VGa;
    
    public CGaHGaEncoder<T> EncodeHGa 
        => Encode.HGa;

    public CGaPGaEncoder<T> EncodePGa 
        => Encode.PGa;

    public CGaOpnsDirectionEncoder<T> EncodeOpnsDirection 
        => Encode.OpnsDirection;

    public CGaOpnsTangentEncoder<T> EncodeOpnsTangent 
        => Encode.OpnsTangent;

    public CGaOpnsFlatEncoder<T> EncodeOpnsFlat 
        => Encode.OpnsFlat;

    public CGaOpnsRoundEncoder<T> EncodeOpnsRound 
        => Encode.OpnsRound;

    public CGaIpnsDirectionEncoder<T> EncodeIpnsDirection 
        => Encode.IpnsDirection;

    public CGaIpnsTangentEncoder<T> EncodeIpnsTangent 
        => Encode.IpnsTangent;

    public CGaIpnsFlatEncoder<T> EncodeIpnsFlat 
        => Encode.IpnsFlat;

    public CGaIpnsRoundEncoder<T> EncodeIpnsRound 
        => Encode.IpnsRound;


    protected CGaGeometricSpace(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
        : base(GaGeometricSpaceBasisSpecs<T>.CreateCGa(scalarProcessor, vSpaceDimensions))
    {
        if (vSpaceDimensions < 4)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        //EuclideanProcessor = XGaProcessor<T>.CreateEuclidean(scalarProcessor);
        ConformalProcessor = XGaProcessor<T>.CreateConformal(scalarProcessor);

        MusicalIsomorphism = XGaMusicalAutomorphism<T>.Create(ConformalProcessor);

        OneScalarBlade = new CGaBlade<T>(this, ConformalProcessor.ScalarOne);
        ZeroVectorBlade = new CGaBlade<T>(this, ConformalProcessor.VectorZero);

        En = new CGaBlade<T>(this, ConformalProcessor.VectorTerm(0));
        Ep = new CGaBlade<T>(this, ConformalProcessor.VectorTerm(1));
        E1 = new CGaBlade<T>(this, ConformalProcessor.VectorTerm(2));
        E2 = new CGaBlade<T>(this, ConformalProcessor.VectorTerm(3));

        var scalarOneOver2 = scalarProcessor.Divide(
            scalarProcessor.OneValue,
            scalarProcessor.TwoValue
        );

        Eo = new CGaBlade<T>(this, ConformalProcessor.Vector(scalarOneOver2, scalarOneOver2));
        Ei = new CGaBlade<T>(this, ConformalProcessor.Vector(ScalarProcessor.OneValue, ScalarProcessor.MinusOneValue));
        EiByTwo = Ei / 2;
        Eoi = Eo.Op(Ei);

        E12 = new CGaBlade<T>(this, ConformalProcessor.BivectorTerm(2, 3));

        Ie = new CGaBlade<T>(this, ConformalProcessor.KVectorTerm((VSpaceDimensions - 2).GetRange(2).ToImmutableArray()));
        IeInv = Ie.Inverse();
        IeRev = Ie.Reverse();

        EoIe = Eo.Op(Ie);
        IeEi = Ie.Op(Ei);

        Ic = new CGaBlade<T>(this, ConformalProcessor.KVectorTerm(VSpaceDimensions.GetRange().ToImmutableArray()));
        IcInv = Ic.Inverse();
        IcRev = Ic.Reverse();

        Encode = new CGaEncoder<T>(this);
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
    public bool IsValidVGaElement(XGaMultivector<T> mv)
    {
        if (!IsValidElement(mv)) return false;

        // EGA elements only contain Euclidean basis vectors, but never E-, E+
        const int enIndex = 0;
        const int epIndex = 1;

        return mv.IsZero ||
               mv.Ids.All(id => !id.SetContains(enIndex) && !id.SetContains(epIndex));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidPGaElement(XGaMultivector<T> mv)
    {
        if (!IsValidElement(mv)) return false;

        // PGA (= CGAo) elements only contain Eo and Euclidean basis vectors, but never Ei
        var eiIndex = VSpaceDimensions - 1;

        return mv.IsZero ||
               BasisSpecs.BasisMap.OmMap(mv).Ids.All(id => !id.SetContains(eiIndex));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidCGaInfElement(XGaMultivector<T> mv)
    {
        if (!IsValidElement(mv)) return false;

        // CGAi elements only contain Ei and Euclidean basis vectors, but never Eo
        const int eoIndex = 0;

        return mv.IsZero ||
               BasisSpecs.BasisMap.OmMap(mv).Ids.All(id => !id.SetContains(eoIndex));
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaKVector<T> RemoveEi(XGaKVector<T> kVector)
    {
        //var eiIdMask = (1UL << (VSpaceDimensions - 1)) - 1UL;
        var eiIndex = VSpaceDimensions - 1;

        var termList = 
            BasisSpecs
                .BasisMap
                .OmMap(kVector)
                .IdScalarPairs
                .Where(term => 
                    term.Key.SetContains(eiIndex)
                ).Select(term => 
                    new KeyValuePair<IndexSet, T>(
                        term.Key.Remove(eiIndex), 
                        term.Value
                    )
                );

        return BasisSpecs.BasisMapInverse.OmMap(
            ConformalProcessor
                .CreateComposer()
                .AddTerms(termList)
                .GetKVector(kVector.Grade - 1)
        );

        //return BasisSpecs.BasisMapInverse.OmMap(
        //    BasisSpecs
        //        .BasisMap
        //        .OmMap(InternalKVector)
        //        .MapBasisBlades(id => id.Remove(eiIndex))
        //        .GetFirstKVectorPart()
        //);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(LinVector2D<T> vector)
    {
        return BasisSpecs.ToLaTeX(
            Encode.VGa.Vector(vector).InternalKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(LinVector3D<T> vector)
    {
        return BasisSpecs.ToLaTeX(
            Encode.VGa.Vector(vector).InternalKVector
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
            Encode.VGa.Bivector(vector).InternalKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(LinBivector3D<T> vector)
    {
        return BasisSpecs.ToLaTeX(
            Encode.VGa.Bivector(vector).InternalKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(LinTrivector3D<T> vector)
    {
        return BasisSpecs.ToLaTeX(
            Encode.VGa.Trivector(vector.Scalar123).InternalKVector
        );
    }
}