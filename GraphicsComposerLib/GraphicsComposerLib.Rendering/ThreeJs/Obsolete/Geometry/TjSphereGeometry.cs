using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Geometry
{
    /// <summary>
    /// https://threejs.org/docs/#api/en/geometries/SphereGeometry
    /// </summary>
    public class TjSphereGeometry :
        TjBufferGeometryBase
    {
        public override string JavaScriptClassName 
            => "SphereGeometry";

        public double Radius { get; set; } = 1d;

        public int WidthSegments { get; set; } = 32;

        public int HeightSegments { get; set; } = 16;

        public double PhiStart { get; set; } = 0d;

        public double PhiLength { get; set; } = 2d * System.Math.PI;

        public double ThetaStart { get; set; } = 0d;

        public double ThetaLength { get; set; } = System.Math.PI;


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
        {
            base.UpdateConstructorAttributes(attributesDictionary);

            attributesDictionary
                .SetValue("radius", Radius, 1d)
                .SetValue("widthSegments", WidthSegments, 32)
                .SetValue("heightSegments", HeightSegments, 16)
                .SetValue("phiStart", PhiStart, 0d)
                .SetValue("phiLength", PhiLength, 2d * System.Math.PI)
                .SetValue("thetaStart", ThetaStart, 0d)
                .SetValue("thetaLength", ThetaLength, System.Math.PI);
        }
    }
}