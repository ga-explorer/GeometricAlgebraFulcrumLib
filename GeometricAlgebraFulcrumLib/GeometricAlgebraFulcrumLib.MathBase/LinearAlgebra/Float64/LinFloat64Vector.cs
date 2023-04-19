using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64
{
    public sealed class LinFloat64Vector :
        IReadOnlyDictionary<int, double>
    {
        public static LinFloat64Vector ZeroVector { get; }
            = new LinFloat64Vector();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator -(LinFloat64Vector mv1)
        {
            return mv1.Negative();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator +(LinFloat64Vector mv1, LinFloat64Vector mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator -(LinFloat64Vector mv1, LinFloat64Vector mv2)
        {
            return mv1.Subtract(mv2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(LinFloat64Vector mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                return new LinFloat64Vector();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(IntegerSign mv1, LinFloat64Vector mv2)
        {
            if (mv1.IsZero)
                return new LinFloat64Vector();

            return mv1.IsPositive ? mv2 : mv2.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(LinFloat64Vector mv1, int mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(int mv1, LinFloat64Vector mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(LinFloat64Vector mv1, uint mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(uint mv1, LinFloat64Vector mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(LinFloat64Vector mv1, long mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(long mv1, LinFloat64Vector mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(LinFloat64Vector mv1, ulong mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(ulong mv1, LinFloat64Vector mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(LinFloat64Vector mv1, float mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(float mv1, LinFloat64Vector mv2)
        {
            return mv2.Times(mv1);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(LinFloat64Vector mv1, double mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator *(double mv1, LinFloat64Vector mv2)
        {
            return mv2.Times(mv1);
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator /(LinFloat64Vector mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                throw new DivideByZeroException();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator /(LinFloat64Vector mv1, int mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator /(LinFloat64Vector mv1, uint mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator /(LinFloat64Vector mv1, long mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator /(LinFloat64Vector mv1, ulong mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator /(LinFloat64Vector mv1, float mv2)
        {
            return mv1.Divide(mv2);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector operator /(LinFloat64Vector mv1, double mv2)
        {
            return mv1.Divide(mv2);
        }
    

        private readonly IReadOnlyDictionary<int, double> _indexScalarDictionary;


        public string VectorClassName
            => "Float64 Linear Vector";
    
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
    
        public double X 
            => this[0];

        public double Y 
            => this[1];

        public double Z 
            => this[2];

        public double W 
            => this[3];

        public double this[int index]
            => GetTermScalar(index);

        public IEnumerable<int> Indices
            => _indexScalarDictionary.Keys;

        public IEnumerable<double> Scalars
            => _indexScalarDictionary.Values;

        public IEnumerable<KeyValuePair<int, double>> IndexScalarPairs
            => _indexScalarDictionary;

        public IEnumerable<int> Keys 
            => _indexScalarDictionary.Keys;

        public IEnumerable<double> Values 
            => _indexScalarDictionary.Values;
        
        public IEnumerable<KeyValuePair<LinBasisVector, double>> BasisScalarPairs
        {
            get
            {
                return _indexScalarDictionary.Select(p =>
                    new KeyValuePair<LinBasisVector, double>(
                        p.Key.ToLinBasisVector(),
                        p.Value
                    )
                );
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinFloat64Vector()
        {
            _indexScalarDictionary = new EmptyDictionary<int, double>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinFloat64Vector(KeyValuePair<int, double> basisScalarPair)
        {
            _indexScalarDictionary =
                new SingleItemDictionary<int, double>(basisScalarPair);
        
            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinFloat64Vector(IReadOnlyDictionary<int, double> idScalarDictionary)
        {
            _indexScalarDictionary = idScalarDictionary;
        
            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return _indexScalarDictionary.IsValidLinVectorDictionary();
        }
    
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1e-12)
        {
            return ENorm().IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearUnit(double epsilon = 1e-12)
        {
            return ENormSquared().IsNearOne(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearOrthonormalWith(LinFloat64Vector vector, double epsilon = 1e-12)
        {
            return IsNearUnit(epsilon) &&
                   vector.IsNearUnit(epsilon) &&
                   ESp(vector).IsNearZero(epsilon);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearOrthonormalWithUnit(LinFloat64Vector vector, double epsilon = 1e-12)
        {
            Debug.Assert(
                vector.IsNearUnit(epsilon)
            );

            return IsNearUnit(epsilon) &&
                   ESp(vector).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearParallelTo(LinFloat64Vector vector, double epsilon = 1e-12)
        {
            return this.GetAngleCos(vector).IsNearZero(epsilon);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearParallelToUnit(LinFloat64Vector vector, double epsilon = 1e-12)
        {
            Debug.Assert(
                vector.IsNearUnit(epsilon)
            );

            return this.GetAngleCosWithUnit(vector).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearOrthogonalTo(LinFloat64Vector vector, double epsilon = 1e-12)
        {
            return ESp(vector).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsVectorBasis(int basisIndex)
        {
            if (_indexScalarDictionary.Count != 1)
                return false;

            var (index, scalar) = _indexScalarDictionary.First();

            return index == basisIndex && scalar.IsOne();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearVectorBasis(int basisIndex, double epsilon = 1e-12d)
        {
            return _indexScalarDictionary.Aggregate(
                0d,
                (norm, indexScalarPair) =>
                    norm + 
                    (indexScalarPair.Key == basisIndex 
                        ? (indexScalarPair.Value - 1d).Square() 
                        : indexScalarPair.Value.Square())
            ).IsNearZero(epsilon);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(int key)
        {
            return !IsZero && _indexScalarDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(int key, out double value)
        {
            return _indexScalarDictionary.TryGetValue(key, out value);
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyDictionary<int, double> GetIndexScalarDictionary()
        {
            return _indexScalarDictionary;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<LinFloat64VectorTerm> GetTerms()
        {
            return _indexScalarDictionary.Select(p =>
                p.Key.CreateLinTerm(p.Value)
            );
        }

        public IEnumerable<LinBasisVector> BasisVectors
            => _indexScalarDictionary.Keys.Select(index => index.ToLinBasisVector());
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetTermScalar(int basisVectorIndex)
        {
            return _indexScalarDictionary.TryGetValue(basisVectorIndex, out var scalar)
                ? scalar
                : 0d;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTermScalar(int basisVectorIndex, out double scalar)
        {
            if (_indexScalarDictionary.TryGetValue(basisVectorIndex, out scalar))
                return true;

            scalar = 0d;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetComponent(ILinSignedBasisVector axis)
        {
            return GetTermScalar(axis.Index) * axis.Sign;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetComponent(int axisIndex, bool axisNegative = false)
        {
            var component = GetTermScalar(axisIndex);

            return axisNegative ? -component : component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector GetSubVector(int vSpaceDimensions)
        {
            return _indexScalarDictionary
                .Where(p => p.Key < vSpaceDimensions)
                .ToDictionary()
                .CreateLinVector();
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector Negative()
        {
            return IsZero
                ? new LinFloat64Vector()
                : this.MapScalars(s => -(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector Times(double scalar)
        {
            if (IsZero || scalar.IsZero())
                return new LinFloat64Vector();

            return scalar.IsOne()
                ? this
                : this.MapScalars(s => s * scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector Divide(double scalar)
        {
            var s1 = 1d / scalar;

            return this.MapScalars(s => s * s1);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector DivideByENormSquared()
        {
            var scalar = 1d / ENormSquared();

            return this.MapScalars(s => s * scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector DivideByENorm()
        {
            var scalar = 1d / ENorm();

            return this.MapScalars(s => s * scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ENormSquared()
        {
            return IsZero
                ? 0
                : _indexScalarDictionary
                    .Values
                    .Select(s => s * s)
                    .Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ENorm()
        {
            return Math.Sqrt(ENormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector Add(LinFloat64VectorTerm mv2)
        {
            if (IsZero)
                return mv2.ToVector();

            if (mv2.IsZero)
                return this;

            return new LinFloat64VectorComposer()
                .SetVector(this)
                .AddTerm(mv2)
                .GetVector();
        }
    
        public LinFloat64Vector Add(LinFloat64Vector mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return new LinFloat64VectorComposer()
                .SetVector(this)
                .AddVector(mv2)
                .GetVector();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector Subtract(LinFloat64VectorTerm mv2)
        {
            if (IsZero)
                return mv2.Negative().ToVector();

            if (mv2.IsZero)
                return this;

            return new LinFloat64VectorComposer()
                .SetVector(this)
                .SubtractTerm(mv2)
                .GetVector();
        }
    
        public LinFloat64Vector Subtract(LinFloat64Vector mv2)
        {
            if (IsZero)
                return mv2.Negative();

            if (mv2.IsZero)
                return this;

            return new LinFloat64VectorComposer()
                .SetVector(this)
                .SubtractVector(mv2)
                .GetVector();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESp(LinFloat64VectorTerm mv2)
        {
            var scalar2 = mv2.ScalarValue;

            return TryGetTermScalar(mv2.BasisVector.Index, out var scalar1)
                ? scalar1 * scalar2
                : 0d;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESp(LinFloat64Vector mv2)
        {
            return Float64ScalarComposer
                .Create()
                .AddESpTerms(this, mv2)
                .ScalarValue;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<int, double>> GetEnumerator()
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