using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga31;
using GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga41;
using GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga51;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Generations;

[SimpleJob]
public class GenerationsBenchmarks
{
    private List<Ga31Multivector> _multivectorList1;
    private List<Ga51SparseMultivector> _multivectorList2;
    private List<XGaFloat64Multivector> _multivectorList3;
        
    public XGaFloat64ConformalProcessor Processor { get; }
        = XGaFloat64ConformalProcessor.Instance;


    private double GetScalar(Random randomGen)
    {
        const double r = 10 / 64d;

        return randomGen.NextDouble() <= r
            ? randomGen.GetFloat64(-1, 1) : 0;
    }
    
    private Ga31Multivector GetMultivector31(Random randomGen)
    {
        var kVector0 = new Ga31KVector0()
        {
            Scalar = GetScalar(randomGen)
        };

        var kVector1 = new Ga31KVector1()
        {
            Scalar1 = GetScalar(randomGen),
            Scalar2 = GetScalar(randomGen),
            Scalar3 = GetScalar(randomGen),
            Scalar4 = GetScalar(randomGen)
        };
            
        var kVector2 = new Ga31KVector2()
        {
            Scalar12 = GetScalar(randomGen),
            Scalar13 = GetScalar(randomGen),
            Scalar23 = GetScalar(randomGen),
            Scalar14 = GetScalar(randomGen),
            Scalar24 = GetScalar(randomGen),
            Scalar34 = GetScalar(randomGen)
        };
            
        var kVector3 = new Ga31KVector3()
        {
            Scalar123 = GetScalar(randomGen),
            Scalar124 = GetScalar(randomGen),
            Scalar134 = GetScalar(randomGen),
            Scalar234 = GetScalar(randomGen)
        };

        var kVector4 = new Ga31KVector4()
        {
            Scalar1234 = GetScalar(randomGen)
        };

        return Ga31Multivector.Create(
            kVector0,
            kVector1,
            kVector2,
            kVector3,
            kVector4
        );
    }
      
    private Ga41Multivector GetMultivector41(Random randomGen)
    {
        var kVector0 = new Ga41KVector0()
        {
            Scalar = GetScalar(randomGen)
        };

        var kVector1 = new Ga41KVector1()
        {
            Scalar1 = GetScalar(randomGen),
            Scalar2 = GetScalar(randomGen),
            Scalar3 = GetScalar(randomGen),
            Scalar4 = GetScalar(randomGen),
            Scalar5 = GetScalar(randomGen)
        };
            
        var kVector2 = new Ga41KVector2()
        {
            Scalar12 = GetScalar(randomGen),
            Scalar13 = GetScalar(randomGen),
            Scalar23 = GetScalar(randomGen),
            Scalar14 = GetScalar(randomGen),
            Scalar24 = GetScalar(randomGen),
            Scalar34 = GetScalar(randomGen),
            Scalar15 = GetScalar(randomGen),
            Scalar25 = GetScalar(randomGen),
            Scalar35 = GetScalar(randomGen),
            Scalar45 = GetScalar(randomGen)
        };
            
        var kVector3 = new Ga41KVector3()
        {
            Scalar123 = GetScalar(randomGen),
            Scalar124 = GetScalar(randomGen),
            Scalar134 = GetScalar(randomGen),
            Scalar234 = GetScalar(randomGen),
            Scalar125 = GetScalar(randomGen),
            Scalar135 = GetScalar(randomGen),
            Scalar235 = GetScalar(randomGen),
            Scalar145 = GetScalar(randomGen),
            Scalar245 = GetScalar(randomGen),
            Scalar345 = GetScalar(randomGen)
        };

        var kVector4 = new Ga41KVector4()
        {
            Scalar1234 = GetScalar(randomGen),
            Scalar1235 = GetScalar(randomGen),
            Scalar1245 = GetScalar(randomGen),
            Scalar1345 = GetScalar(randomGen),
            Scalar2345 = GetScalar(randomGen)
        };

        var kVector5 = new Ga41KVector5()
        {
            Scalar12345 = GetScalar(randomGen)
        };

        return Ga41Multivector.Create(
            kVector0,
            kVector1,
            kVector2,
            kVector3,
            kVector4,
            kVector5
        );
    }
      
