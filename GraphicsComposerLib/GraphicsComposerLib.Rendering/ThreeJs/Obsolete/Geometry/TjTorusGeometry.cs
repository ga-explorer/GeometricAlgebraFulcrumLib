using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Geometry
{
    /// <summary>
    /// https://threejs.org/docs/#api/en/geometries/TorusGeometry
    /// </summary>
    public class TjTorusGeometry :
        TjBufferGeometryBase
    {
        public override string JavaScriptClassName 
            => "TorusGeometry";

        public double Radius { get; set; } = 1d;

        public double TubeRadius { get; set; } = 0.4d;

        public int RadialSegments { get; set; } = 8;

        public int TubularSegments { get; set; } = 64;

        public double CentralAngle { get; set; } = 2d * System.Math.PI;


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
        {
            base.UpdateConstructorAttributes(attributesDictionary);

            attributesDictionary
                .SetValue("radius", Radius, 1d)
                .SetValue("tube", TubeRadius, 0.4d)
                .SetValue("radialSegments", RadialSegments, 8)
                .SetValue("tubularSegments", TubularSegments, 64)
                .SetValue("arc", CentralAngle, 2d * System.Math.PI);
        }
    }
}