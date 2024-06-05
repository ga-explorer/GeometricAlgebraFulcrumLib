using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space2D;

public sealed class LinearPointsPath2D
    : PSeqMapped1D<double, ILinFloat64Vector2D>, IPointsPath2D
{
    public ILinFloat64Vector2D Point1 { get; }

    public ILinFloat64Vector2D Point2 { get; }


    public LinearPointsPath2D(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2, int count)
        : base(new PSeqLinearDouble1D(0, 1, count, 0, 0))
    {
        Point1 = point1;
        Point2 = point2;
    }

    public LinearPointsPath2D(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2, IPeriodicSequence1D<double> parameterSequence)
        : base(parameterSequence)
    {
        Point1 = point1;
        Point2 = point2;
    }


    protected override ILinFloat64Vector2D MappingFunction(double t)
    {
        var s = 1 - t;

        return LinFloat64Vector2D.Create(s * Point1.X + t * Point2.X,
            s * Point1.Y + t * Point2.Y);
    }
        
    public bool IsValid()
    {
        return Point1.IsValid() &&
               Point2.IsValid();
    }
        
    public IPointsPath2D MapPoints(Func<ILinFloat64Vector2D, ILinFloat64Vector2D> pointMapping)
    {
        return new ArrayPointsPath2D(this.Select(pointMapping));
    }
}