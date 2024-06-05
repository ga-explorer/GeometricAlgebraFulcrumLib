using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Elements;

public sealed class XGaProjectiveElementSpecs<T> :
    IEquatable<XGaProjectiveElementSpecs<T>>
{
    public static XGaProjectiveElementSpecs<T> PGaFlatPoint2D(IScalarProcessor<T> scalarProcessor)
        => new XGaProjectiveElementSpecs<T>(
            XGaProjectiveSpace<T>.Create4D(scalarProcessor), 
            XGaProjectiveElementKind.Euclidean,
            XGaProjectiveElementEncoding.PGa,
            0
        );

    public static XGaProjectiveElementSpecs<T> PGaFlatLine2D(IScalarProcessor<T> scalarProcessor)
        => new XGaProjectiveElementSpecs<T>(
            XGaProjectiveSpace<T>.Create4D(scalarProcessor), 
            XGaProjectiveElementKind.Euclidean,
            XGaProjectiveElementEncoding.PGa,
            1
        );

    public static XGaProjectiveElementSpecs<T> PGaFlatPlane2D(IScalarProcessor<T> scalarProcessor)
        => new XGaProjectiveElementSpecs<T>(
            XGaProjectiveSpace<T>.Create4D(scalarProcessor), 
            XGaProjectiveElementKind.Euclidean,
            XGaProjectiveElementEncoding.PGa,
            2
        );
    

    public static XGaProjectiveElementSpecs<T> PGaFlatPoint3D(IScalarProcessor<T> scalarProcessor)
        => new XGaProjectiveElementSpecs<T>(
            XGaProjectiveSpace<T>.Create5D(scalarProcessor), 
            XGaProjectiveElementKind.Euclidean,
            XGaProjectiveElementEncoding.PGa,
            0
        );

    public static XGaProjectiveElementSpecs<T> PGaFlatLine3D(IScalarProcessor<T> scalarProcessor)
        => new XGaProjectiveElementSpecs<T>(
            XGaProjectiveSpace<T>.Create5D(scalarProcessor), 
            XGaProjectiveElementKind.Euclidean,
            XGaProjectiveElementEncoding.PGa,
            1
        );

    public static XGaProjectiveElementSpecs<T> PGaFlatPlane3D(IScalarProcessor<T> scalarProcessor)
        => new XGaProjectiveElementSpecs<T>(
            XGaProjectiveSpace<T>.Create5D(scalarProcessor), 
            XGaProjectiveElementKind.Euclidean,
            XGaProjectiveElementEncoding.PGa,
            2
        );
    
    public static XGaProjectiveElementSpecs<T> PGaFlatVolume3D(IScalarProcessor<T> scalarProcessor)
        => new XGaProjectiveElementSpecs<T>(
            XGaProjectiveSpace<T>.Create5D(scalarProcessor), 
            XGaProjectiveElementKind.Euclidean,
            XGaProjectiveElementEncoding.PGa,
            3
        );
    

    public static XGaProjectiveElementSpecs<T> EGaDirection(XGaProjectiveSpace<T> projectiveSpace, int grade)
    {
        return new XGaProjectiveElementSpecs<T>(
            projectiveSpace,
            XGaProjectiveElementKind.Ideal,
            XGaProjectiveElementEncoding.EGa,
            grade
        );
    }
    
    public static XGaProjectiveElementSpecs<T> EGaFlat(XGaProjectiveSpace<T> projectiveSpace, int grade)
    {
        return new XGaProjectiveElementSpecs<T>(
            projectiveSpace,
            XGaProjectiveElementKind.Euclidean,
            XGaProjectiveElementEncoding.EGa,
            grade
        );
    }
    

    public XGaProjectiveSpace<T> ProjectiveSpace { get; }

    public XGaProjectiveSpace3D<T> ProjectiveSpace3D 
        => (XGaProjectiveSpace3D<T>)ProjectiveSpace;

    public XGaProjectiveSpace4D<T> ProjectiveSpace4D 
        => (XGaProjectiveSpace4D<T>)ProjectiveSpace;

    public XGaGeometrySpaceBasisSpecs<T> BasisSpecs 
        => ProjectiveSpace.BasisSpecs;

    public XGaProjectiveElementKind Kind { get; }

    public XGaProjectiveElementEncoding Encoding { get; }

    public int EGaDirectionGrade { get; }
    
    public bool IsDirection 
        => Kind == XGaProjectiveElementKind.Ideal;
    
    public bool IsFlat 
        => Kind == XGaProjectiveElementKind.Euclidean;
    
    public bool IsPGaFlat
        => Kind == XGaProjectiveElementKind.Euclidean &&
           Encoding == XGaProjectiveElementEncoding.PGa;

    public bool IsPGaFlatPoint 
        => Kind == XGaProjectiveElementKind.Euclidean &&
           Encoding == XGaProjectiveElementEncoding.PGa &&
           EGaDirectionGrade == 0;

    public bool IsPGaFlatLine 
        => Kind == XGaProjectiveElementKind.Euclidean &&
           Encoding == XGaProjectiveElementEncoding.PGa &&
           EGaDirectionGrade == 1;

    public bool IsPGaFlatPlane 
        => Kind == XGaProjectiveElementKind.Euclidean &&
           Encoding == XGaProjectiveElementEncoding.PGa &&
           EGaDirectionGrade == 2;
    
    public bool IsEGa
        => Encoding == XGaProjectiveElementEncoding.EGa;
    
    public bool IsPGa
        => Encoding == XGaProjectiveElementEncoding.PGa;

    public bool EGaIs2D 
        => ProjectiveSpace.Is3D;
    
    public bool EGaIs3D 
        => ProjectiveSpace.Is4D;
    
    public bool EGaDirectionIsUndefined
        => EGaDirectionGrade < 0;

    public bool EGaDirectionIsScalar
        => EGaDirectionGrade == 0;
    
    public bool EGaDirectionIsVector
        => EGaDirectionGrade == 1;
    
    public bool EGaDirectionIsBivector
        => EGaDirectionGrade == 2;
    
    public bool EGaDirectionIsTrivector
        => EGaDirectionGrade == 4;
    
    public bool EGaDirectionIsPseudoVector
        => EGaDirectionGrade == ProjectiveSpace.VSpaceDimensions - 3;

    public bool EGaDirectionIsPseudoScalar
        => EGaDirectionGrade == ProjectiveSpace.VSpaceDimensions - 2;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaProjectiveElementSpecs(XGaProjectiveSpace<T> projectiveSpace, XGaProjectiveElementKind kind, XGaProjectiveElementEncoding encoding)
    {
        ProjectiveSpace = projectiveSpace;
        Kind = kind;
        Encoding = encoding;
        EGaDirectionGrade = -1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaProjectiveElementSpecs(XGaProjectiveSpace<T> projectiveSpace, XGaProjectiveElementKind kind, XGaProjectiveElementEncoding encoding, int egaDirectionGrade)
    {
        ProjectiveSpace = projectiveSpace;
        Kind = kind;
        Encoding = encoding;
        EGaDirectionGrade = egaDirectionGrade;
    }

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaProjectiveElement<T>> getBladeFunc)
    //{
    //    return XGaProjectiveParametricElement<T>.Create(
    //        ProjectiveSpace, 
    //        parameterRange, 
    //        getBladeFunc
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaProjectiveBlade<T>> getBladeFunc)
    //{
    //    return XGaProjectiveParametricElement<T>.Create(
    //        ProjectiveSpace,
    //        parameterRange,
    //        t => getBladeFunc(t).DecodeElement(this)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaProjectiveBlade<T>> getBladeFunc, ILinVector2D<T> egaProbePoint)
    //{
    //    var egaProbePointBlade = 
    //        egaProbePoint.EncodeEGaVectorBlade(ProjectiveSpace);

    //    return XGaProjectiveParametricElement<T>.Create(
    //        ProjectiveSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointBlade, 
    //                this
    //            )
    //        );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaProjectiveBlade<T>> getBladeFunc, ILinVector3D<T> egaProbePoint)
    //{
    //    var egaProbePointBlade = 
    //        egaProbePoint.EncodeEGaVectorBlade(ProjectiveSpace);

    //    return XGaProjectiveParametricElement<T>.Create(
    //        ProjectiveSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointBlade, 
    //                this
    //            )
    //        );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaProjectiveBlade<T>> getBladeFunc, LinVector<T> egaProbePoint)
    //{
    //    var egaProbePointBlade = 
    //        egaProbePoint.EncodeEGaVectorBlade(ProjectiveSpace);

    //    return XGaProjectiveParametricElement<T>.Create(
    //        ProjectiveSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointBlade, 
    //                this
    //            )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaProjectiveBlade<T>> getBladeFunc, XGaVector<T> egaProbePoint)
    //{
    //    var egaProbePointBlade = 
    //        egaProbePoint.EncodeEGaVectorBlade(ProjectiveSpace);

    //    return XGaProjectiveParametricElement<T>.Create(
    //        ProjectiveSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointBlade, 
    //                this
    //            )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaProjectiveBlade<T>> getBladeFunc, IFloat64ParametricCurve2D egaProbePointCurve)
    //{
    //    return XGaProjectiveParametricElement<T>.Create(
    //        ProjectiveSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointCurve
    //                    .GetPoint(t)
    //                    .EncodeEGaVectorBlade(ProjectiveSpace), 
    //                this
    //            )
    //        );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaProjectiveBlade<T>> getBladeFunc, IParametricCurve3D egaProbePointCurve)
    //{
    //    return XGaProjectiveParametricElement<T>.Create(
    //        ProjectiveSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointCurve
    //                    .GetPoint(t)
    //                    .EncodeEGaVectorBlade(ProjectiveSpace), 
    //                this
    //            )
    //        );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(XGaProjectiveElementSpecs<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Kind == other.Kind && Encoding == other.Encoding && EGaDirectionGrade == other.EGaDirectionGrade;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine((int)Kind, (int)Encoding, EGaDirectionGrade);
    }
}