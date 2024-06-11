namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Visualizer;

public sealed record CGaFloat64VisualizerDirectionStyle :
    CGaFloat64VisualizerElementStyle
{
    internal CGaFloat64VisualizerDirectionStyle(CGaFloat64Visualizer visualizer, double thickness, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
        : base(visualizer, thickness, drawPosition, directionRadius, normalDirectionRadius, auxGeometryColorAlpha)
    {
    }


    public CGaFloat64Visualizer SetStyle(double thickness, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
    {
        DrawPosition = drawPosition;
        Thickness = thickness;
        DirectionRadius = directionRadius;
        NormalDirectionRadius = normalDirectionRadius;
        AuxGeometryColorAlpha = auxGeometryColorAlpha;

        return ConformalVisualizer;
    }
}