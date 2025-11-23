using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarXyPointsPath3D : 
    PSeqMapped1D<LinFloat64Vector2D, LinFloat64Vector3D>, 
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


    protected override LinFloat64Vector3D MappingFunction(LinFloat64Vector2D xyPoint)
    {
        return LinFloat64Vector3D.Create(
            xyPoint.X,
            xyPoint.Y,
            ValueZ
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