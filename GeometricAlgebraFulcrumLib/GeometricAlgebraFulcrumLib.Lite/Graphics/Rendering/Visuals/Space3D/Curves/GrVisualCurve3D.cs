using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Curves;

public abstract class GrVisualCurve3D :
    GrVisualElement3D
{
    public GrVisualCurveStyle3D Style { get; }

    public abstract double ArcLength { get; }


    protected GrVisualCurve3D(string name, GrVisualCurveStyle3D style) 
        : base(name)
    {
        Style = style;
    }


    public abstract IPointsPath3D GetPointsPath(int count);
}