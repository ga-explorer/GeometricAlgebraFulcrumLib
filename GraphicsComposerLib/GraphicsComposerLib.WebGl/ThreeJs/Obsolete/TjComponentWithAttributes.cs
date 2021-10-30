using TextComposerLib.Code.JavaScript;
using TextComposerLib.Code.JavaScript.Obsolete;

namespace GraphicsComposerLib.WebGl.ThreeJs.Obsolete
{
    /// <summary>
    /// This class represents a Three.js component like a geometry, camera, light, etc.
    /// </summary>
    public abstract class TjComponentWithAttributes : 
        JsCodeComponentWithAttributes
    {
        protected TjComponentWithAttributes()
        {
            DefaultParentName = "THREE";
        }


        protected override JavaScriptAttributesDictionary CreateAttributesDictionary()
        {
            return new ThreeJsAttributesDictionary();
        }
    }
}
