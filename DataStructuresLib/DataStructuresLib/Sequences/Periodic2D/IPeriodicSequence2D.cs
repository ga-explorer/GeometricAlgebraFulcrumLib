using DataStructuresLib.Collections;
using DataStructuresLib.Sequences.Periodic1D;

namespace DataStructuresLib.Sequences.Periodic2D;

public interface IPeriodicSequence2D<T>
    : IReadOnlyList2D<T>
{
    bool IsBasic { get; }

    bool IsOperator { get; }

    PSeqSlice1D<T> GetSliceAt(int dimension, int index);
}