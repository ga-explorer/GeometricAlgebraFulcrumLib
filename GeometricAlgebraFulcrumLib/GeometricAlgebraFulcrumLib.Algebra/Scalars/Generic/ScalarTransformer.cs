using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic
{
    public class ScalarTransformer<T> :
        IReadOnlyList<Func<T, T>>
    {
        private readonly List<Func<T, T>> _mapFunctionList
            = new List<Func<T, T>>();


        public int Count
            => _mapFunctionList.Count;

        public Func<T, T> this[int index]
        {
            get => _mapFunctionList[index];
            set => _mapFunctionList[index] = value ?? throw new ArgumentNullException(nameof(value));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarTransformer<T> Clear()
        {
            _mapFunctionList.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarTransformer<T> Remove(int index)
        {
            _mapFunctionList.RemoveAt(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarTransformer<T> Append(Func<T, T> mapFunc)
        {
            _mapFunctionList.Add(mapFunc);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarTransformer<T> Prepend(Func<T, T> mapFunc)
        {
            _mapFunctionList.Insert(0, mapFunc);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarTransformer<T> Insert(Func<T, T> mapFunc, int index)
        {
            _mapFunctionList.Insert(index, mapFunc);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T MapScalarValue(T inScalar)
        {
            return _mapFunctionList.Aggregate(
                inScalar,
                (scalar, mapFunc) => mapFunc(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T1 MapScalarValue<T1>(T inScalar, Func<T, T1> mapFunc)
        {
            return mapFunc(
                MapScalarValue(inScalar)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> MapScalar(IScalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return processor.ScalarFromValue(
                MapScalarValue(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinPolarAngle<T> MapAngleRadians(LinPolarAngle<T> angle)
        {
            var processor = angle.ScalarProcessor;

            return processor.CreatePolarAngleFromRadians(
                MapScalarValue(angle.RadiansValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinDirectedAngle<T> MapAngleRadians(LinDirectedAngle<T> angle)
        {
            var processor = angle.ScalarProcessor;

            return processor.CreateDirectedAngleFromRadians(
                MapScalarValue(angle.RadiansValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<T> MapComponents(IPair<T> scalarValuePair)
        {
            return new Pair<T>(
                MapScalarValue(scalarValuePair.Item1),
                MapScalarValue(scalarValuePair.Item2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triplet<T> MapComponents(ITriplet<T> scalarValuePair)
        {
            return new Triplet<T>(
                MapScalarValue(scalarValuePair.Item1),
                MapScalarValue(scalarValuePair.Item2),
                MapScalarValue(scalarValuePair.Item3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quad<T> MapComponents(IQuad<T> scalarValuePair)
        {
            return new Quad<T>(
                MapScalarValue(scalarValuePair.Item1),
                MapScalarValue(scalarValuePair.Item2),
                MapScalarValue(scalarValuePair.Item3),
                MapScalarValue(scalarValuePair.Item4)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quint<T> MapComponents(IQuint<T> scalarValuePair)
        {
            return new Quint<T>(
                MapScalarValue(scalarValuePair.Item1),
                MapScalarValue(scalarValuePair.Item2),
                MapScalarValue(scalarValuePair.Item3),
                MapScalarValue(scalarValuePair.Item4),
                MapScalarValue(scalarValuePair.Item5)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Hexad<T> MapComponents(IHexad<T> scalarValuePair)
        {
            return new Hexad<T>(
                MapScalarValue(scalarValuePair.Item1),
                MapScalarValue(scalarValuePair.Item2),
                MapScalarValue(scalarValuePair.Item3),
                MapScalarValue(scalarValuePair.Item4),
                MapScalarValue(scalarValuePair.Item5),
                MapScalarValue(scalarValuePair.Item6)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector2D<T> MapScalars(ILinVector2D<T> vector)
        {
            var processor = vector.ScalarProcessor;

            return processor.Vector2D(
                MapScalarValue(vector.X.ScalarValue),
                MapScalarValue(vector.Y.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector3D<T> MapScalars(ILinVector3D<T> vector)
        {
            var processor = vector.ScalarProcessor;

            return processor.Vector3D(
                MapScalarValue(vector.X.ScalarValue),
                MapScalarValue(vector.Y.ScalarValue),
                MapScalarValue(vector.Z.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector4D<T> MapScalars(ILinVector4D<T> vector)
        {
            var processor = vector.ScalarProcessor;

            return processor.Vector4D(
                MapScalarValue(vector.X.ScalarValue),
                MapScalarValue(vector.Y.ScalarValue),
                MapScalarValue(vector.Z.ScalarValue),
                MapScalarValue(vector.W.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] MapScalars(T[] scalarArray)
        {
            return scalarArray.MapScalars(MapScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[,] MapScalars(T[,] scalarArray)
        {
            return scalarArray.MapScalars(MapScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> MapScalars(XGaScalar<T> mv)
        {
            var processor = mv.Processor;

            return processor.Scalar(
                MapScalarValue(mv.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> MapScalars(XGaVector<T> mv)
        {
            return mv.MapScalars(MapScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> MapScalars(XGaBivector<T> mv)
        {
            return mv.MapScalars(MapScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> MapScalars(XGaHigherKVector<T> mv)
        {
            return mv.MapScalars(MapScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> MapScalars(XGaKVector<T> mv)
        {
            return mv.MapScalars(MapScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> MapScalars(XGaGradedMultivector<T> mv)
        {
            return mv.MapScalars(MapScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivector<T> MapScalars(XGaUniformMultivector<T> mv)
        {
            return mv.MapScalars(MapScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> MapScalars(XGaMultivector<T> mv)
        {
            return mv.MapScalars(MapScalarValue);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<Func<T, T>> GetEnumerator()
        {
            return _mapFunctionList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
