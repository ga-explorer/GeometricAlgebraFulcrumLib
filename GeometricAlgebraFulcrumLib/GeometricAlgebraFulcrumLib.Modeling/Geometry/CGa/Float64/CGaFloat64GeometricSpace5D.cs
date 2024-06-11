using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Visualizer;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

/// <summary>
/// 5D Conformal Geometric Algebra for 3D Euclidean Space
/// </summary>
public sealed class CGaFloat64GeometricSpace5D :
    CGaFloat64GeometricSpace
{
    public static CGaFloat64GeometricSpace5D Instance { get; }
        = new CGaFloat64GeometricSpace5D();


    public CGaFloat64Blade E3 { get; }

    public CGaFloat64Blade E13 { get; }

    public CGaFloat64Blade E23 { get; }

    public CGaFloat64Visualizer Visualizer { get; }


    private CGaFloat64GeometricSpace5D()
        : base(5)
    {
        E3 = new CGaFloat64Blade(this, ConformalProcessor.VectorTerm(4));

        E13 = new CGaFloat64Blade(this, ConformalProcessor.BivectorTerm(2, 4));
        E23 = new CGaFloat64Blade(this, ConformalProcessor.BivectorTerm(3, 4));

        Visualizer = new CGaFloat64Visualizer(this);
    }
}