using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing
{
    public static class GaProcessorDivideUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Divide<T>(this IGasScalar<T> mv, T scalar2)
        {
            return mv.ScalarProcessor.CreateScalar(
                mv.ScalarProcessor.Divide(mv.Scalar, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Divide<T>(this T scalar2, IGasScalar<T> mv)
        {
            return mv.ScalarProcessor.CreateScalar(
                mv.ScalarProcessor.Divide(scalar2, mv.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Divide<T>(this IGasScalar<T> scalar1, IGasScalar<T> scalar2)
        {
            return scalar1.ScalarProcessor.CreateScalar(
                scalar1.ScalarProcessor.Divide(scalar1.Scalar, scalar2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVectorTerm<T> Divide<T>(this IGasVectorTerm<T> mv, T scalar2)
        {
            return mv.ScalarProcessor.CreateVector(
                mv.Index,
                mv.ScalarProcessor.Divide(mv.Scalar, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivectorTerm<T> Divide<T>(this IGasBivectorTerm<T> mv, T scalar2)
        {
            return mv.ScalarProcessor.CreateBivector(
                mv.Index,
                mv.ScalarProcessor.Divide(mv.Scalar, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GasKVectorTerm<T> DivideAsGaKVectorTermStorage<T>(IGasKVectorTerm<T> mv, T scalar2)
        {
            return new GasKVectorTerm<T>(
                mv.ScalarProcessor,
                mv.BasisBlade,
                mv.ScalarProcessor.Divide(mv.Scalar, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> Divide<T>(this IGasKVectorTerm<T> mv, T scalar2)
        {
            return mv switch
            {
                IGasScalar<T> s => Divide(s, scalar2),
                IGasVectorTerm<T> vt => Divide(vt, scalar2),
                IGasBivectorTerm<T> bvt => Divide(bvt, scalar2),
                _ => DivideAsGaKVectorTermStorage(mv, scalar2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasVector<T> DivideAsGaVectorStorage<T>(this IGasVector<T> mv, T scalar2)
        {
            var scalarProcessor = mv.ScalarProcessor;

            return scalarProcessor.CreateVector(
                mv
                    .GetIndexScalarDictionary()
                    .CopyToDictionary(
                        scalar1 => scalarProcessor.Divide(scalar1, scalar2)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVector<T> Divide<T>(this IGasVector<T> mv, T scalar2)
        {
            return mv is IGasVectorTerm<T> vt
                ? Divide(vt, scalar2)
                : DivideAsGaVectorStorage(mv, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasBivector<T> DivideAsGaBivectorStorage<T>(this IGasBivector<T> mv, T scalar2)
        {
            var scalarProcessor = mv.ScalarProcessor;

            return scalarProcessor.CreateBivector(
                mv
                    .GetIndexScalarDictionary()
                    .CopyToDictionary(
                        scalar1 => scalarProcessor.Divide(scalar1, scalar2)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivector<T> Divide<T>(this IGasBivector<T> mv, T scalar2)
        {
            return mv is IGasBivectorTerm<T> bvt
                ? Divide(bvt, scalar2)
                : DivideAsGaBivectorStorage(mv, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GasKVector<T> DivideAsGaKVectorStorage<T>(this IGasKVector<T> mv, T scalar2)
        {
            var grade = mv.Grade;
            var scalarProcessor = mv.ScalarProcessor;
            var indexScalarDictionary = 
                mv
                    .GetIndexScalarDictionary()
                    .CopyToDictionary(
                        scalar1 => scalarProcessor.Divide(scalar1, scalar2)
                    );

            return new GasKVector<T>(
                scalarProcessor,
                grade,
                indexScalarDictionary,
                mv.MaxBasisBladeId
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> Divide<T>(this IGasKVector<T> mv, T scalar2)
        {
            return mv switch
            {
                IGasScalar<T> s => Divide(s, scalar2),
                IGasVectorTerm<T> vt => Divide(vt, scalar2),
                IGasBivectorTerm<T> bvt => Divide(bvt, scalar2),
                IGasKVectorTerm<T> kvt => DivideAsGaKVectorTermStorage(kvt, scalar2),
                IGasVector<T> v => DivideAsGaVectorStorage(v, scalar2),
                IGasBivector<T> bv => DivideAsGaBivectorStorage(bv, scalar2),
                _ => DivideAsGaKVectorStorage(mv, scalar2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GasGradedMultivector<T> DivideAsGaMultivectorGradedStorage<T>(this IGasGradedMultivector<T> mv, T scalar2)
        {
            var scalarProcessor = mv.ScalarProcessor;
            var gradeIndexScalarDictionary =
                mv
                    .GetGradeIndexScalarDictionary()
                    .CopyToDictionary(indexScalarDictionary => 
                        indexScalarDictionary.CopyToDictionary(scalar1 => 
                            scalarProcessor.Divide(scalar1, scalar2)
                        )
                    );

            return new GasGradedMultivector<T>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                mv.MaxBasisBladeId
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasGradedMultivector<T> Divide<T>(this IGasGradedMultivector<T> mv, T scalar2)
        {
            return mv switch
            {
                IGasScalar<T> s => Divide(s, scalar2),
                IGasVectorTerm<T> vt => Divide(vt, scalar2),
                IGasBivectorTerm<T> bvt => Divide(bvt, scalar2),
                IGasKVectorTerm<T> kvt => DivideAsGaKVectorTermStorage(kvt, scalar2),
                IGasVector<T> v => DivideAsGaVectorStorage(v, scalar2),
                IGasBivector<T> bv => DivideAsGaBivectorStorage(bv, scalar2),
                IGasKVector<T> kv => DivideAsGaKVectorStorage(kv, scalar2),
                _ => DivideAsGaMultivectorGradedStorage(mv, scalar2)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GasTermsMultivector<T> DivideAsGaMultivectorTermsStorage<T>(this IGasTermsMultivector<T> mv, T scalar2)
        {
            var scalarProcessor = mv.ScalarProcessor;
            var idScalarDictionary = 
                mv
                    .GetIdScalarDictionary()
                    .CopyToDictionary(
                        scalar1 => scalarProcessor.Divide(scalar1, scalar2)
                    );

            return new GasTermsMultivector<T>(
                scalarProcessor,
                idScalarDictionary,
                mv.MaxBasisBladeId
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasTermsMultivector<T> Divide<T>(this IGasTermsMultivector<T> mv, T scalar2)
        {
            return mv switch
            {
                IGasScalar<T> s => Divide(s, scalar2),
                IGasVectorTerm<T> vt => Divide(vt, scalar2),
                IGasBivectorTerm<T> bvt => Divide(bvt, scalar2),
                IGasKVectorTerm<T> kvt => DivideAsGaKVectorTermStorage(kvt, scalar2),
                _ => DivideAsGaMultivectorTermsStorage(mv, scalar2)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Divide<T>(this IGasMultivector<T> mv, T scalar2)
        {
            return mv switch
            {
                IGasScalar<T> s => Divide(s, scalar2),
                IGasVectorTerm<T> vt => Divide(vt, scalar2),
                IGasBivectorTerm<T> bvt => Divide(bvt, scalar2),
                IGasKVectorTerm<T> kvt => DivideAsGaKVectorTermStorage(kvt, scalar2),
                IGasVector<T> v => DivideAsGaVectorStorage(v, scalar2),
                IGasBivector<T> bv => DivideAsGaBivectorStorage(bv, scalar2),
                IGasKVector<T> kv => DivideAsGaKVectorStorage(kv, scalar2),
                IGasGradedMultivector<T> gmv => DivideAsGaMultivectorGradedStorage(gmv, scalar2),
                _ => DivideAsGaMultivectorTermsStorage((IGasTermsMultivector<T>) mv, scalar2)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVectorTerm<T> DivideByENorm<T>(this IGasVectorTerm<T> mv)
        {
            return mv.Divide(mv.ENorm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivectorTerm<T> DivideByENorm<T>(this IGasBivectorTerm<T> mv)
        {
            return mv.Divide(mv.ENorm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> DivideByENorm<T>(this IGasKVectorTerm<T> mv)
        {
            return mv.Divide(mv.ENorm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVector<T> DivideByENorm<T>(this IGasVector<T> mv)
        {
            return mv.Divide(mv.ENorm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivector<T> DivideByENorm<T>(this IGasBivector<T> mv)
        {
            return mv.Divide(mv.ENorm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> DivideByENorm<T>(this IGasKVector<T> mv)
        {
            return mv.Divide(mv.ENorm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasGradedMultivector<T> DivideByENorm<T>(this IGasGradedMultivector<T> mv)
        {
            return mv.Divide(mv.ENorm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasTermsMultivector<T> DivideByENorm<T>(this IGasTermsMultivector<T> mv)
        {
            return mv.Divide(mv.ENorm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> DivideByENorm<T>(this IGasMultivector<T> mv)
        {
            return mv.Divide(mv.ENorm());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVectorTerm<T> DivideByENormSquared<T>(this IGasVectorTerm<T> mv)
        {
            return mv.Divide(mv.ENormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivectorTerm<T> DivideByENormSquared<T>(this IGasBivectorTerm<T> mv)
        {
            return mv.Divide(mv.ENormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> DivideByENormSquared<T>(this IGasKVectorTerm<T> mv)
        {
            return mv.Divide(mv.ENormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVector<T> DivideByENormSquared<T>(this IGasVector<T> mv)
        {
            return mv.Divide(mv.ENormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivector<T> DivideByENormSquared<T>(this IGasBivector<T> mv)
        {
            return mv.Divide(mv.ENormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> DivideByENormSquared<T>(this IGasKVector<T> mv)
        {
            return mv.Divide(mv.ENormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasGradedMultivector<T> DivideByENormSquared<T>(this IGasGradedMultivector<T> mv)
        {
            return mv.Divide(mv.ENormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasTermsMultivector<T> DivideByENormSquared<T>(this IGasTermsMultivector<T> mv)
        {
            return mv.Divide(mv.ENormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> DivideByENormSquared<T>(this IGasMultivector<T> mv)
        {
            return mv.Divide(mv.ENormSquared());
        }
    }
}