using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageNormEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ENorm(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1)
        {
            if (!basisSet.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 
                mv1.GetScalars().Sum(s => s * s);

            return Math.Sqrt(spScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ENormSquared(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1)
        {
            if (!basisSet.IsEuclidean)
                throw new InvalidOperationException();

            return mv1.GetScalars().Sum(s => s * s);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T ENormAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1)
        {
            var spScalar = mv1
                .GetLinVectorIndexScalarStorage()
                .GetScalars()
                .Aggregate(
                    scalarProcessor.ScalarZero, 
                    (current, scalar1) => 
                        scalarProcessor.Add(current, scalarProcessor.Times(scalar1, scalar1))
                );

            return scalarProcessor.Sqrt(spScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T ENormSquaredAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1)
        {
            return mv1
                .GetLinVectorIndexScalarStorage()
                .GetScalars()
                .Aggregate(
                    scalarProcessor.ScalarZero, 
                    (current, scalar1) => 
                        scalarProcessor.Add(current, scalarProcessor.Times(scalar1, scalar1))
                );
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T ENormAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1)
        {
            var spScalar = mv1
                .GetLinVectorIdScalarStorage()
                .GetScalars()
                .Aggregate(
                    scalarProcessor.ScalarZero, 
                    (current, scalar1) => 
                        scalarProcessor.Add(current, scalarProcessor.Times(scalar1, scalar1))
                );

            return scalarProcessor.Sqrt(spScalar);
        }

        private static T ENormSquaredAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1)
        {
            return mv1
                .GetLinVectorIdScalarStorage()
                .GetScalars()
                .Aggregate(
                    scalarProcessor.ScalarZero, 
                    (current, scalar1) => 
                        scalarProcessor.Add(current, scalarProcessor.Times(scalar1, scalar1))
                );
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