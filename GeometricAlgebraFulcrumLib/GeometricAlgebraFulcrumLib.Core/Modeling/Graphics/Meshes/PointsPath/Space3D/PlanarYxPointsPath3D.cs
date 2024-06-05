using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarYxPointsPath3D : 
    PSeqMapped1D<ILinFloat64Vector2D, ILinFloat64Vector3D>, 
    IPointsPath3D
{
    public double ValueZ { get; set; }


    public PlanarYxPointsPath3D(IPointsPath2D yxPath)
        : base(yxPath)
    {
        ValueZ = 0;
    }

    public PlanarYxPointsPath3D(IPointsPath2D yxPath, double valueZ)
        : base(yxPath)
    {
        ValueZ = valueZ;
    }


    protected override ILinFloat64Vector3D MappingFunction(ILinFloat64Vector2D yxPoint)
    {
        if (ReferenceEquals(yxPoint, null))
            throw new ArgumentNullException(nameof(yxPoint));

        return LinFloat64Vector3D.Create(
            yxPoint.Y.ScalarValue,
            yxPoint.X.ScalarValue,
            ValueZ
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