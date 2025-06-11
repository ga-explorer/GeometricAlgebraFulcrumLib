using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Benchmarks.GeometricAlgebra;

[SimpleJob]
public class UnaryOperationsBenchmarks
{
    public static void Validate()
    {
        var benchmark = new UnaryOperationsBenchmarks();

        benchmark.Setup();

#if DEBUG
        benchmark.ValidateReverse();
        benchmark.ValidateGradeInvolution();
        benchmark.ValidateCliffordConjugate();

            
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

    public static void TestGrades(string name, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64Multivector> product)
    {
        var benchmark = new UnaryOperationsBenchmarks();

        benchmark.Setup();

        var textList = new SortedSet<string>();

        foreach (var kv1 in benchmark.KVectors)
        {
            if (kv1.IsZero) continue;

            foreach (var kv2 in benchmark.KVectors)
            {
                if (kv2.IsZero) continue;

                var mv = product(kv1, kv2);
                var mvGrades = mv.KVectorGrades.ToArray();

                if (!mv.IsZero && mvGrades.Length > 1)
                    continue;

                var kv1Grade = $"<{kv1.Grade}>";
                var kv2Grade = $"<{kv2.Grade}>";

                var mvGrade = mv.IsZero
                    ? "zero"
                    : "<" + mvGrades[0] + ">";

                textList.Add($"{kv1Grade} {name} {kv2Grade} => {mvGrade}");
            }
        }

        Console.WriteLine(
            textList.Concatenate(Environment.NewLine)
        );
    }


    public static XGaFloat64Processor Processor 
        => XGaFloat64ProjectiveProcessor.Instance;

    //[Params(3, 4, 5, 6, 7, 8, 9, 10)]
    public int VSpaceDimensions { get; set; } = 13;

    public ulong GaSpaceDimensions 
        => 1ul << VSpaceDimensions;

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

        
    private static TimeSpan GetTime(Action sub, int n)
    {
        var t1 = DateTime.Now;

        for (var i = 0; i < n; i++)
            sub();

        var t2 = DateTime.Now;

        return t2 - t1;
    }

        
    public void ValidateGradeInvolution()
    {
        Console.WriteLine("Validating Grade Involution ..");

        foreach (var mv1 in Multivectors)
        {
            var result1 = 
                mv1.GradeInvolution();
                    
            var result2 = 
                Processor
                    .CreateMultivectorComposer()
                    .AddTerms(mv1)
                    .GradeInvolution()
                    .GetSimpleMultivector();

            var result3 = 
                Processor
                    .CreateUniformComposer()
                    .AddTerms(mv1)
                    .GradeInvolution()
                    .GetSimpleMultivector();

            Debug.Assert(
                (result1 - result2).IsNearZero()
            ); 
                
            Debug.Assert(
                (result1 - result3).IsNearZero()
            ); 
        }
    }

    public void ValidateReverse()
    {
        Console.WriteLine("Validating Reverse ..");

        foreach (var mv1 in Multivectors)
        {
            var result1 = 
                mv1.Reverse();
                    
            var result2 = 
                Processor
                    .CreateMultivectorComposer()
                    .AddTerms(mv1)
                    .Reverse()
                    .GetSimpleMultivector();

            var result3 = 
                Processor
                    .CreateUniformComposer()
                    .AddTerms(mv1)
                    .Reverse()
                    .GetSimpleMultivector();

            Debug.Assert(
                (result1 - result2).IsNearZero()
            ); 
                
            Debug.Assert(
                (result1 - result3).IsNearZero()
            ); 
        }
    }
    
    public void ValidateCliffordConjugate()
    {
        Console.WriteLine("Validating Clifford Conjugate ..");

        foreach (var mv1 in Multivectors)
        {
            var result1 = 
                mv1.CliffordConjugate();
                    
            var result2 = 
                Processor
                    .CreateMultivectorComposer()
                    .AddTerms(mv1)
                    .CliffordConjugate()
                    .GetSimpleMultivector();

            var result3 = 
                Processor
                    .CreateUniformComposer()
                    .AddTerms(mv1)
                    .CliffordConjugate()
                    .GetSimpleMultivector();

            Debug.Assert(
                (result1 - result2).IsNearZero()
            ); 
                
            Debug.Assert(
                (result1 - result3).IsNearZero()
            ); 
        }
    }


    [Benchmark]
    public void GradeInvolution1()
    {
        foreach (var mv1 in Multivectors)
            mv1.GradeInvolution();
    }

    [Benchmark]
    public void GradeInvolution2()
    {
        foreach (var mv1 in Multivectors)
        {
            Processor
                .CreateMultivectorComposer()
                .AddTerms(mv1)
                .GradeInvolution();
        }
    }

    [Benchmark]
    public void GradeInvolution3()
    {
        foreach (var mv1 in Multivectors)
        {
            Processor
                .CreateUniformComposer()
                .AddTerms(mv1)
                .GradeInvolution();
        }
    }

    
    [Benchmark]
    public void Reverse1()
    {
        foreach (var mv1 in Multivectors)
            mv1.Reverse();
    }

    [Benchmark]
    public void Reverse2()
    {
        foreach (var mv1 in Multivectors)
        {
            Processor
                .CreateMultivectorComposer()
                .AddTerms(mv1)
                .Reverse();
        }
    }

    [Benchmark]
    public void Reverse3()
    {
        foreach (var mv1 in Multivectors)
        {
            Processor
                .CreateUniformComposer()
                .AddTerms(mv1)
                .Reverse();
        }
    }

    
    [Benchmark]
    public void CliffordConjugate1()
    {
        foreach (var mv1 in Multivectors)
            mv1.CliffordConjugate();
    }

    [Benchmark]
    public void CliffordConjugate2()
    {
        foreach (var mv1 in Multivectors)
        {
            Processor
                .CreateMultivectorComposer()
                .AddTerms(mv1)
                .CliffordConjugate();
        }
    }

    [Benchmark]
    public void CliffordConjugate3()
    {
        foreach (var mv1 in Multivectors)
        {
            Processor
                .CreateUniformComposer()
                .AddTerms(mv1)
                .CliffordConjugate();
        }
    }

}