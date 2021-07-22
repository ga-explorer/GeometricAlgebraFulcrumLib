using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public static class GaStorageFactory
    {
        public static IGasVector<T> CopyToVector<T>(this IDictionary<ulong, T> indexScalarDictionary, IGaScalarProcessor<T> scalarProcessor)
        {
            var termsCount = 
                indexScalarDictionary.Count;

            if (termsCount == 0)
                return scalarProcessor.CreateZeroVector();

            if (termsCount > 1)
                return new GasVector<T>(
                    scalarProcessor,
                    indexScalarDictionary.CopyToDictionary(),
                    indexScalarDictionary.Keys.GetMaxBasisBladeId(1)
                );

            var (index, scalar) = 
                indexScalarDictionary.First();

            return scalarProcessor.CreateVector(index, scalar);
        }

        public static IGasBivector<T> CopyToBivector<T>(this IDictionary<ulong, T> indexScalarDictionary, IGaScalarProcessor<T> scalarProcessor)
        {
            var termsCount = 
                indexScalarDictionary.Count;

            if (termsCount == 0)
                return scalarProcessor.CreateZeroBivector();

            if (termsCount > 1)
                return new GasBivector<T>(
                    scalarProcessor,
                    indexScalarDictionary.CopyToDictionary(),
                    indexScalarDictionary.Keys.GetMaxBasisBladeId(2)
                );

            var (index, scalar) = 
                indexScalarDictionary.First();

            return scalarProcessor.CreateBivector(index, scalar);
        }

        public static IGasKVector<T> CopyToKVector<T>(this IDictionary<ulong, T> indexScalarDictionary, uint grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var termsCount = 
                indexScalarDictionary.Count;

            if (termsCount == 0)
                return scalarProcessor.CreateZeroKVector(grade);

            if (termsCount > 1)
                return scalarProcessor.CreateKVector(
                    grade, 
                    indexScalarDictionary.CopyToDictionary()
                );

            var (index, scalar) = 
                indexScalarDictionary.First();

            return scalarProcessor.CreateKVector(index, scalar);
        }

        public static IGasTermsMultivector<T> CopyToTermsMultivector<T>(this IDictionary<ulong, T> idScalarDictionary, IGaScalarProcessor<T> scalarProcessor)
        {
            var termsCount = 
                idScalarDictionary.Count;

            if (termsCount == 0)
                return scalarProcessor.CreateZeroScalar();

            if (termsCount > 1)
                return new GasTermsMultivector<T>(
                    scalarProcessor,
                    idScalarDictionary.CopyToDictionary(),
                    idScalarDictionary.Keys.GetMaxBasisBladeId()
                );

            var (id, scalar) = 
                idScalarDictionary.First();

            return scalarProcessor.CreateKVector(id, scalar);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> SumToScalar<T>(this IGaScalarProcessor<T> scalarProcessor, params T[] scalars)
        {
            return new GasScalar<T>(
                scalarProcessor,
                scalarProcessor.Add(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> SumToScalar<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return new GasScalar<T>(
                scalarProcessor,
                scalarProcessor.Add(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> SumToScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalars)
        {
            return new GasScalar<T>(
                scalarProcessor,
                scalarProcessor.Add(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasScalar<T> CreateZeroScalar<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GasScalar<T>(
                scalarProcessor,
                scalarProcessor.ZeroScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasScalar<T> CreateBasisScalar<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GasScalar<T>(
                scalarProcessor, 
                scalarProcessor.OneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasScalar<T> CreateBasisScalarNegative<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GasScalar<T>(
                scalarProcessor, 
                scalarProcessor.MinusOneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasScalar<T> CreateScalar<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar)
        {
            return new GasScalar<T>(
                scalarProcessor, 
                scalar
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasVectorTerm<T> CreateZeroVector<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GasVectorTerm<T>(
                scalarProcessor,
                0.CreateBasisVector(),
                scalarProcessor.ZeroScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasVectorTerm<T> CreateVector<T>(this IGaScalarProcessor<T> scalarProcessor, int index, T scalar)
        {
            return new GasVectorTerm<T>(
                scalarProcessor,
                index.CreateBasisVector(),
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasVectorTerm<T> CreateVector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index, T scalar)
        {
            return new GasVectorTerm<T>(
                scalarProcessor,
                index.CreateBasisVector(),
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasVectorTerm<T> CreateVector<T>(this IGaScalarProcessor<T> scalarProcessor, KeyValuePair<ulong, T> indexScalarPair)
        {
            return new GasVectorTerm<T>(
                scalarProcessor,
                indexScalarPair.Key.CreateBasisVector(),
                indexScalarPair.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasVectorTerm<T> CreateBasisVector<T>(this IGaScalarProcessor<T> scalarProcessor, int index)
        {
            return new GasVectorTerm<T>(
                scalarProcessor,
                index.CreateBasisVector(),
                scalarProcessor.OneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasVectorTerm<T> CreateBasisVector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index)
        {
            return new GasVectorTerm<T>(
                scalarProcessor,
                index.CreateBasisVector(),
                scalarProcessor.OneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasVectorTerm<T> CreateBasisVectorNegative<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index)
        {
            return new GasVectorTerm<T>(
                scalarProcessor,
                index.CreateBasisVector(),
                scalarProcessor.MinusOneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasVector<T> CreateOnesVector<T>(this IGaScalarProcessor<T> scalarProcessor, int termsCount)
        {
            return new GasVector<T>(
                scalarProcessor,
                Enumerable.Range(0, termsCount).ToDictionary(
                    i => (ulong) i,
                    _ => scalarProcessor.OneScalar
                ),
                1UL << (termsCount - 1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasVector<T> CreateUnitOnesVector<T>(this IGaScalarProcessor<T> scalarProcessor, int termsCount)
        {
            var length = scalarProcessor.Sqrt(termsCount);

            return new GasVector<T>(
                scalarProcessor,
                Enumerable.Range(0, termsCount).ToDictionary(
                    i => (ulong) i,
                    _ => scalarProcessor.Divide(scalarProcessor.OneScalar, length)
                ),
                1UL << (termsCount - 1)
            );
        }
        
        public static IGasVector<T> CreateVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint termsCount, Func<ulong, T> indexScalarFunc)
        {
            if (termsCount == 0)
                return scalarProcessor.CreateZeroVector();

            if (termsCount > 1)
            {
                var indexScalarDictionary =
                    ((ulong) termsCount).RangeToDictionary(indexScalarFunc);

                return new GasVector<T>(
                    scalarProcessor,
                    indexScalarDictionary,
                    indexScalarDictionary.Keys.GetMaxBasisBladeId(1)
                );
            }

            return new GasVectorTerm<T>(
                scalarProcessor,
                0.CreateBasisVector(),
                indexScalarFunc(0)
            );
        }
        
        public static IGasVector<T> CreateVector<T>(this IGaScalarProcessor<T> scalarProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            var termsCount = indexScalarDictionary.Count;

            if (termsCount == 0)
                return scalarProcessor.CreateZeroVector();

            if (termsCount > 1)
                return new GasVector<T>(
                    scalarProcessor,
                    indexScalarDictionary,
                    indexScalarDictionary.Keys.GetMaxBasisBladeId(1)
                );

            var (index, scalar) = 
                indexScalarDictionary.First();

            return new GasVectorTerm<T>(
                scalarProcessor,
                index.CreateBasisVector(),
                scalar
            );
        }

        public static IGasVector<T> CreateVector<T>(this IGaScalarProcessor<T> scalarProcessor, params T[] scalarArray)
        {
            var indexScalarDictionary = new Dictionary<ulong, T>();

            for (var i = 0; i < scalarArray.Length; i++)
            {
                var scalar = scalarArray[i];

                if (!scalarProcessor.IsZero(scalar))
                    indexScalarDictionary.Add((ulong) i, scalar);
            }

            return scalarProcessor.CreateVector(indexScalarDictionary);
        }

        public static IGasVector<T> CreateVector<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> scalarList)
        {
            var indexScalarDictionary = new Dictionary<ulong, T>();

            for (var i = 0; i < scalarList.Count; i++)
            {
                var scalar = scalarList[i];

                if (!scalarProcessor.IsZero(scalar))
                    indexScalarDictionary.Add((ulong) i, scalar);
            }

            return scalarProcessor.CreateVector(indexScalarDictionary);
        }
        
        public static IGasVector<T> CreateVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(scalarsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> CreateVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> CreateVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> CreateVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaTerm<T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> SumToVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> SumToVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> SumToVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaTerm<T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> CreateVector<T>(this IEnumerable<T> scalarsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(scalarsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> CreateVector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> CreateVector<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> CreateVector<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> SumToVector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> SumToVector<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public static IGasVector<T> SumToVector<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateBivector<T>(this IGaScalarProcessor<T> scalarProcessor, int index1, int index2, T scalar)
        {
            Debug.Assert(index1 != index2);

            if (index1 < index2)
                return new GasBivectorTerm<T>(
                    scalarProcessor,
                    GaBasisFactory.CreateBasisBivector(index1, index2),
                    scalar
                );

            return new GasBivectorTerm<T>(
                scalarProcessor,
                GaBasisFactory.CreateBasisBivector(index2, index1),
                scalarProcessor.Negative(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateBivector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index1, ulong index2, T scalar)
        {
            Debug.Assert(index1 != index2);

            if (index1 < index2)
                return new GasBivectorTerm<T>(
                    scalarProcessor,
                    GaBasisFactory.CreateBasisBivector(index1, index2),
                    scalar
                );

            return new GasBivectorTerm<T>(
                scalarProcessor,
                GaBasisFactory.CreateBasisBivector(index2, index1),
                scalarProcessor.Negative(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateBivector<T>(this IGaScalarProcessor<T> scalarProcessor, KeyValuePair<ulong, T> indexScalarPair)
        {
            return new GasBivectorTerm<T>(
                scalarProcessor,
                indexScalarPair.Key.CreateBasisBivector(),
                indexScalarPair.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateBivector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index, T scalar)
        {
            return new GasBivectorTerm<T>(
                scalarProcessor,
                index.CreateBasisBivector(),
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateBasisBivector<T>(this IGaScalarProcessor<T> scalarProcessor, int index1, int index2)
        {
            Debug.Assert(index1 != index2);

            if (index1 < index2)
                return new GasBivectorTerm<T>(
                    scalarProcessor,
                    GaBasisFactory.CreateBasisBivector(index1, index2),
                    scalarProcessor.OneScalar
                );

            return new GasBivectorTerm<T>(
                scalarProcessor,
                GaBasisFactory.CreateBasisBivector(index2, index1),
                scalarProcessor.MinusOneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateBasisBivector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index1, ulong index2)
        {
            Debug.Assert(index1 != index2);

            if (index1 < index2)
                return new GasBivectorTerm<T>(
                    scalarProcessor,
                    GaBasisFactory.CreateBasisBivector(index1, index2),
                    scalarProcessor.OneScalar
                );

            return new GasBivectorTerm<T>(
                scalarProcessor,
                GaBasisFactory.CreateBasisBivector(index2, index1),
                scalarProcessor.MinusOneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateBasisBivector<T>(this IGaScalarProcessor<T> scalarProcessor, int index)
        {
            return new GasBivectorTerm<T>(
                scalarProcessor,
                ((ulong) index).CreateBasisBivector(),
                scalarProcessor.OneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateBasisBivector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index)
        {
            return new GasBivectorTerm<T>(
                scalarProcessor,
                index.CreateBasisBivector(),
                scalarProcessor.OneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateBasisBivectorNegative<T>(this IGaScalarProcessor<T> scalarProcessor, int index1, int index2)
        {
            Debug.Assert(index1 != index2);

            if (index1 < index2)
                return new GasBivectorTerm<T>(
                    scalarProcessor,
                    GaBasisFactory.CreateBasisBivector(index1, index2),
                    scalarProcessor.MinusOneScalar
                );

            return new GasBivectorTerm<T>(
                scalarProcessor,
                GaBasisFactory.CreateBasisBivector(index2, index1),
                scalarProcessor.OneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateBasisBivectorNegative<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index1, ulong index2)
        {
            Debug.Assert(index1 != index2);

            if (index1 < index2)
                return new GasBivectorTerm<T>(
                    scalarProcessor,
                    GaBasisFactory.CreateBasisBivector(index1, index2),
                    scalarProcessor.MinusOneScalar
                );

            return new GasBivectorTerm<T>(
                scalarProcessor,
                GaBasisFactory.CreateBasisBivector(index2, index1),
                scalarProcessor.OneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateBasisBivectorNegative<T>(this IGaScalarProcessor<T> scalarProcessor, int index)
        {
            return new GasBivectorTerm<T>(
                scalarProcessor,
                ((ulong) index).CreateBasisBivector(),
                scalarProcessor.MinusOneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateBasisBivectorNegative<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index)
        {
            return new GasBivectorTerm<T>(
                scalarProcessor,
                index.CreateBasisBivector(),
                scalarProcessor.MinusOneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasBivectorTerm<T> CreateZeroBivector<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GasBivectorTerm<T>(
                scalarProcessor,
                0.CreateBasisBivector(),
                scalarProcessor.ZeroScalar
            );
        }
        
        public static IGasBivector<T> CreateBivector<T>(this IGaScalarProcessor<T> scalarProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            var termsCount = indexScalarDictionary.Count;

            if (termsCount == 0)
                return scalarProcessor.CreateZeroBivector();

            if (termsCount > 1)
                return new GasBivector<T>(
                    scalarProcessor,
                    indexScalarDictionary,
                    indexScalarDictionary.Keys.GetMaxBasisBladeId(2)
                );

            var (index, scalar) = 
                indexScalarDictionary.First();

            return scalarProcessor.CreateBivector(index, scalar);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> CreatePseudoScalar<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            var index = ((1UL << (int) vSpaceDimension) - 1).BasisBladeIndex();
            var scalar = scalarProcessor.OneScalar;

            return vSpaceDimension switch
            {
                0 => new GasScalar<T>(scalarProcessor, scalar),
                1 => new GasVectorTerm<T>(scalarProcessor, index.CreateBasisVector(), scalar),
                2 => new GasBivectorTerm<T>(scalarProcessor, index.CreateBasisBivector(), scalar),
                _ => new GasKVectorTerm<T>(scalarProcessor, vSpaceDimension.CreateGradedBasisBlade(index), scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> CreatePseudoScalarReverse<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            var index = ((1UL << (int) vSpaceDimension) - 1).BasisBladeIndex();
            var scalar = 
                vSpaceDimension.GradeHasNegativeReverse()
                ? scalarProcessor.MinusOneScalar 
                : scalarProcessor.OneScalar;

            return vSpaceDimension switch
            {
                0 => new GasScalar<T>(scalarProcessor, scalar),
                1 => new GasVectorTerm<T>(scalarProcessor, index.CreateBasisVector(), scalar),
                2 => new GasBivectorTerm<T>(scalarProcessor, index.CreateBasisBivector(), scalar),
                _ => new GasKVectorTerm<T>(scalarProcessor, vSpaceDimension.CreateGradedBasisBlade(index), scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> CreateEuclideanPseudoScalarInverse<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return CreatePseudoScalarReverse(scalarProcessor, vSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> CreatePseudoScalarInverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature baseSignature)
        {
            return baseSignature
                .BladeInverse(
                    scalarProcessor.CreatePseudoScalar(baseSignature.VSpaceDimension)
                ).GetKVectorPart(baseSignature.VSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> CreatePseudoScalarInverse<T>(this IGaSignature baseSignature, IGaScalarProcessor<T> scalarProcessor)
        {
            return baseSignature
                .BladeInverse(
                    scalarProcessor.CreatePseudoScalar(baseSignature.VSpaceDimension)
                ).GetKVectorPart(baseSignature.VSpaceDimension);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> CreateZeroKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade)
        {
            return grade switch
            {
                0 => new GasScalar<T>(scalarProcessor, scalarProcessor.ZeroScalar),
                1 => new GasVectorTerm<T>(scalarProcessor, 0.CreateBasisVector(), scalarProcessor.ZeroScalar),
                2 => new GasBivectorTerm<T>(scalarProcessor, 0.CreateBasisBivector(), scalarProcessor.ZeroScalar),
                _ => new GasKVectorTerm<T>(scalarProcessor, grade.CreateGradedBasisBlade(0), scalarProcessor.ZeroScalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasKVectorTerm<T> CreateBasisBlade<T>(this IGaScalarProcessor<T> scalarProcessor, ulong id)
        {
            return new GasKVectorTerm<T>(
                scalarProcessor,
                new GaBasisUniform(id),
                scalarProcessor.OneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasKVectorTerm<T> CreateBasisBlade<T>(IGaScalarProcessor<T> scalarProcessor, uint grade, ulong index)
        {
            return new GasKVectorTerm<T>(
                scalarProcessor,
                grade.CreateGradedBasisBlade(index),
                scalarProcessor.OneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> CreateKVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaBasisBlade basisBlade, T scalar)
        {
            var (grade, index) = basisBlade.GetGradeIndex();

            return grade switch
            {
                0 => new GasScalar<T>(scalarProcessor, scalar),
                1 => new GasVectorTerm<T>(scalarProcessor, index.CreateBasisVector(), scalar),
                2 => new GasBivectorTerm<T>(scalarProcessor, index.CreateBasisBivector(), scalar),
                _ => new GasKVectorTerm<T>(scalarProcessor, grade.CreateGradedBasisBlade(index), scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> CreateKVector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong id, T scalar)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => new GasScalar<T>(scalarProcessor, scalar),
                1 => new GasVectorTerm<T>(scalarProcessor, index.CreateBasisVector(), scalar),
                2 => new GasBivectorTerm<T>(scalarProcessor, index.CreateBasisBivector(), scalar),
                _ => new GasKVectorTerm<T>(scalarProcessor, grade.CreateGradedBasisBlade(index), scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> CreateKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, ulong index, T scalar)
        {
            return grade switch
            {
                0 => new GasScalar<T>(scalarProcessor, scalar),
                1 => new GasVectorTerm<T>(scalarProcessor, index.CreateBasisVector(), scalar),
                2 => new GasBivectorTerm<T>(scalarProcessor, index.CreateBasisBivector(), scalar),
                _ => new GasKVectorTerm<T>(scalarProcessor, grade.CreateGradedBasisBlade(index), scalar)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> CreateKVector<T>(this IGaScalarProcessor<T> scalarProcessor, KeyValuePair<ulong, T> idScalarPair)
        {
            var (id, scalar) = idScalarPair;

            id.BasisBladeGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => new GasScalar<T>(scalarProcessor, scalar),
                1 => new GasVectorTerm<T>(scalarProcessor, index.CreateBasisVector(), scalar),
                2 => new GasBivectorTerm<T>(scalarProcessor, index.CreateBasisBivector(), scalar),
                _ => new GasKVectorTerm<T>(scalarProcessor, grade.CreateGradedBasisBlade(index), scalar)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> CreateKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, KeyValuePair<ulong, T> indexScalarPair)
        {
            var (index, scalar) = indexScalarPair;

            return grade switch
            {
                0 => new GasScalar<T>(scalarProcessor, scalar),
                1 => new GasVectorTerm<T>(scalarProcessor, index.CreateBasisVector(), scalar),
                2 => new GasBivectorTerm<T>(scalarProcessor, index.CreateBasisBivector(), scalar),
                _ => new GasKVectorTerm<T>(scalarProcessor, grade.CreateGradedBasisBlade(index), scalar)
            };
        }

        public static IGasKVector<T> CreateKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<T> scalarsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(scalarsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public static IGasKVector<T> CreateKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public static IGasKVector<T> CreateKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<Tuple<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public static IGasKVector<T> CreateKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<GaTerm<T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public static IGasKVector<T> SumToKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public static IGasKVector<T> SumToKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<Tuple<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public static IGasKVector<T> SumToKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<GaTerm<T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }
        
        public static IGasKVector<T> CreateKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, Dictionary<ulong, T> indexScalarDictionary)
        {
            var termsCount = 
                indexScalarDictionary.Count;

            if (termsCount == 0)
                return scalarProcessor.CreateZeroKVector(grade);

            if (termsCount > 1) 
                return grade switch
                {
                    0 => new GasScalar<T>(scalarProcessor, indexScalarDictionary.TryGetValue(0, out var s) ? s : scalarProcessor.ZeroScalar),
                    1 => new GasVector<T>(scalarProcessor, indexScalarDictionary, indexScalarDictionary.Keys.GetMaxBasisBladeId(grade)),
                    2 => new GasBivector<T>(scalarProcessor, indexScalarDictionary, indexScalarDictionary.Keys.GetMaxBasisBladeId(grade)),
                    _ => new GasKVector<T>(scalarProcessor, grade, indexScalarDictionary, indexScalarDictionary.Keys.GetMaxBasisBladeId(grade))
                };

            var (index, scalar) = 
                indexScalarDictionary.First();

            return grade switch
            {
                0 => new GasScalar<T>(scalarProcessor, scalar),
                1 => new GasVectorTerm<T>(scalarProcessor, index.CreateBasisVector(), scalar),
                2 => new GasBivectorTerm<T>(scalarProcessor, index.CreateBasisBivector(), scalar),
                _ => new GasKVectorTerm<T>(scalarProcessor, grade.CreateGradedBasisBlade(index), scalar)
            };
        }

        public static IGasKVector<T> CreateKVector<T>(this IEnumerable<T> scalarsList, uint grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(scalarsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public static IGasKVector<T> CreateKVector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, uint grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public static IGasKVector<T> CreateKVector<T>(this IEnumerable<Tuple<ulong, T>> termsList, uint grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public static IGasKVector<T> CreateKVector<T>(this IEnumerable<GaTerm<T>> termsList, uint grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public static IGasKVector<T> SumToKVector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, uint grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public static IGasKVector<T> SumToKVector<T>(this IEnumerable<Tuple<ulong, T>> termsList, uint grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public static IGasKVector<T> SumToKVector<T>(this IEnumerable<GaTerm<T>> termsList, uint grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GasTreeMultivector<T> CreateTreeMultivector<T>(IGaScalarProcessor<T> scalarProcessor, Dictionary<ulong, T> idScalarDictionary)
        {
            return new GasTreeMultivector<T>(
                scalarProcessor,
                idScalarDictionary,
                idScalarDictionary.Keys.GetMaxBasisBladeId()
            );
        }
        
        public static IGasGradedMultivector<T> CreateGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, Dictionary<uint, Dictionary<ulong, T>> gradeIndexScalarDictionary)
        {
            var gradesCount = gradeIndexScalarDictionary.Count;

            if (gradesCount == 0)
                return scalarProcessor.CreateZeroScalar();

            if (gradesCount > 1) 
                return new GasGradedMultivector<T>(
                    scalarProcessor,
                    gradeIndexScalarDictionary,
                    gradeIndexScalarDictionary.GetMaxBasisBladeId()
                );

            var (grade, indexScalarDictionary) = 
                gradeIndexScalarDictionary.First();
                
            var termsCount = indexScalarDictionary.Count;

            if (termsCount == 0)
                return scalarProcessor.CreateZeroKVector(grade);

            if (termsCount > 1) 
                return grade switch
                {
                    0 => new GasScalar<T>(scalarProcessor, indexScalarDictionary.TryGetValue(0, out var s) ? s : scalarProcessor.ZeroScalar),
                    1 => new GasVector<T>(scalarProcessor, indexScalarDictionary, indexScalarDictionary.Keys.GetMaxBasisBladeId(grade)),
                    2 => new GasBivector<T>(scalarProcessor, indexScalarDictionary, indexScalarDictionary.Keys.GetMaxBasisBladeId(grade)),
                    _ => new GasKVector<T>(scalarProcessor, grade, indexScalarDictionary, indexScalarDictionary.Keys.GetMaxBasisBladeId(grade))
                };

            var (index, scalar) = 
                indexScalarDictionary.First();

            return grade switch
            {
                0 => new GasScalar<T>(scalarProcessor, scalar),
                1 => new GasVectorTerm<T>(scalarProcessor, index.CreateBasisVector(), scalar),
                2 => new GasBivectorTerm<T>(scalarProcessor, index.CreateBasisBivector(), scalar),
                _ => new GasKVectorTerm<T>(scalarProcessor, grade.CreateGradedBasisBlade(index), scalar)
            };
        }

        public static IGasGradedMultivector<T> CreateGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(scalarProcessor);

            composer.SetTerms(idScalarPairs);

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }

        public static IGasGradedMultivector<T> CreateGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> idScalarTuples)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(scalarProcessor);

            composer.SetTerms(idScalarTuples);

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }

        public static IGasGradedMultivector<T> CreateGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarTuples)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(scalarProcessor);

            composer.SetTerms(gradeIndexScalarTuples);

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }

        public static IGasGradedMultivector<T> SumToGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(scalarProcessor);

            composer.AddTerms(idScalarPairs);

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }

        public static IGasGradedMultivector<T> SumToGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> idScalarTuples)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(scalarProcessor);

            composer.AddTerms(idScalarTuples);

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }

        public static IGasGradedMultivector<T> SumToGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarTuples)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(scalarProcessor);

            composer.AddTerms(gradeIndexScalarTuples);

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }


        public static IGasTermsMultivector<T> CreateTermsMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, Dictionary<ulong, T> idScalarDictionary)
        {
            var termsCount = idScalarDictionary.Count;

            if (termsCount == 0)
                return scalarProcessor.CreateZeroScalar();

            if (termsCount > 1)
                return new GasTermsMultivector<T>(
                    scalarProcessor,
                    idScalarDictionary,
                    idScalarDictionary.Keys.GetMaxBasisBladeId()
                );

            var (id, scalar) = 
                idScalarDictionary.First();

            id.BasisBladeGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => new GasScalar<T>(scalarProcessor, scalar),
                1 => new GasVectorTerm<T>(scalarProcessor, index.CreateBasisVector(), scalar),
                2 => new GasBivectorTerm<T>(scalarProcessor, index.CreateBasisBivector(), scalar),
                _ => new GasKVectorTerm<T>(scalarProcessor, grade.CreateGradedBasisBlade(index), scalar)
            };
        }

        public static IGasTermsMultivector<T> CreateTermsMultivector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetCompactTermsStorage();
        }

        public static IGasTermsMultivector<T> CreateTermsMultivector<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetCompactTermsStorage();
        }

        public static IGasTermsMultivector<T> CreateTermsMultivector<T>(this IEnumerable<Tuple<uint, ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetCompactTermsStorage();
        }

        public static IGasTermsMultivector<T> CreateTermsMultivector<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetCompactTermsStorage();
        }

        public static IGasTermsMultivector<T> SumToTermsMultivector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetCompactTermsStorage();
        }

        public static IGasTermsMultivector<T> SumToTermsMultivector<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetCompactTermsStorage();
        }

        public static IGasTermsMultivector<T> SumToTermsMultivector<T>(this IEnumerable<Tuple<uint, ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetCompactTermsStorage();
        }

        public static IGasTermsMultivector<T> SumToTermsMultivector<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetCompactTermsStorage();
        }


        public static GasTreeMultivector<T> CreateTreeMultivector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivectorCopy();
        }

        public static GasTreeMultivector<T> CreateTreeMultivector<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivectorCopy();
        }

        public static GasTreeMultivector<T> CreateTreeMultivector<T>(this IEnumerable<Tuple<uint, ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivectorCopy();
        }

        public static GasTreeMultivector<T> CreateTreeMultivector<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivectorCopy();
        }

        public static GasTreeMultivector<T> SumToTreeMultivector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivectorCopy();
        }

        public static GasTreeMultivector<T> SumToTreeMultivector<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivectorCopy();
        }

        public static GasTreeMultivector<T> SumToTreeMultivector<T>(this IEnumerable<Tuple<uint, ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivectorCopy();
        }

        public static GasTreeMultivector<T> SumToTreeMultivector<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivectorCopy();
        }
    }
}