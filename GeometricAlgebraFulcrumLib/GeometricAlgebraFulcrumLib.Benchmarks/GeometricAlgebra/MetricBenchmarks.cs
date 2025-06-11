using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Benchmarks.GeometricAlgebra
{
    [SimpleJob]
    public class MetricBenchmarks
    {
        public static void Validate()
        {
            var bm = new MetricBenchmarks();

            bm.Setup();

            for (var i = 0; i < bm.IndexArrayList1.Count; i++)
            {
                var indexList1 = bm.IndexArrayList1[i];
                var indexList2 = bm.IndexArrayList2[i];

                var b1 = Processor.Op(indexList1);
                var b2 = Processor.Op1(indexList2);

                Debug.Assert(
                    b1.Equals(b2)
                );
            }
        }


        public static XGaFloat64EuclideanProcessor Processor { get; }
            = XGaFloat64EuclideanProcessor.Instance;
        
        public List<int[]> IndexArrayList1 { get; }
            = new List<int[]>();
        
        public List<int[]> IndexArrayList2 { get; }
            = new List<int[]>();


        [GlobalSetup]
        public void Setup()
        {
            const int n = 1000;
            var randGen = new Random(10);

            IndexArrayList1.Add([]);

            for (var i = 1; i < n; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var indexArray = 
                        randGen.GetDistinctIndices(i, n);

                    IndexArrayList1.Add(indexArray);
                }

                {
                    var indexArray =
                        randGen.GetDistinctIndices(i, n);

                    var i1 = randGen.GetInt32(0, indexArray.Length - 1);
                    var i2 = randGen.GetInt32(0, indexArray.Length - 1);

                    indexArray[i2] = indexArray[i1];

                    IndexArrayList1.Add(indexArray);
                }
            }

            IndexArrayList2.AddRange(
                IndexArrayList1.Select(
                    l => (int[])l.Clone()
                )
            );
        }
        
        [Benchmark]
        public void Op1()
        {
            foreach (var indexList in IndexArrayList1)
                Processor.Op(indexList);
        }
        
        [Benchmark]
        public void Op2()
        {
            foreach (var indexList in IndexArrayList2)
                Processor.Op1(indexList);
        }

    }
}
