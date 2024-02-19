using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Geometry;

/// <summary>
/// Creates a torus knot, the particular shape of which is defined by a pair
/// of co-prime integers, p and q. If p and q are not co-prime, the result will
/// be a torus link.
/// https://threejs.org/docs/#api/en/geometries/TorusKnotGeometry
/// </summary>
public class TjTorusKnotGeometry :
    TjBufferGeometryBase
{
    public override string JavaScriptClassName 
        => "TorusKnotGeometry";

    public double Radius { get; set; } = 1d;

    public double TubeRadius { get; set; } = 0.4d;

    public int RadialSegments { get; set; } = 8;

    public int TubularSegments { get; set; } = 64;

    public int PValue { get; set; } = 2;

    public int QValue { get; set; } = 3;


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateConstructorAttributes(attributesDictionary);

        attributesDictionary
            .SetValue("radius", Radius, 1d)
            .SetValue("tube", TubeRadius, 0.4d)
            .SetValue("radialSegments", RadialSegments, 8)
            .SetValue("tubularSegments", TubularSegments, 64)
            .SetValue("p", PValue, 2)
            .SetValue("q", QValue, 3);
    }
}