﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Text;

public class LaTeXComposer<T>
    : ITextComposer<T>
{
    public string BasisName { get; set; }
        = @"\boldsymbol{e}";

    public IScalarProcessor<T> ScalarProcessor { get; }

    public LaTeXComposerBasisFormat BasisFormat { get; set; }
        = LaTeXComposerBasisFormat.CommaSeparated;


    public LaTeXComposer(IScalarProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
    }


    public string GetBasisBladeText(RGaBasisBlade basisBlade)
    {
        return GetBasisBladeText(basisBlade.Id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual string GetAngleText(Float64PlanarAngle angle)
    {
        var angleText = GetScalarText(ScalarProcessor.GetScalarFromNumber(angle.Degrees));

        return $"{angleText}^{{\\circ}}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetScalarText(Scalar<T> scalar)
    {
        return GetScalarText(scalar.ScalarValue);
    }
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public virtual string GetScalarText(double scalar)
    //{
    //    return ScalarProcessor.ToText(
    //        ScalarProcessor.GetScalarFromNumber(scalar)
    //    );
    //}
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual string GetScalarText(Float64Scalar scalar)
    {
        return ScalarProcessor.ToText(
            ScalarProcessor.GetScalarFromNumber(scalar.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual string GetScalarText(T scalar)
    {
        return ScalarProcessor.ToText(scalar);
    }
        
    public string GetTermText(uint grade, int index, T scalar)
    {
        return GetTermText(
            ((ulong) index).BasisBladeIndexToId(grade),
            scalar
        );
    }

    public string GetTermText(uint grade, ulong index, T scalar)
    {
        return GetTermText(
            index.BasisBladeIndexToId(grade),
            scalar
        );
    }

    public string GetTermText(RGaKvIndexScalarRecord<T> idScalarTuple)
    {
        return GetTermText(
            idScalarTuple.KvIndex,
            idScalarTuple.Scalar
        );
    }

    public string GetTermText(RGaGradeKvIndexScalarRecord<T> gradeIndexScalarTuple)
    {
        return GetTermText(
            gradeIndexScalarTuple.Grade,
            gradeIndexScalarTuple.KvIndex,
            gradeIndexScalarTuple.Scalar
        );
    }

    public string GetTermText(RGaBasisBlade basisBlade, T scalar)
    {
        return GetTermText(
            basisBlade.Id,
            scalar
        );
    }

    public string GetTermText(KeyValuePair<ulong, T> term)
    {
        return GetTermText(
            term.Key,
            term.Value
        );
    }

    public string GetTermsText(IEnumerable<RGaKvIndexScalarRecord<T>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetTermText)
            .ConcatenateText(" + ");
    }

    public string GetTermsText(IEnumerable<RGaGradeKvIndexScalarRecord<T>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetTermText)
            .ConcatenateText(" + ");
    }

    public string GetTermsText(uint grade, IEnumerable<RGaKvIndexScalarRecord<T>> indexScalarTuples)
    {
        return indexScalarTuples
            .Select(indexScalarPair => GetTermText(grade, indexScalarPair.KvIndex, indexScalarPair.Scalar))
            .ConcatenateText(" + ");
    }
        
    public string GetArrayText(IReadOnlyList<T> array)
    {
        var colsCount = array.Count;

        var textComposer = new StringBuilder();

        var ccc = string.Concat(
            Enumerable.Repeat("c", array.Count)
        );

        textComposer
            .AppendLine(@"\left(\begin{array}{" + ccc + "}");

        for (var j = 0; j < colsCount; j++)
        {
            textComposer.Append(GetScalarText(array[j]));

            if (j < colsCount - 1)
                textComposer.Append(@" & ");
        }
            
        textComposer
            .AppendLine()
            .AppendLine(@"\end{array}\right)");

        return textComposer.ToString();
    }

    public string GetArrayText(T[,] array)
    {
        var rowsCount = array.GetLength(0);
        var colsCount = array.GetLength(1);

        var textComposer = new StringBuilder();

        var ccc = string.Concat(
            Enumerable.Repeat("c", array.GetLength(1))
        );

        textComposer
            .AppendLine(@"\left(\begin{array}{" + ccc + "}");

        for (var i = 0; i < rowsCount; i++)
        {
            for (var j = 0; j < colsCount; j++)
            {
                textComposer.Append(GetScalarText(array[i, j]));

                if (j < colsCount - 1)
                    textComposer.Append(@" & ");
            }

            if (i < rowsCount - 1)
                textComposer.AppendLine(@" \\");
        }
            
        textComposer
            .AppendLine()
            .AppendLine(@"\end{array}\right)");

        return textComposer.ToString();
    }

    public string GetMultivectorText(IMultivectorStorage<T> storage)
    {
        return GetTermsText(
            ScalarProcessor
                .GetNotZeroTerms(storage)
                .OrderByGradeIndex()
        );
    }

    public string GetBasisVectorText(int index)
    {
        return GetBasisBladeText(new []{index.BasisVectorIndexToId()});
    }

    public string GetBasisVectorText(ulong index)
    {
        return GetBasisBladeText(
            new []{index.BasisVectorIndexToId()}
        );
    }

    public string GetBasisBladeText(ulong id)
    {
        return GetBasisBladeText(
            id.PatternToPositions().Select(i => (ulong) i)
        );
    }

    public string GetBasisBladeText(uint grade, ulong index)
    {
        return GetBasisBladeText(
            index.IndexToCombinadic((int) grade).Select(i => (ulong) i)
        );
    }

    public string GetBasisBladeText(IEnumerable<ulong> indexList)
    {
        if (BasisFormat == LaTeXComposerBasisFormat.OuterProduct)
            return indexList
                .Select(i => $"{BasisName}_{{{i + 1}}}")
                .Concatenate(@" \wedge ");

        var basisSubscript = 
            BasisFormat == LaTeXComposerBasisFormat.CommaSeparated 
                ? indexList.Select(i => i + 1).Concatenate(",") 
                : indexList.Select(i => i + 1).Concatenate();

        return $"{BasisName}_{{{basisSubscript}}}";
    }

    public string GetVectorTermText(int index, T value)
    {
        var valueText = GetScalarText(value);
        var basisText = GetBasisVectorText(index);

        return $@"\left( {valueText} \right) {basisText}";
    }

    public string GetTermText(ulong id, T value)
    {
        var valueText = GetScalarText(value);

        if (id == 0)
            return $@"\left( {valueText} \right)";

        var basisText = GetBasisBladeText(id);

        return $@"\left( {valueText} \right) {basisText}";
    }

    public string GetTermsText(IEnumerable<KeyValuePair<ulong, T>> termsList)
    {
        var termsArray = 
            termsList.OrderByGradeIndex().ToArray();

        return termsArray.Length == 0
            ? "0"
            : termsArray
                .Select(t => GetTermText(t.Key, t.Value))
                .Concatenate(" + ");
    }

    public string GetArrayDisplayEquationText(T[,] array)
    {
        var textComposer = new StringBuilder();

        var code = GetArrayText(array).Trim();

        return textComposer
            .AppendLine(@"\[")
            .AppendLine(code)
            .AppendLine(@"\]")
            .AppendLine()
            .ToString();
    }

    public string GetArrayDisplayEquationText(ILinMatrixStorage<T> array)
    {
        var textComposer = new StringBuilder();

        var code = GetArrayText(array.ToArray()).Trim();

        return textComposer
            .AppendLine(@"\[")
            .AppendLine(code)
            .AppendLine(@"\]")
            .AppendLine()
            .ToString();
    }
        
    public string GetTermsEquationsArrayText(string rightHandSide, IEnumerable<KeyValuePair<ulong, T>> termsList)
    {
        var textComposer = new StringBuilder();

        textComposer.AppendLine(@"\begin{eqnarray*}");

        var termsArray = 
            termsList.OrderByGradeIndex().ToArray();

        var j = 0;
        foreach (var term in termsArray)
        {
            var termCode = GetTermText(term);

            var line = j == 0
                ? $@"{rightHandSide.Trim()} & = & {termCode}"
                : $@" & + & {termCode}";

            if (j < termsArray.Length - 1)
                line += @"\\";

            textComposer.AppendLine(line);

            j++;
        }

        textComposer.AppendLine(@"\end{eqnarray*}");

        return textComposer.ToString();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine()
    {
        Console.WriteLine();

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine(string vText)
    {
        Console.WriteLine(vText);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine(T v, string vText)
    {
        Console.WriteLine(@$"${vText} = {GetScalarText(v)}$");

        return this;
    }
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public LaTeXComposer<T> ConsoleWriteLine(GaVector<T> v, string vText)
    //{
    //    Console.WriteLine(@$"${vText} = {GetMultivectorText(v.VectorStorage)}$");

    //    return this;
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public LaTeXComposer<T> ConsoleWriteLine(GaBivector<T> v, string vText)
    //{
    //    Console.WriteLine(@$"${vText} = {GetMultivectorText(v.BivectorStorage)}$");

    //    return this;
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public LaTeXComposer<T> ConsoleWriteLine(GaKVector<T> v, string vText)
    //{
    //    Console.WriteLine(@$"${vText} = {GetMultivectorText(v.KVectorStorage)}$");

    //    return this;
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public LaTeXComposer<T> ConsoleWriteLine(GaMultivector<T> v, string vText)
    //{
    //    Console.WriteLine(@$"${vText} = {GetMultivectorText(v.MultivectorStorage)}$");

    //    return this;
    //}
}