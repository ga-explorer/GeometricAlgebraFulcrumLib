using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Structures;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64GradedMultivector :
    XGaFloat64Multivector
{
    private readonly IReadOnlyDictionary<int, XGaFloat64KVector> _gradeKVectorDictionary;


    public override string MultivectorClassName
        => "Generic Graded Multivector";

    public override int Count
        => _gradeKVectorDictionary.Count == 0
            ? 0
            : _gradeKVectorDictionary.Values.Sum(kv => kv.Count);

    public override IEnumerable<int> KVectorGrades
        => _gradeKVectorDictionary.Keys;

    public int KVectorCount
        => _gradeKVectorDictionary.Count;

    public override bool IsZero
        => _gradeKVectorDictionary.Count == 0;

    public IEnumerable<XGaFloat64KVector> KVectors
        => _gradeKVectorDictionary.Values;

    public override IEnumerable<KeyValuePair<IndexSet, double>> IdScalarPairs
        => _gradeKVectorDictionary.Values.SelectMany(
            kv => kv.IdScalarPairs
        );

    public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
        => _gradeKVectorDictionary.Values.SelectMany(
            kv => kv.BasisScalarPairs
        );
    
    public override IEnumerable<XGaBasisBlade> BasisBlades
    {
        
        get
        {
            return _gradeKVectorDictionary
                .Values
                .SelectMany(kv => kv.BasisBlades);
        }
    }

    public override IEnumerable<IndexSet> Ids
        => _gradeKVectorDictionary.Values.SelectMany(kv => kv.Ids);

    public override IEnumerable<double> Scalars
        => _gradeKVectorDictionary
            .Values
            .SelectMany(kv => kv.Scalars);


    
    internal XGaFloat64GradedMultivector(XGaFloat64Processor processor)
        : base(processor)
    {
        _gradeKVectorDictionary = new EmptyDictionary<int, XGaFloat64KVector>();
    }

    
    internal XGaFloat64GradedMultivector(XGaFloat64Processor processor, KeyValuePair<int, XGaFloat64KVector> gradeKVectorPair)
        : base(processor)
    {
        _gradeKVectorDictionary =
            new SingleItemDictionary<int, XGaFloat64KVector>(gradeKVectorPair);

        Debug.Assert(
            Metric.IsValidMultivectorDictionary(_gradeKVectorDictionary)
        );
    }

    
    internal XGaFloat64GradedMultivector(XGaFloat64Processor processor, IReadOnlyDictionary<int, XGaFloat64KVector> kVectorDictionary)
        : base(processor)
    {
        _gradeKVectorDictionary = kVectorDictionary;

        Debug.Assert(
            Metric.IsValidMultivectorDictionary(_gradeKVectorDictionary)
        );
    }


    
    public override bool IsScalar()
    {
        return IsZero || _gradeKVectorDictionary.Keys.All(g => g == 0);
    }

    
    public override bool IsVector()
    {
        return IsZero || _gradeKVectorDictionary.Keys.All(g => g == 1);
    }

    
    public override bool IsBivector()
    {
        return IsZero || _gradeKVectorDictionary.Keys.All(g => g == 2);
    }
    
    
    public override bool IsTrivector()
    {
        return IsZero || _gradeKVectorDictionary.Keys.All(g => g == 3);
    }

    
    public override bool IsKVector(int grade)
    {
        return IsZero || _gradeKVectorDictionary.Keys.All(g => g == grade);
    }

    
    public override bool IsOdd()
    {
        return !IsZero && _gradeKVectorDictionary.Keys.All(k => k.IsOdd());
    }

    
    public override bool IsOdd(int maxGrade)
    {
        return !IsZero && _gradeKVectorDictionary.Keys.All(k => k.IsOdd(maxGrade));
    }

    
    public override bool IsEven()
    {
        return !IsZero && _gradeKVectorDictionary.Keys.All(k => k.IsEven());
    }

    
    public override bool IsEven(int maxGrade)
    {
        return !IsZero && _gradeKVectorDictionary.Keys.All(k => k.IsEven(maxGrade));
    }

        
    
    public override int GetMinGrade()
    {
        return IsZero ? 0 : _gradeKVectorDictionary.Keys.Min();
    }

    
    public override int GetMaxGrade()
    {
        return IsZero ? 0 : _gradeKVectorDictionary.Keys.Max();
    }

    
    public override bool ContainsKey(IndexSet id)
    {
        if (IsZero) return false;

        var grade = id.Count;

        return _gradeKVectorDictionary.TryGetValue(grade, out var kVector) &&
               kVector.ContainsKey(id);
    }

    
    public override bool ContainsScalarPart()
    {
        return _gradeKVectorDictionary.ContainsKey(0);
    }

    
    public override bool ContainsVectorPart()
    {
        return _gradeKVectorDictionary.ContainsKey(1);
    }

    
    public override bool ContainsBivectorPart()
    {
        return _gradeKVectorDictionary.ContainsKey(2);
    }

    
    public override bool ContainsKVectorPart(int grade)
    {
        return _gradeKVectorDictionary.ContainsKey(grade);
    }

    
    public override bool ContainsOddPart()
    {
        return !IsZero && _gradeKVectorDictionary.Keys.Any(k => k.IsOdd());
    }

    
    public override bool ContainsOddPart(int maxGrade)
    {
        return !IsZero && _gradeKVectorDictionary.Keys.Any(k => k.IsOdd(maxGrade));
    }

    
    public override bool ContainsEvenPart()
    {
        return !IsZero && _gradeKVectorDictionary.Keys.Any(k => k.IsEven());
    }

    
    public override bool ContainsEvenPart(int maxGrade)
    {
        return !IsZero && _gradeKVectorDictionary.Keys.Any(k => k.IsEven(maxGrade));
    }

    public override int GetKVectorCount()
    {
        return _gradeKVectorDictionary.Count;
    }


    
    public override bool IsValid()
    {
        return Processor.IsValidMultivectorDictionary(_gradeKVectorDictionary);
    }

    public override IEnumerable<XGaFloat64KVector> GetKVectorParts()
    {
        return _gradeKVectorDictionary.Values;
    }

    
    public override double Scalar()
    {
        if (_gradeKVectorDictionary.TryGetValue(0, out var kVector))
            return ((XGaFloat64Scalar)kVector).ScalarValue;

        return 0d;
    }

    public override double GetBasisBladeScalar(IndexSet basisBladeId)
    {
        var grade = basisBladeId.Count;

        if (grade == 0)
            return Scalar();

        return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
            ? kVector.GetBasisBladeScalar(basisBladeId)
            : 0d;
    }


    
    public override bool TryGetScalarValue(out double scalar)
    {
        if (_gradeKVectorDictionary.TryGetValue(0, out var kVector))
            return kVector.TryGetScalarValue(out scalar);

        scalar = 0d;
        return false;
    }

    
    public override bool TryGetBasisBladeScalarValue(IndexSet basisBlade, out double scalar)
    {
        if (_gradeKVectorDictionary.TryGetValue(basisBlade.Count, out var kVector))
            return kVector.TryGetBasisBladeScalarValue(basisBlade, out scalar);

        scalar = 0d;
        return false;
    }

    
    public bool TryGetKVector(int grade, out XGaFloat64KVector kVector)
    {
        return _gradeKVectorDictionary.TryGetValue(grade, out kVector);
    }


    
    public override XGaFloat64Scalar GetScalarPart()
    {
        if (_gradeKVectorDictionary.TryGetValue(0, out var kVector))
            return (XGaFloat64Scalar)kVector;

        return Processor.ScalarZero;
    }

    
    public override XGaFloat64Vector GetVectorPart()
    {
        if (_gradeKVectorDictionary.TryGetValue(1, out var kVector))
            return (XGaFloat64Vector)kVector;

        return Processor.VectorZero;
    }
    
    
    public override XGaFloat64Vector GetVectorPart(Func<int, bool> filterFunc)
    {
        if (_gradeKVectorDictionary.TryGetValue(1, out var kVector))
            return ((XGaFloat64Vector)kVector).GetVectorPart(filterFunc);

        return Processor.VectorZero;
    }

    
    public override XGaFloat64Vector GetVectorPart(Func<double, bool> filterFunc)
    {
        if (_gradeKVectorDictionary.TryGetValue(1, out var kVector))
            return ((XGaFloat64Vector)kVector).GetVectorPart(filterFunc);

        return Processor.VectorZero;
    }

    
    public override XGaFloat64Vector GetVectorPart(Func<int, double, bool> filterFunc)
    {
        if (_gradeKVectorDictionary.TryGetValue(1, out var kVector))
            return ((XGaFloat64Vector)kVector).GetVectorPart(filterFunc);

        return Processor.VectorZero;
    }

    
    public override XGaFloat64Bivector GetBivectorPart()
    {
        if (_gradeKVectorDictionary.TryGetValue(2, out var kVector))
            return (XGaFloat64Bivector)kVector;

        return Processor.BivectorZero;
    }

    
    public override XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
            ? (XGaFloat64HigherKVector)kVector
            : Processor.HigherKVectorZero(grade);
    }

    
    public override XGaFloat64KVector GetKVectorPart(int grade)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
            ? kVector
            : Processor.KVectorZero(grade);
    }
        
    
    public override XGaFloat64KVector GetFirstKVectorPart()
    {
        return _gradeKVectorDictionary.Count > 0
            ? _gradeKVectorDictionary.Values.First()
            : Processor.ScalarZero;
    }

    
    public new XGaFloat64GradedMultivector GetPart(Func<IndexSet, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarPairs = 
            IdScalarPairs.Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor
            .CreateMultivectorComposer()
            .SetTerms(idScalarPairs)
            .GetGradedMultivector();
    }
        
    
    public new XGaFloat64GradedMultivector GetPart(Func<double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarPairs = 
            IdScalarPairs.Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor
            .CreateMultivectorComposer()
            .SetTerms(idScalarPairs)
            .GetGradedMultivector();
    }

    
    public new XGaFloat64GradedMultivector GetPart(Func<IndexSet, double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarPairs = 
            IdScalarPairs.Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor
            .CreateMultivectorComposer()
            .SetTerms(idScalarPairs)
            .GetGradedMultivector();
    }
        
    public override XGaFloat64Multivector GetEvenPart()
    {
        if (IsZero)
            return Processor.ScalarZero;

        var composer = Processor.CreateMultivectorComposer();

        if (_gradeKVectorDictionary.TryGetValue(0, out var scalarPart))
            composer.SetScalarTerm((XGaFloat64Scalar)scalarPart);

        if (_gradeKVectorDictionary.TryGetValue(2, out var bivectorPart))
            composer.SetKVector(bivectorPart);

        var kVectors =
            _gradeKVectorDictionary.Values.Where(
                kv => kv.Grade > 3 && kv.Grade.IsEven()
            );

        composer.SetKVectors(kVectors);

        return composer.GetSimpleMultivector();
    }

    public override XGaFloat64Multivector GetEvenPart(int maxGrade)
    {
        if (maxGrade < 0 || IsZero)
            return Processor.ScalarZero;

        var composer = Processor.CreateMultivectorComposer();

        if (_gradeKVectorDictionary.TryGetValue(0, out var scalarPart))
            composer.SetScalarTerm((XGaFloat64Scalar)scalarPart);

        if (maxGrade < 2)
            return composer.GetSimpleMultivector();

        if (_gradeKVectorDictionary.TryGetValue(2, out var bivectorPart))
            composer.SetKVector(bivectorPart);

        var kVectors =
            _gradeKVectorDictionary.Values.Where(
                kv => kv.Grade > 3 && kv.Grade.IsEven(maxGrade)
            );

        composer.SetKVectors(kVectors);

        return composer.GetSimpleMultivector();
    }

    public override XGaFloat64Multivector GetOddPart()
    {
        if (IsZero)
            return Processor.ScalarZero;

        var composer = Processor.CreateMultivectorComposer();

        if (_gradeKVectorDictionary.TryGetValue(1, out var vectorPart))
            composer.SetKVector(vectorPart);

        var kVectors =
            _gradeKVectorDictionary.Values.Where(
                kv => kv.Grade > 2 && kv.Grade.IsOdd()
            );

        composer.SetKVectors(kVectors);

        return composer.GetSimpleMultivector();
    }

    public override XGaFloat64Multivector GetOddPart(int maxGrade)
    {
        if (maxGrade < 1 || IsZero)
            return Processor.ScalarZero;

        var composer = Processor.CreateMultivectorComposer();

        if (_gradeKVectorDictionary.TryGetValue(1, out var vectorPart))
            composer.SetKVector(vectorPart);

        var kVectors =
            _gradeKVectorDictionary.Values.Where(
                kv => kv.Grade > 2 && kv.Grade.IsOdd(maxGrade)
            );

        composer.SetKVectors(kVectors);

        return composer.GetSimpleMultivector();
    }

    
    
    public override string ToString()
    {
        if (IsZero) return "0";

        return BasisScalarPairs
            .OrderBy(p => p.Key.Id.Count)
            .ThenBy(p => p.Key.Id)
            .Select(p => $"{p.Value:G} {p.Key}")
            .ConcatenateText(" + ");
    }
}