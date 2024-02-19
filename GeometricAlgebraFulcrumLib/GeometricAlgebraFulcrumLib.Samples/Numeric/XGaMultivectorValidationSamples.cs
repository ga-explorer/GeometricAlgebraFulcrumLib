using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Converters.Float64;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric;

public static class XGaMultivectorValidationSamples
{
    public static void Example1()
    {
        var metric = XGaFloat64Processor.Euclidean;

        var set1 = ImmutableSortedSet.Create(3, 7, 9).ToSparseIndexSet();
        var set2 = (IIndexSet) ImmutableSortedSet.Create(3, 6, 9).ToUInt64IndexSet();

        var kv1 = metric.CreateBasisBlade(set1);
        var kv2 = metric.CreateBasisBlade(set2);

        var hashCode1 = set1.GetHashCode();
        var hashCode2 = set2.GetHashCode();
            
        Console.WriteLine(hashCode1);
        Console.WriteLine(hashCode2);
        Console.WriteLine(kv1 == kv2);
    }

    /// <summary>
    /// Compare computation of Euclidean geometric product of basis blades
    /// </summary>
    public static void Example2()
    {
        var metric = XGaFloat64Processor.Euclidean;
        var n = 10;
        var m = 1UL << n;

        var basisBladeIdList1 = 
            m.GetRange().Select(
                id => id.BitPatternToNonUInt64IndexSet()
            ).ToImmutableArray();
            
        foreach (var basisBlade1 in basisBladeIdList1)
        {
            var id1 = basisBlade1.ToUInt64();

            foreach (var basisBlade2 in basisBladeIdList1)
            {
                var id2 = basisBlade2.ToUInt64();

                var idA = id1 ^ id2;
                var signA = id1.EGpSign(id2);

                var egp = 
                    metric.EGp(basisBlade1, basisBlade2);
                    
                var idB = egp.Id.ToUInt64();
                var signB = egp.Sign;

                if (idA != idB || signA != signB)
                {
                    var egp1 = 
                        metric.EGp(basisBlade1, basisBlade2);

                    Console.WriteLine(signA);
                    Console.WriteLine(signB);
                    Console.WriteLine(egp1.ToString());
                    Console.WriteLine();
                }

                //Debug.Assert(
                //    idA == idB
                //);

                //Debug.Assert(
                //    signDiff == 0
                //);
            }
        }
    }

    /// <summary>
    /// Compare timing of Euclidean geometric product of basis blades
    /// </summary>
    public static void Example3()
    {
        var n = 12;
        var m = 1UL << n;

        var metric2 = RGaFloat64Processor.Euclidean;
        IRGaSignedBasisBlade egpTableItem2 = 
            metric2.EGp(0UL, 0UL); // For initializing internal lookup tables

        var basisBladeIdList2 = 
            m.GetRange().ToImmutableArray();
            
        var time1 = DateTime.Now;
            
        foreach (var basisBlade1 in basisBladeIdList2)
        {
            foreach (var basisBlade2 in basisBladeIdList2)
            {
                egpTableItem2 = metric2.EGp(basisBlade1, basisBlade2);
            }
        }

        var time2 = DateTime.Now;

        Console.WriteLine(egpTableItem2.ToString());
        Console.WriteLine($"Time 1: {time2 - time1}");
            

        var metric1 = XGaFloat64Processor.Euclidean;
        IXGaSignedBasisBlade egpTableItem1 = 
            metric1.EGp(EmptyIndexSet.Instance, EmptyIndexSet.Instance); // For initializing internal lookup tables

        var basisBladeIdList1 = 
            m.GetRange().Select(
                id => id.BitPatternToUInt64IndexSet()
            ).ToImmutableArray();

        time1 = DateTime.Now;
            
        foreach (var basisBlade1 in basisBladeIdList1)
        {
            foreach (var basisBlade2 in basisBladeIdList1)
            {
                egpTableItem1 = metric1.EGp(basisBlade1, basisBlade2);

                //Debug.Assert(
                //    metric1.Convert(
                //        metric2.EGp(
                //            basisBlade1.ToUInt64(), 
                //            basisBlade2.ToUInt64()
                //        )
                //    ).Equals(egpTableItem1)
                //);
            }
        }

        time2 = DateTime.Now;

        Console.WriteLine(egpTableItem1.ToString());
        Console.WriteLine($"Time 2: {time2 - time1}");
    }

    public static void Example4()
    {
        var n = 10;
        var m = 1UL << n;

        var rgaProcessor = 
            RGaFloat64Processor.Euclidean;
            
        var xgaProcessor = 
            XGaFloat64Processor.Euclidean;

        var randomComposer = rgaProcessor.CreateRGaRandomComposer(n);
            
        var mvList1 = new List<RGaFloat64Multivector>
        {
            randomComposer.GetScalar(),
            randomComposer.GetSparseVector(n / 2),
            randomComposer.GetSparseBivector(n * (n - 1) / 2)
        };

        for (var grade = 3; grade <= n; grade++)
        {
            var k = (int) Math.Max(1, grade.GetBinomialCoefficient(grade) / 2);

            mvList1.Add(
                randomComposer.GetSparseKVectorOfGrade(grade, k)
            );
        }

        mvList1.Add(
            randomComposer.GetMultivector((int)m / 2)
        );
            
        var mvList2 = mvList1.Select(
            mv => xgaProcessor.Convert(mv)
        ).ToList();


        for (var i1 = 0; i1 < mvList1.Count; i1++)
        {
            var mv11 = mvList1[i1];
            var mv12 = mvList2[i1];

            for (var i2 = 0; i2 < mvList1.Count; i2++)
            {
                var mv21 = mvList1[i2];
                var mv22 = mvList2[i2];

                var mv1 = mv11.EGp(mv21);
                var mv2 = mv12.EGp(mv22);

                var mvDiff = 
                    mv2.Subtract(xgaProcessor.Convert(mv1)).GetPart(s => !s.IsNearZero());

                Debug.Assert(
                    mvDiff.IsZero
                );

                mv1 = mv11.Op(mv21);
                mv2 = mv12.Op(mv22);

                mvDiff = 
                    mv2.Subtract(xgaProcessor.Convert(mv1)).GetPart(s => !s.IsNearZero());

                Debug.Assert(
                    mvDiff.IsZero
                );

                mv1 = mv11.ELcp(mv21);
                mv2 = mv12.ELcp(mv22);

                mvDiff = 
                    mv2.Subtract(xgaProcessor.Convert(mv1)).GetPart(s => !s.IsNearZero());

                Debug.Assert(
                    mvDiff.IsZero
                );

                mv1 = mv11.ERcp(mv21);
                mv2 = mv12.ERcp(mv22);

                mvDiff = 
                    mv2.Subtract(xgaProcessor.Convert(mv1)).GetPart(s => !s.IsNearZero());

                Debug.Assert(
                    mvDiff.IsZero
                );

                var esp1 = mv11.ESp(mv21).ScalarValue();
                var esp2 = mv12.ESp(mv22).ScalarValue();

                var espDiff = 
                    esp2 - esp1;

                Debug.Assert(
                    espDiff.IsNearZero()
                );
            }
        }
    }

