using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Visualizer;

public sealed record CGaFloat64VisualizerFlatStyle :
    CGaFloat64VisualizerElementStyle
{
    private double _radius;
    public double Radius
    {
        get => _radius;
        private set
        {
            if (!value.IsValid() || value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _radius = value;
        }
    }


    internal CGaFloat64VisualizerFlatStyle(CGaFloat64Visualizer visualizer, double thickness, double radius, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
        : base(visualizer, thickness, drawPosition, directionRadius, normalDirectionRadius, auxGeometryColorAlpha)
    {
        Radius = radius;
    }


    public CGaFloat64Visualizer SetStyle(double thickness, double radius, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
    {
        DrawPosition = drawPosition;
        Thickness = thickness;
        Radius = radius;
        DirectionRadius = directionRadius;
        NormalDirectionRadius = normalDirectionRadius;
        AuxGeometryColorAlpha = auxGeometryColorAlpha;

        return ConformalVisualizer;
    }
}