using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.Text
{
    public static class TextComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TextComposer<T> CreateTextComposer<T>(this IScalarProcessor<T> processor)
        {
            return new TextComposer<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaTeXComposer<T> CreateLaTeXComposer<T>(this IScalarProcessor<T> processor)
        {
            return new LaTeXComposer<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBasisVectorText(this int index)
        {
            return GetBasisBladeText(
                new []{index + 1}
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBasisBladeText(this ulong id)
        {
            return GetBasisBladeText(
                id.PatternToPositions().Select(i => i + 1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBasisBladeText(uint grade, ulong index)
        {
            //return $"<Grade:{grade}, Index:{index}>";
            return GetBasisBladeText(
                index.IndexToCombinadic((int) grade).Select(i => i + 1)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBasisBladeText(this IIndexSet id)
        {
            return GetBasisBladeText(
                id.Select(i => i + 1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBasisBladeText(this RGaBasisBlade basisBlade)
        {
            return GetBasisBladeText(basisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBasisBladeText(this XGaBasisBlade basisBlade)
        {
            return GetBasisBladeText(basisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBasisBladeText(this IEnumerable<int> indexList)
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
        public static string GetTermText<T>(IIndexSet id, T scalar)
        {
            return new StringBuilder()
                .Append($"'{scalar}'")
                .Append(GetBasisBladeText(id))
                .ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermText<T>(this KeyValuePair<ulong, T> idScalarPair)
        {
            return GetTermText(
                idScalarPair.Key, 
                idScalarPair.Value
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermText<T>(this KeyValuePair<IIndexSet, T> idScalarPair)
        {
            return GetTermText(
                idScalarPair.Key, 
                idScalarPair.Value
            );
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
        public static string GetTermText<T>(XGaBasisBlade basisBlade, T scalar)
        {
            return GetTermText(
                basisBlade.Id,
                scalar
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermsText<T>(this IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            return idScalarPairs
                .Select(GetTermText)
                .ConcatenateText(", ");
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTermsText<T>(this IEnumerable<KeyValuePair<IIndexSet, T>> idScalarPairs)
        {
            return idScalarPairs
                .Select(GetTermText)
                .ConcatenateText(", ");
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
        public static string GetArrayText<T>(this LinVector<T> array)
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

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static string GetArrayText<T>(this ILinMatrixStorage<T> matrixStorage)
        //{
        //    return GetArrayText(matrixStorage.ToArray());
        //}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetMultivectorText<T>(this RGaMultivector<T> mv)
        {
            return GetTermsText(
                mv
                    .IdScalarPairs
                    .OrderByGradeIndex()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetMultivectorText<T>(this XGaMultivector<T> mv)
        {
            return GetTermsText(
                mv
                    .IdScalarPairs
                    .OrderByGradeIndex()
            );
        }
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static string GetArrayText<T>(this ITextComposer<T> composer, ILinMatrixStorage<T> matrixStorage)
        //{
        //    return composer.GetArrayText(matrixStorage.ToArray());
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static string GetArrayText<T>(this ITextComposer<T> composer, IMatrixStorageContainer<T> container)
        //{
        //    return composer.GetArrayText(
        //        container.GetLinMatrixStorage().ToArray()
        //    );
        //}
    }
}
