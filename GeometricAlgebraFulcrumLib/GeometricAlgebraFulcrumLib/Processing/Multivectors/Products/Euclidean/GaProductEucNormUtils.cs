﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucNormUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult ENormSquared(this IGaSignature signature, ulong id1)
        {
            return new GaBasisBilinearProductResult(
                GaBasisUtils.ENormSquaredSignature(id1), 
                0
            );
        }

        public static double ENorm(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1)
        {
            if (!basisSignature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var scalar = scalar1 * scalar1;

                spScalar = GaBasisUtils.ENormSquaredSignature(id1) > 0
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return Math.Sqrt(Math.Abs(spScalar));
        }

        public static double ENormSquared(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1)
        {
            if (!basisSignature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var scalar = scalar1 * scalar1;

                spScalar = GaBasisUtils.ENormSquaredSignature(id1) > 0
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv1)
        {
            return mv1.IsEmpty()
                ? scalarProcessor.ZeroScalar
                : scalarProcessor.SqrtOfAbs(scalarProcessor.Square(mv1.FirstScalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv1)
        {
            return mv1.IsEmpty()
                ? scalarProcessor.ZeroScalar
                : scalarProcessor.Square(mv1.FirstScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => ENorm(scalarProcessor, s1),
                IGaStorageVector<T> vt1 => ENorm(scalarProcessor, vt1),
                _ => ENormAsScalar(scalarProcessor, mv1)
            };
        }
        
        public static T VectorsENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var id = 1UL << index;
                var scalar1 = vector1[index];

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = GaBasisUtils.ENormSquaredSignature(id) > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }
        
        public static T VectorsENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var id = 1UL << index;
                var scalar1 = vector1[index];

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = GaBasisUtils.ENormSquaredSignature(id) > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        public static T ENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = 1UL << (int) index;
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = GaBasisUtils.ENormSquaredSignature(id) > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = 1UL << (int) index;
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = GaBasisUtils.ENormSquaredSignature(id) > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        private static T ENormAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1)
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
                    GaBasisUtils.ENormSquaredSignature(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        private static T ENormSquaredAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1)
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
                    GaBasisUtils.ENormSquaredSignature(id);

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
        public static T ENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => ENormSquared(scalarProcessor, s1),
                IGaStorageVector<T> vt1 => ENormSquared(scalarProcessor, vt1),
                _ => ENormSquaredAsScalar(scalarProcessor, mv1)
            };
        }

        private static T ENormAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarDictionary1 = mv1.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarDictionary1)
            {
                var signature = 
                    GaBasisUtils.ENormSquaredSignature(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        private static T ENormSquaredAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarDictionary1 = mv1.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarDictionary1)
            {
                var signature = 
                    GaBasisUtils.ENormSquaredSignature(id);

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
        public static T ENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => ENorm(scalarProcessor, s1),
                IGaStorageVector<T> vt1 => ENorm(scalarProcessor, vt1),
                IGaStorageKVector<T> kvt1 => ENorm(scalarProcessor, kvt1),
                _ => ENormAsScalar(scalarProcessor, mv1)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => ENormSquared(scalarProcessor, s1),
                IGaStorageVector<T> vt1 => ENormSquared(scalarProcessor, vt1),
                IGaStorageKVector<T> kvt1 => ENormSquared(scalarProcessor, kvt1),
                _ => ENormSquaredAsScalar(scalarProcessor, mv1)
            };
        }
    }
}