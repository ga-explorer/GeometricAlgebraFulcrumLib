using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Objects;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using JsCodeComponentUtils = GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript.Obsolete.JsCodeComponentUtils;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Lights;

/// <summary>
/// A light that gets emitted in a specific direction. This light will
/// behave as though it is infinitely far away and the rays produced
/// from it are all parallel. The common use case for this is to simulate
/// daylight; the sun is far enough away that its position can be considered
/// to be infinite, and all light rays coming from it are parallel.
/// https://threejs.org/docs/#api/en/lights/DirectionalLight
/// </summary>
public class TjTargetDirectLight :
    TjLight
{
    public override string JavaScriptClassName 
        => "DirectionalLight";

    public bool CastShadow { get; set; }
        = false;

    public ILinFloat64Vector3D Position { get; set; }
        = LinFloat64Vector3D.Create(0, 0, 0);

    public TjObject3D Target { get; set; }


    public override void UpdateComponentAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateComponentAttributes(attributesDictionary);

        JsCodeComponentUtils.SetValue(attributesDictionary
            .SetValue("castShadow", CastShadow, false)
            .SetThreeJsVector3Value("position", Position, LinFloat64Vector3D.Zero), "target", Target, null);
    }
}