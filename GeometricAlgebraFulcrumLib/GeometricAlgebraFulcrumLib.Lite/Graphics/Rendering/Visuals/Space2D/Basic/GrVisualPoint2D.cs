using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D.Styles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D.Basic;

public sealed class GrVisualPoint2D :
    GrVisualElement2D,
    IFloat64Vector2D
{
        
    public IGrVisualElementStyle2D Style { get; init; }

    public IFloat64Vector2D Position { get; }

    public int VSpaceDimensions
        => 2;

    public double Item1
        => Position.Item1;

    public double Item2
        => Position.Item2;

    public Float64Scalar X
        => Position.X;

    public Float64Scalar Y
        => Position.Y;


    public GrVisualPoint2D(string name, IFloat64Vector2D position)
        : base(name)
    {
        Position = position;
    }


    public override bool IsValid()
    {
        return Position.IsValid();
    }

}