using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space2D.Styles;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space2D.Curves;

public sealed class GrVisualCircle2D :
    GrVisualCurve2D
{
    public ILinFloat64Vector2D Center { get; }

    public double Radius { get; }
        
    public override int PathPointCount 
        => 360;

    public override double Length 
        => Math.Tau * Radius;


    public GrVisualCircle2D(string name, IGrVisualCurveStyle2D style, ILinFloat64Vector2D center, double radius)
        : base(name, style)
    {
        Center = center;
        Radius = radius;
    }


    public override bool IsValid()
    {
        return Center.IsValid() &&
               Radius.IsValid() &&
               Radius > 0;
    }
        
    public Triplet<LinFloat64Vector2D> GetPointsTriplet()
    {
        const double angle = Math.Tau / 3d;

        var a = Radius * Math.Cos(angle);
        var b = Radius * Math.Sin(angle);

        var point1 = Center + LinFloat64Vector2D.Create(Radius, 0);
        var point2 = Center + LinFloat64Vector2D.Create(a, b);
        var point3 = Center + LinFloat64Vector2D.Create(a, -b);

        return new Triplet<LinFloat64Vector2D>(point1, point2, point3);
    }
        
    public override IPointsPath2D GetPositionsPath()
    {
        var angles = 
            0d.GetLinearRange(Math.Tau, PathPointCount, false);

        var points =
            angles.Select(angle => 
                Center + Radius * angle.Cos() * LinFloat64Vector2D.E1 +
                Center + Radius * angle.Sin() * LinFloat64Vector2D.E2
            );

        return new ArrayPointsPath2D(points);
    }

}