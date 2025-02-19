﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Geometry.Builtin;

/// <summary>
/// A PlaneGeometry is a parameterized Geometry that defines a
/// plane-shaped mesh for attached Meshes.
/// http://xeogl.org/docs/classes/PlaneGeometry.html
/// </summary>
public sealed class XeoglPlaneGeometry : XeoglBuiltinSolidGeometry
{
    public static XeoglPlaneGeometry Create(double size, int segments)
    {
        return new XeoglPlaneGeometry()
        {
            XSize = size,
            ZSize = size,
            XSegments = segments,
            ZSegments = segments
        };
    }

    public static XeoglPlaneGeometry Create(double size, int xSegments, int zSegments)
    {
        return new XeoglPlaneGeometry()
        {
            XSize = size,
            ZSize = size,
            XSegments = xSegments,
            ZSegments = zSegments
        };
    }

    public static XeoglPlaneGeometry Create(ILinFloat64Vector2D size, int segments)
    {
        return new XeoglPlaneGeometry()
        {
            XSize = size.X,
            ZSize = size.Y,
            XSegments = segments,
            ZSegments = segments
        };
    }

    public static XeoglPlaneGeometry Create(ILinFloat64Vector2D size, int xSegments, int zSegments)
    {
        return new XeoglPlaneGeometry()
        {
            XSize = size.X,
            ZSize = size.Y,
            XSegments = xSegments,
            ZSegments = zSegments
        };
    }

    public static XeoglPlaneGeometry Create(ILinFloat64Vector3D center, double size, int segments)
    {
        return new XeoglPlaneGeometry(center)
        {
            XSize = size,
            ZSize = size,
            XSegments = segments,
            ZSegments = segments
        };
    }

    public static XeoglPlaneGeometry Create(ILinFloat64Vector3D center, double size, int xSegments, int zSegments)
    {
        return new XeoglPlaneGeometry(center)
        {
            XSize = size,
            ZSize = size,
            XSegments = xSegments,
            ZSegments = zSegments
        };
    }

    public static XeoglPlaneGeometry Create(ILinFloat64Vector3D center, ILinFloat64Vector2D size, int segments)
    {
        return new XeoglPlaneGeometry(center)
        {
            XSize = size.X,
            ZSize = size.Y,
            XSegments = segments,
            ZSegments = segments
        };
    }

    public static XeoglPlaneGeometry Create(ILinFloat64Vector3D center, ILinFloat64Vector2D size, int xSegments, int zSegments)
    {
        return new XeoglPlaneGeometry(center)
        {
            XSize = size.X,
            ZSize = size.Y,
            XSegments = xSegments,
            ZSegments = zSegments
        };
    }


    public LinFloat64Vector3DComposer Center { get; }
        = LinFloat64Vector3DComposer.Create();

    public double XSize { get; set; } = 1;

    public double ZSize { get; set; } = 1;

    public int XSegments { get; set; } = 1;

    public int ZSegments { get; set; } = 1;

    public override string JavaScriptClassName => "PlaneGeometry";


    public XeoglPlaneGeometry()
    {
    }

    public XeoglPlaneGeometry(ILinFloat64Vector3D center)
    {
        Center.SetVector(center);
    }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.TriangleList)
            .SetNumbersArrayValue("center", Center, LinFloat64Vector3D.Zero)
            .SetValue("xSize", XSize, 1)
            .SetValue("ZSize", ZSize, 1)
            .SetValue("xSegments", XSegments, 1)
            .SetValue("zSegments", ZSegments, 1);
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