using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv1)
        {
            return mv1.TryGetScalar(out var value)
                ? scalarProcessor.Square(value)
                : scalarProcessor.ScalarZero;
        }
        
        public static T VectorsESp<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var lcpScalar = scalarProcessor.ScalarZero;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var id = index.BasisVectorIndexToId();
                var scalar1 = vector1[index];

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                lcpScalar = GaBasisBladeProductUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return lcpScalar;
        }

        public static T ESp<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var spScalar = scalarProcessor.ScalarZero;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id = index.BasisVectorIndexToId();
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = GaBasisBladeProductUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        private static T ESpAsScalar<T>(IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1)
        {
            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ScalarZero;
            
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id = 
                    index.BasisBladeIndexToId(grade);

                var signature = 
                    GaBasisBladeProductUtils.EGpSignature(id);

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
        public static T ESp<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 => ESp(scalarProcessor, s1),
                IGaVectorStorage<T> vt1 => ESp(scalarProcessor, vt1),
                _ => ESpAsScalar(scalarProcessor, mv1)
            };
        }

        private static T ESpAsScalar<T>(IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var idScalarDictionary1 = mv1.GetIdScalarList();

            foreach (var (id, scalar1) in idScalarDictionary1.GetIndexScalarRecords())
            {
                var signature = 
                    GaBasisBladeProductUtils.EGpSignature(id);

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
        public static T ESp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 => ESp(scalarProcessor, s1),
                IGaVectorStorage<T> vt1 => ESp(scalarProcessor, vt1),
                IGaKVectorStorage<T> kvt1 => ESp(scalarProcessor, kvt1),
                _ => ESpAsScalar(scalarProcessor, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult ESp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                GaBasisBladeProductUtils.ESpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static double ESp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            if (!basisSignature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                if (!idScalarPairs2.TryGetScalar(id1, out var scalar2))
                    continue;

                var scalar = scalar1 * scalar2;

                spScalar = GaBasisBladeProductUtils.IsPositiveEGp(id1)
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv1, IGaScalarStorage<T> mv2)
        {
            return mv1.TryGetScalar(out var value1) && mv2.TryGetScalar(out var value2)
                ? scalarProcessor.Times(value1, value2)
                : scalarProcessor.ScalarZero;
        }
        
        public static T VectorsESp<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            var lcpScalar = scalarProcessor.ScalarZero;

            var count = Math.Min(vector1.Count, vector2.Count);

            for (var index = 0; index < count; index++)
            {
                var id = index.BasisVectorIndexToId();
                var scalar1 = vector1[index];
                var scalar2 = vector2[index];

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                lcpScalar = GaBasisBladeProductUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return lcpScalar;
        }

        public static T ESp<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv1, IGaVectorStorage<T> mv2)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            var spScalar = scalarProcessor.ScalarZero;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                if (!indexScalarPairs2.TryGetScalar(index, out var scalar2))
                    continue;

                var id = index.BasisVectorIndexToId();
                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = GaBasisBladeProductUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        private static T ESpAsScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            if (mv1.Grade != mv2.Grade)
                return scalarProcessor.ScalarZero;

            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ScalarZero;
            
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                if (!indexScalarPairs2.TryGetScalar(index, out var scalar2))
                    continue;

                var id = 
                    index.BasisBladeIndexToId(grade);

                var signature = 
                    GaBasisBladeProductUtils.EGpSignature(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => ESp(scalarProcessor, s1, s2),
                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => ESp(scalarProcessor, vt1, vt2),
                _ => ESpAsScalar(scalarProcessor, mv1, mv2)
            };
        }

        private static T ESpAsScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv1, IGaMultivectorGradedStorage<T> mv2)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var gradeIndexScalarDictionary1 = mv1.GetGradeIndexScalarList();
            var gradeIndexScalarDictionary2 = mv2.GetGradeIndexScalarList();

            foreach (var (grade, indexScalarPairs1) in gradeIndexScalarDictionary1.GetGradeStorageRecords())
            {
                if (!gradeIndexScalarDictionary2.TryGetEvenStorage(grade, out var indexScalarPairs2))
                    continue;

                foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
                {
                    if (!indexScalarPairs2.TryGetScalar(index, out var scalar2))
                        continue;

                    var id = 
                        index.BasisBladeIndexToId(grade);

                    var signature = 
                        GaBasisBladeProductUtils.EGpSignature(id);

                    //if (signature == 0) 
                    //    continue;

                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    spScalar = signature > 0
                        ? scalarProcessor.Add(spScalar, scalar)
                        : scalarProcessor.Subtract(spScalar, scalar);
                }
            }

            return spScalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv1, IGaMultivectorGradedStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => ESp(scalarProcessor, s1, s2),
                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => ESp(scalarProcessor, vt1, vt2),
                IGaKVectorStorage<T> kvt1 when mv2 is IGaKVectorStorage<T> kvt2 => ESp(scalarProcessor, kvt1, kvt2),
                _ => ESpAsScalar(scalarProcessor, mv1, mv2)
            };
        }

        private static T ESpAsScalar<T>(IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                if (!idScalarPairs2.TryGetScalar(id, out var scalar2))
                    continue;
                
                var signature = 
                    GaBasisBladeProductUtils.ESpSignature(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
                
            }

            return spScalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv1, IGaMultivectorSparseStorage<T> mv2)
        {
            return ESpAsScalar(scalarProcessor, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => ESp(scalarProcessor, s1, s2),
                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => ESp(scalarProcessor, vt1, vt2),
                IGaKVectorStorage<T> kvt1 when mv2 is IGaKVectorStorage<T> kvt2 => ESp(scalarProcessor, kvt1, kvt2),
                IGaMultivectorGradedStorage<T> gmv1 when mv2 is IGaMultivectorGradedStorage<T> gmv2 => ESpAsScalar(scalarProcessor, gmv1, gmv2),
                _ => ESpAsScalar(scalarProcessor, mv1, mv2)
            };
        }
    }
}