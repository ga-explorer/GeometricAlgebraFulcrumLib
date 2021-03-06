using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    public sealed class MultiplexedPointsPath2D
        : PSeqMultiplexed1D<ITuple2D>, IPointsPath2D
    {
        public MultiplexedPointsPath2D(IReadOnlyList<IPointsPath2D> sequencesList, IEnumerable<int> sequenceSelectionList) 
            : base(sequencesList, sequenceSelectionList)
        {
        }

        public MultiplexedPointsPath2D(IReadOnlyList<IPointsPath2D> sequencesList, params int[] sequenceSelectionArray) 
            : base(sequencesList, sequenceSelectionArray)
        {
        }
    }
}