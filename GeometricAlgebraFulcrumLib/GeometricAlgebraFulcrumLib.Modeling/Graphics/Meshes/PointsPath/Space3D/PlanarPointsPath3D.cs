using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PlanarPointsPath3D : 
    PSeqMapped1D<LinFloat64Vector2D, LinFloat64Vector3D>, 
    IPointsPath3D
{
    public LinFloat64Vector3D Origin { get; set; }

    public LinFloat64Vector3D Direction1 { get; set; }

    public LinFloat64Vector3D Direction2 { get; set; }


    public PlanarPointsPath3D(IPointsPath2D basePath) 
        : base(basePath)
    {
        Origin = LinFloat64Vector3D.Create(0, 0, 0);
        Direction1 = LinFloat64Vector3D.Create(1, 0, 0);
        Direction2 = LinFloat64Vector3D.Create(0, 1, 0);
    }


    protected override LinFloat64Vector3D MappingFunction(LinFloat64Vector2D pointUv)
    {
        return LinFloat64Vector3D.Create(
            Origin.X + pointUv.X * Direction1.X + pointUv.Y * Direction2.X,
            Origin.Y + pointUv.X * Direction1.Y + pointUv.Y * Direction2.Y,
            Origin.Z + pointUv.X * Direction1.Z + pointUv.Y * Direction2.Z
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