using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GraphicsComposerLib.Rendering.Xeogl.Constants;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Lights
{
    /// <summary>
    /// http://xeogl.org/docs/classes/PointLight.html
    /// </summary>
    public sealed class XeoglPointLight : XeoglLight
    {
        private static Float64Vector3D DefaultLightPosition { get; }
            = Float64Vector3D.Create(1, 1, 1);


        public override string JavaScriptClassName => "PointLight";

        public XeoglSpace Space { get; set; } 
            = XeoglSpace.View;

        public bool CastsShadow { get; set; }

        public double ConstantAttenuation { get; set; }

        public double LinearAttenuation { get; set; }

        public double QuadraticAttenuation { get; set; }

        public Float64Vector3DComposer LightPosition { get; set; }
            = Float64Vector3DComposer.Create(DefaultLightPosition);


        public XeoglPointLight()
        {
        }

        public XeoglPointLight(IFloat64Tuple3D lightPosition)
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
}