﻿using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Points;
using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Geometry.Primitives;

public sealed class XeoglPointsGeometry : XeoglPrimitivesGeometry
{
    public IGraphicsPointGeometry3D GraphicsPointsGeometry { get; set; }

    public override IGraphicsPrimitiveGeometry3D GraphicsGeometry
        => GraphicsPointsGeometry;


    public XeoglPointsGeometry()
    {
    }

    public XeoglPointsGeometry(IGraphicsPointGeometry3D geometryData)
    {
        GraphicsPointsGeometry = geometryData;
    }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
            .SetTextValue("positions", VertexPoints.ToJavaScriptNumbersArrayText(" // Vertex Position "), "[]")
            .SetTextValue("indices", VertexIndices.ToJavaScriptNumbersArrayText(), "[]");
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