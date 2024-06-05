using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective;

/// <summary>
/// This class is a computational context for Projective GA Geometry
/// It can be used to encode and manipulate the following types of geometric objects:
/// - Euclidean subspaces (scalars, vectors, 2-blades, etc.) and versors (rotations and reflections)
/// - Euclidean Projective GA flats (points, lines, planes, etc.) and versors (translations, rotations, reflections)
/// - Projective GA Directions and Flats and versors (Translation, Rotation)
/// </summary>
public class XGaProjectiveSpace<T> :
    XGaGeometrySpace<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveSpace3D<T> Create4D(IScalarProcessor<T> scalarProcessor)
    {
        return XGaProjectiveSpace3D<T>.Create(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveSpace4D<T> Create5D(IScalarProcessor<T> scalarProcessor)
    {
        return XGaProjectiveSpace4D<T>.Create(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveSpace<T> Create(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
    {
        return vSpaceDimensions switch
        {
            < 3 => throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions)),
            3 => XGaProjectiveSpace3D<T>.Create(scalarProcessor),
            4 => XGaProjectiveSpace4D<T>.Create(scalarProcessor),
            _ => new XGaProjectiveSpace<T>(scalarProcessor, vSpaceDimensions)
        };
    }


    public XGaProjectiveProcessor<T> ProjectiveProcessor { get; }

    public bool Is3D 
        => VSpaceDimensions == 3;
    
    public bool Is4D 
        => VSpaceDimensions == 4;
    
    /// <summary>
    /// The Unit Scalar PGA 0-Blade
    /// </summary>
    public XGaProjectiveBlade<T> OneScalarBlade { get; }

    /// <summary>
    /// The Zero Vector PGA 1-Blade
    /// </summary>
    public XGaProjectiveBlade<T> ZeroVectorBlade { get; }
    
    /// <summary>
    /// The PGA zero signature Basis 1-Blade e_{o}
    /// </summary>
    public XGaProjectiveBlade<T> Eo { get; }

    /// <summary>
    /// The PGA zero signature Basis 1-Blade e_{o} internal vector
    /// </summary>
    public XGaVector<T> EoVector 
        => Eo.InternalVector;

    /// <summary>
    /// The First EGA Basis 1-Blade e_{1}
    /// </summary>
    public XGaProjectiveBlade<T> E1 { get; }
    
    public XGaVector<T> E1Vector 
        => E1.InternalVector;

    /// <summary>
    /// The Second EGA Basis 1-Blade e_{1}
    /// </summary>
    public XGaProjectiveBlade<T> E2 { get; }
    
    public XGaVector<T> E2Vector 
        => E2.InternalVector;

    /// <summary>
    /// The EGA Basis 2-Blade e_{1} \wedge e_{2}
    /// </summary>
    public XGaProjectiveBlade<T> E12 { get; }
    
    public XGaBivector<T> E12Bivector 
        => E12.InternalBivector;

    /// <summary>
    /// The EGA Basis Pseudo-Scalar
    /// </summary>
    public XGaProjectiveBlade<T> Ie { get; }
    
    public XGaKVector<T> IeKVector 
        => Ie.InternalKVector;

    /// <summary>
    /// The Inverse of the EGA Basis Pseudo-Scalar
    /// </summary>
    public XGaProjectiveBlade<T> IeInv { get; }
    
    public XGaKVector<T> IeInvKVector 
        => IeInv.InternalKVector;

    /// <summary>
    /// The Reverse of the EGA Basis Pseudo-Scalar
    /// </summary>
    public XGaProjectiveBlade<T> IeRev { get; }
    
    public XGaKVector<T> IeRevKVector 
        => IeRev.InternalKVector;


    protected XGaProjectiveSpace(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
        : base(XGaGeometrySpaceBasisSpecs<T>.CreateCGa(scalarProcessor, vSpaceDimensions))
    {
        if (vSpaceDimensions < 3)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        ProjectiveProcessor = XGaProcessor<T>.CreateProjective(scalarProcessor);

        OneScalarBlade = new XGaProjectiveBlade<T>(this, ProjectiveProcessor.ScalarOne);
        ZeroVectorBlade = new XGaProjectiveBlade<T>(this, ProjectiveProcessor.VectorZero);

        Eo = new XGaProjectiveBlade<T>(this, ProjectiveProcessor.VectorTerm(0));
        E1 = new XGaProjectiveBlade<T>(this, ProjectiveProcessor.VectorTerm(1));
        E2 = new XGaProjectiveBlade<T>(this, ProjectiveProcessor.VectorTerm(2));

        E12 = new XGaProjectiveBlade<T>(this, ProjectiveProcessor.BivectorTerm(1, 2));

        Ie = new XGaProjectiveBlade<T>(this, ProjectiveProcessor.KVectorTerm((VSpaceDimensions - 1).GetRange(1).ToImmutableArray()));
        IeInv = Ie.Inverse();
        IeRev = Ie.Reverse();
    }


    protected IEnumerable<string> GetPGaVectorSubscripts()
    {
        yield return "o";

        for (var i = 1; i < VSpaceDimensions; i++)
            yield return i.ToString();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidEGaElement(XGaMultivector<T> mv)
    {
        if (!IsValidElement(mv)) return false;

        // EGA elements only contain Euclidean basis vectors, but never Eo
        const int eoIndex = 0;

        return mv.IsZero ||
               mv.Ids.All(id => !id.Contains(eoIndex));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidPGaPoint(XGaKVector<T> mv)
    {
        var id = Ie.InternalKVector.Ids.First();
        
        return mv.Grade == VSpaceDimensions - 1 && 
               mv.GetBasisBladeScalar(id).IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidPGaPoint(XGaMultivector<T> mv)
    {
        var id = Ie.InternalKVector.Ids.First();
        
        return mv.IsKVector(VSpaceDimensions - 1) && 
               mv.GetBasisBladeScalar(id).IsNotZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidPGaVector(XGaKVector<T> mv)
    {
        var id = Ie.InternalKVector.Ids.First();

        return mv.Grade == VSpaceDimensions - 1 && 
               mv.GetBasisBladeScalar(id).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidPGaVector(XGaMultivector<T> mv)
    {
        var id = Ie.InternalKVector.Ids.First();

        return mv.IsKVector(VSpaceDimensions - 1) && 
               mv.GetBasisBladeScalar(id).IsZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidHGaPoint(XGaVector<T> mv)
    {
        return mv[0].IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidHGaPoint(XGaKVector<T> mv)
    {
        return mv.Grade == 1 && 
               mv[0].IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidHGaPoint(XGaMultivector<T> mv)
    {
        return mv.IsVector() && 
               mv[0].IsNotZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidHGaVector(XGaVector<T> mv)
    {
        return mv[0].IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidHGaVector(XGaKVector<T> mv)
    {
        return mv.Grade == 1 && 
               mv[0].IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidHGaVector(XGaMultivector<T> mv)
    {
        return mv.IsVector() && 
               mv[0].IsZero();
    }


    /// <summary>
    /// Create a PGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> EncodeScalar(int s)
    {
        return new XGaProjectiveBlade<T>(
            this,
            Processor.Scalar(s)
        );
    }

    /// <summary>
    /// Create a PGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> EncodeScalar(Scalar<T> s)
    {
        return new XGaProjectiveBlade<T>(
            this,
            Processor.Scalar(s)
        );
    }
    
    /// <summary>
    /// Create a PGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> EncodeScalar(T s)
    {
        return new XGaProjectiveBlade<T>(
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