    private Ga51Multivector GetMultivector51(Random randomGen)
    {
        var kVector0 = new Ga51KVector0()
        {
            Scalar = GetScalar(randomGen)
        };

        var kVector1 = new Ga51KVector1()
        {
            Scalar1 = GetScalar(randomGen),
            Scalar2 = GetScalar(randomGen),
            Scalar3 = GetScalar(randomGen),
            Scalar4 = GetScalar(randomGen),
            Scalar5 = GetScalar(randomGen),
            Scalar6 = GetScalar(randomGen)
        };
            
        var kVector2 = new Ga51KVector2()
        {
            Scalar12 = GetScalar(randomGen),
            Scalar13 = GetScalar(randomGen),
            Scalar23 = GetScalar(randomGen),
            Scalar14 = GetScalar(randomGen),
            Scalar24 = GetScalar(randomGen),
            Scalar34 = GetScalar(randomGen),
            Scalar15 = GetScalar(randomGen),
            Scalar25 = GetScalar(randomGen),
            Scalar35 = GetScalar(randomGen),
            Scalar45 = GetScalar(randomGen),
            Scalar16 = GetScalar(randomGen),
            Scalar26 = GetScalar(randomGen),
            Scalar36 = GetScalar(randomGen),
            Scalar46 = GetScalar(randomGen),
            Scalar56 = GetScalar(randomGen)
        };
            
        var kVector3 = new Ga51KVector3()
        {
            Scalar123 = GetScalar(randomGen),
            Scalar124 = GetScalar(randomGen),
            Scalar134 = GetScalar(randomGen),
            Scalar234 = GetScalar(randomGen),
            Scalar125 = GetScalar(randomGen),
            Scalar135 = GetScalar(randomGen),
            Scalar235 = GetScalar(randomGen),
            Scalar145 = GetScalar(randomGen),
            Scalar245 = GetScalar(randomGen),
            Scalar345 = GetScalar(randomGen),
            Scalar126 = GetScalar(randomGen),
            Scalar136 = GetScalar(randomGen),
            Scalar236 = GetScalar(randomGen),
            Scalar146 = GetScalar(randomGen),
            Scalar246 = GetScalar(randomGen),
            Scalar346 = GetScalar(randomGen),
            Scalar156 = GetScalar(randomGen),
            Scalar256 = GetScalar(randomGen),
            Scalar356 = GetScalar(randomGen),
            Scalar456 = GetScalar(randomGen)
        };

        var kVector4 = new Ga51KVector4()
        {
            Scalar1234 = GetScalar(randomGen),
            Scalar1235 = GetScalar(randomGen),
            Scalar1245 = GetScalar(randomGen),
            Scalar1345 = GetScalar(randomGen),
            Scalar2345 = GetScalar(randomGen),
            Scalar1236 = GetScalar(randomGen),
            Scalar1246 = GetScalar(randomGen),
            Scalar1346 = GetScalar(randomGen),
            Scalar2346 = GetScalar(randomGen),
            Scalar1256 = GetScalar(randomGen),
            Scalar1356 = GetScalar(randomGen),
            Scalar2356 = GetScalar(randomGen),
            Scalar1456 = GetScalar(randomGen),
            Scalar2456 = GetScalar(randomGen),
            Scalar3456 = GetScalar(randomGen)
        };

        var kVector5 = new Ga51KVector5()
        {
            Scalar12345 = GetScalar(randomGen),
            Scalar12346 = GetScalar(randomGen),
            Scalar12356 = GetScalar(randomGen),
            Scalar12456 = GetScalar(randomGen),
            Scalar13456 = GetScalar(randomGen),
            Scalar23456 = GetScalar(randomGen)
        };

        var kVector6 = new Ga51KVector6()
        {
            Scalar123456 = GetScalar(randomGen)
        };

        return Ga51Multivector.Create(
            kVector0,
            kVector1,
            kVector2,
            kVector3,
            kVector4,
            kVector5,
            kVector6
        );
    }
        
    private Ga51SparseMultivector ToSparseMultivector(Ga31Multivector mv)
    {
        var scalarArray = mv.GetMultivectorArray();

        var composer = new Ga51SparseMultivector();

        for (var i = 0; i < scalarArray.Length; i++)
            composer[(ulong)i] = scalarArray[i];

        return composer;
    }

    private Ga51SparseMultivector ToSparseMultivector(Ga41Multivector mv)
    {
        var scalarArray = mv.GetMultivectorArray();

        var composer = new Ga51SparseMultivector();

        for (var i = 0; i < scalarArray.Length; i++)
            composer[(ulong)i] = scalarArray[i];

        return composer;
    }

    private Ga51SparseMultivector ToSparseMultivector(Ga51Multivector mv)
    {
        var scalarArray = mv.GetMultivectorArray();

        var composer = new Ga51SparseMultivector();

        for (var i = 0; i < scalarArray.Length; i++)
            composer[(ulong)i] = scalarArray[i];

        return composer;
    }
    
    private XGaFloat64Multivector ToXGaFloat64Multivector(Ga31Multivector mv)
    {
        var scalarArray = mv.GetMultivectorArray();

        var composer = Processor.CreateMultivectorComposer();

        for (var i = 0; i < scalarArray.Length; i++)
            composer.AddTerm((IndexSet)i, scalarArray[i]);

        return composer.GetSimpleMultivector();
    }

    private XGaFloat64Multivector ToXGaFloat64Multivector(Ga41Multivector mv)
    {
        var scalarArray = mv.GetMultivectorArray();

        var composer = Processor.CreateMultivectorComposer();

        for (var i = 0; i < scalarArray.Length; i++)
            composer.AddTerm((IndexSet)i, scalarArray[i]);

        return composer.GetSimpleMultivector();
    }

    private XGaFloat64Multivector ToXGaFloat64Multivector(Ga51Multivector mv)
    {
        var scalarArray = mv.GetMultivectorArray();

        var composer = Processor.CreateMultivectorComposer();

        for (var i = 0; i < scalarArray.Length; i++)
            composer.AddTerm((IndexSet)i, scalarArray[i]);

        return composer.GetSimpleMultivector();
    }


    [GlobalSetup]
    public void Setup()
    {
        var randomGen = new Random(10);

        _multivectorList1 = 10.GetRange(
            _ => GetMultivector31(randomGen)
        ).ToList();
            
        _multivectorList2 = _multivectorList1.Select(
            ToSparseMultivector
        ).ToList();

        _multivectorList3 = _multivectorList1.Select(
            ToXGaFloat64Multivector
        ).ToList();
    }


    [Benchmark(Baseline = true)]
    public void GpGenerated()
    {
        foreach (var mv1 in _multivectorList1)
        foreach (var mv2 in _multivectorList1)
        {
            mv1.Gp(mv2);
        }
    }
        
    [Benchmark()]
    public void GpCoded1()
    {
        foreach (var mv1 in _multivectorList2)
        foreach (var mv2 in _multivectorList2)
        {
            mv1.Gp(mv2);
        }
    }
        
    [Benchmark()]
    public void GpCoded2()
    {
        foreach (var mv1 in _multivectorList3)
        foreach (var mv2 in _multivectorList3)
        {
            mv1.Gp(mv2);
        }
    }
}