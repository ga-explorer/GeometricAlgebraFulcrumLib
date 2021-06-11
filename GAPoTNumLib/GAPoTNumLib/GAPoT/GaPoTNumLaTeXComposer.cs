using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GAPoTNumLib.Structures;
using GAPoTNumLib.Text;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GaPoTNumLaTeXComposer
    {
        public static GaPoTNumLaTeXComposer DefaultComposer { get; }
            = new GaPoTNumLaTeXComposer()
            {
                ScalarDecimals = 7,
                BasisName = @"\boldsymbol{\mu}",
                BasisFormat = GaPoTNumLaTeXComposerBasisFormat.CommaSeparated
            };


        public int ScalarDecimals { get; set; }
            = 7;

        public string BasisName { get; set; }
            = @"\boldsymbol{\mu}";

        public GaPoTNumLaTeXComposerBasisFormat BasisFormat { get; set; }
            = GaPoTNumLaTeXComposerBasisFormat.CommaSeparated;


        public string GetCodeOfScalar(double value)
        {
            var valueText = value.ToString("G");

            if (valueText.Contains("E"))
            {
                var ePosition = valueText.IndexOf('E');

                var magnitude = double.Parse(valueText.Substring(0, ePosition));
                var magnitudeText = Math.Round(magnitude, ScalarDecimals).ToString("G");
                var exponentText = valueText.Substring(ePosition + 1);

                return $@"{magnitudeText} \times 10^{{{exponentText}}}";
            }

            return Math.Round(value, ScalarDecimals).ToString("G");
        }

        public string GetCodeOfBasisVector(int id)
        {
            return GetCodeOfBasisBlade(new []{1 << (id - 1)});
        }

        public string GetCodeOfBasisBlade(int id)
        {
            var indexList = 
                id.PatternToPositions().Select(i => i + 1).ToArray();
            
            return GetCodeOfBasisBlade(indexList);
        }

        public string GetCodeOfBasisBlade(IEnumerable<int> indexList)
        {
            if (BasisFormat == GaPoTNumLaTeXComposerBasisFormat.OuterProduct)
                return indexList.Select(i => $"{BasisName}_{{{i}}}").Concatenate(@" \wedge ");

            var basisSubscript = 
                BasisFormat == GaPoTNumLaTeXComposerBasisFormat.CommaSeparated 
                    ? indexList.Concatenate(",") 
                    : indexList.Concatenate();

            return $"{BasisName}_{{{basisSubscript}}}";
        }

        public string GetCodeOfVectorTerm(int id, double value)
        {
            var valueText = GetCodeOfScalar(value);
            var basisText = GetCodeOfBasisVector(id);

            return $@"\left( {valueText} \right) {basisText}";
        }

        public string GetCodeOfMultivectorTerm(int id, double value)
        {
            var valueText = GetCodeOfScalar(value);

            if (id == 0)
                return $@"\left( {valueText} \right)";

            var basisText = GetCodeOfBasisBlade(id);

            return $@"\left( {valueText} \right) {basisText}";
        }

        public string GetCode(IEnumerable<GaPoTNumMultivectorTerm> termsList)
        {
            var termsArray = 
                termsList.OrderByGrade().ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray
                    .Select(t => GetCodeOfMultivectorTerm(t.IDsPattern, t.Value))
                    .Concatenate(" + ");
        }

        public string GetCode(double[,] matrix)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);

            var textComposer = new StringBuilder();

            var ccc = Enumerable.Repeat("c", matrix.GetLength(1)).Concatenate();
            textComposer.AppendLine(@"\left(\begin{array}{" + ccc + "}");

            for (var i = 0; i < rowsCount; i++)
            {
                for (var j = 0; j < colsCount; j++)
                {
                    textComposer.Append(GetCodeOfScalar(matrix[i, j]));

                    if (j < colsCount - 1)
                        textComposer.Append(@" & ");
                }

                if (i < rowsCount - 1)
                    textComposer.Append(@"\\");
            }
            
            textComposer.AppendLine(@"\end{array}\right)");

            return textComposer.ToString();
        }

        public string GetDisplayEquationCode(double[,] matrix)
        {
            var textComposer = new StringBuilder();

            var code = GetCode(matrix).Trim();

            return textComposer
                .AppendLine(@"\[")
                .AppendLine(code)
                .AppendLine(@"\]")
                .AppendLine()
                .ToString();
        }

        public string GetDisplayEquationCode(string rightHandSide, double[,] matrix)
        {
            var textComposer = new StringBuilder();

            var code = GetCode(matrix).Trim();

            return textComposer
                .AppendLine(@"\[")
                .Append(rightHandSide.Trim())
                .Append(" = ")
                .AppendLine(code)
                .AppendLine(@"\]")
                .AppendLine()
                .ToString();
        }

        public string GetInlineEquationCode(IEnumerable<GaPoTNumMultivectorTerm> termsList)
        {
            var code = GetCode(termsList).Trim();

            return $"${code}$";
        }

        public string GetInlineEquationCode(string rightHandSide, IEnumerable<GaPoTNumMultivectorTerm> termsList)
        {
            var code = GetCode(termsList).Trim();

            return $"${rightHandSide.Trim()} = {code}$";
        }

        public string GetDisplayEquationCode(IEnumerable<GaPoTNumMultivectorTerm> termsList)
        {
            var textComposer = new StringBuilder();

            var code = GetCode(termsList).Trim();

            return textComposer
                .AppendLine(@"\[")
                .AppendLine(code)
                .AppendLine(@"\]")
                .AppendLine()
                .ToString();
        }

        public string GetDisplayEquationCode(string rightHandSide, IEnumerable<GaPoTNumMultivectorTerm> termsList)
        {
            var textComposer = new StringBuilder();

            var code = GetCode(termsList).Trim();

            return textComposer
                .AppendLine(@"\[")
                .Append(rightHandSide.Trim())
                .Append(" = ")
                .AppendLine(code)
                .AppendLine(@"\]")
                .AppendLine()
                .ToString();
        }

        public string GetEquationsArrayCode(string rightHandSide, IEnumerable<GaPoTNumMultivectorTerm> termsList)
        {
            var textComposer = new StringBuilder();

            textComposer.AppendLine(@"\begin{eqnarray*}");

            var termsArray = 
                termsList.OrderByGrade().ToArray();

            var j = 0;
            foreach (var term in termsArray)
            {
                var termCode = 
                    GetCodeOfMultivectorTerm(term.IDsPattern, term.Value);

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