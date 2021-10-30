using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicMath.Tuples.Mutable;
using GraphicsComposerLib.WebGl.Xeogl.Constants;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.WebGl.Xeogl.Lights
{
    /// <summary>
    /// http://xeogl.org/docs/classes/SpotLight.html
    /// </summary>
    public sealed class XeoglSpotLight : XeoglLight
    {
        public static Tuple3D DefaultLightPosition { get; }
            = new Tuple3D(1, 1, 1);

        public static Tuple3D DefaultLightDirection { get; }
            = new Tuple3D(0, -1, 0);


        public override string JavaScriptClassName => "SpotLight";

        public XeoglSpace Space { get; set; } 
            = XeoglSpace.View;

        public bool CastsShadow { get; set; }

        public double ConstantAttenuation { get; set; }

        public double LinearAttenuation { get; set; }

        public double QuadraticAttenuation { get; set; }

        public MutableTuple3D LightPosition { get; set; }
            = new MutableTuple3D(DefaultLightPosition);

        public MutableTuple3D LightDirection { get; set; }
            = new MutableTuple3D(DefaultLightDirection);


        public XeoglSpotLight()
        {
        }

        public XeoglSpotLight(ITuple3D lightPosition, ITuple3D lightDirection)
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
                .SetRgbNumbersArrayValue("color", LightColor, DefaultLightColor)
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