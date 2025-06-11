using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors
{
    public partial class XGaProcessor<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> CreateScalarComposer()
        {
            return new XGaKVectorComposer<T>(this, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> CreateVectorComposer()
        {
            return new XGaKVectorComposer<T>(this, 1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> CreateBivectorComposer()
        {
            return new XGaKVectorComposer<T>(this, 2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> CreateTrivectorComposer()
        {
            return new XGaKVectorComposer<T>(this, 3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> CreateKVectorComposer(int grade)
        {
            return new XGaKVectorComposer<T>(this, grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> CreateMultivectorComposer()
        {
            return new XGaGradedMultivectorComposer<T>(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> CreateUniformComposer()
        {
            return new XGaUniformMultivectorComposer<T>(this);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Scalar(int scalarValue)
        {
            return new XGaScalar<T>(
                this,
                ScalarProcessor.ValueFromNumber(scalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Scalar(uint scalarValue)
        {
            return new XGaScalar<T>(
                this,
                ScalarProcessor.ValueFromNumber(scalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Scalar(long scalarValue)
        {
            return new XGaScalar<T>(
                this,
                ScalarProcessor.ValueFromNumber(scalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Scalar(ulong scalarValue)
        {
            return new XGaScalar<T>(
                this,
                ScalarProcessor.ValueFromNumber(scalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Scalar(float scalarValue)
        {
            return new XGaScalar<T>(
                this,
                ScalarProcessor.ValueFromNumber(scalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Scalar(double scalarValue)
        {
            return new XGaScalar<T>(
                this,
                ScalarProcessor.ValueFromNumber(scalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Scalar(string scalarValue)
        {
            return new XGaScalar<T>(
                this,
                ScalarProcessor.ValueFromText(scalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Scalar(T scalarValue)
        {
            return new XGaScalar<T>(this, scalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Scalar(Scalar<T> scalar)
        {
            return new XGaScalar<T>(this, scalar.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Scalar(IScalar<T> scalar)
        {
            return new XGaScalar<T>(this, scalar.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ScalarFromSum(T scalar1, T scalar2)
        {
            return new XGaScalar<T>(
                this,
                ScalarProcessor.Add(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ScalarFromSum(params T[] scalarValueList)
        {
            var scalar = ScalarProcessor.ZeroValue;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(ScalarProcessor.IsValid(scalarValue));

                if (ScalarProcessor.IsZero(scalarValue))
                    continue;

                scalar = ScalarProcessor.Add(scalar, scalarValue).ScalarValue;
            }

            return new XGaScalar<T>(this, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ScalarFromSum(IEnumerable<T> scalarValueList)
        {
            var scalar = ScalarProcessor.ZeroValue;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(ScalarProcessor.IsValid(scalarValue));

                if (ScalarProcessor.IsZero(scalarValue))
                    continue;

                scalar = ScalarProcessor.Add(scalar, scalarValue).ScalarValue;
            }

            return new XGaScalar<T>(this, scalar);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ScalarFromProduct(T scalar1, int scalar2)
        {
            return new XGaScalar<T>(
                this,
                ScalarProcessor.Times(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ScalarFromProduct(T scalar1, T scalar2)
        {
            return new XGaScalar<T>(
                this,
                ScalarProcessor.Times(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ScalarFromProduct(int sign, T scalar1, T scalar2)
        {
            return new XGaScalar<T>(
                this,
                ScalarProcessor.Times(sign, scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ScalarFromProduct(params T[] scalarValueList)
        {
            var scalar = ScalarProcessor.OneValue;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(ScalarProcessor.IsValid(scalarValue));

                if (ScalarProcessor.IsZero(scalarValue))
                    return new XGaScalar<T>(this);

                scalar = ScalarProcessor.Times(scalar, scalarValue).ScalarValue;
            }

            return new XGaScalar<T>(this, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ScalarFromProduct(IEnumerable<T> scalarValueList)
        {
            var scalar = ScalarProcessor.OneValue;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(ScalarProcessor.IsValid(scalarValue));

                if (ScalarProcessor.IsZero(scalarValue))
                    return new XGaScalar<T>(this);

                scalar = ScalarProcessor.Times(scalar, scalarValue).ScalarValue;
            }

            return new XGaScalar<T>(this, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Dictionary<IndexSet, T> CreateValidVectorDictionary(IEnumerable<T> scalarList)
        {
            var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

            var index = 0;
            foreach (var scalar in scalarList)
            {
                if (!ScalarProcessor.IsValid(scalar))
                    throw new InvalidOperationException();

                if (!ScalarProcessor.IsZero(scalar))
                    basisScalarDictionary.Add(index.ToUnitIndexSet(), scalar);

                index++;
            }

            return basisScalarDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Dictionary<IndexSet, T> CreateValidVectorDictionary(IEnumerable<Scalar<T>> scalarList)
        {
            var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

            var index = 0;
            foreach (var scalar in scalarList)
            {
                if (!scalar.IsValid())
                    throw new InvalidOperationException();

                if (!scalar.IsZero())
                    basisScalarDictionary.Add(index.ToUnitIndexSet(), scalar.ScalarValue);

                index++;
            }

            return basisScalarDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Dictionary<IndexSet, T> CreateValidVectorDictionary(IEnumerable<IScalar<T>> scalarList)
        {
            var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

            var index = 0;
            foreach (var scalar in scalarList)
            {
                if (!scalar.IsValid())
                    throw new InvalidOperationException();

                if (!scalar.IsZero())
                    basisScalarDictionary.Add(index.ToUnitIndexSet(), scalar.ScalarValue);

                index++;
            }

            return basisScalarDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> Vector(IReadOnlyDictionary<IndexSet, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, T>)
                return VectorZero;

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, T>)
                return VectorTerm(basisScalarDictionary.First());

            return new XGaVector<T>(this, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> Vector(IReadOnlyDictionary<int, T> basisScalarDictionary)
        {
            return new XGaVector<T>(this, basisScalarDictionary.CreateVectorDictionary());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> Vector(params double[] scalarArray)
        {
            var scalarDictionary = CreateValidVectorDictionary(
                scalarArray.Select(
                    text => ScalarProcessor.ScalarFromNumber(text)
                )
            );

            return Vector(
                scalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> Vector(params string[] scalarArray)
        {
            var scalarDictionary = CreateValidVectorDictionary(
                scalarArray.Select(
                    text => ScalarProcessor.ScalarFromText(text)
                )
            );

            return Vector(
                scalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> Vector(params T[] scalarArray)
        {
            var scalarDictionary = CreateValidVectorDictionary(scalarArray);

            return Vector(
                scalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> Vector(params Scalar<T>[] scalarArray)
        {
            var scalarDictionary = CreateValidVectorDictionary(scalarArray);

            return Vector(
                scalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> Vector(params IScalar<T>[] scalarArray)
        {
            var scalarDictionary = CreateValidVectorDictionary(scalarArray);

            return Vector(
                scalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> Vector(IEnumerable<T> scalarList)
        {
            var scalarDictionary = CreateValidVectorDictionary(scalarList);

            return Vector(
                scalarDictionary
            );
        }


        public XGaVector<T> Vector(int termsCount, Func<int, double> indexToScalarFunc)
        {
            var composer = CreateVectorComposer();

            for (var index = 0; index < termsCount; index++)
            {
                var scalar = indexToScalarFunc(index);

                composer.SetVectorTerm(index, ScalarProcessor.ValueFromNumber(scalar));
            }

            return composer.GetVector();
        }

        public XGaVector<T> Vector(int termsCount, Func<int, string> indexToScalarFunc)
        {
            var composer = CreateVectorComposer();

            for (var index = 0; index < termsCount; index++)
            {
                var scalar = indexToScalarFunc(index);

                composer.SetVectorTerm(index, ScalarProcessor.ValueFromText(scalar));
            }

            return composer.GetVector();
        }

        public XGaVector<T> Vector(int termsCount, Func<int, T> indexToScalarFunc)
        {
            var composer = CreateVectorComposer();

            for (var index = 0; index < termsCount; index++)
            {
                var scalar = indexToScalarFunc(index);

                composer.SetVectorTerm(index, scalar);
            }

            return composer.GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorTerm(int index)
        {
            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, T>(
                    index.ToUnitIndexSet(),
                    ScalarProcessor.OneValue
                );

            return new XGaVector<T>(this, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorTerm(int index, T scalar)
        {
            if (ScalarProcessor.IsZero(scalar))
                return new XGaVector<T>(this);

            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, T>(
                    index.ToUnitIndexSet(),
                    scalar
                );

            return new XGaVector<T>(this, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorTerm(int index, Scalar<T> scalar)
        {
            if (scalar.IsZero())
                return new XGaVector<T>(this);

            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, T>(
                    index.ToUnitIndexSet(),
                    scalar.ScalarValue
                );

            return new XGaVector<T>(this, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorTerm(KeyValuePair<int, T> indexScalarPair)
        {
            return VectorTerm(indexScalarPair.Key, indexScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorTerm(ulong basisVectorId)
        {
            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, T>(basisVectorId.ToUInt64IndexSet(), ScalarProcessor.OneValue);

            return new XGaVector<T>(this, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorTerm(ulong basisVectorId, T scalar)
        {
            if (ScalarProcessor.IsZero(scalar))
                return new XGaVector<T>(this);

            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, T>(basisVectorId.ToUInt64IndexSet(), scalar);

            return new XGaVector<T>(this, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorTerm(ulong basisVectorId, Scalar<T> scalar)
        {
            if (scalar.IsZero())
                return new XGaVector<T>(this);

            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, T>(
                    basisVectorId.ToUInt64IndexSet(),
                    scalar.ScalarValue
                );

            return new XGaVector<T>(this, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorTerm(KeyValuePair<ulong, T> idScalarPair)
        {
            return VectorTerm(idScalarPair.Key.ToUInt64IndexSet(), idScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorTerm(IndexSet basisVectorId)
        {
            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, T>(basisVectorId, ScalarProcessor.OneValue);

            return new XGaVector<T>(this, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorTerm(IndexSet basisVectorId, T scalar)
        {
            if (ScalarProcessor.IsZero(scalar))
                return new XGaVector<T>(this);

            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, T>(basisVectorId, scalar);

            return new XGaVector<T>(this, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorTerm(IndexSet basisVectorId, Scalar<T> scalar)
        {
            if (scalar.IsZero())
                return new XGaVector<T>(this);

            var basisScalarDictionary =
                new SingleItemDictionary<IndexSet, T>(
                    basisVectorId,
                    scalar.ScalarValue
                );

            return new XGaVector<T>(this, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorTerm(KeyValuePair<IndexSet, T> idScalarPair)
        {
            return VectorTerm(idScalarPair.Key, idScalarPair.Value);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorSymmetric(int count)
        {
            return VectorSymmetric(

                count,
                ScalarProcessor.OneValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorSymmetric(int count, T scalarValue)
        {
            return count switch
            {
                < 0 => throw new InvalidOperationException(),

                0 => new XGaVector<T>(
                    this,
                    new EmptyDictionary<IndexSet, T>()
                ),

                1 => new XGaVector<T>(
                    this,
                    new SingleItemDictionary<IndexSet, T>(0.ToUnitIndexSet(), scalarValue)
                ),

                _ => new XGaVector<T>(
                    this,
                    new XGaRepeatedScalarVectorDictionary<T>(count, scalarValue)
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorSymmetricUnit(int count)
        {
            return VectorSymmetric(
                count,
                ScalarProcessor.Sqrt(count).Inverse().ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorUnit(LinAngle<T> angle, int index1, int index2)
        {
            Debug.Assert(index2 > index1);

            var (scalar1, scalar2) = angle;

            return CreateVectorComposer()
                .SetVectorTerm(index1, scalar1)
                .SetVectorTerm(index2, scalar2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorPhasor(T magnitude, LinAngle<T> angle, int index1, int index2)
        {
            Debug.Assert(index2 > index1);

            Debug.Assert(magnitude != null, nameof(magnitude) + " != null");

            var scalar1 = magnitude * angle.Cos();
            var scalar2 = magnitude * angle.Sin();

            return CreateVectorComposer()
                .SetVectorTerm(index1, scalar1)
                .SetVectorTerm(index2, scalar2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> VectorPhasor(IScalar<T> magnitude, LinAngle<T> angle, int index1, int index2)
        {
            Debug.Assert(index2 > index1);

            var scalar1 = magnitude.Times(angle.Cos());
            var scalar2 = magnitude.Times(angle.Sin());

            return CreateVectorComposer()
                .SetVectorTerm(index1, scalar1)
                .SetVectorTerm(index2, scalar2)
                .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> Vector(LinVector<T> vector)
        {
            var idScalarDictionary =
                vector.GetIndexScalarDictionary().ToDictionary(
                    p => p.Key.ToUnitIndexSet(),
                    p => p.Value
                );

            return Vector(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> Vector(ILinFloat64Vector2D vector)
        {
            return CreateVectorComposer()
                .SetVectorTerm(0, ScalarProcessor.ScalarFromNumber(vector.X))
                .SetVectorTerm(1, ScalarProcessor.ScalarFromNumber(vector.Y))
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> Vector(ILinFloat64Vector3D vector)
        {
            return CreateVectorComposer()
                .SetVectorTerm(0, ScalarProcessor.ScalarFromNumber(vector.X))
                .SetVectorTerm(1, ScalarProcessor.ScalarFromNumber(vector.Y))
                .SetVectorTerm(2, ScalarProcessor.ScalarFromNumber(vector.Z))
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> Vector(ILinFloat64Vector4D vector)
        {
            return CreateVectorComposer()
                .SetVectorTerm(0, ScalarProcessor.ScalarFromNumber(vector.X))
                .SetVectorTerm(1, ScalarProcessor.ScalarFromNumber(vector.Y))
                .SetVectorTerm(2, ScalarProcessor.ScalarFromNumber(vector.Z))
                .SetVectorTerm(3, ScalarProcessor.ScalarFromNumber(vector.W))
                .GetVector();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> Bivector(IReadOnlyDictionary<IndexPair, T> basisScalarDictionary)
        {
            return new XGaBivector<T>(
                this,
                basisScalarDictionary.CreateBivectorDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> Bivector(IReadOnlyDictionary<Int32Pair, T> basisScalarDictionary)
        {
            return new XGaBivector<T>(
                this,
                basisScalarDictionary.CreateBivectorDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> Bivector(IReadOnlyDictionary<IndexSet, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, T>)
                return BivectorZero;

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, T>)
                return BivectorTerm(basisScalarDictionary.First());

            return new XGaBivector<T>(
                this,
                basisScalarDictionary
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> BivectorTerm(IPair<int> indexPair)
        {
            var index1 = indexPair.Item1;
            var index2 = indexPair.Item2;

            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            return new XGaBivector<T>(
                this,

                new SingleItemDictionary<IndexSet, T>(
                    indexPair.ToPairIndexSet(),
                    ScalarProcessor.OneValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> BivectorTerm(IPair<int> indexPair, T scalar)
        {
            var index1 = indexPair.Item1;
            var index2 = indexPair.Item2;

            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            if (ScalarProcessor.IsZero(scalar))
                return new XGaBivector<T>(this);

            return new XGaBivector<T>(
                this,

                new SingleItemDictionary<IndexSet, T>(
                    indexPair.ToPairIndexSet(),
                    scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> BivectorTerm(int index1, int index2)
        {
            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            return new XGaBivector<T>(
                this,

                new SingleItemDictionary<IndexSet, T>(
                    IndexSet.CreatePair(index1, index2),
                    ScalarProcessor.OneValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> BivectorTerm(int index1, int index2, T scalar)
        {
            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            if (ScalarProcessor.IsZero(scalar))
                return new XGaBivector<T>(this);

            return new XGaBivector<T>(
                this,

                new SingleItemDictionary<IndexSet, T>(
                    IndexSet.CreatePair(index1, index2),
                    scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> BivectorTerm(int index1, int index2, IScalar<T> scalar)
        {
            return BivectorTerm(index1, index2, scalar.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> BivectorTerm(KeyValuePair<Int32Pair, T> indexScalarPair)
        {
            return BivectorTerm(

                indexScalarPair.Key,
                indexScalarPair.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> BivectorTerm(KeyValuePair<IndexSet, T> indexScalarPair)
        {
            return BivectorTerm(indexScalarPair.Key, indexScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> BivectorTerm(IndexSet basisBlade)
        {
            return new XGaBivector<T>(
                this,

                new SingleItemDictionary<IndexSet, T>(basisBlade, ScalarProcessor.OneValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> BivectorTerm(IndexSet basisBlade, T scalar)
        {
            if (ScalarProcessor.IsZero(scalar))
                return new XGaBivector<T>(this);

            return new XGaBivector<T>(
                this,

                new SingleItemDictionary<IndexSet, T>(basisBlade, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> BivectorTerm(IndexSet basisBlade, Scalar<T> scalar)
        {
            if (scalar.IsZero())
                return new XGaBivector<T>(this);

            return new XGaBivector<T>(
                this,
                new SingleItemDictionary<IndexSet, T>(basisBlade, scalar.ScalarValue)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> Bivector2D(double scalar01)
        {
            return CreateBivectorComposer()
                .SetBivectorTerm(0, 1, scalar01)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> Bivector2D(string scalar01)
        {
            return CreateBivectorComposer()
                .SetBivectorTerm(0, 1, scalar01)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> Bivector2D(T scalar01)
        {
            return CreateBivectorComposer()
                .SetBivectorTerm(0, 1, scalar01)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> Bivector3D(double scalar01, double scalar02, double scalar12)
        {
            return CreateBivectorComposer()
                .SetBivectorTerm(0, 1, scalar01)
                .SetBivectorTerm(0, 2, scalar02)
                .SetBivectorTerm(1, 2, scalar12)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> Bivector3D(string scalar01, string scalar02, string scalar12)
        {
            return CreateBivectorComposer()
                .SetBivectorTerm(0, 1, scalar01)
                .SetBivectorTerm(0, 2, scalar02)
                .SetBivectorTerm(1, 2, scalar12)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> Bivector3D(T scalar01, T scalar02, T scalar12)
        {
            return CreateBivectorComposer()
                .SetBivectorTerm(0, 1, scalar01)
                .SetBivectorTerm(0, 2, scalar02)
                .SetBivectorTerm(1, 2, scalar12)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> Bivector3D(LinFloat64Bivector3D bivector)
        {
            return CreateBivectorComposer()
                .SetBivectorTerm(0, 1, bivector.Xy)
                .SetBivectorTerm(0, 2, bivector.Xz)
                .SetBivectorTerm(1, 2, bivector.Yz)
                .GetBivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> HigherKVectorZero(int grade)
        {
            return new XGaHigherKVector<T>(this, grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> HigherKVectorTerm(IndexSet id)
        {
            var grade = id.Count;

            return new XGaHigherKVector<T>(
                this,
                grade,
                new SingleItemDictionary<IndexSet, T>(id, ScalarProcessor.OneValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> HigherKVectorTerm(IndexSet id, T scalar)
        {
            var grade = id.Count;

            return ScalarProcessor.IsZero(scalar)
                ? new XGaHigherKVector<T>(this, grade)
                : new XGaHigherKVector<T>(this, grade, new SingleItemDictionary<IndexSet, T>(id, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> HigherKVectorTerm(IndexSet id, IScalar<T> scalar)
        {
            return HigherKVectorTerm(id, scalar.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> HigherKVectorTerm(KeyValuePair<IndexSet, T> term)
        {
            var (id, scalar) = term;

            var grade = id.Count;

            return ScalarProcessor.IsZero(scalar)
                ? new XGaHigherKVector<T>(this, grade)
                : new XGaHigherKVector<T>(this, grade, new SingleItemDictionary<IndexSet, T>(id, scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> HigherKVector(int grade, IReadOnlyDictionary<IndexSet, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, T>)
                return HigherKVectorZero(grade);

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, T>)
                return HigherKVectorTerm(basisScalarDictionary.First());

            return new XGaHigherKVector<T>(
                this,

                grade,
                basisScalarDictionary
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> KVectorZero(int grade)
        {
            if (grade < 0)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return grade switch
            {
                0 => ScalarZero,
                1 => VectorZero,
                2 => BivectorZero,
                _ => new XGaHigherKVector<T>(this, grade)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> KVectorTerm(KeyValuePair<IndexSet, T> term)
        {
            var grade = term.Key.Count;

            return grade switch
            {
                0 => new XGaScalar<T>(this, term.Value),
                1 => new XGaVector<T>(this, term),
                2 => new XGaBivector<T>(this, term),
                _ => new XGaHigherKVector<T>(this, term)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> KVectorTerm(IndexSet basisBlade)
        {
            return KVectorTerm(

                new KeyValuePair<IndexSet, T>(basisBlade, ScalarProcessor.OneValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> KVectorTerm(IndexSet basisBlade, T scalar)
        {
            var grade = basisBlade.Count;

            if (ScalarProcessor.IsZero(scalar))
                return KVectorZero(grade);

            return KVectorTerm(

                new KeyValuePair<IndexSet, T>(basisBlade, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> KVectorTerm(IndexSet basisBlade, string scalar)
        {
            return KVectorTerm(basisBlade, ScalarProcessor.ScalarFromText(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> KVectorTerm(IndexSet basisBlade, IScalar<T> scalar)
        {
            return KVectorTerm(basisBlade, scalar.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> KVector(int grade, IReadOnlyDictionary<IndexSet, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, T>)
                return KVectorZero(grade);

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, T>)
                return KVectorTerm(basisScalarDictionary.First());

            return grade switch
            {
                0 => new XGaScalar<T>(this, basisScalarDictionary),
                1 => new XGaVector<T>(this, basisScalarDictionary),
                2 => new XGaBivector<T>(this, basisScalarDictionary),
                _ => new XGaHigherKVector<T>(this, grade, basisScalarDictionary)
            };
        }

        public XGaKVector<T> KVectorTerm(IReadOnlyList<int> basisVectorIndexList)
        {
            var id = basisVectorIndexList.ToIndexSet(false);
            var grade = id.Grade();

            if (grade == 0)
                return ScalarOne;

            var idScalarDictionary =
                new SingleItemDictionary<IndexSet, T>(id, ScalarProcessor.OneValue);

            if (grade == 1)
                return Vector(idScalarDictionary);

            if (grade == 2)
                return Bivector(idScalarDictionary);

            return new XGaHigherKVector<T>(
                this,
                grade,
                idScalarDictionary
            );
        }

        public XGaKVector<T> KVectorTerm(IReadOnlyList<int> basisVectorIndexList, T scalar)
        {
            var id = basisVectorIndexList.ToIndexSet(false);
            var grade = id.Grade();

            if (ScalarProcessor.IsZero(scalar))
                return ScalarZero;

            if (grade == 0)
                return Scalar(scalar);

            var idScalarDictionary =
                new SingleItemDictionary<IndexSet, T>(id, scalar);

            if (grade == 1)
                return Vector(idScalarDictionary);

            if (grade == 2)
                return Bivector(idScalarDictionary);

            return new XGaHigherKVector<T>(
                this,
                grade,
                idScalarDictionary
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> PseudoScalar(int vSpaceDimensions)
        {
            var id = GetBasisPseudoScalarId(vSpaceDimensions);

            return KVectorTerm(

                new KeyValuePair<IndexSet, T>(id, ScalarProcessor.OneValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> PseudoScalar(int vSpaceDimensions, T scalarValue)
        {
            var id = GetBasisPseudoScalarId(vSpaceDimensions);

            return KVectorTerm(

                new KeyValuePair<IndexSet, T>(id, scalarValue)
            );
        }

        public XGaKVector<T> PseudoScalarReverse(int vSpaceDimensions)
        {
            var id =
                GetBasisPseudoScalarId(vSpaceDimensions);

            var scalar =
                vSpaceDimensions.ReverseIsNegativeOfGrade()
                    ? ScalarProcessor.MinusOneValue
                    : ScalarProcessor.OneValue;

            return KVectorTerm(

                new KeyValuePair<IndexSet, T>(id, scalar)
            );
        }

        public XGaKVector<T> PseudoScalarConjugate(int vSpaceDimensions)
        {
            var id =
                GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                HermitianConjugateSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            var scalar = sign.ValueFromNumber(ScalarProcessor);

            return KVectorTerm(
                new KeyValuePair<IndexSet, T>(id, scalar)
            );
        }

        public XGaKVector<T> PseudoScalarEInverse(int vSpaceDimensions)
        {
            var id =
                GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                EGpSquaredSign(id);

            var scalar = sign.ValueFromNumber(ScalarProcessor);

            return KVectorTerm(

                new KeyValuePair<IndexSet, T>(id, scalar)
            );
        }

        public XGaKVector<T> PseudoScalarInverse(int vSpaceDimensions)
        {
            var id =
                GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                GpSquaredSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            var scalar = sign.ValueFromNumber(ScalarProcessor);

            return KVectorTerm(

                new KeyValuePair<IndexSet, T>(id, scalar)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> SpanToBlade(IEnumerable<XGaVector<T>> mvList)
        {
            XGaKVector<T> blade = ScalarOne;

            foreach (var vector in mvList)
            {
                var newBlade = blade.Op(vector);

                if (newBlade.IsNearZero())
                    continue;

                blade = newBlade;
            }

            return blade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> Op(IEnumerable<XGaVector<T>> mvList)
        {
            XGaKVector<T> blade = ScalarOne;
        
            foreach (var vector in mvList)
            {
                var newBlade = blade.Op(vector);

                if (newBlade.IsZero)
                    return ScalarZero;

                blade = newBlade;
            }

            return blade;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> GradedMultivector(IReadOnlyDictionary<IndexSet, T> termList)
        {
            return CreateMultivectorComposer()
                .SetTerms(termList)
                .GetGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> GradedMultivector(IReadOnlyDictionary<int, XGaKVector<T>> gradeKVectorDictionary)
        {
            if (gradeKVectorDictionary.Count == 0 && gradeKVectorDictionary is not EmptyDictionary<int, XGaKVector<T>>)
                return GradedMultivectorZero;

            if (gradeKVectorDictionary.Count == 1 && gradeKVectorDictionary is not SingleItemDictionary<int, XGaKVector<T>>)
                return gradeKVectorDictionary.Values.First().ToGradedMultivector();

            return new XGaGradedMultivector<T>(this, gradeKVectorDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> GradedMultivector(IEnumerable<KeyValuePair<IndexSet, T>> termList)
        {
            return CreateMultivectorComposer()
                .AddTerms(termList)
                .GetGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> GradedMultivector(IndexSet id)
        {
            var grade = id.Count;

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaKVector<T>>(
                grade,
                KVectorTerm(id, ScalarProcessor.OneValue)
            );

            return new XGaGradedMultivector<T>(
                this,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> GradedMultivector(IndexSet id, T scalar)
        {
            var grade = id.Count;

            if (ScalarProcessor.IsZero(scalar))
                return GradedMultivectorZero;

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaKVector<T>>(
                grade,
                KVectorTerm(id, scalar)
            );

            return new XGaGradedMultivector<T>(
                this,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> GradedMultivector(IndexSet id, IScalar<T> scalar)
        {
            return GradedMultivector(id, scalar.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> GradedMultivector(KeyValuePair<IndexSet, T> basisScalarPair)
        {
            var (id, scalar) = basisScalarPair;
            var grade = id.Count;

            if (ScalarProcessor.IsZero(scalar))
                return GradedMultivectorZero;

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaKVector<T>>(
                grade,
                KVectorTerm(basisScalarPair)
            );

            return new XGaGradedMultivector<T>(
                this,
                gradeKVectorDictionary
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Multivector2D(T scalar, T vectorScalar0, T vectorScalar1, T bivectorScalar)
        {
            return CreateMultivectorComposer()
                .SetScalarTerm(scalar)
                .SetVectorTerm(0, vectorScalar0)
                .SetVectorTerm(1, vectorScalar1)
                .SetBivectorTerm(0, 1, bivectorScalar)
                .GetGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Multivector2D(IScalar<T> scalar, IScalar<T> vectorScalar0, IScalar<T> vectorScalar1, IScalar<T> bivectorScalar)
        {
            return CreateMultivectorComposer()
                .SetScalarTerm(scalar)
                .SetVectorTerm(0, vectorScalar0)
                .SetVectorTerm(1, vectorScalar1)
                .SetBivectorTerm(0, 1, bivectorScalar)
                .GetGradedMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivector<T> UniformMultivector(IReadOnlyDictionary<IndexSet, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, T>)
                return UniformMultivectorZero;

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, T>)
                return UniformMultivector(basisScalarDictionary.First());

            return new XGaUniformMultivector<T>(
                this,
                basisScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivector<T> UniformMultivector(IndexSet basisBlade)
        {
            return new XGaUniformMultivector<T>(this,
                new SingleItemDictionary<IndexSet, T>(
                    basisBlade,
                    ScalarProcessor.OneValue
                ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivector<T> UniformMultivector(IndexSet basisBlade, T scalar)
        {
            if (ScalarProcessor.IsZero(scalar))
                return new XGaUniformMultivector<T>(this);

            return new XGaUniformMultivector<T>(this,
                new SingleItemDictionary<IndexSet, T>(
                    basisBlade,
                    scalar
                ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivector<T> UniformMultivector(KeyValuePair<IndexSet, T> basisScalarPair)
        {
            return UniformMultivector(
                basisScalarPair.Key,
                basisScalarPair.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivector<T> UniformMultivector(IndexSet basisBlade, Scalar<T> scalar)
        {
            var scalarProcessor = scalar.ScalarProcessor;

            if (scalarProcessor.IsZero(scalar.ScalarValue))
                return new XGaUniformMultivector<T>(this);

            return new XGaUniformMultivector<T>(
                this,
                new SingleItemDictionary<IndexSet, T>(
                    basisBlade,
                    scalar.ScalarValue
                ));
        }


        protected Dictionary<int, XGaKVector<T>> SumToGradeKVectorDictionary(IEnumerable<XGaKVector<T>> kVectorList)
        {
            var gradeKVectorDictionary = new Dictionary<int, XGaKVector<T>>();

            foreach (var kVector in kVectorList)
            {
                if (kVector.IsZero) continue;

                var grade = kVector.Grade;

                if (gradeKVectorDictionary.TryGetValue(grade, out var kv1))
                {
                    var kv2 = kv1.AddSameGrade(kVector);

                    if (kv2.IsZero)
                        gradeKVectorDictionary.Remove(grade);
                    else
                        gradeKVectorDictionary[grade] = kv2;

                    continue;
                }

                gradeKVectorDictionary.Add(grade, kVector);
            }

            return gradeKVectorDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> MultivectorFromSum(IEnumerable<XGaKVector<T>> kVectorList)
        {
            var gradeKVectorDictionary =
                SumToGradeKVectorDictionary(kVectorList);

            return gradeKVectorDictionary.Count switch
            {
                0 => ScalarZero,
                1 => gradeKVectorDictionary.Values.First(),
                _ => new XGaGradedMultivector<T>(
                    this,
                    gradeKVectorDictionary
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> GradedMultivectorFromSum(IEnumerable<XGaKVector<T>> kVectorList)
        {
            var gradeKVectorDictionary =
                SumToGradeKVectorDictionary(kVectorList);

            return gradeKVectorDictionary.Count switch
            {
                0 => GradedMultivectorZero,

                1 => new XGaGradedMultivector<T>(
                    this,
                    new SingleItemDictionary<int, XGaKVector<T>>(
                        gradeKVectorDictionary.First()
                    )
                ),

                _ => new XGaGradedMultivector<T>(
                    this,
                    gradeKVectorDictionary
                )
            };
        }

    }
}
