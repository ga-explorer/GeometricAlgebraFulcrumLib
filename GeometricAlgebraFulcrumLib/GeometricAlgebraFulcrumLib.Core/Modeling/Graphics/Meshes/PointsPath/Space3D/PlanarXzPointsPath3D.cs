using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarXzPointsPath3D : 
    PSeqMapped1D<ILinFloat64Vector2D, ILinFloat64Vector3D>, 
    IPointsPath3D
{
    public double ValueY { get; set; }


    public PlanarXzPointsPath3D(IPointsPath2D zxPath)
        : base(zxPath)
    {
        ValueY = 0;
    }

    public PlanarXzPointsPath3D(IPointsPath2D xzPath, double valueY)
        : base(xzPath)
    {
        ValueY = valueY;
    }


    protected override ILinFloat64Vector3D MappingFunction(ILinFloat64Vector2D xzPoint)
    {
        return LinFloat64Vector3D.Create(
            xzPoint.X.ScalarValue,
            ValueY,
            xzPoint.Y.ScalarValue
        );
    }

    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }

    public IPointsPath3D MapPoints(Func<ILinFloat64Vector3D, ILinFloat64Vector3D> pointMapping)
    {
        return new ArrayPointsPath3D(
            this.Select(pointMapping).ToArray()
        );
    }
}