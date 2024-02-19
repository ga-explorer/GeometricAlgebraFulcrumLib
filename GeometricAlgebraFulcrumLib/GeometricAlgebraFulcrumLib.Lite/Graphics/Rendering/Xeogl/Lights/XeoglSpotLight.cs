using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Constants;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using TextComposerLib.Code.JavaScript;
using WebComposerLib.Colors;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Lights;

/// <summary>
/// http://xeogl.org/docs/classes/SpotLight.html
/// </summary>
public sealed class XeoglSpotLight : XeoglLight
{
    public static Float64Vector3D DefaultLightPosition { get; }
        = Float64Vector3D.Create(1, 1, 1);

    public static Float64Vector3D DefaultLightDirection { get; }
        = Float64Vector3D.Create(0, -1, 0);


    public override string JavaScriptClassName => "SpotLight";

    public XeoglSpace Space { get; set; } 
        = XeoglSpace.View;

    public bool CastsShadow { get; set; }

    public double ConstantAttenuation { get; set; }

    public double LinearAttenuation { get; set; }

    public double QuadraticAttenuation { get; set; }

    public Float64Vector3DComposer LightPosition { get; set; }
        = Float64Vector3DComposer.Create(DefaultLightPosition);

    public Float64Vector3DComposer LightDirection { get; set; }
        = Float64Vector3DComposer.Create(DefaultLightDirection);


    public XeoglSpotLight()
    {
    }

    public XeoglSpotLight(IFloat64Vector3D lightPosition, IFloat64Vector3D lightDirection)
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