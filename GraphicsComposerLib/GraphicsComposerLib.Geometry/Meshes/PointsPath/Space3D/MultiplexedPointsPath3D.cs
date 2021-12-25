using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class MultiplexedPointsPath3D
        : PSeqMultiplexed1D<ITuple3D>, IPointsPath3D
    {
        public MultiplexedPointsPath3D(IReadOnlyList<IPointsPath3D> sequencesList, IEnumerable<int> sequenceSelectionList) 
            : base(sequencesList, sequenceSelectionList)
        {
        }

        public MultiplexedPointsPath3D(IReadOnlyList<IPointsPath3D> sequencesList, params int[] sequenceSelectionArray) 
            : base(sequencesList, sequenceSelectionArray)
        {
        }
    }
}