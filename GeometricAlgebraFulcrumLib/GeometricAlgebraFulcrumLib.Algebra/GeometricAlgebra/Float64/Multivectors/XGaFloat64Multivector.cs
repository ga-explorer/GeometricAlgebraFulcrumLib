using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Collections;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

/// <summary>
/// A sparse Euclidean multivector or arbitrary dimensions with double
/// precision scalars
/// </summary>
public abstract partial class XGaFloat64Multivector :
    IReadOnlyCollection<KeyValuePair<IndexSet, double>>,
    IXGaFloat64Element
{
    public abstract string MultivectorClassName { get; }

    public XGaFloat64Processor Processor { get; }

    public XGaMetric Metric
        => Processor;

    public bool IsEuclidean
        => Metric.IsEuclidean;

    public bool IsNonEuclidean
        => Metric.IsNonEuclidean;

    /// <summary>
    /// The dimensions of the base vector space, dynamically determined from
    /// stored terms
    /// </summary>
    public int VSpaceDimensions
        => IsZero ? 0 : Ids.Max(id => id.VSpaceDimensions());

    /// <summary>
    /// The number of stored terms in this multivector
    /// </summary>
    public abstract int Count { get; }

    /// <summary>
    /// True if this is a zero multivector
    /// </summary>
    public abstract bool IsZero { get; }

    /// <summary>
    /// Get basis blades of all stored terms
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<XGaBasisBlade> BasisBlades { get; }

    /// <summary>
    /// Get basis blade IDs of all stored terms
    /// </summary>
    public abstract IEnumerable<IndexSet> Ids { get; }

    /// <summary>
    /// Get scalars of all stored terms
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<double> Scalars { get; }

    /// <summary>
    /// Get all stored terms as pairs of (BasisBlade ID, Scalar)
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<KeyValuePair<IndexSet, double>> IdScalarPairs { get; }

    /// <summary>
    /// Get all stored terms as tuples of (BasisBlade ID, Scalar)
    /// </summary>
    /// <value></value>
    public IEnumerable<Tuple<IndexSet, double>> IdScalarTuples
        => IdScalarPairs.Select(p =>
            new Tuple<IndexSet, double>(p.Key, p.Value)
        );

    /// <summary>
    /// Get all stored terms as pairs of (BasisBlade, Scalar)
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs { get; }

    /// <summary>
    /// The stored k-vector grades in this multivector
    /// </summary>
    public abstract IEnumerable<int> KVectorGrades { get; }

    public double this[int index]
        => Scalar(index);

    public double this[int index1, int index2]
        => Scalar(index1, index2);

    public double this[int index1, int index2, int index3]
        => Scalar(index1, index2, index3);

    public double this[params int[] indexList]
        => Scalar(indexList);

    public double this[IReadOnlyList<int> indexList]
        => Scalar(indexList);

    public double this[IXGaSignedBasisBlade basisBlade]
        => Scalar(basisBlade);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaFloat64Multivector(XGaFloat64Processor processor)
    {
        Processor = processor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return IsZero ||
               Scalars.All(s => s.IsNearZero(zeroEpsilon));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasSameMetric(XGaFloat64Multivector mv)
    {
        return Processor.HasSameSignature(mv.Processor);
    }

    public abstract bool IsValid();
    
    public abstract bool IsScalar();

    public abstract bool IsVector();

    public abstract bool IsBivector();

    public abstract bool IsTrivector();

    public abstract bool IsKVector(int grade);

    public abstract bool IsOdd();

    public abstract bool IsOdd(int maxGrade);

    public abstract bool IsEven();

    public abstract bool IsEven(int maxGrade);


    public abstract int GetMinGrade();

    public abstract int GetMaxGrade();

    public abstract bool ContainsKey(IndexSet key);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(IndexSet key, out double value)
    {
        return TryGetBasisBladeScalarValue(key, out value);
    }

    /// <summary>
    /// The number of stored k-vectors in this multivector
    /// </summary>
    public abstract int GetKVectorCount();


    /// <summary>
    /// Get the scalar coefficient associated with the basis scalar term
    /// </summary>
    /// <returns></returns>
    public abstract double Scalar();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Scalar(int index)
    {
        return Scalar(
            Metric.BasisVector(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Scalar(int index1, int index2)
    {
        return Scalar(
            Metric.Op(index1, index2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Scalar(int index1, int index2, int index3)
    {
        return Scalar(
            Metric.Op(index1, index2, index3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Scalar(params int[] indexList)
    {
        return Scalar(
            Metric.Op(indexList)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Scalar(IReadOnlyList<int> indexList)
    {
        return Scalar(
            Metric.Op(indexList)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Scalar(IXGaSignedBasisBlade basisBlade)
    {
        return basisBlade.IsZero
            ? 0d
            : basisBlade.IsPositive
                ? GetBasisBladeScalar(basisBlade.Id)
                : -GetBasisBladeScalar(basisBlade.Id);
    }


    public abstract bool TryGetBasisBladeScalarValue(IndexSet basisBlade, out double scalar);

    public abstract bool TryGetScalarValue(out double scalar);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out double scalar, int index)
    {
        return TryGetScalarValue(
            out scalar,
            Metric.BasisVector(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out double scalar, int index1, int index2)
    {
        return TryGetScalarValue(
            out scalar,
            Metric.Op(index1, index2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out double scalar, int index1, int index2, int index3)
    {
        return TryGetScalarValue(
            out scalar,
            Metric.Op(index1, index2, index3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out double scalar, params int[] indexList)
    {
        return TryGetScalarValue(
            out scalar,
            Metric.Op(indexList)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out double scalar, IReadOnlyList<int> indexList)
    {
        return TryGetScalarValue(
            out scalar,
            Metric.Op(indexList)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out double scalar, IXGaSignedBasisBlade basisBlade)
    {
        if (!basisBlade.IsZero && TryGetBasisBladeScalarValue(basisBlade.Id, out scalar))
        {
            if (basisBlade.IsNegative)
                scalar = -scalar;

            return true;
        }

        scalar = 0d;
        return false;
    }


    /// <summary>
    /// Get all stored terms as (Id, Scalar) records for constructing a column
    /// vector array for this multivector
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, double>> GetMultivectorArrayItems()
    {
        return IdScalarPairs.Select(
            term =>
                new KeyValuePair<ulong, double>(
                    term.Key.ToUInt64(),
                    term.Value
                )
        );
    }


    /// <summary>
    /// Get the scalar coefficient associated with a basis blade term
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    public abstract double GetBasisBladeScalar(IndexSet basisBladeId);
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet GetStoredBasisVectorIndices()
    {
        return IndexSet.Create(
            Ids.SelectMany(id => id), 
            false
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet GetStoredKVectorGrades()
    {
        return IndexSet.Create(KVectorGrades, false);
    }


    public abstract bool ContainsScalarPart();

    public abstract bool ContainsVectorPart();

    public abstract bool ContainsBivectorPart();

    public abstract bool ContainsKVectorPart(int grade);

    public abstract bool ContainsOddPart();

    public abstract bool ContainsOddPart(int maxGrade);

    public abstract bool ContainsEvenPart();

    public abstract bool ContainsEvenPart(int maxGrade);


    public abstract XGaFloat64Scalar GetScalarPart();

    public abstract XGaFloat64Vector GetVectorPart();

    public abstract XGaFloat64Vector GetVectorPart(Func<int, bool> filterFunc);

    public abstract XGaFloat64Vector GetVectorPart(Func<double, bool> filterFunc);

    public abstract XGaFloat64Vector GetVectorPart(Func<int, double, bool> filterFunc);

    public abstract XGaFloat64Bivector GetBivectorPart();

    public abstract XGaFloat64HigherKVector GetHigherKVectorPart(int grade);

    public abstract XGaFloat64KVector GetKVectorPart(int grade);

    public abstract XGaFloat64KVector GetFirstKVectorPart();

    public abstract XGaFloat64Multivector GetEvenPart();

    public abstract XGaFloat64Multivector GetEvenPart(int maxGrade);

    public abstract XGaFloat64Multivector GetOddPart();

    public abstract XGaFloat64Multivector GetOddPart(int maxGrade);

    public abstract IEnumerable<XGaFloat64KVector> GetKVectorParts();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaFloat64Multivector GetPart(Func<IndexSet, bool> filterFunc);
    //{
    //    return this switch
    //    {
    //        XGaFloat64Scalar s => s.GetPart(filterFunc),
    //        XGaFloat64Vector v => v.GetPart(filterFunc),
    //        XGaFloat64Bivector bv => bv.GetPart(filterFunc),
    //        XGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
    //        XGaFloat64GradedMultivector mv1 => mv1.GetPart(filterFunc),
    //        XGaFloat64UniformMultivector mv1 => mv1.GetPart(filterFunc),
    //        _ => throw new InvalidOperationException()
    //    };
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaFloat64Multivector GetPart(Func<double, bool> filterFunc);
    //{
    //    return this switch
    //    {
    //        XGaFloat64Scalar s => s.GetPart(filterFunc),
    //        XGaFloat64Vector v => v.GetPart(filterFunc),
    //        XGaFloat64Bivector bv => bv.GetPart(filterFunc),
    //        XGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
    //        XGaFloat64GradedMultivector mv1 => mv1.GetPart(filterFunc),
    //        XGaFloat64UniformMultivector mv1 => mv1.GetPart(filterFunc),
    //        _ => throw new InvalidOperationException()
    //    };
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaFloat64Multivector GetPart(Func<IndexSet, double, bool> filterFunc);
    //{
    //    return this switch
    //    {
    //        XGaFloat64Scalar s => s.GetPart(filterFunc),
    //        XGaFloat64Vector v => v.GetPart(filterFunc),
    //        XGaFloat64Bivector bv => bv.GetPart(filterFunc),
    //        XGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
    //        XGaFloat64GradedMultivector mv1 => mv1.GetPart(filterFunc),
    //        XGaFloat64UniformMultivector mv1 => mv1.GetPart(filterFunc),
    //        _ => throw new InvalidOperationException()
    //    };
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<XGaFloat64Scalar, XGaFloat64Bivector> GetScalarBivectorParts()
    {
        return new Tuple<XGaFloat64Scalar, XGaFloat64Bivector>(
            GetScalarPart(),
            GetBivectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<XGaFloat64Multivector, XGaFloat64Multivector> GetEvenOddParts()
    {
        return new Tuple<XGaFloat64Multivector, XGaFloat64Multivector>(
            GetEvenPart(),
            GetOddPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<XGaFloat64Multivector, XGaFloat64Multivector> GetEvenOddParts(int maxGrade)
    {
        return new Tuple<XGaFloat64Multivector, XGaFloat64Multivector>(
            GetEvenPart(maxGrade),
            GetOddPart(maxGrade)
        );
    }


    public Tuple<IndexSet, double> GetMinScalarMagnitudeIdScalar()
    {
        if (IsZero)
            throw new InvalidOperationException();

        var (minValueId, minValue) = IdScalarPairs.First();
        minValue = minValue.Abs();

        foreach (var (id, scalar) in IdScalarPairs)
        {
            var absNumber = scalar.Abs();

            if (absNumber >= minValue) continue;

            minValue = absNumber;
            minValueId = id;
        }

        return new Tuple<IndexSet, double>(minValueId, minValue);
    }

    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<IndexSet, double>> GetEnumerator()
    {
        return IdScalarPairs.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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