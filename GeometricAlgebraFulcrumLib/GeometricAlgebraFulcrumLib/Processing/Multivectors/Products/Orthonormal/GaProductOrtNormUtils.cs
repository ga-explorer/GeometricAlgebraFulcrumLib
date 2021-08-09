using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal
{
    public static class GaProductOrtNormUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult NormSquared(this IGaSignature signature, ulong id1)
        {
            return new GaBasisBilinearProductResult(
                signature.NormSquaredSignature(id1), 
                0
            );
        }

        public static double Norm(this GaSignatureLookup signature, IGaStorageMultivector<double> mv1)
        {
            if (!signature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var sig = signature.NormSquaredSignature(id1);

                if (sig == 0)
                    continue;

                var scalar = scalar1 * scalar1;

                spScalar = sig > 0
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return Math.Sqrt(Math.Abs(spScalar));
        }

        public static double NormSquared(this GaSignatureLookup signature, IGaStorageMultivector<double> mv1)
        {
            if (!signature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var sig = signature.NormSquaredSignature(id1);

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
        public static T Norm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageScalar<T> mv1)
        {
            return scalarProcessor.SqrtOfAbs(
                scalarProcessor.Times(
                    mv1.GetScalar(scalarProcessor), 
                    mv1.GetScalar(scalarProcessor)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageScalar<T> mv1)
        {
            return scalarProcessor.Square(
                mv1.GetScalar(scalarProcessor)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageKVector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => 
                    Norm(scalarProcessor, signature, s1),

                IGaStorageVector<T> vt1 => 
                    Norm(scalarProcessor, signature, vt1),

                _ => 
                    NormAsScalar(scalarProcessor, signature, mv1)
            };
        }
        
        public static T VectorsNorm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var sig = signature.NormSquaredSignature(1UL << index);

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
        
        public static T VectorsNormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var sig = signature.NormSquaredSignature(1UL << index);

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
        public static T Norm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageVector<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var sig = signature.NormSquaredSignature(1UL << (int) index);

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
        public static T NormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageVector<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var sig = signature.NormSquaredSignature(1UL << (int) index);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        private static T NormAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageKVector<T> mv1)
        {
            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ZeroScalar;
            
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var sig = signature.NormSquaredSignature(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        private static T NormSquaredAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageKVector<T> mv1)
        {
            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ZeroScalar;
            
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var sig = signature.NormSquaredSignature(id);

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
        public static T NormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageKVector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => NormSquared(scalarProcessor, signature, s1),
                IGaStorageVector<T> vt1 => NormSquared(scalarProcessor, signature, vt1),
                _ => NormSquaredAsScalar(scalarProcessor, signature, mv1)
            };
        }

        private static T NormAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivector<T> mv1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarDictionary1 = mv1.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarDictionary1)
            {
                var sig = signature.NormSquaredSignature(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        private static T NormSquaredAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivector<T> mv1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarDictionary1 = mv1.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarDictionary1)
            {
                var sig = signature.NormSquaredSignature(id);

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
        public static T Norm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => Norm(scalarProcessor, signature, s1),
                IGaStorageVector<T> vt1 => Norm(scalarProcessor, signature, vt1),
                IGaStorageKVector<T> kvt1 => Norm(scalarProcessor, signature, kvt1),
                _ => NormAsScalar(scalarProcessor, signature, mv1)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => NormSquared(scalarProcessor, signature, s1),
                IGaStorageVector<T> vt1 => NormSquared(scalarProcessor, signature, vt1),
                IGaStorageKVector<T> kvt1 => NormSquared(scalarProcessor, signature, kvt1),
                _ => NormSquaredAsScalar(scalarProcessor, signature, mv1)
            };
        }
        
    }
}