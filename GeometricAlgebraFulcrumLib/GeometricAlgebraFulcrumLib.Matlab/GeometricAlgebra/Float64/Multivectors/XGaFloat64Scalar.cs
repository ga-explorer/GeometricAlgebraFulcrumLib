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

public sealed partial class XGaFloat64Scalar :
    XGaFloat64KVector,
    IFloat64Scalar
{
    private readonly double _scalar;

    public double ScalarValue
        => _scalar;

    public override string MultivectorClassName
        => "Generic Scalar";

    public override int Count
        => IsZero ? 0 : 1;

    public override int Grade
        => 0;

    public override bool IsZero { get; }

    public bool IsOne
        => ScalarValue.IsOne();

    public bool IsMinusOne
        => ScalarValue.IsMinusOne();


    public override IEnumerable<int> KVectorGrades
    {
        get
        {
            if (!IsZero) yield return 0;
        }
    }

    public override IEnumerable<XGaBasisBlade> BasisBlades
    {
        
        get
        {
            if (!IsZero) yield return Processor.UnitBasisScalar;
        }
    }

    public override IEnumerable<IndexSet> Ids
    {
        get
        {
            if (!IsZero) yield return IndexSet.EmptySet;
        }
    }

    public override IEnumerable<double> Scalars
    {
        get
        {
            if (!IsZero) yield return ScalarValue;
        }
    }

    public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
    {
        
        get
        {
            if (!IsZero)
                yield return new KeyValuePair<XGaBasisBlade, double>(
                    Metric.UnitBasisScalar,
                    ScalarValue
                );
        }
    }

    public override IEnumerable<KeyValuePair<IndexSet, double>> IdScalarPairs
    {
        get
        {
            if (!IsZero)
                yield return new KeyValuePair<IndexSet, double>(
                    IndexSet.EmptySet,
                    ScalarValue
                );
        }
    }


    
    internal XGaFloat64Scalar(XGaFloat64Processor metric, double scalar)
        : base(metric)
    {
        Debug.Assert(
            scalar.IsValid()
        );

        _scalar = scalar;
        IsZero = scalar.IsZero();
    }

    
    internal XGaFloat64Scalar(XGaFloat64Processor metric, IFloat64Scalar scalar)
        : base(metric)
    {
        Debug.Assert(
            scalar.IsValid()
        );

        _scalar = scalar.ToScalar();
        IsZero = scalar.IsZero();
    }

    
    internal XGaFloat64Scalar(XGaFloat64Processor metric)
        : base(metric)
    {
        _scalar = 0d;
        IsZero = true;
    }

    
    internal XGaFloat64Scalar(XGaFloat64Processor metric, IReadOnlyDictionary<IndexSet, double> idScalarDictionary)
        : base(metric)
    {
        _scalar = idScalarDictionary.GetValueOrDefault(IndexSet.EmptySet, 0d);

        Debug.Assert(
            _scalar.IsValid()
        );

        IsZero = _scalar.IsZero();
    }


    
    public override bool IsValid()
    {
        return ScalarValue.IsValid();
    }


    
    public override IReadOnlyDictionary<IndexSet, double> GetIdScalarDictionary()
    {
        return IsZero
            ? new EmptyDictionary<IndexSet, double>()
            : new SingleItemDictionary<IndexSet, double>(IndexSet.EmptySet, _scalar);
    }

    
    public override bool ContainsKey(IndexSet key)
    {
        return key.IsEmptySet && !IsZero;
    }


    
    public override XGaFloat64Scalar GetScalarPart()
    {
        return this;
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
        return Processor.HigherKVectorZero(grade);
    }

    
    public new XGaFloat64Scalar GetPart(Func<IndexSet, bool> filterFunc)
    {
        return IsZero || filterFunc(IndexSet.EmptySet)
            ? this
            : Processor.ScalarZero;
    }

    
    public new XGaFloat64Scalar GetPart(Func<double, bool> filterFunc)
    {
        return IsZero || filterFunc(ScalarValue)
            ? this
            : Processor.ScalarZero;
    }

    
    public new XGaFloat64Scalar GetPart(Func<IndexSet, double, bool> filterFunc)
    {
        return IsZero || filterFunc(IndexSet.EmptySet, ScalarValue)
            ? this
            : Processor.ScalarZero;
    }


    
    public override double Scalar()
    {
        return _scalar;
    }

    
    public override double GetBasisBladeScalar(IndexSet basisBladeId)
    {
        return basisBladeId.IsEmptySet
            ? _scalar
            : 0d;
    }

    
    public override bool TryGetScalarValue(out double scalar)
    {
        if (!IsZero)
        {
            scalar = ScalarValue;
            return true;
        }

        scalar = 0d;
        return false;
    }

    
    public override bool TryGetBasisBladeScalarValue(IndexSet basisBlade, out double scalar)
    {
        if (basisBlade.IsEmptySet)
        {
            scalar = ScalarValue;
            return true;
        }

        scalar = 0d;
        return false;
    }

    
    private bool Equals(XGaFloat64Scalar other)
    {
        return Equals(_scalar, other._scalar);
    }

    
    public override bool Equals(object obj)
    {
        return ReferenceEquals(this, obj) || obj is XGaFloat64Scalar other && Equals(other);
    }

    
    public override int GetHashCode()
    {
        return _scalar.GetHashCode();
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