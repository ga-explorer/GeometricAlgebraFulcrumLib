using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors
{
    /// <summary>
    /// A sparse Euclidean multivector or arbitrary dimensions with double
    /// precision scalars
    /// </summary>
    public abstract partial class RGaFloat64Multivector :
        IReadOnlyDictionary<ulong, double>,
        IRGaFloat64Element
    {
        public abstract string MultivectorClassName { get; }

        public RGaFloat64Processor Processor { get; }

        public RGaMetric Metric 
            => Processor;
        
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
        public abstract IEnumerable<double> Scalars { get; }

        /// <summary>
        /// Get all stored terms as pairs of (BasisBlade ID, Scalar)
        /// </summary>
        /// <value></value>
        public abstract IEnumerable<KeyValuePair<ulong, double>> IdScalarPairs { get; }

        /// <summary>
        /// Get all stored terms as pairs of (BasisBlade, Scalar)
        /// </summary>
        /// <value></value>
        public abstract IEnumerable<KeyValuePair<RGaBasisBlade, double>> BasisScalarPairs { get; }

        public IEnumerable<ulong> Keys
            => Ids;

        public IEnumerable<double> Values
            => Scalars;

        public double this[int index]
            => GetTermScalar(
                index.BasisVectorIndexToId()
            );

        public double this[int index1, int index2]
            => GetTermScalar(
                BasisBivectorUtils.IndexPairToBivectorId(index1, index2)
            );

        public double this[ulong basisBladeId]
            => GetTermScalar(basisBladeId);

        public double this[RGaBasisBlade basisBlade]
            => GetTermScalar(basisBlade.Id);

        public double this[RGaSignedBasisBlade basisBlade]
            => basisBlade.IsZero
                ? 0d
                : basisBlade.IsPositive
                    ? GetTermScalar(basisBlade.Id)
                    : -GetTermScalar(basisBlade.Id);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected RGaFloat64Multivector(RGaFloat64Processor metric)
        {
            Processor = metric;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero()
        {
            return IsZero ||
                   Scalars.All(s => s.IsNearZero());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasSameMetric(RGaFloat64Multivector mv)
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
        public RGaFloat64Scalar AsScalar()
        {
            return (RGaFloat64Scalar)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector AsVector()
        {
            return (RGaFloat64Vector)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector AsBivector()
        {
            return (RGaFloat64Bivector)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64HigherKVector AsHigherKVector()
        {
            return (RGaFloat64HigherKVector)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector AsKVector()
        {
            return (RGaFloat64KVector)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64GradedMultivector AsGradedMultivector()
        {
            return (RGaFloat64GradedMultivector)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector AsUniformMultivector()
        {
            return (RGaFloat64UniformMultivector)this;
        }

        public abstract IReadOnlyDictionary<ulong, double> GetIdScalarDictionary();

        public abstract int GetMaxGrade();

        public abstract bool ContainsKey(ulong key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key, out double value)
        {
            return TryGetTermScalar(key, out value);
        }

        /// <summary>
        /// The number of stored k-vectors in this multivector
        /// </summary>
        public abstract int GetKVectorCount();

        public abstract RGaFloat64Scalar GetScalarPart();

        public abstract RGaFloat64Vector GetVectorPart();

        public abstract RGaFloat64Bivector GetBivectorPart();

        public abstract RGaFloat64HigherKVector GetHigherKVectorPart(int grade);

        public abstract RGaFloat64KVector GetKVectorPart(int grade);

        public abstract RGaFloat64Multivector GetEvenPart();

        public abstract RGaFloat64Multivector GetEvenPart(int maxGrade);

        public abstract RGaFloat64Multivector GetOddPart();

        public abstract RGaFloat64Multivector GetOddPart(int maxGrade);

        public abstract IEnumerable<RGaFloat64KVector> GetKVectorParts();

        /// <summary>
        /// Get the scalar coefficient associated with the basis scalar term
        /// </summary>
        /// <returns></returns>
        public abstract double GetScalarTermScalar();

        /// <summary>
        /// Get the scalar coefficient associated with a basis blade term
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        public abstract double GetTermScalar(ulong basisBladeId);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetTermScalar(RGaBasisBlade basisBlade)
        {
            return GetTermScalar(basisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetTermScalar(RGaSignedBasisBlade basisBlade)
        {
            return basisBlade.IsZero
                ? 0d
                : basisBlade.IsPositive
                    ? GetTermScalar(basisBlade.Id)
                    : -GetTermScalar(basisBlade.Id);
        }

        public abstract bool TryGetScalarTermScalar(out double scalar);

        public abstract bool TryGetTermScalar(ulong basisBlade, out double scalar);

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
                        term.Key, 
                        term.Value
                    )
            );
        }

        /// <summary>
        /// Simplify the storage of this multivector
        /// </summary>
        /// <returns></returns>
        public abstract RGaFloat64Multivector Simplify();
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<ulong, double>> GetEnumerator()
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
