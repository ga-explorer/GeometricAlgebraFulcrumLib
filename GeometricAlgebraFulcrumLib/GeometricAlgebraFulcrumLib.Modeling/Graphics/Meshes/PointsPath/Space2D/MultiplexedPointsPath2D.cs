using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;

public sealed class MultiplexedPointsPath2D
    : PSeqMultiplexed1D<ILinFloat64Vector2D>, IPointsPath2D
{
    public MultiplexedPointsPath2D(IReadOnlyList<IPointsPath2D> sequencesList, IEnumerable<int> sequenceSelectionList) 
        : base(sequencesList, sequenceSelectionList)
    {
    }

    public MultiplexedPointsPath2D(IReadOnlyList<IPointsPath2D> sequencesList, params int[] sequenceSelectionArray) 
        : base(sequencesList, sequenceSelectionArray)
    {
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