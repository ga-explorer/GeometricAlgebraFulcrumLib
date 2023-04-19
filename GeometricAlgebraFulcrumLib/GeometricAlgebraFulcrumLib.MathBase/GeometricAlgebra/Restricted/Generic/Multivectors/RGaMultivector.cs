using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors
{
    /// <summary>
    /// A sparse Euclidean multivector or arbitrary dimensions with T
    /// precision scalars
    /// </summary>
    public abstract partial class RGaMultivector<T> :
        IReadOnlyDictionary<ulong, T>,
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

        public IEnumerable<ulong> Keys
            => Ids;

        public IEnumerable<T> Values
            => Scalars;

        public Scalar<T> this[int index]
            => GetTermScalar(
                index.BasisVectorIndexToId()
            );

        public Scalar<T> this[int index1, int index2]
            => GetTermScalar(
                BasisBivectorUtils.IndexPairToBivectorId(index1, index2)
            );

        public T this[ulong basisBladeId]
            => GetTermScalar(basisBladeId).ScalarValue;

        public Scalar<T> this[RGaBasisBlade basisBlade]
            => GetTermScalar(basisBlade.Id);

        public Scalar<T> this[RGaSignedBasisBlade basisBlade]
            => basisBlade.IsZero
                ? ScalarProcessor.CreateScalarZero()
                : basisBlade.IsPositive
                    ? GetTermScalar(basisBlade.Id)
                    : -GetTermScalar(basisBlade.Id);


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

        public abstract IReadOnlyDictionary<ulong, T> GetIdScalarDictionary();

        public abstract int GetMaxGrade();

        public abstract bool ContainsKey(ulong key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key, out T value)
        {
            return TryGetTermScalar(key, out value);
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

        public abstract RGaMultivector<T> GetEvenPart();

        public abstract RGaMultivector<T> GetEvenPart(int maxGrade);

        public abstract RGaMultivector<T> GetOddPart();

        public abstract RGaMultivector<T> GetOddPart(int maxGrade);

        public abstract IEnumerable<RGaKVector<T>> GetKVectorParts();

        /// <summary>
        /// Get the scalar coefficient associated with the basis scalar term
        /// </summary>
        /// <returns></returns>
        public abstract Scalar<T> GetScalarTermScalar();

        /// <summary>
        /// Get the scalar coefficient associated with a basis blade term
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        public abstract Scalar<T> GetTermScalar(ulong basisBladeId);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetTermScalar(RGaBasisBlade basisBlade)
        {
            return GetTermScalar(basisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetTermScalar(RGaSignedBasisBlade basisBlade)
        {
            return basisBlade.IsZero
                ? ScalarProcessor.CreateScalarZero()
                : basisBlade.IsPositive
                    ? GetTermScalar(basisBlade.Id)
                    : -GetTermScalar(basisBlade.Id);
        }

        public abstract bool TryGetScalarTermScalar(out T scalar);

        public abstract bool TryGetTermScalar(ulong basisBlade, out T scalar);

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
}
