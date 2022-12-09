using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageNormOrtUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBilinearProductResult NormSquared(this BasisBladeSet basisSet, ulong id1)
        {
            return new BasisBilinearProductResult(
                basisSet.NormSquaredSign(id1),
                0
            );
        }

        public static double Norm(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1)
        {
            if (!basisSet.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 =
                mv1.GetIdScalarRecords();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var sig = basisSet.NormSquaredSign(id1);

                if (sig == 0)
                    continue;

                var scalar = scalar1 * scalar1;

                spScalar = sig > 0
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return Math.Sqrt(Math.Abs(spScalar));
        }

        public static double NormSquared(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1)
        {
            if (!basisSet.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 =
                mv1.GetIdScalarRecords();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var sig = basisSet.NormSquaredSign(id1);

                if (sig == 0)
                    continue;

                var scalar = scalar1 * scalar1;

                spScalar = sig > 0
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, T mv1)
        {
            return scalarProcessor.SqrtOfAbs(
                scalarProcessor.Times(mv1, mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, T mv1)
        {
            return scalarProcessor.Square(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, KVectorStorage<T> mv1)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 =>
                    scalarProcessor.Norm(basisSet, vt1),

                _ =>
                    scalarProcessor.NormAsScalar(basisSet, mv1)
            };
        }

        public static T VectorsNorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var sig = basisSet.NormSquaredSign(index.BasisVectorIndexToId());

                if (sig == 0)
                    continue;

                var scalar1 = vector1[index];
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        public static T VectorsNormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var sig = basisSet.NormSquaredSign(index.BasisVectorIndexToId());

                if (sig == 0)
                    continue;

                var scalar1 = vector1[index];
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, VectorStorage<T> mv1)
        {
            var indexScalarPairs1 =
                mv1.GetLinVectorIndexScalarStorage();

            var spScalar = scalarProcessor.ScalarZero;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var sig = basisSet.NormSquaredSign(index.BasisVectorIndexToId());

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, VectorStorage<T> mv1)
        {
            var indexScalarPairs1 =
                mv1.GetLinVectorIndexScalarStorage();

            var spScalar = scalarProcessor.ScalarZero;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var sig = basisSet.NormSquaredSign(index.BasisVectorIndexToId());

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        private static T NormAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, KVectorStorage<T> mv1)
        {
            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ScalarZero;

            var indexScalarPairs1 =
                mv1.GetLinVectorIndexScalarStorage();

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id = index.BasisBladeIndexToId(grade);
                var sig = basisSet.NormSquaredSign(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        private static T NormSquaredAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, KVectorStorage<T> mv1)
        {
            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ScalarZero;

            var indexScalarPairs1 =
                mv1.GetLinVectorIndexScalarStorage();

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id = index.BasisBladeIndexToId(grade);
                var sig = basisSet.NormSquaredSign(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, KVectorStorage<T> mv1)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 => scalarProcessor.NormSquared(basisSet, vt1),
                _ => scalarProcessor.NormSquaredAsScalar(basisSet, mv1)
            };
        }

        private static T NormAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var idScalarDictionary1 =
                mv1.GetLinVectorIdScalarStorage();

            foreach (var (id, scalar1) in idScalarDictionary1.GetIndexScalarRecords())
            {
                var sig = basisSet.NormSquaredSign(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        private static T NormSquaredAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var idScalarDictionary1 =
                mv1.GetLinVectorIdScalarStorage();

            foreach (var (id, scalar1) in idScalarDictionary1.GetIndexScalarRecords())
            {
                var sig = basisSet.NormSquaredSign(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 => scalarProcessor.Norm(basisSet, vt1),
                KVectorStorage<T> kvt1 => scalarProcessor.Norm(basisSet, kvt1),
                _ => scalarProcessor.NormAsScalar(basisSet, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 => scalarProcessor.NormSquared(basisSet, vt1),
                KVectorStorage<T> kvt1 => scalarProcessor.NormSquared(basisSet, kvt1),
                _ => scalarProcessor.NormSquaredAsScalar(basisSet, mv1)
            };
        }

    }
}