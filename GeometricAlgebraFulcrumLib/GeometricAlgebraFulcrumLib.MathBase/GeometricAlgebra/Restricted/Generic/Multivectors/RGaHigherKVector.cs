using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors
{
    public sealed partial class RGaHigherKVector<T> :
        RGaKVector<T>
    {
        private readonly IReadOnlyDictionary<ulong, T> _idScalarDictionary;


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

        public override IEnumerable<RGaBasisBlade> BasisBlades
            => _idScalarDictionary.Keys.Select(Processor.CreateBasisBlade);

        public override IEnumerable<ulong> Ids 
            => _idScalarDictionary.Keys;

        public override IEnumerable<T> Scalars
            => _idScalarDictionary.Values;

        public override IEnumerable<KeyValuePair<ulong, T>> IdScalarPairs
            => _idScalarDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaHigherKVector(RGaProcessor<T> processor, int grade)
            : base(processor)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            _idScalarDictionary = new EmptyDictionary<ulong, T>();

            Grade = grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaHigherKVector(RGaProcessor<T> processor, KeyValuePair<ulong, T> basisScalarPair)
            : base(processor)
        {
            var grade = basisScalarPair.Key.Grade();

            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            _idScalarDictionary =
                new SingleItemDictionary<ulong, T>(basisScalarPair);

            Grade = grade;

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaHigherKVector(RGaProcessor<T> processor, int grade, IReadOnlyDictionary<ulong, T> indexScalarDictionary)
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
            return _idScalarDictionary.IsValidKVectorDictionary(ScalarProcessor, Grade);
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
            return Processor.CreateZeroVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaBivector<T> GetBivectorPart()
        {
            return Processor.CreateZeroBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaHigherKVector<T> GetHigherKVectorPart(int grade)
        {
            return grade == Grade
                ? this
                : Processor.CreateZeroHigherKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaHigherKVector<T> GetPart(Func<ulong, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key)
                ).ToDictionary();

            return Processor.CreateHigherKVector(Grade, idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaHigherKVector<T> GetPart(Func<T, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Value)
                ).ToDictionary();

            return Processor.CreateHigherKVector(Grade, idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaHigherKVector<T> GetPart(Func<ulong, T, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key, p.Value)
                ).ToDictionary();

            return Processor.CreateHigherKVector(Grade, idScalarDictionary);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> Scalar()
        {
            return ScalarProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetBasisBladeScalar(ulong basisBlade)
        {
            return _idScalarDictionary.TryGetValue(basisBlade, out var scalar)
                ? scalar.CreateScalar(ScalarProcessor)
                : ScalarProcessor.CreateScalarZero();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarValue(out T scalar)
        {
            scalar = ScalarProcessor.ScalarZero;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetBasisBladeScalarValue(ulong basisBlade, out T scalar)
        {
            if (basisBlade.Grade() == Grade && _idScalarDictionary.TryGetValue(basisBlade, out scalar))
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
            return IdScalarPairs
                .OrderBy(p => p.Key)
                .Select(p => $"'{p.Value:G}'{p.Key}")
                .Concatenate(" + ");
        }
    }
}
