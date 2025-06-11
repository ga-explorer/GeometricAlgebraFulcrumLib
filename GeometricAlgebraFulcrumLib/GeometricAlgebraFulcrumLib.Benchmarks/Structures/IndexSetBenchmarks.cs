using System;
using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Structures;

[SimpleJob]
public class IndexSetBenchmarks
{
    public ulong GaSpaceDimension { get; private set; }

    public IndexSet[] IndexSetArray1 { get; private set; }
    
    public IndexSet[] IndexSetArray2 { get; private set; }

    public IndexSet2[] IndexSetArray3 { get; private set; }

    
    private static TimeSpan GetTime(Action sub, int n)
    {
        var t1 = DateTime.Now;

        for (var i = 0; i < n; i++)
            sub();

        var t2 = DateTime.Now;

        return t2 - t1;
    }

    public static void Validate()
    {
        var benchmark = new IndexSetBenchmarks();

        benchmark.Setup();

        //for (var id1 = 0ul; id1 < benchmark.GaSpaceDimension; id1++)
        //{
        //    var indexSet1 = id1.ToUInt64IndexSet();

        //    for (var id2 = 0ul; id2 < benchmark.GaSpaceDimension; id2++)
        //    {
        //        var indexSet2 = id2.ToUInt64IndexSet();

        //        var (f1, m1) = id1.EGpIsNegativeId(id2);
        //        var (f2, m2) = indexSet1.EGpIsNegativeId(indexSet2);

        //        Debug.Assert(f1 == f2);
        //        Debug.Assert(m1 == m2.ToUInt64());
        //        Debug.Assert(m1.ToUInt64IndexSet() == m2);
        //    }
        //}

        Console.WriteLine("Start timing ..");

        var time1 = GetTime(() => benchmark.Merge1(), 20);
        Console.WriteLine($"Time1: {time1}");
        Console.WriteLine();

        var time2 = GetTime(() => benchmark.Merge2(), 20);
        Console.WriteLine($"Time2: {time2}");
        Console.WriteLine();
    }

    [GlobalSetup]
    public void Setup()
    {
        GaSpaceDimension = 1ul << 12;
        
        IndexSetArray1 = new IndexSet[GaSpaceDimension];
        IndexSetArray2 = new IndexSet[GaSpaceDimension];
        IndexSetArray3 = new IndexSet2[GaSpaceDimension];

        for (var id = 0ul; id < GaSpaceDimension; id++)
        {
            IndexSetArray1[id] = id.ToUInt64IndexSet();
            IndexSetArray2[id] = IndexSetArray1[id].ShiftIndices(65);
            IndexSetArray3[id] = IndexSet2.Create(IndexSetArray2[id].ToImmutableSortedSet());
        }
    }


    [Benchmark]
    public void ShiftIndices1()
    {
        foreach (var indexSet1 in IndexSetArray1)
        {
            indexSet1.ShiftIndices(2);
        }
    }

    [Benchmark]
    public void ShiftIndices2()
    {
        foreach (var indexSet1 in IndexSetArray2)
        {
            indexSet1.ShiftIndices(2);
        }
    }
    
    [Benchmark]
    public void ShiftIndices3()
    {
        foreach (var indexSet1 in IndexSetArray3)
        {
            indexSet1.ShiftIndices(2);
        }
    }


    [Benchmark]
    public void Contains1()
    {
        foreach (var indexSet1 in IndexSetArray1)
        foreach (var indexSet2 in IndexSetArray1)
        {
            indexSet1.SetContains(indexSet2);
        }
    }
    
    [Benchmark]
    public void Contains2()
    {
        foreach (var indexSet1 in IndexSetArray2)
        foreach (var indexSet2 in IndexSetArray2)
        {
            indexSet1.SetContains(indexSet2);
        }
    }
    
    [Benchmark]
    public void Contains3()
    {
        foreach (var indexSet1 in IndexSetArray3)
        foreach (var indexSet2 in IndexSetArray3)
        {
            indexSet1.SetContains(indexSet2);
        }
    }


    [Benchmark]
    public void CompareTo1()
    {
        foreach (var indexSet1 in IndexSetArray1)
            foreach (var indexSet2 in IndexSetArray1)
            {
                var _ = indexSet1.CompareTo(indexSet2);
            }
    }

    [Benchmark]
    public void CompareTo2()
    {
        foreach (var indexSet1 in IndexSetArray2)
            foreach (var indexSet2 in IndexSetArray2)
            {
                var _ = indexSet1.CompareTo(indexSet2);
            }
    }
    
    [Benchmark]
    public void CompareTo3()
    {
        foreach (var indexSet1 in IndexSetArray3)
        foreach (var indexSet2 in IndexSetArray3)
        {
            var _ = indexSet1.CompareTo(indexSet2);
        }
    }


