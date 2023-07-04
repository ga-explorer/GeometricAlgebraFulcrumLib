using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GraphicsComposerLib.Rendering.Xeogl.Constants;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Lights
{
    /// <summary>
    /// http://xeogl.org/docs/classes/DirLight.html
    /// </summary>
    public sealed class XeoglDirectionalLight : XeoglLight
    {
        private static Float64Vector3D DefaultLightDirection { get; }
            = Float64Vector3D.Create(1, 1, 1);


        public override string JavaScriptClassName => "DirLight";

        public XeoglSpace Space { get; set; } 
            = XeoglSpace.View;

        public bool CastsShadow { get; set; }

        public Float64Vector3DComposer LightDirection { get; set; }
            = Float64Vector3DComposer.Create(DefaultLightDirection);


        public XeoglDirectionalLight()
        {
        }

        public XeoglDirectionalLight(IFloat64Vector3D lightDirection)
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
}
