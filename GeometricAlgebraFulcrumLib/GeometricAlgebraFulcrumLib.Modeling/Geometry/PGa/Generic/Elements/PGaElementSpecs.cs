using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Elements;

public sealed class PGaElementSpecs<T> :
    IEquatable<PGaElementSpecs<T>>
{
    public static PGaElementSpecs<T> PGaFlatPoint2D(IScalarProcessor<T> scalarProcessor)
        => new PGaElementSpecs<T>(
            PGaGeometricSpace<T>.Create4D(scalarProcessor),
            PGaElementKind.Euclidean,
            PGaElementEncoding.PGa,
            0
        );

    public static PGaElementSpecs<T> PGaFlatLine2D(IScalarProcessor<T> scalarProcessor)
        => new PGaElementSpecs<T>(
            PGaGeometricSpace<T>.Create4D(scalarProcessor),
            PGaElementKind.Euclidean,
            PGaElementEncoding.PGa,
            1
        );

    public static PGaElementSpecs<T> PGaFlatPlane2D(IScalarProcessor<T> scalarProcessor)
        => new PGaElementSpecs<T>(
            PGaGeometricSpace<T>.Create4D(scalarProcessor),
            PGaElementKind.Euclidean,
            PGaElementEncoding.PGa,
            2
        );


    public static PGaElementSpecs<T> PGaFlatPoint3D(IScalarProcessor<T> scalarProcessor)
        => new PGaElementSpecs<T>(
            PGaGeometricSpace<T>.Create5D(scalarProcessor),
            PGaElementKind.Euclidean,
            PGaElementEncoding.PGa,
            0
        );

    public static PGaElementSpecs<T> PGaFlatLine3D(IScalarProcessor<T> scalarProcessor)
        => new PGaElementSpecs<T>(
            PGaGeometricSpace<T>.Create5D(scalarProcessor),
            PGaElementKind.Euclidean,
            PGaElementEncoding.PGa,
            1
        );

    public static PGaElementSpecs<T> PGaFlatPlane3D(IScalarProcessor<T> scalarProcessor)
        => new PGaElementSpecs<T>(
            PGaGeometricSpace<T>.Create5D(scalarProcessor),
            PGaElementKind.Euclidean,
            PGaElementEncoding.PGa,
            2
        );

    public static PGaElementSpecs<T> PGaFlatVolume3D(IScalarProcessor<T> scalarProcessor)
        => new PGaElementSpecs<T>(
            PGaGeometricSpace<T>.Create5D(scalarProcessor),
            PGaElementKind.Euclidean,
            PGaElementEncoding.PGa,
            3
        );


    public static PGaElementSpecs<T> VGaDirection(PGaGeometricSpace<T> pgaGeometricSpace, int grade)
    {
        return new PGaElementSpecs<T>(
            pgaGeometricSpace,
            PGaElementKind.Ideal,
            PGaElementEncoding.VGa,
            grade
        );
    }

    public static PGaElementSpecs<T> VGaFlat(PGaGeometricSpace<T> pgaGeometricSpace, int grade)
    {
        return new PGaElementSpecs<T>(
            pgaGeometricSpace,
            PGaElementKind.Euclidean,
            PGaElementEncoding.VGa,
            grade
        );
    }


    public PGaGeometricSpace<T> Geometry { get; }

    public PGaGeometricSpace3D<T> Geometry3D
        => (PGaGeometricSpace3D<T>)Geometry;

    public PGaGeometricSpace4D<T> Geometry4D
        => (PGaGeometricSpace4D<T>)Geometry;

    public GaGeometricSpaceBasisSpecs<T> BasisSpecs
        => Geometry.BasisSpecs;

    public PGaElementKind Kind { get; }

    public PGaElementEncoding Encoding { get; }

    public int VGaDirectionGrade { get; }

    public bool IsDirection
        => Kind == PGaElementKind.Ideal;

    public bool IsFlat
        => Kind == PGaElementKind.Euclidean;

    public bool IsPGaFlat
        => Kind == PGaElementKind.Euclidean &&
           Encoding == PGaElementEncoding.PGa;

    public bool IsPGaFlatPoint
        => Kind == PGaElementKind.Euclidean &&
           Encoding == PGaElementEncoding.PGa &&
           VGaDirectionGrade == 0;

    public bool IsPGaFlatLine
        => Kind == PGaElementKind.Euclidean &&
           Encoding == PGaElementEncoding.PGa &&
           VGaDirectionGrade == 1;

    public bool IsPGaFlatPlane
        => Kind == PGaElementKind.Euclidean &&
           Encoding == PGaElementEncoding.PGa &&
           VGaDirectionGrade == 2;

    public bool IsVGa
        => Encoding == PGaElementEncoding.VGa;

    public bool IsPGa
        => Encoding == PGaElementEncoding.PGa;

    public bool VGaIs2D
        => Geometry.Is3D;

    public bool VGaIs3D
        => Geometry.Is4D;

    public bool VGaDirectionIsUndefined
        => VGaDirectionGrade < 0;

    public bool VGaDirectionIsScalar
        => VGaDirectionGrade == 0;

    public bool VGaDirectionIsVector
        => VGaDirectionGrade == 1;

    public bool VGaDirectionIsBivector
        => VGaDirectionGrade == 2;

    public bool VGaDirectionIsTrivector
        => VGaDirectionGrade == 4;

    public bool VGaDirectionIsPseudoVector
        => VGaDirectionGrade == Geometry.VSpaceDimensions - 3;

    public bool VGaDirectionIsPseudoScalar
        => VGaDirectionGrade == Geometry.VSpaceDimensions - 2;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal PGaElementSpecs(PGaGeometricSpace<T> pgaGeometricSpace, PGaElementKind kind, PGaElementEncoding encoding)
    {
        Geometry = pgaGeometricSpace;
        Kind = kind;
        Encoding = encoding;
        VGaDirectionGrade = -1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal PGaElementSpecs(PGaGeometricSpace<T> pgaGeometricSpace, PGaElementKind kind, PGaElementEncoding encoding, int egaDirectionGrade)
    {
        Geometry = pgaGeometricSpace;
        Kind = kind;
        Encoding = encoding;
        VGaDirectionGrade = egaDirectionGrade;
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, PGaElement<T>> getBladeFunc)
    //{
    //    return XGaProjectiveParametricElement<T>.Create(
    //        ProjectiveSpace, 
    //        parameterRange, 
    //        getBladeFunc
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, PGaBlade<T>> getBladeFunc)
    //{
    //    return XGaProjectiveParametricElement<T>.Create(
    //        ProjectiveSpace,
    //        parameterRange,
    //        t => getBladeFunc(t).DecodeElement(this)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, PGaBlade<T>> getBladeFunc, IPair<Scalar<T>> egaProbePoint)
    //{
    //    var egaProbePointBlade = 
    //        egaProbePoint.EncodeVGaVectorBlade(ProjectiveSpace);

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
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, PGaBlade<T>> getBladeFunc, ITriplet<Scalar<T>> egaProbePoint)
    //{
    //    var egaProbePointBlade = 
    //        egaProbePoint.EncodeVGaVectorBlade(ProjectiveSpace);

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
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, PGaBlade<T>> getBladeFunc, LinVector<T> egaProbePoint)
    //{
    //    var egaProbePointBlade = 
    //        egaProbePoint.EncodeVGaVectorBlade(ProjectiveSpace);

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
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, PGaBlade<T>> getBladeFunc, XGaVector<T> egaProbePoint)
    //{
    //    var egaProbePointBlade = 
    //        egaProbePoint.EncodeVGaVectorBlade(ProjectiveSpace);

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
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, PGaBlade<T>> getBladeFunc, IFloat64ParametricCurve2D egaProbePointCurve)
    //{
    //    return XGaProjectiveParametricElement<T>.Create(
    //        ProjectiveSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointCurve
    //                    .GetPoint(t)
    //                    .EncodeVGaVectorBlade(ProjectiveSpace), 
    //                this
    //            )
    //        );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaProjectiveParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, PGaBlade<T>> getBladeFunc, IParametricCurve3D egaProbePointCurve)
    //{
    //    return XGaProjectiveParametricElement<T>.Create(
    //        ProjectiveSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointCurve
    //                    .GetPoint(t)
    //                    .EncodeVGaVectorBlade(ProjectiveSpace), 
    //                this
    //            )
    //        );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(PGaElementSpecs<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Kind == other.Kind && Encoding == other.Encoding && VGaDirectionGrade == other.VGaDirectionGrade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine((int)Kind, (int)Encoding, VGaDirectionGrade);
    }
}