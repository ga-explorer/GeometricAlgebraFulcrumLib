using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class LinearPointsPath3D : 
    PSeqMapped1D<double, ILinFloat64Vector3D>, 
    IPointsPath3D
{
    public ILinFloat64Vector3D Point1 { get; set; }

    public ILinFloat64Vector3D Point2 { get; set; }


    public LinearPointsPath3D(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, int count)
        : base(new PSeqLinearDouble1D(0, 1, count, 0, 0))
    {
        Point1 = point1;
        Point2 = point2;
    }

    public LinearPointsPath3D(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, IPeriodicSequence1D<double> parameterSequence)
        : base(parameterSequence)
    {
        Point1 = point1;
        Point2 = point2;
    }


    protected override ILinFloat64Vector3D MappingFunction(double t)
    {
        var s = 1 - t;

        return LinFloat64Vector3D.Create(s * Point1.X + t * Point2.X,
            s * Point1.Y + t * Point2.Y,
            s * Point1.Z + t * Point2.Z);
    }

    public bool IsValid()
    {
        return Point1.IsValid() &&
               Point2.IsValid();
    }
        
    public IPointsPath3D MapPoints(Func<ILinFloat64Vector3D, ILinFloat64Vector3D> pointMapping)
    {
        return new ArrayPointsPath3D(this.Select(pointMapping));
    }
}