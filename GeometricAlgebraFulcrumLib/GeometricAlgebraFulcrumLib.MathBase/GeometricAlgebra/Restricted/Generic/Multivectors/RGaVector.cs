using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors
{
    public sealed partial class RGaVector<T> :
        RGaKVector<T>
    {
        private readonly IReadOnlyDictionary<ulong, T> _idScalarDictionary;


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

        public override IEnumerable<ulong> Ids
            => _idScalarDictionary.Keys;

        public override IEnumerable<T> Scalars
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _idScalarDictionary.Values;
        }
        
        public IEnumerable<KeyValuePair<int, T>> IndexScalarPairs
            => _idScalarDictionary.Select(p => 
                new KeyValuePair<int, T>(p.Key.FirstOneBitPosition(), p.Value)
            );

        public override IEnumerable<KeyValuePair<ulong, T>> IdScalarPairs
            => _idScalarDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaVector(RGaProcessor<T> processor)
            : base(processor)
        {
            _idScalarDictionary = new EmptyDictionary<ulong, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaVector(RGaProcessor<T> processor, KeyValuePair<ulong, T> basisScalarPair)
            : base(processor)
        {
            _idScalarDictionary =
                new SingleItemDictionary<ulong, T>(basisScalarPair);

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaVector(RGaProcessor<T> processor, IReadOnlyDictionary<ulong, T> idScalarDictionary)
            : base(processor)
        {
            _idScalarDictionary = idScalarDictionary;

            Debug.Assert(IsValid());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _idScalarDictionary.IsValidVectorDictionary(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IReadOnlyDictionary<ulong, T> GetIdScalarDictionary()
        {
            return _idScalarDictionary;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(ulong key)
        {
            return !IsZero && _idScalarDictionary.ContainsKey(key);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> GetScalarPart()
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaVector<T> GetVectorPart()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaBivector<T> GetBivectorPart()
        {
            return Processor.CreateZeroBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaHigherKVector<T> GetHigherKVectorPart(int grade)
        {
            return Processor.CreateZeroHigherKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> GetVectorPart(Func<int, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = 
                _idScalarDictionary.Where(term => 
                    filterFunc(term.Key.FirstOneBitPosition())
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> GetPart(Func<ulong, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> GetPart(Func<T, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Value)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> GetPart(Func<ulong, T, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key, p.Value)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }


        public override IEnumerable<RGaBasisBlade> BasisBlades
            => _idScalarDictionary.Keys.Select(Processor.CreateBasisBlade);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalarTermScalar()
        {
            return ScalarProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetTermScalar(ulong basisBladeId)
        {
            return basisBladeId.IsBasisVector() &&
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
        public override bool TryGetTermScalar(ulong basisBladeId, out T scalar)
        {
            if (basisBladeId.IsBasisVector() && _idScalarDictionary.TryGetValue(basisBladeId, out scalar))
                return true;

            scalar = ScalarProcessor.ScalarZero;
            return false;
        }


        public override IEnumerable<KeyValuePair<RGaBasisBlade, T>> BasisScalarPairs
        {
            get
            {
                return _idScalarDictionary.Select(p =>
                    new KeyValuePair<RGaBasisBlade, T>(
                        Processor.CreateBasisBlade(p.Key),
                        p.Value
                    )
                );
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Simplify()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : this;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return BasisScalarPairs
                .OrderBy(p => p.Key.Id.Grade())
                .ThenBy(p => p.Key.Id)
                .Select(p => $"'{p.Value:G}'{p.Key}")
                .Concatenate(" + ");
        }
    }
}