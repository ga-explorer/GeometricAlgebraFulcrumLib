using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Structures;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64Bivector :
    XGaFloat64KVector
{
    private readonly IReadOnlyDictionary<IndexSet, double> _idScalarDictionary;


    public override string MultivectorClassName
        => "Generic Bivector";

    public override int Count
        => _idScalarDictionary.Count;

    public override int Grade
        => 2;

    public override bool IsZero
        => _idScalarDictionary.Count == 0;

    public override IEnumerable<int> KVectorGrades
    {
        get
        {
            if (!IsZero) yield return 2;
        }
    }

    public override IEnumerable<IndexSet> Ids
        => _idScalarDictionary.Keys;

    public override IEnumerable<double> Scalars
    {
        
        get => _idScalarDictionary.Values;
    }

    public override IEnumerable<KeyValuePair<IndexSet, double>> IdScalarPairs
        => _idScalarDictionary;

    public override IEnumerable<XGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Metric.BasisBlade);


    
    internal XGaFloat64Bivector(XGaFloat64Processor processor)
        : base(processor)
    {
        _idScalarDictionary = new EmptyDictionary<IndexSet, double>();
    }

    
    internal XGaFloat64Bivector(XGaFloat64Processor processor, KeyValuePair<IndexSet, double> basisScalarPair)
        : base(processor)
    {
        _idScalarDictionary =
            new SingleItemDictionary<IndexSet, double>(basisScalarPair);

        Debug.Assert(IsValid());
    }

    
    internal XGaFloat64Bivector(XGaFloat64Processor processor, IReadOnlyDictionary<IndexSet, double> scalarDictionary)
        : base(processor)
    {
        _idScalarDictionary = scalarDictionary;

        Debug.Assert(IsValid());
    }


    
    public override bool IsValid()
    {
        return _idScalarDictionary.IsValidBivectorDictionary();
    }


    
    public Pair<XGaFloat64Vector> GetVectorBasis()
    {
        var closestVector = Processor.VectorTerm(0);

        return GetVectorBasis(closestVector);
    }

    
    public Pair<XGaFloat64Vector> GetVectorBasis(int closestBasisVectorIndex)
    {
        var closestVector = Processor.VectorTerm(closestBasisVectorIndex);

        return GetVectorBasis(closestVector);
    }

    
    public Pair<XGaFloat64Vector> GetVectorBasis(XGaFloat64Vector closestVector)
    {
        var e1 = closestVector.Lcp(this).DivideByNorm();
        var e2 = e1.Lcp(this);

        Debug.Assert((e1.Op(e2) - this).IsNearZero());

        return new Pair<XGaFloat64Vector>(e1, e2);
    }


    
    public override IReadOnlyDictionary<IndexSet, double> GetIdScalarDictionary()
    {
        return _idScalarDictionary;
    }

    public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
    {
        get
        {
            return _idScalarDictionary.Select(p =>
                new KeyValuePair<XGaBasisBlade, double>(Metric.BasisBlade(p.Key), p.Value)
            );
        }
    }


    
    public override bool ContainsKey(IndexSet key)
    {
        return !IsZero && _idScalarDictionary.ContainsKey(key);
    }


    
    public override XGaFloat64Scalar GetScalarPart()
    {
        return Processor.ScalarZero;
    }

    
    public override XGaFloat64Vector GetVectorPart()
    {
        return Processor.VectorZero;
    }

    
    public override XGaFloat64Vector GetVectorPart(Func<int, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    
    public override XGaFloat64Vector GetVectorPart(Func<double, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    
    public override XGaFloat64Vector GetVectorPart(Func<int, double, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    
    public override XGaFloat64Bivector GetBivectorPart()
    {
        return this;
    }

    
    public override XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        return Processor.HigherKVectorZero(grade);
    }

    
    public XGaFloat64Bivector GetBivectorPart(Func<int, int, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary =
            _idScalarDictionary.Where(term =>
                filterFunc(term.Key.FirstIndex, term.Key.LastIndex)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }


    
    public new XGaFloat64Bivector GetPart(Func<IndexSet, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }

    
    public new XGaFloat64Bivector GetPart(Func<double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }

    
    public new XGaFloat64Bivector GetPart(Func<IndexSet, double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }


    
    public override double Scalar()
    {
        return 0d;
    }

    
    public override double GetBasisBladeScalar(IndexSet basisBlade)
    {
        return _idScalarDictionary.GetValueOrDefault(basisBlade, 0d);
    }


    
    public override bool TryGetScalarValue(out double scalar)
    {
        scalar = 0d;
        return false;
    }

    
    public override bool TryGetBasisBladeScalarValue(IndexSet basisBlade, out double scalar)
    {
        if (_idScalarDictionary.TryGetValue(basisBlade, out scalar))
            return true;

        scalar = 0d;
        return false;
    }


    
    public override string ToString()
    {
        if (IsZero) return "0";

        return BasisScalarPairs
            .OrderBy(p => p.Key.Id)
            .Select(p => $"{p.Value:G} {p.Key}")
            .ConcatenateText(" + ");
    }
}