    public static void Example5()
    {
        var n = 10;
        var m = 1UL << n;

        var processor = RGaFloat64Processor.Euclidean;

        var randomComposer = processor.CreateRGaRandomComposer(n, 10);

        var scalar1 = randomComposer.GetScalar();
        var vector1 = randomComposer.GetSparseVector(n / 2);
        var bivector1 = randomComposer.GetSparseBivector(n * (n - 1) / 2);
        var kVector51 = randomComposer.GetSparseKVectorOfGrade(5, (int) n.GetBinomialCoefficient(5) / 2);
        var kVector81 = randomComposer.GetSparseKVectorOfGrade(8, (int) n.GetBinomialCoefficient(8) / 2);
        var mv1 = randomComposer.GetUniformMultivector((int)m / 2);

        var scalar2 = randomComposer.GetScalar();
        var vector2 = randomComposer.GetSparseVector(n / 2);
        var bivector2 = randomComposer.GetSparseBivector(n * (n - 1) / 2);
        var kVector52 = randomComposer.GetSparseKVectorOfGrade(5, (int) n.GetBinomialCoefficient(5) / 2);
        var kVector82 = randomComposer.GetSparseKVectorOfGrade(8, (int) n.GetBinomialCoefficient(8) / 2);
        var mv2 = randomComposer.GetUniformMultivector((int)m / 2);

        Debug.Assert(
            scalar1.EGp(scalar2).Subtract(scalar1.EGp(scalar2)).IsZero
        );
            
        Debug.Assert(
            scalar1.EGp(vector2).Subtract(scalar1.EGp(vector2)).IsZero
        );
            
        Debug.Assert(
            scalar1.EGp(bivector2).Subtract(scalar1.EGp(bivector2)).IsZero
        );

        //var m1 = scalar1.EGp(kVector52).ToEGaMultivector();
        //var s1 = scalar1.ToEGaKVector();
        //var s2 = kVector52.ToEGaKVector();
        //var m2 = s1.EGp(s2);
            
        Debug.Assert(
            scalar1.EGp(kVector52).Subtract(scalar1.EGp(kVector52)).IsZero
        );
            
        Debug.Assert(
            scalar1.EGp(kVector82).Subtract(scalar1.EGp(kVector82)).IsZero
        );
            
        Debug.Assert(
            scalar1.EGp(mv2).Subtract(scalar1.EGp(mv2)).IsZero
        );
            
        Debug.Assert(
            vector1.EGp(scalar2).Subtract(vector1.EGp(scalar2)).IsZero
        );
            
        Debug.Assert(
            vector1.EGp(vector2).Subtract(vector1.EGp(vector2)).IsZero
        );
            
        Debug.Assert(
            vector1.EGp(bivector2).Subtract(vector1.EGp(bivector2)).IsZero
        );
            
        Debug.Assert(
            vector1.EGp(kVector52).Subtract(vector1.EGp(kVector52)).IsZero
        );
            
        Debug.Assert(
            vector1.EGp(kVector82).Subtract(vector1.EGp(kVector82)).IsZero
        );
            
        Debug.Assert(
            vector1.EGp(mv2).Subtract(vector1.EGp(mv2)).IsZero
        );

        Debug.Assert(
            bivector1.EGp(scalar2).Subtract(bivector1.EGp(scalar2)).IsZero
        );
            
        Debug.Assert(
            bivector1.EGp(vector2).Subtract(bivector1.EGp(vector2)).IsZero
        );
            
        Debug.Assert(
            bivector1.EGp(bivector2).Subtract(bivector1.EGp(bivector2)).IsZero
        );
            
        Debug.Assert(
            bivector1.EGp(kVector52).Subtract(bivector1.EGp(kVector52)).IsZero
        );
            
        Debug.Assert(
            bivector1.EGp(kVector82).Subtract(bivector1.EGp(kVector82)).IsZero
        );
            
        Debug.Assert(
            bivector1.EGp(mv2).Subtract(bivector1.EGp(mv2)).IsZero
        );
            
        Debug.Assert(
            kVector51.EGp(scalar2).Subtract(kVector51.EGp(scalar2)).IsZero
        );
            
        Debug.Assert(
            kVector51.EGp(vector2).Subtract(kVector51.EGp(vector2)).IsZero
        );
            
        Debug.Assert(
            kVector51.EGp(bivector2).Subtract(kVector51.EGp(bivector2)).IsZero
        );
            
        Debug.Assert(
            kVector51.EGp(kVector52).Subtract(kVector51.EGp(kVector52)).IsZero
        );
            
        Debug.Assert(
            kVector51.EGp(kVector82).Subtract(kVector51.EGp(kVector82)).IsZero
        );
            
        Debug.Assert(
            kVector51.EGp(mv2).Subtract(kVector51.EGp(mv2)).IsZero
        );

        Debug.Assert(
            kVector81.EGp(scalar2).Subtract(kVector81.EGp(scalar2)).IsZero
        );
            
        Debug.Assert(
            kVector81.EGp(vector2).Subtract(kVector81.EGp(vector2)).IsZero
        );
            
        Debug.Assert(
            kVector81.EGp(bivector2).Subtract(kVector81.EGp(bivector2)).IsZero
        );
            
        Debug.Assert(
            kVector81.EGp(kVector52).Subtract(kVector81.EGp(kVector52)).IsZero
        );
            
        Debug.Assert(
            kVector81.EGp(kVector82).Subtract(kVector81.EGp(kVector82)).IsZero
        );

        Debug.Assert(
            kVector81.EGp(mv2).Subtract(kVector81.EGp(mv2)).IsZero
        );

        Debug.Assert(
            mv1.EGp(scalar2).Subtract(mv1.EGp(scalar2)).IsZero
        );

        Debug.Assert(
            mv1.EGp(vector2).Subtract(mv1.EGp(vector2)).GetPart(s => !s.IsNearZero()).IsZero
        );

        Debug.Assert(
            mv1.EGp(bivector2).Subtract(mv1.EGp(bivector2)).GetPart(s => !s.IsNearZero()).IsZero
        );

        Debug.Assert(
            mv1.EGp(kVector52).Subtract(mv1.EGp(kVector52)).GetPart(s => !s.IsNearZero()).IsZero
        );

        Debug.Assert(
            mv1.EGp(kVector82).Subtract(mv1.EGp(kVector82)).GetPart(s => !s.IsNearZero()).IsZero
        );
            
        Debug.Assert(
            mv1.EGp(mv2).Subtract(mv1.EGp(mv2)).GetPart(s => !s.IsNearZero()).IsZero
        );
    }
        
