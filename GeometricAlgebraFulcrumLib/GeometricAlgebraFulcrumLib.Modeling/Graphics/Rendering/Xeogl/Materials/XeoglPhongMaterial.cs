using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;
using JsCodeComponentUtils = GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript.Obsolete.JsCodeComponentUtils;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Materials;

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


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        JsCodeComponentUtils
            .SetValue(JsCodeComponentUtils.SetValue(JsCodeComponentUtils.SetValue(JsCodeComponentUtils.SetValue(JsCodeComponentUtils.SetValue(composer
                .SetRgbaNumbersArrayValue("ambient", AmbientColor.ToSystemDrawingColor(), Color.White.ToSystemDrawingColor())
                .SetRgbaNumbersArrayValue("diffuse", DiffuseColor.ToSystemDrawingColor(), Color.White.ToSystemDrawingColor())
                .SetRgbaNumbersArrayValue("specular", SpecularColor.ToSystemDrawingColor(), Color.White.ToSystemDrawingColor())
                .SetRgbNumbersArrayValue("emissive", EmissiveColor.ToSystemDrawingColor(), Color.Black.ToSystemDrawingColor()), "diffuseFresnel", DiffuseFresnel, null), "specularFresnel", SpecularFresnel, null), "emissiveFresnel", EmissiveFresnel, null), "reflectivityFresnel", ReflectivityFresnel, null), "alphaFresnel", AlphaFresnel, null)
            .SetValue("alpha", Alpha, 1)
            .SetValue("alphaCutoff", AlphaCutoff, 0.5)
            .SetValue("alphaMode", AlphaMode, XeoglAlphaMode.Blend)
            .SetValue("shininess", Shininess, 80)
            .SetValue("reflectivity", Reflectivity, 1)
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