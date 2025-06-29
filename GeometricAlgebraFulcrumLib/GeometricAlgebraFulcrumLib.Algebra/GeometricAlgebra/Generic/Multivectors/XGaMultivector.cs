﻿using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using System.Collections;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

/// <summary>
/// A sparse Euclidean multivector or arbitrary dimensions with T
/// precision scalars
/// </summary>
public abstract partial class XGaMultivector<T> :
    IReadOnlyCollection<KeyValuePair<IndexSet, T>>,
    IXGaElement<T>
{
    public abstract string MultivectorClassName { get; }

    public XGaProcessor<T> Processor { get; }

    public XGaMetric Metric 
        => Processor; 

    public IScalarProcessor<T> ScalarProcessor 
        => Processor.ScalarProcessor;

    public bool IsEuclidean
        => Processor.IsEuclidean;

    public bool IsNonEuclidean
        => Processor.IsNonEuclidean;

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
    /// The stored k-vector grades in this multivector
    /// </summary>
    public abstract IEnumerable<int> KVectorGrades { get; }

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
    public abstract IEnumerable<T> Scalars { get; }

    /// <summary>
    /// Get all stored terms as pairs of (BasisBlade ID, Scalar)
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs { get; }

    /// <summary>
    /// Get all stored terms as tuples of (BasisBlade ID, Scalar)
    /// </summary>
    /// <value></value>
    public IEnumerable<Tuple<IndexSet, T>> IdScalarTuples 
        => IdScalarPairs.Select(p => 
            new Tuple<IndexSet, T>(p.Key, p.Value)
        );

    /// <summary>
    /// Get all stored terms as pairs of (BasisBlade, Scalar)
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs { get; }
        
    public Scalar<T> this[int index]
        => Scalar(index);

    public Scalar<T> this[int index1, int index2]
        => Scalar(index1, index2);
        
    public Scalar<T> this[int index1, int index2, int index3]
        => Scalar(index1, index2, index3);

    public Scalar<T> this[params int[] indexList]
        => Scalar(indexList);
        
    public Scalar<T> this[IReadOnlyList<int> indexList]
        => Scalar(indexList);

    public Scalar<T> this[IXGaSignedBasisBlade basisBlade]
        => Scalar(basisBlade);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaMultivector(XGaProcessor<T> processor)
    {
        Processor = processor;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero()
    {
        return IsZero ||
               Scalars.All(ScalarProcessor.IsNearZero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasSameMetric(XGaMultivector<T> mv)
    {
        return Processor.HasSameSignature(mv.Processor);
    }

    public abstract bool IsValid();

    public abstract bool IsScalar();

    public abstract bool IsVector();

    public abstract bool IsBivector();

    public abstract bool IsKVector(int grade);

    public abstract bool IsOdd();

    public abstract bool IsOdd(int maxGrade);

    public abstract bool IsEven();

    public abstract bool IsEven(int maxGrade);


    public abstract bool ContainsScalarPart();

    public abstract bool ContainsVectorPart();

    public abstract bool ContainsBivectorPart();

    public abstract bool ContainsKVectorPart(int grade);
    
    public abstract bool ContainsOddPart();

    public abstract bool ContainsOddPart(int maxGrade);
    
    public abstract bool ContainsEvenPart();

    public abstract bool ContainsEvenPart(int maxGrade);


    public abstract int GetMinGrade();

    public abstract int GetMaxGrade();

    public abstract bool ContainsKey(IndexSet key);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(IndexSet key, out T value)
    {
        return TryGetBasisBladeScalarValue(key, out value);
    }

    /// <summary>
    /// The number of stored k-vectors in this multivector
    /// </summary>
    public abstract int GetKVectorCount();

    public abstract XGaScalar<T> GetScalarPart();

    public abstract XGaVector<T> GetVectorPart();
    
    public abstract XGaVector<T> GetVectorPart(Func<int, bool> filter);

    public abstract XGaBivector<T> GetBivectorPart();

    public abstract XGaHigherKVector<T> GetHigherKVectorPart(int grade);

    public abstract XGaKVector<T> GetKVectorPart(int grade);
        
    public abstract XGaKVector<T> GetFirstKVectorPart();

    public abstract XGaMultivector<T> GetEvenPart();

    public abstract XGaMultivector<T> GetEvenPart(int maxGrade);

    public abstract XGaMultivector<T> GetOddPart();

    public abstract XGaMultivector<T> GetOddPart(int maxGrade);

    public abstract IEnumerable<XGaKVector<T>> GetKVectorParts();

    
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


    /// <summary>
    /// Get the scalar coefficient associated with a basis blade term
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    public abstract Scalar<T> GetBasisBladeScalar(IndexSet basisBladeId);
        
    /// <summary>
    /// Get the scalar coefficient associated with the basis scalar term
    /// </summary>
    /// <returns></returns>
    public abstract Scalar<T> Scalar();
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(int index)
    {
        return Scalar(
            Metric.BasisVector(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(int index1, int index2)
    {
        return Scalar(
            Metric.Op(index1, index2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(int index1, int index2, int index3)
    {
        return Scalar(
            Metric.Op(index1, index2, index3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(params int[] indexList)
    {
        return Scalar(
            Metric.Op(indexList)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(IReadOnlyList<int> indexList)
    {
        return Scalar(
            Metric.Op(indexList)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(IXGaSignedBasisBlade basisBlade)
    {
        return basisBlade.IsZero
            ? ScalarProcessor.Zero
            : basisBlade.IsPositive
                ? GetBasisBladeScalar(basisBlade.Id)
                : -GetBasisBladeScalar(basisBlade.Id);
    }


    public abstract bool TryGetBasisBladeScalarValue(IndexSet basisBlade, out T scalar);
        
    public abstract bool TryGetScalarValue(out T scalar);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, int index)
    {
        return TryGetScalarValue(
            out scalar, 
            Metric.BasisVector(index)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, int index1, int index2)
    {
        return TryGetScalarValue(
            out scalar, 
            Metric.Op(index1, index2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, int index1, int index2, int index3)
    {
        return TryGetScalarValue(
            out scalar, 
            Metric.Op(index1, index2, index3)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, params int[] indexList)
    {
        return TryGetScalarValue(
            out scalar, 
            Metric.Op(indexList)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, IReadOnlyList<int> indexList)
    {
        return TryGetScalarValue(
            out scalar, 
            Metric.Op(indexList)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, IXGaSignedBasisBlade basisBlade)
    {
        if (!basisBlade.IsZero && TryGetBasisBladeScalarValue(basisBlade.Id, out scalar))
        {
            if (basisBlade.IsNegative)
                scalar = ScalarProcessor.Negative(scalar).ScalarValue;

            return true;
        }

        scalar = ScalarProcessor.ZeroValue;
        return false;
    }
        

    /// <summary>
    /// Get all stored terms as (Id, Scalar) records for constructing a column
    /// vector array for this multivector
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, T>> GetMultivectorArrayItems()
    {
        return IdScalarPairs.Select(
            term => 
                new KeyValuePair<ulong, T>(
                    term.Key.ToUInt64(), 
                    term.Value
                )
        );
    }

    
    public virtual T[] MultivectorToArray(int vSpaceDimensions)
    {
        if (vSpaceDimensions > 31 || vSpaceDimensions < VSpaceDimensions)
            throw new ArgumentException(nameof(vSpaceDimensions));

        var gaSpaceDimensions =
            1 << vSpaceDimensions;

        var array = ScalarProcessor.CreateArrayZero1D(gaSpaceDimensions);

        foreach (var (index, scalar) in GetMultivectorArrayItems())
            array[index] = scalar;

        return array;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> GetPart(Func<IndexSet, bool> filterFunc);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> GetPart(Func<T, bool> filterFunc);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> GetPart(Func<IndexSet, T, bool> filterFunc);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Tuple<XGaScalar<T>, XGaBivector<T>> GetScalarBivectorParts()
    {
        return new Tuple<XGaScalar<T>, XGaBivector<T>>(
            GetScalarPart(),
            GetBivectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Tuple<XGaMultivector<T>, XGaMultivector<T>> GetEvenOddParts()
    {
        return new Tuple<XGaMultivector<T>, XGaMultivector<T>>(
            GetEvenPart(),
            GetOddPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Tuple<XGaMultivector<T>, XGaMultivector<T>> GetEvenOddParts(int maxGrade)
    {
        return new Tuple<XGaMultivector<T>, XGaMultivector<T>>(
            GetEvenPart(maxGrade),
            GetOddPart(maxGrade)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEuclideanRotor()
    {
        return IsEven() && (EGp(Reverse()) - 1d).IsZero;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetMultivectorText()
    {
        return IdScalarPairs
            .OrderByGradeIndex()
            .GetTermsText();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<IndexSet, T>> GetEnumerator()
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
        if (IsZero)
            return $"'{ScalarProcessor.Zero.ToText()}'<>";

        return BasisScalarPairs
            .OrderBy(p => p.Key.Id.Count)
            .ThenBy(p => p.Key.Id)
            .Select(p => $"'{ScalarProcessor.ToText(p.Value)}'{p.Key}")
            .Concatenate(" + ");
    }
}