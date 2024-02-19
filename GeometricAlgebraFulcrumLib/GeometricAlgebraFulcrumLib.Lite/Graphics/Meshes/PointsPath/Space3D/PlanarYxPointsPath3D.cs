using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarYxPointsPath3D : 
    PSeqMapped1D<IFloat64Vector2D, IFloat64Vector3D>, 
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


    protected override IFloat64Vector3D MappingFunction(IFloat64Vector2D yxPoint)
    {
        if (ReferenceEquals(yxPoint, null))
            throw new ArgumentNullException(nameof(yxPoint));

        return Float64Vector3D.Create(
            yxPoint.Y.Value,
            yxPoint.X.Value,
            ValueZ
        );
    }

    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }

    public IPointsPath3D MapPoints(Func<IFloat64Vector3D, IFloat64Vector3D> pointMapping)
    {
        return new ArrayPointsPath3D(
            this.Select(pointMapping).ToArray()
        );
    }
}