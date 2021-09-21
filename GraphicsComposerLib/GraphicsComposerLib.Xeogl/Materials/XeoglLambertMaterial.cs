using System.Drawing;
using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Materials
{
    public sealed class XeoglLambertMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "LambertMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Lambert;

        public Color AmbientColor { get; set; } = Color.White;

        public Color DiffuseColor { get; set; } = Color.White;

        public Color EmissiveColor { get; set; } = Color.Black;

        public double Alpha { get; set; } = 1;

        public int LinePixelsWidth { get; set; } = 1;

        public int PointPixelsSize { get; set; } = 1;

        public bool RenderBackfaces { get; set; }

        public XeoglWindingDirection FrontfaceWindingDirection { get; set; }
            = XeoglWindingDirection.CounterClockwise;


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValueRgba("ambient", AmbientColor, Color.White)
                .SetAttributeValueRgba("color", DiffuseColor, Color.White)
                .SetAttributeValueRgb("emissive", EmissiveColor, Color.Black)
                .SetAttributeValue("alpha", Alpha, 1)
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