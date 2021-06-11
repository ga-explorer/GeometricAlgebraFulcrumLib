using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraLib.Multivectors.Bases
{
    public static class GaBasisUtils
    {
        public static IEnumerable<GaBasisUniform> IdToBasisUniform(this IEnumerable<ulong> idList)
        {
            return idList.Select(id => new GaBasisUniform(id));
        }

        public static IEnumerable<GaBasisGraded> IdToBasisGraded(this IEnumerable<ulong> idList)
        {
            return idList.Select(id => new GaBasisGraded(id));
        }

        public static IEnumerable<GaBasisFull> IdToBasisFull(this IEnumerable<ulong> idList)
        {
            return idList.Select(id => new GaBasisFull(id));
        }


        public static IEnumerable<GaBasisUniform> IndexToBasisUniform(this IEnumerable<ulong> indexList, int grade)
        {
            return indexList.Select(index => new GaBasisUniform(grade, index));
        }

        public static IEnumerable<GaBasisGraded> IndexToBasisGraded(this IEnumerable<ulong> indexList, int grade)
        {
            return indexList.Select(index => new GaBasisGraded(grade, index));
        }

        public static IEnumerable<GaBasisFull> IndexToBasisFull(this IEnumerable<ulong> indexList, int grade)
        {
            return indexList.Select(index => new GaBasisFull(grade, index));
        }

        public static IEnumerable<GaBasisVector> IndexToBasisVector(this IEnumerable<ulong> indexList)
        {
            return indexList.Select(index => new GaBasisVector(index));
        }

        public static IEnumerable<GaBasisBivector> IndexToBasisBivector(this IEnumerable<ulong> indexList)
        {
            return indexList.Select(index => new GaBasisBivector(index));
        }


        public static IEnumerable<GaBasisUniform> GradeIndexToBasisUniform(this IEnumerable<Tuple<int, ulong>> gradeIndexList)
        {
            return gradeIndexList.Select(tuple => new GaBasisUniform(tuple.Item1, tuple.Item2));
        }

        public static IEnumerable<GaBasisGraded> GradeIndexToBasisGraded(this IEnumerable<Tuple<int, ulong>> gradeIndexList)
        {
            return gradeIndexList.Select(tuple => new GaBasisGraded(tuple.Item1, tuple.Item2));
        }

        public static IEnumerable<GaBasisFull> GradeIndexToBasisFull(this IEnumerable<Tuple<int, ulong>> gradeIndexList)
        {
            return gradeIndexList.Select(tuple => new GaBasisFull(tuple.Item1, tuple.Item2));
        }


    }
}