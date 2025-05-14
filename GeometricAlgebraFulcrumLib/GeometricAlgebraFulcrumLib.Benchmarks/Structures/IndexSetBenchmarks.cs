using System;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Structures;

[SimpleJob]
public class IndexSetBenchmarks
{
    public ulong GaSpaceDimension { get; private set; }

    public ulong[] IndexSetArray1 { get; private set; }
    
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

        //var time1 = GetTime(() => benchmark.EGp1(), 20);
        //Console.WriteLine($"Time1: {time1}");
        //Console.WriteLine();

        var time2 = GetTime(() => benchmark.EGp2(), 20);
        Console.WriteLine($"Time2: {time2}");
        Console.WriteLine();
    }

    [GlobalSetup]
    public void Setup()
    {
        GaSpaceDimension = 1ul << 12;
        
        IndexSetArray1 = new ulong[GaSpaceDimension];
        IndexSetArray2 = new IndexSet[GaSpaceDimension];
        IndexSetArray3 = new IndexSet2[GaSpaceDimension];

        for (var id = 0ul; id < GaSpaceDimension; id++)
        {
            var indexArray = 
                id.GetSetBitPositions().ToArray();

            IndexSetArray1[id] = id;
            IndexSetArray2[id] = IndexSet.Create(indexArray);
            IndexSetArray3[id] = IndexSet2.Create(indexArray.ToImmutableSortedSet());
        }
    }

    //[Benchmark]
    //public ulong EGp1()
    //{
    //    var result = 0UL;

    //    foreach (var indexSet1 in IndexSetArray1)
    //        foreach (var indexSet2 in IndexSetArray1)
    //        {
    //            (_, result) = indexSet1.EGpIsNegativeId(indexSet2);
    //        }

    //    return result;
    //}

    [Benchmark]
    public IndexSet EGp2()
    {
        var result = IndexSet.EmptySet;

        foreach (var indexSet1 in IndexSetArray2)
            foreach (var indexSet2 in IndexSetArray2)
            {
                (_, result) = indexSet1.EGpIsNegativeId(indexSet2);
            }

        return result;
    }

    //[Benchmark]
    //public void ShiftIndices2()
    //{
    //    foreach (var indexSet1 in IndexSetArray2)
    //    {
    //        indexSet1.ShiftIndices(2);
    //    }
    //}

    //[Benchmark]
    //public void ShiftIndices3()
    //{
    //    foreach (var indexSet1 in IndexSetArray3)
    //    {
    //        indexSet1.ShiftIndices(2);
    //    }
    //}

    //[Benchmark]
    //public void Contains1()
    //{
    //    foreach (var indexSet1 in IndexSetArray1)
    //    foreach (var indexSet2 in IndexSetArray1)
    //    {
    //        indexSet1.Contains(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void Contains2()
    //{
    //    foreach (var indexSet1 in IndexSetArray2)
    //    foreach (var indexSet2 in IndexSetArray2)
    //    {
    //        indexSet1.Contains(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void CompareTo1()
    //{
    //    foreach (var indexSet1 in IndexSetArray1)
    //    foreach (var indexSet2 in IndexSetArray1)
    //    {
    //        indexSet1.CompareTo(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void CompareTo2()
    //{
    //    foreach (var indexSet1 in IndexSetArray2)
    //    foreach (var indexSet2 in IndexSetArray2)
    //    {
    //        indexSet1.CompareTo(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void Overlaps1()
    //{
    //    foreach (var indexSet1 in IndexSetArray1)
    //    foreach (var indexSet2 in IndexSetArray1)
    //    {
    //        indexSet1.Overlaps(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void Overlaps2()
    //{
    //    foreach (var indexSet1 in IndexSetArray2)
    //    foreach (var indexSet2 in IndexSetArray2)
    //    {
    //        indexSet1.Overlaps(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void Intersect1()
    //{
    //    foreach (var indexSet1 in IndexSetArray1)
    //    foreach (var indexSet2 in IndexSetArray1)
    //    {
    //        indexSet1.Intersect(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void Intersect2()
    //{
    //    foreach (var indexSet1 in IndexSetArray2)
    //    foreach (var indexSet2 in IndexSetArray2)
    //    {
    //        indexSet1.Intersect(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void Join1()
    //{
    //    foreach (var indexSet1 in IndexSetArray1)
    //    foreach (var indexSet2 in IndexSetArray1)
    //    {
    //        indexSet1.Join(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void Join2()
    //{
    //    foreach (var indexSet1 in IndexSetArray2)
    //    foreach (var indexSet2 in IndexSetArray2)
    //    {
    //        indexSet1.Join(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void Difference1()
    //{
    //    foreach (var indexSet1 in IndexSetArray1)
    //    foreach (var indexSet2 in IndexSetArray1)
    //    {
    //        indexSet1.Difference(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void Difference2()
    //{
    //    foreach (var indexSet1 in IndexSetArray2)
    //    foreach (var indexSet2 in IndexSetArray2)
    //    {
    //        indexSet1.Difference(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void SymmetricDifference1()
    //{
    //    foreach (var indexSet1 in IndexSetArray1)
    //    foreach (var indexSet2 in IndexSetArray1)
    //    {
    //        indexSet1.SymmetricDifference(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void SymmetricDifference2()
    //{
    //    foreach (var indexSet1 in IndexSetArray2)
    //    foreach (var indexSet2 in IndexSetArray2)
    //    {
    //        indexSet1.SymmetricDifference(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void Merge1()
    //{
    //    foreach (var indexSet1 in IndexSetArray1)
    //    foreach (var indexSet2 in IndexSetArray1)
    //    {
    //        indexSet1.MergeCountSwapsTrackCommon(indexSet2);
    //    }
    //}

    //[Benchmark]
    //public void Merge2()
    //{
    //    foreach (var indexSet1 in IndexSetArray2)
    //    foreach (var indexSet2 in IndexSetArray2)
    //    {
    //        indexSet1.MergeCountSwapsTrackCommon(indexSet2);
    //    }
    //}
}