using System.Drawing;
using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Materials
{
    /// <summary>
    /// http://xeogl.org/docs/classes/PhongMaterial.html
    /// </summary>
    public sealed class XeoglPhongMaterial : XeoglMaterial
    {
        public static XeoglPhongMaterial CreateDiffuse(Color color)
        {
            return new XeoglPhongMaterial()
            {
                DiffuseColor = color
            };
        }

        public static XeoglPhongMaterial CreateEmissive(Color color)
        {
            return new XeoglPhongMaterial()
            {
                EmissiveColor = color
            };
        }

        //TODO: Complete the remaining properties

        public override string JavaScriptClassName => "PhongMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Phong;

        public Color AmbientColor { get; set; } = Color.White;

        public Color DiffuseColor { get; set; } = Color.White;

        public Color SpecularColor { get; set; } = Color.White;

        public Color EmissiveColor { get; set; } = Color.Black;

        public double Alpha { get; set; } = 1;

        public XeoglAlphaMode AlphaMode { get; set; } = XeoglAlphaMode.Blend;

        public double AlphaCutoff { get; set; } = 0.5;

        public double Shininess { get; set; } = 80;

        public double Reflectivity { get; set; } = 1;

        public XeoglFresnelEffect DiffuseFresnel { get; set; }

        public XeoglFresnelEffect SpecularFresnel { get; set; }

        public XeoglFresnelEffect EmissiveFresnel { get; set; }

        public XeoglFresnelEffect AlphaFresnel { get; set; }

        public XeoglFresnelEffect ReflectivityFresnel { get; set; }

        public int LinePixelsWidth { get; set; } = 1;

        public int PointPixelsSize { get; set; } = 1;

        public bool RenderBackfaces { get; set; }

        public XeoglWindingDirection FrontfaceWindingDirection { get; set; } 
            = XeoglWindingDirection.CounterClockwise;


        public XeoglPhongMaterial()
        {
        }

        public XeoglPhongMaterial(XeoglPhongMaterial sourceMaterial)
        {
            AmbientColor = sourceMaterial.AmbientColor;
            DiffuseColor = sourceMaterial.DiffuseColor;
            SpecularColor = sourceMaterial.SpecularColor;
            EmissiveColor = sourceMaterial.EmissiveColor;
            Alpha = sourceMaterial.Alpha;
            AlphaMode = sourceMaterial.AlphaMode;
            AlphaCutoff = sourceMaterial.AlphaCutoff;
            Shininess = sourceMaterial.Shininess;
            Reflectivity = sourceMaterial.Reflectivity;
            DiffuseFresnel = sourceMaterial.DiffuseFresnel;
            SpecularFresnel = sourceMaterial.SpecularFresnel;
            EmissiveFresnel = sourceMaterial.EmissiveFresnel;
            AlphaFresnel = sourceMaterial.AlphaFresnel;
            ReflectivityFresnel = sourceMaterial.ReflectivityFresnel;
            LinePixelsWidth = sourceMaterial.LinePixelsWidth;
            PointPixelsSize = sourceMaterial.PointPixelsSize;
            RenderBackfaces = sourceMaterial.RenderBackfaces;
            FrontfaceWindingDirection = sourceMaterial.FrontfaceWindingDirection;
        }


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValueRgba("ambient", AmbientColor, Color.White)
                .SetAttributeValueRgba("diffuse", DiffuseColor, Color.White)
                .SetAttributeValueRgba("specular", SpecularColor, Color.White)
                .SetAttributeValueRgb("emissive", EmissiveColor, Color.Black)
                .SetAttributeValue("diffuseFresnel", DiffuseFresnel, null)
                .SetAttributeValue("specularFresnel", SpecularFresnel, null)
                .SetAttributeValue("emissiveFresnel", EmissiveFresnel, null)
                .SetAttributeValue("reflectivityFresnel", ReflectivityFresnel, null)
                .SetAttributeValue("alphaFresnel", AlphaFresnel, null)
                .SetAttributeValue("alpha", Alpha, 1)
                .SetAttributeValue("alphaCutoff", AlphaCutoff, 0.5)
                .SetAttributeValue("alphaMode", AlphaMode, XeoglAlphaMode.Blend)
                .SetAttributeValue("shininess", Shininess, 80)
                .SetAttributeValue("reflectivity", Reflectivity, 1)
                .SetAttributeValue("lineWidth", LinePixelsWidth, 1)
                .SetAttributeValue("pointSize", PointPixelsSize, 1)
                .SetAttributeValue("backfaces", RenderBackfaces, false)
                .SetAttributeValue("frontface", FrontfaceWindingDirection, XeoglWindingDirection.CounterClockwise);
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