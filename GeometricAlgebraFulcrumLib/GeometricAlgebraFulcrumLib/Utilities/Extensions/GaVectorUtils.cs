using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaVectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new Scalar<T>(
                processor,
                processor.Sp(v1.VectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sp<T>(this GaVector<T> v1)
        {
            var processor = v1.Processor;

            return new Scalar<T>(
                processor,
                processor.Sp(v1.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Inverse<T>(this GaVector<T> v1)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.Sp(v1.VectorStorage))
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ENorm<T>(this GaVector<T> v1)
        {
            var processor = v1.Processor;

            return new Scalar<T>(
                processor,
                processor.ENorm(v1.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ENormSquared<T>(this GaVector<T> v1)
        {
            var processor = v1.Processor;

            return new Scalar<T>(
                processor,
                processor.ENormSquared(v1.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaVector<T> v1)
        {
            var processor = v1.Processor;

            return new Scalar<T>(
                processor,
                processor.ESp(v1.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ESp<T>(this GaVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new Scalar<T>(
                processor,
                processor.ESp(v1.VectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EInverse<T>(this GaVector<T> v1)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.ESp(v1.VectorStorage))
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> Op<T>(this GaVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Op(v1.VectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> Op<T>(this GaVector<T> v1, IGaVectorStorage<T> v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Op(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> Op<T>(this IGaVectorStorage<T> v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaBivector<T>(
                processor,
                processor.Op(v1, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> MapVector<T>(this IGaRotor<T> rotor, GaVector<T> vector)
        {
            var processor = vector.Processor;

            return new GaVector<T>(
                processor,
                rotor.MapVector(vector.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> ProjectVector<T>(this GaSubspace<T> subspace, GaVector<T> vector)
        {
            var processor = vector.Processor;

            return new GaVector<T>(
                processor,
                subspace.Project(vector.VectorStorage).GetVectorPart()
            );
        }
    }
}