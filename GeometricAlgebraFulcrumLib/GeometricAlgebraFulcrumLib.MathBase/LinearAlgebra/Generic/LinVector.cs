using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic
{
    public sealed class LinVector<T> :
        IReadOnlyDictionary<int, T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(LinVector<T> mv1)
        {
            return mv1.Negative();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(LinVector<T> mv1, LinVector<T> mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(LinVector<T> mv1, LinVector<T> mv2)
        {
            return mv1.Subtract(mv2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                return mv1.ScalarProcessor.CreateZeroLinVector();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(IntegerSign mv1, LinVector<T> mv2)
        {
            if (mv1.IsZero)
                return mv2.ScalarProcessor.CreateZeroLinVector();

            return mv1.IsPositive ? mv2 : mv2.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> mv1, int mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(int mv1, LinVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> mv1, uint mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(uint mv1, LinVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> mv1, long mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(long mv1, LinVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> mv1, ulong mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(ulong mv1, LinVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> mv1, float mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(float mv1, LinVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> mv1, double mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(double mv1, LinVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> mv1, T mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(T mv1, LinVector<T> mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> mv1, Scalar<T> mv2)
        {
            return mv1.Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(Scalar<T> mv1, LinVector<T> mv2)
        {
            return mv2.Times(mv1.ScalarValue);
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                throw new DivideByZeroException();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> mv1, int mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> mv1, uint mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> mv1, long mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> mv1, ulong mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> mv1, float mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> mv1, double mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> mv1, T mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> mv1, Scalar<T> mv2)
        {
            return mv1.Divide(mv2.ScalarValue);
        }
    

        private readonly IReadOnlyDictionary<int, T> _indexScalarDictionary;


        public string VectorClassName
            => "Generic Linear Vector";

        public IScalarProcessor<T> ScalarProcessor { get; }
    
        /// <summary>
        /// The dimensions of the base vector space, dynamically determined from
        /// stored terms
        /// </summary>
        public int VSpaceDimensions
            => IsZero ? 0 : Indices.Max(id => id) + 1;

        public int Count
            => _indexScalarDictionary.Count;
    
        public bool IsZero
            => _indexScalarDictionary.Count == 0;
    
        public T this[int index]
            => GetTermScalar(index);

        public IEnumerable<int> Indices
            => _indexScalarDictionary.Keys;

        public IEnumerable<T> Scalars
            => _indexScalarDictionary.Values;

        public IEnumerable<KeyValuePair<int, T>> IndexScalarPairs
            => _indexScalarDictionary;

        public IEnumerable<int> Keys 
            => _indexScalarDictionary.Keys;

        public IEnumerable<T> Values 
            => _indexScalarDictionary.Values;
        
        public IEnumerable<KeyValuePair<LinBasisVector, T>> BasisScalarPairs
        {
            get
            {
                return _indexScalarDictionary.Select(p =>
                    new KeyValuePair<LinBasisVector, T>(
                        p.Key.ToLinBasisVector(),
                        p.Value
                    )
                );
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinVector(IScalarProcessor<T> scalarProcessor)
        {
            _indexScalarDictionary = new EmptyDictionary<int, T>();

            ScalarProcessor = scalarProcessor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinVector(IScalarProcessor<T> scalarProcessor, KeyValuePair<int, T> basisScalarPair)
        {
            _indexScalarDictionary =
                new SingleItemDictionary<int, T>(basisScalarPair);
        
            ScalarProcessor = scalarProcessor;

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinVector(IScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, T> idScalarDictionary)
        {
            _indexScalarDictionary = idScalarDictionary;
        
            ScalarProcessor = scalarProcessor;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return _indexScalarDictionary.IsValidLinVectorDictionary(ScalarProcessor);
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(int key)
        {
            return !IsZero && _indexScalarDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(int key, out T value)
        {
            return _indexScalarDictionary.TryGetValue(key, out value);
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyDictionary<int, T> GetIndexScalarDictionary()
        {
            return _indexScalarDictionary;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<LinVectorTerm<T>> GetTerms()
        {
            return _indexScalarDictionary.Select(p =>
                ScalarProcessor.CreateLinTerm(p.Key, p.Value)
            );
        }

        public IEnumerable<LinBasisVector> BasisVectors
            => _indexScalarDictionary.Keys.Select(index => index.ToLinBasisVector());
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetTermScalar(int basisVectorIndex)
        {
            return _indexScalarDictionary.TryGetValue(basisVectorIndex, out var scalar)
                ? ScalarProcessor.CreateScalar(scalar)
                : ScalarProcessor.CreateScalarZero();
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTermScalar(int basisVectorIndex, out T scalar)
        {
            if (_indexScalarDictionary.TryGetValue(basisVectorIndex, out scalar))
                return true;

            scalar = ScalarProcessor.ScalarZero;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector<T> GetSubVector(int vSpaceDimensions)
        {
            return _indexScalarDictionary
                .Where(p => p.Key < vSpaceDimensions)
                .ToDictionary()
                .CreateLinVector(ScalarProcessor);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector<T> Negative()
        {
            return IsZero
                ? ScalarProcessor.CreateZeroLinVector()
                : this.MapScalars(s => ScalarProcessor.Negative(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector<T> Times(T scalar)
        {
            if (IsZero || ScalarProcessor.IsOne(scalar))
                return this;

            return ScalarProcessor.IsZero(scalar)
                ? ScalarProcessor.CreateZeroLinVector()
                : this.MapScalars(s => ScalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector<T> Divide(T scalar)
        {
            return this.MapScalars(s => ScalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector<T> DivideByENormSquared()
        {
            var scalar = ENormSquared().ScalarValue;

            return this.MapScalars(s => ScalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector<T> DivideByENorm()
        {
            var scalar = ENorm().ScalarValue;

            return this.MapScalars(s => ScalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ENormSquared()
        {
            var scalarList =
                _indexScalarDictionary
                    .Values
                    .Select(s => ScalarProcessor.Times(s, s));

            return ScalarProcessor.AddToScalar(scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ENorm()
        {
            return ENormSquared().Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector<T> Add(LinVectorTerm<T> mv2)
        {
            if (IsZero)
                return mv2.ToVector();

            if (mv2.IsZero)
                return this;

            return ScalarProcessor
                .CreateLinVectorComposer()
                .SetVector(this)
                .AddTerm(mv2)
                .GetVector();
        }
    
        public LinVector<T> Add(LinVector<T> mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return ScalarProcessor
                .CreateLinVectorComposer()
                .SetVector(this)
                .AddVector(mv2)
                .GetVector();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector<T> Subtract(LinVectorTerm<T> mv2)
        {
            if (IsZero)
                return mv2.Negative().ToVector();

            if (mv2.IsZero)
                return this;

            return ScalarProcessor
                .CreateLinVectorComposer()
                .SetVector(this)
                .SubtractTerm(mv2)
                .GetVector();
        }
    
        public LinVector<T> Subtract(LinVector<T> mv2)
        {
            if (IsZero)
                return mv2.Negative();

            if (mv2.IsZero)
                return this;

            return ScalarProcessor
                .CreateLinVectorComposer()
                .SetVector(this)
                .SubtractVector(mv2)
                .GetVector();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ESp(LinVectorTerm<T> mv2)
        {
            var scalar2 = mv2.ScalarValue;

            return TryGetTermScalar(mv2.BasisVector.Index, out var scalar1)
                ? ScalarProcessor.TimesToScalar(scalar1, scalar2)
                : ScalarProcessor.CreateScalarZero();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ESp(LinVector<T> mv2)
        {
            return ScalarProcessor
                .CreateScalarComposer()
                .AddESpTerms(this, mv2)
                .GetScalar();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            return _indexScalarDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return BasisScalarPairs
                .OrderBy(p => p.Key)
                .Select(p => $"'{p.Value:G}'{p.Key}")
                .Concatenate(" + ");
        }

    }
}