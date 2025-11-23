using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarYzPointsPath3D : 
    PSeqMapped1D<LinFloat64Vector2D, LinFloat64Vector3D>, 
    IPointsPath3D
{
    public double ValueX { get; set; }


    public PlanarYzPointsPath3D(IPointsPath2D xyPath)
        : base(xyPath)
    {
        ValueX = 0;
    }

    public PlanarYzPointsPath3D(IPointsPath2D xyPath, double valueX)
        : base(xyPath)
    {
        ValueX = valueX;
    }


    protected override LinFloat64Vector3D MappingFunction(LinFloat64Vector2D yzPoint)
    {
        return LinFloat64Vector3D.Create(
            ValueX,
            yzPoint.X,
            yzPoint.Y
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