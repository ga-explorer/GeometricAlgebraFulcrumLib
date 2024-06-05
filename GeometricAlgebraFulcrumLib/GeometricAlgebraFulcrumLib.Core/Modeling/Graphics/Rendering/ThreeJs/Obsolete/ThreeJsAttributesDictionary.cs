using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete;

/// <summary>
/// This class is internally used to compose code of individual Three.js components
/// </summary>
public class ThreeJsAttributesDictionary :
    JavaScriptAttributesDictionary
{
    public ThreeJsAttributesDictionary()
    {
    }


    //public JsCodeComponentComposer RemoveTransformAttributes()
    //{
    //    RemoveAttributes(
    //        "matrix",
    //        "rotation",
    //        "scale",
    //        "position",
    //        "quaternion"
    //    );

    //    return this;
    //}

    //public JsCodeComponentComposer SetTransformAttributes(IXeoglTransform transform)
    //{
    //    RemoveTransformAttributes();

    //    if (ReferenceEquals(transform, null))
    //        return this;

    //    if (transform.ContainsMatrix)
    //    {
    //        SetAttributeValue("matrix", transform.GetMatrixText());
    //        return this;
    //    }

    //    if (transform.ContainsQuaternion)
    //        SetAttributeValue("quaternion", transform.GetQuaternionText());

    //    if (transform.ContainsRotate)
    //        SetAttributeValue("rotation", transform.GetRotateText());

    //    if (transform.ContainsScale)
    //        SetAttributeValue("scale", transform.GetScaleText());

    //    if (transform.ContainsTranslate)
    //        SetAttributeValue("position", transform.GetTranslateText());

    //    return this;
    //}

    //public XeoglAttributesTextComposer SetAttributeValue(string key, XeoglGeometry value)
    //{
    //    base.SetAttributeValue(
    //        key,
    //        value?.ToString() ?? string.Empty
    //    );

    //    return this;
    //}
}