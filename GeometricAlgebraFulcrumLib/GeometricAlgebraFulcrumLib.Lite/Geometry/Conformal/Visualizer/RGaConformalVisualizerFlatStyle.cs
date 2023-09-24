using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Visualizer;

public sealed record RGaConformalVisualizerFlatStyle :
    RGaConformalVisualizerElementStyle
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
 

    internal RGaConformalVisualizerFlatStyle(RGaConformalVisualizer visualizer, double thickness, double radius, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
        : base(visualizer, thickness, drawPosition, directionRadius, normalDirectionRadius, auxGeometryColorAlpha)
    {
        Radius = radius;
    }
    
    
    public RGaConformalVisualizer SetStyle(double thickness, double radius, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
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