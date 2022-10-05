using GraphicsComposerLib.Rendering.Colors;
using GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Objects;
using SixLabors.ImageSharp;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Math
{
    /// <summary>
    /// https://threejs.org/docs/#api/en/math/Color
    /// </summary>
    public sealed class TjColor : 
        TjComponentSimple, 
        ITjSceneBackgroundObject
    {
        public override string JavaScriptClassName 
            => "Color";

        public Color ColorValue { get; }

        public int ColorIntegerValue
            => ColorValue.ToSystemDrawingColor().ToJavaScriptRgbInteger();

        public string ColorHexTextValue
            => ColorValue.ToSystemDrawingColor().ToJavaScriptRgbHexText();


        public TjColor()
        {
            ColorValue = Color.White;
        }

        public TjColor(double r, double g, double b)
        {
            ColorValue = Color.FromRgb(
                (byte) (r * 255d),
                (byte) (g * 255d),
                (byte) (b * 255d)
            );
        }

        public TjColor(int r, int g, int b)
        {
            ColorValue = Color.FromRgb(
                (byte) r, 
                (byte) g, 
                (byte) b
            );
        }


        protected override string GetConstructorArgumentsText()
        {
            return ColorHexTextValue;
        }

        protected override string GetSetMethodArgumentsText()
        {
            return ColorHexTextValue;
        }
    }
}