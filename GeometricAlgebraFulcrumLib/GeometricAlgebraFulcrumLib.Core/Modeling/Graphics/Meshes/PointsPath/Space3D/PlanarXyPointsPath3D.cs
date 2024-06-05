using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarXyPointsPath3D : 
    PSeqMapped1D<ILinFloat64Vector2D, ILinFloat64Vector3D>, 
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


    protected override ILinFloat64Vector3D MappingFunction(ILinFloat64Vector2D xyPoint)
    {
        return LinFloat64Vector3D.Create(
            xyPoint.X.ScalarValue,
            xyPoint.Y.ScalarValue,
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