namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Visualizer;

public sealed record RGaConformalVisualizerTangentStyle :
    RGaConformalVisualizerElementStyle
{
    internal RGaConformalVisualizerTangentStyle(RGaConformalVisualizer visualizer, double thickness, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
        : base(visualizer, thickness, drawPosition, directionRadius, normalDirectionRadius, auxGeometryColorAlpha)
    {
    }
    

    public RGaConformalVisualizer SetStyle(double thickness, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
    {
        DrawPosition = drawPosition;
        Thickness = thickness;
        DirectionRadius = directionRadius;
        NormalDirectionRadius = normalDirectionRadius;
        AuxGeometryColorAlpha = auxGeometryColorAlpha;
        
        return ConformalVisualizer;
    }
    
}