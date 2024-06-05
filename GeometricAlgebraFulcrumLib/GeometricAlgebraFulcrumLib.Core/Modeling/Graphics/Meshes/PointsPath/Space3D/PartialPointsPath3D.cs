using GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

public class PartialPointsPath3D : 
    PSeqPartial1D<ILinFloat64Vector3D>, 
    IPointsPath3D
{
    public IPointsPath3D BasePath { get; }


    public PartialPointsPath3D(IPointsPath3D basePath, IndexMapRange1D baseIndexRange) 
        : base(basePath, baseIndexRange)
    {
        BasePath = basePath;
    }

    public PartialPointsPath3D(IPointsPath3D basePath, int firstIndex) 
        : base(basePath, firstIndex)
    {
        BasePath = basePath;
    }

    public PartialPointsPath3D(IPointsPath3D basePath, int firstIndex, int count) 
        : base(basePath, firstIndex, count)
    {
        BasePath = basePath;
    }

    public PartialPointsPath3D(IPointsPath3D basePath, int firstIndex, int count, bool moveForward) 
        : base(basePath, firstIndex, count, moveForward)
    {
        BasePath = basePath;
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