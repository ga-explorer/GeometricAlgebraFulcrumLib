using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageNormEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBilinearProductResult ENormSquared(this GeometricAlgebraBasisSet basisSet, ulong id1)
        {
            return new BasisBilinearProductResult(
                BasisBladeProductUtils.ENormSquaredSignature(id1), 
                0
            );
        }

        public static double ENorm(this GeometricAlgebraBasisSet basisSet, IMultivectorStorage<double> mv1)
        {
            if (!basisSet.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var scalar = scalar1 * scalar1;

                spScalar = BasisBladeProductUtils.ENormSquaredSignature(id1) > 0
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return Math.Sqrt(Math.Abs(spScalar));
        }

        public static double ENormSquared(this GeometricAlgebraBasisSet basisSet, IMultivectorStorage<double> mv1)
        {
            if (!basisSet.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var scalar = scalar1 * scalar1;

                spScalar = BasisBladeProductUtils.ENormSquaredSignature(id1) > 0
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T mv1)
        {
            return scalarProcessor.SqrtOfAbs(scalarProcessor.Square(mv1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T mv1)
        {
            return scalarProcessor.Square(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 => ENorm(scalarProcessor, vt1),
                _ => ENormAsScalar(scalarProcessor, mv1)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T VectorENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> vector1)
        {
            return scalarProcessor.VectorENormSquared(vector1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T VectorENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> vector1)
        {
            return vector1
                .Select(scalar1 => scalarProcessor.Times(scalar1, scalar1))
                .Aggregate(scalarProcessor.ScalarZero, scalarProcessor.Add);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv1)
        {
            return scalarProcessor.Sqrt(scalarProcessor.ENormSquared(mv1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv1)
        {
            return mv1
                .GetLinVectorIndexScalarStorage()
                .GetScalars()
                .Select(scalar1 => scalarProcessor.Times(scalar1, scalar1))
                .Aggregate(
                    scalarProcessor.ScalarZero, 
                    scalarProcessor.Add
                );
        }

        private static T ENormAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1)
        {
            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ScalarZero;
            
            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id = 
                    index.BasisBladeIndexToId(grade);

                var signature = 
                    BasisBladeProductUtils.ENormSquaredSignature(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        private static T ENormSquaredAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1)
        {
            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ScalarZero;
            
            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id = 
                    index.BasisBladeIndexToId(grade);

                var signature = 
                    BasisBladeProductUtils.ENormSquaredSignature(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 => ENormSquared(scalarProcessor, vt1),
                _ => ENormSquaredAsScalar(scalarProcessor, mv1)
            };
        }

        private static T ENormAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var idScalarDictionary1 = 
                mv1.GetLinVectorIdScalarStorage();

            foreach (var (id, scalar1) in idScalarDictionary1.GetIndexScalarRecords())
            {
                var signature = 
                    BasisBladeProductUtils.ENormSquaredSignature(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        private static T ENormSquaredAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var idScalarDictionary1 = 
                mv1.GetLinVectorIdScalarStorage();

            foreach (var (id, scalar1) in idScalarDictionary1.GetIndexScalarRecords())
            {
                var signature = 
                    BasisBladeProductUtils.ENormSquaredSignature(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 => ENorm(scalarProcessor, vt1),
                KVectorStorage<T> kvt1 => ENorm(scalarProcessor, kvt1),
                _ => ENormAsScalar(scalarProcessor, mv1)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 => ENormSquared(scalarProcessor, vt1),
                KVectorStorage<T> kvt1 => ENormSquared(scalarProcessor, kvt1),
                _ => ENormSquaredAsScalar(scalarProcessor, mv1)
            };
        }
    }
}