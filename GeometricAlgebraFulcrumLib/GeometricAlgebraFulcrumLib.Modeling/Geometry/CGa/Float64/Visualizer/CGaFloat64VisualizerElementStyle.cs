using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Visualizer;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Visualizer;

public abstract record CGaFloat64VisualizerElementStyle :
    GeometryVisualizerElementStyle
{
    public CGaFloat64Visualizer ConformalVisualizer
        => (CGaFloat64Visualizer)Visualizer;

    public bool DrawPosition { get; protected set; }

    public bool DrawDirection
        => DirectionRadius > 0;

    public bool DrawNormalDirection
        => NormalDirectionRadius > 0;

    private double _directionRadius;
    public double DirectionRadius
    {
        get => _directionRadius;
        protected set
        {
            if (!value.IsValid() || value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _directionRadius = value;
        }
    }

    private double _normalDirectionRadius;
    public double NormalDirectionRadius
    {
        get => _normalDirectionRadius;
        protected set
        {
            if (!value.IsValid() || value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _normalDirectionRadius = value;
        }
    }

    private double _auxGeometryColorAlpha = 0.4;
    public double AuxGeometryColorAlpha
    {
        get => _auxGeometryColorAlpha;
        protected set
        {
            if (!value.IsValid() || value < 0 || value > 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _auxGeometryColorAlpha = value;
        }
    }


    protected CGaFloat64VisualizerElementStyle(CGaFloat64Visualizer visualizer, double thickness, bool drawPosition = false, double directionRadius = 0, double normalDirectionRadius = 0, double auxGeometryColorAlpha = 0.4)
        : base(visualizer, thickness)
    {
        DrawPosition = drawPosition;
        DirectionRadius = directionRadius;
        NormalDirectionRadius = normalDirectionRadius;
        AuxGeometryColorAlpha = auxGeometryColorAlpha;
    }


    public GrVisualCurveTubeStyle3D GetVectorVisualStyle(Color color)
    {
        return new GrVisualCurveTubeStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            Thickness
        );
    }

    public GrVisualCurveTubeStyle3D GetVectorVisualStyle(Color color, double thickness)
    {
        return new GrVisualCurveTubeStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            thickness
        );
    }

    public GrVisualCurveTubeStyle3D GetBivectorVisualStyle(Color color, double thickness)
    {
        return new GrVisualCurveTubeStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            thickness
        );
    }

    public GrVisualSurfaceThickStyle3D GetPointVisualStyle(Color color)
    {
        return new GrVisualSurfaceThickStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            Thickness
        );
    }

    public GrVisualSurfaceThickStyle3D GetPointVisualStyle(Color color, double thickness)
    {
        return new GrVisualSurfaceThickStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            thickness
        );
    }

    public GrVisualCurveTubeStyle3D GetLineVisualStyle(Color color)
    {
        return new GrVisualCurveTubeStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            Thickness
        );
    }

    public GrVisualCurveTubeStyle3D GetLineVisualStyle(Color color, double thickness)
    {
        return new GrVisualCurveTubeStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            thickness
        );
    }

    public GrVisualSurfaceThinStyle3D GetPlaneVisualStyle(Color color)
    {
        return new GrVisualSurfaceThinStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color)
        );
    }
}