using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarXzPointsPath3D : 
    PSeqMapped1D<LinFloat64Vector2D, LinFloat64Vector3D>, 
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


    protected override LinFloat64Vector3D MappingFunction(LinFloat64Vector2D xzPoint)
    {
        return LinFloat64Vector3D.Create(
            xzPoint.X,
            ValueY,
            xzPoint.Y
        );
    }

    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }

    public IPointsPath3D MapPoints(Func<LinFloat64Vector3D, LinFloat64Vector3D> pointMapping)
    {
        return new ArrayPointsPath3D(
            this.Select(pointMapping).ToArray()
        );
    }
}