﻿using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Geometry.Builtin;

/// <summary>
/// An AABBGeometry is a Geometry that shows the extents of a
/// World-space axis-aligned bounding box (AABB).
/// http://xeogl.org/docs/classes/AABBGeometry.html
/// </summary>
public sealed class XeoglAaBbGeometry 
    : XeoglGeometry
{
    public string Target { get; set; }

    public Float64BoundingBoxComposer3D TargetAaBb { get; }
        = Float64BoundingBoxComposer3D.Create();

    public override string JavaScriptClassName => "AABBGeometry";


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetTextValue("target", Target)
            .SetTextValue("targetAABB", TargetAaBb.ToJavaScriptNumbersArrayText(), "[]");
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