using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors
{
    public sealed partial class XGaScalar<T> :
        XGaKVector<T>
    {
        public override string MultivectorClassName
            => "Generic Scalar";

        [NotNull]
        public T ScalarValue
            => Scalar.ScalarValue;

        public Scalar<T> Scalar { get; }

        public override int Count
            => IsZero ? 0 : 1;

        public override IEnumerable<int> KVectorGrades
        {
            get
            {
                if (!IsZero) yield return 0;
            }
        }

        public override int Grade
            => 0;

        public override bool IsZero { get; }

        public bool IsOne
            => ScalarProcessor.IsOne(ScalarValue);
    
        public bool IsMinusOne
            => ScalarProcessor.IsMinusOne(ScalarValue);

        public override IEnumerable<XGaBasisBlade> BasisBlades
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (!IsZero) yield return Processor.BasisScalar;
            }
        }

        public override IEnumerable<IIndexSet> Ids
        {
            get
            {
                if (!IsZero) yield return EmptyIndexSet.Instance;
            }
        }

        public override IEnumerable<T> Scalars
        {
            get
            {
                if (!IsZero) yield return ScalarValue;
            }
        }

        public override IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (!IsZero)
                    yield return new KeyValuePair<XGaBasisBlade, T>(
                        Processor.BasisScalar,
                        ScalarValue
                    );
            }
        }

        public override IEnumerable<KeyValuePair<IIndexSet, T>> IdScalarPairs
        {
            get
            {
                if (!IsZero)
                    yield return new KeyValuePair<IIndexSet, T>(
                        EmptyIndexSet.Instance,
                        ScalarValue
                    );
            }
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaScalar(XGaProcessor<T> processor)
            : base(processor)
        {
            Scalar = processor.ScalarProcessor.CreateScalarZero();
            IsZero = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaScalar(XGaProcessor<T> processor, T scalarValue)
            : base(processor)
        {
            Debug.Assert(
                processor.ScalarProcessor.IsValid(scalarValue)
            );

            Scalar = processor.ScalarProcessor.CreateScalar(scalarValue);
            IsZero = processor.ScalarProcessor.IsZero(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaScalar(XGaProcessor<T> processor, IReadOnlyDictionary<IIndexSet, T> idScalarDictionary)
            : base(processor)
        {
            Scalar = processor.ScalarProcessor.CreateScalar(
                idScalarDictionary.TryGetValue(EmptyIndexSet.Instance, out var scalar)
                    ? scalar
                    : processor.ScalarProcessor.ScalarZero
            );

            Debug.Assert(
                Scalar.IsValid()
            );

            IsZero = Scalar.IsZero();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return ScalarProcessor.IsValid(ScalarValue);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IReadOnlyDictionary<IIndexSet, T> GetIdScalarDictionary()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(IIndexSet key)
        {
            return key.IsEmptySet && !IsZero;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> GetScalarPart()
        {
            return this;
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
            return Processor.CreateZeroHigherKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> GetPart(Func<IIndexSet, bool> filterFunc)
        {
            return IsZero || filterFunc(EmptyIndexSet.Instance) 
                ? this 
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> GetPart(Func<T, bool> filterFunc)
        {
            return IsZero || filterFunc(ScalarValue) 
                ? this 
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> GetPart(Func<IIndexSet, T, bool> filterFunc)
        {
            return IsZero || filterFunc(EmptyIndexSet.Instance, ScalarValue) 
                ? this 
                : Processor.CreateZeroScalar();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalarTermScalar()
        {
            return Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetTermScalar(IIndexSet basisBladeId)
        {
            return basisBladeId.IsEmptySet
                ? Scalar
                : Processor.ScalarProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarTermScalar(out T scalar)
        {
            if (!IsZero)
            {
                scalar = ScalarValue;
                return true;
            }

            scalar = ScalarProcessor.ScalarZero;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermScalar(IIndexSet basisBlade, out T scalar)
        {
            if (basisBlade.IsEmptySet)
            {
                scalar = ScalarValue;
                return true;
            }

            scalar = ScalarProcessor.ScalarZero;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Simplify()
        {
            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool Equals(XGaScalar<T> other)
        {
            return Equals(Scalar, other.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is XGaScalar<T> other && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return Scalar.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return IsZero ? string.Empty : $"'{ScalarValue:G}'<>";
        }
    }
}