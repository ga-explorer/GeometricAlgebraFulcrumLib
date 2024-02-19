using TextComposerLib.Code.JavaScript;
using TextComposerLib.Code.JavaScript.Obsolete;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl;

/// <summary>
/// This class represents a xeogl component like a geometry, camera, light, etc.
/// </summary>
public abstract class XeoglComponent : 
    JsCodeComponentWithAttributes
{
    protected XeoglComponent()
    {
        DefaultParentName = "xeogl";
    }


    protected override JavaScriptAttributesDictionary CreateAttributesDictionary()
    {
        return new XeoglComponentAttributesDictionary();
    }
}