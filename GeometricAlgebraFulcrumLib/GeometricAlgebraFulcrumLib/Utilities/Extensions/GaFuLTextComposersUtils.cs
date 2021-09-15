using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructuresLib;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaFuLTextComposersUtils
    {
        public static string GetBasisVectorText(this ulong index)
        {
            return GetBasisBladeText(
                new []{index + 1}
            );
        }

        public static string GetBasisBladeText(this ulong id)
        {
            return GetBasisBladeText(
                id.BasisBladeIdToBasisVectorIndices().Select(i => i + 1)
            );
        }

        public static string GetBasisBladeText(uint grade, ulong index)
        {
            //return $"<Grade:{grade}, Index:{index}>";
            return GetBasisBladeText(
                index.IndexToCombinadic((int) grade).Select(i => (ulong) (i + 1))
            );
        }

        public static string GetBasisBladeText(this BasisBlade basisBlade)
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

        public static string GetTermText<T>(this IndexScalarRecord<T> idScalarTuple)
        {
            return GetTermText(
                idScalarTuple.Index, 
                idScalarTuple.Scalar
            );
        }

        public static string GetTermText<T>(this GradeIndexScalarRecord<T> gradeIndexScalarTuple)
        {
            return GetTermText(
                gradeIndexScalarTuple.Grade, 
                gradeIndexScalarTuple.Index,
                gradeIndexScalarTuple.Scalar
            );
        }

        public static string GetTermText<T>(BasisBlade basisBlade, T scalar)
        {
            return GetTermText(
                basisBlade.Id,
                scalar
            );
        }

        public static string GetTermText<T>(this BasisTerm<T> term)
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

        public static string GetTermsText<T>(this IEnumerable<IndexScalarRecord<T>> idScalarTuples)
        {
            return idScalarTuples
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        public static string GetTermsText<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
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

        public static string GetTermsText<T>(uint grade, IEnumerable<IndexScalarRecord<T>> indexScalarTuples)
        {
            return indexScalarTuples
                .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Index, indexScalarPair.Scalar))
                .ConcatenateText(", ");
        }

        public static string GetTermsText<T>(this IEnumerable<BasisTerm<T>> terms)
        {
            return terms
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        public static string GetArrayText<T>(this IReadOnlyList<T> array)
        {
            var composer = new StringBuilder();

            var scalarsCount = array.Count;

            for (var i = 0; i < scalarsCount; i++)
            {
                if (i > 0)
                    composer.Append(", ");

                composer.Append(GetScalarText(array[i]));
            }

            return composer.ToString();
        }

        public static string GetArrayText<T>(this ILinVectorStorage<T> array)
        {
            return GetArrayText(array.ToArray());
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

        public static string GetArrayText<T>(this ILinMatrixStorage<T> matrixStorage)
        {
            return GetArrayText(matrixStorage.ToArray());
        }

        public static string GetMultivectorText<T>(this IMultivectorStorage<T> storage)
        {
            return GetTermsText(
                storage
                    .GetTerms()
                    .OrderByGradeIndex()
            );
        }


        public static string GetArrayText<T>(this ITextComposer<T> composer, ILinVectorStorage<T> vectorStorage)
        {
            return composer.GetArrayText(vectorStorage.ToArray());
        }
        
        public static string GetArrayText<T>(this ITextComposer<T> composer, ILinMatrixStorage<T> matrixStorage)
        {
            return composer.GetArrayText(matrixStorage.ToArray());
        }
    }
}