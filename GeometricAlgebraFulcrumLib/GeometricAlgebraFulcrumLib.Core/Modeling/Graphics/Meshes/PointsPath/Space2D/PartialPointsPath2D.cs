using GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space2D;

public class PartialPointsPath2D
    : PSeqPartial1D<ILinFloat64Vector2D>, IPointsPath2D
{
    public IPointsPath2D BasePath { get; }


    public PartialPointsPath2D(IPointsPath2D basePath, IndexMapRange1D baseIndexRange) 
        : base(basePath, baseIndexRange)
    {
        BasePath = basePath;
    }

    public PartialPointsPath2D(IPointsPath2D basePath, int firstIndex) 
        : base(basePath, firstIndex)
    {
        BasePath = basePath;
    }

    public PartialPointsPath2D(IPointsPath2D basePath, int firstIndex, int count) 
        : base(basePath, firstIndex, count)
    {
        BasePath = basePath;
    }

    public PartialPointsPath2D(IPointsPath2D basePath, int firstIndex, int count, bool moveForward) 
        : base(basePath, firstIndex, count, moveForward)
    {
        BasePath = basePath;
    }

        
    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }

    public IPointsPath2D MapPoints(Func<ILinFloat64Vector2D, ILinFloat64Vector2D> pointMapping)
    {
        return new ArrayPointsPath2D(
            this.Select(pointMapping).ToArray()
        );
    }
}