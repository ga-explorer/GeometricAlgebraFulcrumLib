using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Transforms;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Objects;

/// <summary>
/// http://xeogl.org/docs/classes/Object.html
/// </summary>
public abstract class XeoglObject : 
    XeoglComponent
{
    private readonly string _colorizeDefaultValue =
        Color.White.ToSystemDrawingColor().ToJavaScriptRgbNumbersArrayText();


    public string ParentSceneName { get; internal set; }
        = string.Empty;

    public string EntityType { get; set; }

    public IXeoglTransform Transform { get; set; }
        = new XeoglERotateScaleTranslateTransform();

    public bool Visible { get; set; } = true;

    public bool EnableCulling { get; set; }

    public bool EnablePicking { get; set; } = true;

    public bool EnableClipping { get; set; } = true;

    public bool EnableColliding { get; set; } = true;

    public bool EnableShadowCasting { get; set; } = true;

    public bool EnableShadowReceiving { get; set; } = true;

    public bool IsOutlined { get; set; }

    public bool IsGhosted { get; set; }

    public bool IsHighlighted { get; set; }

    public bool IsSelected { get; set; }

    public bool AreEdgesEmphasized { get; set; }

    public bool AaBbVisible { get; set; }

    public Color ColorFactor { get; set; } = Color.White;

    public bool InheritStates { get; set; } = true;


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        var xeoglComposer = (XeoglComponentAttributesDictionary) composer;

        xeoglComposer
            .SetTextValue("entityType", EntityType.DoubleQuote(), "\"\"");

        xeoglComposer
            .SetTransformAttributes(Transform)
            .SetValue("visible", Visible, true)
            .SetValue("culled", EnableCulling, false)
            .SetValue("pickable", EnablePicking, true)
            .SetValue("clippable", EnableClipping, true)
            .SetValue("collidable", EnableColliding, true)
            .SetValue("castShadow", EnableShadowCasting, true)
            .SetValue("receiveShadow", EnableShadowReceiving, true)
            .SetValue("outlined", IsOutlined, false)
            .SetValue("ghosted", IsGhosted, false)
            .SetValue("highlighted", IsHighlighted, false)
            .SetValue("selected", IsSelected, false)
            .SetValue("edges", AreEdgesEmphasized, false)
            .SetValue("aabbVisible", AaBbVisible, false)
            .SetValue("inheritStates", InheritStates, true)
            .SetRgbNumbersArrayValue("colorize", ColorFactor.ToSystemDrawingColor(), Color.White.ToSystemDrawingColor())
            .SetValue("opacity", ColorFactor.ToPixel<Rgba32>().A / 255.0d, 1);
    }
}