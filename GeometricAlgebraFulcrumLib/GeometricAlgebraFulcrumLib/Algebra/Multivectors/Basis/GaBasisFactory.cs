using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Combinations;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public static class GaBasisFactory
    {
        public static GaBasisScalar BasisScalar { get; }
            = new GaBasisScalar();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisVector CreateBasisVector(this int index)
        {
            Debug.Assert(index >= 0);

            return new GaBasisVector((ulong) index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisVector CreateBasisVector(this ulong index)
        {
            return new GaBasisVector(index);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBivector CreateBasisBivector(int basisVectorIndex1, int basisVectorIndex2)
        {
            Debug.Assert(basisVectorIndex1 >= 0 && basisVectorIndex2 >= 0);

            return new GaBasisBivector(
                (ulong) basisVectorIndex1, 
                (ulong) basisVectorIndex2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBivector CreateBasisBivector(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            return new GaBasisBivector(
                basisVectorIndex1, 
                basisVectorIndex2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBivector CreateBasisBivector(this int index)
        {
            Debug.Assert(index >= 0);

            var (basisVectorIndex1, basisVectorIndex2) = 
                BinaryCombinationsUtilsUInt64.IndexToCombinadic((ulong) index);

            return new GaBasisBivector(
                basisVectorIndex1, 
                basisVectorIndex2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBivector CreateBasisBivector(this ulong index)
        {
            var (basisVectorIndex1, basisVectorIndex2) = 
                BinaryCombinationsUtilsUInt64.IndexToCombinadic(index);

            return new GaBasisBivector(
                basisVectorIndex1, 
                basisVectorIndex2
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisUniform CreateUniformBasisBlade(this ulong id)
        {
            return new GaBasisUniform(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisUniform CreateUniformBasisBlade(this uint grade, int index)
        {
            Debug.Assert(index >= 0);

            var id = GaBasisUtils.BasisBladeId(grade, (ulong) index);

            return new GaBasisUniform(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisUniform CreateUniformBasisBlade(this uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return new GaBasisUniform(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisUniform ToUniformBasisBlade(this IGaBasisBlade basisBlade)
        {
            return basisBlade is GaBasisUniform uniformBasisBlade
                ? uniformBasisBlade
                : new GaBasisUniform(basisBlade.Id);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisGraded CreateGradedBasisBlade(this ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return new GaBasisGraded(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisGraded CreateGradedBasisBlade(this uint grade, int index)
        {
            Debug.Assert(index >= 0);

            return new GaBasisGraded(grade, (ulong) index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisGraded CreateGradedBasisBlade(this uint grade, ulong index)
        {
            return new GaBasisGraded(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisGraded ToGradedBasisBlade(this IGaBasisBlade basisBlade)
        {
            if (basisBlade is GaBasisGraded gradedBasisBlade)
                return gradedBasisBlade;

            basisBlade.GetGradeIndex(out var grade, out var index);

            return new GaBasisGraded(grade, index);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisFull CreateFullBasisBlade(this ulong id)
        {
            return new GaBasisFull(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisFull CreateFullBasisBlade(this uint grade, int index)
        {
            Debug.Assert(index >= 0);

            return new GaBasisFull(grade, (ulong) index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisFull CreateFullBasisBlade(this uint grade, ulong index)
        {
            return new GaBasisFull(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisFull ToFullBasisBlade(this IGaBasisBlade basisBlade)
        {
            return basisBlade is GaBasisFull fullBasisBlade
                ? fullBasisBlade
                : new GaBasisFull(basisBlade.Id);
        }


        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisUniform> IdListToUniformBasisBlades(this IEnumerable<ulong> idList)
        {
            return idList.Select(CreateUniformBasisBlade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisGraded> IdListToGradedBasisBlades(this IEnumerable<ulong> idList)
        {
            return idList.Select(CreateGradedBasisBlade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisFull> IdListToFullBasisBlades(this IEnumerable<ulong> idList)
        {
            return idList.Select(id => id.CreateFullBasisBlade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisUniform> IndexListToUniformBasisBlades(this IEnumerable<int> indexList, uint grade)
        {
            return indexList.Select(index => grade.CreateUniformBasisBlade(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisUniform> IndexListToUniformBasisBlades(this IEnumerable<ulong> indexList, uint grade)
        {
            return indexList.Select(index => grade.CreateUniformBasisBlade(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisUniform> IndexListToUniformBasisBlades(this uint grade, IEnumerable<int> indexList)
        {
            return indexList.Select(index => grade.CreateUniformBasisBlade(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisUniform> IndexListToUniformBasisBlades(this uint grade, IEnumerable<ulong> indexList)
        {
            return indexList.Select(index => grade.CreateUniformBasisBlade(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisGraded> IndexListToGradedBasisBlades(this uint grade, IEnumerable<int> indexList)
        {
            return indexList.Select(index => grade.CreateGradedBasisBlade(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisGraded> IndexListToGradedBasisBlades(this uint grade, IEnumerable<ulong> indexList)
        {
            return indexList.Select(index => grade.CreateGradedBasisBlade(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisGraded> IndexListToGradedBasisBlades(this IEnumerable<int> indexList, uint grade)
        {
            return indexList.Select(index => grade.CreateGradedBasisBlade(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisGraded> IndexListToGradedBasisBlades(this IEnumerable<ulong> indexList, uint grade)
        {
            return indexList.Select(index => grade.CreateGradedBasisBlade(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisFull> IndexListToFullBasisBlades(this uint grade, IEnumerable<int> indexList)
        {
            return indexList.Select(index => grade.CreateFullBasisBlade(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisFull> IndexListToFullBasisBlades(this uint grade, IEnumerable<ulong> indexList)
        {
            return indexList.Select(index => grade.CreateFullBasisBlade(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisFull> IndexListToFullBasisBlades(this IEnumerable<int> indexList, uint grade)
        {
            return indexList.Select(index => grade.CreateFullBasisBlade(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisFull> IndexListToFullBasisBlades(this IEnumerable<ulong> indexList, uint grade)
        {
            return indexList.Select(index => grade.CreateFullBasisBlade(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisVector> IndexListToBasisVectors(this IEnumerable<int> indexList)
        {
            return indexList.Select(CreateBasisVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisVector> IndexListToBasisVectors(this IEnumerable<ulong> indexList)
        {
            return indexList.Select(CreateBasisVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisBivector> IndexListToBasisBivectors(this IEnumerable<int> indexList)
        {
            return indexList.Select(index => CreateBasisBivector(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisBivector> IndexListToBasisBivectors(this IEnumerable<ulong> indexList)
        {
            return indexList.Select(CreateBasisBivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisUniform> GradeIndexTuplesToUniformBasisBlades(this IEnumerable<Tuple<uint, ulong>> gradeIndexList)
        {
            return gradeIndexList.Select(tuple => tuple.Item1.CreateUniformBasisBlade(tuple.Item2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisGraded> GradeIndexTuplesToGradedBasisBlades(this IEnumerable<Tuple<uint, ulong>> gradeIndexList)
        {
            return gradeIndexList.Select(tuple => tuple.Item1.CreateGradedBasisBlade(tuple.Item2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisFull> GradeIndexTuplesToFullBasisBlades(this IEnumerable<Tuple<uint, ulong>> gradeIndexList)
        {
            return gradeIndexList.Select(tuple => tuple.Item1.CreateFullBasisBlade(tuple.Item2));
        }
    }
}