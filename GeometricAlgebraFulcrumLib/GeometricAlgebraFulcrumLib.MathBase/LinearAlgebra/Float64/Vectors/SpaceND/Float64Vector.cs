using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND
{
    public sealed class Float64Vector :
        IReadOnlyDictionary<int, double>,
        ILinearElement
    {
        public static Float64Vector ZeroVector { get; }
            = new Float64Vector();


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public LinFloat64Vector(double item0)
        //{
        //    Debug.Assert(item0.IsNotNaN());

        //    MathNetVector = Vector.Build.DenseOfArray(new []
        //    {
        //        item0
        //    });
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector Create(double item0)
        {
            var vector = new[]
            {
                item0
            };

            return new Float64Vector(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector Create(double item0, double item1)
        {
            var vector = new[]
            {
                item0,
                item1
            };

            return new Float64Vector(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector Create(params double[] scalarArray)
        {
            return new Float64Vector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateCopy(double[] scalarArray)
        {
            var scalarArray1 = new double[scalarArray.Length];
            scalarArray.CopyTo(scalarArray1, 0);

            return new Float64Vector(scalarArray1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateCopy(IReadOnlyList<double> tuple)
        {
            var scalarArray = new double[tuple.Count];

            for (var i = 0; i < scalarArray.Length; i++)
                scalarArray[i] = tuple[i];

            return new Float64Vector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector Create(IEnumerable<double> scalarList)
        {
            var scalarArray = scalarList.ToArray();

            return new Float64Vector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector Create(IPair<double> tuple)
        {
            var scalarArray = tuple.GetItemArray();

            return new Float64Vector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector Create(ITriplet<double> tuple)
        {
            var scalarArray = tuple.GetItemArray();

            return new Float64Vector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector Create(IQuad<double> tuple)
        {
            var scalarArray = tuple.GetItemArray();

            return new Float64Vector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector Create(IQuint<double> tuple)
        {
            var scalarArray = tuple.GetItemArray();

            return new Float64Vector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector Create(IHexad<double> tuple)
        {
            var scalarArray = tuple.GetItemArray();

            return new Float64Vector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateFromRgba(Color color)
        {
            var tuple = color.ToPixel<Rgba32>();

            var scalarArray = new[]
            {
                tuple.R / 255d,
                tuple.G / 255d,
                tuple.B / 255d,
                tuple.A / 255d
            };

            return new Float64Vector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateZero(int dimensions)
        {
            return new Float64Vector(new double[dimensions]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateBasis(int dimensions, int index)
        {
            var scalarArray = new double[dimensions];
            scalarArray[index] = 1d;

            return new Float64Vector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateScaledBasis(int dimensions, int index, double scalar)
        {
            var scalarArray = new double[dimensions];
            scalarArray[index] = scalar;

            return new Float64Vector(scalarArray);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator -(Float64Vector mv1)
        {
            return mv1.Negative();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator +(Float64Vector mv1, Float64Vector vector2)
        {
            return mv1.Add(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator -(Float64Vector mv1, Float64Vector vector2)
        {
            return mv1.Subtract(vector2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(Float64Vector mv1, IntegerSign vector2)
        {
            if (vector2.IsZero)
                return new Float64Vector();

            return vector2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(IntegerSign mv1, Float64Vector vector2)
        {
            if (mv1.IsZero)
                return new Float64Vector();

            return mv1.IsPositive ? vector2 : vector2.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(Float64Vector mv1, int vector2)
        {
            return mv1.Times(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(int mv1, Float64Vector vector2)
        {
            return vector2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(Float64Vector mv1, uint vector2)
        {
            return mv1.Times(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(uint mv1, Float64Vector vector2)
        {
            return vector2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(Float64Vector mv1, long vector2)
        {
            return mv1.Times(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(long mv1, Float64Vector vector2)
        {
            return vector2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(Float64Vector mv1, ulong vector2)
        {
            return mv1.Times(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(ulong mv1, Float64Vector vector2)
        {
            return vector2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(Float64Vector mv1, float vector2)
        {
            return mv1.Times(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(float mv1, Float64Vector vector2)
        {
            return vector2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(Float64Vector mv1, double vector2)
        {
            return mv1.Times(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator *(double mv1, Float64Vector vector2)
        {
            return vector2.Times(mv1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator /(Float64Vector mv1, IntegerSign vector2)
        {
            if (vector2.IsZero)
                throw new DivideByZeroException();

            return vector2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator /(Float64Vector mv1, int vector2)
        {
            return mv1.Divide(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator /(Float64Vector mv1, uint vector2)
        {
            return mv1.Divide(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator /(Float64Vector mv1, long vector2)
        {
            return mv1.Divide(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator /(Float64Vector mv1, ulong vector2)
        {
            return mv1.Divide(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator /(Float64Vector mv1, float vector2)
        {
            return mv1.Divide(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector operator /(Float64Vector mv1, double vector2)
        {
            return mv1.Divide(vector2);
        }


        private readonly IReadOnlyDictionary<int, double> _indexScalarDictionary;


        public string VectorClassName
            => "Float64 Linear Vector";

        /// <summary>
        /// The dimensions of the base vector space, dynamically determined from
        /// stored terms
        /// </summary>
        public int VSpaceDimensions
            => IsZero ? 0 : Indices.Max() + 1;

        public int Count
            => _indexScalarDictionary.Count;

        public int FirstIndex
            => IsZero ? -1 : Indices.Min();

        public int LastIndex
            => IsZero ? -1 : Indices.Max();

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
        internal Float64Vector()
        {
            _indexScalarDictionary = new EmptyDictionary<int, double>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Float64Vector(KeyValuePair<int, double> basisScalarPair)
        {
            _indexScalarDictionary =
                new SingleItemDictionary<int, double>(basisScalarPair);

            Debug.Assert(IsValid());
        }

        public Float64Vector(IEnumerable<double> scalarList)
            : this(scalarList.CreateValidLinVectorDictionary())
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Float64Vector(IReadOnlyDictionary<int, double> idScalarDictionary)
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
        public bool IsNearOrthonormalWith(Float64Vector vector, double epsilon = 1e-12)
        {
            return IsNearUnit(epsilon) &&
                   vector.IsNearUnit(epsilon) &&
                   ESp(vector).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearOrthonormalWithUnit(Float64Vector vector, double epsilon = 1e-12)
        {
            Debug.Assert(
                vector.IsNearUnit(epsilon)
            );

            return IsNearUnit(epsilon) &&
                   ESp(vector).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearParallelTo(ILinSignedBasisVector vector, double epsilon = 1e-12)
        {
            Debug.Assert(
                vector.IsNonZero
            );

            return this.GetAngleCosWithUnit(vector).Abs().IsNearOne(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearParallelTo(Float64Vector vector, double epsilon = 1e-12)
        {
            return this.GetAngleCos(vector).Abs().IsNearOne(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearParallelToUnit(ILinSignedBasisVector vector, double epsilon = 1e-12)
        {
            Debug.Assert(
                vector.IsNonZero
            );

            return this.GetAngleCosWithUnit(vector).Abs().IsNearOne(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearParallelToUnit(Float64Vector vector, double epsilon = 1e-12)
        {
            Debug.Assert(
                vector.IsNearUnit(epsilon)
            );

            return this.GetAngleCosWithUnit(vector).Abs().IsNearOne(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearOppositeTo(Float64Vector vector, double epsilon = 1e-12)
        {
            return this.GetAngleCos(vector).IsNearMinusOne(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearOppositeToUnit(ILinSignedBasisVector vector, double epsilon = 1e-12)
        {
            return this.GetAngleCosWithUnit(vector).IsNearMinusOne(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearOppositeToUnit(Float64Vector vector, double epsilon = 1e-12)
        {
            return this.GetAngleCosWithUnit(vector).IsNearMinusOne(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearOrthogonalTo(Float64Vector vector, double epsilon = 1e-12)
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
        public Float64Vector GetCopyByIndex(Func<int, bool> filterFunc)
        {
            return new Float64Vector(
                _indexScalarDictionary
                    .Where(p => filterFunc(p.Key))
                    .ToDictionary(
                        p => p.Key,
                        p => p.Value
                    ).ToSimpleDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector GetCopyByScalar(Func<double, bool> filterFunc)
        {
            return new Float64Vector(
                _indexScalarDictionary
                    .Where(p => filterFunc(p.Value))
                    .ToDictionary(
                        p => p.Key,
                        p => p.Value
                    ).ToSimpleDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector GetCopyByIndexScalar(Func<int, double, bool> filterFunc)
        {
            return new Float64Vector(
                _indexScalarDictionary
                    .Where(p => filterFunc(p.Key, p.Value))
                    .ToDictionary(
                        p => p.Key,
                        p => p.Value
                    ).ToSimpleDictionary()
            );
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
        public IEnumerable<Float64VectorTerm> GetTerms()
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
        public Float64Vector GetSubVector(int vSpaceDimensions)
        {
            return _indexScalarDictionary
                .Where(p => p.Key < vSpaceDimensions)
                .ToDictionary()
                .CreateLinVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector Negative()
        {
            return IsZero
                ? new Float64Vector()
                : this.MapScalars(s => -s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector Times(double scalar)
        {
            if (IsZero || scalar.IsZero())
                return new Float64Vector();

            return scalar.IsOne()
                ? this
                : this.MapScalars(s => s * scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector Divide(double scalar)
        {
            var s1 = 1d / scalar;

            return this.MapScalars(s => s * s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector DivideByENormSquared()
        {
            var scalar = 1d / ENormSquared();

            return this.MapScalars(s => s * scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector DivideByENorm()
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
        public Float64Vector Add(Float64VectorTerm vector2)
        {
            if (IsZero)
                return vector2.ToVector();

            if (vector2.IsZero)
                return this;

            return Float64VectorComposer.Create()
                .SetVector(this)
                .AddTerm(vector2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector Add(Float64Vector vector2)
        {
            if (IsZero)
                return vector2;

            if (vector2.IsZero)
                return this;

            return Float64VectorComposer.Create()
                .SetVector(this)
                .AddVector(vector2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector Subtract(ILinSignedBasisVector vector2)
        {
            if (IsZero)
                return vector2.Negative().ToVector();

            if (vector2.IsZero)
                return this;

            return Float64VectorComposer.Create()
                .SetVector(this)
                .SubtractVector(vector2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector Subtract(Float64VectorTerm vector2)
        {
            if (IsZero)
                return vector2.Negative().ToVector();

            if (vector2.IsZero)
                return this;

            return Float64VectorComposer.Create()
                .SetVector(this)
                .SubtractTerm(vector2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector Subtract(Float64Vector vector2)
        {
            if (IsZero)
                return vector2.Negative();

            if (vector2.IsZero)
                return this;

            return Float64VectorComposer.Create()
                .SetVector(this)
                .SubtractVector(vector2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESp(ILinSignedBasisVector axis)
        {
            return GetTermScalar(axis.Index) * axis.Sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESp(Float64VectorTerm vector2)
        {
            var scalar2 = vector2.ScalarValue;

            return TryGetTermScalar(vector2.BasisVector.Index, out var scalar1)
                ? scalar1 * scalar2
                : 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESp(Float64Vector vector2)
        {
            return Float64ScalarComposer
                .Create()
                .AddESpTerms(this, vector2)
                .ScalarValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector RejectOnVector(ILinSignedBasisVector vector2)
        {
            if (vector2.IsZero)
                return this;

            if (!_indexScalarDictionary.ContainsKey(vector2.Index))
                return this;

            return Float64VectorComposer.Create()
                .SetVector(this)
                .RemoveTerm(vector2.Index)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector RejectOnUnitVector(ILinSignedBasisVector vector2)
        {
            Debug.Assert(vector2.IsNonZero);

            if (vector2.IsZero)
                return this;

            if (!_indexScalarDictionary.ContainsKey(vector2.Index))
                return this;

            return Float64VectorComposer.Create()
                .SetVector(this)
                .RemoveTerm(vector2.Index)
                .GetVector();
        }

        public double[] ToScalarArray()
        {
            var scalarArray = new double[VSpaceDimensions];

            foreach (var (index, scalar) in _indexScalarDictionary)
                scalarArray[index] = scalar;

            return scalarArray;
        }

        public double[] ToScalarArray(int length)
        {
            Debug.Assert(
                length >= VSpaceDimensions
            );

            var scalarArray = new double[length];

            foreach (var (index, scalar) in _indexScalarDictionary)
                scalarArray[index] = scalar;

            return scalarArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<double> ToMathNetVector()
        {
            return Vector<double>.Build.DenseOfArray(
                ToScalarArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<double> ToMathNetVector(int length)
        {
            return Vector<double>.Build.DenseOfArray(
                ToScalarArray(length)
            );
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
                .OrderBy(p => p.Key.Index)
                .Select(p => $"'{p.Value:G}'{p.Key}")
                .Concatenate(" + ");
        }

    }
}