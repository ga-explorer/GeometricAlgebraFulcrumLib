using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Multivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetScalar(out var scalar)
                ? processor.CreateScalar(processor.Times(scalar, v2.ScalarValue))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Scalar<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return v2.MultivectorStorage.TryGetScalar(out var scalar)
                ? processor.CreateScalar(processor.Times(v1.ScalarValue, scalar))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Multivector<T> v1, Vector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetVectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.Sp(vectorStorage, v2.VectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Vector<T> v1, Multivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v2.MultivectorStorage.TryGetVectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.Sp(v1.VectorStorage, vectorStorage))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Multivector<T> v1, Bivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetBivectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.Sp(vectorStorage, v2.BivectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Bivector<T> v1, Multivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v2.MultivectorStorage.TryGetBivectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.Sp(v1.BivectorStorage, vectorStorage))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Multivector<T> v1, KVector<T> v2)
        {
            var grade = v2.Grade;
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetKVectorPart(grade, out var vectorStorage)
                ? processor.CreateScalar(processor.Sp(vectorStorage, v2.KVectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this KVector<T> v1, Multivector<T> v2)
        {
            var grade = v1.Grade;
            var processor = v1.GeometricProcessor;

            return v2.MultivectorStorage.TryGetKVectorPart(grade, out var vectorStorage)
                ? processor.CreateScalar(processor.Sp(v1.KVectorStorage, vectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Multivector<T> v1, Multivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return processor.CreateScalar(
                processor.Sp(
                    v1.MultivectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Multivector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return v1.GeometricProcessor.CreateScalar(processor.Sp(v1.MultivectorStorage));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Multivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetScalar(out var scalar)
                ? processor.CreateScalar(processor.Times(scalar, v2.ScalarValue))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Scalar<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return v2.MultivectorStorage.TryGetScalar(out var scalar)
                ? processor.CreateScalar(processor.Times(v1.ScalarValue, scalar))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Multivector<T> v1, Vector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetVectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.ESp(vectorStorage, v2.VectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Vector<T> v1, Multivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v2.MultivectorStorage.TryGetVectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.ESp(v1.VectorStorage, vectorStorage))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Multivector<T> v1, Bivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetBivectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.ESp(vectorStorage, v2.BivectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Bivector<T> v1, Multivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v2.MultivectorStorage.TryGetBivectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.ESp(v1.BivectorStorage, vectorStorage))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Multivector<T> v1, KVector<T> v2)
        {
            var grade = v2.Grade;
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetKVectorPart(grade, out var vectorStorage)
                ? processor.CreateScalar(processor.ESp(vectorStorage, v2.KVectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this KVector<T> v1, Multivector<T> v2)
        {
            var grade = v1.Grade;
            var processor = v1.GeometricProcessor;

            return v2.MultivectorStorage.TryGetKVectorPart(grade, out var vectorStorage)
                ? processor.CreateScalar(processor.ESp(v1.KVectorStorage, vectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Multivector<T> v1, Multivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return processor.CreateScalar(
                processor.ESp(
                    v1.MultivectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Multivector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return v1.GeometricProcessor.CreateScalar(processor.ESp(v1.MultivectorStorage));
        }
    }
}