using System.Drawing;
using GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Objects;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Math
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
            => ColorValue.ToJavaScriptRgbInteger();

        public string ColorHexTextValue
            => ColorValue.ToJavaScriptRgbHexText();


        public TjColor()
        {
            ColorValue = Color.White;
        }

        public TjColor(double r, double g, double b)
        {
            ColorValue = Color.FromArgb(
                (int) (r * 255d),
                (int) (g * 255d),
                (int) (b * 255d)
            );
        }

        public TjColor(int r, int g, int b)
        {
            ColorValue = Color.FromArgb(r, g, b);
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