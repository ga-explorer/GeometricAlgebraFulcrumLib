using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using DataStructuresLib;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.TextComposers
{
    public enum GaLaTeXComposerBasisFormat
    {
        CommaSeparated,
        Concatenated,
        OuterProduct
    }

    public abstract class GaLaTeXComposer<T>
        : IGaTextComposer<T>
    {
        public string BasisName { get; set; }
            = @"\boldsymbol{e}";

        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public GaLaTeXComposerBasisFormat BasisFormat { get; set; }
            = GaLaTeXComposerBasisFormat.CommaSeparated;


        protected GaLaTeXComposer([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        public string GetBasisBladeText(IGaBasisBlade basisBlade)
        {
            return GetBasisBladeText(basisBlade.Id);
        }

        public abstract string GetScalarText(T scalar);
        
        public string GetTermText(uint grade, int index, T scalar)
        {
            return GetTermText(
                GaBasisUtils.BasisBladeId(grade, (ulong) index),
                scalar
            );
        }

        public string GetTermText(uint grade, ulong index, T scalar)
        {
            return GetTermText(
                GaBasisUtils.BasisBladeId(grade, index),
                scalar
            );
        }

        public string GetTermText(KeyValuePair<ulong, T> idScalarPair)
        {
            return GetTermText(
                idScalarPair.Key,
                idScalarPair.Value
            );
        }

        public string GetTermText(Tuple<ulong, T> idScalarTuple)
        {
            return GetTermText(
                idScalarTuple.Item1,
                idScalarTuple.Item2
            );
        }

        public string GetTermText(Tuple<uint, ulong, T> gradeIndexScalarTuple)
        {
            return GetTermText(
                gradeIndexScalarTuple.Item1,
                gradeIndexScalarTuple.Item2,
                gradeIndexScalarTuple.Item3
            );
        }

        public string GetTermText(IGaBasisBlade basisBlade, T scalar)
        {
            return GetTermText(
                basisBlade.Id,
                scalar
            );
        }

        public string GetTermText(GaTerm<T> term)
        {
            return GetTermText(
                term.BasisBlade.Id,
                term.Scalar
            );
        }

        public string GetTermsText(IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            return idScalarPairs
                .Select(GetTermText)
                .ConcatenateText(" + ");
        }

        public string GetTermsText(IEnumerable<Tuple<ulong, T>> idScalarTuples)
        {
            return idScalarTuples
                .Select(GetTermText)
                .ConcatenateText(" + ");
        }

        public string GetTermsText(IEnumerable<Tuple<uint, ulong, T>> idScalarTuples)
        {
            return idScalarTuples
                .Select(GetTermText)
                .ConcatenateText(" + ");
        }

        public string GetTermsText(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            return indexScalarPairs
                .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Key, indexScalarPair.Value))
                .ConcatenateText(" + ");
        }

        public string GetTermsText(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            return indexScalarTuples
                .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Item1, indexScalarPair.Item2))
                .ConcatenateText(" + ");
        }
        
        public string GetArrayText(T[] array)
        {
            var colsCount = array.Length;

            var textComposer = new StringBuilder();

            var ccc = string.Concat(
                Enumerable.Repeat("c", array.GetLength(1))
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

        public string GetMultivectorText(IGasMultivector<T> storage)
        {
            return GetTermsText(
                storage
                    .GetNotZeroTerms()
                    .OrderByGradeIndex()
            );
        }

        public string GetBasisVectorText(int index)
        {
            return GetBasisBladeText(new []{1UL << index});
        }

        public string GetBasisVectorText(ulong index)
        {
            return GetBasisBladeText(
                new []{1UL << (int)index}
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
            if (BasisFormat == GaLaTeXComposerBasisFormat.OuterProduct)
                return indexList
                    .Select(i => $"{BasisName}_{{{i + 1}}}")
                    .Concatenate(@" \wedge ");

            var basisSubscript = 
                BasisFormat == GaLaTeXComposerBasisFormat.CommaSeparated 
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

        public string GetTermsText(IEnumerable<GaTerm<T>> termsList)
        {
            var termsArray = 
                termsList.OrderByGradeIndex().ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray
                    .Select(t => GetTermText(t.BasisBlade.Id, t.Scalar))
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
        
        public string GetTermsEquationsArrayText(string rightHandSide, IEnumerable<GaTerm<T>> termsList)
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
    }
}