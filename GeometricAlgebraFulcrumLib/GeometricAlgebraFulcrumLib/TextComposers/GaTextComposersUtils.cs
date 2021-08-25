using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructuresLib;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Utils;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.TextComposers
{
    public static class GaTextComposersUtils
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

        public static string GetBasisBladeText(this GaBasisBlade basisBlade)
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

        public static string GetTermText<T>(this GaRecordKeyValue<T> idScalarTuple)
        {
            return GetTermText(
                idScalarTuple.Key, 
                idScalarTuple.Value
            );
        }

        public static string GetTermText<T>(this GaRecordGradeKeyValue<T> gradeIndexScalarTuple)
        {
            return GetTermText(
                gradeIndexScalarTuple.Grade, 
                gradeIndexScalarTuple.Key,
                gradeIndexScalarTuple.Value
            );
        }

        public static string GetTermText<T>(GaBasisBlade basisBlade, T scalar)
        {
            return GetTermText(
                basisBlade.Id,
                scalar
            );
        }

        public static string GetTermText<T>(this GaBasisTerm<T> term)
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

        public static string GetTermsText<T>(this IEnumerable<GaRecordKeyValue<T>> idScalarTuples)
        {
            return idScalarTuples
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        public static string GetTermsText<T>(this IEnumerable<GaRecordGradeKeyValue<T>> gradeIndexScalarTuples)
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

        public static string GetTermsText<T>(uint grade, IEnumerable<GaRecordKeyValue<T>> indexScalarTuples)
        {
            return indexScalarTuples
                .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Key, indexScalarPair.Value))
                .ConcatenateText(", ");
        }

        public static string GetTermsText<T>(this IEnumerable<GaBasisTerm<T>> terms)
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

        public static string GetArrayText<T>(this IGaListEven<T> array)
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

        public static string GetArrayText<T>(this IGaGridEven<T> grid)
        {
            return GetArrayText(grid.ToArray());
        }

        public static string GetMultivectorText<T>(this IGaStorageMultivector<T> storage)
        {
            return GetTermsText(
                storage
                    .GetTerms()
                    .OrderByGradeIndex()
            );
        }


        public static string GetArrayText<T>(this IGaTextComposer<T> composer, IGaListEven<T> evenList)
        {
            return composer.GetArrayText(evenList.ToArray());
        }
        
        public static string GetArrayText<T>(this IGaTextComposer<T> composer, IGaGridEven<T> grid)
        {
            return composer.GetArrayText(grid.ToArray());
        }
    }
}