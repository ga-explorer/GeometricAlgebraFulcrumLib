using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;

public class PSeqConstant2D<T>
    : IPeriodicSequence2D<T>
{
    public T Value { get; set; }


    public int Count1 { get; }

    public int Count2 { get; }

    public int Count
        => Count1 * Count2;

    public T this[int index] 
        => Value;

    public T this[int index1, int index2]
        => Value;

    public bool IsBasic
        => true;

    public bool IsOperator
        => false;


    public PSeqConstant2D(int count1, int count2)
    {
        Count1 = count1;
        Count2 = count2;
    }

    public PSeqConstant2D(int count1, int count2, T value)
    {
        Count1 = count1;
        Count2 = count2;

        Value = value;
    }


    public PSeqSlice1D<T> GetSliceAt(int dimension, int index)
    {
        return new PSeqSlice1D<T>(this, dimension, index);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Enumerable.Repeat(Value, Count).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}