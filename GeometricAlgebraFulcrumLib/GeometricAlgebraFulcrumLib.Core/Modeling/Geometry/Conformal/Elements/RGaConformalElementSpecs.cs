using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;

public sealed class RGaConformalElementSpecs :
    IEquatable<RGaConformalElementSpecs>
{
    public static RGaConformalElementSpecs PGaFlatPoint2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.PGa,
            0
        );

    public static RGaConformalElementSpecs PGaFlatLine2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.PGa,
            1
        );

    public static RGaConformalElementSpecs PGaFlatPlane2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.PGa,
            2
        );
    

    public static RGaConformalElementSpecs PGaFlatPoint3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.PGa,
            0
        );

    public static RGaConformalElementSpecs PGaFlatLine3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.PGa,
            1
        );

    public static RGaConformalElementSpecs PGaFlatPlane3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.PGa,
            2
        );
    
    public static RGaConformalElementSpecs PGaFlatVolume3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.PGa,
            3
        );
    
    
    public static RGaConformalElementSpecs OpnsDirectionPoint2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Opns,
            0
        );

    public static RGaConformalElementSpecs OpnsDirectionLine2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Opns,
            1
        );

    public static RGaConformalElementSpecs OpnsDirectionPlane2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Opns,
            2
        );


    public static RGaConformalElementSpecs OpnsDirectionPoint3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Opns,
            0
        );

    public static RGaConformalElementSpecs OpnsDirectionLine3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Opns,
            1
        );

    public static RGaConformalElementSpecs OpnsDirectionPlane3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Opns,
            2
        );
    
    public static RGaConformalElementSpecs OpnsDirectionVolume3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Opns,
            3
        );

    
    public static RGaConformalElementSpecs OpnsTangentPoint2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Opns,
            0
        );

    public static RGaConformalElementSpecs OpnsTangentLine2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Opns,
            1
        );

    public static RGaConformalElementSpecs OpnsTangentPlane2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Opns,
            2
        );


    public static RGaConformalElementSpecs OpnsTangentPoint3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Opns,
            0
        );

    public static RGaConformalElementSpecs OpnsTangentLine3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Opns,
            1
        );

    public static RGaConformalElementSpecs OpnsTangentPlane3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Opns,
            2
        );
    
    public static RGaConformalElementSpecs OpnsTangentVolume3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Opns,
            3
        );

    
    public static RGaConformalElementSpecs OpnsFlatPoint2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Opns,
            0
        );

    public static RGaConformalElementSpecs OpnsFlatLine2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Opns,
            1
        );

    public static RGaConformalElementSpecs OpnsFlatPlane2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Opns,
            2
        );
    

    public static RGaConformalElementSpecs OpnsFlatPoint3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Opns,
            0
        );

    public static RGaConformalElementSpecs OpnsFlatLine3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Opns,
            1
        );

    public static RGaConformalElementSpecs OpnsFlatPlane3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Opns,
            2
        );
    
    public static RGaConformalElementSpecs OpnsFlatVolume3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Opns,
            3
        );
    

    public static RGaConformalElementSpecs OpnsRoundPoint2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Opns,
            0
        );

    public static RGaConformalElementSpecs OpnsRoundPointPair2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Opns,
            1
        );

    public static RGaConformalElementSpecs OpnsRoundCircle2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Opns,
            2
        );
    

    public static RGaConformalElementSpecs OpnsRoundPoint3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Opns,
            0
        );

    public static RGaConformalElementSpecs OpnsRoundPointPair3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Opns,
            1
        );

    public static RGaConformalElementSpecs OpnsRoundCircle3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Opns,
            2
        );

    public static RGaConformalElementSpecs OpnsRoundSphere3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Opns,
            3
        );
    
    
    public static RGaConformalElementSpecs IpnsDirectionPoint2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Ipns,
            0
        );

    public static RGaConformalElementSpecs IpnsDirectionLine2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Ipns,
            1
        );

    public static RGaConformalElementSpecs IpnsDirectionPlane2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Ipns,
            2
        );


    public static RGaConformalElementSpecs IpnsDirectionPoint3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Ipns,
            0
        );

    public static RGaConformalElementSpecs IpnsDirectionLine3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Ipns,
            1
        );

    public static RGaConformalElementSpecs IpnsDirectionPlane3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Ipns,
            2
        );
    
    public static RGaConformalElementSpecs IpnsDirectionVolume3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.Ipns,
            3
        );

    
    public static RGaConformalElementSpecs IpnsTangentPoint2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Ipns,
            0
        );

    public static RGaConformalElementSpecs IpnsTangentLine2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Ipns,
            1
        );

    public static RGaConformalElementSpecs IpnsTangentPlane2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Ipns,
            2
        );


    public static RGaConformalElementSpecs IpnsTangentPoint3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Ipns,
            0
        );

    public static RGaConformalElementSpecs IpnsTangentLine3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Ipns,
            1
        );

    public static RGaConformalElementSpecs IpnsTangentPlane3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Ipns,
            2
        );
    
    public static RGaConformalElementSpecs IpnsTangentVolume3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.Ipns,
            3
        );

    
    public static RGaConformalElementSpecs IpnsFlatPoint2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Ipns,
            0
        );

    public static RGaConformalElementSpecs IpnsFlatLine2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Ipns,
            1
        );

    public static RGaConformalElementSpecs IpnsFlatPlane2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Ipns,
            2
        );
    

    public static RGaConformalElementSpecs IpnsFlatPoint3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Ipns,
            0
        );

    public static RGaConformalElementSpecs IpnsFlatLine3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Ipns,
            1
        );

    public static RGaConformalElementSpecs IpnsFlatPlane3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Ipns,
            2
        );
    
    public static RGaConformalElementSpecs IpnsFlatVolume3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Ipns,
            3
        );
    

    public static RGaConformalElementSpecs IpnsRoundPoint2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Ipns,
            0
        );

    public static RGaConformalElementSpecs IpnsRoundPointPair2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Ipns,
            1
        );

    public static RGaConformalElementSpecs IpnsRoundCircle2D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space4D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Ipns,
            2
        );
    

    public static RGaConformalElementSpecs IpnsRoundPoint3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Ipns,
            0
        );

    public static RGaConformalElementSpecs IpnsRoundPointPair3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Ipns,
            1
        );

    public static RGaConformalElementSpecs IpnsRoundCircle3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Ipns,
            2
        );

    public static RGaConformalElementSpecs IpnsRoundSphere3D { get; }
        = new RGaConformalElementSpecs(
            RGaConformalSpace.Space5D, 
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.Ipns,
            3
        );

    public static RGaConformalElementSpecs EGaDirection(RGaConformalSpace conformalSpace, int grade)
    {
        return new RGaConformalElementSpecs(
            conformalSpace,
            RGaConformalElementKind.Direction,
            RGaConformalElementEncoding.EGa,
            grade
        );
    }
    
    public static RGaConformalElementSpecs EGaTangent(RGaConformalSpace conformalSpace, int grade)
    {
        return new RGaConformalElementSpecs(
            conformalSpace,
            RGaConformalElementKind.Tangent,
            RGaConformalElementEncoding.EGa,
            grade
        );
    }
    
    public static RGaConformalElementSpecs EGaFlat(RGaConformalSpace conformalSpace, int grade)
    {
        return new RGaConformalElementSpecs(
            conformalSpace,
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.EGa,
            grade
        );
    }
    
    public static RGaConformalElementSpecs EGaRound(RGaConformalSpace conformalSpace, int grade)
    {
        return new RGaConformalElementSpecs(
            conformalSpace,
            RGaConformalElementKind.Round,
            RGaConformalElementEncoding.EGa,
            grade
        );
    }


    public RGaConformalSpace ConformalSpace { get; }

    public RGaGeometrySpaceBasisSpecs BasisSpecs 
        => ConformalSpace.BasisSpecs;

    public RGaConformalElementKind Kind { get; }

    public RGaConformalElementEncoding Encoding { get; }

    public int EGaDirectionGrade { get; }
    
    public bool IsDirection 
        => Kind == RGaConformalElementKind.Direction;
    
    public bool IsOpnsDirection
        => Kind == RGaConformalElementKind.Direction &&
           Encoding == RGaConformalElementEncoding.Opns;

    public bool IsIpnsDirection
        => Kind == RGaConformalElementKind.Direction &&
           Encoding == RGaConformalElementEncoding.Ipns;

    public bool IsTangent 
        => Kind == RGaConformalElementKind.Tangent;
    
    public bool IsOpnsTangent
        => Kind == RGaConformalElementKind.Tangent &&
           Encoding == RGaConformalElementEncoding.Opns;

    public bool IsIpnsTangent
        => Kind == RGaConformalElementKind.Tangent &&
           Encoding == RGaConformalElementEncoding.Ipns;

    public bool IsFlat 
        => Kind == RGaConformalElementKind.Flat;
    
    public bool IsPGaFlat
        => Kind == RGaConformalElementKind.Flat &&
           Encoding == RGaConformalElementEncoding.PGa;

    public bool IsPGaFlatPoint 
        => Kind == RGaConformalElementKind.Flat &&
           Encoding == RGaConformalElementEncoding.PGa &&
           EGaDirectionGrade == 0;

    public bool IsPGaFlatLine 
        => Kind == RGaConformalElementKind.Flat &&
           Encoding == RGaConformalElementEncoding.PGa &&
           EGaDirectionGrade == 1;

    public bool IsPGaFlatPlane 
        => Kind == RGaConformalElementKind.Flat &&
           Encoding == RGaConformalElementEncoding.PGa &&
           EGaDirectionGrade == 2;
    
    public bool IsOpnsFlat
        => Kind == RGaConformalElementKind.Flat &&
           Encoding == RGaConformalElementEncoding.Opns;

    public bool IsOpnsFlatPoint 
        => Kind == RGaConformalElementKind.Flat &&
           Encoding == RGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 0;
    
    public bool IsOpnsFlatLine
        => Kind == RGaConformalElementKind.Flat &&
           Encoding == RGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 1;
    
    public bool IsOpnsFlatPlane 
        => Kind == RGaConformalElementKind.Flat &&
           Encoding == RGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 2;
    
    public bool IsIpnsFlat
        => Kind == RGaConformalElementKind.Flat &&
           Encoding == RGaConformalElementEncoding.Ipns;

    public bool IsIpnsFlatPoint 
        => Kind == RGaConformalElementKind.Flat &&
           Encoding == RGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 0;
    
    public bool IsIpnsFlatLine
        => Kind == RGaConformalElementKind.Flat &&
           Encoding == RGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 1;
    
    public bool IsIpnsFlatPlane 
        => Kind == RGaConformalElementKind.Flat &&
           Encoding == RGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 2;

    public bool IsRound 
        => Kind == RGaConformalElementKind.Round;
    
    public bool IsOpnsRound
        => Kind == RGaConformalElementKind.Round &&
           Encoding == RGaConformalElementEncoding.Opns;

    public bool IsIpnsRound
        => Kind == RGaConformalElementKind.Round &&
           Encoding == RGaConformalElementEncoding.Ipns;

    public bool IsOpnsRoundPoint 
        => Kind == RGaConformalElementKind.Round &&
           Encoding == RGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 0;
    
    public bool IsOpnsRoundPointPair
        => Kind == RGaConformalElementKind.Round &&
           Encoding == RGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 1;
    
    public bool IsOpnsRoundCircle
        => Kind == RGaConformalElementKind.Round &&
           Encoding == RGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 2;
    
    public bool IsOpnsRoundSphere
        => Kind == RGaConformalElementKind.Round &&
           Encoding == RGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 3;

    public bool IsIpnsRoundPoint 
        => Kind == RGaConformalElementKind.Round &&
           Encoding == RGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 0;
    
    public bool IsIpnsRoundPointPair
        => Kind == RGaConformalElementKind.Round &&
           Encoding == RGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 1;
    
    public bool IsIpnsRoundCircle
        => Kind == RGaConformalElementKind.Round &&
           Encoding == RGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 2;
    
    public bool IsIpnsRoundSphere
        => Kind == RGaConformalElementKind.Round &&
           Encoding == RGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 3;

    public bool IsEGa
        => Encoding == RGaConformalElementEncoding.EGa;
    
    public bool IsHGa
        => Encoding == RGaConformalElementEncoding.HGa;
    
    public bool IsPGa
        => Encoding == RGaConformalElementEncoding.PGa;

    public bool IsOpns 
        => Encoding == RGaConformalElementEncoding.Opns;

    public bool IsIpns 
        => Encoding == RGaConformalElementEncoding.Ipns;

    public bool EGaIs2D 
        => ConformalSpace.Is4D;
    
    public bool EGaIs3D 
        => ConformalSpace.Is5D;
    
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
        => EGaDirectionGrade == ConformalSpace.VSpaceDimensions - 3;

    public bool EGaDirectionIsPseudoScalar
        => EGaDirectionGrade == ConformalSpace.VSpaceDimensions - 2;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalElementSpecs(RGaConformalSpace conformalSpace, RGaConformalElementKind kind, RGaConformalElementEncoding encoding)
    {
        ConformalSpace = conformalSpace;
        Kind = kind;
        Encoding = encoding;
        EGaDirectionGrade = -1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalElementSpecs(RGaConformalSpace conformalSpace, RGaConformalElementKind kind, RGaConformalElementEncoding encoding, int egaDirectionGrade)
    {
        ConformalSpace = conformalSpace;
        Kind = kind;
        Encoding = encoding;
        EGaDirectionGrade = egaDirectionGrade;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, RGaConformalElement> getBladeFunc)
    {
        return RGaConformalParametricElement.Create(
            ConformalSpace, 
            parameterRange, 
            getBladeFunc
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, RGaConformalBlade> getBladeFunc)
    {
        return RGaConformalParametricElement.Create(
            ConformalSpace,
            parameterRange,
            t => getBladeFunc(t).DecodeElement(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, RGaConformalBlade> getBladeFunc, ILinFloat64Vector2D egaProbePoint)
    {
        var egaProbePointBlade = 
            egaProbePoint.EncodeEGaVectorBlade(ConformalSpace);

        return RGaConformalParametricElement.Create(
            ConformalSpace,
            parameterRange,
            t => 
                getBladeFunc(t).DecodeElement(
                    egaProbePointBlade, 
                    this
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, RGaConformalBlade> getBladeFunc, ILinFloat64Vector3D egaProbePoint)
    {
        var egaProbePointBlade = 
            egaProbePoint.EncodeEGaVectorBlade(ConformalSpace);

        return RGaConformalParametricElement.Create(
            ConformalSpace,
            parameterRange,
            t => 
                getBladeFunc(t).DecodeElement(
                    egaProbePointBlade, 
                    this
                )
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, RGaConformalBlade> getBladeFunc, LinFloat64Vector egaProbePoint)
    {
        var egaProbePointBlade = 
            egaProbePoint.EncodeEGaVectorBlade(ConformalSpace);

        return RGaConformalParametricElement.Create(
            ConformalSpace,
            parameterRange,
            t => 
                getBladeFunc(t).DecodeElement(
                    egaProbePointBlade, 
                    this
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, RGaConformalBlade> getBladeFunc, RGaFloat64Vector egaProbePoint)
    {
        var egaProbePointBlade = 
            egaProbePoint.EncodeEGaVectorBlade(ConformalSpace);

        return RGaConformalParametricElement.Create(
            ConformalSpace,
            parameterRange,
            t => 
                getBladeFunc(t).DecodeElement(
                    egaProbePointBlade, 
                    this
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, RGaConformalBlade> getBladeFunc, IFloat64ParametricCurve2D egaProbePointCurve)
    {
        return RGaConformalParametricElement.Create(
            ConformalSpace,
            parameterRange,
            t => 
                getBladeFunc(t).DecodeElement(
                    egaProbePointCurve
                        .GetPoint(t)
                        .EncodeEGaVectorBlade(ConformalSpace), 
                    this
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalParametricElement CreateParametricElement(Float64ScalarRange parameterRange, Func<double, RGaConformalBlade> getBladeFunc, IParametricCurve3D egaProbePointCurve)
    {
        return RGaConformalParametricElement.Create(
            ConformalSpace,
            parameterRange,
            t => 
                getBladeFunc(t).DecodeElement(
                    egaProbePointCurve
                        .GetPoint(t)
                        .EncodeEGaVectorBlade(ConformalSpace), 
                    this
                )
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(RGaConformalElementSpecs? other)
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