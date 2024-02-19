

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Materials;

public static class XeoglMaterialsUtils
{
    public static XeoglLambertMaterial ToXeoglEmissiveLambertMaterial(this Color color)
    {
        return new XeoglLambertMaterial()
        {
            EmissiveColor = color
        };
    }

    public static XeoglPhongMaterial ToXeoglEmissivePhongMaterial(this Color color)
    {
        return new XeoglPhongMaterial()
        {
            EmissiveColor = color
        };
    }

    public static XeoglPhongMaterial ToXeoglEmissivePhongMaterial(this Color color, int linePixelsWidth)
    {
        return new XeoglPhongMaterial()
        {
            EmissiveColor = color,
            LinePixelsWidth = linePixelsWidth
        };
    }
}