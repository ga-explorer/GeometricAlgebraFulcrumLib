using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructuresLib;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Storage;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.TextComposers
{
    public static class GaTextComposersUtils
    {
        public static string GetBasisVectorText(this ulong index)
        {
            return GetBasisBladeText(
                new []{1UL << (int) (index + 1)}
            );
        }

        public static string GetBasisBladeText(this ulong id)
        {
            return GetBasisBladeText(
                id.BasisVectorIndexesInside().Select(i => i + 1)
            );
        }

        public static string GetBasisBladeText(uint grade, ulong index)
        {
            //return $"<Grade:{grade}, Index:{index}>";
            return GetBasisBladeText(
                index.IndexToCombinadic((int) grade).Select(i => (ulong) (i + 1))
            );
        }

        public static string GetBasisBladeText(this IGaBasisBlade basisBlade)
        {
            return GetBasisBladeText(basisBlade.Id);
        }

        public static string GetBasisBladeText(this IEnumerable<ulong> indexList)
        {
            return indexList.Concatenate(", ", "<", ">");
        }

        public static string GetScalarText<T>(this T scalar)
        {
            return scalar.ToString();
        }

        public static string GetTermText<T>(ulong id, T scalar)
        {
            return new StringBuilder()
                .Append($"'{scalar}'")
                .Append(GetBasisBladeText(id))
                .ToString();
        }

        public static string GetTermText<T>(uint grade, int index, T scalar)
        {
            return GetTermText(grade, (ulong) index, scalar);
        }

        public static string GetTermText<T>(uint grade, ulong index, T scalar)
        {
            return new StringBuilder()
                .Append($"'{GetScalarText(scalar)}'")
                .Append(GetBasisBladeText(grade, index))
                .ToString();
        }

        public static string GetTermText<T>(this KeyValuePair<ulong, T> idScalarPair)
        {
            return GetTermText(
                idScalarPair.Key, 
                idScalarPair.Value
            );
        }

        public static string GetTermText<T>(this Tuple<ulong, T> idScalarTuple)
        {
            return GetTermText(
                idScalarTuple.Item1, 
                idScalarTuple.Item2
            );
        }

        public static string GetTermText<T>(this Tuple<uint, ulong, T> gradeIndexScalarTuple)
        {
            return GetTermText(
                gradeIndexScalarTuple.Item1, 
                gradeIndexScalarTuple.Item2,
                gradeIndexScalarTuple.Item3
            );
        }

        public static string GetTermText<T>(IGaBasisBlade basisBlade, T scalar)
        {
            return GetTermText(
                basisBlade.Id,
                scalar
            );
        }

        public static string GetTermText<T>(this GaTerm<T> term)
        {
            return GetTermText(
                term.BasisBlade.Id,
                term.Scalar
            );
        }

        public static string GetTermsText<T>(this IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            return idScalarPairs
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        public static string GetTermsText<T>(this IEnumerable<Tuple<ulong, T>> idScalarTuples)
        {
            return idScalarTuples
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        public static string GetTermsText<T>(this IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarTuples)
        {
            return gradeIndexScalarTuples
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        public static string GetTermsText<T>(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            return indexScalarPairs
                .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Key, indexScalarPair.Value))
                .ConcatenateText(", ");
        }

        public static string GetTermsText<T>(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            return indexScalarTuples
                .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Item1, indexScalarPair.Item2))
                .ConcatenateText(", ");
        }

        public static string GetTermsText<T>(this IEnumerable<GaTerm<T>> terms)
        {
            return terms
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        public static string GetArrayText<T>(this T[] array)
        {
            var composer = new StringBuilder();

            var scalarsCount = array.Length;

            for (var i = 0; i < scalarsCount; i++)
            {
                if (i > 0)
                    composer.Append(", ");

                composer.Append(GetScalarText(array[i]));
            }

            return composer.ToString();
        }

        public static string GetArrayText<T>(this T[,] array)
        {
            var composer = new StringBuilder();

            var rowsCount = array.GetLength(0);
            var colsCount = array.GetLength(1);

            for (var i = 0; i < rowsCount; i++)
            {
                if (i > 0)
                    composer.AppendLine();

                for (var j = 0; j < colsCount; j++)
                {
                    if (j > 0)
                        composer.Append(", ");

                    composer.Append(GetScalarText(array[i, j]));
                }
            }

            return composer.ToString();
        }

        public static string GetMultivectorText<T>(this IGasMultivector<T> storage)
        {
            return GetTermsText(
                storage
                    .GetNotZeroTerms()
                    .OrderByGradeIndex()
            );
        }
    }
}