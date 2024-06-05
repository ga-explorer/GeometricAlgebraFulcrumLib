using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;

public interface IPeriodicSequence2D<T>
    : IReadOnlyList2D<T>
{
    bool IsBasic { get; }

    bool IsOperator { get; }

    PSeqSlice1D<T> GetSliceAt(int dimension, int index);
}