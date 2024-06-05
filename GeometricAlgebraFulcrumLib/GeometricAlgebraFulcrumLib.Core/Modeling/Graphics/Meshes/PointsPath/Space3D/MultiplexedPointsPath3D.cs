using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class MultiplexedPointsPath3D : 
    PSeqMultiplexed1D<ILinFloat64Vector3D>, 
    IPointsPath3D
{
    public IReadOnlyList<IPeriodicSequence1D<ILinFloat64Vector3D>> BasePaths { get; }


    public MultiplexedPointsPath3D(IReadOnlyList<IPointsPath3D> sequencesList, IEnumerable<int> sequenceSelectionList) 
        : base(sequencesList, sequenceSelectionList)
    {
        BasePaths = sequencesList;
    }

    public MultiplexedPointsPath3D(IReadOnlyList<IPointsPath3D> sequencesList, params int[] sequenceSelectionArray) 
        : base(sequencesList, sequenceSelectionArray)
    {
        BasePaths = sequencesList;
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