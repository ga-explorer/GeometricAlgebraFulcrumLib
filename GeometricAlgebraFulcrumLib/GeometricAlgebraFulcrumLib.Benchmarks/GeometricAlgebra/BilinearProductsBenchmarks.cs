using BenchmarkDotNet.Attributes;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Benchmarks.GeometricAlgebra
{
    [SimpleJob]
    public class BilinearProductsBenchmarks
    {
        public static XGaFloat64Processor Processor 
            => XGaFloat64ProjectiveProcessor.Instance;

        //[Params(3, 4, 5, 6, 7, 8, 9, 10)]
        public int VSpaceDimensions { get; set; } = 9;

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

        
        public void ValidateOp()
        {
            Console.WriteLine("Validating Op ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.Op(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddOpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddOpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }

        public void ValidateESp()
        {
            Console.WriteLine("Validating ESp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.ESp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddESpTerms(mv1, mv2)
                        .GetScalar();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddESpTerms(mv1, mv2)
                        .GetScalar();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }

        public void ValidateSp()
        {
            Console.WriteLine("Validating Sp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.Sp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddSpTerms(mv1, mv2)
                        .GetScalar();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddSpTerms(mv1, mv2)
                        .GetScalar();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }
        
        public void ValidateELcp()
        {
            Console.WriteLine("Validating ELcp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.ELcp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddELcpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddELcpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }

        public void ValidateLcp()
        {
            Console.WriteLine("Validating Lcp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.Lcp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddLcpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddLcpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }
        
        public void ValidateERcp()
        {
            Console.WriteLine("Validating ERcp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.ERcp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddERcpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddERcpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }

        public void ValidateRcp()
        {
            Console.WriteLine("Validating Rcp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.Rcp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddRcpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddRcpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }
        
        public void ValidateEFdp()
        {
            Console.WriteLine("Validating EFdp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.EFdp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddEFdpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddEFdpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }

        public void ValidateFdp()
        {
            Console.WriteLine("Validating Fdp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.Fdp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddFdpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddFdpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }
        
        public void ValidateEHip()
        {
            Console.WriteLine("Validating EHip ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.EHip(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddEHipTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddEHipTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }

        public void ValidateHip()
        {
            Console.WriteLine("Validating Hip ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.Hip(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddHipTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddHipTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }
        
        public void ValidateECp()
        {
            Console.WriteLine("Validating ECp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.ECp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddECpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddECpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }

        public void ValidateCp()
        {
            Console.WriteLine("Validating Cp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.Cp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddCpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddCpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }
        
        public void ValidateEAcp()
        {
            Console.WriteLine("Validating EAcp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.EAcp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddEAcpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddEAcpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }

        public void ValidateAcp()
        {
            Console.WriteLine("Validating Acp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.Acp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddAcpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddAcpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }

        public void ValidateEGp()
        {
            Console.WriteLine("Validating EGp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.EGp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddEGpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddEGpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }

        public void ValidateGp()
        {
            Console.WriteLine("Validating Gp ..");

            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                var result1 = 
                    mv1.Gp(mv2);
                    
                var result2 = 
                    Processor
                        .CreateMultivectorComposer()
                        .AddGpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                var result3 = 
                    Processor
                        .CreateUniformComposer()
                        .AddGpTerms(mv1, mv2)
                        .GetSimpleMultivector();

                Debug.Assert(
                    (result1 - result2).IsNearZero()
                ); 
                
                Debug.Assert(
                    (result1 - result3).IsNearZero()
                ); 
            }
        }


        public static void Validate()
        {
            var benchmark = new BilinearProductsBenchmarks();

            benchmark.Setup();

            #if DEBUG
            benchmark.ValidateESp();
            benchmark.ValidateSp();
            
            benchmark.ValidateOp();
            
            benchmark.ValidateELcp();
            benchmark.ValidateLcp();
            
            benchmark.ValidateERcp();
            benchmark.ValidateRcp();
            
            benchmark.ValidateEFdp();
            benchmark.ValidateFdp();
            
            benchmark.ValidateEHip();
            benchmark.ValidateHip();
            
            benchmark.ValidateECp();
            benchmark.ValidateCp();
            
            benchmark.ValidateEAcp();
            benchmark.ValidateAcp();

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

        public static void TestGrades(string name, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64Multivector> product)
        {
            var benchmark = new BilinearProductsBenchmarks();

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


        [Benchmark]
        public void ESp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.ESp(mv2);
        }

        [Benchmark]
        public void ESp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddESpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void ESp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddESpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void Sp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.Sp(mv2);
        }

        [Benchmark]
        public void Sp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddSpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void Sp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddSpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void Op1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.Op(mv2);
        }

        [Benchmark]
        public void Op2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddOpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void Op3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddOpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void ELcp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.ELcp(mv2);
        }

        [Benchmark]
        public void ELcp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddELcpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void ELcp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddELcpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void Lcp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.Lcp(mv2);
        }

        [Benchmark]
        public void Lcp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddLcpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void Lcp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddLcpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void ERcp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.ERcp(mv2);
        }

        [Benchmark]
        public void ERcp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddERcpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void ERcp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddERcpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void Rcp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.Rcp(mv2);
        }

        [Benchmark]
        public void Rcp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddRcpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void Rcp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddRcpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void EFdp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.EFdp(mv2);
        }

        [Benchmark]
        public void EFdp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddEFdpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void EFdp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddEFdpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void Fdp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.Fdp(mv2);
        }

        [Benchmark]
        public void Fdp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddFdpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void Fdp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddFdpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void EHip1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.EHip(mv2);
        }

        [Benchmark]
        public void EHip2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddEHipTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void EHip3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddEHipTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void Hip1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.Hip(mv2);
        }

        [Benchmark]
        public void Hip2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddHipTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void Hip3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddHipTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void ECp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.ECp(mv2);
        }

        [Benchmark]
        public void ECp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddECpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void ECp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddECpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void Cp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.Cp(mv2);
        }

        [Benchmark]
        public void Cp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddCpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void Cp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddCpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void EAcp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.EAcp(mv2);
        }

        [Benchmark]
        public void EAcp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddEAcpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void EAcp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddEAcpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void Acp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.Acp(mv2);
        }

        [Benchmark]
        public void Acp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddAcpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void Acp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddAcpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void EGp1()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                    mv1.EGp(mv2);
        }

        [Benchmark]
        public void EGp2()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateMultivectorComposer()
                        .AddEGpTerms(mv1, mv2)
                        ;
                }
        }

        [Benchmark]
        public void EGp3()
        {
            foreach (var mv1 in Multivectors)
                foreach (var mv2 in Multivectors)
                {
                    Processor
                        .CreateUniformComposer()
                        .AddEGpTerms(mv1, mv2)
                        ;
                }
        }


        [Benchmark]
        public void Gp1()
        {
            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
                mv1.Gp(mv2);
        }
        
        [Benchmark]
        public void Gp2()
        {
            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                Processor
                    .CreateMultivectorComposer()
                    .AddGpTerms(mv1, mv2)
                    ;
            }
        }
        
        [Benchmark]
        public void Gp3()
        {
            foreach (var mv1 in Multivectors)
            foreach (var mv2 in Multivectors)
            {
                Processor
                    .CreateUniformComposer()
                    .AddGpTerms(mv1, mv2)
                    ;
            }
        }
    }
}
