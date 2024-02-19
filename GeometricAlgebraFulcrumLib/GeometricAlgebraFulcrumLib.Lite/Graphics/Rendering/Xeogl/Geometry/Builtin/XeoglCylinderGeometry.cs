﻿using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Geometry.Builtin;

/// <summary>
/// A CylinderGeometry is a parameterized Geometry that defines a
/// cylinder-shaped mesh for attached Meshes.
/// http://xeogl.org/docs/classes/CylinderGeometry.html
/// </summary>
public sealed class XeoglCylinderGeometry : XeoglBuiltinSolidGeometry
{
    public static XeoglCylinderGeometry CreateClosed(double radius, double height)
    {
        return new XeoglCylinderGeometry()
        {
            RadiusTop = radius,
            RadiusBottom = radius,
            Height = height
        };
    }

    public static XeoglCylinderGeometry CreateClosed(double radius, double height, int radialSegments, int heightSegments)
    {
        return new XeoglCylinderGeometry()
        {
            RadiusTop = radius,
            RadiusBottom = radius,
            Height = height,
            RadialSegments = radialSegments,
            HeightSegments = heightSegments
        };
    }

    public static XeoglCylinderGeometry CreateClosed(double radiusTop, double radiusBottom, double height)
    {
        return new XeoglCylinderGeometry()
        {
            RadiusTop = radiusTop,
            RadiusBottom = radiusBottom,
            Height = height
        };
    }

    public static XeoglCylinderGeometry CreateClosed(double radiusTop, double radiusBottom, double height, int radialSegments, int heightSegments)
    {
        return new XeoglCylinderGeometry()
        {
            RadiusTop = radiusTop,
            RadiusBottom = radiusBottom,
            Height = height,
            RadialSegments = radialSegments,
            HeightSegments = heightSegments
        };
    }

    public static XeoglCylinderGeometry CreateClosed(IFloat64Vector3D center, double radius, double height)
    {
        return new XeoglCylinderGeometry(center)
        {
            RadiusTop = radius,
            RadiusBottom = radius,
            Height = height
        };
    }

    public static XeoglCylinderGeometry CreateClosed(IFloat64Vector3D center, double radius, double height, int radialSegments, int heightSegments)
    {
        return new XeoglCylinderGeometry(center)
        {
            RadiusTop = radius,
            RadiusBottom = radius,
            Height = height,
            RadialSegments = radialSegments,
            HeightSegments = heightSegments
        };
    }

    public static XeoglCylinderGeometry CreateClosed(IFloat64Vector3D center, double radiusTop, double radiusBottom, double height)
    {
        return new XeoglCylinderGeometry(center)
        {
            RadiusTop = radiusTop,
            RadiusBottom = radiusBottom,
            Height = height
        };
    }

    public static XeoglCylinderGeometry CreateClosed(IFloat64Vector3D center, double radiusTop, double radiusBottom, double height, int radialSegments, int heightSegments)
    {
        return new XeoglCylinderGeometry(center)
        {
            RadiusTop = radiusTop,
            RadiusBottom = radiusBottom,
            Height = height,
            RadialSegments = radialSegments,
            HeightSegments = heightSegments
        };
    }

    public static XeoglCylinderGeometry CreateOpened(double radius, double height)
    {
        return new XeoglCylinderGeometry()
        {
            RadiusTop = radius,
            RadiusBottom = radius,
            Height = height,
            OpenEnded = true
        };
    }

    public static XeoglCylinderGeometry CreateOpened(double radius, double height, int radialSegments, int heightSegments)
    {
        return new XeoglCylinderGeometry()
        {
            RadiusTop = radius,
            RadiusBottom = radius,
            Height = height,
            RadialSegments = radialSegments,
            HeightSegments = heightSegments,
            OpenEnded = true
        };
    }

    public static XeoglCylinderGeometry CreateOpened(double radiusTop, double radiusBottom, double height)
    {
        return new XeoglCylinderGeometry()
        {
            RadiusTop = radiusTop,
            RadiusBottom = radiusBottom,
            Height = height,
            OpenEnded = true
        };
    }

    public static XeoglCylinderGeometry CreateOpened(double radiusTop, double radiusBottom, double height, int radialSegments, int heightSegments)
    {
        return new XeoglCylinderGeometry()
        {
            RadiusTop = radiusTop,
            RadiusBottom = radiusBottom,
            Height = height,
            RadialSegments = radialSegments,
            HeightSegments = heightSegments,
            OpenEnded = true
        };
    }

    public static XeoglCylinderGeometry CreateOpened(IFloat64Vector3D center, double radius, double height)
    {
        return new XeoglCylinderGeometry(center)
        {
            RadiusTop = radius,
            RadiusBottom = radius,
            Height = height,
            OpenEnded = true
        };
    }

    public static XeoglCylinderGeometry CreateOpened(IFloat64Vector3D center, double radius, double height, int radialSegments, int heightSegments)
    {
        return new XeoglCylinderGeometry(center)
        {
            RadiusTop = radius,
            RadiusBottom = radius,
            Height = height,
            RadialSegments = radialSegments,
            HeightSegments = heightSegments,
            OpenEnded = true
        };
    }

