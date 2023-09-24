using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Geometry
{
    /// <summary>
    /// https://threejs.org/docs/#api/en/geometries/ConeGeometry
    /// </summary>
    public class TjConeGeometry :
        TjBufferGeometryBase
    {
        public override string JavaScriptClassName 
            => "ConeGeometry";

        public double Radius { get; set; } = 1d;

        public double Height { get; set; } = 1d;

        public int RadialSegments { get; set; } = 8;

        public int HeightSegments { get; set; } = 1;

        public bool OpenEnded { get; set; } = false;

        public double ThetaStart { get; set; } = 0d;

        public double ThetaLength { get; set; } = 2d * System.Math.PI;

        
        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
        {
            base.UpdateConstructorAttributes(attributesDictionary);

            attributesDictionary
                .SetValue("radius", Radius, 1d)
                .SetValue("height", Height, 1d)
                .SetValue("radialSegments", RadialSegments, 8)
                .SetValue("heightSegments", HeightSegments, 1)
                .SetValue("openEnded", OpenEnded, false)
                .SetValue("thetaStart", ThetaStart, 0d)
                .SetValue("thetaLength", ThetaLength, 2d * System.Math.PI);
        }
    }
}