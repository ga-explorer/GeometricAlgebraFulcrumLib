using System;
using System.Collections.Generic;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Benchmarks.GeometricAlgebra;

[SimpleJob]
public class BasisBilinearProductsBenchmarks
{
    public static XGaFloat64Processor Processor 
        => XGaFloat64ProjectiveProcessor.Instance;

    //[Params(3, 4, 5, 6, 7, 8, 9, 10)]
    public int VSpaceDimensions { get; set; } = 9;

    public ulong GaSpaceDimensions 
        => 1ul << VSpaceDimensions;

    public List<IndexSet> BasisBlades1 { get; private set; }
        = new List<IndexSet>();
    
    public List<XGaFloat64KVector> BasisBlades2 { get; private set; }
        = new List<XGaFloat64KVector>();

    public List<XGaFloat64Scalar> Scalars { get; private set; }
        = new List<XGaFloat64Scalar>();

    public List<XGaFloat64Vector> Vectors { get; private set; }
        = new List<XGaFloat64Vector>();

    public List<XGaFloat64Bivector> Bivectors { get; private set; }
        = new List<XGaFloat64Bivector>();

    public List<XGaFloat64HigherKVector> HigherKVectors { get; private set; }
        = new List<XGaFloat64HigherKVector>();
        
    public List<XGaFloat64KVector> KVectors { get; private set; }
        = new List<XGaFloat64KVector>();

    public List<XGaFloat64GradedMultivector> GradedMultivectors { get; private set; }
        = new List<XGaFloat64GradedMultivector>();

    public List<XGaFloat64UniformMultivector> UniformMultivectors { get; private set; }
        = new List<XGaFloat64UniformMultivector>();
        
    public List<XGaFloat64Multivector> Multivectors { get; private set; } 
        = new List<XGaFloat64Multivector>();

        
    private XGaFloat64GradedMultivector GetRandomGradedMultivector(Random randGen)
    {
        var composer = Processor.CreateMultivectorComposer();

        for (var id = 0UL; id < GaSpaceDimensions; id++)
            composer.SetTerm(
                id.ToUInt64IndexSet(), 
                randGen.GetFloat64(-1, 1)
            );

        return composer.GetGradedMultivector();
    }


    [GlobalSetup]
    public void Setup()
    {
        const int n = 1;

        foreach (var id in Processor.GetBasisBladeIds(VSpaceDimensions))
        {
            BasisBlades1.Add(id);
            BasisBlades2.Add(Processor.KVectorTerm(id));
        }

        Scalars.Add(Processor.ScalarZero);
        Vectors.Add(Processor.VectorZero);
        Bivectors.Add(Processor.BivectorZero);
        HigherKVectors.AddRange(
            (VSpaceDimensions - 2).GetRange(
                g => Processor.HigherKVectorZero(g + 3)
            )
        );

        var randGen = new Random(10);

        for (var i = 0; i < n; i++)
        {
            var mv = 
                GetRandomGradedMultivector(randGen);

            Scalars.Add(mv.GetScalarPart());
            Vectors.Add(mv.GetVectorPart());
            Bivectors.Add(mv.GetBivectorPart());
            HigherKVectors.AddRange(
                (VSpaceDimensions - 2).GetRange(
                    g => mv.GetHigherKVectorPart(g + 3)
                )
            );

            GradedMultivectors.Add(
                GetRandomGradedMultivector(randGen)
            );

            UniformMultivectors.Add(
                GetRandomGradedMultivector(randGen).ToUniformMultivector()
            );
        }

        KVectors.AddRange(Scalars);
        KVectors.AddRange(Vectors);
        KVectors.AddRange(Bivectors);
        KVectors.AddRange(HigherKVectors);

        Multivectors.AddRange(Scalars);
        Multivectors.AddRange(Vectors);
        Multivectors.AddRange(Bivectors);
        Multivectors.AddRange(HigherKVectors);
        Multivectors.AddRange(KVectors);
        Multivectors.AddRange(GradedMultivectors);
        Multivectors.AddRange(UniformMultivectors);
    }
        
    public void ValidateEGp()
    {
        Console.WriteLine("Validating EGp ..");

        foreach (var mv1 in Multivectors)
        {
            for (var i2 = 0; i2 < BasisBlades1.Count; i2++)
            {
                var b1 = BasisBlades1[i2];
                var b2 = BasisBlades2[i2];

                var result1 = mv1.EGp(b1);
                var result2 = mv1.EGp(b2);

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                );
            }
        }
    }

    public void ValidateGp()
    {
        Console.WriteLine("Validating Gp ..");

        foreach (var mv1 in Multivectors)
        {
            for (var i2 = 0; i2 < BasisBlades1.Count; i2++)
            {
                var b1 = BasisBlades1[i2];
                var b2 = BasisBlades2[i2];

                var result1 = mv1.Gp(b1);
                var result2 = mv1.Gp(b2);

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                );
            }
        }
    }


    public static void Validate()
    {
        var benchmark = new BasisBilinearProductsBenchmarks();

        benchmark.Setup();

#if DEBUG
        //benchmark.ValidateESp();
        //benchmark.ValidateSp();
            
        //benchmark.ValidateOp();
            
        //benchmark.ValidateELcp();
        //benchmark.ValidateLcp();
            
        //benchmark.ValidateERcp();
        //benchmark.ValidateRcp();
            
        //benchmark.ValidateEFdp();
        //benchmark.ValidateFdp();
            
        //benchmark.ValidateEHip();
        //benchmark.ValidateHip();
            
        //benchmark.ValidateECp();
        //benchmark.ValidateCp();
            
        //benchmark.ValidateEAcp();
        //benchmark.ValidateAcp();

        benchmark.ValidateEGp();
        benchmark.ValidateGp();

            
#endif

        //Console.WriteLine("Start timing ..");

        //var time1 = GetTime(() => benchmark.Lcp1(), 5);
        //Console.WriteLine($"Time1: {time1}");
        //Console.WriteLine();

        //var time2 = GetTime(() => benchmark.Lcp2(), 5);
        //Console.WriteLine($"Time2: {time2}");
        //Console.WriteLine();
            
        //var time3 = GetTime(() => benchmark.Lcp3(), 5);
        //Console.WriteLine($"Time3: {time3}");
        //Console.WriteLine();
    }
        
    [Benchmark]
    public void EGp1()
    {
        foreach (var mv1 in Multivectors)
        foreach (var mv2 in BasisBlades1)
            mv1.EGp(mv2);
    }

    [Benchmark]
    public void EGp2()
    {
        foreach (var mv1 in Multivectors)
        foreach (var mv2 in BasisBlades2)
            mv1.EGp(mv2);
    }
    

    [Benchmark]
    public void Gp1()
    {
        foreach (var mv1 in Multivectors)
        foreach (var mv2 in BasisBlades1)
            mv1.Gp(mv2);
    }
        
    [Benchmark]
    public void Gp2()
    {
        foreach (var mv1 in Multivectors)
        foreach (var mv2 in BasisBlades2)
            mv1.Gp(mv2);
    }
}