    public static XeoglCylinderGeometry CreateOpened(IFloat64Vector3D center, double radiusTop, double radiusBottom, double height)
    {
        return new XeoglCylinderGeometry(center)
        {
            RadiusTop = radiusTop,
            RadiusBottom = radiusBottom,
            Height = height,
            OpenEnded = true
        };
    }

    public static XeoglCylinderGeometry CreateOpened(IFloat64Vector3D center, double radiusTop, double radiusBottom, double height, int radialSegments, int heightSegments)
    {
        return new XeoglCylinderGeometry(center)
        {
            RadiusTop = radiusTop,
            RadiusBottom = radiusBottom,
            Height = height,
            RadialSegments = radialSegments,
            HeightSegments = heightSegments,
            OpenEnded = true
        };
    }

    public static XeoglCylinderGeometry CreateClosedCone(double radiusBottom, double height)
    {
        return new XeoglCylinderGeometry()
        {
            RadiusTop = 0.001,
            RadiusBottom = radiusBottom,
            Height = height
        };
    }

    public static XeoglCylinderGeometry CreateClosedCone(double radiusBottom, double height, int radialSegments, int heightSegments)
    {
        return new XeoglCylinderGeometry()
        {
            RadiusTop = 0.001,
            RadiusBottom = radiusBottom,
            Height = height,
            RadialSegments = radialSegments,
            HeightSegments = heightSegments
        };
    }

    public static XeoglCylinderGeometry CreateClosedCone(IFloat64Vector3D center, double radiusBottom, double height)
    {
        return new XeoglCylinderGeometry(center)
        {
            RadiusTop = 0.001,
            RadiusBottom = radiusBottom,
            Height = height
        };
    }

    public static XeoglCylinderGeometry CreateClosedCone(IFloat64Vector3D center, double radiusBottom, double height, int radialSegments, int heightSegments)
    {
        return new XeoglCylinderGeometry(center)
        {
            RadiusTop = 0.001,
            RadiusBottom = radiusBottom,
            Height = height,
            RadialSegments = radialSegments,
            HeightSegments = heightSegments
        };
    }

    public static XeoglCylinderGeometry CreateOpenedCone(double radiusBottom, double height)
    {
        return new XeoglCylinderGeometry()
        {
            RadiusTop = 0.001,
            RadiusBottom = radiusBottom,
            Height = height,
            OpenEnded = true
        };
    }

    public static XeoglCylinderGeometry CreateOpenedCone(double radiusBottom, double height, int radialSegments, int heightSegments)
    {
        return new XeoglCylinderGeometry()
        {
            RadiusTop = 0.001,
            RadiusBottom = radiusBottom,
            Height = height,
            RadialSegments = radialSegments,
            HeightSegments = heightSegments,
            OpenEnded = true
        };
    }

    public static XeoglCylinderGeometry CreateOpenedCone(IFloat64Vector3D center, double radiusBottom, double height)
    {
        return new XeoglCylinderGeometry(center)
        {
            RadiusTop = 0.001,
            RadiusBottom = radiusBottom,
            Height = height,
            OpenEnded = true
        };
    }

    public static XeoglCylinderGeometry CreateOpenedCone(IFloat64Vector3D center, double radiusBottom, double height, int radialSegments, int heightSegments)
    {
        return new XeoglCylinderGeometry(center)
        {
            RadiusTop = 0.001,
            RadiusBottom = radiusBottom,
            Height = height,
            RadialSegments = radialSegments,
            HeightSegments = heightSegments,
            OpenEnded = true
        };
    }

        
    public Float64Vector3DComposer Center { get; }
        = Float64Vector3DComposer.Create();

    public double RadiusTop { get; set; } = 1;

    public double RadiusBottom { get; set; } = 1;

    public double Height { get; set; } = 1;

    public int RadialSegments { get; set; } = 60;

    public int HeightSegments { get; set; } = 1;

    public bool OpenEnded { get; set; }

    public double LevelOfDetail { get; set; } = 1;

    public override string JavaScriptClassName => "CylinderGeometry";


    public XeoglCylinderGeometry()
    {
    }

    public XeoglCylinderGeometry(IFloat64Vector3D center)
    {
        Center.SetVector(center);
    }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
            .SetNumbersArrayValue("center", Center, Float64Vector3D.Zero)
            .SetValue("radiusTop", RadiusTop, 1)
            .SetValue("radiusBottom", RadiusBottom, 1)
            .SetValue("height", Height, 1)
            .SetValue("radialSegments", RadialSegments, 60)
            .SetValue("heightSegments", HeightSegments, 1)
            .SetValue("openEnded", OpenEnded, false)
            .SetValue("lod", LevelOfDetail, 1.0);
    }

    //public override string ToString()
    //{
    //    var composer = new XeoglAttributesTextComposer();

    //    UpdateAttributesComposer(composer);

    //    return composer
    //        .AppendXeoglConstructorCall(this)
    //        .ToString();
    //}
}