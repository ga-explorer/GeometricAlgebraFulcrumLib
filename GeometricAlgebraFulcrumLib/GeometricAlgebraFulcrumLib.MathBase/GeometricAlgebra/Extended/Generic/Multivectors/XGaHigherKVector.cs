using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors
{
    public sealed partial class XGaHigherKVector<T> :
        XGaKVector<T>
    {
        private readonly IReadOnlyDictionary<IIndexSet, T> _idScalarDictionary;


        public override string MultivectorClassName
            => $"Generic {Grade}-Vector";

        public override int Count
            => _idScalarDictionary.Count;

        public override IEnumerable<int> KVectorGrades
        {
            get
            {
                if (!IsZero) yield return Grade;
            }
        }

        public override int Grade { get; }

        public override bool IsZero
            => _idScalarDictionary.Count == 0;

        public override IEnumerable<XGaBasisBlade> BasisBlades
            => _idScalarDictionary.Keys.Select(Processor.CreateBasisBlade);

        public override IEnumerable<IIndexSet> Ids 
            => _idScalarDictionary.Keys;

        public override IEnumerable<T> Scalars
            => _idScalarDictionary.Values;

        public override IEnumerable<KeyValuePair<IIndexSet, T>> IdScalarPairs
            => _idScalarDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaHigherKVector(XGaProcessor<T> processor, int grade)
            : base(processor)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            _idScalarDictionary = new EmptyDictionary<IIndexSet, T>();

            Grade = grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaHigherKVector(XGaProcessor<T> processor, KeyValuePair<IIndexSet, T> basisScalarPair)
            : base(processor)
        {
            var grade = basisScalarPair.Key.Count;

            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            _idScalarDictionary =
                new SingleItemDictionary<IIndexSet, T>(basisScalarPair);

            Grade = grade;

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaHigherKVector(XGaProcessor<T> processor, int grade, IReadOnlyDictionary<IIndexSet, T> indexScalarDictionary)
            : base(processor)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            _idScalarDictionary = indexScalarDictionary;

            Grade = grade;

            Debug.Assert(IsValid());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _idScalarDictionary.IsValidKVectorDictionary(Processor, Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IReadOnlyDictionary<IIndexSet, T> GetIdScalarDictionary()
        {
            return _idScalarDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(IIndexSet key)
        {
            return !IsZero && _idScalarDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> GetScalarPart()
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> GetVectorPart()
        {
            return Processor.CreateZeroVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> GetBivectorPart()
        {
            return Processor.CreateZeroBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> GetHigherKVectorPart(int grade)
        {
            return grade == Grade
                ? this
                : Processor.CreateZeroHigherKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> GetPart(Func<IIndexSet, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key)
                ).ToDictionary();

            return Processor.CreateHigherKVector(Grade, idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> GetPart(Func<T, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Value)
                ).ToDictionary();

            return Processor.CreateHigherKVector(Grade, idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> GetPart(Func<IIndexSet, T, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key, p.Value)
                ).ToDictionary();

            return Processor.CreateHigherKVector(Grade, idScalarDictionary);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalarTermScalar()
        {
            return ScalarProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetTermScalar(IIndexSet basisBlade)
        {
            if (basisBlade.Count != Grade)
                throw new IndexOutOfRangeException(nameof(basisBlade));

            return _idScalarDictionary.TryGetValue(basisBlade, out var scalar)
                ? ScalarProcessor.CreateScalar(scalar)
                : ScalarProcessor.CreateScalarZero();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarTermScalar(out T scalar)
        {
            scalar = ScalarProcessor.ScalarZero;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermScalar(IIndexSet basisBlade, out T scalar)
        {
            if (basisBlade.Count == Grade && _idScalarDictionary.TryGetValue(basisBlade, out scalar))
                return true;

            scalar = ScalarProcessor.ScalarZero;
            return false;
        }


        public override IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs
        {
            get
            {
                return _idScalarDictionary.Select(p =>
                    new KeyValuePair<XGaBasisBlade, T>(
                        Processor.CreateBasisBlade(p.Key),
                        p.Value
                    )
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Simplify()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return IdScalarPairs
                .OrderBy(p => p.Key)
                .Select(p => $"'{p.Value:G}'{p.Key}")
                .Concatenate(" + ");
        }
    }
}
