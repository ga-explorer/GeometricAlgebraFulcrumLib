using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Geometry
{
    /// <summary>
    /// A polyhedron is a solid in three dimensions with flat faces. This
    /// class will take an array of vertices, project them onto a sphere,
    /// and then divide them up to the desired level of detail. This class
    /// is used by DodecahedronGeometry, IcosahedronGeometry, OctahedronGeometry,
    /// and TetrahedronGeometry to generate their respective geometries.
    /// https://threejs.org/docs/#api/en/geometries/PolyhedronGeometry
    /// </summary>
    public abstract class TjPolyhedronGeometryBase :
        TjBufferGeometryBase
    {
        public double Radius { get; set; } = 1d;

        public int Detail { get; set; } = 0;


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
        {
            base.UpdateConstructorAttributes(attributesDictionary);

            attributesDictionary
                .SetValue("radius", Radius, 1d)
                .SetValue("detail", Detail, 0);
        }
    }
}