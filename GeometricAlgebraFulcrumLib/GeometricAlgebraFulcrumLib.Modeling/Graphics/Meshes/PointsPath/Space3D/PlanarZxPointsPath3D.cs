using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarZxPointsPath3D : 
    PSeqMapped1D<ILinFloat64Vector2D, ILinFloat64Vector3D>, 
    IPointsPath3D
{
    public double ValueY { get; set; }


    public PlanarZxPointsPath3D(IPointsPath2D zxPath)
        : base(zxPath)
    {
        ValueY = 0;
    }

    public PlanarZxPointsPath3D(IPointsPath2D zxPath, double valueX)
        : base(zxPath)
    {
        ValueY = valueX;
    }


    protected override ILinFloat64Vector3D MappingFunction(ILinFloat64Vector2D zxPoint)
    {
        return LinFloat64Vector3D.Create(
            zxPoint.Y.ScalarValue,
            ValueY,
            zxPoint.X.ScalarValue
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