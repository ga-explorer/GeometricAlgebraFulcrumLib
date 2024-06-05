namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Geometry.Builtin;

/// <summary>
/// A TeapotGeometry defines a Utah Teapot geometry for attached Meshes.
/// http://xeogl.org/docs/classes/TeapotGeometry.html
/// </summary>
public sealed class XeoglTeapotGeometry
    : XeoglGeometry
{
    public override string JavaScriptClassName => "TeapotGeometry";


    //protected override void UpdateAttributesComposer(XeoglAttributesTextComposer composer)
    //{
    //    base.UpdateAttributesComposer(composer);

    //    //composer
    //    //    .SetAttributeValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles);
    //}

    //public override string ToString()
    //{
    //    var composer = new XeoglAttributesTextComposer();

    //    UpdateAttributesComposer(composer);

    //    return composer
    //        .AppendXeoglConstructorCall(this)
    //        .ToString();
    //}
}