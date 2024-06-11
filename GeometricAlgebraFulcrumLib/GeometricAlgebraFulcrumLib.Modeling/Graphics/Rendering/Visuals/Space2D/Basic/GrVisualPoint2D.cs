using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space2D.Styles;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space2D.Basic;

public sealed class GrVisualPoint2D :
    GrVisualElement2D,
    ILinFloat64Vector2D
{
        
    public IGrVisualElementStyle2D Style { get; init; }

    public ILinFloat64Vector2D Position { get; }

    public int VSpaceDimensions
        => 2;

    public Float64Scalar Item1
        => Position.Item1;

    public Float64Scalar Item2
        => Position.Item2;

    public Float64Scalar X
        => Position.X;

    public Float64Scalar Y
        => Position.Y;


    public GrVisualPoint2D(string name, ILinFloat64Vector2D position)
        : base(name)
    {
        Position = position;
    }


    public override bool IsValid()
    {
        return Position.IsValid();
    }

}