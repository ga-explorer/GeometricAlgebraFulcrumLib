using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageNormEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBilinearProductResult ENormSquared(this IGeometricAlgebraSignature signature, ulong id1)
        {
            return new BasisBilinearProductResult(
                BasisBladeProductUtils.ENormSquaredSignature(id1), 
                0
            );
        }

        public static double ENorm(this GeometricAlgebraSignatureLookup basisSignature, IMultivectorStorage<double> mv1)
        {
            if (!basisSignature.IsEuclidean)
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

        public static double ENormSquared(this GeometricAlgebraSignatureLookup basisSignature, IMultivectorStorage<double> mv1)
        {
            if (!basisSignature.IsEuclidean)
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
        
        public static T VectorsENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var id = index.BasisVectorIndexToId();
                var scalar1 = vector1[index];

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = BasisBladeProductUtils.ENormSquaredSignature(id) > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }
        
        public static T VectorsENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var id = index.BasisVectorIndexToId();
                var scalar1 = vector1[index];

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = BasisBladeProductUtils.ENormSquaredSignature(id) > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        public static T ENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            var spScalar = scalarProcessor.ScalarZero;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id = index.BasisVectorIndexToId();
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = BasisBladeProductUtils.ENormSquaredSignature(id) > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            var spScalar = scalarProcessor.ScalarZero;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id = index.BasisVectorIndexToId();
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = BasisBladeProductUtils.ENormSquaredSignature(id) > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
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