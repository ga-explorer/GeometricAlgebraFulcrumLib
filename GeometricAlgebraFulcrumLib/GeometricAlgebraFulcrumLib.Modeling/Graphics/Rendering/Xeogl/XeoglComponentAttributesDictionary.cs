using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Transforms;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl;

/// <summary>
/// This class is internally used to compose code of individual xeogl components
/// </summary>
///
public class XeoglComponentAttributesDictionary :
    JavaScriptAttributesDictionary
{
    public XeoglComponentAttributesDictionary()
    {
    }


    public JavaScriptAttributesDictionary RemoveTransformAttributes()
    {
        RemoveAttributes(
            "matrix",
            "rotation",
            "scale",
            "position",
            "quaternion"
        );

        return this;
    }

    public JavaScriptAttributesDictionary SetTransformAttributes(IXeoglTransform transform)
    {
        RemoveTransformAttributes();

        if (ReferenceEquals(transform, null))
            return this;

        if (transform.ContainsMatrix)
        {
            SetTextValue("matrix", transform.GetMatrixText());
            return this;
        }

        if (transform.ContainsQuaternion)
            SetTextValue("quaternion", transform.GetQuaternionText());

        if (transform.ContainsRotate)
            SetTextValue("rotation", transform.GetRotateText());

        if (transform.ContainsScale)
            SetTextValue("scale", transform.GetScaleText());

        if (transform.ContainsTranslate)
            SetTextValue("position", transform.GetTranslateText());

        return this;
    }

    //public XeoglAttributesTextComposer SetAttributeValue(string key, XeoglGeometry value)
    //{
    //    base.SetAttributeValue(
    //        key,
    //        value?.ToString() ?? string.Empty
    //    );

    //    return this;
    //}
}