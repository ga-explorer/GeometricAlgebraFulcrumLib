using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Lights;

/// <summary>
/// http://xeogl.org/docs/classes/PointLight.html
/// </summary>
public sealed class XeoglPointLight : XeoglLight
{
    private static LinFloat64Vector3D DefaultLightPosition { get; }
        = LinFloat64Vector3D.Create(1, 1, 1);


    public override string JavaScriptClassName => "PointLight";

    public XeoglSpace Space { get; set; } 
        = XeoglSpace.View;

    public bool CastsShadow { get; set; }

    public double ConstantAttenuation { get; set; }

    public double LinearAttenuation { get; set; }

    public double QuadraticAttenuation { get; set; }

    public LinFloat64Vector3DComposer LightPosition { get; set; }
        = LinFloat64Vector3DComposer.Create(DefaultLightPosition);


    public XeoglPointLight()
    {
    }

    public XeoglPointLight(ILinFloat64Vector3D lightPosition)
    {
        LightPosition.SetVector(lightPosition);
    }


    public XeoglPointLight SetAttenuation(double constantAttenuation, double linearAttenuation, double quadraticAttenuation)
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