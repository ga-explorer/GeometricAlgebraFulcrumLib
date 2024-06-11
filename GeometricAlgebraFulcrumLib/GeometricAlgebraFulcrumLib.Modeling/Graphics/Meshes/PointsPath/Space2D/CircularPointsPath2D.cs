using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;

public sealed class CircularPointsPath2D : 
    PSeqMapped1D<double, ILinFloat64Vector2D>, 
    IPointsPath2D
{
    public ILinFloat64Vector2D Center { get; }

    public double Radius { get; }


    public CircularPointsPath2D(ILinFloat64Vector2D center, double radius, int count)
        : base(PeriodicSequenceUtils.CreateLinearDoubleSequence(count))
    {
        if (Count < 2)
            throw new ArgumentOutOfRangeException(nameof(count));

        Center = center;
        Radius = radius;
    }

    public CircularPointsPath2D(ILinFloat64Vector2D center, double radius, IPeriodicSequence1D<double> parameterSequence)
        : base(parameterSequence)
    {
        Center = center;
        Radius = radius;
    }


    protected override ILinFloat64Vector2D MappingFunction(double t)
    {
        var angle = 2 * Math.PI * t;
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        return LinFloat64Vector2D.Create(Center.X + Radius * cosAngle,
            Center.Y + Radius * sinAngle);
    }

        
    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }
        
    public IPointsPath2D MapPoints(Func<ILinFloat64Vector2D, ILinFloat64Vector2D> pointMapping)
    {
        return new ArrayPointsPath2D(
            this.Select(pointMapping).ToArray()
        );
    }
}