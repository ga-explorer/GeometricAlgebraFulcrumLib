using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GAPoTNumLib.GAPoT;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace GeometricAlgebraFulcrumLib.Benchmarks.GAPoT;

[SimpleJob(baseline: true)]
public class ClarkeBenchmark
{
    [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48)]
    //[Params(28, 29, 30, 31, 32, 33)]
    public int VSpaceDimension { get; set; }// = 24;
    
    public IReadOnlyList<Float64Tuple> VectorList { get; private set; }
    
    public MatrixRotation ClarkeMatrixRotation { get; private set; }

    public VectorToVectorRotationSequence ClarkeSequenceRotation { get; private set; }
    

    [GlobalSetup]
    public void Setup()
    {
        var vectorArrayList =
            Enumerable
                .Range(0, 1000)
                .Select(_ => GetRandomVector());

        VectorList =
            vectorArrayList
                .Select(Float64Tuple.Create)
                .ToImmutableArray();

        ClarkeMatrixRotation = 
            MatrixRotation.CreateForwardClarkeRotation(VSpaceDimension);
        
        ClarkeSequenceRotation =
            ClarkeMatrixRotation.ToVectorToVectorRotationSequence();

        Validate();
    }

    public double[] GetRandomVector()
    {
        var random = new Random(10);

        return Enumerable
            .Range(0, VSpaceDimension)
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
    public IReadOnlyList<Float64Tuple> ClarkeMatrix()
    {
        return VectorList.Select(ClarkeMatrixRotation.MapVector).ToImmutableArray();
    }
        
    [Benchmark]
    public IReadOnlyList<Float64Tuple> ClarkeSequence()
    {
        return VectorList.Select(ClarkeSequenceRotation.MapVector).ToImmutableArray();
    }
}