using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Constants;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Lights;

/// <summary>
/// http://xeogl.org/docs/classes/DirLight.html
/// </summary>
public sealed class XeoglDirectionalLight : XeoglLight
{
    private static LinFloat64Vector3D DefaultLightDirection { get; }
        = LinFloat64Vector3D.Create(1, 1, 1);


    public override string JavaScriptClassName => "DirLight";

    public XeoglSpace Space { get; set; } 
        = XeoglSpace.View;

    public bool CastsShadow { get; set; }

    public LinFloat64Vector3DComposer LightDirection { get; set; }
        = LinFloat64Vector3DComposer.Create(DefaultLightDirection);


    public XeoglDirectionalLight()
    {
    }

    public XeoglDirectionalLight(ILinFloat64Vector3D lightDirection)
    {
        LightDirection.SetVector(lightDirection);
    }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("space", Space, XeoglSpace.View)
            .SetValue("shadow", CastsShadow, false)
            .SetNumbersArrayValue("dir", LightDirection, DefaultLightDirection);
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