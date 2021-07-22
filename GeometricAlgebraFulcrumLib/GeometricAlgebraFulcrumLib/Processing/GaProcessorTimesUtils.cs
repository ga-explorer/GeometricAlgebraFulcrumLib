using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing
{
    public static class GaProcessorTimesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Times<T>(this IGasScalar<T> mv, T scalar2)
        {
            return mv.ScalarProcessor.CreateScalar(
                mv.ScalarProcessor.Times(mv.Scalar, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Times<T>(this T scalar2, IGasScalar<T> mv)
        {
            return mv.ScalarProcessor.CreateScalar(
                mv.ScalarProcessor.Times(scalar2, mv.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVectorTerm<T> Times<T>(this IGasVectorTerm<T> mv, T scalar2)
        {
            return mv.ScalarProcessor.CreateVector(
                mv.Index,
                mv.ScalarProcessor.Times(mv.Scalar, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVectorTerm<T> Times<T>(this T scalar2, IGasVectorTerm<T> mv)
        {
            return mv.ScalarProcessor.CreateVector(
                mv.Index,
                mv.ScalarProcessor.Times(scalar2, mv.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivectorTerm<T> Times<T>(this IGasBivectorTerm<T> mv, T scalar2)
        {
            return mv.ScalarProcessor.CreateBivector(
                mv.Index,
                mv.ScalarProcessor.Times(mv.Scalar, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivectorTerm<T> Times<T>(this T scalar2, IGasBivectorTerm<T> mv)
        {
            return mv.ScalarProcessor.CreateBivector(
                mv.Index,
                mv.ScalarProcessor.Times(scalar2, mv.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GasKVectorTerm<T> TimesAsGaKVectorTermStorage<T>(IGasKVectorTerm<T> mv, T scalar2)
        {
            return new GasKVectorTerm<T>(
                mv.ScalarProcessor,
                mv.BasisBlade,
                mv.ScalarProcessor.Times(mv.Scalar, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GasKVectorTerm<T> TimesAsGaKVectorTermStorage<T>(T scalar2, IGasKVectorTerm<T> mv)
        {
            return new GasKVectorTerm<T>(
                mv.ScalarProcessor,
                mv.BasisBlade,
                mv.ScalarProcessor.Times(scalar2, mv.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> Times<T>(this IGasKVectorTerm<T> mv, T scalar2)
        {
            return mv switch
            {
                IGasScalar<T> s => Times(s, scalar2),
                IGasVectorTerm<T> vt => Times(vt, scalar2),
                IGasBivectorTerm<T> bvt => Times(bvt, scalar2),
                _ => TimesAsGaKVectorTermStorage(mv, scalar2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> Times<T>(this T scalar2, IGasKVectorTerm<T> mv)
        {
            return mv switch
            {
                IGasScalar<T> s => Times(scalar2, s),
                IGasVectorTerm<T> vt => Times(scalar2, vt),
                IGasBivectorTerm<T> bvt => Times(scalar2, bvt),
                _ => TimesAsGaKVectorTermStorage(scalar2, mv)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasVector<T> TimesAsGaVectorStorage<T>(this IGasVector<T> mv, T scalar2)
        {
            var scalarProcessor = mv.ScalarProcessor;

            return scalarProcessor.CreateVector(
                mv
                    .GetIndexScalarDictionary()
                    .CopyToDictionary(
                        scalar1 => scalarProcessor.Times(scalar1, scalar2)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasVector<T> TimesAsGaVectorStorage<T>(this T scalar2, IGasVector<T> mv)
        {
            var scalarProcessor = mv.ScalarProcessor;

            return scalarProcessor.CreateVector(
                mv
                    .GetIndexScalarDictionary()
                    .CopyToDictionary(
                        scalar1 => scalarProcessor.Times(scalar2, scalar1)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVector<T> Times<T>(this IGasVector<T> mv, T scalar2)
        {
            return mv is IGasVectorTerm<T> vt
                ? Times(vt, scalar2)
                : TimesAsGaVectorStorage(mv, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVector<T> Times<T>(this T scalar2, IGasVector<T> mv)
        {
            return mv is IGasVectorTerm<T> vt
                ? Times(scalar2, vt)
                : TimesAsGaVectorStorage(scalar2, mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasBivector<T> TimesAsGaBivectorStorage<T>(this IGasBivector<T> mv, T scalar2)
        {
            var scalarProcessor = mv.ScalarProcessor;

            return scalarProcessor.CreateBivector(
                mv
                    .GetIndexScalarDictionary()
                    .CopyToDictionary(
                        scalar1 => scalarProcessor.Times(scalar1, scalar2)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasBivector<T> TimesAsGaBivectorStorage<T>(this T scalar2, IGasBivector<T> mv)
        {
            var scalarProcessor = mv.ScalarProcessor;

            return scalarProcessor.CreateBivector(
                mv
                    .GetIndexScalarDictionary()
                    .CopyToDictionary(
                        scalar1 => scalarProcessor.Times(scalar2, scalar1)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivector<T> Times<T>(this IGasBivector<T> mv, T scalar2)
        {
            return mv is IGasBivectorTerm<T> bvt
                ? Times(bvt, scalar2)
                : TimesAsGaBivectorStorage(mv, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivector<T> Times<T>(this T scalar2, IGasBivector<T> mv)
        {
            return mv is IGasBivectorTerm<T> bvt
                ? Times(scalar2, bvt)
                : TimesAsGaBivectorStorage(scalar2, mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GasKVector<T> TimesAsGaKVectorStorage<T>(this IGasKVector<T> mv, T scalar2)
        {
            var grade = mv.Grade;
            var scalarProcessor = mv.ScalarProcessor;
            var indexScalarDictionary = 
                mv
                    .GetIndexScalarDictionary()
                    .CopyToDictionary(
                        scalar1 => scalarProcessor.Times(scalar1, scalar2)
                    );

            return new GasKVector<T>(
                scalarProcessor,
                grade,
                indexScalarDictionary,
                mv.MaxBasisBladeId
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GasKVector<T> TimesAsGaKVectorStorage<T>(this T scalar2, IGasKVector<T> mv)
        {
            var grade = mv.Grade;
            var scalarProcessor = mv.ScalarProcessor;
            var indexScalarDictionary = 
                mv
                    .GetIndexScalarDictionary()
                    .CopyToDictionary(
                        scalar1 => scalarProcessor.Times(scalar2, scalar1)
                    );

            return new GasKVector<T>(
                scalarProcessor,
                grade,
                indexScalarDictionary,
                mv.MaxBasisBladeId
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> Times<T>(this IGasKVector<T> mv, T scalar2)
        {
            return mv switch
            {
                IGasScalar<T> s => Times(s, scalar2),
                IGasVectorTerm<T> vt => Times(vt, scalar2),
                IGasBivectorTerm<T> bvt => Times(bvt, scalar2),
                IGasKVectorTerm<T> kvt => TimesAsGaKVectorTermStorage(kvt, scalar2),
                IGasVector<T> v => TimesAsGaVectorStorage(v, scalar2),
                IGasBivector<T> bv => TimesAsGaBivectorStorage(bv, scalar2),
                _ => TimesAsGaKVectorStorage(mv, scalar2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> Times<T>(this T scalar2, IGasKVector<T> mv)
        {
            return mv switch
            {
                IGasScalar<T> s => Times(scalar2, s),
                IGasVectorTerm<T> vt => Times(scalar2, vt),
                IGasBivectorTerm<T> bvt => Times(scalar2, bvt),
                IGasKVectorTerm<T> kvt => TimesAsGaKVectorTermStorage(scalar2, kvt),
                IGasVector<T> v => TimesAsGaVectorStorage(scalar2, v),
                IGasBivector<T> bv => TimesAsGaBivectorStorage(scalar2, bv),
                _ => TimesAsGaKVectorStorage(scalar2, mv)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GasGradedMultivector<T> TimesAsGaMultivectorGradedStorage<T>(this IGasGradedMultivector<T> mv, T scalar2)
        {
            var scalarProcessor = mv.ScalarProcessor;
            var gradeIndexScalarDictionary =
                mv
                    .GetGradeIndexScalarDictionary()
                    .CopyToDictionary(indexScalarDictionary => 
                        indexScalarDictionary.CopyToDictionary(scalar1 => 
                            scalarProcessor.Times(scalar1, scalar2)
                        )
                    );

            return new GasGradedMultivector<T>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                mv.MaxBasisBladeId
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GasGradedMultivector<T> TimesAsGaMultivectorGradedStorage<T>(this T scalar2, IGasGradedMultivector<T> mv)
        {
            var scalarProcessor = mv.ScalarProcessor;
            var gradeIndexScalarDictionary =
                mv
                    .GetGradeIndexScalarDictionary()
                    .CopyToDictionary(indexScalarDictionary => 
                        indexScalarDictionary.CopyToDictionary(scalar1 => 
                            scalarProcessor.Times(scalar2, scalar1)
                        )
                    );

            return new GasGradedMultivector<T>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                mv.MaxBasisBladeId
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasGradedMultivector<T> Times<T>(this IGasGradedMultivector<T> mv, T scalar2)
        {
            return mv switch
            {
                IGasScalar<T> s => Times(s, scalar2),
                IGasVectorTerm<T> vt => Times(vt, scalar2),
                IGasBivectorTerm<T> bvt => Times(bvt, scalar2),
                IGasKVectorTerm<T> kvt => TimesAsGaKVectorTermStorage(kvt, scalar2),
                IGasVector<T> v => TimesAsGaVectorStorage(v, scalar2),
                IGasBivector<T> bv => TimesAsGaBivectorStorage(bv, scalar2),
                IGasKVector<T> kv => TimesAsGaKVectorStorage(kv, scalar2),
                _ => TimesAsGaMultivectorGradedStorage(mv, scalar2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasGradedMultivector<T> Times<T>(this T scalar2, IGasGradedMultivector<T> mv)
        {
            return mv switch
            {
                IGasScalar<T> s => Times(scalar2, s),
                IGasVectorTerm<T> vt => Times(scalar2, vt),
                IGasBivectorTerm<T> bvt => Times(scalar2, bvt),
                IGasKVectorTerm<T> kvt => TimesAsGaKVectorTermStorage(scalar2, kvt),
                IGasVector<T> v => TimesAsGaVectorStorage(scalar2, v),
                IGasBivector<T> bv => TimesAsGaBivectorStorage(scalar2, bv),
                IGasKVector<T> kv => TimesAsGaKVectorStorage(scalar2, kv),
                _ => TimesAsGaMultivectorGradedStorage(scalar2, mv)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GasTermsMultivector<T> TimesAsGaMultivectorTermsStorage<T>(this IGasTermsMultivector<T> mv, T scalar2)
        {
            var scalarProcessor = mv.ScalarProcessor;
            var idScalarDictionary = 
                mv
                    .GetIdScalarDictionary()
                    .CopyToDictionary(
                        scalar1 => scalarProcessor.Times(scalar1, scalar2)
                    );

            return new GasTermsMultivector<T>(
                scalarProcessor,
                idScalarDictionary,
                mv.MaxBasisBladeId
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GasTermsMultivector<T> TimesAsGaMultivectorTermsStorage<T>(this T scalar2, IGasMultivector<T> mv)
        {
            var scalarProcessor = mv.ScalarProcessor;
            var idScalarDictionary = 
                mv
                    .GetIdScalarDictionary()
                    .CopyToDictionary(
                        scalar1 => scalarProcessor.Times(scalar2, scalar1)
                    );

            return new GasTermsMultivector<T>(
                scalarProcessor,
                idScalarDictionary,
                mv.MaxBasisBladeId
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasTermsMultivector<T> Times<T>(this IGasTermsMultivector<T> mv, T scalar2)
        {
            return mv switch
            {
                IGasScalar<T> s => Times(s, scalar2),
                IGasVectorTerm<T> vt => Times(vt, scalar2),
                IGasBivectorTerm<T> bvt => Times(bvt, scalar2),
                IGasKVectorTerm<T> kvt => TimesAsGaKVectorTermStorage(kvt, scalar2),
                _ => TimesAsGaMultivectorTermsStorage(mv, scalar2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasTermsMultivector<T> Times<T>(this T scalar2, IGasTermsMultivector<T> mv)
        {
            return mv switch
            {
                IGasScalar<T> s => Times(scalar2, s),
                IGasVectorTerm<T> vt => Times(scalar2, vt),
                IGasBivectorTerm<T> bvt => Times(scalar2, bvt),
                IGasKVectorTerm<T> kvt => TimesAsGaKVectorTermStorage(scalar2, kvt),
                _ => TimesAsGaMultivectorTermsStorage(scalar2, mv)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Times<T>(this IGasMultivector<T> mv, T scalar2)
        {
            return mv switch
            {
                IGasScalar<T> s => Times(s, scalar2),
                IGasVectorTerm<T> vt => Times(vt, scalar2),
                IGasBivectorTerm<T> bvt => Times(bvt, scalar2),
                IGasKVectorTerm<T> kvt => TimesAsGaKVectorTermStorage(kvt, scalar2),
                IGasVector<T> v => TimesAsGaVectorStorage(v, scalar2),
                IGasBivector<T> bv => TimesAsGaBivectorStorage(bv, scalar2),
                IGasKVector<T> kv => TimesAsGaKVectorStorage(kv, scalar2),
                IGasGradedMultivector<T> gmv => TimesAsGaMultivectorGradedStorage(gmv, scalar2),
                _ => TimesAsGaMultivectorTermsStorage((IGasTermsMultivector<T>) mv, scalar2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Times<T>(this T scalar2, IGasMultivector<T> mv)
        {
            return mv switch
            {
                IGasScalar<T> s => Times(scalar2, s),
                IGasVectorTerm<T> vt => Times(scalar2, vt),
                IGasBivectorTerm<T> bvt => Times(scalar2, bvt),
                IGasKVectorTerm<T> kvt => TimesAsGaKVectorTermStorage(scalar2, kvt),
                IGasVector<T> v => TimesAsGaVectorStorage(scalar2, v),
                IGasBivector<T> bv => TimesAsGaBivectorStorage(scalar2, bv),
                IGasKVector<T> kv => TimesAsGaKVectorStorage(scalar2, kv),
                IGasGradedMultivector<T> gmv => TimesAsGaMultivectorGradedStorage(scalar2, gmv),
                _ => TimesAsGaMultivectorTermsStorage(scalar2, mv)
            };
        }
    }
}