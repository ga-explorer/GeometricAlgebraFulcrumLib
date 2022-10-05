using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaMultivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetScalar(out var scalar)
                ? processor.CreateScalar(processor.Times(scalar, v2.ScalarValue))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Scalar<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return v2.MultivectorStorage.TryGetScalar(out var scalar)
                ? processor.CreateScalar(processor.Times(v1.ScalarValue, scalar))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaMultivector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetVectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.Sp(vectorStorage, v2.VectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v2.MultivectorStorage.TryGetVectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.Sp(v1.VectorStorage, vectorStorage))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaMultivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetBivectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.Sp(vectorStorage, v2.BivectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaBivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v2.MultivectorStorage.TryGetBivectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.Sp(v1.BivectorStorage, vectorStorage))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaMultivector<T> v1, GaKVector<T> v2)
        {
            var grade = v2.Grade;
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetKVectorPart(grade, out var vectorStorage)
                ? processor.CreateScalar(processor.Sp(vectorStorage, v2.KVectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaKVector<T> v1, GaMultivector<T> v2)
        {
            var grade = v1.Grade;
            var processor = v1.GeometricProcessor;

            return v2.MultivectorStorage.TryGetKVectorPart(grade, out var vectorStorage)
                ? processor.CreateScalar(processor.Sp(v1.KVectorStorage, vectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaMultivector<T> v1, GaMultivector<T> v2)
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
        public static Scalar<T> SpSquared<T>(this GaMultivector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return v1.GeometricProcessor.CreateScalar(processor.SpSquared(v1.MultivectorStorage));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaMultivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetScalar(out var scalar)
                ? processor.CreateScalar(processor.Times(scalar, v2.ScalarValue))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Scalar<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return v2.MultivectorStorage.TryGetScalar(out var scalar)
                ? processor.CreateScalar(processor.Times(v1.ScalarValue, scalar))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaMultivector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetVectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.ESp(vectorStorage, v2.VectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v2.MultivectorStorage.TryGetVectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.ESp(v1.VectorStorage, vectorStorage))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaMultivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetBivectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.ESp(vectorStorage, v2.BivectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaBivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return v2.MultivectorStorage.TryGetBivectorPart(out var vectorStorage)
                ? processor.CreateScalar(processor.ESp(v1.BivectorStorage, vectorStorage))
                : processor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaMultivector<T> v1, GaKVector<T> v2)
        {
            var grade = v2.Grade;
            var processor = v1.GeometricProcessor;

            return v1.MultivectorStorage.TryGetKVectorPart(grade, out var vectorStorage)
                ? processor.CreateScalar(processor.ESp(vectorStorage, v2.KVectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaKVector<T> v1, GaMultivector<T> v2)
        {
            var grade = v1.Grade;
            var processor = v1.GeometricProcessor;

            return v2.MultivectorStorage.TryGetKVectorPart(grade, out var vectorStorage)
                ? processor.CreateScalar(processor.ESp(v1.KVectorStorage, vectorStorage))
                : processor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaMultivector<T> v1, GaMultivector<T> v2)
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
        public static Scalar<T> ESpSquared<T>(this GaMultivector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return v1.GeometricProcessor.CreateScalar(processor.ESpSquared(v1.MultivectorStorage));
        }
    }
}