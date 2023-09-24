using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D
{
    public sealed class MultiplexedPointsPath2D
        : PSeqMultiplexed1D<IFloat64Vector2D>, IPointsPath2D
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

        public IPointsPath2D MapPoints(Func<IFloat64Vector2D, IFloat64Vector2D> pointMapping)
        {
            return new ArrayPointsPath2D(
                this.Select(pointMapping).ToArray()
            );
        }
    }
}