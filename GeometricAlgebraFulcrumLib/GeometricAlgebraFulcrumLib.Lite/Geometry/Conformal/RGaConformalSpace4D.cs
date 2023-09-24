using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Visualizer;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal;

/// <summary>
/// Conformal Geometric Algebra for 2D Euclidean Space
/// </summary>
public sealed class RGaConformalSpace4D :
    RGaConformalSpace
{
    public static RGaConformalSpace4D Instance { get; } 
        = new RGaConformalSpace4D();
    

    public RGaConformalVisualizer Visualizer { get; }


    private RGaConformalSpace4D() 
        : base(4)
    {
        Visualizer = new RGaConformalVisualizer(this);
    }
}