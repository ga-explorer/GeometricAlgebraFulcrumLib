using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space2D.Styles;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space2D.Curves;

public abstract class GrVisualCurve2D :
    GrVisualElement2D
{
    public IGrVisualCurveStyle2D Style { get; }

    public abstract int PathPointCount { get; }

    public abstract double Length { get; }


    protected GrVisualCurve2D(string name, IGrVisualCurveStyle2D style)
        : base(name)
    {
        Style = style;
    }


    public abstract IPointsPath2D GetPositionsPath();
}