    public static void Example6()
    {
        var n = 10;
        var m = 1UL << n;

        var processor = RGaFloat64Processor.Euclidean;

        var randomComposer = processor.CreateRGaRandomComposer(n, 10);

        var scalar1 = randomComposer.GetScalar();
        var vector1 = randomComposer.GetSparseVector(n / 2);
        var bivector1 = randomComposer.GetSparseBivector(n * (n - 1) / 2);
        var kVector51 = randomComposer.GetSparseKVectorOfGrade(5, (int) n.GetBinomialCoefficient(5) / 2);
        var kVector81 = randomComposer.GetSparseKVectorOfGrade(8, (int) n.GetBinomialCoefficient(8) / 2);
        var mv1 = randomComposer.GetUniformMultivector((int)m / 2);

        var scalar2 = randomComposer.GetScalar();
        var vector2 = randomComposer.GetSparseVector(n / 2);
        var bivector2 = randomComposer.GetSparseBivector(n * (n - 1) / 2);
        var kVector52 = randomComposer.GetSparseKVectorOfGrade(5, (int) n.GetBinomialCoefficient(5) / 2);
        var kVector82 = randomComposer.GetSparseKVectorOfGrade(8, (int) n.GetBinomialCoefficient(8) / 2);
        var mv2 = randomComposer.GetUniformMultivector((int)m / 2);
            
        Debug.Assert(
            scalar1.Op(scalar2).Subtract(scalar1.Op(scalar2)).IsZero
        );
            
        Debug.Assert(
            scalar1.Op(vector2).Subtract(scalar1.Op(vector2)).IsZero
        );
            
        Debug.Assert(
            scalar1.Op(bivector2).Subtract(scalar1.Op(bivector2)).IsZero
        );

        //var m1 = scalar1.Op(kVector52).ToEGaMultivector();
        //var s1 = scalar1.ToEGaKVector();
        //var s2 = kVector52.ToEGaKVector();
        //var m2 = s1.Op(s2);
            
        Debug.Assert(
            scalar1.Op(kVector52).Subtract(scalar1.Op(kVector52)).IsZero
        );
            
        Debug.Assert(
            scalar1.Op(kVector82).Subtract(scalar1.Op(kVector82)).IsZero
        );
            
        Debug.Assert(
            scalar1.Op(mv2).Subtract(scalar1.Op(mv2)).IsZero
        );
            
        Debug.Assert(
            vector1.Op(scalar2).Subtract(vector1.Op(scalar2)).IsZero
        );
            
        Debug.Assert(
            vector1.Op(vector2).Subtract(vector1.Op(vector2)).IsZero
        );
            
        Debug.Assert(
            vector1.Op(bivector2).Subtract(vector1.Op(bivector2)).IsZero
        );
            
        Debug.Assert(
            vector1.Op(kVector52).Subtract(vector1.Op(kVector52)).IsZero
        );
            
        Debug.Assert(
            vector1.Op(kVector82).Subtract(vector1.Op(kVector82)).IsZero
        );
            
        Debug.Assert(
            vector1.Op(mv2).Subtract(vector1.Op(mv2)).IsZero
        );
            
        Debug.Assert(
            bivector1.Op(scalar2).Subtract(bivector1.Op(scalar2)).IsZero
        );
            
        Debug.Assert(
            bivector1.Op(vector2).Subtract(bivector1.Op(vector2)).IsZero
        );
            
        Debug.Assert(
            bivector1.Op(bivector2).Subtract(bivector1.Op(bivector2)).IsZero
        );
            
        Debug.Assert(
            bivector1.Op(kVector52).Subtract(bivector1.Op(kVector52)).IsZero
        );
            
        Debug.Assert(
            bivector1.Op(kVector82).Subtract(bivector1.Op(kVector82)).IsZero
        );
            
        Debug.Assert(
            bivector1.Op(mv2).Subtract(bivector1.Op(mv2)).IsZero
        );
            
        Debug.Assert(
            kVector51.Op(scalar2).Subtract(kVector51.Op(scalar2)).IsZero
        );
            
        Debug.Assert(
            kVector51.Op(vector2).Subtract(kVector51.Op(vector2)).IsZero
        );
            
        Debug.Assert(
            kVector51.Op(bivector2).Subtract(kVector51.Op(bivector2)).IsZero
        );
            
        Debug.Assert(
            kVector51.Op(kVector52).Subtract(kVector51.Op(kVector52)).IsZero
        );
            
        Debug.Assert(
            kVector51.Op(kVector82).Subtract(kVector51.Op(kVector82)).IsZero
        );
            
        Debug.Assert(
            kVector51.Op(mv2).Subtract(kVector51.Op(mv2)).IsZero
        );
            
        Debug.Assert(
            kVector81.Op(scalar2).Subtract(kVector81.Op(scalar2)).IsZero
        );
            
        Debug.Assert(
            kVector81.Op(vector2).Subtract(kVector81.Op(vector2)).IsZero
        );
            
        Debug.Assert(
            kVector81.Op(bivector2).Subtract(kVector81.Op(bivector2)).IsZero
        );
            
        Debug.Assert(
            kVector81.Op(kVector52).Subtract(kVector81.Op(kVector52)).IsZero
        );
            
        Debug.Assert(
            kVector81.Op(kVector82).Subtract(kVector81.Op(kVector82)).IsZero
        );
            
        Debug.Assert(
            kVector81.Op(mv2).Subtract(kVector81.Op(mv2)).IsZero
        );
            
        Debug.Assert(
            mv1.Op(scalar2).Subtract(mv1.Op(scalar2)).IsZero
        );
            
        Debug.Assert(
            mv1.Op(vector2).Subtract(mv1.Op(vector2)).GetPart(s => !s.IsNearZero()).IsZero
        );

        Debug.Assert(
            mv1.Op(bivector2).Subtract(mv1.Op(bivector2)).GetPart(s => !s.IsNearZero()).IsZero
        );

        Debug.Assert(
            mv1.Op(kVector52).Subtract(mv1.Op(kVector52)).GetPart(s => !s.IsNearZero()).IsZero
        );

        Debug.Assert(
            mv1.Op(kVector82).Subtract(mv1.Op(kVector82)).GetPart(s => !s.IsNearZero()).IsZero
        );

        Debug.Assert(
            mv1.Op(mv2).Subtract(mv1.Op(mv2)).GetPart(s => !s.IsNearZero()).IsZero
        );
    }

