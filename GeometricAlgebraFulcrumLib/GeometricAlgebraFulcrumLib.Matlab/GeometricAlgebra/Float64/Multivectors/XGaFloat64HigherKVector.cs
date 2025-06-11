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

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64HigherKVector :
    XGaFloat64KVector
{
    private readonly IReadOnlyDictionary<IndexSet, double> _idScalarDictionary;


    public override string MultivectorClassName
        => $"Generic {Grade}-Vector";

    public override int Count
        => _idScalarDictionary.Count;

    public override int Grade { get; }

    public override bool IsZero
        => _idScalarDictionary.Count == 0;

    public override IEnumerable<XGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Metric.BasisBlade);

    public override IEnumerable<IndexSet> Ids
        => _idScalarDictionary.Keys;

    public override IEnumerable<double> Scalars
        => _idScalarDictionary.Values;

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
            if (!IsZero) yield return Grade;
        }
    }
    

    
    internal XGaFloat64HigherKVector(XGaFloat64Processor processor, int grade)
        : base(processor)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        _idScalarDictionary = new EmptyDictionary<IndexSet, double>();

        Grade = grade;
    }

    
    internal XGaFloat64HigherKVector(XGaFloat64Processor processor, KeyValuePair<IndexSet, double> basisScalarPair)
        : base(processor)
    {
        var grade = basisScalarPair.Key.Count;

        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        _idScalarDictionary =
            new SingleItemDictionary<IndexSet, double>(basisScalarPair);

        Grade = grade;

        Debug.Assert(IsValid());
    }

    
    internal XGaFloat64HigherKVector(XGaFloat64Processor processor, int grade, IReadOnlyDictionary<IndexSet, double> indexScalarDictionary)
        : base(processor)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        _idScalarDictionary = indexScalarDictionary;

        Grade = grade;

        Debug.Assert(IsValid());
    }


    
    public override bool IsValid()
    {
        return _idScalarDictionary.IsValidKVectorDictionary(Grade);
    }


    
    public override IReadOnlyDictionary<IndexSet, double> GetIdScalarDictionary()
    {
        return _idScalarDictionary;
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
        return Processor.BivectorZero;
    }

    
    public override XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        return grade == Grade
            ? this
            : Processor.HigherKVectorZero(grade);
    }

    
    public new XGaFloat64HigherKVector GetPart(Func<IndexSet, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor.HigherKVector(Grade, idScalarDictionary);
    }

    
    public new XGaFloat64HigherKVector GetPart(Func<double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor.HigherKVector(Grade, idScalarDictionary);
    }

    
    public new XGaFloat64HigherKVector GetPart(Func<IndexSet, double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor.HigherKVector(Grade, idScalarDictionary);
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
        if (basisBlade.Count == Grade && _idScalarDictionary.TryGetValue(basisBlade, out scalar))
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