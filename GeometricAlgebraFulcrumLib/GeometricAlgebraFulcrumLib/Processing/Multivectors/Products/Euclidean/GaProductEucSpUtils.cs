using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv1)
        {
            return mv1.TryGetScalar(out var value)
                ? scalarProcessor.Square(value)
                : scalarProcessor.GetZeroScalar();
        }
        
        public static T VectorsESp<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var lcpScalar = scalarProcessor.GetZeroScalar();

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

        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var spScalar = scalarProcessor.GetZeroScalar();

            foreach (var (index, scalar1) in indexScalarPairs1.GetKeyValueRecords())
            {
                var id = index.BasisVectorIndexToId();
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = GaBasisBladeProductUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        private static T ESpAsScalar<T>(IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1)
        {
            var grade = mv1.Grade;
            var spScalar = scalarProcessor.GetZeroScalar();
            
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            foreach (var (index, scalar1) in indexScalarPairs1.GetKeyValueRecords())
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
        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => ESp(scalarProcessor, s1),
                IGaStorageVector<T> vt1 => ESp(scalarProcessor, vt1),
                _ => ESpAsScalar(scalarProcessor, mv1)
            };
        }

        private static T ESpAsScalar<T>(IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1)
        {
            var spScalar = scalarProcessor.GetZeroScalar();

            var idScalarDictionary1 = mv1.GetIdScalarList();

            foreach (var (id, scalar1) in idScalarDictionary1.GetKeyValueRecords())
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
        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => ESp(scalarProcessor, s1),
                IGaStorageVector<T> vt1 => ESp(scalarProcessor, vt1),
                IGaStorageKVector<T> kvt1 => ESp(scalarProcessor, kvt1),
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

        public static double ESp(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1, IGaStorageMultivector<double> mv2)
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
                if (!idScalarPairs2.TryGetValue(id1, out var scalar2))
                    continue;

                var scalar = scalar1 * scalar2;

                spScalar = GaBasisBladeProductUtils.IsPositiveEGp(id1)
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv1, IGaStorageScalar<T> mv2)
        {
            return mv1.TryGetScalar(out var value1) && mv2.TryGetScalar(out var value2)
                ? scalarProcessor.Times(value1, value2)
                : scalarProcessor.GetZeroScalar();
        }
        
        public static T VectorsESp<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            var lcpScalar = scalarProcessor.GetZeroScalar();

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

        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv1, IGaStorageVector<T> mv2)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            var spScalar = scalarProcessor.GetZeroScalar();

            foreach (var (index, scalar1) in indexScalarPairs1.GetKeyValueRecords())
            {
                if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
                    continue;

                var id = index.BasisVectorIndexToId();
                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = GaBasisBladeProductUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        private static T ESpAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            if (mv1.Grade != mv2.Grade)
                return scalarProcessor.GetZeroScalar();

            var grade = mv1.Grade;
            var spScalar = scalarProcessor.GetZeroScalar();
            
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index, scalar1) in indexScalarPairs1.GetKeyValueRecords())
            {
                if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
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
        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => ESp(scalarProcessor, s1, s2),
                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => ESp(scalarProcessor, vt1, vt2),
                _ => ESpAsScalar(scalarProcessor, mv1, mv2)
            };
        }

        private static T ESpAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv1, IGaStorageMultivectorGraded<T> mv2)
        {
            var spScalar = scalarProcessor.GetZeroScalar();

            var gradeIndexScalarDictionary1 = mv1.GetGradeIndexScalarList();
            var gradeIndexScalarDictionary2 = mv2.GetGradeIndexScalarList();

            foreach (var (grade, indexScalarPairs1) in gradeIndexScalarDictionary1.GetGradeListRecords())
            {
                if (!gradeIndexScalarDictionary2.TryGetList(grade, out var indexScalarPairs2))
                    continue;

                foreach (var (index, scalar1) in indexScalarPairs1.GetKeyValueRecords())
                {
                    if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
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
        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv1, IGaStorageMultivectorGraded<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => ESp(scalarProcessor, s1, s2),
                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => ESp(scalarProcessor, vt1, vt2),
                IGaStorageKVector<T> kvt1 when mv2 is IGaStorageKVector<T> kvt2 => ESp(scalarProcessor, kvt1, kvt2),
                _ => ESpAsScalar(scalarProcessor, mv1, mv2)
            };
        }

        private static T ESpAsScalar<T>(IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var spScalar = scalarProcessor.GetZeroScalar();

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                if (!idScalarPairs2.TryGetValue(id, out var scalar2))
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
        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv1, IGaStorageMultivectorSparse<T> mv2)
        {
            return ESpAsScalar(scalarProcessor, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => ESp(scalarProcessor, s1, s2),
                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => ESp(scalarProcessor, vt1, vt2),
                IGaStorageKVector<T> kvt1 when mv2 is IGaStorageKVector<T> kvt2 => ESp(scalarProcessor, kvt1, kvt2),
                IGaStorageMultivectorGraded<T> gmv1 when mv2 is IGaStorageMultivectorGraded<T> gmv2 => ESpAsScalar(scalarProcessor, gmv1, gmv2),
                _ => ESpAsScalar(scalarProcessor, mv1, mv2)
            };
        }
    }
}