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
    public sealed partial class XGaVector<T> :
        XGaKVector<T>
    {
        private readonly IReadOnlyDictionary<IIndexSet, T> _idScalarDictionary;


        public override string MultivectorClassName
            => "Generic Vector";

        public override int Count
            => _idScalarDictionary.Count;

        public override IEnumerable<int> KVectorGrades
        {
            get
            {
                if (!IsZero) yield return 1;
            }
        }

        public override int Grade
            => 1;

        public override bool IsZero
            => _idScalarDictionary.Count == 0;

        public override IEnumerable<IIndexSet> Ids
            => _idScalarDictionary.Keys;

        public override IEnumerable<T> Scalars
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _idScalarDictionary.Values;
        }
        
        public IEnumerable<KeyValuePair<int, T>> IndexScalarPairs
            => _idScalarDictionary.Select(p => 
                new KeyValuePair<int, T>(p.Key.FirstIndex, p.Value)
            );

        public override IEnumerable<KeyValuePair<IIndexSet, T>> IdScalarPairs
            => _idScalarDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaVector(XGaProcessor<T> processor)
            : base(processor)
        {
            _idScalarDictionary = new EmptyDictionary<IIndexSet, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaVector(XGaProcessor<T> processor, KeyValuePair<IIndexSet, T> basisScalarPair)
            : base(processor)
        {
            _idScalarDictionary =
                new SingleItemDictionary<IIndexSet, T>(basisScalarPair);

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaVector(XGaProcessor<T> processor, IReadOnlyDictionary<IIndexSet, T> idScalarDictionary)
            : base(processor)
        {
            _idScalarDictionary = idScalarDictionary;

            Debug.Assert(IsValid());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _idScalarDictionary.IsValidVectorDictionary(Processor);
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
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> GetBivectorPart()
        {
            return Processor.CreateZeroBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> GetHigherKVectorPart(int grade)
        {
            return Processor.CreateZeroHigherKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> GetVectorPart(Func<int, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = 
                _idScalarDictionary.Where(term => 
                    filterFunc(term.Key.FirstIndex)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> GetPart(Func<IIndexSet, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> GetPart(Func<T, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Value)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> GetPart(Func<IIndexSet, T, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key, p.Value)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }


        public override IEnumerable<XGaBasisBlade> BasisBlades
            => _idScalarDictionary.Keys.Select(Processor.CreateBasisBlade);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalarTermScalar()
        {
            return ScalarProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetTermScalar(IIndexSet basisBladeId)
        {
            return basisBladeId.IsSingleIndexSet &&
                   _idScalarDictionary.TryGetValue(basisBladeId, out var scalar)
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
        public override bool TryGetTermScalar(IIndexSet basisBladeId, out T scalar)
        {
            if (basisBladeId.IsSingleIndexSet && _idScalarDictionary.TryGetValue(basisBladeId, out scalar))
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
            return BasisScalarPairs
                .OrderBy(p => p.Key.Id.Count)
                .ThenBy(p => p.Key.Id)
                .Select(p => $"'{p.Value:G}'{p.Key}")
                .Concatenate(" + ");
        }
    }
}