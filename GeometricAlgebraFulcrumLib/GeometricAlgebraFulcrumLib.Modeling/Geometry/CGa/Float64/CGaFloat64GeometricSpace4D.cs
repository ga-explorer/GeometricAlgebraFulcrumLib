using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Visualizer;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

/// <summary>
/// Conformal Geometric Algebra for 2D Euclidean Space
/// </summary>
public sealed class CGaFloat64GeometricSpace4D :
    CGaFloat64GeometricSpace
{
    public static CGaFloat64GeometricSpace4D Instance { get; }
        = new CGaFloat64GeometricSpace4D();


    public CGaFloat64Visualizer Visualizer { get; }


    private CGaFloat64GeometricSpace4D()
        : base(4)
    {
        Visualizer = new CGaFloat64Visualizer(this);
    }
}