    public static void Example7()
    {
        var n = 10;
        var m = 1UL << n;

        var processor = RGaFloat64Processor.Euclidean;

        var randomComposer = processor.CreateRGaRandomComposer(n, 10);

        var scalar1 = randomComposer.GetScalar();
        var vector1 = randomComposer.GetSparseVector(n / 2);
        var bivector1 = randomComposer.GetSparseBivector(n * (n - 1) / 2);
        var kVector51 = randomComposer.GetSparseKVectorOfGrade(5, (int) n.GetBinomialCoefficient(5) / 2);
        var kVector81 = randomComposer.GetSparseKVectorOfGrade(8, (int) n.GetBinomialCoefficient(8) / 2);
        var mv1 = randomComposer.GetUniformMultivector((int)m / 2);

        var scalar2 = randomComposer.GetScalar();
        var vector2 = randomComposer.GetSparseVector(n / 2);
        var bivector2 = randomComposer.GetSparseBivector(n * (n - 1) / 2);
        var kVector52 = randomComposer.GetSparseKVectorOfGrade(5, (int) n.GetBinomialCoefficient(5) / 2);
        var kVector82 = randomComposer.GetSparseKVectorOfGrade(8, (int) n.GetBinomialCoefficient(8) / 2);
        var mv2 = randomComposer.GetUniformMultivector((int)m / 2);

        RGaFloat64Multivector diff =
            scalar1.ELcp(scalar2).Subtract(
                scalar1.ELcp(scalar2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            scalar1.ELcp(vector2).Subtract(
                scalar1.ELcp(vector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            scalar1.ELcp(bivector2).Subtract(
                scalar1.ELcp(bivector2)
            );

        Debug.Assert(diff.IsZero);

        //var m1 = scalar1.ELcp(kVector52).ToEGaMultivector();
        //var s1 = scalar1.ToEGaKVector();
        //var s2 = kVector52.ToEGaKVector();
        //var m2 = s1.ELcp(s2);

        diff =
            scalar1.ELcp(kVector52).Subtract(
                scalar1.ELcp(kVector52)
            );

        Debug.Assert(diff.IsZero);

        diff =
            scalar1.ELcp(kVector82).Subtract(
                scalar1.ELcp(kVector82)
            );

        Debug.Assert(diff.IsZero);

        diff =
            scalar1.ELcp(mv2).Subtract(
                scalar1.ELcp(mv2)
            );

        Debug.Assert(diff.IsZero);



        diff =
            vector1.ELcp(scalar2).Subtract(
                vector1.ELcp(scalar2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ELcp(vector2).Subtract(
                vector1.ELcp(vector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ELcp(bivector2).Subtract(
                vector1.ELcp(bivector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ELcp(kVector52).Subtract(
                vector1.ELcp(kVector52)
            );

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ELcp(kVector82).Subtract(
                vector1.ELcp(kVector82)
            );

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ELcp(mv2).Subtract(
                vector1.ELcp(mv2)
            );

        Debug.Assert(diff.IsZero);



        diff =
            bivector1.ELcp(scalar2).Subtract(
                bivector1.ELcp(scalar2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ELcp(vector2).Subtract(
                bivector1.ELcp(vector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ELcp(bivector2).Subtract(
                bivector1.ELcp(bivector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ELcp(kVector52).Subtract(
                bivector1.ELcp(kVector52)
            );

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ELcp(kVector82).Subtract(
                bivector1.ELcp(kVector82)
            );

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ELcp(mv2).Subtract(
                bivector1.ELcp(mv2)
            );

        Debug.Assert(diff.IsZero);



        diff =
            kVector51.ELcp(scalar2).Subtract(
                kVector51.ELcp(scalar2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ELcp(vector2).Subtract(
                kVector51.ELcp(vector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ELcp(bivector2).Subtract(
                kVector51.ELcp(bivector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ELcp(kVector52).Subtract(
                kVector51.ELcp(kVector52)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ELcp(kVector82).Subtract(
                kVector51.ELcp(kVector82)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ELcp(mv2).Subtract(
                kVector51.ELcp(mv2)
            );

        Debug.Assert(diff.IsZero);



        diff =
            kVector81.ELcp(scalar2).Subtract(
                kVector81.ELcp(scalar2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ELcp(vector2).Subtract(
                kVector81.ELcp(vector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ELcp(bivector2).Subtract(
                kVector81.ELcp(bivector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ELcp(kVector52).Subtract(
                kVector81.ELcp(kVector52)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ELcp(kVector82).Subtract(
                kVector81.ELcp(kVector82)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ELcp(mv2).Subtract(
                kVector81.ELcp(mv2)
            );

        Debug.Assert(diff.IsZero);



        diff =
            mv1.ELcp(scalar2).Subtract(
                mv1.ELcp(scalar2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ELcp(vector2).Subtract(
                mv1.ELcp(vector2)
            ).MapScalars(
                s => s.IsNearZero() ? 0 : s
            );

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ELcp(bivector2).Subtract(
                mv1.ELcp(bivector2)
            ).MapScalars(
                s => s.IsNearZero() ? 0 : s
            );

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ELcp(kVector52).Subtract(
                mv1.ELcp(kVector52)
            ).MapScalars(
                s => s.IsNearZero() ? 0 : s
            );

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ELcp(kVector82).Subtract(
                mv1.ELcp(kVector82)
            ).MapScalars(
                s => s.IsNearZero() ? 0 : s
            );

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ELcp(mv2).Subtract(
                mv1.ELcp(mv2)
            ).MapScalars(
                s => s.IsNearZero() ? 0 : s
            );

        Debug.Assert(diff.IsZero);
    }
        
    public static void Example8()
    {
        var n = 10;
        var m = 1UL << n;

        var processor = RGaFloat64Processor.Euclidean;

        var randomComposer = processor.CreateRGaRandomComposer(n, 10);

        var scalar1 = randomComposer.GetScalar();
        var vector1 = randomComposer.GetSparseVector(n / 2);
        var bivector1 = randomComposer.GetSparseBivector(n * (n - 1) / 2);
        var kVector51 = randomComposer.GetSparseKVectorOfGrade(5, (int) n.GetBinomialCoefficient(5) / 2);
        var kVector81 = randomComposer.GetSparseKVectorOfGrade(8, (int) n.GetBinomialCoefficient(8) / 2);
        var mv1 = randomComposer.GetUniformMultivector((int)m / 2);

        var scalar2 = randomComposer.GetScalar();
        var vector2 = randomComposer.GetSparseVector(n / 2);
        var bivector2 = randomComposer.GetSparseBivector(n * (n - 1) / 2);
        var kVector52 = randomComposer.GetSparseKVectorOfGrade(5, (int) n.GetBinomialCoefficient(5) / 2);
        var kVector82 = randomComposer.GetSparseKVectorOfGrade(8, (int) n.GetBinomialCoefficient(8) / 2);
        var mv2 = randomComposer.GetUniformMultivector((int)m / 2);

        RGaFloat64Multivector diff =
            scalar1.ERcp(scalar2).Subtract(
                scalar1.ERcp(scalar2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            scalar1.ERcp(vector2).Subtract(
                scalar1.ERcp(vector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            scalar1.ERcp(bivector2).Subtract(
                scalar1.ERcp(bivector2)
            );

        Debug.Assert(diff.IsZero);
            
        diff =
            scalar1.ERcp(kVector52).Subtract(
                scalar1.ERcp(kVector52)
            );

        Debug.Assert(diff.IsZero);

        diff =
            scalar1.ERcp(kVector82).Subtract(
                scalar1.ERcp(kVector82)
            );

        Debug.Assert(diff.IsZero);

        diff =
            scalar1.ERcp(mv2).Subtract(
                scalar1.ERcp(mv2)
            );

        Debug.Assert(diff.IsZero);



        diff =
            vector1.ERcp(scalar2).Subtract(
                vector1.ERcp(scalar2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ERcp(vector2).Subtract(
                vector1.ERcp(vector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ERcp(bivector2).Subtract(
                vector1.ERcp(bivector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ERcp(kVector52).Subtract(
                vector1.ERcp(kVector52)
            );

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ERcp(kVector82).Subtract(
                vector1.ERcp(kVector82)
            );

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ERcp(mv2).Subtract(
                vector1.ERcp(mv2)
            );

        Debug.Assert(diff.IsZero);
            

        diff =
            bivector1.ERcp(scalar2).Subtract(
                bivector1.ERcp(scalar2)
            );

        Debug.Assert(diff.IsZero);

        //var m1 = bivector1.ERcp(vector2).ToEGaMultivector();
        //var s1 = bivector1.ToEGaBivector();
        //var s2 = vector2.ToEGaVector();
        //var m2 = s1.ERcp(s2);

        diff =
            bivector1.ERcp(vector2).Subtract(
                bivector1.ERcp(vector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ERcp(bivector2).Subtract(
                bivector1.ERcp(bivector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ERcp(kVector52).Subtract(
                bivector1.ERcp(kVector52)
            );

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ERcp(kVector82).Subtract(
                bivector1.ERcp(kVector82)
            );

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ERcp(mv2).Subtract(
                bivector1.ERcp(mv2)
            );

        Debug.Assert(diff.IsZero);



        diff =
            kVector51.ERcp(scalar2).Subtract(
                kVector51.ERcp(scalar2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ERcp(vector2).Subtract(
                kVector51.ERcp(vector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ERcp(bivector2).Subtract(
                kVector51.ERcp(bivector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ERcp(kVector52).Subtract(
                kVector51.ERcp(kVector52)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ERcp(kVector82).Subtract(
                kVector51.ERcp(kVector82)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ERcp(mv2).Subtract(
                kVector51.ERcp(mv2)
            );

        Debug.Assert(diff.IsZero);



        diff =
            kVector81.ERcp(scalar2).Subtract(
                kVector81.ERcp(scalar2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ERcp(vector2).Subtract(
                kVector81.ERcp(vector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ERcp(bivector2).Subtract(
                kVector81.ERcp(bivector2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ERcp(kVector52).Subtract(
                kVector81.ERcp(kVector52)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ERcp(kVector82).Subtract(
                kVector81.ERcp(kVector82)
            );

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ERcp(mv2).Subtract(
                kVector81.ERcp(mv2)
            );

        Debug.Assert(diff.IsZero);



        diff =
            mv1.ERcp(scalar2).Subtract(
                mv1.ERcp(scalar2)
            );

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ERcp(vector2).Subtract(
                mv1.ERcp(vector2)
            ).MapScalars(
                s => s.IsNearZero() ? 0 : s
            );

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ERcp(bivector2).Subtract(
                mv1.ERcp(bivector2)
            ).MapScalars(
                s => s.IsNearZero() ? 0 : s
            );

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ERcp(kVector52).Subtract(
                mv1.ERcp(kVector52)
            ).MapScalars(
                s => s.IsNearZero() ? 0 : s
            );

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ERcp(kVector82).Subtract(
                mv1.ERcp(kVector82)
            ).MapScalars(
                s => s.IsNearZero() ? 0 : s
            );

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ERcp(mv2).Subtract(
                mv1.ERcp(mv2)
            ).MapScalars(
                s => s.IsNearZero() ? 0 : s
            );

        Debug.Assert(diff.IsZero);
    }

    public static void Example9()
    {
        var n = 10;
        var m = 1UL << n;
            
        var processor = RGaFloat64Processor.Euclidean;

        var randomComposer = processor.CreateRGaRandomComposer(n, 10);

        var scalar1 = randomComposer.GetScalar();
        var vector1 = randomComposer.GetSparseVector(n / 2);
        var bivector1 = randomComposer.GetSparseBivector(n * (n - 1) / 2);
        var kVector51 = randomComposer.GetSparseKVectorOfGrade(5, (int) n.GetBinomialCoefficient(5) / 2);
        var kVector81 = randomComposer.GetSparseKVectorOfGrade(8, (int) n.GetBinomialCoefficient(8) / 2);
        var mv1 = randomComposer.GetUniformMultivector((int)m / 2);

        var scalar2 = randomComposer.GetScalar();
        var vector2 = randomComposer.GetSparseVector(n / 2);
        var bivector2 = randomComposer.GetSparseBivector(n * (n - 1) / 2);
        var kVector52 = randomComposer.GetSparseKVectorOfGrade(5, (int) n.GetBinomialCoefficient(5) / 2);
        var kVector82 = randomComposer.GetSparseKVectorOfGrade(8, (int) n.GetBinomialCoefficient(8) / 2);
        var mv2 = randomComposer.GetUniformMultivector((int)m / 2);

        var diff =
            scalar1.ESp(scalar2) -
            scalar1.ESp(scalar2);

        Debug.Assert(diff.IsZero);

        diff =
            scalar1.ESp(vector2) - 
            scalar1.ESp(vector2);

        Debug.Assert(diff.IsZero);

        diff =
            scalar1.ESp(bivector2) - 
            scalar1.ESp(bivector2);

        Debug.Assert(diff.IsZero);

        //var m1 = scalar1.ESp(kVector52).ToEGaMultivector();
        //var s1 = scalar1.ToEGaKVector();
        //var s2 = kVector52.ToEGaKVector();
        //var m2 = s1.ESp(s2);

        diff =
            scalar1.ESp(kVector52) - 
            scalar1.ESp(kVector52);

        Debug.Assert(diff.IsZero);

        diff =
            scalar1.ESp(kVector82) - 
            scalar1.ESp(kVector82);

        Debug.Assert(diff.IsZero);

        diff =
            scalar1.ESp(mv2) - 
            scalar1.ESp(mv2);

        Debug.Assert(diff.IsZero);



        diff =
            vector1.ESp(scalar2) - 
            vector1.ESp(scalar2);

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ESp(vector2) - 
            vector1.ESp(vector2);

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ESp(bivector2) - 
            vector1.ESp(bivector2);

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ESp(kVector52) - 
            vector1.ESp(kVector52);

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ESp(kVector82) - 
            vector1.ESp(kVector82);

        Debug.Assert(diff.IsZero);

        diff =
            vector1.ESp(mv2) - 
            vector1.ESp(mv2);

        Debug.Assert(diff.IsZero);



        diff =
            bivector1.ESp(scalar2) - 
            bivector1.ESp(scalar2);

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ESp(vector2) - 
            bivector1.ESp(vector2);

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ESp(bivector2) - 
            bivector1.ESp(bivector2);

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ESp(kVector52) - 
            bivector1.ESp(kVector52);

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ESp(kVector82) - 
            bivector1.ESp(kVector82);

        Debug.Assert(diff.IsZero);

        diff =
            bivector1.ESp(mv2) - 
            bivector1.ESp(mv2);

        Debug.Assert(diff.IsZero);



        diff =
            kVector51.ESp(scalar2) - 
            kVector51.ESp(scalar2);

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ESp(vector2) - 
            kVector51.ESp(vector2);

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ESp(bivector2) - 
            kVector51.ESp(bivector2);

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ESp(kVector52) - 
            kVector51.ESp(kVector52);

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ESp(kVector82) - 
            kVector51.ESp(kVector82);

        Debug.Assert(diff.IsZero);

        diff =
            kVector51.ESp(mv2) - 
            kVector51.ESp(mv2);

        Debug.Assert(diff.IsZero);



        diff =
            kVector81.ESp(scalar2) - 
            kVector81.ESp(scalar2);

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ESp(vector2) - 
            kVector81.ESp(vector2);

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ESp(bivector2) - 
            kVector81.ESp(bivector2);

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ESp(kVector52) - 
            kVector81.ESp(kVector52);

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ESp(kVector82) - 
            kVector81.ESp(kVector82);

        Debug.Assert(diff.IsZero);

        diff =
            kVector81.ESp(mv2) - 
            kVector81.ESp(mv2);

        Debug.Assert(diff.IsZero);



        diff =
            mv1.ESp(scalar2) - 
            mv1.ESp(scalar2);

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ESp(vector2) - 
            mv1.ESp(vector2);

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ESp(bivector2) - 
            mv1.ESp(bivector2);

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ESp(kVector52) - 
            mv1.ESp(kVector52);

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ESp(kVector82) - 
            mv1.ESp(kVector82);

        Debug.Assert(diff.IsZero);

        diff =
            mv1.ESp(mv2) - 
            mv1.ESp(mv2);

        Debug.Assert(diff.IsZero);
    }

        
    private static void ValidateDifference(double mv1, double mv2)
    {
        var mvDiff = mv2 - mv1;

        Debug.Assert(
            mvDiff.IsNearZero()
        );
    }
        
    private static void ValidateDifference(RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
    {
        var mvDiff =
            mv2.Subtract(mv1).MapScalars(s => s.IsNearZero() ? 0 : s);

        Debug.Assert(
            mvDiff.IsZero
        );
    }

    private static void ValidateDifference(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        var mvDiff =
            mv2.Subtract(mv1).MapScalars(s => s.IsNearZero() ? 0 : s);

        Debug.Assert(
            mvDiff.IsZero
        );
    }
        
    private static void ValidateDifference(XGaMultivector<double> mv1, XGaMultivector<double> mv2)
    {
        var mvDiff =
            mv2.Subtract(mv1).MapScalars(s => s.IsNearZero() ? 0 : s);

        Debug.Assert(
            mvDiff.IsZero
        );
    }
        
    private static void ValidateDifference(RGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        var mv3 = mv2.Processor.Convert(mv1);

        var mvDiff =
            mv2.Subtract(mv3).GetPart(s => !s.IsNearZero());

        Debug.Assert(
            mvDiff.IsZero
        );
    }

    public static void Example10()
    {
        var time1 = DateTime.Now;

        var n = 10;
        var m = 1UL << n;

        var metric = XGaFloat64Processor.Euclidean;

        var basisBlades = 
            metric.GetBasisBlades(n).ToImmutableArray();

        foreach (var basisBlade1 in basisBlades)
        {
            foreach (var basisBlade2 in basisBlades)
            {
                var sign1 = basisBlade1.EGp(basisBlade2).Sign;
                var sign2 = basisBlade1.EGpSign(basisBlade2);

                Debug.Assert(
                    sign1 == sign2
                );

                sign1 = basisBlade1.Gp(basisBlade2).Sign;
                sign2 = basisBlade1.GpSign(basisBlade2);

                Debug.Assert(
                    sign1 == sign2
                );
            }
        }

        var randomComposer = metric.CreateXGaRandomComposer(n, 10);
            
        var mvList1 = new List<XGaFloat64Multivector>
        {
            randomComposer.GetScalar(),
            randomComposer.GetScalar(),
            randomComposer.GetSparseVector(n / 2),
            randomComposer.GetSparseVector(n / 2),
            randomComposer.GetSparseBivector(n * (n - 1) / 2),
            randomComposer.GetSparseBivector(n * (n - 1) / 2)
        };

        for (var grade = 3; grade < n; grade++)
        {
            var k = (int) Math.Max(1, grade.GetBinomialCoefficient(grade) / 2);

            mvList1.Add(
                randomComposer.GetSparseKVectorOfGrade(grade, k)
            );

            mvList1.Add(
                randomComposer.GetSparseKVectorOfGrade(grade, k)
            );
        }

        mvList1.Add(
            randomComposer.GetKVectorOfGrade(n)
        );
            
        mvList1.Add(
            randomComposer.GetKVectorOfGrade(n)
        );

        mvList1.Add(
            randomComposer.GetMultivector((int)m / 2)
        );
            
        mvList1.Add(
            randomComposer.GetMultivector((int)m / 2)
        );


        var mvList2 = mvList1.Select(
            mv => mv.ToUniformMultivector()
        ).ToList();


        for (var i1 = 0; i1 < mvList1.Count; i1++)
        {
            var mv11 = mvList1[i1];
            var mv12 = mvList2[i1];

            // Validate unary operations
            ValidateDifference(
                mv11,
                mv12
            );

            var scalar = randomComposer.GetScalarValue();
            ValidateDifference(
                mv11.Times(scalar),
                mv12.Times(scalar)
            );

            ValidateDifference(
                mv11.Negative(),
                mv12.Negative()
            );

            ValidateDifference(
                mv11.Reverse(),
                mv12.Reverse()
            );
                
            ValidateDifference(
                mv11.GradeInvolution(),
                mv12.GradeInvolution()
            );
                
            ValidateDifference(
                mv11.CliffordConjugate(),
                mv12.CliffordConjugate()
            );
                
            ValidateDifference(
                mv11.Conjugate(),
                mv12.Conjugate()
            );
                
            ValidateDifference(
                mv11.Conjugate(),
                mv12.Conjugate()
            );

            // Validate binary operations
            for (var i2 = 0; i2 < mvList1.Count; i2++)
            {
                var mv21 = mvList1[i2];
                var mv22 = mvList2[i2];

                Console.WriteLine(
                    mv11.MultivectorClassName + ", " + mv21.MultivectorClassName
                );

                ValidateDifference(
                    mv11.Add(mv21),
                    mv12.Add(mv22)
                );
                    
                ValidateDifference(
                    mv11.Subtract(mv21),
                    mv12.Subtract(mv22)
                );
                    
                ValidateDifference(
                    mv11.Op(mv21),
                    mv12.Op(mv22)
                );
                    
                ValidateDifference(
                    mv11.EGp(mv21),
                    mv12.EGp(mv22)
                );
                    
                ValidateDifference(
                    mv11.ELcp(mv21),
                    mv12.ELcp(mv22)
                );
                    
                ValidateDifference(
                    mv11.ERcp(mv21),
                    mv12.ERcp(mv22)
                );
                    
                ValidateDifference(
                    mv11.Gp(mv21),
                    mv12.Gp(mv22)
                );
                    
                ValidateDifference(
                    mv11.Lcp(mv21),
                    mv12.Lcp(mv22)
                );
                    
                ValidateDifference(
                    mv11.Rcp(mv21),
                    mv12.Rcp(mv22)
                );

                ValidateDifference(
                    mv11.ESp(mv21).ScalarValue(),
                    mv12.ESp(mv22).ScalarValue()
                );
                    
                ValidateDifference(
                    mv11.Sp(mv21).ScalarValue(),
                    mv12.Sp(mv22).ScalarValue()
                );
            }
        }

        var time2 = DateTime.Now;

        Console.WriteLine($"Time: {time2 - time1}");
    }
        
    public static void Example11()
    {
        var time1 = DateTime.Now;

        var n = 10;
        var m = 1UL << n;

        var metric = RGaFloat64Processor.Euclidean;

        var basisBlades = 
            metric.GetBasisBlades(n).ToImmutableArray();

        foreach (var basisBlade1 in basisBlades)
        {
            foreach (var basisBlade2 in basisBlades)
            {
                var sign1 = basisBlade1.EGp(basisBlade2).Sign;
                var sign2 = basisBlade1.EGpSign(basisBlade2);

                Debug.Assert(
                    sign1 == sign2
                );

                sign1 = basisBlade1.Gp(basisBlade2).Sign;
                sign2 = basisBlade1.GpSign(basisBlade2);

                Debug.Assert(
                    sign1 == sign2
                );
            }
        }

        var randomComposer = metric.CreateRGaRandomComposer(n, 10);
            
        var mvList1 = new List<RGaFloat64Multivector>
        {
            randomComposer.GetScalar(),
            randomComposer.GetScalar(),
            randomComposer.GetSparseVector(n / 2),
            randomComposer.GetSparseVector(n / 2),
            randomComposer.GetSparseBivector(n * (n - 1) / 2),
            randomComposer.GetSparseBivector(n * (n - 1) / 2)
        };

        for (var grade = 3; grade < n; grade++)
        {
            var k = (int) Math.Max(1, grade.GetBinomialCoefficient(grade) / 2);

            mvList1.Add(
                randomComposer.GetSparseKVectorOfGrade(grade, k)
            );

            mvList1.Add(
                randomComposer.GetSparseKVectorOfGrade(grade, k)
            );
        }

        mvList1.Add(
            randomComposer.GetKVectorOfGrade(n)
        );
            
        mvList1.Add(
            randomComposer.GetKVectorOfGrade(n)
        );

        mvList1.Add(
            randomComposer.GetMultivector((int)m / 2)
        );
            
        mvList1.Add(
            randomComposer.GetMultivector((int)m / 2)
        );


        var mvList2 = mvList1.Select(
            mv => mv.ToUniformMultivector()
        ).ToList();


        for (var i1 = 0; i1 < mvList1.Count; i1++)
        {
            var mv11 = mvList1[i1];
            var mv12 = mvList2[i1];

            // Validate unary operations
            ValidateDifference(
                mv11,
                mv12
            );

            var scalar = randomComposer.GetScalarValue();
            ValidateDifference(
                mv11.Times(scalar),
                mv12.Times(scalar)
            );

            ValidateDifference(
                mv11.Negative(),
                mv12.Negative()
            );

            ValidateDifference(
                mv11.Reverse(),
                mv12.Reverse()
            );
                
            ValidateDifference(
                mv11.GradeInvolution(),
                mv12.GradeInvolution()
            );
                
            ValidateDifference(
                mv11.CliffordConjugate(),
                mv12.CliffordConjugate()
            );
                
            ValidateDifference(
                mv11.Conjugate(),
                mv12.Conjugate()
            );
                
            ValidateDifference(
                mv11.Conjugate(),
                mv12.Conjugate()
            );

            // Validate binary operations
            for (var i2 = 0; i2 < mvList1.Count; i2++)
            {
                var mv21 = mvList1[i2];
                var mv22 = mvList2[i2];

                Console.WriteLine(
                    mv11.MultivectorClassName + ", " + mv21.MultivectorClassName
                );

                ValidateDifference(
                    mv11.Add(mv21),
                    mv12.Add(mv22)
                );
                    
                ValidateDifference(
                    mv11.Subtract(mv21),
                    mv12.Subtract(mv22)
                );
                    
                ValidateDifference(
                    mv11.Op(mv21),
                    mv12.Op(mv22)
                );
                    
                ValidateDifference(
                    mv11.EGp(mv21),
                    mv12.EGp(mv22)
                );
                    
                ValidateDifference(
                    mv11.ELcp(mv21),
                    mv12.ELcp(mv22)
                );
                    
                ValidateDifference(
                    mv11.ERcp(mv21),
                    mv12.ERcp(mv22)
                );
                    
                ValidateDifference(
                    mv11.Gp(mv21),
                    mv12.Gp(mv22)
                );
                    
                ValidateDifference(
                    mv11.Lcp(mv21),
                    mv12.Lcp(mv22)
                );
                    
                ValidateDifference(
                    mv11.Rcp(mv21),
                    mv12.Rcp(mv22)
                );

                ValidateDifference(
                    mv11.ESp(mv21).ScalarValue(),
                    mv12.ESp(mv22).ScalarValue()
                );
                    
                ValidateDifference(
                    mv11.Sp(mv21).ScalarValue(),
                    mv12.Sp(mv22).ScalarValue()
                );
            }
        }

        var time2 = DateTime.Now;

        Console.WriteLine($"Time: {time2 - time1}");
    }
        
    public static void Example12()
    {
        var n = 10;
        var m = 1UL << n;

        var metric = XGaFloat64Processor.Euclidean;
        var geometricProcessor = RGaFloat64Processor.Euclidean;

        var basisBlades = 
            metric.GetBasisBlades(n).ToImmutableArray();

        var basisBladeIDs = 
            basisBlades.Select(b => b.Id.ToUInt64()).ToImmutableArray();

        for (var i1 = 0; i1 < basisBlades.Length; i1++)
        {
            var basisBlade1 = basisBlades[i1];
            var id1 = basisBladeIDs[i1];

            for (var i2 = 0; i2 < basisBlades.Length; i2++)
            {
                var basisBlade2 = basisBlades[i2];
                var id2 = basisBladeIDs[i2];

                var egp1 = basisBlade1.EGp(basisBlade2);
                var egpId1 = egp1.Id.ToUInt64();
                var egpSign1 = egp1.Sign;

                var egpId2 = id1 ^ id2;
                var egpSign2 = id1.EGpSign(id2);

                Debug.Assert(
                    egpId1 == egpId2 &&
                    egpSign1 == egpSign2
                );
            }
        }

        var randomComposer = geometricProcessor.CreateRGaRandomComposer(n, 10);

        var mvList1 = new List<RGaFloat64Multivector>
        {
            randomComposer.GetMultivector(),
            randomComposer.GetMultivector(),
            randomComposer.GetSparseVector(n / 2),
            randomComposer.GetSparseVector(n / 2),
            randomComposer.GetSparseBivector(n * (n - 1) / 2),
            randomComposer.GetSparseBivector(n * (n - 1) / 2)
        };
            
        for (var grade = 3; grade < n; grade++)
        {
            var k = (int) Math.Max(1, grade.GetBinomialCoefficient(grade) / 2);

            mvList1.Add(
                randomComposer.GetSparseKVectorOfGrade( grade, k)
            );

            mvList1.Add(
                randomComposer.GetSparseKVectorOfGrade( grade, k)
            );
        }
            
        mvList1.Add(
            randomComposer.GetKVectorOfGrade( n)
        );
            
        mvList1.Add(
            randomComposer.GetKVectorOfGrade( n)
        );

        mvList1.Add(
            randomComposer.GetUniformMultivector((int)m / 2)
        );
            
        mvList1.Add(
            randomComposer.GetUniformMultivector((int)m / 2)
        );

        var mvList2 =
            mvList1.Select(mv => mv).ToList();
            
        for (var i1 = 0; i1 < mvList1.Count; i1++)
        {
            var mv11 = mvList1[i1];
            var mv12 = mvList2[i1];

            // Validate unary operations
            ValidateDifference(
                mv11,
                mv12
            );

            var scalar = randomComposer.GetScalarValue();
            ValidateDifference(
                mv11 * scalar,
                mv12.Times(scalar)
            );

            ValidateDifference(
                -mv11,
                mv12.Negative()
            );

            ValidateDifference(
                mv11.Reverse(),
                mv12.Reverse()
            );
                
            ValidateDifference(
                mv11.GradeInvolution(),
                mv12.GradeInvolution()
            );
                
            ValidateDifference(
                mv11.CliffordConjugate(),
                mv12.CliffordConjugate()
            );
                
            ValidateDifference(
                mv11.Conjugate(),
                mv12.Conjugate()
            );
                
            ValidateDifference(
                mv11.Conjugate(),
                mv12.Conjugate()
            );

            // Validate binary operations
            for (var i2 = 0; i2 < mvList1.Count; i2++)
            {
                var mv21 = mvList1[i2];
                var mv22 = mvList2[i2];

                Console.WriteLine(
                    mv12.MultivectorClassName + ", " + mv22.MultivectorClassName
                );

                ValidateDifference(
                    mv11 + mv21,
                    mv12.Add(mv22)
                );
                    
                ValidateDifference(
                    mv11 - mv21,
                    mv12.Subtract(mv22)
                );
                    
                ValidateDifference(
                    mv11.Op(mv21),
                    mv12.Op(mv22)
                );
                    
                ValidateDifference(
                    mv11.EGp(mv21),
                    mv12.EGp(mv22)
                );
                    
                ValidateDifference(
                    mv11.ELcp(mv21),
                    mv12.ELcp(mv22)
                );
                    
                ValidateDifference(
                    mv11.ERcp(mv21),
                    mv12.ERcp(mv22)
                );
                    
                ValidateDifference(
                    mv11.Gp(mv21),
                    mv12.Gp(mv22)
                );
                    
                ValidateDifference(
                    mv11.Lcp(mv21),
                    mv12.Lcp(mv22)
                );
                    
                ValidateDifference(
                    mv11.Rcp(mv21),
                    mv12.Rcp(mv22)
                );

                ValidateDifference(
                    mv11.ESp(mv21).ScalarValue(),
                    mv12.ESp(mv22).ScalarValue()
                );
                    
                ValidateDifference(
                    mv11.Sp(mv21).ScalarValue(),
                    mv12.Sp(mv22).ScalarValue()
                );
            }
        }
    }
}