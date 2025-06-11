using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64Vector :
    XGaFloat64KVector
{
    private readonly IReadOnlyDictionary<IndexSet, double> _idScalarDictionary;


    public override string MultivectorClassName
        => "Generic Vector";

    public override int Count
        => _idScalarDictionary.Count;

    public override int Grade
        => 1;

    public override bool IsZero
        => _idScalarDictionary.Count == 0;

    public override IEnumerable<IndexSet> Ids
        => _idScalarDictionary.Keys;

    public override IEnumerable<double> Scalars
    {
        
        get => _idScalarDictionary.Values;
    }

    public IEnumerable<KeyValuePair<int, double>> IndexScalarPairs
        => _idScalarDictionary.Select(p =>
            new KeyValuePair<int, double>(p.Key.FirstIndex, p.Value)
        );

    public override IEnumerable<KeyValuePair<IndexSet, double>> IdScalarPairs
        => _idScalarDictionary;

    public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
    {
        get
        {
            return _idScalarDictionary.Select(p =>
                new KeyValuePair<XGaBasisBlade, double>(
                    Metric.BasisBlade(p.Key),
                    p.Value
                )
            );
        }
    }

    public override IEnumerable<int> KVectorGrades
    {
        get
        {
            if (!IsZero) yield return 1;
        }
    }


    
    internal XGaFloat64Vector(XGaFloat64Processor metric)
        : base(metric)
    {
        _idScalarDictionary = new EmptyDictionary<IndexSet, double>();
    }

    
    internal XGaFloat64Vector(XGaFloat64Processor metric, KeyValuePair<IndexSet, double> basisScalarPair)
        : base(metric)
    {
        _idScalarDictionary =
            new SingleItemDictionary<IndexSet, double>(basisScalarPair);

        Debug.Assert(IsValid());
    }

    
    internal XGaFloat64Vector(XGaFloat64Processor metric, IReadOnlyDictionary<IndexSet, double> idScalarDictionary)
        : base(metric)
    {
        _idScalarDictionary = idScalarDictionary;

        Debug.Assert(IsValid());
    }


    
    public override bool IsValid()
    {
        return _idScalarDictionary.IsValidVectorDictionary();
    }


    
    public override IReadOnlyDictionary<IndexSet, double> GetIdScalarDictionary()
    {
        return _idScalarDictionary;
    }

    
    public override bool ContainsKey(IndexSet key)
    {
        return !IsZero && _idScalarDictionary.ContainsKey(key);
    }

    
    public double GetTermScalarByIndex(int index)
    {
        var id = index.ToUnitIndexSet();

        return GetBasisBladeScalar(id);
    }


    
    public override XGaFloat64Scalar GetScalarPart()
    {
        return Processor.ScalarZero;
    }

    
    public override XGaFloat64Vector GetVectorPart()
    {
        return this;
    }

    
    public override XGaFloat64Vector GetVectorPart(Func<int, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary =
            _idScalarDictionary.Where(term =>
                filterFunc(term.Key.FirstIndex)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }

    
    public override XGaFloat64Vector GetVectorPart(Func<double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary =
            _idScalarDictionary.Where(term =>
                filterFunc(term.Value)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }

    
    public override XGaFloat64Vector GetVectorPart(Func<int, double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary =
            _idScalarDictionary.Where(term =>
                filterFunc(term.Key.FirstIndex, term.Value)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }

    
    public override XGaFloat64Bivector GetBivectorPart()
    {
        return Processor.BivectorZero;
    }

    
    public override XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        return Processor.HigherKVectorZero(grade);
    }


    
    public new XGaFloat64Vector GetPart(Func<IndexSet, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }

    
    public new XGaFloat64Vector GetPart(Func<double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }

    
    public new XGaFloat64Vector GetPart(Func<IndexSet, double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }



    public override IEnumerable<XGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Metric.BasisBlade);

    
    public override double Scalar()
    {
        return 0d;
    }

    
    public override double GetBasisBladeScalar(IndexSet basisBladeId)
    {
        return _idScalarDictionary.GetValueOrDefault(basisBladeId, 0d);
    }


    
    public override bool TryGetScalarValue(out double scalar)
    {
        scalar = 0d;
        return false;
    }

    
    public override bool TryGetBasisBladeScalarValue(IndexSet basisBladeId, out double scalar)
    {
        if (basisBladeId.IsUnitSet && _idScalarDictionary.TryGetValue(basisBladeId, out scalar))
            return true;

        scalar = 0d;
        return false;
    }

    public IXGaSignedBasisBlade GetDominantBasisBlade()
    {
        if (_idScalarDictionary.Count == 0)
            return new XGaBasisBlade(Metric, IndexSet.EmptySet);

        var dominantId = IndexSet.EmptySet;
        var dominantScalar = 0d;

        foreach (var (id, scalar) in _idScalarDictionary.ToTuples())
        {
            var absScalar = scalar.Abs();

            if (absScalar <= dominantScalar)
                continue;

            //if (absScalar == dominantScalar && id.Grade() <= dominantId.Grade()) 
            //    continue;

            dominantId = id;
            dominantScalar = absScalar;
        }

        return new XGaBasisBlade(Metric, dominantId);
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