using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Text
{
    public static class TextComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBasisVectorText(this ulong index)
        {
            return GetBasisBladeText(
                new []{index + 1}
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBasisBladeText(this ulong id)
        {
            return GetBasisBladeText(
                id.BasisBladeIdToBasisVectorIndices().Select(i => i + 1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBasisBladeText(uint grade, ulong index)
        {
            //return $"<Grade:{grade}, Index:{index}>";
            return GetBasisBladeText(
                index.IndexToCombinadic((int) grade).Select(i => (ulong) (i + 1))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBasisBladeText(this RGaBasisBlade basisBlade)
        {
            return GetBasisBladeText(basisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBasisBladeText(this IEnumerable<ulong> indexList)
        {
            return indexList.Concatenate(", ", "<", ">");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetScalarText<T>(this T scalar)
        {
            return scalar.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermText<T>(ulong id, T scalar)
        {
            return new StringBuilder()
                .Append($"'{scalar}'")
                .Append(GetBasisBladeText(id))
                .ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermText<T>(uint grade, int index, T scalar)
        {
            return GetTermText(grade, (ulong) index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermText<T>(uint grade, ulong index, T scalar)
        {
            return new StringBuilder()
                .Append($"'{GetScalarText(scalar)}'")
                .Append(GetBasisBladeText(grade, index))
                .ToString();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermText<T>(this RGaKvIndexScalarRecord<T> idScalarTuple)
        {
            return GetTermText(
                idScalarTuple.KvIndex, 
                idScalarTuple.Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermText<T>(this RGaGradeKvIndexScalarRecord<T> gradeIndexScalarTuple)
        {
            return GetTermText(
                gradeIndexScalarTuple.Grade, 
                gradeIndexScalarTuple.KvIndex,
                gradeIndexScalarTuple.Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermText<T>(RGaBasisBlade basisBlade, T scalar)
        {
            return GetTermText(
                basisBlade.Id,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermText<T>(this KeyValuePair<ulong, T> term)
        {
            return GetTermText(
                term.Key,
                term.Value
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermsText<T>(this IEnumerable<RGaKvIndexScalarRecord<T>> idScalarTuples)
        {
            return idScalarTuples
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermsText<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            return gradeIndexScalarTuples
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermsText<T>(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            return indexScalarPairs
                .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Key, indexScalarPair.Value))
                .ConcatenateText(", ");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermsText<T>(uint grade, IEnumerable<RGaKvIndexScalarRecord<T>> indexScalarTuples)
        {
            return indexScalarTuples
                .Select(indexScalarPair => GetTermText(grade, indexScalarPair.KvIndex, indexScalarPair.Scalar))
                .ConcatenateText(", ");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermsText<T>(this IEnumerable<KeyValuePair<ulong, T>> terms)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetArrayText<T>(this ILinMatrixStorage<T> matrixStorage)
        {
            return GetArrayText(matrixStorage.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetMultivectorText<T>(this IMultivectorStorage<T> storage)
        {
            return GetTermsText(
                storage
                    .GetTerms()
                    .OrderByGradeIndex()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetArrayText<T>(this ITextComposer<T> composer, ILinVectorStorage<T> vectorStorage)
        {
            return composer.GetArrayText(vectorStorage.ToArray());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetArrayText<T>(this ITextComposer<T> composer, ILinMatrixStorage<T> matrixStorage)
        {
            return composer.GetArrayText(matrixStorage.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetArrayText<T>(this ITextComposer<T> composer, IMatrixStorageContainer<T> container)
        {
            return composer.GetArrayText(
                container.GetLinMatrixStorage().ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetMultivectorText<T>(this ITextComposer<T> composer, IMultivectorStorageContainer<T> container)
        {
            return composer.GetMultivectorText(
                container.GetMultivectorStorage()
            );
        }
    }
}
