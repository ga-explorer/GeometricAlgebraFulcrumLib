using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarZyPointsPath3D : 
    PSeqMapped1D<ILinFloat64Vector2D, ILinFloat64Vector3D>, 
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


    protected override ILinFloat64Vector3D MappingFunction(ILinFloat64Vector2D zyPoint)
    {
        return LinFloat64Vector3D.Create(
            ValueX,
            zyPoint.Y.ScalarValue,
            zyPoint.X.ScalarValue
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