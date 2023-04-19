using GraphicsComposerLib.Rendering.Colors;
using GraphicsComposerLib.Rendering.Xeogl.Constants;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Materials
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


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetRgbaNumbersArrayValue("ambient", AmbientColor.ToSystemDrawingColor(), Color.White.ToSystemDrawingColor())
                .SetRgbaNumbersArrayValue("color", DiffuseColor.ToSystemDrawingColor(), Color.White.ToSystemDrawingColor())
                .SetRgbNumbersArrayValue("emissive", EmissiveColor.ToSystemDrawingColor(), Color.Black.ToSystemDrawingColor())
                .SetValue("alpha", Alpha, 1)
                .SetValue("lineWidth", LinePixelsWidth, 1)
                .SetValue("pointSize", PointPixelsSize, 1)
                .SetValue("backfaces", RenderBackfaces, false)
                .SetValue("frontface", FrontfaceWindingDirection, XeoglWindingDirection.CounterClockwise);
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