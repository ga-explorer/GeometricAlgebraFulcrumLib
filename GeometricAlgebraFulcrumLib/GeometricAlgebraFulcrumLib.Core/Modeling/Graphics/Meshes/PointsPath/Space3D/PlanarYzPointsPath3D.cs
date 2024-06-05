using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarYzPointsPath3D : 
    PSeqMapped1D<ILinFloat64Vector2D, ILinFloat64Vector3D>, 
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


    protected override ILinFloat64Vector3D MappingFunction(ILinFloat64Vector2D yzPoint)
    {
        return LinFloat64Vector3D.Create(
            ValueX,
            yzPoint.X.ScalarValue,
            yzPoint.Y.ScalarValue
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