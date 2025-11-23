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

    public double Item1
        => Position.Item1;

    public double Item2
        => Position.Item2;

    public double X
        => Position.X;

    public double Y
        => Position.Y;
    
    public double this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            _ => Float64Scalar.Zero
        };


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