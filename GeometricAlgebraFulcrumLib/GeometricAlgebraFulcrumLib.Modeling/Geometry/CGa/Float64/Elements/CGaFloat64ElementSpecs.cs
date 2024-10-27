using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public sealed class CGaFloat64ElementSpecs :
    IEquatable<CGaFloat64ElementSpecs>
{
    public static CGaFloat64ElementSpecs PGaFlatPoint2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.PGa,
            0
        );

    public static CGaFloat64ElementSpecs PGaFlatLine2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.PGa,
            1
        );

    public static CGaFloat64ElementSpecs PGaFlatPlane2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.PGa,
            2
        );


    public static CGaFloat64ElementSpecs PGaFlatPoint3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.PGa,
            0
        );

    public static CGaFloat64ElementSpecs PGaFlatLine3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.PGa,
            1
        );

    public static CGaFloat64ElementSpecs PGaFlatPlane3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.PGa,
            2
        );

    public static CGaFloat64ElementSpecs PGaFlatVolume3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.PGa,
            3
        );


    public static CGaFloat64ElementSpecs OpnsDirectionPoint2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Opns,
            0
        );

    public static CGaFloat64ElementSpecs OpnsDirectionLine2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Opns,
            1
        );

    public static CGaFloat64ElementSpecs OpnsDirectionPlane2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Opns,
            2
        );


    public static CGaFloat64ElementSpecs OpnsDirectionPoint3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Opns,
            0
        );

    public static CGaFloat64ElementSpecs OpnsDirectionLine3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Opns,
            1
        );

    public static CGaFloat64ElementSpecs OpnsDirectionPlane3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Opns,
            2
        );

    public static CGaFloat64ElementSpecs OpnsDirectionVolume3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Opns,
            3
        );


    public static CGaFloat64ElementSpecs OpnsTangentPoint2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Opns,
            0
        );

    public static CGaFloat64ElementSpecs OpnsTangentLine2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Opns,
            1
        );

    public static CGaFloat64ElementSpecs OpnsTangentPlane2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Opns,
            2
        );


    public static CGaFloat64ElementSpecs OpnsTangentPoint3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Opns,
            0
        );

    public static CGaFloat64ElementSpecs OpnsTangentLine3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Opns,
            1
        );

    public static CGaFloat64ElementSpecs OpnsTangentPlane3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Opns,
            2
        );

    public static CGaFloat64ElementSpecs OpnsTangentVolume3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Opns,
            3
        );


    public static CGaFloat64ElementSpecs OpnsFlatPoint2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Opns,
            0
        );

    public static CGaFloat64ElementSpecs OpnsFlatLine2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Opns,
            1
        );

    public static CGaFloat64ElementSpecs OpnsFlatPlane2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Opns,
            2
        );


    public static CGaFloat64ElementSpecs OpnsFlatPoint3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Opns,
            0
        );

    public static CGaFloat64ElementSpecs OpnsFlatLine3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Opns,
            1
        );

    public static CGaFloat64ElementSpecs OpnsFlatPlane3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Opns,
            2
        );

    public static CGaFloat64ElementSpecs OpnsFlatVolume3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Opns,
            3
        );


    public static CGaFloat64ElementSpecs OpnsRoundPoint2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Opns,
            0
        );

    public static CGaFloat64ElementSpecs OpnsRoundPointPair2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Opns,
            1
        );

    public static CGaFloat64ElementSpecs OpnsRoundCircle2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Opns,
            2
        );


    public static CGaFloat64ElementSpecs OpnsRoundPoint3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Opns,
            0
        );

    public static CGaFloat64ElementSpecs OpnsRoundPointPair3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Opns,
            1
        );

    public static CGaFloat64ElementSpecs OpnsRoundCircle3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Opns,
            2
        );

    public static CGaFloat64ElementSpecs OpnsRoundSphere3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Opns,
            3
        );


    public static CGaFloat64ElementSpecs IpnsDirectionPoint2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Ipns,
            0
        );

    public static CGaFloat64ElementSpecs IpnsDirectionLine2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Ipns,
            1
        );

    public static CGaFloat64ElementSpecs IpnsDirectionPlane2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Ipns,
            2
        );


    public static CGaFloat64ElementSpecs IpnsDirectionPoint3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Ipns,
            0
        );

    public static CGaFloat64ElementSpecs IpnsDirectionLine3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Ipns,
            1
        );

    public static CGaFloat64ElementSpecs IpnsDirectionPlane3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Ipns,
            2
        );

    public static CGaFloat64ElementSpecs IpnsDirectionVolume3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.Ipns,
            3
        );


    public static CGaFloat64ElementSpecs IpnsTangentPoint2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Ipns,
            0
        );

    public static CGaFloat64ElementSpecs IpnsTangentLine2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Ipns,
            1
        );

    public static CGaFloat64ElementSpecs IpnsTangentPlane2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Ipns,
            2
        );


    public static CGaFloat64ElementSpecs IpnsTangentPoint3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Ipns,
            0
        );

    public static CGaFloat64ElementSpecs IpnsTangentLine3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Ipns,
            1
        );

    public static CGaFloat64ElementSpecs IpnsTangentPlane3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Ipns,
            2
        );

    public static CGaFloat64ElementSpecs IpnsTangentVolume3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.Ipns,
            3
        );


    public static CGaFloat64ElementSpecs IpnsFlatPoint2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Ipns,
            0
        );

    public static CGaFloat64ElementSpecs IpnsFlatLine2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Ipns,
            1
        );

    public static CGaFloat64ElementSpecs IpnsFlatPlane2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Ipns,
            2
        );


    public static CGaFloat64ElementSpecs IpnsFlatPoint3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Ipns,
            0
        );

    public static CGaFloat64ElementSpecs IpnsFlatLine3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Ipns,
            1
        );

    public static CGaFloat64ElementSpecs IpnsFlatPlane3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Ipns,
            2
        );

    public static CGaFloat64ElementSpecs IpnsFlatVolume3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Ipns,
            3
        );


    public static CGaFloat64ElementSpecs IpnsRoundPoint2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Ipns,
            0
        );

    public static CGaFloat64ElementSpecs IpnsRoundPointPair2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Ipns,
            1
        );

    public static CGaFloat64ElementSpecs IpnsRoundCircle2D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space4D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Ipns,
            2
        );


    public static CGaFloat64ElementSpecs IpnsRoundPoint3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Ipns,
            0
        );

    public static CGaFloat64ElementSpecs IpnsRoundPointPair3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Ipns,
            1
        );

    public static CGaFloat64ElementSpecs IpnsRoundCircle3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Ipns,
            2
        );

    public static CGaFloat64ElementSpecs IpnsRoundSphere3D { get; }
        = new CGaFloat64ElementSpecs(
            CGaFloat64GeometricSpace.Space5D,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.Ipns,
            3
        );

    public static CGaFloat64ElementSpecs VGaDirection(CGaFloat64GeometricSpace cgaGeometricSpace, int grade)
    {
        return new CGaFloat64ElementSpecs(
            cgaGeometricSpace,
            CGaFloat64ElementKind.Direction,
            CGaFloat64ElementEncoding.VGa,
            grade
        );
    }

    public static CGaFloat64ElementSpecs VGaTangent(CGaFloat64GeometricSpace cgaGeometricSpace, int grade)
    {
        return new CGaFloat64ElementSpecs(
            cgaGeometricSpace,
            CGaFloat64ElementKind.Tangent,
            CGaFloat64ElementEncoding.VGa,
            grade
        );
    }

    public static CGaFloat64ElementSpecs VGaFlat(CGaFloat64GeometricSpace cgaGeometricSpace, int grade)
    {
        return new CGaFloat64ElementSpecs(
            cgaGeometricSpace,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.VGa,
            grade
        );
    }

    public static CGaFloat64ElementSpecs VGaRound(CGaFloat64GeometricSpace cgaGeometricSpace, int grade)
    {
        return new CGaFloat64ElementSpecs(
            cgaGeometricSpace,
            CGaFloat64ElementKind.Round,
            CGaFloat64ElementEncoding.VGa,
            grade
        );
    }


    public CGaFloat64GeometricSpace GeometricSpace { get; }

    public GaFloat64GeometricSpaceBasisSpecs BasisSpecs
        => GeometricSpace.BasisSpecs;

    public CGaFloat64ElementKind Kind { get; }

    public CGaFloat64ElementEncoding Encoding { get; }

    public int VGaDirectionGrade { get; }

    public bool IsDirection
        => Kind == CGaFloat64ElementKind.Direction;

    public bool IsOpnsDirection
        => Kind == CGaFloat64ElementKind.Direction &&
           Encoding == CGaFloat64ElementEncoding.Opns;

    public bool IsIpnsDirection
        => Kind == CGaFloat64ElementKind.Direction &&
           Encoding == CGaFloat64ElementEncoding.Ipns;

    public bool IsTangent
        => Kind == CGaFloat64ElementKind.Tangent;

    public bool IsOpnsTangent
        => Kind == CGaFloat64ElementKind.Tangent &&
           Encoding == CGaFloat64ElementEncoding.Opns;

    public bool IsIpnsTangent
        => Kind == CGaFloat64ElementKind.Tangent &&
           Encoding == CGaFloat64ElementEncoding.Ipns;

    public bool IsFlat
        => Kind == CGaFloat64ElementKind.Flat;

    public bool IsPGaFlat
        => Kind == CGaFloat64ElementKind.Flat &&
           Encoding == CGaFloat64ElementEncoding.PGa;

    public bool IsPGaFlatPoint
        => Kind == CGaFloat64ElementKind.Flat &&
           Encoding == CGaFloat64ElementEncoding.PGa &&
           VGaDirectionGrade == 0;

    public bool IsPGaFlatLine
        => Kind == CGaFloat64ElementKind.Flat &&
           Encoding == CGaFloat64ElementEncoding.PGa &&
           VGaDirectionGrade == 1;

    public bool IsPGaFlatPlane
        => Kind == CGaFloat64ElementKind.Flat &&
           Encoding == CGaFloat64ElementEncoding.PGa &&
           VGaDirectionGrade == 2;

    public bool IsOpnsFlat
        => Kind == CGaFloat64ElementKind.Flat &&
           Encoding == CGaFloat64ElementEncoding.Opns;

    public bool IsOpnsFlatPoint
        => Kind == CGaFloat64ElementKind.Flat &&
           Encoding == CGaFloat64ElementEncoding.Opns &&
           VGaDirectionGrade == 0;

    public bool IsOpnsFlatLine
        => Kind == CGaFloat64ElementKind.Flat &&
           Encoding == CGaFloat64ElementEncoding.Opns &&
           VGaDirectionGrade == 1;

    public bool IsOpnsFlatPlane
        => Kind == CGaFloat64ElementKind.Flat &&
           Encoding == CGaFloat64ElementEncoding.Opns &&
           VGaDirectionGrade == 2;

    public bool IsIpnsFlat
        => Kind == CGaFloat64ElementKind.Flat &&
           Encoding == CGaFloat64ElementEncoding.Ipns;

    public bool IsIpnsFlatPoint
        => Kind == CGaFloat64ElementKind.Flat &&
           Encoding == CGaFloat64ElementEncoding.Ipns &&
           VGaDirectionGrade == 0;

    public bool IsIpnsFlatLine
        => Kind == CGaFloat64ElementKind.Flat &&
           Encoding == CGaFloat64ElementEncoding.Ipns &&
           VGaDirectionGrade == 1;

    public bool IsIpnsFlatPlane
        => Kind == CGaFloat64ElementKind.Flat &&
           Encoding == CGaFloat64ElementEncoding.Ipns &&
           VGaDirectionGrade == 2;

    public bool IsRound
        => Kind == CGaFloat64ElementKind.Round;

    public bool IsOpnsRound
        => Kind == CGaFloat64ElementKind.Round &&
           Encoding == CGaFloat64ElementEncoding.Opns;

    public bool IsIpnsRound
        => Kind == CGaFloat64ElementKind.Round &&
           Encoding == CGaFloat64ElementEncoding.Ipns;

    public bool IsOpnsRoundPoint
        => Kind == CGaFloat64ElementKind.Round &&
           Encoding == CGaFloat64ElementEncoding.Opns &&
           VGaDirectionGrade == 0;

    public bool IsOpnsRoundPointPair
        => Kind == CGaFloat64ElementKind.Round &&
           Encoding == CGaFloat64ElementEncoding.Opns &&
           VGaDirectionGrade == 1;

    public bool IsOpnsRoundCircle
        => Kind == CGaFloat64ElementKind.Round &&
           Encoding == CGaFloat64ElementEncoding.Opns &&
           VGaDirectionGrade == 2;

    public bool IsOpnsRoundSphere
        => Kind == CGaFloat64ElementKind.Round &&
           Encoding == CGaFloat64ElementEncoding.Opns &&
           VGaDirectionGrade == 3;

    public bool IsIpnsRoundPoint
        => Kind == CGaFloat64ElementKind.Round &&
           Encoding == CGaFloat64ElementEncoding.Ipns &&
           VGaDirectionGrade == 0;

    public bool IsIpnsRoundPointPair
        => Kind == CGaFloat64ElementKind.Round &&
           Encoding == CGaFloat64ElementEncoding.Ipns &&
           VGaDirectionGrade == 1;

    public bool IsIpnsRoundCircle
        => Kind == CGaFloat64ElementKind.Round &&
           Encoding == CGaFloat64ElementEncoding.Ipns &&
           VGaDirectionGrade == 2;

    public bool IsIpnsRoundSphere
        => Kind == CGaFloat64ElementKind.Round &&
           Encoding == CGaFloat64ElementEncoding.Ipns &&
           VGaDirectionGrade == 3;

    public bool IsVGa
        => Encoding == CGaFloat64ElementEncoding.VGa;

    public bool IsHGa
        => Encoding == CGaFloat64ElementEncoding.HGa;

    public bool IsPGa
        => Encoding == CGaFloat64ElementEncoding.PGa;

    public bool IsOpns
        => Encoding == CGaFloat64ElementEncoding.Opns;

    public bool IsIpns
        => Encoding == CGaFloat64ElementEncoding.Ipns;

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
    internal CGaFloat64ElementSpecs(CGaFloat64GeometricSpace cgaGeometricSpace, CGaFloat64ElementKind kind, CGaFloat64ElementEncoding encoding)
    {
        GeometricSpace = cgaGeometricSpace;
        Kind = kind;
        Encoding = encoding;
        VGaDirectionGrade = -1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFloat64ElementSpecs(CGaFloat64GeometricSpace cgaGeometricSpace, CGaFloat64ElementKind kind, CGaFloat64ElementEncoding encoding, int egaDirectionGrade)
    {
        GeometricSpace = cgaGeometricSpace;
        Kind = kind;
        Encoding = encoding;
        VGaDirectionGrade = egaDirectionGrade;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, CGaFloat64Element> getBladeFunc)
    {
        return CGaFloat64ParametricElement.Create(
            GeometricSpace,
            parameterRange,
            getBladeFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, CGaFloat64Blade> getBladeFunc)
    {
        return CGaFloat64ParametricElement.Create(
            GeometricSpace,
            parameterRange,
            t => getBladeFunc(t).Decode.Element(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, CGaFloat64Blade> getBladeFunc, ILinFloat64Vector2D egaProbePoint)
    {
        var egaProbePointBlade =
            egaProbePoint.EncodeVGaVector(GeometricSpace);

        return CGaFloat64ParametricElement.Create(
            GeometricSpace,
            parameterRange,
            t =>
                getBladeFunc(t).Decode.Element(
                    egaProbePointBlade,
                    this
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, CGaFloat64Blade> getBladeFunc, ILinFloat64Vector3D egaProbePoint)
    {
        var egaProbePointBlade =
            egaProbePoint.EncodeVGaVector(GeometricSpace);

        return CGaFloat64ParametricElement.Create(
            GeometricSpace,
            parameterRange,
            t =>
                getBladeFunc(t).Decode.Element(
                    egaProbePointBlade,
                    this
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, CGaFloat64Blade> getBladeFunc, LinFloat64Vector egaProbePoint)
    {
        var egaProbePointBlade =
            egaProbePoint.EncodeVGaVector(GeometricSpace);

        return CGaFloat64ParametricElement.Create(
            GeometricSpace,
            parameterRange,
            t =>
                getBladeFunc(t).Decode.Element(
                    egaProbePointBlade,
                    this
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, CGaFloat64Blade> getBladeFunc, RGaFloat64Vector egaProbePoint)
    {
        var egaProbePointBlade =
            egaProbePoint.EncodeVGaVector(GeometricSpace);

        return CGaFloat64ParametricElement.Create(
            GeometricSpace,
            parameterRange,
            t =>
                getBladeFunc(t).Decode.Element(
                    egaProbePointBlade,
                    this
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, CGaFloat64Blade> getBladeFunc, IFloat64ParametricCurve2D egaProbePointCurve)
    {
        return CGaFloat64ParametricElement.Create(
            GeometricSpace,
            parameterRange,
            t =>
                getBladeFunc(t).Decode.Element(
                    egaProbePointCurve
                        .GetPoint(t)
                        .EncodeVGaVector(GeometricSpace),
                    this
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, CGaFloat64Blade> getBladeFunc, IParametricCurve3D egaProbePointCurve)
    {
        return CGaFloat64ParametricElement.Create(
            GeometricSpace,
            parameterRange,
            t =>
                getBladeFunc(t).Decode.Element(
                    egaProbePointCurve
                        .GetPoint(t)
                        .EncodeVGaVector(GeometricSpace),
                    this
                )
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(CGaFloat64ElementSpecs? other)
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