using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class KVectorSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this IGeometricAlgebraProcessor<T> processor, ulong id1, ulong id2)
        {
            return processor.Sp(
                processor.CreateKVectorStorageBasis(id1),
                processor.CreateKVectorStorageBasis(id2)
            ).CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this IGeometricAlgebraProcessor<T> processor, ulong id1, ulong id2)
        {
            return processor.ESp(
                processor.CreateKVectorStorageBasis(id1),
                processor.CreateKVectorStorageBasis(id2)
            ).CreateScalar(processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaKVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.Grade == 0
                ? processor.CreateScalar(processor.Times(v1.AsScalarValue(), v2.ScalarValue))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Scalar<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return v2.Grade == 0
                ? processor.CreateScalar(processor.Times(v1.ScalarValue, v2.AsScalarValue()))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaKVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.Grade == 1
                ? processor.CreateScalar(processor.Sp(v1.AsVectorStorage(), v2.VectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaVector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v2.Grade == 1
                ? processor.CreateScalar(processor.Sp(v1.VectorStorage, v2.AsVectorStorage()))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaKVector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.Grade == 2
                ? processor.CreateScalar(processor.Sp(v1.AsBivectorStorage(), v2.BivectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaBivector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v2.Grade == 2
                ? processor.CreateScalar(processor.Sp(v1.BivectorStorage, v2.AsBivectorStorage()))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaKVector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.Grade == v2.Grade
                ? processor.CreateScalar(processor.Sp(v1.KVectorStorage, v2.KVectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> SpSquared<T>(this GaKVector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return v1.GeometricProcessor.CreateScalar(processor.SpSquared(v1.KVectorStorage));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaKVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.Grade == 0
                ? processor.CreateScalar(processor.Times(v1.AsScalarValue(), v2.ScalarValue))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Scalar<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return v2.Grade == 0
                ? processor.CreateScalar(processor.Times(v1.ScalarValue, v2.AsScalarValue()))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaKVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.Grade == 1
                ? processor.CreateScalar(processor.ESp(v1.AsVectorStorage(), v2.VectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaVector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v2.Grade == 1
                ? processor.CreateScalar(processor.ESp(v1.VectorStorage, v2.AsVectorStorage()))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaKVector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.Grade == 2
                ? processor.CreateScalar(processor.ESp(v1.AsBivectorStorage(), v2.BivectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaBivector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v2.Grade == 2
                ? processor.CreateScalar(processor.ESp(v1.BivectorStorage, v2.AsBivectorStorage()))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaKVector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.Grade == v2.Grade
                ? processor.CreateScalar(processor.ESp(v1.KVectorStorage, v2.KVectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaKVector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return v1.GeometricProcessor.CreateScalar(processor.ESp(v1.KVectorStorage));
        }
    }
}