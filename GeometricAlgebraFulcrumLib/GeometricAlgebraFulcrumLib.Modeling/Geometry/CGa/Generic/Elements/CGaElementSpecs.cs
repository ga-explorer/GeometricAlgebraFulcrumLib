using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

public sealed class CGaElementSpecs<T> :
    IEquatable<CGaElementSpecs<T>>
{
    public static CGaElementSpecs<T> PGaFlatPoint2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.PGa,
            0
        );

    public static CGaElementSpecs<T> PGaFlatLine2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.PGa,
            1
        );

    public static CGaElementSpecs<T> PGaFlatPlane2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.PGa,
            2
        );


    public static CGaElementSpecs<T> PGaFlatPoint3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.PGa,
            0
        );

    public static CGaElementSpecs<T> PGaFlatLine3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.PGa,
            1
        );

    public static CGaElementSpecs<T> PGaFlatPlane3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.PGa,
            2
        );

    public static CGaElementSpecs<T> PGaFlatVolume3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.PGa,
            3
        );


    public static CGaElementSpecs<T> OpnsDirectionPoint2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Opns,
            0
        );

    public static CGaElementSpecs<T> OpnsDirectionLine2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Opns,
            1
        );

    public static CGaElementSpecs<T> OpnsDirectionPlane2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Opns,
            2
        );


    public static CGaElementSpecs<T> OpnsDirectionPoint3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Opns,
            0
        );

    public static CGaElementSpecs<T> OpnsDirectionLine3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Opns,
            1
        );

    public static CGaElementSpecs<T> OpnsDirectionPlane3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Opns,
            2
        );

    public static CGaElementSpecs<T> OpnsDirectionVolume3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Opns,
            3
        );


    public static CGaElementSpecs<T> OpnsTangentPoint2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Opns,
            0
        );

    public static CGaElementSpecs<T> OpnsTangentLine2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Opns,
            1
        );

    public static CGaElementSpecs<T> OpnsTangentPlane2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Opns,
            2
        );


    public static CGaElementSpecs<T> OpnsTangentPoint3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Opns,
            0
        );

    public static CGaElementSpecs<T> OpnsTangentLine3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Opns,
            1
        );

    public static CGaElementSpecs<T> OpnsTangentPlane3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Opns,
            2
        );

    public static CGaElementSpecs<T> OpnsTangentVolume3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Opns,
            3
        );


    public static CGaElementSpecs<T> OpnsFlatPoint2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Opns,
            0
        );

    public static CGaElementSpecs<T> OpnsFlatLine2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Opns,
            1
        );

    public static CGaElementSpecs<T> OpnsFlatPlane2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Opns,
            2
        );


    public static CGaElementSpecs<T> OpnsFlatPoint3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Opns,
            0
        );

    public static CGaElementSpecs<T> OpnsFlatLine3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Opns,
            1
        );

    public static CGaElementSpecs<T> OpnsFlatPlane3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Opns,
            2
        );

    public static CGaElementSpecs<T> OpnsFlatVolume3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Opns,
            3
        );


    public static CGaElementSpecs<T> OpnsRoundPoint2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Opns,
            0
        );

    public static CGaElementSpecs<T> OpnsRoundPointPair2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Opns,
            1
        );

    public static CGaElementSpecs<T> OpnsRoundCircle2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Opns,
            2
        );


    public static CGaElementSpecs<T> OpnsRoundPoint3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Opns,
            0
        );

    public static CGaElementSpecs<T> OpnsRoundPointPair3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Opns,
            1
        );

    public static CGaElementSpecs<T> OpnsRoundCircle3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Opns,
            2
        );

    public static CGaElementSpecs<T> OpnsRoundSphere3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Opns,
            3
        );


    public static CGaElementSpecs<T> IpnsDirectionPoint2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Ipns,
            0
        );

    public static CGaElementSpecs<T> IpnsDirectionLine2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Ipns,
            1
        );

    public static CGaElementSpecs<T> IpnsDirectionPlane2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Ipns,
            2
        );


    public static CGaElementSpecs<T> IpnsDirectionPoint3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Ipns,
            0
        );

    public static CGaElementSpecs<T> IpnsDirectionLine3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Ipns,
            1
        );

    public static CGaElementSpecs<T> IpnsDirectionPlane3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Ipns,
            2
        );

    public static CGaElementSpecs<T> IpnsDirectionVolume3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Direction,
            CGaElementEncoding.Ipns,
            3
        );


    public static CGaElementSpecs<T> IpnsTangentPoint2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Ipns,
            0
        );

    public static CGaElementSpecs<T> IpnsTangentLine2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Ipns,
            1
        );

    public static CGaElementSpecs<T> IpnsTangentPlane2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Ipns,
            2
        );


    public static CGaElementSpecs<T> IpnsTangentPoint3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Ipns,
            0
        );

    public static CGaElementSpecs<T> IpnsTangentLine3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Ipns,
            1
        );

    public static CGaElementSpecs<T> IpnsTangentPlane3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Ipns,
            2
        );

    public static CGaElementSpecs<T> IpnsTangentVolume3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Tangent,
            CGaElementEncoding.Ipns,
            3
        );


    public static CGaElementSpecs<T> IpnsFlatPoint2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Ipns,
            0
        );

    public static CGaElementSpecs<T> IpnsFlatLine2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Ipns,
            1
        );

    public static CGaElementSpecs<T> IpnsFlatPlane2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Ipns,
            2
        );


    public static CGaElementSpecs<T> IpnsFlatPoint3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Ipns,
            0
        );

    public static CGaElementSpecs<T> IpnsFlatLine3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Ipns,
            1
        );

    public static CGaElementSpecs<T> IpnsFlatPlane3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Ipns,
            2
        );

    public static CGaElementSpecs<T> IpnsFlatVolume3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Flat,
            CGaElementEncoding.Ipns,
            3
        );


    public static CGaElementSpecs<T> IpnsRoundPoint2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Ipns,
            0
        );

    public static CGaElementSpecs<T> IpnsRoundPointPair2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Ipns,
            1
        );

    public static CGaElementSpecs<T> IpnsRoundCircle2D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create4D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Ipns,
            2
        );


    public static CGaElementSpecs<T> IpnsRoundPoint3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Ipns,
            0
        );

    public static CGaElementSpecs<T> IpnsRoundPointPair3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Ipns,
            1
        );

    public static CGaElementSpecs<T> IpnsRoundCircle3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Ipns,
            2
        );

    public static CGaElementSpecs<T> IpnsRoundSphere3D(IScalarProcessor<T> scalarProcessor)
        => new CGaElementSpecs<T>(
            CGaGeometricSpace<T>.Create5D(scalarProcessor),
            CGaElementKind.Round,
            CGaElementEncoding.Ipns,
            3
        );

    public static CGaElementSpecs<T> VGaDirection(CGaGeometricSpace<T> cgaGeometricSpace, int grade)
    {
        return new CGaElementSpecs<T>(
            cgaGeometricSpace,
            CGaElementKind.Direction,
            CGaElementEncoding.VGa,
            grade
        );
    }

    public static CGaElementSpecs<T> VGaTangent(CGaGeometricSpace<T> cgaGeometricSpace, int grade)
    {
        return new CGaElementSpecs<T>(
            cgaGeometricSpace,
            CGaElementKind.Tangent,
            CGaElementEncoding.VGa,
            grade
        );
    }

    public static CGaElementSpecs<T> VGaFlat(CGaGeometricSpace<T> cgaGeometricSpace, int grade)
    {
        return new CGaElementSpecs<T>(
            cgaGeometricSpace,
            CGaElementKind.Flat,
            CGaElementEncoding.VGa,
            grade
        );
    }

    public static CGaElementSpecs<T> VGaRound(CGaGeometricSpace<T> cgaGeometricSpace, int grade)
    {
        return new CGaElementSpecs<T>(
            cgaGeometricSpace,
            CGaElementKind.Round,
            CGaElementEncoding.VGa,
            grade
        );
    }


    public CGaGeometricSpace<T> GeometricSpace { get; }

    public CGaGeometricSpace4D<T> GeometricSpace4D
        => (CGaGeometricSpace4D<T>)GeometricSpace;

    public CGaGeometricSpace5D<T> GeometricSpace5D
        => (CGaGeometricSpace5D<T>)GeometricSpace;

    public GaGeometricSpaceBasisSpecs<T> BasisSpecs
        => GeometricSpace.BasisSpecs;

    public CGaElementKind Kind { get; }

    public CGaElementEncoding Encoding { get; }

    public int VGaDirectionGrade { get; }

    public bool IsDirection
        => Kind == CGaElementKind.Direction;

    public bool IsOpnsDirection
        => Kind == CGaElementKind.Direction &&
           Encoding == CGaElementEncoding.Opns;

    public bool IsIpnsDirection
        => Kind == CGaElementKind.Direction &&
           Encoding == CGaElementEncoding.Ipns;

    public bool IsTangent
        => Kind == CGaElementKind.Tangent;

    public bool IsOpnsTangent
        => Kind == CGaElementKind.Tangent &&
           Encoding == CGaElementEncoding.Opns;

    public bool IsIpnsTangent
        => Kind == CGaElementKind.Tangent &&
           Encoding == CGaElementEncoding.Ipns;

    public bool IsFlat
        => Kind == CGaElementKind.Flat;

    public bool IsPGaFlat
        => Kind == CGaElementKind.Flat &&
           Encoding == CGaElementEncoding.PGa;

    public bool IsPGaFlatPoint
        => Kind == CGaElementKind.Flat &&
           Encoding == CGaElementEncoding.PGa &&
           VGaDirectionGrade == 0;

    public bool IsPGaFlatLine
        => Kind == CGaElementKind.Flat &&
           Encoding == CGaElementEncoding.PGa &&
           VGaDirectionGrade == 1;

    public bool IsPGaFlatPlane
        => Kind == CGaElementKind.Flat &&
           Encoding == CGaElementEncoding.PGa &&
           VGaDirectionGrade == 2;

    public bool IsOpnsFlat
        => Kind == CGaElementKind.Flat &&
           Encoding == CGaElementEncoding.Opns;

    public bool IsOpnsFlatPoint
        => Kind == CGaElementKind.Flat &&
           Encoding == CGaElementEncoding.Opns &&
           VGaDirectionGrade == 0;

    public bool IsOpnsFlatLine
        => Kind == CGaElementKind.Flat &&
           Encoding == CGaElementEncoding.Opns &&
           VGaDirectionGrade == 1;

    public bool IsOpnsFlatPlane
        => Kind == CGaElementKind.Flat &&
           Encoding == CGaElementEncoding.Opns &&
           VGaDirectionGrade == 2;

    public bool IsIpnsFlat
        => Kind == CGaElementKind.Flat &&
           Encoding == CGaElementEncoding.Ipns;

    public bool IsIpnsFlatPoint
        => Kind == CGaElementKind.Flat &&
           Encoding == CGaElementEncoding.Ipns &&
           VGaDirectionGrade == 0;

    public bool IsIpnsFlatLine
        => Kind == CGaElementKind.Flat &&
           Encoding == CGaElementEncoding.Ipns &&
           VGaDirectionGrade == 1;

    public bool IsIpnsFlatPlane
        => Kind == CGaElementKind.Flat &&
           Encoding == CGaElementEncoding.Ipns &&
           VGaDirectionGrade == 2;

    public bool IsRound
        => Kind == CGaElementKind.Round;

    public bool IsOpnsRound
        => Kind == CGaElementKind.Round &&
           Encoding == CGaElementEncoding.Opns;

    public bool IsIpnsRound
        => Kind == CGaElementKind.Round &&
           Encoding == CGaElementEncoding.Ipns;

    public bool IsOpnsRoundPoint
        => Kind == CGaElementKind.Round &&
           Encoding == CGaElementEncoding.Opns &&
           VGaDirectionGrade == 0;

    public bool IsOpnsRoundPointPair
        => Kind == CGaElementKind.Round &&
           Encoding == CGaElementEncoding.Opns &&
           VGaDirectionGrade == 1;

    public bool IsOpnsRoundCircle
        => Kind == CGaElementKind.Round &&
           Encoding == CGaElementEncoding.Opns &&
           VGaDirectionGrade == 2;

    public bool IsOpnsRoundSphere
        => Kind == CGaElementKind.Round &&
           Encoding == CGaElementEncoding.Opns &&
           VGaDirectionGrade == 3;

    public bool IsIpnsRoundPoint
        => Kind == CGaElementKind.Round &&
           Encoding == CGaElementEncoding.Ipns &&
           VGaDirectionGrade == 0;

    public bool IsIpnsRoundPointPair
        => Kind == CGaElementKind.Round &&
           Encoding == CGaElementEncoding.Ipns &&
           VGaDirectionGrade == 1;

    public bool IsIpnsRoundCircle
        => Kind == CGaElementKind.Round &&
           Encoding == CGaElementEncoding.Ipns &&
           VGaDirectionGrade == 2;

    public bool IsIpnsRoundSphere
        => Kind == CGaElementKind.Round &&
           Encoding == CGaElementEncoding.Ipns &&
           VGaDirectionGrade == 3;

    public bool IsVGa
        => Encoding == CGaElementEncoding.VGa;

    public bool IsHGa
        => Encoding == CGaElementEncoding.HGa;

    public bool IsPGa
        => Encoding == CGaElementEncoding.PGa;

    public bool IsOpns
        => Encoding == CGaElementEncoding.Opns;

    public bool IsIpns
        => Encoding == CGaElementEncoding.Ipns;

    public bool VGaIs2D
        => GeometricSpace.Is4D;

    public bool VGaIs3D
        => GeometricSpace.Is5D;

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
        => VGaDirectionGrade == GeometricSpace.VSpaceDimensions - 3;

    public bool VGaDirectionIsPseudoScalar
        => VGaDirectionGrade == GeometricSpace.VSpaceDimensions - 2;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaElementSpecs(CGaGeometricSpace<T> cgaGeometricSpace, CGaElementKind kind, CGaElementEncoding encoding)
    {
        GeometricSpace = cgaGeometricSpace;
        Kind = kind;
        Encoding = encoding;
        VGaDirectionGrade = -1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaElementSpecs(CGaGeometricSpace<T> cgaGeometricSpace, CGaElementKind kind, CGaElementEncoding encoding, int egaDirectionGrade)
    {
        GeometricSpace = cgaGeometricSpace;
        Kind = kind;
        Encoding = encoding;
        VGaDirectionGrade = egaDirectionGrade;
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalElement<T>> getBladeFunc)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        ConformalSpace, 
    //        parameterRange, 
    //        getBladeFunc
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalBlade<T>> getBladeFunc)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        ConformalSpace,
    //        parameterRange,
    //        t => getBladeFunc(t).DecodeElement(this)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalBlade<T>> getBladeFunc, ILinVector2D<T> egaProbePoint)
    //{
    //    var egaProbePointBlade = 
    //        egaProbePoint.EncodeVGaVectorBlade(ConformalSpace);

    //    return XGaConformalParametricElement<T>.Create(
    //        ConformalSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointBlade, 
    //                this
    //            )
    //        );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalBlade<T>> getBladeFunc, ILinVector3D<T> egaProbePoint)
    //{
    //    var egaProbePointBlade = 
    //        egaProbePoint.EncodeVGaVectorBlade(ConformalSpace);

    //    return XGaConformalParametricElement<T>.Create(
    //        ConformalSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointBlade, 
    //                this
    //            )
    //        );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalBlade<T>> getBladeFunc, LinVector<T> egaProbePoint)
    //{
    //    var egaProbePointBlade = 
    //        egaProbePoint.EncodeVGaVectorBlade(ConformalSpace);

    //    return XGaConformalParametricElement<T>.Create(
    //        ConformalSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointBlade, 
    //                this
    //            )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalBlade<T>> getBladeFunc, XGaVector<T> egaProbePoint)
    //{
    //    var egaProbePointBlade = 
    //        egaProbePoint.EncodeVGaVectorBlade(ConformalSpace);

    //    return XGaConformalParametricElement<T>.Create(
    //        ConformalSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointBlade, 
    //                this
    //            )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalBlade<T>> getBladeFunc, IFloat64ParametricCurve2D egaProbePointCurve)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        ConformalSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointCurve
    //                    .GetPoint(t)
    //                    .EncodeVGaVectorBlade(ConformalSpace), 
    //                this
    //            )
    //        );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> CreateParametricElement(ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalBlade<T>> getBladeFunc, IParametricCurve3D egaProbePointCurve)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        ConformalSpace,
    //        parameterRange,
    //        t => 
    //            getBladeFunc(t).DecodeElement(
    //                egaProbePointCurve
    //                    .GetPoint(t)
    //                    .EncodeVGaVectorBlade(ConformalSpace), 
    //                this
    //            )
    //        );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(CGaElementSpecs<T>? other)
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