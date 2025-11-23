using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarZyPointsPath3D : 
    PSeqMapped1D<LinFloat64Vector2D, LinFloat64Vector3D>, 
    IPointsPath3D
{
    public double ValueX { get; set; }


    public PlanarZyPointsPath3D(IPointsPath2D zyPath)
        : base(zyPath)
    {
        ValueX = 0;
    }

    public PlanarZyPointsPath3D(IPointsPath2D zyPath, double valueX)
        : base(zyPath)
    {
        ValueX = valueX;
    }


    protected override LinFloat64Vector3D MappingFunction(LinFloat64Vector2D zyPoint)
    {
        return LinFloat64Vector3D.Create(
            ValueX,
            zyPoint.Y,
            zyPoint.X
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