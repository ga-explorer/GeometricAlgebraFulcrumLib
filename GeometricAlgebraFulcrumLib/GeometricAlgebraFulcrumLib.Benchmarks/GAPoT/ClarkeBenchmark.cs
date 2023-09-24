using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Composers;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Benchmarks.GAPoT
{
    [SimpleJob(baseline: true)]
    public class ClarkeBenchmark
    {
        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48)]
        //[Params(28, 29, 30, 31, 32, 33)]
        public int VSpaceDimensions { get; set; }// = 24;
    
        public IReadOnlyList<Float64Vector> VectorList { get; private set; }
    
        public LinFloat64MatrixRotation ClarkeMatrixRotation { get; private set; }

        public LinFloat64PlanarRotationSequence ClarkeSequenceRotation { get; private set; }
    

        [GlobalSetup]
        public void Setup()
        {
            var vectorArrayList =
                Enumerable
                    .Range(0, 1000)
                    .Select(_ => GetRandomVector());

            VectorList =
                vectorArrayList
                    .Select(a => a.CreateLinVector())
                    .ToImmutableArray();

            ClarkeMatrixRotation = 
                LinFloat64MatrixRotation.CreateForwardClarkeRotation(VSpaceDimensions);
        
            ClarkeSequenceRotation =
                ClarkeMatrixRotation.ToVectorToVectorRotationSequence();

            Validate();
        }

        public double[] GetRandomVector()
        {
            var random = new Random(10);

            return Enumerable
                .Range(0, VSpaceDimensions)
                .Select(_ => random.NextDouble())
                .ToArray();
        }
    
        public void Validate()
        {
            var vList1 = ClarkeMatrix();
            var vList2 = ClarkeSequence();

            for (var i = 0; i < vList1.Count; i++)
            {
                var diff = (vList1[i] - vList2[i]).GetVectorNormSquared();

                if (!diff.IsNearZero())
                    throw new InvalidOperationException();
            }
        }


        [Benchmark]
        public IReadOnlyList<Float64Vector> ClarkeMatrix()
        {
            return VectorList.Select(ClarkeMatrixRotation.MapVector).ToImmutableArray();
        }
        
        [Benchmark]
        public IReadOnlyList<Float64Vector> ClarkeSequence()
        {
            return VectorList.Select(ClarkeSequenceRotation.MapVector).ToImmutableArray();
        }
    }
}