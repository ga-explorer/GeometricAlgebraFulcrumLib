using System;
using System.Collections;
using System.Collections.Generic;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Sequences.Periodic1D;

public sealed class PSeqLinearDouble1D
    : IPeriodicSequence1D<double>
{
    public double ValueLimit1 { get; }

    public double ValueLimit2 { get; }

    public int Count { get; }

    public double ValueDelta
        => (Value2 - Value1) / (Count - 1);

    public int SkipValues1 { get; }

    public int SkipValues2 { get; }

    public int TotalCount 
        => Count + SkipValues1 + SkipValues2;

    public double TotalDelta
        => (ValueLimit2 - ValueLimit1) / (Count + SkipValues1 + SkipValues2 - 1);

    public double Value1 { get; }

    public double Value2 { get; }

    public double this[int index]
    {
        get
        {
            var i = index.Mod(Count);
            var j = Count - i - 1;

            return (j * Value2 + i * Value1) / (Count - 1);
        }
    }

    public bool IsBasic 
        => true;

    public bool IsOperator 
        => false;


    public PSeqLinearDouble1D(int count)
    {
        if (count < 2)
            throw new ArgumentOutOfRangeException(nameof(count));

        SkipValues1 = 0;
        SkipValues2 = 0;

        ValueLimit1 = 0;
        ValueLimit2 = 1;

        Count = count;

        Value1 = 0;
        Value2 = 1;
    }

    public PSeqLinearDouble1D(double valueLimit1, double valueLimit2, int count)
    {
        if (Count < 2)
            throw new ArgumentOutOfRangeException(nameof(count));

        SkipValues1 = 0;
        SkipValues2 = 0;

        ValueLimit1 = valueLimit1;
        ValueLimit2 = valueLimit2;

        Count = count;

        Value1 = valueLimit1;
        Value2 = valueLimit2;
    }

    public PSeqLinearDouble1D(double valueLimit1, double valueLimit2, int count, int skipValues1, int skipValues2)
    {
        if (count < 2)
            throw new ArgumentOutOfRangeException(nameof(count));

        if (skipValues1 < 0)
            throw new ArgumentOutOfRangeException(nameof(skipValues1));

        if (skipValues2 < 0)
            throw new ArgumentOutOfRangeException(nameof(skipValues2));

        SkipValues1 = skipValues1;
        SkipValues2 = skipValues2;

        ValueLimit1 = valueLimit1;
        ValueLimit2 = valueLimit2;

        Count = count;

        var totalDelta = 
            (ValueLimit2 - ValueLimit1) / 
            (count + skipValues1 + skipValues2 - 1);

        Value1 = valueLimit1 + totalDelta * skipValues1;
        Value2 = valueLimit2 - totalDelta * skipValues2;
    }


    public IEnumerator<double> GetEnumerator()
    {
        var delta = 1.0d / (Count - 1);
        var j = Count - 1;
        for (var i = 0; i < Count; i++)
        {
            yield return (i * Value2 + j * Value1) * delta;
            j--;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}