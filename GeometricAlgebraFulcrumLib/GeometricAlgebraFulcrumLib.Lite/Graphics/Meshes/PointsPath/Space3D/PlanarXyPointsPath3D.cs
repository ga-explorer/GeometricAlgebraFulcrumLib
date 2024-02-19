using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarXyPointsPath3D : 
    PSeqMapped1D<IFloat64Vector2D, IFloat64Vector3D>, 
    IPointsPath3D
{
    public double ValueZ { get; set; }


    public PlanarXyPointsPath3D(IPointsPath2D xyPath)
        : base(xyPath)
    {
        ValueZ = 0;
    }

    public PlanarXyPointsPath3D(IPointsPath2D xyPath, double valueZ)
        : base(xyPath)
    {
        ValueZ = valueZ;
    }


    protected override IFloat64Vector3D MappingFunction(IFloat64Vector2D xyPoint)
    {
        return Float64Vector3D.Create(
            xyPoint.X.Value,
            xyPoint.Y.Value,
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