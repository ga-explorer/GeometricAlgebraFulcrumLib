using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv1)
        {
            return mv1.IsEmpty()
                ? scalarProcessor.ZeroScalar
                : scalarProcessor.Square(mv1.FirstScalar);
        }
        
        public static T VectorsESp<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var lcpScalar = scalarProcessor.ZeroScalar;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var id = 1UL << index;
                var scalar1 = vector1[index];

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                lcpScalar = GaBasisUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return lcpScalar;
        }

        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = 1UL << (int) index;
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = GaBasisUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        private static T ESpAsScalar<T>(IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1)
        {
            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ZeroScalar;
            
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = 
                    GaBasisUtils.BasisBladeId(grade, index);

                var signature = 
                    GaBasisUtils.EGpSignature(id);

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
            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarDictionary1 = mv1.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarDictionary1)
            {
                var signature = 
                    GaBasisUtils.EGpSignature(id);

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
                GaBasisUtils.ESpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static double ESp(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1, IGaStorageMultivector<double> mv2)
        {
            if (!basisSignature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                if (!idScalarPairs2.TryGetValue(id1, out var scalar2))
                    continue;

                var scalar = scalar1 * scalar2;

                spScalar = GaBasisUtils.IsPositiveEGp(id1)
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv1, IGaStorageScalar<T> mv2)
        {
            return mv1.IsEmpty() || mv2.IsEmpty()
                ? scalarProcessor.ZeroScalar
                : scalarProcessor.Times(mv1.FirstScalar, mv2.FirstScalar);
        }
        
        public static T VectorsESp<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            var lcpScalar = scalarProcessor.ZeroScalar;

            var count = Math.Min(vector1.Count, vector2.Count);

            for (var index = 0; index < count; index++)
            {
                var id = 1UL << index;
                var scalar1 = vector1[index];
                var scalar2 = vector2[index];

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                lcpScalar = GaBasisUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return lcpScalar;
        }

        public static T ESp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv1, IGaStorageVector<T> mv2)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var indexScalarPairs2 = 
                mv2.IndexScalarDictionary;

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
                    continue;

                var id = 1UL << (int) index;
                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = GaBasisUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        private static T ESpAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            if (mv1.Grade != mv2.Grade)
                return scalarProcessor.ZeroScalar;

            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ZeroScalar;
            
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var indexScalarPairs2 = 
                mv2.IndexScalarDictionary;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
                    continue;

                var id = 
                    GaBasisUtils.BasisBladeId(grade, index);

                var signature = 
                    GaBasisUtils.EGpSignature(id);

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
            var spScalar = scalarProcessor.ZeroScalar;

            var gradeIndexScalarDictionary1 = mv1.GetGradeIndexScalarDictionary();
            var gradeIndexScalarDictionary2 = mv2.GetGradeIndexScalarDictionary();

            foreach (var (grade, indexScalarPairs1) in gradeIndexScalarDictionary1)
            {
                if (!gradeIndexScalarDictionary2.TryGetValue(grade, out var indexScalarPairs2))
                    continue;

                foreach (var (index, scalar1) in indexScalarPairs1)
                {
                    if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
                        continue;

                    var id = 
                        GaBasisUtils.BasisBladeId(grade, index);

                    var signature = 
                        GaBasisUtils.EGpSignature(id);

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
            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                if (!idScalarPairs2.TryGetValue(id, out var scalar2))
                    continue;
                
                var signature = 
                    GaBasisUtils.ESpSignature(id);

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