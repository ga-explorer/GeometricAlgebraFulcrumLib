using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Objects;
using TextComposerLib.Code.JavaScript;
using JsCodeComponentUtils = TextComposerLib.Code.JavaScript.Obsolete.JsCodeComponentUtils;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Lights
{
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

        public IFloat64Tuple3D Position { get; set; }
            = new Float64Tuple3D(0, 0, 0);

        public TjObject3D Target { get; set; }


        public override void UpdateComponentAttributes(JavaScriptAttributesDictionary attributesDictionary)
        {
            base.UpdateComponentAttributes(attributesDictionary);

            JsCodeComponentUtils.SetValue(attributesDictionary
                    .SetValue("castShadow", CastShadow, false)
                    .SetThreeJsVector3Value("position", Position, Float64Tuple3D.Zero), "target", Target, null);
        }
    }
}