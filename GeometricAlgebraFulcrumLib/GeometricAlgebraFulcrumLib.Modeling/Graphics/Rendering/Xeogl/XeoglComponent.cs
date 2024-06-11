using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript.Obsolete;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl;

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