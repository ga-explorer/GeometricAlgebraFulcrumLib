using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

public class ReversedPointsPath3D : 
    PSeqReverse1D<LinFloat64Vector3D>, 
    IPointsPath3D
{
    public IPointsPath3D BasePath { get; }


    public ReversedPointsPath3D(IPointsPath3D basePath)
        : base(basePath)
    {
        BasePath = basePath;
    }


    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }

    public IPointsPath3D MapPoints(Func<LinFloat64Vector3D, LinFloat64Vector3D> pointMapping)
    {
        return new ReversedPointsPath3D(
            BasePath.MapPoints(pointMapping)
        );
    }
}