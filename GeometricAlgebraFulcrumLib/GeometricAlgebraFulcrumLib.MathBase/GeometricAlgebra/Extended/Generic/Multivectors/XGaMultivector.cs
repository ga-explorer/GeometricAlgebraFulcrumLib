using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors
{
    /// <summary>
    /// A sparse Euclidean multivector or arbitrary dimensions with T
    /// precision scalars
    /// </summary>
    public abstract partial class XGaMultivector<T> :
        IReadOnlyDictionary<IIndexSet, T>,
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
        public abstract IEnumerable<IIndexSet> Ids { get; }

        /// <summary>
        /// Get scalars of all stored terms
        /// </summary>
        /// <value></value>
        public abstract IEnumerable<T> Scalars { get; }

        /// <summary>
        /// Get all stored terms as pairs of (BasisBlade ID, Scalar)
        /// </summary>
        /// <value></value>
        public abstract IEnumerable<KeyValuePair<IIndexSet, T>> IdScalarPairs { get; }

        /// <summary>
        /// Get all stored terms as pairs of (BasisBlade, Scalar)
        /// </summary>
        /// <value></value>
        public abstract IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs { get; }

        public IEnumerable<IIndexSet> Keys
            => Ids;

        public IEnumerable<T> Values
            => Scalars;

        public Scalar<T> this[int index]
            => GetTermScalar(
                index.IndexToIndexSet()
            );

        public Scalar<T> this[int index1, int index2]
            => GetTermScalar(
                IndexSetUtils.IndexPairToIndexSet(index1, index2)
            );

        public T this[IIndexSet basisBladeId]
            => GetTermScalar(basisBladeId).ScalarValue;

        public Scalar<T> this[XGaBasisBlade basisBlade]
            => GetTermScalar(basisBlade.Id);

        public Scalar<T> this[XGaSignedBasisBlade basisBlade]
            => basisBlade.IsZero
                ? ScalarProcessor.CreateScalarZero()
                : basisBlade.IsPositive
                    ? GetTermScalar(basisBlade.Id)
                    : -GetTermScalar(basisBlade.Id);


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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> AsScalar()
        {
            return (XGaScalar<T>)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> AsVector()
        {
            return (XGaVector<T>)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> AsBivector()
        {
            return (XGaBivector<T>)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> AsHigherKVector()
        {
            return (XGaHigherKVector<T>)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> AsKVector()
        {
            return (XGaKVector<T>)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> AsGradedMultivector()
        {
            return (XGaGradedMultivector<T>)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivector<T> AsUniformMultivector()
        {
            return (XGaUniformMultivector<T>)this;
        }

        public abstract IReadOnlyDictionary<IIndexSet, T> GetIdScalarDictionary();

        public abstract int GetMaxGrade();

        public abstract bool ContainsKey(IIndexSet key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(IIndexSet key, out T value)
        {
            return TryGetTermScalar(key, out value);
        }

        /// <summary>
        /// The number of stored k-vectors in this multivector
        /// </summary>
        public abstract int GetKVectorCount();

        public abstract XGaScalar<T> GetScalarPart();

        public abstract XGaVector<T> GetVectorPart();

        public abstract XGaBivector<T> GetBivectorPart();

        public abstract XGaHigherKVector<T> GetHigherKVectorPart(int grade);

        public abstract XGaKVector<T> GetKVectorPart(int grade);

        public abstract XGaMultivector<T> GetEvenPart();

        public abstract XGaMultivector<T> GetEvenPart(int maxGrade);

        public abstract XGaMultivector<T> GetOddPart();

        public abstract XGaMultivector<T> GetOddPart(int maxGrade);

        public abstract IEnumerable<XGaKVector<T>> GetKVectorParts();

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
        public abstract Scalar<T> GetTermScalar(IIndexSet basisBladeId);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetTermScalar(XGaBasisBlade basisBlade)
        {
            return GetTermScalar(basisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetTermScalar(XGaSignedBasisBlade basisBlade)
        {
            return basisBlade.IsZero
                ? ScalarProcessor.CreateScalarZero()
                : basisBlade.IsPositive
                    ? GetTermScalar(basisBlade.Id)
                    : -GetTermScalar(basisBlade.Id);
        }

        public abstract bool TryGetScalarTermScalar(out T scalar);

        public abstract bool TryGetTermScalar(IIndexSet basisBlade, out T scalar);

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

        /// <summary>
        /// Simplify the storage of this multivector
        /// </summary>
        /// <returns></returns>
        public abstract XGaMultivector<T> Simplify();
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<IIndexSet, T>> GetEnumerator()
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
