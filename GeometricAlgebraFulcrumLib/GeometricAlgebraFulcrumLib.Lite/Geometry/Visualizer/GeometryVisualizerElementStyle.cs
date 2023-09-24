using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Visualizer;

public abstract record GeometryVisualizerElementStyle
{
    public GeometryVisualizer Visualizer { get; }

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


    protected GeometryVisualizerElementStyle(GeometryVisualizer visualizer)
    {
        Visualizer = visualizer;
        Thickness = 0.05;
    }

    protected GeometryVisualizerElementStyle(GeometryVisualizer visualizer, double thickness)
    {
        Debug.Assert(thickness > 0);

        Visualizer = visualizer;
        Thickness = thickness;
    }
}