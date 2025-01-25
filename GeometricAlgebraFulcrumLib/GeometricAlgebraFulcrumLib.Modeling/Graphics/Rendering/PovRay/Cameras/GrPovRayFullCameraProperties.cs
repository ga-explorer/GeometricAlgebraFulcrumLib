using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;

public sealed class GrPovRayFullCameraProperties :
    GrPovRayCameraProperties
{
    public static GrPovRayFullCameraProperties CreateDefault()
    {
        return new GrPovRayFullCameraProperties();
    }

    public static GrPovRayFullCameraProperties Create(GrPovRayFullCameraProperties properties)
    {
        return new GrPovRayFullCameraProperties(properties);
    }

    public static GrPovRayFullCameraProperties Create(LinFloat64Angle angle, Float64Scalar aspectRatio)
    {
        var cameraProperties = 
            new GrPovRayFullCameraProperties
            {
                Angle = angle,
                Up = LinFloat64Vector3D.E1,
                Right = LinFloat64Vector3D.E3 * aspectRatio,
                Direction = LinFloat64Vector3D.NegativeE2
            };

        return cameraProperties;
    }
    
    public static GrPovRayFullCameraProperties CreateOrthographic(double width, double height)
    {
        Debug.Assert(
            width > 0 && height > 0
        );

        var cameraProperties = 
            new GrPovRayFullCameraProperties
            {
                Up = LinFloat64Vector3D.E1 * height,
                Right = LinFloat64Vector3D.E3 * width,
                Direction = LinFloat64Vector3D.NegativeE2
            };

        return cameraProperties;
    }


    public GrPovRayVector3Value? Up
    {
        get => GetAttributeValueOrNull<GrPovRayVector3Value>("up");
        set => SetAttributeValue("up", value);
    }

    public GrPovRayVector3Value? Right
    {
        get => GetAttributeValueOrNull<GrPovRayVector3Value>("right");
        set => SetAttributeValue("right", value);
    }
        
    public GrPovRayVector3Value? Direction
    {
        get => GetAttributeValueOrNull<GrPovRayVector3Value>("direction");
        set => SetAttributeValue("direction", value);
    }
        
    public GrPovRayAngleValue? Angle
    {
        get => GetAttributeValueOrNull<GrPovRayAngleValue>("angle");
        set => SetAttributeValue("angle", value);
    }

    public GrPovRayVector3Value? FocalPoint
    {
        get => GetAttributeValueOrNull<GrPovRayVector3Value>("focal_point");
        set => SetAttributeValue("focal_point", value);
    }
    

    private GrPovRayFullCameraProperties()
    {
        //Angle = 67.380.DegreesToPolarAngle();
        Up = LinFloat64Vector3D.E1;
        Right = LinFloat64Vector3D.E3;
        Direction = LinFloat64Vector3D.NegativeE2;
    }

    private GrPovRayFullCameraProperties(GrPovRayFullCameraProperties properties)
    {
        SetAttributeValues(properties);
    }


    public override string GetPovRayCode()
    {
        return GetKeyValueCodePairs(
            "right",
            "up",
            "direction",
            "angle",
            "sky",
            "location",
            "look_at",
            "focal_point"
        ).Select(
            p => $"{p.Key} {p.Value}"
        ).Concatenate(Environment.NewLine);
    }
}