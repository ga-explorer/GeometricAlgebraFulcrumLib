using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors
{
    public sealed partial class XGaBivector<T> :
        XGaKVector<T>
    {
        private readonly IReadOnlyDictionary<IIndexSet, T> _idScalarDictionary;


        public override string MultivectorClassName
            => "Generic Bivector";

        public override int Count
            => _idScalarDictionary.Count;

        public override IEnumerable<int> KVectorGrades
        {
            get
            {
                if (!IsZero) yield return 2;
            }
        }

        public override int Grade
            => 2;

        public override bool IsZero
            => _idScalarDictionary.Count == 0;

        public override IEnumerable<IIndexSet> Ids
            => _idScalarDictionary.Keys;

        public override IEnumerable<T> Scalars
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _idScalarDictionary.Values;
        }

        public override IEnumerable<KeyValuePair<IIndexSet, T>> IdScalarPairs
            => _idScalarDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaBivector(XGaProcessor<T> processor)
            : base(processor)
        {
            _idScalarDictionary = new EmptyDictionary<IIndexSet, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaBivector(XGaProcessor<T> processor, KeyValuePair<IIndexSet, T> basisScalarPair)
            : base(processor)
        {
            _idScalarDictionary =
                new SingleItemDictionary<IIndexSet, T>(basisScalarPair);

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaBivector(XGaProcessor<T> processor, IReadOnlyDictionary<IIndexSet, T> scalarDictionary)
            : base(processor)
        {
            _idScalarDictionary = scalarDictionary;

            Debug.Assert(IsValid());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _idScalarDictionary.IsValidBivectorDictionary(Processor);
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
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> GetHigherKVectorPart(int grade)
        {
            return Processor.CreateZeroHigherKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> GetBivectorPart(Func<int, int, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = 
                _idScalarDictionary.Where(term => 
                    filterFunc(term.Key.FirstIndex, term.Key.LastIndex)
                ).ToDictionary();

            return Processor.CreateBivector(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> GetPart(Func<IIndexSet, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key)
                ).ToDictionary();

            return Processor.CreateBivector(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> GetPart(Func<T, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Value)
                ).ToDictionary();

            return Processor.CreateBivector(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> GetPart(Func<IIndexSet, T, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key, p.Value)
                ).ToDictionary();

            return Processor.CreateBivector(idScalarDictionary);
        }


        public override IEnumerable<XGaBasisBlade> BasisBlades
            => _idScalarDictionary.Keys.Select(Processor.CreateBasisBlade);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> Scalar()
        {
            return ScalarProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetBasisBladeScalar(IIndexSet basisBlade)
        {
            return _idScalarDictionary.TryGetValue(basisBlade, out var scalar)
                ? ScalarProcessor.CreateScalar(scalar)
                : ScalarProcessor.CreateScalarZero();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarValue(out T scalar)
        {
            scalar = ScalarProcessor.ScalarZero;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetBasisBladeScalarValue(IIndexSet basisBlade, out T scalar)
        {
            if (_idScalarDictionary.TryGetValue(basisBlade, out scalar))
                return true;

            scalar = ScalarProcessor.ScalarZero;
            return false;
        }


        public override IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs
        {
            get
            {
                return _idScalarDictionary.Select(p =>
                    new KeyValuePair<XGaBasisBlade, T>(Processor.CreateBasisBlade(p.Key), p.Value)
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