    [Benchmark]
    public void Overlaps1()
    {
        foreach (var indexSet1 in IndexSetArray1)
            foreach (var indexSet2 in IndexSetArray1)
            {
                indexSet1.SetOverlaps(indexSet2);
            }
    }

    [Benchmark]
    public void Overlaps2()
    {
        foreach (var indexSet1 in IndexSetArray2)
            foreach (var indexSet2 in IndexSetArray2)
            {
                indexSet1.SetOverlaps(indexSet2);
            }
    }
    
    [Benchmark]
    public void Overlaps3()
    {
        foreach (var indexSet1 in IndexSetArray3)
        foreach (var indexSet2 in IndexSetArray3)
        {
            indexSet1.SetOverlaps(indexSet2);
        }
    }


    [Benchmark]
    public void Intersect1()
    {
        foreach (var indexSet1 in IndexSetArray1)
            foreach (var indexSet2 in IndexSetArray1)
            {
                indexSet1.SetIntersect(indexSet2);
            }
    }

    [Benchmark]
    public void Intersect2()
    {
        foreach (var indexSet1 in IndexSetArray2)
            foreach (var indexSet2 in IndexSetArray2)
            {
                indexSet1.SetIntersect(indexSet2);
            }
    }
    
    [Benchmark]
    public void Intersect3()
    {
        foreach (var indexSet1 in IndexSetArray3)
        foreach (var indexSet2 in IndexSetArray3)
        {
            indexSet1.SetIntersect(indexSet2);
        }
    }


    [Benchmark]
    public void Join1()
    {
        foreach (var indexSet1 in IndexSetArray1)
            foreach (var indexSet2 in IndexSetArray1)
            {
                indexSet1.SetUnion(indexSet2);
            }
    }

    [Benchmark]
    public void Join2()
    {
        foreach (var indexSet1 in IndexSetArray2)
            foreach (var indexSet2 in IndexSetArray2)
            {
                indexSet1.SetUnion(indexSet2);
            }
    }
    
    [Benchmark]
    public void Join3()
    {
        foreach (var indexSet1 in IndexSetArray3)
        foreach (var indexSet2 in IndexSetArray3)
        {
            indexSet1.SetUnion(indexSet2);
        }
    }


    [Benchmark]
    public void Difference1()
    {
        foreach (var indexSet1 in IndexSetArray1)
            foreach (var indexSet2 in IndexSetArray1)
            {
                indexSet1.SetDifference(indexSet2);
            }
    }

    [Benchmark]
    public void Difference2()
    {
        foreach (var indexSet1 in IndexSetArray2)
            foreach (var indexSet2 in IndexSetArray2)
            {
                indexSet1.SetDifference(indexSet2);
            }
    }
    
    [Benchmark]
    public void Difference3()
    {
        foreach (var indexSet1 in IndexSetArray3)
        foreach (var indexSet2 in IndexSetArray3)
        {
            indexSet1.SetDifference(indexSet2);
        }
    }


    [Benchmark]
    public void SymmetricDifference1()
    {
        foreach (var indexSet1 in IndexSetArray1)
            foreach (var indexSet2 in IndexSetArray1)
            {
                indexSet1.SetMerge(indexSet2);
            }
    }

    [Benchmark]
    public void SymmetricDifference2()
    {
        foreach (var indexSet1 in IndexSetArray2)
            foreach (var indexSet2 in IndexSetArray2)
            {
                indexSet1.SetMerge(indexSet2);
            }
    }
    
    [Benchmark]
    public void SymmetricDifference3()
    {
        foreach (var indexSet1 in IndexSetArray3)
        foreach (var indexSet2 in IndexSetArray3)
        {
            indexSet1.SetMerge(indexSet2);
        }
    }


    [Benchmark]
    public void Merge1()
    {
        foreach (var indexSet1 in IndexSetArray1)
            foreach (var indexSet2 in IndexSetArray1)
            {
                indexSet1.SetMergeCountSwapsTrackCommon(indexSet2);
            }
    }

    [Benchmark]
    public void Merge2()
    {
        foreach (var indexSet1 in IndexSetArray2)
            foreach (var indexSet2 in IndexSetArray2)
            {
                indexSet1.SetMergeCountSwapsTrackCommon(indexSet2);
            }
    }
    
    [Benchmark]
    public void Merge3()
    {
        foreach (var indexSet1 in IndexSetArray3)
        foreach (var indexSet2 in IndexSetArray3)
        {
            indexSet1.SetMergeCountSwapsTrackCommon(indexSet2);
        }
    }
}