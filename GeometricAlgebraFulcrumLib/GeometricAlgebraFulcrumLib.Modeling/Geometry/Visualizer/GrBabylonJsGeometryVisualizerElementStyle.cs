using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Visualizer;

public abstract record GrBabylonJsGeometryVisualizerElementStyle
{
    public GrBabylonJsGeometryAnimationComposer AnimationComposer { get; }

    private double _thickness;
    public double Thickness
    {
        get => _thickness;
        protected set
        {
            if (!value.IsValid() || value <= 0 || value > 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _thickness = value;
        }
    }


    protected GrBabylonJsGeometryVisualizerElementStyle(GrBabylonJsGeometryAnimationComposer visualizer)
    {
        AnimationComposer = visualizer;
        Thickness = 0.05;
    }

    protected GrBabylonJsGeometryVisualizerElementStyle(GrBabylonJsGeometryAnimationComposer visualizer, double thickness)
    {
        Debug.Assert(thickness > 0);

        AnimationComposer = visualizer;
        Thickness = thickness;
    }
}