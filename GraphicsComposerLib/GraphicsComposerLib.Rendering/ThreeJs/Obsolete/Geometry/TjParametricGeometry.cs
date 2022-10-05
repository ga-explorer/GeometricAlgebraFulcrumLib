using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Geometry
{
    /// <summary>
    /// Generate geometry representing a parametric surface.
    /// https://threejs.org/docs/#api/en/geometries/ParametricGeometry
    /// </summary>
    public class TjParametricGeometry :
        TjBufferGeometryBase
    {
        public override string JavaScriptClassName 
            => "ParametricGeometry";

        public string ParametricFunctionCode { get; set; }

        public int SlicesCount { get; set; }

        public int StacksCount { get; set; }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
        {
            base.UpdateConstructorAttributes(attributesDictionary);

            attributesDictionary
                .SetValue("slices", SlicesCount)
                .SetValue("stacks", StacksCount)
                .SetTextValue("func", ParametricFunctionCode);
        }
    }
}