using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

public sealed class XGaConformalElementSpecs<T> :
    IEquatable<XGaConformalElementSpecs<T>>
{
    public static XGaConformalElementSpecs<T> PGaFlatPoint2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.PGa,
            0
        );

    public static XGaConformalElementSpecs<T> PGaFlatLine2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.PGa,
            1
        );

    public static XGaConformalElementSpecs<T> PGaFlatPlane2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.PGa,
            2
        );
    

    public static XGaConformalElementSpecs<T> PGaFlatPoint3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.PGa,
            0
        );

    public static XGaConformalElementSpecs<T> PGaFlatLine3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.PGa,
            1
        );

    public static XGaConformalElementSpecs<T> PGaFlatPlane3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.PGa,
            2
        );
    
    public static XGaConformalElementSpecs<T> PGaFlatVolume3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.PGa,
            3
        );
    
    
    public static XGaConformalElementSpecs<T> OpnsDirectionPoint2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Opns,
            0
        );

    public static XGaConformalElementSpecs<T> OpnsDirectionLine2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Opns,
            1
        );

    public static XGaConformalElementSpecs<T> OpnsDirectionPlane2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Opns,
            2
        );


    public static XGaConformalElementSpecs<T> OpnsDirectionPoint3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Opns,
            0
        );

    public static XGaConformalElementSpecs<T> OpnsDirectionLine3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Opns,
            1
        );

    public static XGaConformalElementSpecs<T> OpnsDirectionPlane3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Opns,
            2
        );
    
    public static XGaConformalElementSpecs<T> OpnsDirectionVolume3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Opns,
            3
        );

    
    public static XGaConformalElementSpecs<T> OpnsTangentPoint2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Opns,
            0
        );

    public static XGaConformalElementSpecs<T> OpnsTangentLine2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Opns,
            1
        );

    public static XGaConformalElementSpecs<T> OpnsTangentPlane2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Opns,
            2
        );


    public static XGaConformalElementSpecs<T> OpnsTangentPoint3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Opns,
            0
        );

    public static XGaConformalElementSpecs<T> OpnsTangentLine3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Opns,
            1
        );

    public static XGaConformalElementSpecs<T> OpnsTangentPlane3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Opns,
            2
        );
    
    public static XGaConformalElementSpecs<T> OpnsTangentVolume3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Opns,
            3
        );

    
    public static XGaConformalElementSpecs<T> OpnsFlatPoint2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Opns,
            0
        );

    public static XGaConformalElementSpecs<T> OpnsFlatLine2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Opns,
            1
        );

    public static XGaConformalElementSpecs<T> OpnsFlatPlane2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Opns,
            2
        );
    

    public static XGaConformalElementSpecs<T> OpnsFlatPoint3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Opns,
            0
        );

    public static XGaConformalElementSpecs<T> OpnsFlatLine3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Opns,
            1
        );

    public static XGaConformalElementSpecs<T> OpnsFlatPlane3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Opns,
            2
        );
    
    public static XGaConformalElementSpecs<T> OpnsFlatVolume3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Opns,
            3
        );
    

    public static XGaConformalElementSpecs<T> OpnsRoundPoint2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Opns,
            0
        );

    public static XGaConformalElementSpecs<T> OpnsRoundPointPair2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Opns,
            1
        );

    public static XGaConformalElementSpecs<T> OpnsRoundCircle2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Opns,
            2
        );
    

    public static XGaConformalElementSpecs<T> OpnsRoundPoint3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Opns,
            0
        );

    public static XGaConformalElementSpecs<T> OpnsRoundPointPair3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Opns,
            1
        );

    public static XGaConformalElementSpecs<T> OpnsRoundCircle3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Opns,
            2
        );

    public static XGaConformalElementSpecs<T> OpnsRoundSphere3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Opns,
            3
        );
    
    
    public static XGaConformalElementSpecs<T> IpnsDirectionPoint2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Ipns,
            0
        );

    public static XGaConformalElementSpecs<T> IpnsDirectionLine2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Ipns,
            1
        );

    public static XGaConformalElementSpecs<T> IpnsDirectionPlane2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Ipns,
            2
        );


    public static XGaConformalElementSpecs<T> IpnsDirectionPoint3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Ipns,
            0
        );

    public static XGaConformalElementSpecs<T> IpnsDirectionLine3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Ipns,
            1
        );

    public static XGaConformalElementSpecs<T> IpnsDirectionPlane3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Ipns,
            2
        );
    
    public static XGaConformalElementSpecs<T> IpnsDirectionVolume3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.Ipns,
            3
        );

    
    public static XGaConformalElementSpecs<T> IpnsTangentPoint2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Ipns,
            0
        );

    public static XGaConformalElementSpecs<T> IpnsTangentLine2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Ipns,
            1
        );

    public static XGaConformalElementSpecs<T> IpnsTangentPlane2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Ipns,
            2
        );


    public static XGaConformalElementSpecs<T> IpnsTangentPoint3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Ipns,
            0
        );

    public static XGaConformalElementSpecs<T> IpnsTangentLine3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Ipns,
            1
        );

    public static XGaConformalElementSpecs<T> IpnsTangentPlane3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Ipns,
            2
        );
    
    public static XGaConformalElementSpecs<T> IpnsTangentVolume3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.Ipns,
            3
        );

    
    public static XGaConformalElementSpecs<T> IpnsFlatPoint2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Ipns,
            0
        );

    public static XGaConformalElementSpecs<T> IpnsFlatLine2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Ipns,
            1
        );

    public static XGaConformalElementSpecs<T> IpnsFlatPlane2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Ipns,
            2
        );
    

    public static XGaConformalElementSpecs<T> IpnsFlatPoint3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Ipns,
            0
        );

    public static XGaConformalElementSpecs<T> IpnsFlatLine3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Ipns,
            1
        );

    public static XGaConformalElementSpecs<T> IpnsFlatPlane3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Ipns,
            2
        );
    
    public static XGaConformalElementSpecs<T> IpnsFlatVolume3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Ipns,
            3
        );
    

    public static XGaConformalElementSpecs<T> IpnsRoundPoint2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Ipns,
            0
        );

    public static XGaConformalElementSpecs<T> IpnsRoundPointPair2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Ipns,
            1
        );

    public static XGaConformalElementSpecs<T> IpnsRoundCircle2D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create4D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Ipns,
            2
        );
    

    public static XGaConformalElementSpecs<T> IpnsRoundPoint3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Ipns,
            0
        );

    public static XGaConformalElementSpecs<T> IpnsRoundPointPair3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Ipns,
            1
        );

    public static XGaConformalElementSpecs<T> IpnsRoundCircle3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Ipns,
            2
        );

    public static XGaConformalElementSpecs<T> IpnsRoundSphere3D(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalElementSpecs<T>(
            XGaConformalSpace<T>.Create5D(scalarProcessor), 
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.Ipns,
            3
        );

    public static XGaConformalElementSpecs<T> EGaDirection(XGaConformalSpace<T> conformalSpace, int grade)
    {
        return new XGaConformalElementSpecs<T>(
            conformalSpace,
            XGaConformalElementKind.Direction,
            XGaConformalElementEncoding.EGa,
            grade
        );
    }
    
    public static XGaConformalElementSpecs<T> EGaTangent(XGaConformalSpace<T> conformalSpace, int grade)
    {
        return new XGaConformalElementSpecs<T>(
            conformalSpace,
            XGaConformalElementKind.Tangent,
            XGaConformalElementEncoding.EGa,
            grade
        );
    }
    
    public static XGaConformalElementSpecs<T> EGaFlat(XGaConformalSpace<T> conformalSpace, int grade)
    {
        return new XGaConformalElementSpecs<T>(
            conformalSpace,
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.EGa,
            grade
        );
    }
    
    public static XGaConformalElementSpecs<T> EGaRound(XGaConformalSpace<T> conformalSpace, int grade)
    {
        return new XGaConformalElementSpecs<T>(
            conformalSpace,
            XGaConformalElementKind.Round,
            XGaConformalElementEncoding.EGa,
            grade
        );
    }


    public XGaConformalSpace<T> ConformalSpace { get; }

    public XGaConformalSpace4D<T> ConformalSpace4D 
        => (XGaConformalSpace4D<T>)ConformalSpace;

    public XGaConformalSpace5D<T> ConformalSpace5D 
        => (XGaConformalSpace5D<T>)ConformalSpace;

    public XGaGeometrySpaceBasisSpecs<T> BasisSpecs 
        => ConformalSpace.BasisSpecs;

    public XGaConformalElementKind Kind { get; }

    public XGaConformalElementEncoding Encoding { get; }

    public int EGaDirectionGrade { get; }
    
    public bool IsDirection 
        => Kind == XGaConformalElementKind.Direction;
    
    public bool IsOpnsDirection
        => Kind == XGaConformalElementKind.Direction &&
           Encoding == XGaConformalElementEncoding.Opns;

    public bool IsIpnsDirection
        => Kind == XGaConformalElementKind.Direction &&
           Encoding == XGaConformalElementEncoding.Ipns;

    public bool IsTangent 
        => Kind == XGaConformalElementKind.Tangent;
    
    public bool IsOpnsTangent
        => Kind == XGaConformalElementKind.Tangent &&
           Encoding == XGaConformalElementEncoding.Opns;

    public bool IsIpnsTangent
        => Kind == XGaConformalElementKind.Tangent &&
           Encoding == XGaConformalElementEncoding.Ipns;

    public bool IsFlat 
        => Kind == XGaConformalElementKind.Flat;
    
    public bool IsPGaFlat
        => Kind == XGaConformalElementKind.Flat &&
           Encoding == XGaConformalElementEncoding.PGa;

    public bool IsPGaFlatPoint 
        => Kind == XGaConformalElementKind.Flat &&
           Encoding == XGaConformalElementEncoding.PGa &&
           EGaDirectionGrade == 0;

    public bool IsPGaFlatLine 
        => Kind == XGaConformalElementKind.Flat &&
           Encoding == XGaConformalElementEncoding.PGa &&
           EGaDirectionGrade == 1;

    public bool IsPGaFlatPlane 
        => Kind == XGaConformalElementKind.Flat &&
           Encoding == XGaConformalElementEncoding.PGa &&
           EGaDirectionGrade == 2;
    
    public bool IsOpnsFlat
        => Kind == XGaConformalElementKind.Flat &&
           Encoding == XGaConformalElementEncoding.Opns;

    public bool IsOpnsFlatPoint 
        => Kind == XGaConformalElementKind.Flat &&
           Encoding == XGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 0;
    
    public bool IsOpnsFlatLine
        => Kind == XGaConformalElementKind.Flat &&
           Encoding == XGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 1;
    
    public bool IsOpnsFlatPlane 
        => Kind == XGaConformalElementKind.Flat &&
           Encoding == XGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 2;
    
    public bool IsIpnsFlat
        => Kind == XGaConformalElementKind.Flat &&
           Encoding == XGaConformalElementEncoding.Ipns;

    public bool IsIpnsFlatPoint 
        => Kind == XGaConformalElementKind.Flat &&
           Encoding == XGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 0;
    
    public bool IsIpnsFlatLine
        => Kind == XGaConformalElementKind.Flat &&
           Encoding == XGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 1;
    
    public bool IsIpnsFlatPlane 
        => Kind == XGaConformalElementKind.Flat &&
           Encoding == XGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 2;

    public bool IsRound 
        => Kind == XGaConformalElementKind.Round;
    
    public bool IsOpnsRound
        => Kind == XGaConformalElementKind.Round &&
           Encoding == XGaConformalElementEncoding.Opns;

    public bool IsIpnsRound
        => Kind == XGaConformalElementKind.Round &&
           Encoding == XGaConformalElementEncoding.Ipns;

    public bool IsOpnsRoundPoint 
        => Kind == XGaConformalElementKind.Round &&
           Encoding == XGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 0;
    
    public bool IsOpnsRoundPointPair
        => Kind == XGaConformalElementKind.Round &&
           Encoding == XGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 1;
    
    public bool IsOpnsRoundCircle
        => Kind == XGaConformalElementKind.Round &&
           Encoding == XGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 2;
    
    public bool IsOpnsRoundSphere
        => Kind == XGaConformalElementKind.Round &&
           Encoding == XGaConformalElementEncoding.Opns &&
           EGaDirectionGrade == 3;

    public bool IsIpnsRoundPoint 
        => Kind == XGaConformalElementKind.Round &&
           Encoding == XGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 0;
    
    public bool IsIpnsRoundPointPair
        => Kind == XGaConformalElementKind.Round &&
           Encoding == XGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 1;
    
    public bool IsIpnsRoundCircle
        => Kind == XGaConformalElementKind.Round &&
           Encoding == XGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 2;
    
    public bool IsIpnsRoundSphere
        => Kind == XGaConformalElementKind.Round &&
           Encoding == XGaConformalElementEncoding.Ipns &&
           EGaDirectionGrade == 3;

    public bool IsEGa
        => Encoding == XGaConformalElementEncoding.EGa;
    
    public bool IsHGa
        => Encoding == XGaConformalElementEncoding.HGa;
    
    public bool IsPGa
        => Encoding == XGaConformalElementEncoding.PGa;

    public bool IsOpns 
        => Encoding == XGaConformalElementEncoding.Opns;

    public bool IsIpns 
        => Encoding == XGaConformalElementEncoding.Ipns;

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
    internal XGaConformalElementSpecs(XGaConformalSpace<T> conformalSpace, XGaConformalElementKind kind, XGaConformalElementEncoding encoding)
    {
        ConformalSpace = conformalSpace;
        Kind = kind;
        Encoding = encoding;
        EGaDirectionGrade = -1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalElementSpecs(XGaConformalSpace<T> conformalSpace, XGaConformalElementKind kind, XGaConformalElementEncoding encoding, int egaDirectionGrade)
    {
        ConformalSpace = conformalSpace;
        Kind = kind;
        Encoding = encoding;
        EGaDirectionGrade = egaDirectionGrade;
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
    //        egaProbePoint.EncodeEGaVectorBlade(ConformalSpace);

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
    //        egaProbePoint.EncodeEGaVectorBlade(ConformalSpace);

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
    //        egaProbePoint.EncodeEGaVectorBlade(ConformalSpace);

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
    //        egaProbePoint.EncodeEGaVectorBlade(ConformalSpace);

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
    //                    .EncodeEGaVectorBlade(ConformalSpace), 
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
    //                    .EncodeEGaVectorBlade(ConformalSpace), 
    //                this
    //            )
    //        );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(XGaConformalElementSpecs<T>? other)
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