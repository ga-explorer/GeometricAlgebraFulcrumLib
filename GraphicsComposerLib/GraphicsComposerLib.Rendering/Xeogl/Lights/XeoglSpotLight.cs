using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;
using GraphicsComposerLib.Rendering.Colors;
using GraphicsComposerLib.Rendering.Xeogl.Constants;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Lights
{
    /// <summary>
    /// http://xeogl.org/docs/classes/SpotLight.html
    /// </summary>
    public sealed class XeoglSpotLight : XeoglLight
    {
        public static Float64Tuple3D DefaultLightPosition { get; }
            = new Float64Tuple3D(1, 1, 1);

        public static Float64Tuple3D DefaultLightDirection { get; }
            = new Float64Tuple3D(0, -1, 0);


        public override string JavaScriptClassName => "SpotLight";

        public XeoglSpace Space { get; set; } 
            = XeoglSpace.View;

        public bool CastsShadow { get; set; }

        public double ConstantAttenuation { get; set; }

        public double LinearAttenuation { get; set; }

        public double QuadraticAttenuation { get; set; }

        public MutableFloat64Tuple3D LightPosition { get; set; }
            = new MutableFloat64Tuple3D(DefaultLightPosition);

        public MutableFloat64Tuple3D LightDirection { get; set; }
            = new MutableFloat64Tuple3D(DefaultLightDirection);


        public XeoglSpotLight()
        {
        }

        public XeoglSpotLight(IFloat64Tuple3D lightPosition, IFloat64Tuple3D lightDirection)
        {
            LightPosition.SetTuple(lightPosition);
            LightDirection.SetTuple(lightDirection);
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
}