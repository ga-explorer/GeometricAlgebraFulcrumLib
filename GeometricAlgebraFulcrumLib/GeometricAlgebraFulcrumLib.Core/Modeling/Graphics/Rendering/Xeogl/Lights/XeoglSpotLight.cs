using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Constants;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Lights;

/// <summary>
/// http://xeogl.org/docs/classes/SpotLight.html
/// </summary>
public sealed class XeoglSpotLight : XeoglLight
{
    public static LinFloat64Vector3D DefaultLightPosition { get; }
        = LinFloat64Vector3D.Create(1, 1, 1);

    public static LinFloat64Vector3D DefaultLightDirection { get; }
        = LinFloat64Vector3D.Create(0, -1, 0);


    public override string JavaScriptClassName => "SpotLight";

    public XeoglSpace Space { get; set; } 
        = XeoglSpace.View;

    public bool CastsShadow { get; set; }

    public double ConstantAttenuation { get; set; }

    public double LinearAttenuation { get; set; }

    public double QuadraticAttenuation { get; set; }

    public LinFloat64Vector3DComposer LightPosition { get; set; }
        = LinFloat64Vector3DComposer.Create(DefaultLightPosition);

    public LinFloat64Vector3DComposer LightDirection { get; set; }
        = LinFloat64Vector3DComposer.Create(DefaultLightDirection);


    public XeoglSpotLight()
    {
    }

    public XeoglSpotLight(ILinFloat64Vector3D lightPosition, ILinFloat64Vector3D lightDirection)
    {
        LightPosition.SetVector(lightPosition);
        LightDirection.SetVector(lightDirection);
    }


    public XeoglSpotLight SetAttenuation(double constantAttenuation, double linearAttenuation, double quadraticAttenuation)
    {
        ConstantAttenuation = constantAttenuation;
        LinearAttenuation = linearAttenuation;
        QuadraticAttenuation = quadraticAttenuation;

        return this;
    }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("space", Space, XeoglSpace.View)
            .SetValue("shadow", CastsShadow, false)
            .SetNumbersArrayValue("pos", LightPosition, DefaultLightPosition)
            .SetNumbersArrayValue("dir", LightDirection, DefaultLightDirection)
            .SetRgbNumbersArrayValue("color", LightColor.ToSystemDrawingColor(), DefaultLightColor.ToSystemDrawingColor())
            .SetValue("intensity", LightIntensity, 1)
            .SetValue("constantAttenuation", ConstantAttenuation, 0)
            .SetValue("linearAttenuation", LinearAttenuation, 0)
            .SetValue("quadraticAttenuation", QuadraticAttenuation, 0);
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