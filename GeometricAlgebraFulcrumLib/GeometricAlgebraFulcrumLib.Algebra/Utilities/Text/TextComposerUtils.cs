﻿using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

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
    public static string GetTermText<T>(ITextComposer<T> textComposer, uint grade, int index, IScalar<double> scalar)
    {
        return textComposer.GetTermText(grade, index, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermText<T>(ITextComposer<T> textComposer, uint grade, int index, IScalar<T> scalar)
    {
        return textComposer.GetTermText(grade, index, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermText<T>(ITextComposer<T> textComposer, IndexSet id, IScalar<double> scalar)
    {
        return textComposer.GetTermText(id, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermText<T>(ITextComposer<T> textComposer, IndexSet id, IScalar<T> scalar)
    {
        return textComposer.GetTermText(id, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermText<T>(ITextComposer<T> textComposer, XGaBasisBlade basisBlade, IScalar<double> scalar)
    {
        return textComposer.GetTermText(basisBlade, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermText<T>(ITextComposer<T> textComposer, XGaBasisBlade basisBlade, IScalar<T> scalar)
    {
        return textComposer.GetTermText(basisBlade, scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetBasisVectorText(this int index)
    {
        return new[] { index }.GetBasisBladeText();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetBasisBladeText(uint grade, ulong index)
    {
        //return $"<Grade:{grade}, Index:{index}>";
        return index.IndexToCombinadic((int)grade).Concatenate(", ", "<", ">");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetBasisBladeText(this IndexSet id)
    {
        return id.Concatenate(", ", "<", ">");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetBasisBladeText(this IEnumerable<int> indexList)
    {
        return indexList.Concatenate(", ", "<", ">");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetScalarText<T>(this T scalar)
    {
        return scalar?.ToString() ?? string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermText<T>(uint grade, int index, T scalar)
    {
        return GetTermText(grade, (ulong)index, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermText<T>(uint grade, ulong index, T scalar)
    {
        return new StringBuilder()
            .Append($"'{scalar.GetScalarText()}'")
            .Append(GetBasisBladeText(grade, index))
            .ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermText<T>(IndexSet id, T scalar)
    {
        return new StringBuilder()
            .Append($"'{scalar}'")
            .Append(id.GetBasisBladeText())
            .ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermText<T>(this KeyValuePair<IndexSet, T> idScalarPair)
    {
        return GetTermText(
            idScalarPair.Key,
            idScalarPair.Value
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermText<T>(this Tuple<IndexSet, T> idScalarTuple)
    {
        return GetTermText(
            idScalarTuple.Item1,
            idScalarTuple.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermText<T>(this Tuple<uint, ulong, T> gradeIndexScalarTuple)
    {
        return GetTermText(
            gradeIndexScalarTuple.Item1,
            gradeIndexScalarTuple.Item2,
            gradeIndexScalarTuple.Item3
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
    public static string GetTermsText<T>(this IEnumerable<KeyValuePair<IndexSet, T>> idScalarPairs)
    {
        return idScalarPairs
            .Select(GetTermText)
            .ConcatenateText(", ");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermsText<T>(this IEnumerable<Tuple<IndexSet, T>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetTermText)
            .ConcatenateText(", ");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTermsText<T>(this IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarTuples)
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
    public static string GetTermsText<T>(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
    {
        return indexScalarTuples
            .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Item1, indexScalarPair.Item2))
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

            composer.Append(array[i].GetScalarText());
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

                composer.Append(array[i, j].GetScalarText());
            }
        }

        return composer.ToString();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static string GetArrayText<T>(this ILinMatrixStorage<T> matrixStorage)
    //{
    //    return GetArrayText(matrixStorage.ToArray());
    //}

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