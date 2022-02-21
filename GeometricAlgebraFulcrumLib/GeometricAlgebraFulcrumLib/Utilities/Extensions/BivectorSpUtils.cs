using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class BivectorSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Bivector<T> v1, Scalar<T> v2)
        {
            return v1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Scalar<T> v1, Bivector<T> v2)
        {
            return v2.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Bivector<T> v1, Vector<T> v2)
        {
            return v1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Vector<T> v1, Bivector<T> v2)
        {
            return v1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Bivector<T> v1, Bivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return processor.CreateScalar(
                processor.Sp(
                    v1.BivectorStorage, 
                    v2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this Bivector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return processor.SpSquared(v1.BivectorStorage).CreateScalar(processor);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Bivector<T> v1, Scalar<T> v2)
        {
            return v1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Scalar<T> v1, Bivector<T> v2)
        {
            return v2.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Bivector<T> v1, Vector<T> v2)
        {
            return v1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Vector<T> v1, Bivector<T> v2)
        {
            return v1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Bivector<T> v1, Bivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return processor.CreateScalar(
                processor.ESp(
                    v1.BivectorStorage, 
                    v2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this Bivector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return processor.ESp(v1.BivectorStorage).CreateScalar(processor);
        }
    }
}