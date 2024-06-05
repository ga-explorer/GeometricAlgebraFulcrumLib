using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Math;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Geometry;

/// <summary>
/// Creates meshes with axial symmetry like vases. The lathe rotates around the Y axis.
/// https://threejs.org/docs/#api/en/geometries/LatheGeometry
/// </summary>
public class TjLatheGeometry :
    TjBufferGeometryBase
{
    public override string JavaScriptClassName 
        => "LatheGeometry";
        
    public IReadOnlyList<TjVector2> XyPoints { get; }

    public int Segments { get; set; } = 12;

    public double PhiStart { get; set; } = 0d;

    public double PhiLength { get; set; } = 2d * System.Math.PI;


    public TjLatheGeometry(IReadOnlyList<TjVector2> xyPoints)
    {
        XyPoints = xyPoints;
    }

    public TjLatheGeometry(IEnumerable<ILinFloat64Vector2D> xyPoints)
    {
        XyPoints = xyPoints.Select(t => new TjVector2(t)).ToArray();
    }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateConstructorAttributes(attributesDictionary);

        var pointsText = 
            XyPoints
                .Select(t => t.GetJavaScriptVariableNameOrCode())
                .Concatenate(", ");

        attributesDictionary
            .SetValue("segments", Segments, 12)
            .SetValue("phiStart", PhiStart, 0d)
            .SetValue("phiLength", PhiLength, 2d * System.Math.PI)
            .SetTextValue("points", pointsText);
    }
}