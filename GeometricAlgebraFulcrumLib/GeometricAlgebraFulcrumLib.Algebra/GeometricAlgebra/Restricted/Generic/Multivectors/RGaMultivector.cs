using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

/// <summary>
/// A sparse Euclidean multivector or arbitrary dimensions with T
/// precision scalars
/// </summary>
public abstract partial class RGaMultivector<T> :
    IReadOnlyCollection<KeyValuePair<ulong, T>>,
    IRGaElement<T>
{
    public abstract string MultivectorClassName { get; }

    public RGaProcessor<T> Processor { get; }

    public RGaMetric Metric 
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
    public abstract IEnumerable<RGaBasisBlade> BasisBlades { get; }

    /// <summary>
    /// Get basis blade IDs of all stored terms
    /// </summary>
    public abstract IEnumerable<ulong> Ids { get; }

    /// <summary>
    /// Get scalars of all stored terms
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<T> Scalars { get; }

    /// <summary>
    /// Get all stored terms as pairs of (BasisBlade ID, Scalar)
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<KeyValuePair<ulong, T>> IdScalarPairs { get; }

    /// <summary>
    /// Get all stored terms as pairs of (BasisBlade, Scalar)
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<KeyValuePair<RGaBasisBlade, T>> BasisScalarPairs { get; }
        
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

    public Scalar<T> this[IRGaSignedBasisBlade basisBlade]
        => Scalar(basisBlade);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected RGaMultivector(RGaProcessor<T> processor)
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
    public bool HasSameMetric(RGaMultivector<T> mv)
    {
        return Processor.HasSameSignature(mv.Metric);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetStoredGradesBitPattern()
    {
        return KVectorGrades.Aggregate(
            0UL, 
            (a, g) => a | (1UL << g)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> AsScalar()
    {
        return (RGaScalar<T>)this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> AsVector()
    {
        return (RGaVector<T>)this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<T> AsBivector()
    {
        return (RGaBivector<T>)this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaHigherKVector<T> AsHigherKVector()
    {
        return (RGaHigherKVector<T>)this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> AsKVector()
    {
        return (RGaKVector<T>)this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaGradedMultivector<T> AsGradedMultivector()
    {
        return (RGaGradedMultivector<T>)this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> AsUniformMultivector()
    {
        return (RGaUniformMultivector<T>)this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> MultivectorToLinVector()
    {
        var indexScalarDictionary =
            IdScalarPairs.ToDictionary(
                p => (int) p.Key.BasisBladeIdToIndex(),
                p => p.Value
            );

        return ScalarProcessor.CreateLinVector(indexScalarDictionary);
    }
        
    public abstract int GetMinGrade();

    public abstract int GetMaxGrade();

    public abstract bool ContainsKey(ulong key);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(ulong key, out T value)
    {
        return TryGetBasisBladeScalarValue(key, out value);
    }

    /// <summary>
    /// The number of stored k-vectors in this multivector
    /// </summary>
    public abstract int GetKVectorCount();

    public abstract RGaScalar<T> GetScalarPart();

    public abstract RGaVector<T> GetVectorPart();

    public abstract RGaBivector<T> GetBivectorPart();

    public abstract RGaHigherKVector<T> GetHigherKVectorPart(int grade);
        
    public abstract RGaKVector<T> GetKVectorPart(int grade);
        
    public abstract RGaKVector<T> GetFirstKVectorPart();

    public abstract RGaMultivector<T> GetEvenPart();

    public abstract RGaMultivector<T> GetEvenPart(int maxGrade);

    public abstract RGaMultivector<T> GetOddPart();

    public abstract RGaMultivector<T> GetOddPart(int maxGrade);

    public abstract IEnumerable<RGaKVector<T>> GetKVectorParts();


    /// <summary>
    /// Get the scalar coefficient associated with a basis blade term
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    public abstract Scalar<T> GetBasisBladeScalar(ulong basisBladeId);
        
    /// <summary>
    /// Get the scalar coefficient associated with the basis scalar term
    /// </summary>
    /// <returns></returns>
    public abstract Scalar<T> Scalar();
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(int index)
    {
        return Scalar(
            Metric.CreateBasisVector(index)
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
    public Scalar<T> Scalar(IRGaSignedBasisBlade basisBlade)
    {
        return basisBlade.IsZero
            ? ScalarProcessor.Zero
            : basisBlade.IsPositive
                ? GetBasisBladeScalar(basisBlade.Id)
                : -GetBasisBladeScalar(basisBlade.Id);
    }


    public abstract bool TryGetBasisBladeScalarValue(ulong basisBlade, out T scalar);
        
    public abstract bool TryGetScalarValue(out T scalar);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, int index)
    {
        return TryGetScalarValue(
            out scalar, 
            Metric.CreateBasisVector(index)
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
    public bool TryGetScalarValue(out T scalar, IRGaSignedBasisBlade basisBlade)
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
                    term.Key, 
                    term.Value
                )
        );
    }

    /// <summary>
    /// Simplify the storage of this multivector
    /// </summary>
    /// <returns></returns>
    public abstract RGaMultivector<T> Simplify();
        
        
    /// <summary>
    /// Construct a binary tree representation of this storage
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaGbtBinaryTree<T> GetBinaryTree(int treeDepth)
    {
        if (treeDepth < VSpaceDimensions)
            throw new InvalidOperationException();

        var dict = 
            IdScalarPairs.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

        return new RGaGbtBinaryTree<T>(treeDepth, dict);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity)
    {
        //return RGaGbtKVectorStorageStack1<T>.Create(capacity, treeDepth, this);
        //return RGaGbtMultivectorStorageGradedStack1<T>.Create(capacity, treeDepth, this);
        return RGaMultivectorGbtUniformStack1<T>.Create(
            capacity, 
            treeDepth,
            this
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
    {
        return IdScalarPairs.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}