using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;

public sealed class PSeqLinearDouble2D
    : IPeriodicSequence2D<Pair<double>>
{
    public PSeqLinearDouble1D BaseSequence1 { get; }

    public PSeqLinearDouble1D BaseSequence2 { get; }

    public int Count1 
        => BaseSequence1.Count;

    public int Count2
        => BaseSequence2.Count;

    public int Count 
        => BaseSequence1.Count * BaseSequence2.Count;

    public Pair<double> this[int index] 
    { 
        get 
        {
            var indexPair = this.GetItemIndexPair(index);
                
            return new Pair<double>(
                BaseSequence1[indexPair.Item1],
                BaseSequence2[indexPair.Item2]
            );
        }
    }

    public Pair<double> this[int index1, int index2] 
        => new Pair<double>(
            BaseSequence1[index1],
            BaseSequence2[index2]
        );

    public bool IsBasic 
        => true;

    public bool IsOperator 
        => false;


    public PSeqLinearDouble2D(int count1, int count2)
    {
        if (count1 < 2)
            throw new ArgumentOutOfRangeException(nameof(count1));

        if (count2 < 2)
            throw new ArgumentOutOfRangeException(nameof(count2));

        BaseSequence1 = new PSeqLinearDouble1D(count1);
        BaseSequence2 = new PSeqLinearDouble1D(count2);
    }

    public PSeqLinearDouble2D(PSeqLinearDouble1D baseSequence1, PSeqLinearDouble1D baseSequence2)
    {
        if (ReferenceEquals(baseSequence1, null))
            throw new ArgumentNullException(nameof(baseSequence1));

        if (ReferenceEquals(baseSequence2, null))
            throw new ArgumentNullException(nameof(baseSequence2));

        BaseSequence1 = baseSequence1;
        BaseSequence2 = baseSequence2;
    }

        
    public PSeqSlice1D<Pair<double>> GetSliceAt(int dimension, int index)
    {
        return new PSeqSlice1D<Pair<double>>(this, dimension, index);
    }

    public IEnumerator<Pair<double>> GetEnumerator()
    {
        foreach (var value1 in BaseSequence1)
        foreach (var value2 in BaseSequence2)
            yield return new Pair<double>(value1, value2);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}